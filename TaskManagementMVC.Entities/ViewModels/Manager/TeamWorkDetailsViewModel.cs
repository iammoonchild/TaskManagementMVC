using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Repositories.Enums;

namespace TaskManagementMVC.Entities.ViewModels.Manager;
public class TeamWorkDetailsViewModel
{
    public int TeamId { get; set; }
    public IEnumerable<TeamMemberDetails> TeamMemberDetails { get; set; }
    public MemberForm Member{ get; set; } = new MemberForm();
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

public class MemberForm
{
    public int TeamId { get; set; }
    [Required]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public short RoleId { get; set; }
}