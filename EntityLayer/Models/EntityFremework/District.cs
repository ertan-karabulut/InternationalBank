using System;
using System.Collections.Generic;

namespace EntityLayer.Models.EntityFremework;

public partial class District
{
    public int Id { get; set; }

    public string DistrictName { get; set; } = null!;

    public int? CityId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<Adress> Adresses { get; } = new List<Adress>();

    public virtual ICollection<Branch> Branches { get; } = new List<Branch>();

    public virtual City? City { get; set; }
}
