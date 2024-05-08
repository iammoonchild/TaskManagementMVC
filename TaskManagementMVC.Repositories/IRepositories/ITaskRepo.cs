using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.Models;
using TaskManagementMVC.Entities.ViewModels.Common;

namespace TaskManagementMVC.Repositories.IRepositories
{
    public interface ITaskRepo
    {
        Entities.Models.Task AddTask(Entities.Models.Task taskToAdd);
        void AddTaskLog(TaskLog taskLog);
        Entities.Models.Task GetTaskFromTaskId(long taskId);
        IQueryable<Entities.Models.Task> GetTeamTasksFromTeamId(long teamId);
        void UpdateTask(Entities.Models.Task task);
    }
}
