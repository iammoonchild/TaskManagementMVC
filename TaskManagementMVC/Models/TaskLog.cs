using System;
using System.Collections.Generic;

namespace TaskManagementMVC.Models;

public partial class TaskLog
{
    public long LogId { get; set; }

    public long TaskId { get; set; }

    public long TransferredById { get; set; }

    public long TransferredToId { get; set; }

    public long TransferredFromId { get; set; }

    public int LogTypeId { get; set; }

    public long CommentedById { get; set; }

    public string? LogDescription { get; set; }

    public DateOnly CreatedDate { get; set; }

    public virtual AspNetUser CommentedBy { get; set; } = null!;

    public virtual LogType LogType { get; set; } = null!;

    public virtual Task Task { get; set; } = null!;

    public virtual AspNetUser TransferredBy { get; set; } = null!;

    public virtual AspNetUser TransferredFrom { get; set; } = null!;

    public virtual AspNetUser TransferredTo { get; set; } = null!;
}
