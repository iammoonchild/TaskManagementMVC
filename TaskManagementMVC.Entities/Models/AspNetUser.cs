using System;
using System.Collections.Generic;

namespace TaskManagementMVC.Entities.Models;

public partial class AspNetUser
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNo { get; set; }

    public string Password { get; set; } = null!;

    public short RoleId { get; set; }

    public bool? IsActive { get; set; }

    public long? CreatedBy { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public string OldPassword { get; set; } = null!;

    public long? TeamId { get; set; }

    public string? Avatar { get; set; }

    public virtual ICollection<Task> TaskAssignedBies { get; set; } = new List<Task>();

    public virtual ICollection<Task> TaskAssignedTos { get; set; } = new List<Task>();

    public virtual ICollection<Task> TaskCreatedBies { get; set; } = new List<Task>();

    public virtual ICollection<TaskLog> TaskLogCommentedBies { get; set; } = new List<TaskLog>();

    public virtual ICollection<TaskLog> TaskLogTransferredBies { get; set; } = new List<TaskLog>();

    public virtual ICollection<TaskLog> TaskLogTransferredFroms { get; set; } = new List<TaskLog>();

    public virtual ICollection<TaskLog> TaskLogTransferredTos { get; set; } = new List<TaskLog>();

    public virtual ICollection<Task> TaskModifiedByNavigations { get; set; } = new List<Task>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
