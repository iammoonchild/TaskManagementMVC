using Microsoft.AspNetCore.Mvc;
using TaskManagementMVC.Entities.ViewModels.Common;
using TaskManagementMVC.Services.IServices;

namespace TaskManagementMVC.Controllers;
public class MemberController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITeamLeadService _Service;
    public MemberController(IHttpContextAccessor httpContextAccessor, ITeamLeadService service)
    {
        _httpContextAccessor = httpContextAccessor;
        _Service = service;
    }
    [HttpGet("Kanban")]
    public IActionResult Kanban()
    {
        long TeamId = _Service.GetTeamIdFromUserId(long.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId")));
        KanbanViewModel kanbanViewModel = _Service.GetTeamLeadKanban(TeamId);
        return View(kanbanViewModel);
    }
}
