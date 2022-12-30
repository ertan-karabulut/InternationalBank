using System;
using System.Collections.Generic;

namespace EntityLayer.Models.EntityFremework;

public partial class CardPassword
{
    public int Id { get; set; }

    public int CardId { get; set; }

    public DateTime? CreateDate { get; set; }

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual Card Card { get; set; } = null!;
}
