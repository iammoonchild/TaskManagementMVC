using System;
using System.Collections.Generic;

namespace TaskManagementMVC.Models;

public partial class TaskType
{
    public int TaskTypeId { get; set; }

    public string TaskTypeName { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
