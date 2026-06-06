using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Railway.Api.Models;

namespace Railway.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly DatabaseProjectContext _context;

        public StaffController(DatabaseProjectContext context)
        {
            _context = context;
        }

        // ==========================================================
        // 1. DRIVER OPERATIONS
        // ==========================================================
        [HttpGet("Assignments/{staffId}")]
        public async Task<IActionResult> GetStaffAssignments(int staffId)
        {
            var assignments = await _context.Staffassignments
                .Where(sa => sa.StaffId == staffId)
                .Join(_context.Trainschedules, 
                      sa => sa.ScheduleId, 
                      ts => ts.ScheduleId, 
                      (sa, ts) => new { ScheduleId = ts.ScheduleId, Date = ts.Date, DepartureTime = ts.DepartureTime, RouteId = ts.RouteId })
                .ToListAsync();

            return Ok(assignments.Any() ? assignments : new List<object>());
        }

        // ==========================================================
        // 2. TECHNICIAN AND CUSTOMS 
        // ==========================================================
        [HttpPost("AddMaintenance")]
        public async Task<IActionResult> AddMaintenance([FromBody] MaintenanceRequest request)
        {
            var record = new Maintenancerecord
            {
                TrainId = request.TrainId,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Details = request.Details
            };
            _context.Maintenancerecords.Add(record);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Maintenance/repair record added." });
        }

        [HttpGet("Sensors/{trainId}")]
        public async Task<IActionResult> GetSensors(int trainId)
        {
            var sensors = await _context.Sensors.Where(s => s.TrainId == trainId).ToListAsync();
            return Ok(sensors);
        }

        [HttpPost("AddSensor")]
        public async Task<IActionResult> AddSensor([FromBody] Sensor request)
        {
            var trainExists = await _context.Trains.AnyAsync(t => t.TrainId == request.TrainId);
            if (!trainExists) return BadRequest("No train found with the specified ID.");

            _context.Sensors.Add(request);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Sensor added successfully." });
        }
        // ------------------------------------

        // ==========================================================
        // 3. ADMINISTRATOR OPERATIONS
        // ==========================================================
        [HttpPost("AssignStaff")]
        public async Task<IActionResult> AssignStaff([FromBody] StaffassignmentRequest request)
        {
            _context.Staffassignments.Add(new Staffassignment { StaffId = request.StaffId, ScheduleId = request.ScheduleId, AssignmentDate = request.Date });
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Assignment successful." });
        }

        [HttpGet("Dropdown/Trains")]
        public async Task<IActionResult> GetTrainsDropdown() => Ok(await _context.Trains.Select(t => new { t.TrainId, t.Name }).ToListAsync());

        [HttpGet("Dropdown/Staffs")]
        public async Task<IActionResult> GetStaffDropdown() => Ok(await _context.Staff.Select(s => new { s.StaffId, s.Name, s.Role }).ToListAsync());

        [HttpGet("Dropdown/Routes")]
        public async Task<IActionResult> GetRoutesDropdown() => Ok(await _context.Routes.Select(r => new { r.RouteId, r.RouteName }).ToListAsync());

        [HttpPost("Management/AddTrain")]
        public async Task<IActionResult> AddNewTrain([FromBody] TrainCreateRequest request)
        {
            _context.Trains.Add(new Train { Name = request.Name, TrainType = request.TrainType });
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Train added." });
        }
        [HttpGet("Dropdown/Schedules")]
       public async Task<IActionResult> GetSchedulesDropdown() 
       {
         var list = await _context.Trainschedules
        .Select(s => new { 
            ScheduleId = s.ScheduleId, 
            DisplayText = $"Sefer #{s.ScheduleId} ({s.Date})" 
        })
        .ToListAsync();
         return Ok(list);
        }
        [HttpGet("Dropdown/Passengers")]
public async Task<IActionResult> GetPassengersDropdown() 
{
    var list = await _context.Passengers
        .Select(p => new { 
            PassengerId = p.PassengerId, 
            Name = p.Name 
        })
        .ToListAsync();
    
    return Ok(list);
}
        [HttpPost("Management/AddStaff")]
        public async Task<IActionResult> AddNewStaff([FromBody] StaffCreateRequest request)
        {
            _context.Staff.Add(new Staff { Name = request.Name, Role = request.Role, Email = request.Email });
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Staff member added." });
        }

        [HttpPost("Management/AddSchedule")]
        public async Task<IActionResult> AddNewSchedule([FromBody] ScheduleCreateRequest request)
        {
            if (!TimeSpan.TryParse(request.DepartureTime, out TimeSpan timeSpan))
                return BadRequest("Invalid time format (HH:mm).");

            _context.Trainschedules.Add(new Trainschedule 
            { 
                Date = request.Date, 
                DepartureTime = TimeOnly.FromTimeSpan(timeSpan), 
                TrainId = request.TrainId, 
                RouteId = request.RouteId 
            });
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Schedule added." });
        }
    }

    public class MaintenanceRequest { public int TrainId { get; set; } public string Details { get; set; } = null!; }
    public class StaffassignmentRequest { public int StaffId { get; set; } public int ScheduleId { get; set; } public DateOnly Date { get; set; } }
    public class TrainCreateRequest { public string Name { get; set; } = null!; public string TrainType { get; set; } = null!; }
    public class StaffCreateRequest { public string Name { get; set; } = null!; public string Role { get; set; } = null!; public string Email { get; set; } = null!; }
    public class ScheduleCreateRequest { public DateOnly Date { get; set; } public string DepartureTime { get; set; } = null!; public int TrainId { get; set; } public int RouteId { get; set; } }
}