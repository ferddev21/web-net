using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientImplement.Repository.Interface
{
    public interface IRepository<Entity, Key> where Entity : class
    {
        Task<List<Entity>> GetAll();
        Task<Entity> Get(Key key);
        string Post(Entity entity);
        string Put(Key key, Entity entity);
        HttpStatusCode Delete(Key key);
    }
}