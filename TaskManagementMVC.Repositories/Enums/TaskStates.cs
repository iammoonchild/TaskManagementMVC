using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementMVC.Repositories.Enums;
public class TaskStates
{
    public enum TaskStateEnum
    {
        Pending = 1,
        InProgress = 2,
        Completed = 3
    }
}
