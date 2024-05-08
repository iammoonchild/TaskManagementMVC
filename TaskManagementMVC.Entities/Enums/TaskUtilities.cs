using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementMVC.Repositories.Enums;
public class TaskUtilities
{
    public enum TaskStateEnum
    {
        New = 1,
        Pending = 2,
        InProgress = 3,
        Completed = 4
    }

    public enum TaskTypeEnum
    {
        Bug = 1,
        Feature = 2,
        Task = 3,
        SubTask = 4
    }

    public enum TaskLogTypeEnum
    {
        Created = 1,
        Assigned = 2,
        StateChanged = 3,
        DeadlineChanged = 4,
        DescriptionChanged = 5,
        TitleChanged = 6,
        Deleted = 7,
        Commented = 8
    }
}
