using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Reservation
{
    public int ReservationId { get; set; }

    public int PassengerId { get; set; }

    public int ScheduleId { get; set; }

    public DateOnly Date { get; set; }

    public string ReservationStatus { get; set; } = null!;

    public virtual ICollection<Luggage> Luggage { get; set; } = new List<Luggage>();

    public virtual Passenger Passenger { get; set; } = null!;

    public virtual Trainschedule Schedule { get; set; } = null!;

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
