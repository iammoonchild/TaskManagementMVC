using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.Models;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;
using TaskManagementMVC.Repositories.IRepositories;

namespace TaskManagementMVC.Repositories.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly DbTaskManagementContext _context;
        public UserRepo(DbTaskManagementContext context)
        {
            _context = context;
        }

        public void AddUser(AspNetUser user)
        {
            _context.AspNetUsers.Add(user);
            _context.SaveChanges();
        }

        public IQueryable<AspNetUser> GetTeamUsersFromTeamId(long teamId)
        {
            return _context.AspNetUsers.Include(x=>x.Team).Where(x => x.TeamId == teamId && (x.IsActive==true || x.IsActive == null));
        }

        public AspNetUser GetUserByEmail(string email)
        {
           return _context.AspNetUsers.Include(x=>x.Role).Where(x => x.Email == email).FirstOrDefault();
        }

        public AspNetUser GetUserFromUserId(long currentUserId)
        {
            return _context.AspNetUsers.Where(x => x.Id == currentUserId).FirstOrDefault();
        }

        public void ResetPassword(ResetPWDViewModel model)
        {
            var user = _context.AspNetUsers.Where(x => x.Email == model.Email).FirstOrDefault();
            user.OldPassword = user.Password;
            user.IsPasswordChanged = true;
            user.Password = model.Passwords.Password;
            user.ModifiedDate = new DateOnly(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
            user.ModifiedBy = user.Id;
            _context.Update(user);
            _context.SaveChanges();
        }

        public void UpdateUser(AspNetUser user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }
    }
}
