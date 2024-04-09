using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;

namespace TaskManagementMVC.Repositories.IRepositories
{
    public interface IManagerRepo
    {
        void SetTeamMembersData(TeamMembersViewModel viewModel);
    }
}
