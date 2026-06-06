using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Tracksegment
{
    public int TrackId { get; set; }

    public int StartStationId { get; set; }

    public int EndStationId { get; set; }

    public decimal Distance { get; set; }

    public int MaxPassengerSpeed { get; set; }

    public int MaxFreightSpeed { get; set; }

    public string Status { get; set; } = null!;

    public virtual Station EndStation { get; set; } = null!;

    public virtual ICollection<Maintenancerecord> Maintenancerecords { get; set; } = new List<Maintenancerecord>();

    public virtual Station StartStation { get; set; } = null!;
}
