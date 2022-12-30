using System;
using System.Collections.Generic;

namespace EntityLayer.Models.EntityFremework;

public partial class Card
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string CardNumber { get; set; } = null!;

    public DateTime? CreateDate { get; set; }

    public bool IsActive { get; set; }

    public virtual AtmCard? AtmCard { get; set; }

    public virtual ICollection<CardPassword> CardPasswords { get; } = new List<CardPassword>();

    public virtual CreditCard? CreditCard { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
