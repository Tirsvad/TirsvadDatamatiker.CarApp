namespace CarApp
{
    internal static class Globals
    {
        public const string JsonFileName = "CarAppData.json";
        public const string DbSqliteFileName = "CarApp.db";
        public const string DbSqliteCreateDbFileName = "CreateDatabase.sql";

        public static DbSqliteHandler DbSqlHandler = new DbSqliteHandler(Globals.DbSqliteFileName);
    }
}
