namespace CarApp
{
    /// <summary>  
    /// Provides constant values used throughout the CarApp application.  
    /// </summary>  
    public static class Constants
    {
        #region File paths
        /// <summary>  
        /// The name of the JSON file used for storing application data.  
        /// </summary>  
        public const string jsonFilePath = "CarAppData.json";

        /// <summary>  
        /// The name of the SQLite database file.  
        /// </summary>  
        public const string dbSqliteFileName = "CarApp.db";

        /// <summary>  
        /// The name of the SQL file used for creating the SQLite database schema.  
        /// </summary>  
        public const string dbSqliteCreateDbFileName = "CreateDatabase.sql";

        #endregion File paths

        public const string carAppTitle = "Car Application";
        public const string carAppVersion = "v1.0";

        #region Error messages
        // Error Messages

        /// <summary>
        /// Error message for when no car is selected.
        /// </summary>
        public const string errorNoCarSelected = "Ingen bil valgt. Tilføj eller vælg en bil først.";

        #endregion Error messages
    }
}