using Microsoft.AspNetCore.Mvc;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;
using TaskManagementMVC.Services.IServices;
using TaskManagementMVC.Services.Services;

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
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                string RoleId = _userServices.CheckLoginDetails(loginViewModel);
                if (RoleId != "0")
                {
                    TempData["LoginSuccess"] = "LoggedIn Successfully";
                    return RedirectToAction("CreateTeam","Manager");
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

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(string Email)
        {
            if (Email != null)
            {
                bool user = _userServices.CheckEmailDetails(Email);
                if (user)
                {
                    TempData["ResetPassword"] = "Password Reset Successfully";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["ResetPasswordFailed"] = "Invalid Email! Try Again!";
                    return View();
                }
            }
            TempData["ResetPasswordFailed"] = "Invalid Email! Try Again!";
            return View();
        }
    }
}
