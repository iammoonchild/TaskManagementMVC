using System;
using System.Collections.Generic;

namespace TaskManagementMVC.Models;

public partial class LogType
{
    public int LogTypeId { get; set; }

    public string LogTypeName { get; set; } = null!;

    public virtual ICollection<TaskLog> TaskLogs { get; set; } = new List<TaskLog>();
}
