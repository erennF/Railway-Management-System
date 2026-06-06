using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Staffassignment
{
    public int StaffId { get; set; }

    public int ScheduleId { get; set; }

    public DateOnly AssignmentDate { get; set; }

    public virtual Trainschedule Schedule { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}
