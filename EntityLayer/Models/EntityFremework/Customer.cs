using System;
using System.Collections.Generic;

namespace EntityLayer.Models.EntityFremework;

public partial class Customer
{
    public int Id { get; set; }

    public string CustomerNumber { get; set; } = null!;

    public string IdentityNumber { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public DateTime? CreateDate { get; set; }

    public short Gender { get; set; }

    public DateTime DateofBirth { get; set; }

    public string? Photo { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();

    public virtual ICollection<Adress> Adresses { get; } = new List<Adress>();

    public virtual ICollection<Card> Cards { get; } = new List<Card>();

    public virtual ICollection<CustomerRole> CustomerRoles { get; } = new List<CustomerRole>();

    public virtual ICollection<EMail> EMails { get; } = new List<EMail>();

    public virtual ICollection<InternetPassword> InternetPasswords { get; } = new List<InternetPassword>();

    public virtual ICollection<PhoneNumber> PhoneNumbers { get; } = new List<PhoneNumber>();
}
