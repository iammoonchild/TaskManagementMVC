using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Repositories.IRepositories;
using TaskManagementMVC.Services.IServices;

namespace TaskManagementMVC.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepo _userRepo;
        public UserServices(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
    }
}
