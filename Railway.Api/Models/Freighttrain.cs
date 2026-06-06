using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Freighttrain
{
    public int TrainId { get; set; }

    public decimal MaxCargoWeight { get; set; }

    public virtual ICollection<Freightshipment> Freightshipments { get; set; } = new List<Freightshipment>();

    public virtual Train Train { get; set; } = null!;
}
