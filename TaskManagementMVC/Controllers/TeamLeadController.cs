using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using TaskManagementMVC.Entities.ViewModels.Common;
using TaskManagementMVC.Entities.ViewModels.TeamLead;
using TaskManagementMVC.Services.IServices;

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
}
