using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClientImplement.Controllers
{
    public class LatihanController : Controller
    {
        private readonly ILogger<LatihanController> _logger;

        public LatihanController(ILogger<LatihanController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Display()
        {
            return View();
        }

        public IActionResult BoxModel()
        {
            return View();
        }

        public IActionResult Position()
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