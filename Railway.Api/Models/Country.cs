using System;
using System.Collections.Generic;

namespace Railway.Api.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Station> Stations { get; set; } = new List<Station>();
}
