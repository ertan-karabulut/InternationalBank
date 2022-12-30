using System;
using System.Collections.Generic;

namespace EntityLayer.Models.EntityFremework;

public partial class Country
{
    public int Id { get; set; }

    public string CountryName { get; set; } = null!;

    public string CountryCode { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<Adress> Adresses { get; } = new List<Adress>();

    public virtual ICollection<City> Cities { get; } = new List<City>();

    public virtual Currency? Currency { get; set; }
}
