using System;
using System.Collections.Generic;

namespace EntityLayer.Models.EntityFremework;

public partial class City
{
    public int Id { get; set; }

    public int? CityNumber { get; set; }

    public string CityName { get; set; } = null!;

    public int? CountryId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<Adress> Adresses { get; } = new List<Adress>();

    public virtual ICollection<Branch> Branches { get; } = new List<Branch>();

    public virtual Country? Country { get; set; }

    public virtual ICollection<District> Districts { get; } = new List<District>();
}
