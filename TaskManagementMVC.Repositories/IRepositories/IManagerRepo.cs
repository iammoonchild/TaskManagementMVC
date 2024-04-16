using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.Models;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;

namespace TaskManagementMVC.Repositories.IRepositories
{
    public interface IManagerRepo
    {
        List<AspNetUser> GetAspNetUserTable();
        TeamMembersViewModel GetTeamMembersData(int managerId);
        IQueryable<Team> GetTeams(int managerId);
        void SetTeamMembersData(TeamMembersViewModel viewModel);
    }
}
