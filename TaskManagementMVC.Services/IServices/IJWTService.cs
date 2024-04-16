using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;

namespace TaskManagementMVC.Services.IServices
{
    public interface IJWTService
    {
        string GenerateJWTToken(UserInfoViewModel model);
        bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken);
        UserInfoViewModel getDetails(string token);
    }
}
