using System.Net.Http;
using System.Threading.Tasks;
using ClientImplement.Base.Urls;
using ClientImplement.Models;
using ClientImplement.Repository.Interface;
using Microsoft.AspNetCore.Http;
using netcore.Models;

namespace ClientImplement.Repository.Data
{
    public class UniversityRepository : GeneralRepository<University, int>
    {
        private readonly Address address;
        private readonly University university;
        private readonly string request;
        private readonly HttpClient httpClient;

        public UniversityRepository(Address address, string request = "university/") : base(address, request)
        {
            this.address = address;
            this.request = request;

        }
    }
}