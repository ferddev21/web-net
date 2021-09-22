using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ClientImplement.Base.Urls;
using netcore.Models;
using netcore.ViewModel;
using Newtonsoft.Json;

namespace ClientImplement.Repository.Data
{
    public class AuthRepository : GeneralRepository<Account, string>
    {
        private readonly Address address;
        private readonly Account account;
        private readonly string request;
        private readonly HttpClient httpClient;

        public AuthRepository(Address address, string request = "account/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<JWTokenVM> Auth(LoginVM loginVM)
        {
            JWTokenVM token = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(request + "login", content);

            string apiResponse = await result.Content.ReadAsStringAsync();
            token = JsonConvert.DeserializeObject<JWTokenVM>(apiResponse);

            return token;
        }
    }
}