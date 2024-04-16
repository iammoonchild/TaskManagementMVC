using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.Models;
using TaskManagementMVC.Entities.ViewModels.Manager;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;
using TaskManagementMVC.Repositories.Enums;
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

        public IEnumerable<TeamListingViewModel> GetTeamListing(int managerId)
        {
            IQueryable<Team> teams = _managerRepo.GetTeams(managerId);
            List<TeamListingViewModel> teamListing = new List<TeamListingViewModel>();
            foreach (var team in teams)
            {
                teamListing.Add(new TeamListingViewModel
                {
                    TeamId = team.TeamId,
                    TeamAvatar = team.AspNetUsers.Where(x => x.RoleId == (short)RoleEnum.Role.TeamLeader).FirstOrDefault().Avatar,
                    TeamName = "Team " + team.AspNetUsers.Where(x => x.RoleId == (short)RoleEnum.Role.TeamLeader).FirstOrDefault().Name,
                    TeamSize = team.AspNetUsers.Count,
                    CreatedOn = team.CreatedDate
                });
            } 
            return teamListing;
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
