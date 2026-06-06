using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Freightshipment
{
    public int ShipmentId { get; set; }

    public int TrainId { get; set; }

    public string Shipper { get; set; } = null!;

    public decimal Weight { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Cargocontainer> Cargocontainers { get; set; } = new List<Cargocontainer>();

    public virtual ICollection<Customsclearance> Customsclearances { get; set; } = new List<Customsclearance>();

    public virtual Freighttrain Train { get; set; } = null!;
}
