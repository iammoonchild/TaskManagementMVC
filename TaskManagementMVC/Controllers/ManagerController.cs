using Microsoft.AspNetCore.Mvc;
using TaskManagementMVC.Repositories.IRepositories;
using TaskManagementMVC.Services.IServices;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;
using TaskManagementMVC.Authorization;
using TaskManagementMVC.Entities.ViewModels.Manager;

namespace TaskManagementMVC.Controllers
{

    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;
        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        public IActionResult Index()
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

        public IActionResult ManagerDashboard()
        {
            int managerId = 6;
            IEnumerable<TeamListingViewModel> model = _managerService.GetTeamListing(managerId);
            return View(model);
        }

        public PartialViewResult GetTeamListing(int managerId)
        {
            var model = _managerService.GetTeamListing(managerId);
            return PartialView("~/Views/Shared/Manager/_TeamListing.cshtml", model);
        }

        [HttpPost]
        public IActionResult SetTeamMembersData(TeamMembersViewModel viewModel)
        {
            var ManagerId = 6;
            var check = viewModel.FirstName.First();
            _managerService.SetTeamMembersData(viewModel);

            return RedirectToAction("GetTeamListing",new {managerId = 6});
        }

        public IActionResult TeamWorkDetails([FromQuery] int teamId)
        {
            TeamWorkDetailsViewModel model = _managerService.GetTeamWorkDetails(teamId);
            return View(model);
        }
        


    }

}
