﻿using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_Social_Network_UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
