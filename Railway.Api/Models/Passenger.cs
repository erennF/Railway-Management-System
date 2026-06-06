using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Passenger
{
    public int PassengerId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();

    public virtual Loyaltyaccount? Loyaltyaccount { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Waitinglist> Waitinglists { get; set; } = new List<Waitinglist>();
}
