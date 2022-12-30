using System;
using System.Collections.Generic;

namespace EntityLayer.Models.EntityFremework;

public partial class InternetPassword
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string Password { get; set; } = null!;

    public DateTime? CreateDate { get; set; }

    public bool IsActive { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
