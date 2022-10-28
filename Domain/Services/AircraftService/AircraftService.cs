using System.Collections.Generic;
using API_Aircraft.Models;
using API_Aircraft.Utils;
using MongoDB.Driver;

namespace API_Aircraft.Service
{
    public class AircraftService
    {
        #region Attribute
        private readonly IMongoCollection<Aircraft> _aircraftService;
        #endregion

        #region Method
        public AircraftService(IDatabaseSettings settings)
        {
            var aircraft = new MongoClient(settings.ConnectionString);
            var database = aircraft.GetDatabase(settings.DatabaseName);
            _aircraftService = database.GetCollection<Aircraft>(settings.AircraftCollectionName);
        }
        #endregion

        #region Insert AirCraft
        public Aircraft CreateAircraft(Aircraft aircraft)
        {
            _aircraftService.InsertOne(aircraft);
            return aircraft;
        }
        #endregion

        #region Read All AirCraft
        public List<Aircraft> GetAllAircraft() => _aircraftService.Find<Aircraft>(aircraft => true).ToList();
        #endregion

        #region Read AirCraft One From RAB
        public Aircraft GetByAircraft(string rab) => _aircraftService.Find<Aircraft>(aircraft => aircraft.RAB == rab).FirstOrDefault();
        #endregion

        #region Read AirCraft One From CNPJ
        //public List<AirCraft> GetByCNPJ(string cnpj) => _aircraft.Find<AirCraft>(aircraft => aircraft.CNPJ == cnpj).ToList();
        #endregion

        #region Update AirCraft
        public void UpdateAircraft(string rab, Aircraft aircraftIn) => _aircraftService.ReplaceOne(aircraft => aircraft.RAB == rab, aircraftIn);
        #endregion

        #region Delete AirCraft
        public void RemoveAircraft(Aircraft aircraftIn) => _aircraftService.DeleteOne(aircraft => aircraft.RAB == aircraftIn.RAB);
        #endregion
    }
}
