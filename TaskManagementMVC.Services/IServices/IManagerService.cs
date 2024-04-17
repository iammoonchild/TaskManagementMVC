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
        IEnumerable<TeamListingViewModel> GetTeamListing(int managerId);
        TeamMembersViewModel GetTeamMembersData(int ManagerId);
        TeamWorkDetailsViewModel GetTeamWorkDetails(int teamId);
        void SetTeamMembersData(TeamMembersViewModel viewModel);
    }
}
