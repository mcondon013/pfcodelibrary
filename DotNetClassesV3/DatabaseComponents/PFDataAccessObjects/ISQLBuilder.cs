using System;
namespace PFDataAccessObjects
{
#pragma warning disable 1591
    public interface ISQLBuilder
    {
        AnsiSQLLevel AnsiSQLVersion { get; set; }
        string ConnectionString { get; set; }
        QueryBuilderDatabasePlatform ConvertDbPlatformToQueryBuilderPlatform(PFDataAccessObjects.DatabasePlatform dbPlat);
        QueryBuilderDatabasePlatform DatabasePlatform { get; set; }
        string ShowQueryBuilder(string queryText);
    }
#pragma warning restore 1591
}
