using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementMVC.Entities.ViewModels.UserViewModels
{
    public class TeamMembersViewModel
    {
        public string UserName { get; set; }
        public List<int> RoleId { get; set; }
        public List<long> TeamMemberId { get; set; }
        public List<string> FirstName { get; set; }
        public List<string> LastName { get; set; }
        public List<string> Email { get; set; }
        public List<string> Role { get; set; }

        public List<TeamMembersDataViewModel> GetTeamMembersData { get; set; }
    }

    public class TeamMembersDataViewModel
    {
        public int RoleId { get; set; }

        public long TeamMemberId { get; set; }

        public string Name { get; set; }
        
        public string Email { get; set; }
    }
}
