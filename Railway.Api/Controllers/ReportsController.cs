using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Railway.Api.Models;

namespace Railway.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly DatabaseProjectContext _context;

        public ReportsController(DatabaseProjectContext context)
        {
            _context = context;
        }

        // REPORT 1: Active trains on a specific date
        [HttpGet("ActiveTrains/{date}")]
        public async Task<IActionResult> GetActiveTrains(DateOnly date)
        {
            var activeSchedules = await _context.Trainschedules
                .Include(s => s.Train) 
                .Include(s => s.Route) 
                .Where(s => s.Date == date)
                .Select(s => new {
                    ScheduleId = s.ScheduleId,
                    TrainName = s.Train.Name,
                    TrainType = s.Train.TrainType,
                    RouteName = s.Route.RouteName,
                    DepartureTime = s.DepartureTime
                })
                .ToListAsync();

            if (!activeSchedules.Any()) return NotFound("No active schedules found for the specified date.");
            return Ok(activeSchedules);
        }

        // REPORT 2: Ordered list of stations for a selected route
        [HttpGet("RouteStations/{routeId}")]
        public async Task<IActionResult> GetRouteStations(int routeId)
        {
            var stations = await _context.Routestops
                .Include(rs => rs.Station)
                .Where(rs => rs.RouteId == routeId)
                .OrderBy(rs => rs.SequenceNumber) 
                .Select(rs => new {
                    StopSequence = rs.SequenceNumber,
                    StationName = rs.Station.Name,
                    StationType = rs.Station.StationType
                })
                .ToListAsync();

            if (!stations.Any()) return NotFound("No stations found for the specified route.");
            return Ok(stations);
        }

        // REPORT 3: Reservation details for a selected passenger
        [HttpGet("PassengerReservations/{passengerId}")]
        public async Task<IActionResult> GetPassengerReservations(int passengerId)
        {
            var reservations = await _context.Reservations
                .Include(r => r.Schedule).ThenInclude(s => s.Train)
                .Include(r => r.Schedule).ThenInclude(s => s.Route)
                .Include(r => r.Seats)  
                .Include(r => r.Luggage)  
                .Where(r => r.PassengerId == passengerId)
                .Select(r => new {
                    ReservationId = r.ReservationId,
                    Status = r.ReservationStatus,
                    TrainName = r.Schedule.Train.Name,
                    RouteName = r.Schedule.Route.RouteName,
                    TravelDate = r.Date,
                    Seats = r.Seats.Select(seat => new { seat.SeatNumber, seat.CoachType }),
                    TotalLuggageWeight = r.Luggage.Sum(l => l.Weight)
                })
                .ToListAsync();

            if (!reservations.Any()) return NotFound("No reservations found for the specified passenger.");
            return Ok(reservations);
        }
    
    // REPORT 4: Waitlist for a specific train schedule
        [HttpGet("Waitlist/{scheduleId}")]
        public async Task<IActionResult> GetWaitlist(int scheduleId)
        {
            var waitlistedPassengers = await _context.Waitinglists
                .Include(w => w.Passenger)
                .Where(w => w.ScheduleId == scheduleId)
                .OrderBy(w => w.WaitlistId) 
                .Select(w => new {
                    w.Passenger.Name,
                    w.Passenger.Email,
                    w.DateAdded
                })
                .ToListAsync();

            if (!waitlistedPassengers.Any()) return NotFound("No passengers found on the waiting list for the specified schedule.");
            return Ok(waitlistedPassengers);
        }

       // REPORT 5: Load Factor Calculation for Trains
        [HttpGet("LoadFactor/{scheduleId}")]
        public async Task<IActionResult> GetLoadFactor(int scheduleId)
        {
         
            var totalReservations = await _context.Reservations
                .Where(r => r.ScheduleId == scheduleId && r.ReservationStatus != "Cancelled")
                .CountAsync();

            var scheduleDetails = await _context.Trainschedules
                .Where(s => s.ScheduleId == scheduleId)
                .Select(s => new { 
                    TrainName = s.Train.Name,
                    Capacity = _context.Passengertrains
                                .Where(pt => pt.TrainId == s.TrainId)
                                .Select(pt => pt.PassengerCapacity)
                                .FirstOrDefault()
                })
                .FirstOrDefaultAsync();

            if (scheduleDetails == null) return NotFound("Schedule not found.");

            // Occupancy Rate (Percentage) = (Booked Seats / Total Capacity) * 100
            double occupancyRate = scheduleDetails.Capacity > 0 
                ? Math.Round((double)totalReservations / scheduleDetails.Capacity * 100, 2) 
                : 0;

            return Ok(new {
                TrainName = scheduleDetails.TrainName,
                TotalCapacity = scheduleDetails.Capacity,
                BookedSeats = totalReservations,
                OccupancyPercentage = $"{occupancyRate}%"
            });
        }
        

        // REPORT 6: Most Frequent Traveling Loyal Customers (Bronze, Silver, Gold categorized)
        [HttpGet("TopLoyaltyCustomers")]
        public async Task<IActionResult> GetTopLoyaltyCustomers()
        {
            var topCustomers = await _context.Loyaltyaccounts
                .Include(l => l.Passenger)
                .OrderByDescending(l => l.TotalMiles) 
                .Take(10) 
                .Select(l => new {
                    l.Passenger.Name,
                    l.Class,
                    l.TotalMiles
                })
                .ToListAsync();

            if (!topCustomers.Any()) return NotFound("No loyal customers found.");
            return Ok(topCustomers);
        }
        // REPORT 7: Dependents (family members) traveling on a specific date
        [HttpGet("DependentsOnDate/{date}")]
        public async Task<IActionResult> GetDependentsOnDate(DateOnly date)
        {
            var travelingDependents = await _context.Reservations
                .Include(r => r.Passenger).ThenInclude(p => p.Dependents)
                .Where(r => r.Date == date && r.ReservationStatus != "Cancelled")
                .SelectMany(r => r.Passenger.Dependents.Select(d => new {
                    PassengerName = r.Passenger.Name,
                    DependentName = d.Name,
                    TravelDate = r.Date,
                    ReservationStatus = r.ReservationStatus
                }))
                .Distinct()
                .ToListAsync();

            if (!travelingDependents.Any()) return NotFound("No dependents found for the specified date.");
            return Ok(travelingDependents);
        }
        // REPORT 8: Selected Train's Maintenance History
        [HttpGet("MaintenanceHistory/{trainId}")]
        public async Task<IActionResult> GetMaintenanceHistory(int trainId)
        {
            var records = await _context.Maintenancerecords
                .Where(m => m.TrainId == trainId)
                .OrderByDescending(m => m.Date)
                .Select(m => new {
                    Date = m.Date,
                    Details = m.Details
                })
                .ToListAsync();

            if (!records.Any()) return NotFound("No maintenance history found for the specified train.");
            return Ok(records);
        }
        // REPORT 9: Selected Country's Freight Shipment History
        [HttpGet("FreightByCountry/{countryId}")]
        public async Task<IActionResult> GetFreightByCountry(int countryId)
        {
            var shipments = await _context.Customsclearances
                .Include(c => c.Shipment)
                .Include(c => c.Station)
                .Where(c => c.Station.CountryId == countryId)
                .Select(c => new {
                    CustomsDate = c.Date,
                    StationName = c.Station.Name,
                    ShipperName = c.Shipment.Shipper,
                    CargoWeight = c.Shipment.Weight,
                    ClearanceStatus = c.Status
                })
                .ToListAsync();

            if (!shipments.Any()) return NotFound("No freight shipment records found for the specified country.");
            return Ok(shipments);
        }
        // REPORT 10: Staff assignments for a specific date
        [HttpGet("StaffAssignments/{date}")]
        public async Task<IActionResult> GetStaffAssignments(DateOnly date)
        {
            var assignments = await _context.Staffassignments
                .Include(sa => sa.Staff)
                .Include(sa => sa.Schedule).ThenInclude(s => s.Train)
                .Where(sa => sa.AssignmentDate == date)
                .Select(sa => new {
                    StaffName = sa.Staff.Name,
                    Role = sa.Staff.Role,
                    TrainName = sa.Schedule.Train.Name,
                    DepartureTime = sa.Schedule.DepartureTime
                })
                .ToListAsync();

            if (!assignments.Any()) return NotFound("No staff assignments found for the specified date.");
            return Ok(assignments);
        }
}
}