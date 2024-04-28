using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Repositories.Enums;

namespace TaskManagementMVC.Entities.ViewModels.Manager;
public class TeamWorkDetailsViewModel
{
    public int TeamId { get; set; }
    public IEnumerable<TeamMemberDetails> TeamMemberDetails { get; set; }
}

public class TeamMemberDetails
{
    public long MemberId { get; set; }
    public string MemberName { get; set; }
    public int NoOfTaskAssigned { get; set; }
    public int NoOfTaskInProgress { get; set; }
    public int NoOfTaskPending { get; set; }
    public int NoOfTaskCompletedOnBeforeDeadline { get; set; }
    public int NoOfTaskCompletedAfterDeadline { get; set; }
    public bool IsMemberActive { get; set; }
    public int Role { get; set; }
}