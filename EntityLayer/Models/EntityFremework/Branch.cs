using System;
using System.Collections.Generic;

namespace EntityLayer.Models.EntityFremework;

public partial class Branch
{
    public int Id { get; set; }

    public string BranchNumber { get; set; } = null!;

    public string BranchName { get; set; } = null!;

    public string BranchAdress { get; set; } = null!;

    public int? DistrictId { get; set; }

    public int CityId { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();

    public virtual City City { get; set; } = null!;

    public virtual District? District { get; set; }
}
