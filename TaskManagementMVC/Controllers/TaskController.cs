using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{taskId}")]
        public IActionResult TaskDetails([FromRoute] long taskId)
        {

            return View();
        }

        [HttpPost("SaveComment")]
        public PartialViewResult SaveComment(string comment,long taskId)
        {
            return PartialView("_TaskLogs");
        }
    }
}
