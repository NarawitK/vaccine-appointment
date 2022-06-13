namespace VaccineReportDataLib.DataAccess.UnitOfWork
{
    public static class Caller
    {
        public static IUnitOfWork GetDefaultUnitOfWork()
        {
            return new UnitOfWork(DatabaseCore.DatabaseFactory.GetDatabaseConnection());
        }
    }
}
