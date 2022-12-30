using System;
using System.Collections.Generic;

namespace EntityLayer.Models.EntityFremework;

public partial class AccountBalanceHistory
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public decimal NowBalance { get; set; }

    public decimal BeforeBalance { get; set; }

    public decimal Amount { get; set; }

    public string? Explanation { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Account Account { get; set; } = null!;
}
