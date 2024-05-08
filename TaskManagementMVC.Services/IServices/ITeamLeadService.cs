using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.ViewModels.Common;

namespace TaskManagementMVC.Services.IServices;
public interface ITeamLeadService
{
    TaskCardViewModel AddNewTask(TaskCardViewModel model, long currentUserId);
    void ChangeTaskStatus(long taskId, int statusId,long userId);
    long GetTeamIdFromUserId(long UserId);
    KanbanViewModel GetTeamLeadKanban(long teamId,string search=null, DateTime? dt = null);
    List<Entities.ViewModels.Common.TaskCardViewModel> GetTeamTasksFromTeamId(long teamId);
}
