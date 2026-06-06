using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Cargocontainer
{
    public int ContainerId { get; set; }

    public int ShipmentId { get; set; }

    public string ContainerType { get; set; } = null!;

    public virtual Freightshipment Shipment { get; set; } = null!;
}
