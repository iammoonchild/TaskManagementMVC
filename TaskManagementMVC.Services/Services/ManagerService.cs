using BCrypt.Net;
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
using TaskManagementMVC.Repositories.Utilities;
using TaskManagementMVC.Services.IServices;
using static TaskManagementMVC.Repositories.Enums.TaskUtilities;

namespace TaskManagementMVC.Services.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepo _managerRepo;
        private readonly ITaskRepo _taskRepo;
        private readonly IUserRepo _userRepo;
        public ManagerService(IManagerRepo managerRepo, ITaskRepo taskRepo, IUserRepo userRepo)
        {
            _managerRepo = managerRepo;
            _taskRepo = taskRepo;
            _userRepo = userRepo;
        }

        public void AddNewMember(MemberForm model)
        {
            _managerRepo.AddNewMember(model);
        }

        public void EditUser(int userId, int role, bool status)
        {
            AspNetUser user = _userRepo.GetUserFromUserId(userId);
            user.RoleId = (short)role;
            user.IsActive = status;
            _userRepo.UpdateUser(user);
        }

        public IEnumerable<TeamListingViewModel> GetTeamListing(int managerId)
        {
            IQueryable<Team> teams = _managerRepo.GetTeams(managerId);
            List<TeamListingViewModel> teamListing = new List<TeamListingViewModel>();
            foreach (var team in teams.ToList())
            {
                var tasks = _taskRepo.GetTeamTasksFromTeamId(team.TeamId).ToList();
                teamListing.Add(new TeamListingViewModel
                {
                    TeamId = team.TeamId,
                    TeamAvatar = team.AspNetUsers.Where(x => x.RoleId == (short)RoleEnum.Role.TeamLeader).FirstOrDefault()?.Avatar ?? "",
                    TeamName = "Team " + team.AspNetUsers.Where(x => x.RoleId == (short)RoleEnum.Role.TeamLeader).FirstOrDefault()?.Name,
                    TeamSize = team.AspNetUsers.Count,
                    CreatedOn = team.CreatedDate,
                    PendingTasks = tasks.Count(x => x.TaskStateId == (int)TaskStateEnum.Pending || x.TaskStateId == (int)TaskStateEnum.New),
                    InprogressTasks = tasks.Count(x => x.TaskStateId == (int)TaskStateEnum.InProgress),
                    CompletedTasks = tasks.Count(x => x.TaskStateId == (int)TaskStateEnum.Completed)
                });
            } 
            return teamListing;
        }

        public TeamMembersViewModel GetTeamMembersData(int ManagerId)
        {
            return _managerRepo.GetTeamMembersData(ManagerId);
        }

        public List<AspNetUser> GetTeamMembersDataForCalendar(long pMId, int TeamId)
        {
            return _managerRepo.GetTeamMembersDataForCalendar(pMId).Where(x => TeamId == 0 ||  x.TeamId == TeamId).ToList();
        }

        public List<AspNetUser> GetTeamsForCalendar(long pMId)
        {
            return _managerRepo.GetTeamsForCalendar(pMId).ToList();
        }

        public TeamWorkDetailsViewModel GetTeamWorkDetails(int teamId)
        {
            IQueryable<AspNetUser> model = _managerRepo.GetTeamWorkDetails(teamId);
            var temp = model.Select(x => new TeamMemberDetails
            {
                MemberId = x.Id,
                MemberName = x.Name,
                IsMemberActive = x.IsActive ?? false,
                NoOfTaskAssigned = x.TaskAssignedTos.Where(y => y.AssignedToId == x.Id).Count(),
                NoOfTaskPending = x.TaskAssignedTos.Where(y => y.AssignedToId==x.Id && (y.TaskStateId == (int)TaskStateEnum.Pending || y.TaskStateId == (int)TaskStateEnum.New)).Count(),
                NoOfTaskInProgress = x.TaskAssignedTos.Where(y => y.AssignedToId == x.Id && y.TaskStateId == (int)TaskStateEnum.InProgress).Count(),
                NoOfTaskCompletedOnBeforeDeadline = x.TaskAssignedTos.Where(y => y.AssignedToId == x.Id && y.TaskStateId == (int)TaskStateEnum.Completed).Count(),
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
