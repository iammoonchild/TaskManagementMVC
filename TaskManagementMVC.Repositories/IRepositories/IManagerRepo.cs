using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.Models;
using TaskManagementMVC.Entities.ViewModels.Manager;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;

namespace TaskManagementMVC.Repositories.IRepositories
{
    public interface IManagerRepo
    {
        void AddNewMember(MemberForm model);
        TeamMembersViewModel GetTeamMembersData(int managerId);
        IQueryable<AspNetUser> GetTeamMembersDataForCalendar(long pMId);
        IQueryable<Team> GetTeams(int managerId);
        IQueryable<AspNetUser> GetTeamsForCalendar(long pMId);
        IQueryable<AspNetUser> GetTeamWorkDetails(int teamId);
        void SetTeamMembersData(TeamMembersViewModel viewModel,long PMId);
    }
}
