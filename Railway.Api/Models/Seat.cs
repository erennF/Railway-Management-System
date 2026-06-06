using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Seat
{
    public int SeatId { get; set; }

    public int ReservationId { get; set; }

    public string SeatNumber { get; set; } = null!;

    public string CoachType { get; set; } = null!;

    public virtual Reservation Reservation { get; set; } = null!;
}
