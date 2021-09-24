using System.Threading.Tasks;
using ClientImplement.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

public class BaseController<Entity, Repository, Key> : Controller
where Entity : class
where Repository : IRepository<Entity, Key>
{
    private readonly Repository repository;

    public BaseController(Repository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    public async Task<JsonResult> GetAll()
    {
        var result = await repository.GetAll();
        return Json(result);
    }

    [HttpGet("{key}")]
    public async Task<JsonResult> Get(Key key)
    {
        var result = await repository.Get(key);
        return Json(result);
    }

    [HttpPost]
    public JsonResult Post(Entity entity)
    {
        var result = repository.Post(entity);

        return Json(result);
    }

    [HttpPut]
    public JsonResult Put(Key key, Entity entity)
    {
        var result = repository.Put(key, entity);
        return Json(result);
    }

    [HttpDelete("{key}")]
    public JsonResult Delete(Key key)
    {
        var result = repository.Delete(key);

        return Json(result);
    }
}