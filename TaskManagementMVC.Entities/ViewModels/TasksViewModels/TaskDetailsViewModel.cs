using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementMVC.Entities.ViewModels.TasksViewModels;
public class TaskDetailsViewModel
{
    public string Title { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string AssignedTo { get; set; }
    public string Avatar{ get; set; }
    public DateOnly CreatedDate { get; set; }
    public DateOnly Deadline { get; set; }
    public List<Dictionary<long,string>> Logs { get; set; }
    public string Comment { get; set; }

}
