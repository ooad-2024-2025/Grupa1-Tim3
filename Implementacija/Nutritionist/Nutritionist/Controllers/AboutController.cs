﻿using Microsoft.AspNetCore.Mvc;

namespace Nutritionist.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
