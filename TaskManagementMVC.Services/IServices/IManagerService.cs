using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.ViewModels;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;

namespace TaskManagementMVC.Services.IServices
{
    public interface IManagerService
    {
        void SetTeamMembersData(TeamMembersViewModel viewModel);
    }
}
