using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;

namespace TaskManagementMVC.Services.IServices
{
    public interface IUserServices
    {
        bool CheckEmailDetails(string email);
        string CheckLoginDetails(LoginViewModel loginViewModel);
        UserInfoViewModel CheckValidUserWithRole(string email, string password);
    }
}
