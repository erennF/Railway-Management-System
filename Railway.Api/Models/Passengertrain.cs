using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Passengertrain
{
    public int TrainId { get; set; }

    public int PassengerCapacity { get; set; }

    public virtual Train Train { get; set; } = null!;
}
