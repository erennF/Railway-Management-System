using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Loyaltyaccount
{
    public int AccountId { get; set; }

    public int PassengerId { get; set; }

    public int? TotalMiles { get; set; }

    public string? Class { get; set; }

    public virtual Passenger Passenger { get; set; } = null!;
}
