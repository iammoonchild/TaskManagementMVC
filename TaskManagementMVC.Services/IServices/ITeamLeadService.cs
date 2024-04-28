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
    long GetTeamIdFromUserId(long UserId);
    KanbanViewModel GetTeamLeadKanban(long teamId);
    List<Entities.ViewModels.Common.TaskCardViewModel> GetTeamTasksFromTeamId(long teamId);
}
