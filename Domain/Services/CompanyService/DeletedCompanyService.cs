using API_Company.Models;
using API_Company.Utils;
using MongoDB.Driver;

namespace API_Company.Services
{
    public class DeletedCompanyService
    {
        private readonly IMongoCollection<DeletedCompany> _deleted;

        public DeletedCompanyService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _deleted = database.GetCollection<DeletedCompany>(settings.DeleteCollectionName);
        }

        public void Create(DeletedCompany delete)
        {
            _deleted.InsertOne(delete);
        }
    }
}
