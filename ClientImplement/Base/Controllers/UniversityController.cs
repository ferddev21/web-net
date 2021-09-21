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
    public class UniversityController : BaseController<University, UniversityRepository, int>
    {
        private readonly UniversityRepository repository;
        public UniversityController(UniversityRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}