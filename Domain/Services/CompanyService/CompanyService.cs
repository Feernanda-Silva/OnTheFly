
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using API_Aircraft.Models;
using API_Company.Utils;
using Domain.Models;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace API_Company.Services
{
    public class CompanyService
    {
        private readonly IMongoCollection<Company> _company;

        public CompanyService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _company = database.GetCollection<Company>(settings.CompanyCollectionName);
        }

        public Company Create(Company company)
        {
            _company.InsertOne(company);
            return company; 
        }

        public async Task<Aircraft> PostAircraft(Aircraft aircraft)
        {

            using (HttpClient _aircraftClient = new HttpClient())
            {
                JsonContent content = JsonContent.Create(aircraft); 
                HttpResponseMessage response = await _aircraftClient.PostAsync("https://localhost:44321/api/Aircraft", content);
                var aircraftJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return aircraft = JsonConvert.DeserializeObject<Aircraft>(aircraftJson);
                else
                    return null;

            }
        }

        public List<Company> Get() => _company.Find<Company>(company => true).ToList();

        public Company Get(string cnpj) => _company.Find<Company>(company => company.Cnpj == cnpj).FirstOrDefault();
        public void Update(string cnpj , Company companyIn)
        {
            _company.ReplaceOne(company => company.Cnpj == cnpj, companyIn); 
        }

        public void Remove(Company companyIn) => _company.DeleteOne(company => company.Cnpj == companyIn.Cnpj);

    }
}
