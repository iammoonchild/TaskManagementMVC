using Microsoft.AspNetCore.Mvc;
using TaskManagementMVC.Services.IServices;

namespace TaskManagementMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
