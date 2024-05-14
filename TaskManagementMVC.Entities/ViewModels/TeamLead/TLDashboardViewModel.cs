using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementMVC.Entities.ViewModels.TeamLead;
public class TLDashboardViewModel
{
    public int TaskCompletionRate { get; set; }
    public int PendingTask { get; set; }
    public int InProgressTask { get; set; }
    public int CompletedTask { get; set; }
    public List<DashboardTeamMembersViewModel> TeamMembers { get; set; }
    public List<string> ManagerInstructions { get; set; }
}

public class DashboardTeamMembersViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Avatar { get; set; }
    public int PendingTask { get; set; }
}
