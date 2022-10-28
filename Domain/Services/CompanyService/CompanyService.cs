

using System.Collections.Generic;
using API_Company.Utils;
using Domain.Models;
using MongoDB.Driver;

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

        public List<Company> Get() => _company.Find<Company>(company => true).ToList();

        public Company Get(string cnpj) => _company.Find<Company>(company => company.Cnpj == cnpj).FirstOrDefault();
        public void Update(string cnpj , Company companyi)
        {
            _company.ReplaceOne(company => company.Cnpj == cnpj, companyi); 
        }

        public void Remove (Company company) //Remover e colocar na collection delete
        {

        }

    }
}
