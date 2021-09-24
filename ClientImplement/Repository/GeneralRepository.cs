using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ClientImplement.Base.Urls;
using ClientImplement.Repository.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClientImplement.Repository
{
    public class GeneralRepository<Entity, Key> : IRepository<Entity, Key>
    where Entity : class
    {
        private readonly Address address;
        private readonly string request;
        private readonly HttpClient httpClient;
        public GeneralRepository(Address address, string request)
        {
            this.address = address;
            this.request = request;

            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<List<Entity>> GetAll()
        {
            List<Entity> entities = new List<Entity>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<Entity>>(apiResponse);
            }
            return entities;
        }

        public async Task<Entity> Get(Key key)
        {
            Entity entity = null;

            using (var response = await httpClient.GetAsync(request + key))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<Entity>(apiResponse);
            }
            return entity;
        }

        public String Post(Entity entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

            return httpClient.PostAsync(request, content).Result.Content.ReadAsStringAsync().Result;

        }

        public String Put(Key key, Entity entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

            return httpClient.PutAsync(request, content).Result.Content.ReadAsStringAsync().Result;
        }
        public HttpStatusCode Delete(Key key)
        {
            var result = httpClient.DeleteAsync(request + key).Result;
            return result.StatusCode;
        }

    }
}