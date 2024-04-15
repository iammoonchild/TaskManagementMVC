using System;
using System.Collections.Generic;

namespace TaskManagementMVC.Entities.Models;

public partial class Team
{
    public long TeamId { get; set; }

    public long Pmid { get; set; }

    public DateOnly CreatedDate { get; set; }

    public virtual ICollection<AspNetUser> AspNetUsers { get; set; } = new List<AspNetUser>();

    public virtual AspNetUser Pm { get; set; } = null!;
}
