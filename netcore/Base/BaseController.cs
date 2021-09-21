using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using netcore.Repository.Interface;

namespace netcore.Base
{
    // [Authorize(Roles = "Manager")]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<Entity, Repository, Key> : ControllerBase
    where Entity : class
    where Repository : IGeneralRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [EnableCors("AllowAllOrigins")]
        [HttpGet]
        public ActionResult GetAll()
        {
            var Entity = repository.GetAll();
            if (Entity == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound, new
                {

                    status = (int)HttpStatusCode.NoContent,
                    result = repository.GetAll(),
                    message = "Data tidak ditemukan",

                });

            }
            else
            {
                // return StatusCode((int)HttpStatusCode.OK, new
                // {
                //     status = (int)HttpStatusCode.OK,
                //     result = repository.GetAll(),
                //     message = "Success",
                // });
                return Ok(Entity);
            }

        }

        [EnableCors("AllowAllOrigins")]
        [HttpGet("{key}")] //BASEURL/api/Entitys/18120571
        public ActionResult Get(Key key)
        {
            var Entity = repository.Get(key);
            if (Entity == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound, new
                {

                    status = (int)HttpStatusCode.NoContent,
                    result = repository.Get(key),
                    message = "Data tidak ditemukan",

                });
            }
            else
            {
                // return StatusCode((int)HttpStatusCode.OK, new
                // {
                //     status = (int)HttpStatusCode.OK,
                //     result = repository.Get(key),
                //     message = "Success",
                // });
                return Ok(Entity);
            }

        }

        [DisableCors]
        [HttpPost]
        public ActionResult Insert(Entity entity)
        {
            try
            {
                repository.Insert(entity);
                return StatusCode((int)HttpStatusCode.Created, new
                {
                    status = (int)HttpStatusCode.Created,
                    message = "Success created"
                });

            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    status = (int)HttpStatusCode.InternalServerError,
                    message = e.Message
                });
            }
        }

        [EnableCors("AllowAllOrigins")]
        [HttpPut]
        public ActionResult Update(Entity Entity)
        {

            try
            {
                repository.Update(Entity);
                return StatusCode((int)HttpStatusCode.OK, new
                {
                    status = (int)HttpStatusCode.OK,
                    message = "Success updated"
                });

            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    status = (int)HttpStatusCode.InternalServerError,
                    message = e.Message
                });
            }

        }

        [EnableCors("AllowAllOrigins")]
        [HttpDelete("{key}")]
        public ActionResult Delete(Key key)
        {
            try
            {
                repository.Delete(key);
                return StatusCode((int)HttpStatusCode.OK, new
                {
                    status = (int)HttpStatusCode.OK,
                    message = "Success deleted"
                });

            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    status = (int)HttpStatusCode.InternalServerError,
                    message = e.Message
                });
            }

        }
    }
}