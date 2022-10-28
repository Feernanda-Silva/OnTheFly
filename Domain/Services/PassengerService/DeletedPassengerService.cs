using MongoDB.Driver;
using Passenger_API.Models;
using Passenger_API.Utils;
using System.Collections.Generic;

namespace Passenger_API.Service
{
    public class DeletedPassengerService
    {
        private readonly IMongoCollection<DeletedPassenger> _deletedPassenger;
        public DeletedPassengerService(IDatabaseSettings settings)
        {
            var passenger = new MongoClient(settings.ConnectionString);
            var database = passenger.GetDatabase(settings.DatabaseName);
            _deletedPassenger = database.GetCollection<DeletedPassenger>(settings.DeletedPassengerCollectionName);
        }
        public DeletedPassenger Create(DeletedPassenger deletedPassenger)
        {
            _deletedPassenger.InsertOne(deletedPassenger);
            return deletedPassenger;
        }
        public List<DeletedPassenger> Get() => _deletedPassenger.Find<DeletedPassenger>(deletedPassenger => true).ToList();
        public DeletedPassenger Get(string cpf) => _deletedPassenger.Find<DeletedPassenger>(deletedPassenger => deletedPassenger.CPF == cpf).FirstOrDefault();
    }
}
