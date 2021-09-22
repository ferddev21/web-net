using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClientImplement.Controllers
{
    [Authorize]
    public class TemplatingController : Controller
    {
        private readonly ILogger<TemplatingController> _logger;

        public TemplatingController(ILogger<TemplatingController> logger)
        {

            _logger = logger;

        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }

            ViewBag.Token = HttpContext.Session.GetString("JWToken");
            return View();
        }

        public IActionResult University()
        {
            return View();
        }

        public IActionResult Register()
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