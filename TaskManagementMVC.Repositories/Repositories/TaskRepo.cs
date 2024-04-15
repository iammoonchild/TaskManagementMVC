using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.Models;
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
    }
}
