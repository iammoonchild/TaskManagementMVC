using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.Models;
using TaskManagementMVC.Entities.ViewModels.Common;
using TaskManagementMVC.Entities.ViewModels.TeamLead;

namespace TaskManagementMVC.Services.IServices;
public interface ITeamLeadService
{
    TaskCardViewModel AddNewTask(TaskCardViewModel model, long currentUserId);
    void ChangeTaskStatus(long taskId, int statusId,long userId);
    long GetTeamIdFromUserId(long UserId);
    TLDashboardViewModel GetTeamLeadDashboard(long v);
    KanbanViewModel GetTeamLeadKanban(long teamId,string search=null, DateTime? dt = null);
    List<AspNetUser> GetTeamMembersDataForCalendar(long tLId, int teamId);
    List<Entities.ViewModels.Common.TaskCardViewModel> GetTeamTasksFromTeamId(long teamId);
}
