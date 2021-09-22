using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using netcore.Models;
using ClientImplement.Repository.Data;
using netcore.ViewModel;
using Microsoft.AspNetCore.Http;
using ClientImplement.Models;

namespace ClientImplement.Base.Controllers
{

    [Route("[controller]")]
    public class AuthController : BaseController<Account, AuthRepository, string>
    {

        private readonly AuthRepository repository;
        public AuthController(AuthRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpPost("login-check")]
        public async Task<IActionResult> Auth(string Email, string Password)
        {
            var loginVM = new LoginVM
            {
                Email = Email,
                Password = Password
            };

            var jwtToken = await repository.Auth(loginVM);
            var token = jwtToken.Token;

            if (token == null)
            {
                return RedirectToAction("login");
            }

            HttpContext.Session.SetString("JWToken", token);
            // HttpContext.Session.SetString("Name", jwtHandler.GetName(token));
            // HttpContext.Session.SetString("ProfilePicture", "assets/img/theme/user.png");

            return RedirectToAction("index", "templating");
        }

        //VIEW
        [HttpGet("login")]
        public IActionResult Login()
        {
            //If user login
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "templating");
            }

            return View();
        }

        [HttpGet("logout")]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}