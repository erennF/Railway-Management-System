using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Train
{
    public int TrainId { get; set; }

    public string Name { get; set; } = null!;

    public string TrainType { get; set; } = null!;

    public virtual Freighttrain? Freighttrain { get; set; }

    public virtual ICollection<Maintenancerecord> Maintenancerecords { get; set; } = new List<Maintenancerecord>();

    public virtual Passengertrain? Passengertrain { get; set; }

    public virtual ICollection<Trainschedule> Trainschedules { get; set; } = new List<Trainschedule>();
}
