using System.Collections.Generic;
using API_Aircraft.Models;
using API_Aircraft.Utils;
using MongoDB.Driver;

namespace API_Aircraft.Service
{
    public class DeletedAircraftService
    {
        #region Attribute
        private readonly IMongoCollection<DeletedAircraft> _deletedAircraft;
        #endregion

        #region Method
        public DeletedAircraftService(IDatabaseSettings settings)
        {
            var aircraft = new MongoClient(settings.ConnectionString);
            var database = aircraft.GetDatabase(settings.DatabaseName);
            _deletedAircraft = database.GetCollection<DeletedAircraft>(settings.DeletedAircraftCollectionName);
        }
        #endregion

        #region Insert Deleted AirCraft
        public DeletedAircraft Create(DeletedAircraft deletedAircraft)
        {
            _deletedAircraft.InsertOne(deletedAircraft);
            return deletedAircraft;
        }
        #endregion

        #region Read All Deleted AirCraft
        public List<DeletedAircraft> GetAllDeletedAircraft() => _deletedAircraft.Find<DeletedAircraft>(deletedaircraft => true).ToList();
        #endregion

        #region Read Deleted AirCraft One From RAB
        public DeletedAircraft GetDeletedAircraft(string rab) => _deletedAircraft.Find<DeletedAircraft>(deletedAircraft => deletedAircraft.RAB == rab).FirstOrDefault();
        #endregion
    }
}
