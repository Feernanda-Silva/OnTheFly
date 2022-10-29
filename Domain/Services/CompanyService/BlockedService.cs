using API_Company.Models;
using API_Company.Utils;
using MongoDB.Driver;

namespace API_Company.Services
{
    public class BlockedService
    {
        private readonly IMongoCollection<Blocked> _blocked;

        public BlockedService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _blocked = database.GetCollection<Blocked>(settings.BlockedCollectionName);
        }

        public void Create(Blocked block)
        {
            _blocked.InsertOne(block);
        }

        public Blocked Get(string cnpj) => _blocked.Find<Blocked>(blocked => blocked.Cnpj == cnpj).FirstOrDefault();
    }
}

