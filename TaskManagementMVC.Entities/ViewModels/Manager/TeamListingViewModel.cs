using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementMVC.Entities.ViewModels.Manager;
public class TeamListingViewModel
{
    public long TeamId { get; set; }
    public string TeamAvatar { get; set; } // this will be TeamLeader (who is having role 2 in team)'s avatar
    public string TeamName { get; set; } // this will be TeamLeader (who is having role 2 in team)'s name
    public int TeamSize { get; set; }
    public DateOnly CreatedOn { get; set; }
}
