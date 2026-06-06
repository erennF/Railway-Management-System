using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Railway.Api.Models;

namespace Railway.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengersController : ControllerBase
    {
        private readonly DatabaseProjectContext _context;

        public PassengersController(DatabaseProjectContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPassengers()
        {
            var passengers = await _context.Passengers.ToListAsync();
           
            if (passengers == null || !passengers.Any())
            {
                return NotFound("No passengers found in the system.");
            }

            return Ok(passengers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPassenger(int id)
        {
            var passenger = await _context.Passengers.FindAsync(id);
            if (passenger == null) return NotFound("Passenger not found.");
            
            return Ok(new {
                passenger.PassengerId,
                passenger.Name,
                passenger.Email
            });
        }
        
        [HttpGet("{passengerId}/Loyalty")]
        public async Task<IActionResult> GetLoyaltyInfo(int passengerId)
        {
            var loyalty = await _context.Loyaltyaccounts
                .Where(l => l.PassengerId == passengerId)
                .Select(l => new {
                    l.Class,
                    l.TotalMiles
                })
                .FirstOrDefaultAsync();

            if (loyalty == null) return NotFound("No loyalty account found for the specified passenger.");
            return Ok(loyalty);
        }
    }
}