using Microsoft.AspNetCore.Mvc;
using TaskManagementMVC.Repositories.IRepositories;
using TaskManagementMVC.Services.IServices;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;
namespace TaskManagementMVC.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;    
        public ManagerController(IManagerService managerService) { 
            _managerService = managerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateTeam()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetTeamMembersData(TeamMembersViewModel viewModel)
        {
           var check =  viewModel.FirstName.First();
            _managerService.SetTeamMembersData(viewModel);
            return Json(true);
        }
    }
    
}
