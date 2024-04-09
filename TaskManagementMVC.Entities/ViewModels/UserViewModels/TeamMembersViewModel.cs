﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementMVC.Entities.ViewModels.UserViewModels
{
    public class TeamMembersViewModel
    {
        public List<string> FirstName { get; set; }
        public List<string> LastName { get; set; }
        public List<string> Email { get; set; }
        public List<string> Role { get; set; }
    }
}