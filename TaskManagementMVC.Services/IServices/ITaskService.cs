using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.ViewModels.TasksViewModels;

namespace TaskManagementMVC.Services.IServices
{
    public interface ITaskService
    {
        TaskDetailsViewModel GetTaskDetails(long taskId);
        void UpdateTask(long taskId, TaskDetailsViewModel model, long userId);
    }
}
