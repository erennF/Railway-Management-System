using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Luggage
{
    public int LuggageId { get; set; }

    public int ReservationId { get; set; }

    public decimal Weight { get; set; }

    public virtual Reservation Reservation { get; set; } = null!;
}
