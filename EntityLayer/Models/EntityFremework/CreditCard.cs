using System;
using System.Collections.Generic;

namespace EntityLayer.Models.EntityFremework;

public partial class CreditCard
{
    public int CreditCardId { get; set; }

    public string? CreditCardName { get; set; }

    public decimal CreditCardLimit { get; set; }

    public virtual ICollection<CardBalanceHistory> CardBalanceHistories { get; } = new List<CardBalanceHistory>();

    public virtual ICollection<CardBalance> CardBalances { get; } = new List<CardBalance>();

    public virtual Card CreditCardNavigation { get; set; } = null!;
}
