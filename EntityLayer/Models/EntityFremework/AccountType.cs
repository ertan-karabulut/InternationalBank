using System;
using System.Collections.Generic;

namespace EntityLayer.Models.EntityFremework;

public partial class AccountType
{
    public int Id { get; set; }

    public string TypeName { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
