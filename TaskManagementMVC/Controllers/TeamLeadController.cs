using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using TaskManagementMVC.Entities.Models;
using TaskManagementMVC.Entities.ViewModels.Common;
using TaskManagementMVC.Entities.ViewModels.Manager;
using TaskManagementMVC.Entities.ViewModels.TeamLead;
using TaskManagementMVC.Services.IServices;
using TaskManagementMVC.Services.Services;

namespace TaskManagementMVC.Controllers;
[Route("TeamLead")]
public class TeamLeadController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITeamLeadService _Service;
    public TeamLeadController(IHttpContextAccessor httpContextAccessor, ITeamLeadService service)
    {
        _httpContextAccessor = httpContextAccessor;
        _Service = service;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpGet("Dashboard")]
    public IActionResult Dashboard()
    {
        TLDashboardViewModel model = _Service.GetTeamLeadDashboard(long.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId")));
        return View(model);
    }

    [HttpGet("Kanban")]
    public IActionResult Kanban()
    {
        long TeamId = _Service.GetTeamIdFromUserId(long.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId")));
        KanbanViewModel kanbanViewModel = _Service.GetTeamLeadKanban(TeamId);
        return View(kanbanViewModel);
    }

    [HttpPost("AddNewTask")]
    public IActionResult AddNewTask(TaskCardViewModel model)
    {
        long UserId = long.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));
        TaskCardViewModel taskCardViewModel = _Service.AddNewTask(model,UserId);
        return RedirectToAction("KanbanBoardPartial", "TeamLead");
    }

    [HttpGet("KanbanBoardPartial")]
    public PartialViewResult KanbanBoardPartial([FromQuery]string search=null, [FromQuery] DateTime? endDate=null)
    {
        long TeamId = _Service.GetTeamIdFromUserId(long.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId")));
        KanbanViewModel kanbanViewModel = _Service.GetTeamLeadKanban(TeamId,search, endDate);
        return PartialView("_KanbanBoardPartial", kanbanViewModel);
    }
    [HttpGet("TeamLeadDashboard")]
    public PartialViewResult TeamLeadDashboard()
    {
        TLDashboardViewModel model = _Service.GetTeamLeadDashboard(long.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId")));
        return PartialView("_TeamLeadDashboard", model);
    }

    [HttpPost]
    public void ChangeTaskStatus(long taskId, int stateId)
    {
        var userId = long.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));
        _Service.ChangeTaskStatus(taskId, stateId, userId);
    }

    [HttpGet("Calendar")]
    public IActionResult Calendar()
    {
        return View();
    }

    [HttpGet("GetTeamMembersData")]
    public IActionResult GetTeamMembersData(int TeamId)
    {
        var TLId = long.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));
        List<AspNetUser> data = _Service.GetTeamMembersDataForCalendar(TLId, TeamId);
        List<CalendarDataViewModel> list = data.Select(x => new CalendarDataViewModel
        {
            Id = x.Id,
            title = x.Name,
        }).ToList();
        return Json(list);
    }

    [HttpGet("GetTaskData")]
    public IActionResult GetTaskData(int TeamId)
    {
        var PMId = long.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));
        var data = _Service.GetTeamMembersDataForCalendar(PMId, TeamId);
        var startTime = new TimeOnly(9, 0, 0);
        var endTime = new TimeOnly(18, 0, 0);


        List<CalendarTaskViewModel> list = data.SelectMany(x => x.TaskAssignedTos.Select(taskAssignedTo => new CalendarTaskViewModel
        {
            resourceId = x.Id,
            Id = taskAssignedTo.TaskId,
            title = taskAssignedTo.TaskTitle,
            start = taskAssignedTo.Deadline.ToDateTime(startTime).ToString("yyyy-MM-ddTHH:mm:ss"),
            end = taskAssignedTo.Deadline.ToDateTime(endTime).ToString("yyyy-MM-ddTHH:mm:ss"),
            color = taskAssignedTo.TaskStateId == 1 ? "#FF0000" : taskAssignedTo.TaskStateId == 2 ? "#FFA500" : "#008000"
        })).ToList();

        return Json(list);
    }
}
