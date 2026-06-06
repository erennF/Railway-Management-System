using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using Railway.Api.Models;

namespace Railway.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly DatabaseProjectContext _context; 

    public AuthController(DatabaseProjectContext context)
    {
        _context = context;
    }

    [HttpPost("Login")]
    
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        Console.WriteLine("--- LOGIN METHOD ENTERED.---");
        Console.WriteLine("Email received by the backend: " + request.Email);
        if (string.IsNullOrEmpty(request.Email))
        {
            return BadRequest("Email is required.");
        }

        var passenger = await _context.Passengers
            .FirstOrDefaultAsync(p => p.Email == request.Email);
        
        if (passenger != null) 
        {
            return Ok(new { Role = "Passenger", Id = passenger.PassengerId });
        }

        var staff = await _context.Staff
            .FirstOrDefaultAsync(s => s.Email == request.Email);

        if (staff != null) 
        {
            return Ok(new { Role = staff.Role, Id = staff.StaffId });
        }

        return Unauthorized("User not found.");
    }
}

public class LoginRequest 
{
    [System.Text.Json.Serialization.JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
}