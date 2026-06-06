using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Maintenancerecord
{
    public int MaintenanceId { get; set; }

    public int? TrainId { get; set; }

    public int? TrackId { get; set; }

    public DateOnly Date { get; set; }

    public string Details { get; set; } = null!;

    public virtual Tracksegment? Track { get; set; }

    public virtual Train? Train { get; set; }
}
