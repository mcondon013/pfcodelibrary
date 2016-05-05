namespace PFSQLBuilderObjects
{

    /// <summary>
    /// Used to determine which metadata provider and syntax checker the query builder will use.
    /// </summary>
    public enum QueryBuilderDatabasePlatform
    {
#pragma warning disable 1591
        Unknown = 0,          //if Unknown then Universal should be used
        MSSQLServer = 1,
        SQLServerCE = 2,
        OLEDB = 3,
        ODBC = 4,
        MySQL = 5,
        Oracle = 6,
        OracleNative = 7,
        DB2 = 8,
        Sybase = 9,
        Advantage = 10,
        Firebird = 11,
        Informix = 12,
        Postgre = 13,
        VistaDB4 = 14,
        Universal = 15,
        MSAccess = 16,
        SQLServerCE_V4 = 17,
        SQLAnywhere = 18,
        SQLAnywhereUL = 19
#pragma warning restore 1591
    }

    /// <summary>
    /// Used to determine what type of SQL syntax checking to use for OLEDB and ODBC connections.
    /// </summary>
    public enum AnsiSQLLevel
    {
#pragma warning disable 1591
        Unknown = 0,
        SQL89 = 1,
        SQL92 = 2,
        SQL2003 = 3
#pragma warning restore 1591
    }



}//end namespace