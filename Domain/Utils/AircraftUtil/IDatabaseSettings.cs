namespace API_Aircraft.Utils
{
    public interface IDatabaseSettings
    {
        #region Method
        string AircraftCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string DeletedAircraftCollectionName { get; set; }

        //inserir companhia aerea
        #endregion
    }
}
