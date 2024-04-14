using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.Models;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;
using TaskManagementMVC.Repositories.IRepositories;
using TaskManagementMVC.Services.IServices;

namespace TaskManagementMVC.Services.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepo _managerRepo;
        public ManagerService(IManagerRepo managerRepo)
        {
            _managerRepo = managerRepo;
        }

        public bool CheckLoginDetails(LoginViewModel loginViewModel)
        {
            List<AspNetUser> user = _managerRepo.GetAspNetUserTable();
            var check = user.Where(x => x.Email.Trim().ToLower()==(loginViewModel.Email.Trim().ToLower()) && x.Password == loginViewModel.Password).Any();
            return check;
        }

        public TeamMembersViewModel GetTeamMembersData(int ManagerId)
        {
            return _managerRepo.GetTeamMembersData(ManagerId);
        }

        public void SetTeamMembersData(TeamMembersViewModel viewModel)
        {
            _managerRepo.SetTeamMembersData(viewModel);
        }
    }
}
