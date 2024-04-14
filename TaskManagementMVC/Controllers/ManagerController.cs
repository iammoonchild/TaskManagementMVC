using Microsoft.AspNetCore.Mvc;
using TaskManagementMVC.Repositories.IRepositories;
using TaskManagementMVC.Services.IServices;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;
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

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                bool user = _managerService.CheckLoginDetails(loginViewModel);
                if (user)
                {
                    TempData["LoginSuccess"] = "LoggedIn Successfully";
                    return RedirectToAction("CreateTeam");
                }
                else
                {
                    TempData["LoginFailed"] = "Invalid Credentials! Try Again!";
                    ModelState.AddModelError("", "Invalid Credentials");
                    return View();
                }
            }
            return View();
        }
        public IActionResult CreateTeam()
        {
            var ManagerId = 6;
            TeamMembersViewModel model = _managerService.GetTeamMembersData(ManagerId);
            return View(model);
        }

        [HttpPost]
        public IActionResult SetTeamMembersData(TeamMembersViewModel viewModel)
        {
            var ManagerId = 6;
            var check = viewModel.FirstName.First();
            _managerService.SetTeamMembersData(viewModel);

            return View("CreateTeam");
        }
    }

}
