using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Station
{
    public int StationId { get; set; }

    public string Name { get; set; } = null!;

    public string StationType { get; set; } = null!;

    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Customsclearance> Customsclearances { get; set; } = new List<Customsclearance>();

    public virtual ICollection<Routestop> Routestops { get; set; } = new List<Routestop>();

    public virtual ICollection<Tracksegment> TracksegmentEndStations { get; set; } = new List<Tracksegment>();

    public virtual ICollection<Tracksegment> TracksegmentStartStations { get; set; } = new List<Tracksegment>();
}
