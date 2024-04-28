using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.Models;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;

namespace TaskManagementMVC.Repositories.IRepositories
{
    public interface IUserRepo
    {
        IQueryable<AspNetUser> GetTeamUsersFromTeamId(long teamId);
        AspNetUser GetUserByEmail(string email);
        AspNetUser GetUserFromUserId(long currentUserId);
        void ResetPassword(ResetPWDViewModel model);
    }
}
