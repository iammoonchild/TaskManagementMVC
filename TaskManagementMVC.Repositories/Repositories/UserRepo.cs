using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.Data;
using TaskManagementMVC.Repositories.IRepositories;

namespace TaskManagementMVC.Repositories.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly DbTaskManagementContext _context;
        public UserRepo(DbTaskManagementContext context)
        {
            _context = context;
        }
    }
}
