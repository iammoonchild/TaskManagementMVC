using Microsoft.AspNetCore.Mvc;

namespace TaskManagementMVC.Controllers
{
    [Route("Task")]
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{taskId}")]
        public IActionResult TaskDetails([FromRoute] long taskId)
        {
            return View();
        }
    }
}
