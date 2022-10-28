using MongoDB.Driver;
using Passenger_API.Models;
using Passenger_API.Utils;
using System.Collections.Generic;

namespace Passenger_API.Service
{
    public class RestrictedService
    {
        private readonly IMongoCollection<Restricted> _restricted;

        public RestrictedService(IDatabaseSettings settings)
        {
            var restricted = new MongoClient(settings.ConnectionString);
            var database = restricted.GetDatabase(settings.DatabaseName);
            _restricted = database.GetCollection<Restricted>(settings.RestrictedCollectionName);
        }
        public Restricted Create(Restricted restrictedpassenger)
        {
            _restricted.InsertOne(restrictedpassenger);
            return restrictedpassenger;
        }
        public List<Restricted> Get() => _restricted.Find<Restricted>(restricted => true).ToList();
        public Restricted Get(string cpf) => _restricted.Find<Restricted>(restricted => restricted.CPF == cpf).FirstOrDefault();
    }
}
