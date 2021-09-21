using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClientImplement.Controllers
{
    public class LatihanAjaxNetcoreController : Controller
    {
        private readonly ILogger<LatihanAjaxNetcoreController> _logger;

        public LatihanAjaxNetcoreController(ILogger<LatihanAjaxNetcoreController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

         public IActionResult University()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}