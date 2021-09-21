using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ClientImplement.Repository.Interface
{
    public interface IRepository<Entity, Key> where Entity : class
    {
        Task<List<Entity>> GetAll();
        Task<Entity> Get(Key key);
        HttpStatusCode Post(Entity entity);
        HttpStatusCode Put(Key key, Entity entity);
        HttpStatusCode Delete(Key key);
    }
}