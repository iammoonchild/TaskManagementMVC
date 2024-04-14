using System;
using System.Collections.Generic;

namespace TaskManagementMVC.Models;

public partial class Team
{
    public long TeamId { get; set; }

    public long Pmid { get; set; }

    public DateOnly CreatedDate { get; set; }

    public virtual AspNetUser Pm { get; set; } = null!;
}
