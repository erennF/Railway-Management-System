using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Route
{
    public int RouteId { get; set; }

    public string RouteName { get; set; } = null!;

    public virtual ICollection<Routestop> Routestops { get; set; } = new List<Routestop>();

    public virtual ICollection<Trainschedule> Trainschedules { get; set; } = new List<Trainschedule>();
}
