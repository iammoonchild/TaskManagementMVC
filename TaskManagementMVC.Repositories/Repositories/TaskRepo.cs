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

        public IQueryable<Entities.Models.Task> GetTeamTasksFromTeamId(long teamId)
        {
            return _context.Tasks.Include(x => x.AssignedTo).Where(x => x.AssignedTo.TeamId == teamId);
        }
    }
}
