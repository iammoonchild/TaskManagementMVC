using Microsoft.AspNetCore.Mvc;
using TaskManagementMVC.Entities.ViewModels.TasksViewModels;
using TaskManagementMVC.Services.IServices;

namespace TaskManagementMVC.Controllers
{
    [Route("Task")]
    public class TaskController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITaskService _Service;
        public TaskController(IHttpContextAccessor httpContextAccessor, ITaskService service)
        {
            _httpContextAccessor = httpContextAccessor;
            _Service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("TaskDetails/{taskId}")]
        public IActionResult TaskDetails([FromRoute] long taskId)
        {
            TaskDetailsViewModel model = _Service.GetTaskDetails(taskId);
            return View(model);
        }

        [HttpPost("TaskDetails/{taskId}")]
        public IActionResult TaskDetails([FromRoute] long taskId,TaskDetailsViewModel model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));
            _Service.UpdateTask(taskId,model,userId);
            return View(model);
        }

        [HttpPost("SaveComment")]
        public PartialViewResult SaveComment(string comment,long taskId)
        {
            return PartialView("_TaskLogs");
        }
    }
}
