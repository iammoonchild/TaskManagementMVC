using Microsoft.AspNetCore.Mvc;
using TaskManagementMVC.Entities.ViewModels.Common;
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
        var model = new { };
        return View();
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
    public PartialViewResult KanbanBoardPartial()
    {
        long TeamId = _Service.GetTeamIdFromUserId(long.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId")));
        KanbanViewModel kanbanViewModel = _Service.GetTeamLeadKanban(TeamId);
        return PartialView("_KanbanBoardPartial", kanbanViewModel);
    }

    public PartialViewResult TeamLeadDashboard()
    {
        return PartialView("_TeamLeadDashboard");
    }
}
