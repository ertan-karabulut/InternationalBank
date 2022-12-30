using System;
using System.Collections.Generic;

namespace EntityLayer.Models.EntityFremework;

public partial class PhoneNumber
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string PhoneNumber1 { get; set; } = null!;

    public string? NumberName { get; set; }

    public bool IsFavorite { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
