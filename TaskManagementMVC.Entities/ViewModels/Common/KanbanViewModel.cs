using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.Models;
using TaskManagementMVC.Repositories.Enums;

namespace TaskManagementMVC.Entities.ViewModels.Common;
public class KanbanViewModel
{
    public long TeamId { get; set; }
    public string TeamName { get; set; }
    public List<TaskCardViewModel> TaskCards { get; set; }
    public List<KanbanUserListViewModel> UserList { get; set; }
}

public class TaskCardViewModel
{
    public long Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; } = null!;

    public TaskUtilities.TaskTypeEnum Type { get; set; }

    public TaskUtilities.TaskStateEnum State { get; set; }

    public long? AssignedToId { get; set; }
    public string? AssignedToName { get; set; }
    public string AssignedToAvatar { get; set; }

    public string AssignedBy { get; set; }

    public string CreatedBy { get; set; }

    public DateOnly Deadline { get; set; }
}

public class KanbanUserListViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Avatar { get; set; }
}