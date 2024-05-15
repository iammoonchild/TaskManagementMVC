using Microsoft.AspNetCore.Mvc;
using TaskManagementMVC.Repositories.IRepositories;
using TaskManagementMVC.Services.IServices;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;
using TaskManagementMVC.Authorization;
using TaskManagementMVC.Entities.ViewModels.Manager;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using TaskManagementMVC.Entities.Models;

namespace TaskManagementMVC.Controllers
{
    [CustomAuthorize("Manager")]
    [Route("Manager")]
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ManagerController(IManagerService managerService,IHttpContextAccessor http)
        {
            _managerService = managerService;
            _httpContextAccessor = http;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }
        [HttpGet("CreateTeam")]
        
        public IActionResult CreateTeam()
        {
            var ManagerId = 6;
            TeamMembersViewModel model = _managerService.GetTeamMembersData(ManagerId);
            return View(model);
        }
        [Route("MyTeams")]
        public IActionResult MyTeams()
        {
            var managerId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));
            IEnumerable<TeamListingViewModel> model = _managerService.GetTeamListing(managerId);
            return View(model);
        }

        public PartialViewResult GetTeamListing(int managerId)
        {
            var model = _managerService.GetTeamListing(managerId);
            return PartialView("~/Views/Shared/Manager/_TeamListing.cshtml", model);
        }

        [HttpPost("SetTeamMembersData")]
        public IActionResult SetTeamMembersData(TeamMembersViewModel viewModel)
        {
            long PMId = long.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));
            _managerService.SetTeamMembersData(viewModel, PMId);
            return RedirectToAction("GetTeamListing",new {managerId = 6});
        }
        [Route("MyTeams/{teamId}")]
        public IActionResult TeamWorkDetails([FromRoute] int teamId)
        {
            TeamWorkDetailsViewModel model = _managerService.GetTeamWorkDetails(teamId);
            return View(model);
        }

        [HttpPost("EditUser")]
        public void EditUser(int userId, int role, bool status)
        {
            _managerService.EditUser(userId, role, status);
        }

        [HttpPost("AddNewMember")]
        public IActionResult AddNewMember(MemberForm model)
        {
            _managerService.AddNewMember(model);
            return RedirectToAction("CreateTeam");
        }
        

        [HttpGet("Calendar")]
        public IActionResult Calendar()
        {
            return View();
        }

        [HttpGet("GetTeamMembersData")]
        public IActionResult GetTeamMembersData(int TeamId)
        {
            var PMId = long.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));
            var data = _managerService.GetTeamMembersDataForCalendar(PMId, TeamId);
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
            var data = _managerService.GetTeamMembersDataForCalendar(PMId, TeamId);
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

        [HttpGet("GetTeams")]
        public IActionResult GetTeams()
        {
            var PMId = long.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));
            var list = _managerService.GetTeamsForCalendar(PMId).ToList();
            return Json(list);
        }

    }

}
