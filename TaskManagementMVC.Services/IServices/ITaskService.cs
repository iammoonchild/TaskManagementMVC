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
        List<Dictionary<long, string>> GetComments(long taskId);
        TaskDetailsViewModel GetTaskDetails(long taskId);
        void SaveComment(string comment, long taskId, long userId);
        void UpdateTask(long taskId, TaskDetailsViewModel model, long userId);
    }
}
