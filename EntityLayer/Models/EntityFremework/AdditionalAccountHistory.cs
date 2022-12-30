using System;
using System.Collections.Generic;

namespace EntityLayer.Models.EntityFremework;

public partial class AdditionalAccountHistory
{
    public int Id { get; set; }

    public int AdditionalAccountId { get; set; }

    public decimal NowBalance { get; set; }

    public decimal BeforeBalance { get; set; }

    public decimal Amount { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual AdditionalAccount AdditionalAccount { get; set; } = null!;
}
