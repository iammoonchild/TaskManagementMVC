using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementMVC.Entities.ViewModels.UserViewModels
{
    public class UserInfoViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}