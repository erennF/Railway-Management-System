using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Trainschedule
{
    public int ScheduleId { get; set; }

    public int TrainId { get; set; }

    public int RouteId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly DepartureTime { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual Route Route { get; set; } = null!;

    public virtual ICollection<Staffassignment> Staffassignments { get; set; } = new List<Staffassignment>();

    public virtual Train Train { get; set; } = null!;

    public virtual ICollection<Waitinglist> Waitinglists { get; set; } = new List<Waitinglist>();
}
