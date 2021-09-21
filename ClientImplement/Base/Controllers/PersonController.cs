using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ClientImplement.Base.Urls;
using ClientImplement.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using netcore.Base.Controllers;
using netcore.Models;

namespace ClientImplement.Base.Controllers
{

    [Route("[controller]")]
    public class PersonController : BaseController<Person, PersonRepository, string>
    {
        private readonly PersonRepository repository;
        public PersonController(PersonRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("register")]
        public async Task<JsonResult> GetRegister()
        {
            var result = await repository.GetRegisterAll();
            return Json(result);
        }

        [HttpGet("register/{nik}")]
        public async Task<JsonResult> GetRegisterByNIK(string nik)
        {
            var result = await repository.GetRegister(nik);
            return Json(result);
        }

    }

}