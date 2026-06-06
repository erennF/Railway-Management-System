using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Railway.Api.Models;

namespace Railway.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerOpsController : ControllerBase
    {
        private readonly DatabaseProjectContext _context;

        public PassengerOpsController(DatabaseProjectContext context)
        {
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterPassenger([FromBody] Passenger newPassenger)
        {
            _context.Passengers.Add(newPassenger);
            await _context.SaveChangesAsync();
            var loyalty = new Loyaltyaccount
            {
                PassengerId = newPassenger.PassengerId,
                Class = "Green",
                TotalMiles = 0
            };
            _context.Loyaltyaccounts.Add(loyalty);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "The passenger was successfully registered and a loyalty account was created.", PassengerId = newPassenger.PassengerId });
        }

        [HttpPost("AddDependent")]
        public async Task<IActionResult> AddDependent([FromBody] Dependent dependent)
        {
            var parentExists = await _context.Passengers.AnyAsync(p => p.PassengerId == dependent.PassengerId);
            if (!parentExists) return NotFound("The parent passenger was not found.");

            _context.Dependents.Add(dependent);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "The dependent passenger was successfully added." });
        }

        [HttpPost("JoinWaitlist")]
        public async Task<IActionResult> JoinWaitlist([FromBody] Waitinglist waitlistRecord)
        {
            _context.Waitinglists.Add(waitlistRecord);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "You have been successfully added to the waiting list. You will be notified if a spot becomes available." });
        }
    }
}