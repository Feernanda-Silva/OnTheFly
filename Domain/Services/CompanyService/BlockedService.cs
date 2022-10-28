using API_Company.Models;
using API_Company.Utils;
using MongoDB.Driver;

namespace API_Company.Services
{
    public class BlockedService
    {
        private readonly IMongoCollection<Blocked> _block;

        public BlockedService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _block = database.GetCollection<Blocked>(settings.BlockedCollectionName);
        }

        public void Create(Blocked block)
        {
            _block.InsertOne(block);
        }
    }
}

