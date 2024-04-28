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
using static TaskManagementMVC.Repositories.Enums.TaskUtilities;

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
                    TeamAvatar = team.AspNetUsers.Where(x => x.RoleId == (short)RoleEnum.Role.TeamLeader).FirstOrDefault()?.Avatar ?? "",
                    TeamName = "Team " + team.AspNetUsers.Where(x => x.RoleId == (short)RoleEnum.Role.TeamLeader).FirstOrDefault()?.Name,
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

        public TeamWorkDetailsViewModel GetTeamWorkDetails(int teamId)
        {
            IQueryable<AspNetUser> model = _managerRepo.GetTeamWorkDetails(teamId);
            var temp = model.Select(x => new TeamMemberDetails
            {
                MemberId = x.Id,
                MemberName = x.Name,
                IsMemberActive = x.IsActive ?? false,
                NoOfTaskAssigned = x.TaskAssignedTos.Count,
                NoOfTaskPending = x.TaskAssignedTos.Where(y => y.AssignedToId==x.Id && y.TaskStateId == (int)TaskStateEnum.Pending).Count(),
                NoOfTaskInProgress = x.TaskAssignedTos.Where(y => y.AssignedToId == x.Id && y.TaskStateId == (int)TaskStateEnum.InProgress).Count(),
                NoOfTaskCompletedOnBeforeDeadline = x.TaskAssignedTos.Where(y => y.AssignedToId == x.Id && y.IsCompleted==true).Count(),
                NoOfTaskCompletedAfterDeadline = x.TaskAssignedTos.Where(y => y.AssignedToId == x.Id && y.IsCompleted == true).Count(),
                Role = x.RoleId
            }).ToList();
            return new TeamWorkDetailsViewModel
            {
                TeamId = teamId,
                TeamMemberDetails = temp
            };
        }

        public void SetTeamMembersData(TeamMembersViewModel viewModel, long PMId)
        {
            _managerRepo.SetTeamMembersData(viewModel, PMId);
        }
    }
}
