using System;
using System.Collections.Generic;

namespace EntityLayer.Models.EntityFremework;

public partial class AccountBalance
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public decimal Balance { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Account Account { get; set; } = null!;
}
