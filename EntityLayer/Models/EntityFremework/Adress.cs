using System;
using System.Collections.Generic;

namespace EntityLayer.Models.EntityFremework;

public partial class Adress
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string? AdressName { get; set; }

    public string AdressDetail { get; set; } = null!;

    public int CityId { get; set; }

    public int DistrictId { get; set; }

    public int CountryId { get; set; }

    public DateTime? DomicileStartDate { get; set; }

    public bool IsFavorite { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual District District { get; set; } = null!;
}
