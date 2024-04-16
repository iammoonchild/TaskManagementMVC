using Microsoft.AspNetCore.Mvc;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;
using TaskManagementMVC.Services.IServices;
using TaskManagementMVC.Services.Services;

namespace TaskManagementMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        public readonly IJWTService _JWTService;
        public UserController(IUserServices userServices, IJWTService JWTService)
        {
            _userServices = userServices;
            _JWTService = JWTService;
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
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string RoleId = _userServices.CheckLoginDetails(model);
                if (RoleId != "0")
                {
                    UserInfoViewModel validUser = _userServices.CheckValidUserWithRole(model.Email, model.Password);
                    var JWTToken = _JWTService.GenerateJWTToken(validUser);
                    Response.Cookies.Append("jwt", JWTToken);
                    TempData["LoginSuccess"] = "LoggedIn Successfully";
                    return RedirectToAction("ManagerDashboard", "Manager");
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

        public IActionResult LogOut()
        {
            Response.Cookies.Delete("jwt");
            return View("Login");
        }

        public IActionResult ErrorPage()
        {
            return View();
        }   
    }
}
