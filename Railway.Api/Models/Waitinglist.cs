using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Waitinglist
{
    public int WaitlistId { get; set; }

    public int PassengerId { get; set; }

    public int ScheduleId { get; set; }

    public DateTime DateAdded { get; set; }

    public virtual Passenger Passenger { get; set; } = null!;

    public virtual Trainschedule Schedule { get; set; } = null!;
}
