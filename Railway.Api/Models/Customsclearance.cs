using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Customsclearance
{
    public int ClearanceId { get; set; }

    public int ShipmentId { get; set; }

    public int StationId { get; set; }

    public DateOnly Date { get; set; }

    public string Status { get; set; } = null!;

    public virtual Freightshipment Shipment { get; set; } = null!;

    public virtual Station Station { get; set; } = null!;
}
