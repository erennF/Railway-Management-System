using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Routestop
{
    public int RouteId { get; set; }

    public int StationId { get; set; }

    public int SequenceNumber { get; set; }

    public virtual Route Route { get; set; } = null!;

    public virtual Station Station { get; set; } = null!;
}
