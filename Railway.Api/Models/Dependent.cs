using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Dependent
{
    public int PassengerId { get; set; }

    public int DependentId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Passenger Passenger { get; set; } = null!;
}
