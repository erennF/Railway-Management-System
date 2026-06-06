using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string Name { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Staffassignment> Staffassignments { get; set; } = new List<Staffassignment>();
}
