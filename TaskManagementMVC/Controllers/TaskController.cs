﻿using Microsoft.AspNetCore.Mvc;

namespace TaskManagementMVC.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
