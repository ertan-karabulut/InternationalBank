using System;
using System.Collections.Generic;

namespace EntityLayer.Models.EntityFremework;

public partial class CustomerRole
{
    public int RoleId { get; set; }

    public int CustomerId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
