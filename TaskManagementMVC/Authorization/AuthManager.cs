using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementMVC.Services.IServices;
using TaskManagementMVC.Repositories.IRepositories;
using System.IdentityModel.Tokens.Jwt;

namespace TaskManagementMVC.Authorization
{
    partial interface IAuthManager
    {
        //public string Login(string username, string password);    
    }
    public class AuthManager : IAuthManager
    {

    }
    public class CustomAuthorize : Attribute, IAuthorizationFilter
    {
        public readonly string _role;
        public CustomAuthorize(string role = "")
        {
            this._role = role;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            //var user = SessionUtils.SessionUtils.GetLoggedInUser(context.HttpContext.Session);
            var jwtService = context.HttpContext.RequestServices.GetService<IJWTService>();
            var adminRepo = context.HttpContext.RequestServices.GetService<IManagerRepo>();
            if (jwtService == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "Login", }));
                return;
            }

            var request = context.HttpContext.Request;
            var token = request.Cookies["jwt"];

            if (token == null || !jwtService.ValidateToken(token, out JwtSecurityToken jwtToken))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "Login", }));
                return;
            }

            var roleClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);
            if (roleClaim == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "ErrorPage", }));
                return;
            }
            if (string.IsNullOrWhiteSpace(_role) || roleClaim.Value != _role)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "ErrorPage", }));
                return;
            }

            //var user = SessionUtils.GetLoggedInUser(context.HttpContext.Session);

            //Redirect to login if not logged in
            //if (user == null)
            //{
            //    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Login" }));
            //    return;
            //}

            ////Redirect to access denied only if roles mismatch
            //if (!string.IsNullOrEmpty(_role))
            //{
            //    if (!(user?.Role == _role))
            //    {
            //        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Login" }));


            //    }
            //}

        }
    }
}
