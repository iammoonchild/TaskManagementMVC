using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;
using TaskManagementMVC.Services.IServices;

namespace TaskManagementMVC.Services.Services
{
    public class JWTService: IJWTService
    {
        private readonly IConfiguration configuration;

        public JWTService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateJWTToken(UserInfoViewModel model)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Role, model.Role),
                new Claim("RoleId",model.RoleId.ToString()),
                new Claim("firstName", model.Name.ToString()),
                new Claim("UserId", model.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(20);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken)
        {
            jwtSecurityToken = null;

            if (token == null)
            {
                return false;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("this is my custom Secret key for authentication");
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)

,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                jwtSecurityToken = (JwtSecurityToken)validatedToken;

                if (jwtSecurityToken != null)
                {
                    return true;
                }
                return false;

            }
            catch
            {
                return false;
            }
        }


        public UserInfoViewModel getDetails(string token)
        {
            JwtSecurityToken jwtSecurityToken = null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("this is my custom Secret key for authentication");


            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)

,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            // Corrected access to the validatedToken
            jwtSecurityToken = (JwtSecurityToken)validatedToken;

            UserInfoViewModel model = new UserInfoViewModel()
            {
                UserId = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "UserId").Value,
                Role = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value,
                Email = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value,
                Name = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "firstName").Value,
                RoleId = int.Parse(jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "RoleId").Value),
            };

            return model;
        }
    }
}
