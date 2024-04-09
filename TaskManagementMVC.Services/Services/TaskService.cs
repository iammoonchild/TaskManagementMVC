using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Repositories.IRepositories;
using TaskManagementMVC.Services.IServices;

namespace TaskManagementMVC.Services.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepo _taskRepo;
        public TaskService(ITaskRepo taskRepo)
        {
            _taskRepo = taskRepo;
        }
    }
}
