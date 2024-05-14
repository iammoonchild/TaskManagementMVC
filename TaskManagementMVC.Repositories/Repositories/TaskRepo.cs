using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.Models;
using TaskManagementMVC.Entities.ViewModels.Common;
using TaskManagementMVC.Repositories.IRepositories;

namespace TaskManagementMVC.Repositories.Repositories
{
    public class TaskRepo : ITaskRepo
    {
        private readonly DbTaskManagementContext _context;
        public TaskRepo(DbTaskManagementContext context)
        {
            _context = context;
        }

        public Entities.Models.Task AddTask(Entities.Models.Task taskToAdd)
        {
            var task = _context.Tasks.Add(taskToAdd);
            _context.SaveChanges();
            return task.Entity;
        }

        public void AddTaskLog(TaskLog taskLog)
        {
            _context.TaskLogs.Add(taskLog);
            _context.SaveChanges();
        }

        public Entities.Models.Task GetTaskFromTaskId(long taskId)
        {
            return _context.Tasks.Include(x => x.TaskLogs).Include(x => x.AssignedBy).Include(x => x.AssignedTo).Include(x=> x.CreatedBy).FirstOrDefault(x => x.TaskId == taskId);
        }

        public IQueryable<Entities.Models.Task> GetTeamTasksFromTeamId(long teamId)
        {
            return _context.Tasks.Include(x => x.AssignedTo).Where(x => x.AssignedTo.TeamId == teamId);
        }

        public void UpdateTask(Entities.Models.Task task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }
    }
}
