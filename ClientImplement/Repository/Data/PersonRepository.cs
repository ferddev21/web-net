using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ClientImplement.Base.Urls;
using ClientImplement.Models;
using ClientImplement.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using netcore.Models;
using netcore.ViewModel;
using Newtonsoft.Json;
using System;

namespace ClientImplement.Repository.Data
{
    public class PersonRepository : GeneralRepository<Person, string>
    {
        private readonly Address address;
        private readonly Person person;
        private readonly string request;
        private readonly HttpClient httpClient;

        public PersonRepository(Address address, string request = "person/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<List<RegisterVM>> GetRegisterAll()
        {
            List<RegisterVM> registers = new List<RegisterVM>();
            using (var response = await httpClient.GetAsync(request + "register"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                registers = JsonConvert.DeserializeObject<List<RegisterVM>>(apiResponse);

            }
            return registers;
        }

        public async Task<RegisterVM> GetRegister(string nik)
        {
            RegisterVM register = new RegisterVM();

            using (var response = await httpClient.GetAsync(request + "register/" + nik))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                register = JsonConvert.DeserializeObject<RegisterVM>(apiResponse);
            }
            return register;
        }

    }
}