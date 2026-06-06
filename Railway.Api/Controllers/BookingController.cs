using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Railway.Api.Models;

namespace Railway.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly DatabaseProjectContext _context;

        public BookingController(DatabaseProjectContext context)
        {
            _context = context;
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchTrains(int fromStation, int toStation, DateOnly date)
        {
            var availableTrains = await _context.Trainschedules
                .Include(s => s.Train)
                .Include(s => s.Route)
                .Where(s => s.Date == date)
                .Select(s => new {
                    ScheduleId = s.ScheduleId,
                    TrainName = s.Train.Name,
                    RouteName = s.Route.RouteName,
                    DepartureTime = s.DepartureTime,
                    AvailableDate = s.Date
                })
                .ToListAsync();

            if (!availableTrains.Any()) return NotFound("No flights matching these criteria were found..");
            return Ok(availableTrains);
        }
        [HttpPost("CreateReservation")]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationRequest request)
        {
            // A passenger can have a maximum of 5 active reservations.
            var activeReservationsCount = await _context.Reservations
                .CountAsync(r => r.PassengerId == request.PassengerId && 
                                 (r.ReservationStatus == "Pending" || r.ReservationStatus == "Paid"));

            if (activeReservationsCount >= 5)
            {
                return BadRequest("A passenger can have a maximum of 5 active reservations.");
            }

            var isSeatTaken = await _context.Seats
                .Include(s => s.Reservation)
                .AnyAsync(s => s.Reservation.ScheduleId == request.ScheduleId && 
                               s.SeatNumber == request.SeatNumber && 
                               s.Reservation.ReservationStatus != "Cancelled");

            if (isSeatTaken)
            {
                return BadRequest("The seat you selected is already reserved for this trip. Please choose another seat.");
            }
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // A new reservation record is being created.
                var newReservation = new Reservation
                {
                    PassengerId = request.PassengerId,
                    ScheduleId = request.ScheduleId,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    ReservationStatus = "Pending"
                };

                _context.Reservations.Add(newReservation);
                await _context.SaveChangesAsync();

                // The selected seat is being linked to the reservation.
                var newSeat = new Seat
                {
                    ReservationId = newReservation.ReservationId,
                    SeatNumber = request.SeatNumber,
                    CoachType = request.CoachType
                };

                _context.Seats.Add(newSeat);
                await _context.SaveChangesAsync();

                // If everything went well, commit the changes to the database
                await transaction.CommitAsync();

                return Ok(new { 
                    Message = "Your reservation and seat allocation process has been successfully completed.!", 
                    ReservationId = newReservation.ReservationId,
                    Status = newReservation.ReservationStatus
                });
            }
            catch (Exception)
            {
                // If an error occurs in the background, roll back all operations (Rollback)
                await transaction.RollbackAsync();
                return StatusCode(500, "An error occurred while creating the reservation. Operations have been rolled back.");
            }
        }
        [HttpPost("JoinWaitingList")]
        public async Task<IActionResult> JoinWaitingList([FromBody] JoinWaitlistRequest request)
        {
            try
            {
                var exists = await _context.Waitinglists
                    .AnyAsync(w => w.PassengerId == request.PassengerId && w.ScheduleId == request.ScheduleId);

                if (exists)
                {
                    return BadRequest("You are already on the waiting list for this trip.");
                }

                // Create a new waiting list entry
                var newWaitlist = new Waitinglist
                {
                    PassengerId = request.PassengerId,
                    ScheduleId = request.ScheduleId,
                    DateAdded = DateTime.Now 
                };

                _context.Waitinglists.Add(newWaitlist);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Successfully added to the waiting list." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPut("CancelReservation/{reservationId}")]
        public async Task<IActionResult> CancelReservation(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation == null) return NotFound("The reservation you want to cancel was not found.");

            reservation.ReservationStatus = "Cancelled";
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Your reservation has been successfully cancelled. Your seat has been released." });
        }

        //  Make Payment and Update Status
        [HttpPut("PayReservation/{reservationId}")]
        public async Task<IActionResult> PayReservation(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            
            if (reservation == null) return NotFound("The reservation you want to pay for was not found.");
            if (reservation.ReservationStatus == "Paid") return BadRequest("This reservation has already been paid for.");

            reservation.ReservationStatus = "Paid";
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Payment successfully processed. Your reservation has been confirmed." });
        }
    
        // Ticket Price and Discount Calculator
        [HttpGet("CalculatePrice")]
        public async Task<IActionResult> CalculatePrice(int passengerId, decimal basePrice, bool hasDependent)
        {
            decimal finalPrice = basePrice;
            string discountMessage = "No discount applied.";

            var loyaltyAccount = await _context.Loyaltyaccounts.FirstOrDefaultAsync(l => l.PassengerId == passengerId);
            
            if (loyaltyAccount != null)
            {
                if (loyaltyAccount.Class == "Gold") 
                { 
                    finalPrice -= basePrice * 0.25m; 
                    discountMessage = "Gold Class (%25 Discount)"; 
                }
                else if (loyaltyAccount.Class == "Silver") 
                { 
                    finalPrice -= basePrice * 0.10m; 
                    discountMessage = "Silver Class (%10 Discount)"; 
                }
                else if (loyaltyAccount.Class == "Green") 
                { 
                    finalPrice -= basePrice * 0.05m; 
                    discountMessage = "Green Class (%5 Discount)"; 
                }
            }

            if (hasDependent)
            {
            
                finalPrice -= basePrice * 0.25m;
                discountMessage += " + Dependent Passenger (%25 Discount)";
            }

            if (finalPrice < 0) finalPrice = 0;

            return Ok(new { 
                OriginalPrice = $"{basePrice} TL", 
                FinalPrice = $"{finalPrice} TL", 
                AppliedDiscounts = discountMessage 
            });
        }
    }

    public class ReservationRequest
    {
        public int PassengerId { get; set; }
        public int ScheduleId { get; set; }
        public string SeatNumber { get; set; } = null!;
        public string CoachType { get; set; } = null!;
    }
    public class JoinWaitlistRequest
    {
        public int PassengerId { get; set; }
        public int ScheduleId { get; set; }
    }
} 