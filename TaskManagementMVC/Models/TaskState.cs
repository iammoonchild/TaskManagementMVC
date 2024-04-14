using System;
using System.Collections.Generic;

namespace TaskManagementMVC.Models;

public partial class TaskState
{
    public int StateId { get; set; }

    public string StateName { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
