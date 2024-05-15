using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.ViewModels;
using TaskManagementMVC.Entities.ViewModels.Manager;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;

namespace TaskManagementMVC.Services.IServices
{
    public interface IManagerService
    {
        void AddNewMember(MemberForm model);
        void EditUser(int userId, int role, bool status);
        IEnumerable<TeamListingViewModel> GetTeamListing(int managerId);
        TeamMembersViewModel GetTeamMembersData(int ManagerId);
        TeamWorkDetailsViewModel GetTeamWorkDetails(int teamId);
        void SetTeamMembersData(TeamMembersViewModel viewModel, long PMId);
    }
}
