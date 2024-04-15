using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.Models;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;
using TaskManagementMVC.Repositories.IRepositories;
using TaskManagementMVC.Repositories.Repositories;
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

        public bool CheckEmailDetails(string email)
        {
            List<AspNetUser> user = _userRepo.GetAspNetUserTable();
             var check = user.Where(x => x.Email.Trim().ToLower() == (email.Trim().ToLower())).Any();
            return check;
        }

        public string CheckLoginDetails(LoginViewModel loginViewModel)
        {
            List<AspNetUser> user = _userRepo.GetAspNetUserTable();
            var check = user.Where(x => x.Email.Trim().ToLower() == (loginViewModel.Email.Trim().ToLower()) && x.Password == loginViewModel.Password).Select(x => x.RoleId).FirstOrDefault().ToString() ?? "0";
            return check;
        }

        public UserInfoViewModel CheckValidUserWithRole(string email, string password)
        {
            List<AspNetUser> user = _userRepo.GetAspNetUserTable();
            var check = user.Where(x => x.Email.Trim().ToLower() == (email.Trim().ToLower()) && x.Password == password).Select(x => new UserInfoViewModel
            {
                UserId = x.Id.ToString(),
                Name = x.Name,
                Email = x.Email,
                RoleId = x.RoleId,
                Role = x.Role.RoleName
            }).FirstOrDefault();
            return check;
        }
    }
}
