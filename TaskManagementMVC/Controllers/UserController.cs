using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;
using TaskManagementMVC.Repositories.Enums;
using TaskManagementMVC.Services.IServices;
using TaskManagementMVC.Services.Services;

namespace TaskManagementMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        public readonly IJWTService _JWTService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(IUserServices userServices, IJWTService JWTService, IHttpContextAccessor httpContextAccessor)
        {
            _userServices = userServices;
            _JWTService = JWTService;
            _httpContextAccessor = httpContextAccessor;
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
                UserInfoViewModel user = _userServices.CheckUserWithCredentials(model);
                if (user == null)
                {
                    TempData["LoginFailed"] = "Invalid Credentials! Try Again!";
                    ModelState.AddModelError("", "Invalid Credentials");
                    return View();
                }
                var JWTToken = _JWTService.GenerateJWTToken(user);
                Response.Cookies.Append("jwt", JWTToken);
                _httpContextAccessor.HttpContext.Session.SetString("userId", user.UserId.ToString());
                _httpContextAccessor.HttpContext.Session.SetString("Email", user.Email);
                TempData["LoginSuccess"] = "LoggedIn Successfully";
                ViewBag.UserName = user.Name;
                if (!user.IsPasswordChanged)
                {
                    return RedirectToAction("ResetPasswordByUser");
                }

                if (user.RoleId == (int)RoleEnum.Role.PM)
                    return RedirectToAction("Dashboard", "Manager");
                else if (user.RoleId == (int)RoleEnum.Role.TeamLeader)
                    return RedirectToAction("Kanban", "TeamLead");
                else if (user.RoleId == (int)RoleEnum.Role.SE)
                    return RedirectToAction("Dashboard", "Member");
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
                
                //if (user)
                //{
                //    TempData["ResetPassword"] = "Password Reset Successfully";
                //    return RedirectToAction("Login");
                //}
                //else
                //{
                //    TempData["ResetPasswordFailed"] = "Invalid Email! Try Again!";
                //    return View();
                //}
            }
            TempData["ResetPasswordFailed"] = "Invalid Email! Try Again!";
            return View();
        }

        public IActionResult LogOut()
        {
            Response.Cookies.Delete("jwt");
            _httpContextAccessor.HttpContext.Session.Clear();
            return View("Login");
        }

        public IActionResult ErrorPage()
        {
            return View();
        }

        public IActionResult ResetPasswordByUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetUserPassword(PasswordViewModel pwdModel)
        {
            ResetPWDViewModel model = new ResetPWDViewModel();
            model.Passwords = pwdModel;
            model.Passwords.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(pwdModel.Password, HashType.SHA512);
            model.Email = _httpContextAccessor.HttpContext.Session.GetString("Email");
            _userServices.ResetPassword(model);
            return View();
        }
    }
}
