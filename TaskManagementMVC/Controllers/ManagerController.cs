using Microsoft.AspNetCore.Mvc;
using TaskManagementMVC.Repositories.IRepositories;
using TaskManagementMVC.Services.IServices;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;
using TaskManagementMVC.Authorization;
using TaskManagementMVC.Entities.ViewModels.Manager;
using System.Security.Claims;
using System.Text;

namespace TaskManagementMVC.Controllers
{
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

        [CustomAuthorize("Manager")]
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
        


    }

}
