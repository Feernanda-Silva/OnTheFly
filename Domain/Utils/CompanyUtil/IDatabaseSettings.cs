namespace API_Company.Utils
{
    public interface IDatabaseSettings
    {
        string CompanyCollectionName { get; set; }
        string DeleteCollectionName { get; set; }
        string AddressCollectionName { get; set; }
        string BlockedCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

    }
}
