using System;
using System.Collections.Generic;

namespace EntityLayer.Models.EntityFremework;

public partial class EMail
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string EMail1 { get; set; } = null!;

    public bool IsFavorite { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
