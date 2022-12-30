using System;
using System.Collections.Generic;

namespace EntityLayer.Models.EntityFremework;

public partial class Role
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public int? RoleLevel { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual CustomerRole? CustomerRole { get; set; }
}
