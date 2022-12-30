using System;
using System.Collections.Generic;

namespace EntityLayer.Models.EntityFremework;

public partial class Currency
{
    public int CountryId { get; set; }

    public string Name { get; set; } = null!;

    public string ShortName { get; set; } = null!;

    public string? IconPath { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();

    public virtual Country Country { get; set; } = null!;
}
