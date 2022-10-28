namespace Passenger_API.Utils
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string PassengerCollectionName { get; set; }
        public string AdressCollectionName { get; set; }
        public string DeletedPassengerCollectionName { get; set; }
        public string RestrictedCollectionName { get; set; }
    }
}
