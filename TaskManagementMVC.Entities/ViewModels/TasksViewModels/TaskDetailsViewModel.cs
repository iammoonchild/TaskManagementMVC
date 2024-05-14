using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementMVC.Entities.ViewModels.TasksViewModels;
public class TaskDetailsViewModel
{
    public long Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string Status { get; set; }
    [Required]
    public string Description { get; set; }
    public string Type { get; set; }
    public string AssignedTo { get; set; }
    public string AssignedBy { get; set; }
    public string CreatedBy { get; set; }
    public string Avatar{ get; set; }
    [Required]
    public DateOnly Deadline { get; set; }
    public List<Dictionary<long,string>> Logs { get; set; }
    public List<Dictionary<long,string>> Comments { get; set; }
    public string Comment { get; set; }

}
