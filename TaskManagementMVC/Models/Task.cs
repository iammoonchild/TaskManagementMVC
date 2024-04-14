using System;
using System.Collections.Generic;

namespace TaskManagementMVC.Models;

public partial class Task
{
    public long TaskId { get; set; }

    public int TaskTypeId { get; set; }

    public int TaskStateId { get; set; }

    public long? AssignedToId { get; set; }

    public long AssignedById { get; set; }

    public long CreatedById { get; set; }

    public long? ModifiedBy { get; set; }

    public bool? IsCompleted { get; set; }

    public string TaskDescription { get; set; } = null!;

    public virtual AspNetUser AssignedBy { get; set; } = null!;

    public virtual AspNetUser? AssignedTo { get; set; }

    public virtual AspNetUser CreatedBy { get; set; } = null!;

    public virtual AspNetUser? ModifiedByNavigation { get; set; }

    public virtual ICollection<TaskLog> TaskLogs { get; set; } = new List<TaskLog>();

    public virtual TaskState TaskState { get; set; } = null!;

    public virtual TaskType TaskType { get; set; } = null!;
}
