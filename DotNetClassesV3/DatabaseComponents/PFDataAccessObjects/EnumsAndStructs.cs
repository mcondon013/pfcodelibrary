namespace PFDataAccessObjects
{
    /// <summary>
    /// Type of database in which the data access operations are occurring.
    /// </summary>
    public enum DatabasePlatform
    {
#pragma warning disable 1591
        Unknown = 0,          //if Unknown then generic syntax based on GetSchema DataTypes should be used
        MSSQLServer = 1,
        SQLServerCE35 = 2,
        SQLServerCE40 = 3,
        MSAccess = 4,
        OLEDB = 5,
        ODBC = 6,
        MySQL = 7,
        MSOracle = 8,
        OracleNative = 9,
        DB2 = 10,
        Sybase = 11,
        SQLAnywhere = 12,
        SQLAnywhereUltraLite = 13,
        Informix = 14
#pragma warning restore 1591
    }


    /// <summary>
    /// Enumerates the categories of data types that the .NET data types fall into.
    /// </summary>
    public enum DataCategory
    {
#pragma warning disable 1591
        Unknown = 0,
        Integer = 1,
        FloatingPoint = 2,
        Decimal = 3,
        Boolean = 4,
        DateTime = 5,
        String = 6,
        Char = 7,
        Byte = 8,
        CharArray = 9,
        ByteArray = 10,
        Guid = 11,
        Object = 12
#pragma warning restore 1591
    }

    /// <summary>
    /// Struct used in constucting generic odbc and oledb create table statements.
    /// </summary>
    public struct DataTypeMapping
    {
#pragma warning disable 1591
        public string DotNetDataType;
        public string DatabaseDataType;
        public long MaxLength;
        public DataCategory dataTypeCategory;
        public string CreateFormat;
        public string CreateParameters;
#pragma warning restore 1591
    }

    /// <summary>
    /// Enum for describing result of a table copy operation.
    /// </summary>
    public enum TableCopyResult
    {
#pragma warning disable 1591
        Success = 0,
        Failure = 1,
        Warning = 2,
        Alert = 3
#pragma warning restore 1591
    }

    /// <summary>
    /// Detailed table copy information.
    /// </summary>
    public class TableCopyDetails
    {
#pragma warning disable 1591
        public string sourceTableName;
        public string destinationTableName;
        public long numSourceRows;
        public long numRowsCopied;
        public TableCopyResult result;
        public string messages;
#pragma warning restore 1591
    }

    /// <summary>
    /// Three common parts to a table name.
    /// </summary>
    public class TableNameQualifiers
    {
#pragma warning disable 1591
        public string TableCatalog;
        public string TableSchema;
        public string TableName;
#pragma warning restore 1591
    }

    /// <summary>
    /// Maximum values for key SQLCE 3.5 connection string parameters.
    /// </summary>
    public enum enSQLCE35Defaults
    {
#pragma warning disable 1591
        MaxDatabaseSize = 128,
        MaxBufferSize = 640,
        MaxTempFileSize = 128
#pragma warning restore 1591
    }

    /// <summary>
    /// Maximum values for key SQLCE 4.0 connection string parameters.
    /// </summary>
    public enum enSQLCE40Defaults
    {
#pragma warning disable 1591
        MaxDatabaseSize = 256,
        MaxBufferSize = 4096,
        MaxTempFileSize = 256
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration of valid SQL Server CE 3.5 encryption modes.
    /// </summary>
    public enum SQLCE35EncryptionMode
    {
        /// <summary>
        /// Not specified or do not know.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// In this mode, the database is encrypted using AES256_SHA512, where AES256 is the encryption algorithm and SHA512 is the secure hash algorithm. The default key length is used to maintain backward compatibility with SQL Server Compact 3.5.
        /// </summary>
        EngineDefault = 1,
        /// <summary>
        /// The algorithms used in this mode are AES128_SHA256, where AES128 is the encryption algorithm with 128-bit key and SHA256 is the hash algorithm with 256-bit key. This is the default encryption mode option on all SQL Server Compact 4.0 supported platforms.
        /// </summary>
        PlatformDefault = 2,
        /// <summary>
        /// Pocket PC compatibility mode.
        /// </summary>
        PPC2003Compatibility = 3
    }

    /// <summary>
    /// Type of encryption for a SQL Server CE 4.0 database.
    /// </summary>
    public enum SQLCE40EncryptionMode
    {
        /// <summary>
        /// Not specified or do not know.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// In this mode, the database is encrypted using AES256_SHA512, where AES256 is the encryption algorithm and SHA512 is the secure hash algorithm. The default key length is used to maintain backward compatibility with SQL Server Compact 3.5.
        /// </summary>
        EngineDefault = 1,
        /// <summary>
        /// The algorithms used in this mode are AES128_SHA256, where AES128 is the encryption algorithm with 128-bit key and SHA256 is the hash algorithm with 256-bit key. This is the default encryption mode option on all SQL Server Compact 4.0 supported platforms.
        /// </summary>
        PlatformDefault = 2,
    }

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