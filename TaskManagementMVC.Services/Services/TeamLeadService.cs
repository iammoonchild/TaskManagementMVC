using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.Models;
using TaskManagementMVC.Entities.ViewModels.Common;
using TaskManagementMVC.Entities.ViewModels.TeamLead;
using TaskManagementMVC.Repositories.Enums;
using TaskManagementMVC.Repositories.IRepositories;
using TaskManagementMVC.Services.IServices;
using static TaskManagementMVC.Repositories.Enums.TaskUtilities;

namespace TaskManagementMVC.Services.Services;
public class TeamLeadService : ITeamLeadService
{
    private readonly ITeamLeadRepo _Repo;
    private readonly ITaskRepo _TaskRepo;
    private readonly IUserRepo _UserRepo;
    public TeamLeadService(ITeamLeadRepo repo, ITaskRepo taskRepo, IUserRepo userRepo)
    {
        _Repo = repo;
        _TaskRepo = taskRepo;
        _UserRepo = userRepo;

    }

    public TaskCardViewModel AddNewTask(TaskCardViewModel model, long currentUserId)
    {
        AspNetUser user = _UserRepo.GetUserFromUserId(currentUserId);
        Entities.Models.Task taskToAdd = new()
        {
            TaskTypeId = (int)model.Type,
            TaskStateId = (int)model.State,
            AssignedToId = model.AssignedToId,
            AssignedById = currentUserId,
            CreatedById = currentUserId,
            TaskTitle = model.Title,
            TaskDescription = model.Description,
            Deadline = model.Deadline,
            IsCompleted = false
        };
        Entities.Models.Task task = _TaskRepo.AddTask(taskToAdd);
        TaskLog taskLog = new()
        {
            TaskId = task.TaskId,
            LogTypeId = (short)TaskLogTypeEnum.Created,
            LogDescription = $"Task created By {user.Name} on {DateTime.Now}",
        };
        _TaskRepo.AddTaskLog(taskLog);

        return new TaskCardViewModel()
        {
            Id = task.TaskId,
            Title = task.TaskTitle,
            Description = task.TaskDescription,
            Type = (TaskUtilities.TaskTypeEnum)task.TaskTypeId,
            State = (TaskUtilities.TaskStateEnum)task.TaskStateId,
            AssignedToId = model.AssignedToId,
            //AssignedToName = task.AssignedTo.Name,
            //AssignedToAvatar = task.AssignedTo.Avatar,
            Deadline = task.Deadline
        };
    }

    public void ChangeTaskStatus(long taskId, int statusId,long userId)
    {
        var user = _UserRepo.GetUserFromUserId(userId);
        Entities.Models.Task task = _TaskRepo.GetTaskFromTaskId(taskId);
        var oldState = task.TaskStateId;
        task.TaskStateId = statusId;
        _TaskRepo.UpdateTask(task);
        TaskLog taskLog = new()
        {
            TaskId = taskId,
            LogTypeId = (short)TaskLogTypeEnum.StateChanged,
            LogDescription = $"Task state changed from {((TaskStateEnum)oldState).ToString()} to {((TaskStateEnum)statusId).ToString()} by {user.Name} on {DateTime.Now}",
            CommentedById = user.Id
        };
        _TaskRepo.AddTaskLog(taskLog);
    }

    public long GetTeamIdFromUserId(long UserId)
    {
        return _Repo.GetTeamIdFromUserId(UserId);
    }

    public TLDashboardViewModel GetTeamLeadDashboard(long v)
    {
        AspNetUser user = _UserRepo.GetUserFromUserId(v);
        IQueryable<Entities.Models.Task> tasks = _TaskRepo.GetTeamTasksFromTeamId(user.TeamId ?? 1);

        TLDashboardViewModel model = new TLDashboardViewModel();
        model.PendingTask = tasks.Where(x => x.TaskStateId <= (short)TaskStateEnum.Pending).Count();
        model.InProgressTask = tasks.Where(x => x.TaskStateId == (short)TaskStateEnum.InProgress).Count();
        model.CompletedTask = tasks.Where(x => x.TaskStateId == (short)TaskStateEnum.Completed).Count();
        model.TaskCompletionRate = tasks.Count() > 0 ? ((model.CompletedTask * 100) / tasks.Count()) : 100;
        model.ManagerInstructions = new ();
        model.TeamMembers = _UserRepo.GetTeamUsersFromTeamId(user.TeamId ?? 1).Select(x => new DashboardTeamMembersViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Avatar = x.Avatar,
            PendingTask = tasks.Where(y => y.AssignedToId == x.Id && y.TaskStateId <= (short)TaskStateEnum.Pending).Count()
        }).ToList();
        return model;
    }

    public KanbanViewModel GetTeamLeadKanban(long teamId, string search=null, DateTime? dt=null)
    {
        IQueryable<Entities.Models.Task> taskCards = _TaskRepo.GetTeamTasksFromTeamId(teamId);
        IQueryable<AspNetUser> users = _UserRepo.GetTeamUsersFromTeamId(teamId);
        if(!string.IsNullOrEmpty(search))
        {
            taskCards = taskCards.Where(x => x.TaskTitle.ToLower().Contains(search.Trim()) || x.TaskDescription.ToLower().Contains(search.Trim()));
        }
        if(dt != null)
        {
            if(dt.HasValue)
            {
                taskCards = taskCards.Where(x => x.Deadline >= new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) && x.Deadline <= new DateOnly(dt.Value.Year,dt.Value.Month,dt.Value.Day));
            }
        }
        KanbanViewModel kanban = new()
        {
            TeamId = teamId,
            TeamName = $"Team - {users.First(x => x.RoleId == (short)RoleEnum.Role.TeamLeader).Name}",
            TaskCards = 
                taskCards.Select(x => new TaskCardViewModel
                {
                    Id = x.TaskId,
                    Title = x.TaskTitle,
                    Description = x.TaskDescription,
                    Type = (TaskTypeEnum)x.TaskTypeId,
                    State = (TaskStateEnum)x.TaskStateId,
                    AssignedToId = x.AssignedTo.Id,
                    AssignedToName = x.AssignedTo.Name,
                    AssignedToAvatar = x.AssignedTo.Avatar ?? "/images/memberavatar.png",
                    AssignedBy = x.AssignedBy.Name,
                    CreatedBy = x.CreatedBy.Name,
                    Deadline = x.Deadline
                }).ToList()
            ,
             UserList = users.Select(x => new KanbanUserListViewModel
             {
                 Id = x.Id,
                 Name = x.Name,
                 Avatar = x.Avatar
             }).ToList()
        };
        return kanban;
    }

    //ON HOLDDDDDDDDDDDDD
    public List<Entities.ViewModels.Common.TaskCardViewModel> GetTeamTasksFromTeamId(long teamId)
    {
        IQueryable<AspNetUser> users = _Repo.GetTeamUsersWithTasksFromTeamId(teamId);
        return new();
    }
}
