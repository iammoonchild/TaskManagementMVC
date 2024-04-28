using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskManagementMVC.Entities.Models;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;
using TaskManagementMVC.Repositories.IRepositories;
using TaskManagementMVC.Repositories.Repositories;
using TaskManagementMVC.Services.IServices;
using static TaskManagementMVC.Repositories.Enums.RoleEnum;

namespace TaskManagementMVC.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepo _userRepo;
        public UserServices(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public UserInfoViewModel CheckUserWithCredentials(LoginViewModel model)
        {
            AspNetUser user = _userRepo.GetUserByEmail(model.Email);
            if (BCrypt.Net.BCrypt.EnhancedVerify(model.Password, user.Password, HashType.SHA512))
            {
                return new UserInfoViewModel
                {
                    UserId = user.Id.ToString(),
                    Name = user.Name,
                    Email = user.Email,
                    RoleId = user.RoleId,
                    Role = user.Role.RoleName,
                    IsPasswordChanged = user.IsPasswordChanged ?? false
                };
            }
            return null;
        }

        public void ResetPassword(ResetPWDViewModel model)
        {
            _userRepo.ResetPassword(model);
        }
    }
}
