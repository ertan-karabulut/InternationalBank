using System;
using System.Collections.Generic;

namespace EntityLayer.Models.EntityFremework;

public partial class Account
{
    public int Id { get; set; }

    public int BranchId { get; set; }

    public int CustomerId { get; set; }

    public string? AccountName { get; set; }

    public string Iban { get; set; } = null!;

    public string AccountNumber { get; set; } = null!;

    public int TypeId { get; set; }

    public DateTime? CreateDate { get; set; }

    public int CurrencyUnitId { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AccountBalanceHistory> AccountBalanceHistories { get; } = new List<AccountBalanceHistory>();

    public virtual ICollection<AccountBalance> AccountBalances { get; } = new List<AccountBalance>();

    public virtual ICollection<AdditionalAccount> AdditionalAccounts { get; } = new List<AdditionalAccount>();

    public virtual Branch Branch { get; set; } = null!;

    public virtual Currency CurrencyUnit { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual AccountType Type { get; set; } = null!;
}
