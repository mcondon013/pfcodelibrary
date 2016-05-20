//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using ADOX;
using ADODB;
using System.IO;
using PFDataAccessObjects;
using PFCollectionsObjects;
using PFTextObjects;
using PFListObjects;

namespace PFDataAccessObjects
{
    /// <summary>
    /// Enumeration of permitted OleDb providers for processing MsAccess databases.
    /// </summary>
    public enum PFAccessOleDbProvider
    {
        /// <summary>
        /// Used for Access 2002-2003 database file.
        /// </summary>
        MicrosoftJetOLEDB_4_0 = 1,
        /// <summary>
        /// Can be used for using either Access 2003 or Access 2007 database files.
        /// </summary>
        MicrosoftACEOLEDB_12_0 = 2
    }

    /// <summary>
    /// Used when constructing a connecting string. Engine type 5 is for Access 2003 databases; engine type 6 is for Access 2007 format databases.
    /// </summary>
    public enum EngineType
    {
#pragma warning disable 1591
        EngineType_5 = 5,
        EngineType_6 = 6
#pragma warning restore 1591
    }

#pragma warning disable 1591
    public enum AccessVersion
    {
        Access2003=1,
        Access2007=2
    }
#pragma warning restore 1591


    /// <summary>
    /// Class for processisng Microsoft Access databases.
    /// </summary>
    public class PFMsAccess : IDatabaseProvider 
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private DatabasePlatform _dbPlatform = DatabasePlatform.MSAccess;

        private OleDbConnection _conn = new OleDbConnection();
        private OleDbCommand _cmd = new OleDbCommand();
        private System.Data.CommandType _commandType = CommandType.Text;
        private int _commandTimeout = 300;
        private string _connectionString = string.Empty;
        private PFKeyValueList<string, string> _connectionStringKeyVals = new PFKeyValueList<string, string>();

        //private variables for properties
        private PFAccessOleDbProvider _oleDbProvider = PFAccessOleDbProvider.MicrosoftACEOLEDB_12_0;
        private string _databasePath = string.Empty;
        private string _databaseUsername = "admin";
        private string _databasePassword = string.Empty;
        private string _engineType = string.Empty;
        private string _sqlQuery = string.Empty;

#pragma warning disable 1591
        public delegate void ResultDelegate(DataColumnCollection columns, DataRow data, int tabNumber);
        public event ResultDelegate returnResult;
        public delegate void ResultAsStringDelegate(string outputLine, int tabNumber);
        public event ResultAsStringDelegate returnResultAsString;
#pragma warning restore 1591



        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        public PFMsAccess()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="databasePath">Full path to the database file.</param>
        public PFMsAccess(string databasePath)
        {
            _databasePath = databasePath;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="databasePath">Full path to the database file.</param>
        /// <param name="dbUsername">Username to use for database logon.</param>
        /// <param name="dbPassword">Password to use for database logon.</param>
        public PFMsAccess(string databasePath, string dbUsername, string dbPassword)
        {
            _databasePath = databasePath;
            _databaseUsername = dbUsername;
            _databasePassword = dbPassword;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbProvider">Allowed values are specified by the <see cref="PFAccessOleDbProvider"/> enumeration.</param>
        /// <param name="databasePath">Full path to the database file.</param>
        public PFMsAccess(PFAccessOleDbProvider dbProvider, string databasePath)
        {
            _oleDbProvider = dbProvider;
            _databasePath = databasePath;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbProvider">Allowed values are specified by the <see cref="PFAccessOleDbProvider"/> enumeration.</param>
        /// <param name="databasePath">Full path to the database file.</param>
        /// <param name="dbUsername">Username to use for database logon.</param>
        /// <param name="dbPassword">Password to use for database logon.</param>
        public PFMsAccess(PFAccessOleDbProvider dbProvider, string databasePath, string dbUsername, string dbPassword)
        {
            _oleDbProvider = dbProvider;
            _databasePath = databasePath;
            _databaseUsername = dbUsername;
            _databasePassword = dbPassword;
        }

        
        //properties

        /// <summary>
        /// Identifies the database platform supported by this class.
        /// </summary>
        public DatabasePlatform DbPlatform
        {
            get
            {
                return _dbPlatform;
            }
        }

        /// <summary>
        /// ADO.NET connection object for this instance.
        /// </summary>
        public DbConnection Connection
        {
            get
            {
                return _conn;
            }
        }

        /// <summary>
        /// ADO.NET command object for this instance.
        /// </summary>
        public DbCommand Command
        {
            get
            {
                return _cmd;
            }
        }

        /// <summary>
        /// Number of seconds to wait before a command is timed out.
        /// </summary>
        public int CommandTimeout
        {
            get
            {
                return _commandTimeout;
            }
            set
            {
                _commandTimeout = value;
            }
        }

        /// <summary>
        /// OleDbProvider to use when reading and writing the database.
        /// </summary>
        /// <remarks>If provider is not specified, default provider Microsoft.ACE.OLEDB.12.0 will be used. </remarks>
        public PFAccessOleDbProvider OleDbProvider
        {
            get
            {
                return _oleDbProvider;
            }
            set
            {
                _oleDbProvider = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// Full path to the database file.
        /// </summary>
        public string DatabasePath
        {
            get
            {
                return _databasePath;
            }
            set
            {
                _databasePath = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// Username for accessing the database.
        /// </summary>
        /// <remarks> If username is not specified , admin username will be used.</remarks>
        public string DatabaseUsername
        {
            get
            {
                return _databaseUsername;
            }
            set
            {
                _databaseUsername = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// Password for the database.
        /// </summary>
        /// <remarks> If password is not specified , empty string will be used as password.</remarks>
        public string DatabasePassword
        {
            get
            {
                return _databasePassword;
            }
            set
            {
                _databasePassword = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// Type of command: text or stored procedure.
        /// </summary>
        public System.Data.CommandType CommandType
        {
            get
            {
                return _commandType;
            }
            set
            {
                _commandType = value;
            }
        }

        /// <summary>
        /// SQL text to execute.
        /// </summary>
        public string SQLQuery
        {
            get
            {
                return _sqlQuery;
            }
            set
            {
                _sqlQuery = value;
            }
        }

        /// <summary>
        /// True if connection state is open, executing or fetching. Otherwise false.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                bool connected = false;

                if (_conn != null)
                    if (_conn.State == ConnectionState.Open || _conn.State == ConnectionState.Executing || _conn.State == ConnectionState.Fetching)
                        connected = true;

                return connected;
            }

        }

        /// <summary>
        /// Read-only property. Returns connection string that was built to enable the current connection.
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
                ParseConnectionString();
            }
        }

        /// <summary>
        /// Returns ConnectState property of the connection object.
        /// </summary>
        public ConnectionState ConnectionState
        {
            get
            {
                if (_conn != null)
                    return _conn.State;
                else
                    return ConnectionState.Closed;
            }
        }

        /// <summary>
        /// Returns list of all the keys and their values contained in the current connection string.
        /// </summary>
        public PFKeyValueList<string, string> ConnectionStringKeyVals
        {
            get
            {
                return GetConnectionStringKeyVals();
            }
        }


        //static methods
        /// <summary>
        /// Creates an empty Access database in specified databaseVersion format.
        /// </summary>
        /// <param name="databasePath">Path including file name of database.</param>
        /// <param name="databaseVersion">Access 2003 or Access 2007 database versions supported. See <see cref="AccessVersion"/> enumeration.</param>
        /// <param name="overwriteExistingFile">True to first delete any file with same name at databasePath. If False, any file exists, an exception will be thrown.</param>
        /// <param name="dbUsername">Username to logon with. Default is admin.</param>
        /// <param name="dbPassword">Password to logon with. Default is empty string.</param>
        /// <returns>Object representing an MS Acess database.</returns>
        public static PFMsAccess CreateDatabase(string databasePath, AccessVersion databaseVersion, bool overwriteExistingFile,  string dbUsername, string dbPassword)
        {
            string errMsg = string.Empty;
            PFMsAccess db = new PFMsAccess();
            string connectionString = string.Empty;
            Catalog cat = new Catalog();

            db.DatabasePath = databasePath;
            db.DatabaseUsername = dbUsername;
            db.DatabasePassword = dbPassword;

            switch (databaseVersion)
            {
                case AccessVersion.Access2003:
                    db.OleDbProvider = PFAccessOleDbProvider.MicrosoftJetOLEDB_4_0;
                    break;
                case AccessVersion.Access2007:
                    db.OleDbProvider = PFAccessOleDbProvider.MicrosoftACEOLEDB_12_0;
                    break;
                default:
                    errMsg = String.Format("Unexpected databaseVersion: {0}. Unable to create database.", databaseVersion.ToString());
                    break;
            }

            if(errMsg.ToString().Length>0)
                throw new System.Exception(errMsg);

            connectionString = db.BuildConnectionString();
            if (File.Exists(databasePath))
            {
                if (overwriteExistingFile)
                {
                    File.Delete(databasePath);
                }
                else
                {
                    errMsg = String.Format("File already exists and overwriteExistingFile is False: {0}", databasePath);
                    throw new System.Exception(errMsg);
                }
            }

            cat.Create(connectionString);


            return db;
        }


        //Instance methods

        private void ParseConnectionString()
        {
            PFParseString sqlConnBuilder = new PFParseString();
            string val = string.Empty;
            if (_connectionString.Trim().Length > 0)
            {
                sqlConnBuilder.KeyType = PFParseString.PFArgumentKeyType.NamedKey;
                sqlConnBuilder.Delimiters = ";";
                sqlConnBuilder.StringToParse = _connectionString.Trim();

                PFParseString.PFKeyValuePair kv = new PFParseString.PFKeyValuePair();
                _msg.Append("Show first to last:\r\n");
                kv = sqlConnBuilder.GetFirstKeyValue();
                while (kv.Key != string.Empty)
                {
                    if (kv.Key.ToLower() == "provider")
                        _oleDbProvider = (kv.Key.ToLower() == PFAccessOleDbProvider.MicrosoftJetOLEDB_4_0.ToString() ? PFAccessOleDbProvider.MicrosoftJetOLEDB_4_0 : PFAccessOleDbProvider.MicrosoftACEOLEDB_12_0);
                    else if (kv.Key.ToLower() == "data source")
                        _databasePath = kv.Value;
                    else if (kv.Key.ToLower() == "user id" || kv.Key.ToLower() == "uid")
                        _databaseUsername = kv.Value;
                    else if (kv.Key.ToLower() == "pwd" || kv.Key.ToLower() == "password")
                        _databasePassword = kv.Value;
                    else if (kv.Key.ToLower() == "jet oledb:engine type")
                        _engineType = kv.Value;
                    else
                        val = string.Empty;   //placeholder: nothing to do
                    kv = sqlConnBuilder.GetNextKeyValue();
                }
                if (_engineType == string.Empty)
                {
                    if (_oleDbProvider == PFAccessOleDbProvider.MicrosoftJetOLEDB_4_0)
                        _engineType = "5";
                    else if (_oleDbProvider == PFAccessOleDbProvider.MicrosoftACEOLEDB_12_0)
                        _engineType = "6";
                    else if (Path.GetExtension(_databasePath).ToLower() == ".accdb")
                        _engineType = "6";
                    else if (Path.GetExtension(_databasePath).ToLower() == ".mdb")
                        _engineType = "5";
                    else
                        _engineType = string.Empty;
                }

            }
            else
            {
                _oleDbProvider = PFAccessOleDbProvider.MicrosoftACEOLEDB_12_0;
                _databasePath = string.Empty;
                _databaseUsername = "admin";
                _databasePassword = string.Empty;
                _engineType = string.Empty;
            }

        }//end method


        
        private string BuildConnectionString()
        {
            _str.Length = 0;
            _str.Append("Provider=");
            _str.Append(this.OleDbProvider == PFAccessOleDbProvider.MicrosoftJetOLEDB_4_0 ? "Microsoft.Jet.OLEDB.4.0" : "Microsoft.ACE.OLEDB.12.0");
            _str.Append(";");
            _str.Append("Data Source=");
            _str.Append(this.DatabasePath);
            _str.Append(";");
            _str.Append("User Id=");
            _str.Append(this.DatabaseUsername);
            _str.Append(";");
            _str.Append("Password=");
            _str.Append(this.DatabasePassword);
            _str.Append(";");
            _str.Append("Jet OLEDB:Engine Type=");
            _str.Append(this.OleDbProvider == PFAccessOleDbProvider.MicrosoftJetOLEDB_4_0 ? "5" : "6");
            _str.Append(";");

            _connectionString = _str.ToString();

            return _str.ToString();
        }

        /// <summary>
        /// Returns a list of key/value pairs that contains all the keys and their associated values for the current connection string.
        /// </summary>
        /// <returns>List in key/value format.</returns>
        private PFKeyValueList<string, string> GetConnectionStringKeyVals()
        {
            
            OleDbConnectionStringBuilder odbcConnBuilder = new OleDbConnectionStringBuilder(this.ConnectionString);
            _connectionStringKeyVals.Clear();

            foreach (string key in odbcConnBuilder.Keys)
            {
                _connectionStringKeyVals.Add(new stKeyValuePair<string, string>(key, odbcConnBuilder[key].ToString()));
            }
            return this._connectionStringKeyVals;
        }


        /// <summary>
        /// Creates a table in the database using the definition contained in a DataTable object.
        /// </summary>
        /// <param name="dt">DataTable object containng table name and column definitions.</param>
        public bool CreateTable(DataTable dt)
        {
            bool bSuccess = true;
            ADOX.Table tab = new ADOX.Table();
            string tabName = String.IsNullOrEmpty(dt.TableName)==false ? dt.TableName : "Table01";
            string colName = string.Empty;
            ADOX.DataTypeEnum colType = ADOX.DataTypeEnum.adVariant;
            int definedSize = 0;
            int colInx = 0;
            int maxColInx = -1;

            tab.Name = tabName;
            maxColInx = dt.Columns.Count - 1;
            for (colInx = 0; colInx <= maxColInx; colInx++)
            {
                colName = dt.Columns[colInx].ColumnName;
                colType = ADOX.DataTypeEnum.adVarBinary;
                definedSize = 0;
                switch (dt.Columns[colInx].DataType.FullName)
                {
                    case "System.String":
                        if(dt.Columns[colInx].MaxLength > 0 && dt.Columns[colInx].MaxLength <= 255)
                            colType = ADOX.DataTypeEnum.adVarWChar;
                        else
                            colType = ADOX.DataTypeEnum.adLongVarWChar;
                        definedSize = dt.Columns[colInx].MaxLength;
                        if (definedSize < 1)
                            definedSize = 1000000;
                        break;
                    case "System.Int32":
                        colType = ADOX.DataTypeEnum.adInteger;
                        definedSize = 0;
                        break;
                    case "System.UInt32":
                        colType = ADOX.DataTypeEnum.adDouble;
                        definedSize = 0;
                        break;
                    case "System.Int64":
                        colType = ADOX.DataTypeEnum.adDouble;
                        definedSize = 0;
                        break;
                    case "System.UInt64":
                        colType = ADOX.DataTypeEnum.adDouble;
                        definedSize = 0;
                        break;
                    case "System.Int16":
                        colType = ADOX.DataTypeEnum.adSmallInt;
                        definedSize = 0;
                        break;
                    case "System.UInt16":
                        colType = ADOX.DataTypeEnum.adInteger;
                        definedSize = 0;
                        break;
                    case "System.Double":
                        colType = ADOX.DataTypeEnum.adDouble;
                        definedSize = 0;
                        break;
                    case "System.Single":
                        colType = ADOX.DataTypeEnum.adSingle;
                        definedSize = 0;
                        break;
                    case "System.Decimal":
                        colType = ADOX.DataTypeEnum.adDouble;
                        definedSize = 0;
                        break;
                    case "System.DateTime":
                        colType = ADOX.DataTypeEnum.adDate;
                        definedSize = 0;
                        break;
                    case "System.Boolean":
                        colType = ADOX.DataTypeEnum.adBoolean;
                        definedSize = 0;
                        break;
                    case "System.Char":
                        colType = ADOX.DataTypeEnum.adUnsignedTinyInt;
                        definedSize = 0;
                        break;
                    case "System.Char[]":
                        colType = ADOX.DataTypeEnum.adLongVarWChar;
                        definedSize = 0;
                        break;
                    case "System.Byte":
                        colType = ADOX.DataTypeEnum.adUnsignedTinyInt;
                        definedSize = 0;
                        break;
                    case "System.Byte[]":
                        colType = ADOX.DataTypeEnum.adLongVarBinary;
                        definedSize = 0;
                        break;
                    case "System.SByte":
                        colType = ADOX.DataTypeEnum.adUnsignedTinyInt;
                        definedSize = 0;
                        break;
                    case "System.Guid":
                        colType = ADOX.DataTypeEnum.adGUID;
                        definedSize = 0;
                        break;
                    case "System.Object":
                        colType = ADOX.DataTypeEnum.adLongVarBinary;
                        definedSize = 0;
                        break;
                    default:
                        colType = ADOX.DataTypeEnum.adLongVarBinary;
                        definedSize = 0;
                        break;
                }
                ADOX.Column col = new Column();
                col.Name = colName;
                col.Type = colType;
                col.DefinedSize = definedSize;
                if(dt.Columns[colInx].AllowDBNull)
                    if(IsPrimaryKey(dt, colName) == false)
                        if(colType != ADOX.DataTypeEnum.adBoolean)
                            col.Attributes = ColumnAttributesEnum.adColNullable;
                //tab.Columns.Append(colName, colType, definedSize);
                tab.Columns.Append((object)col);

            }//end for

            ADOX.Catalog cat = new Catalog();
            string connectionString = this.BuildConnectionString();
            ADODB.Connection cnn = new ADODB.Connection();
            cnn.ConnectionString = connectionString;
            cnn.Open();
            cat.ActiveConnection = cnn;
            cat.Tables.Append(tab);
            cnn.Close();
            
            return bSuccess;
        }

        private bool IsPrimaryKey (DataTable dt, string colName)
        {
            bool isPK = false;

            if (dt.PrimaryKey.Length > 0)
            {
                for (int i = 0; i < dt.PrimaryKey.Length; i++)
                {
                    if (dt.PrimaryKey[i].ColumnName.ToUpper() == colName.ToUpper())
                    {
                        isPK = true;
                        break;
                    }
                }
            }


            return isPK;
        }

        /// <summary>
        /// Method creates a a table in the database based on the column definitions and table name contained in the DataTable parameter.
        /// </summary>
        /// <param name="dt">DataTable object.</param>
        /// <param name="createScript">Always an empty string for this class. Tables are built using ADO routines, not table create statements.</param>
        /// <returns>True if table created; otherwise false.</returns>
        public bool CreateTable(DataTable dt, out string createScript)
        {
            createScript = string.Empty;

            //return CreateTable(dt, out createScript);
            return CreateTable(dt);
        }

        /// <summary>
        /// Builds a SQL table create statement using the properties on the specified data table.
        /// </summary>
        /// <param name="dt">Object containing the table definition.</param>
        /// <returns>String containing a SQL table create statement.</returns>
        public string BuildTableCreateStatement(DataTable dt)
        {
            PFTableBuilder tableBuilder = new PFTableBuilder(this.DbPlatform);

            return tableBuilder.BuildTableCreateStatement(dt);


        }

        /// <summary>
        /// Function to determine if the type of table object is a user (base) table. This function is used to eliminate system tables from the output.
        /// </summary>
        /// <param name="dr">DataRow returned by GetSchema.</param>
        /// <returns>True if the table is a user or base table.</returns>
        public bool TypeIsUserTable(DataRow dr)
        {
            bool result = false;

            if (dr["TABLE_TYPE"].ToString().ToUpper() == "TABLE")
                result = true;

            return result;
        }

        /// <summary>
        /// Function to build a qualified table name. Ususally this means attaching schema name in front of table name. In some cases both the catalog name and the schema name will be prepended to the table namne.
        ///  Result depends on the requirements of the database platform implementing this function.
        /// </summary>
        /// <param name="dr">DataRow returned by GetSchema that contains Tables information.</param>
        /// <returns>Full table name in either schemaname.tablename or catalogname.schemaname.tablename format.</returns>
        public string GetFullTableName(DataRow dr)
        {
            return dr["TABLE_NAME"].ToString();
        }

        /// <summary>
        /// Function to return the catalog, schema and name parts of a fully qualified table name. Some databases will only return the schema and name since catalog is not used by those database engines.
        /// </summary>
        /// <param name="dr">DataRow returned by GetSchema that contains Tables information.</param>
        /// <returns>Object containing the different qualifiers in the table name.</returns>
        public TableNameQualifiers GetTableNameQualifiers(DataRow dr)
        {
            TableNameQualifiers tnq = new TableNameQualifiers();

            tnq.TableCatalog = string.Empty;
            tnq.TableSchema = string.Empty;
            tnq.TableName = dr["TABLE_NAME"].ToString();


            return tnq;
        }

        /// <summary>
        /// Routine to recreate with a different schema name. Used when transferring a table definition to a new database.
        /// </summary>
        /// <param name="tabDef">Object containing the table definition to be reformatted.</param>
        /// <param name="newSchemaName">Schema name to apply to the reformatted table name. NOTE: NewSchemaName, if any, will not be applied. UltraLite database does not use schema names.</param>
        /// <returns>Table name. Schema name is ignored by the UltraLite class.</returns>
        public string RebuildFullTableName(PFTableDef tabDef, string newSchemaName)
        {
            string tabName = string.Empty;

            tabName = tabDef.TableName;


            return tabName;
        }


        /// <summary>
        /// Method to determine if a table exists.
        /// </summary>
        /// <param name="tableName">Name of table.</param>
        /// <returns>True if table exists; otherwise false.</returns>
        public bool TableExists(string tableName)
        {
            bool ret = false;

            this.ReopenConnection();

            DataTable dt = _conn.GetSchema("tables");

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine(row["TABLE_NAME"].ToString().ToUpper() + " == " + tableName.ToUpper());
                if (row["TABLE_NAME"].ToString().ToUpper() == tableName.ToUpper())
                {
                    ret = true;
                    Console.WriteLine("Result is TRUE");
                    break;
                }
            }


            return ret;
        }

        /// <summary>
        /// Method to determine if a table exists in the database.
        /// </summary>
        /// <param name="schemaName">Name of the schema to which the table belongs.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table exists; otherwise false.</returns>
        public bool TableExists(string schemaName, string tableName)
        {
            return TableExists(tableName);
        }

        /// <summary>
        /// Method to determine if a table exists in the database.
        /// </summary>
        /// <param name="catalogName">Name of database. This parameter is ignored and only schema and table name is used for the existence check.</param>
        /// <param name="schemaName">Name of the schema to which the table belongs.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table exists; otherwise false.</returns>
        public bool TableExists(string catalogName, string schemaName, string tableName)
        {
            return TableExists(tableName);
        }


        /// <summary>
        /// Routine uses table name information supplied by a PFTableDef object to determine if a table exists in the database.
        /// </summary>
        /// <param name="td">Object containing table definition information.</param>
        /// <returns>True if table exists; otherwise false.</returns>
        public bool TableExists(PFTableDef td)
        {
            return TableExists(td.TableOwner, td.TableName);
        }


        /// <summary>
        /// Method to remove a table from the database.
        /// </summary>
        /// <param name="tableName">Name of table to be deleted.</param>
        /// <returns>Returns true if table deleted; otherwise returns false. </returns>
        public bool DropTable(string tableName)
        {
            bool ret = false;
            StringBuilder sql = new StringBuilder();

            if (TableExists(tableName))
            {
                sql.Length = 0;
                sql.Append("DROP TABLE ");
                sql.Append(tableName);
                RunNonQuery(sql.ToString(), CommandType.Text);
                if (TableExists(tableName) == false)
                    ret = true;
                else
                    ret = false;
            }
            else
                ret = true;

            return ret;
        }

        /// <summary>
        /// Method to drop a table from the database.
        /// </summary>
        /// <param name="schemaName">Name of the schema to which the table belongs.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table no longer exists; otherwise returns false if table still exists.</returns>
        public bool DropTable(string schemaName, string tableName)
        {
            return DropTable(tableName);
        }

        /// <summary>
        /// Method to drop a table from the database.
        /// </summary>
        /// <param name="catalogName">Catalog name is ignored by this version of method.</param>
        /// <param name="schemaName">Schema or owner name for the table.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table no longer exists; otherwise returns false if table still exists.</returns>
        public bool DropTable(string catalogName, string schemaName, string tableName)
        {
            return DropTable(tableName);
        }


        /// <summary>
        /// Routine retrieves table name information supplied by a PFTableDef object to drop (delete) a table in the database.
        /// </summary>
        /// <param name="td">Object containing table definition information.</param>
        /// <returns>True if table no longer exists; otherwise returns false if table still exists.</returns>
        public bool DropTable(PFTableDef td)
        {
            return DropTable(td.TableOwner, td.TableName);
        }


        /// <summary>
        /// Opens connection to database.
        /// </summary>
        public void OpenConnection()
        {
            _connectionString = this.BuildConnectionString();
            _conn.ConnectionString = _connectionString;
            _conn.Open();
        }

        /// <summary>
        /// Closes connection to database.
        /// </summary>
        public void CloseConnection()
        {
            if (_conn.State == ConnectionState.Open)
                _conn.Close();
        }


        /// <summary>
        /// Opens connection to database.
        /// </summary>
        public void ReopenConnection()
        {
            if (this.IsConnected)
                this.CloseConnection();
            this.OpenConnection();
        }


        /// <summary>
        /// Transforms a DataReader object into a DataTable object.
        /// </summary>
        /// <param name="rdr">DataReader object.</param>
        /// <returns>DataTable object.</returns>
        public DataTable ConvertDataReaderToDataTable(DbDataReader rdr)
        {
            return ConvertDataReaderToDataTable(rdr, "Table");
        }

        /// <summary>
        /// Transforms a DataReader object into a DataTable object.
        /// </summary>
        /// <param name="rdr">DataReader object.</param>
        /// <param name="tableName">Name that identifies the table.</param>
        /// <returns>DataTable object.</returns>
        public DataTable ConvertDataReaderToDataTable(DbDataReader rdr, string tableName)
        {
            DataTable dtSchema = rdr.GetSchemaTable();
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            // You can also use an ArrayList instead of List<>
            List<DataColumn> listCols = new List<DataColumn>();
            if (dtSchema != null)
            {
                foreach (DataRow drow in dtSchema.Rows)
                {
                    string columnName = System.Convert.ToString(drow["ColumnName"]);
                    DataColumn column = new DataColumn(columnName, (Type)(drow["DataType"]));
                    column.Unique = (bool)drow["IsUnique"];
                    column.AllowDBNull = (bool)drow["AllowDBNull"];
                    column.AutoIncrement = (bool)drow["IsAutoIncrement"];
                    if (column.DataType.FullName == "System.String")
                        column.MaxLength = (int)drow["ColumnSize"];
                    else
                        column.MaxLength = -1;
                    listCols.Add(column);
                    dt.Columns.Add(column);
                }

            }

            // Read rows from DataReader and populate the DataTable

            while (rdr.Read())
            {
                DataRow dataRow = dt.NewRow();
                for (int i = 0; i < listCols.Count; i++)
                {
                    dataRow[((DataColumn)listCols[i])] = rdr[i];

                }
                dt.Rows.Add(dataRow);

            }


            return dt;

        }//end method

        /// <summary>
        /// Runs query specified via properties.
        /// </summary>
        /// <returns>Data reader object.</returns>
        public DbDataReader RunQueryDataReader()
        {
            if (_sqlQuery.Trim().Length == 0)
                throw new Exception("You must specify a SQL query to execute.");

            return RunQueryDataReader(_sqlQuery);
        }

        /// <summary>
        /// Runs query specified by the parameters.
        /// </summary>
        /// <param name="sqlQuery">SQL to execute.</param>
        /// <returns>Data reader object.</returns>
        public DbDataReader RunQueryDataReader(string sqlQuery)
        {
            _cmd.Connection = _conn;
            _cmd.CommandType = _commandType;
            _cmd.CommandTimeout = _commandTimeout;
            _cmd.CommandText = sqlQuery;
            _sqlQuery = sqlQuery;
            DbDataReader rdr = _cmd.ExecuteReader();
            return rdr;
        }

        /// <summary>
        /// Runs query specified by the parameters.
        /// </summary>
        /// <param name="sqlQuery">SQL to execute.</param>
        /// <param name="pCommandType">Type of command to run: text or stored procedure.</param>
        /// <returns>Data reader object.</returns>
        public DbDataReader RunQueryDataReader(string sqlQuery, CommandType pCommandType)
        {
            return RunQueryDataReader(sqlQuery);
        }

        /// <summary>
        /// Runs query.
        /// </summary>
        /// <returns>Returns dataset object.</returns>
        public DataSet RunQueryDataSet()
        {
            return RunQueryDataSet(_sqlQuery, _commandType);
        }

        /// <summary>
        /// Runs query specified by the parameters.
        /// </summary>
        /// <param name="sqlQuery">SQL to execute.</param>
        /// <param name="pCommandType">Type of command to run: text or stored procedure.</param>
        /// <returns>Dataset object.</returns>
        public DataSet RunQueryDataSet(string sqlQuery, CommandType pCommandType)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataSet ds = new DataSet();
            _cmd.Connection = _conn;
            _cmd.CommandType = pCommandType;
            _cmd.CommandTimeout = _commandTimeout;
            _cmd.CommandText = sqlQuery;
            _commandType = pCommandType;
            _sqlQuery = sqlQuery;

            da.SelectCommand = _cmd;
            da.FillSchema(ds, SchemaType.Source);
            da.Fill(ds);

            return ds;

        }

        /// <summary>
        /// Runs query.
        /// </summary>
        /// <returns>Returns dataset object.</returns>
        public DataTable RunQueryDataTable()
        {
            return RunQueryDataTable(_sqlQuery, _commandType);
        }


        /// <summary>
        /// Runs query specified by the parameters.
        /// </summary>
        /// <param name="sqlQuery">SQL to execute.</param>
        /// <param name="pCommandType">Type of command to run: text or stored procedure.</param>
        /// <returns>DataTable object.</returns>
        public DataTable RunQueryDataTable(string sqlQuery, CommandType pCommandType)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            _cmd.Connection = _conn;
            _cmd.CommandType = pCommandType;
            _cmd.CommandTimeout = _commandTimeout;
            _cmd.CommandText = sqlQuery;
            _commandType = pCommandType;
            _sqlQuery = sqlQuery;

            da.SelectCommand = _cmd;
            da.FillSchema(dt, SchemaType.Source);
            da.Fill(dt);

            return dt;
        }

        /// <summary>
        /// Runs a non-query (e.g. update, insert, delete statements).
        /// </summary>
        /// <returns>Number of rows affected.</returns>
        public int RunNonQuery()
        {
            return RunNonQuery(_sqlQuery, _commandType);
        }

        /// <summary>
        /// Runs a non-query (e.g. update, insert, delete statements) using the query specified in the parameters.
        /// </summary>
        /// <param name="sqlText">SQL to execute.</param>
        /// <param name="pCommandType">Type of command to run: text or stored procedure.</param>
        /// <returns>Number of rows affected.</returns>
        public int RunNonQuery(string sqlText, CommandType pCommandType)
        {
            int numRowsAffected = -1;
            OleDbCommand cmd = new OleDbCommand(sqlText, _conn);
            cmd.CommandTimeout = _commandTimeout;
            numRowsAffected = cmd.ExecuteNonQuery();
            return numRowsAffected;
        }


        /// <summary>
        /// Retrieves the data schema represented by the query text provided to this instance of the class.
        /// </summary>
        /// <returns>Returns data table object that contains schema for the query defined for this instance.</returns>
        public DataTable GetQueryDataSchema()
        {
            return GetQueryDataSchema(_sqlQuery, _commandType);
        }

        /// <summary>
        /// Retrieves the data schema represented by the query text provided to this routine.
        /// </summary>
        /// <param name="sqlQuery">SQL to parse.</param>
        /// <param name="pCommandType">Type of command represented by the query: text or stored procedure.</param>
        /// <returns>DataTable object containing the schema information for the columns that would be returned by this query.</returns>
        public DataTable GetQueryDataSchema(string sqlQuery, CommandType pCommandType)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            _cmd.Connection = _conn;
            _cmd.CommandType = pCommandType;
            _cmd.CommandTimeout = _commandTimeout;
            _cmd.CommandText = sqlQuery;
            _commandType = pCommandType;
            _sqlQuery = sqlQuery;

            da.SelectCommand = _cmd;
            da.FillSchema(dt, SchemaType.Source);

            return dt;
        }



        //Routines to process results
        /// <summary>
        /// Returns data from a DataReader object to the caller.
        /// </summary>
        /// <param name="rdr">DataReader object containing data to be returned to the caller.</param>
        public void ProcessDataReader(DbDataReader rdr)
        {
            ProcessDataTable(ConvertDataReaderToDataTable(rdr), (int)1);
        }

        /// <summary>
        /// Returns data from a DataSet to the caller.
        /// </summary>
        /// <param name="ds">DataSet object containing data to be returned to the caller.</param>
        public void ProcessDataSet(DataSet ds)
        {
            int tabInx = 0;
            int maxTabInx = ds.Tables.Count - 1;

            for (tabInx = 0; tabInx <= maxTabInx; tabInx++)
            {
                ProcessDataTable(ds.Tables[tabInx], tabInx);
            }
        }

        /// <summary>
        /// Returns data from a DataTable to the caller.
        /// </summary>
        /// <param name="tab">DataTable object containing data to be returned to the caller.</param>
        public void ProcessDataTable(DataTable tab)
        {
            ProcessDataTable(tab, (int)1);
        }

        /// <summary>
        /// Returns data from a DataTable to the caller.
        /// </summary>
        /// <param name="tab">DataTable object containing data to be returned to the caller.</param>
        /// <param name="tableNumber">Arbitrary number used for identifying multiple DataTables.</param>
        private void ProcessDataTable(DataTable tab, int tableNumber)
        {
            PFDataProcessor dataProcessor = new PFDataProcessor();

            dataProcessor.returnResult += new PFDataProcessor.ResultDelegate(OutputResults);

            dataProcessor.ProcessDataTable(tab);

        }//end method

                /// <summary>
        /// Copies data from DataTable to a database table.
        /// </summary>
        /// <param name="dt">DataTable object containing data to import.</param>
        /// <remarks>Table name in the DataTable must be the same as the destination table name in the database.</remarks>
        public void ImportDataFromDataTable(DataTable dt)
        {
            ImportDataFromDataTable(dt, 1);
        }

        /// <summary>
        /// Copies data from DataTable to a database table.
        /// </summary>
        /// <param name="dt">DataTable object containing data to import.</param>
        /// <remarks>Table name in the DataTable must be the same as the destination table name in the database.</remarks>
        /// <param name="updateBatchSize">Number of individual SQL modification statements to include in a table modification operation. WARNING: This parameter is ignored and UpdateBatchSize is always set to 1 for this class.</param>
        public void ImportDataFromDataTable(DataTable dt, int updateBatchSize)
        {
            //close and reopen the connection to make sure instance knows about any schema changes
            this.ReopenConnection();

            OleDbDataAdapter da = new OleDbDataAdapter();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.TableDirect;
            _cmd.CommandTimeout = _commandTimeout;
            _cmd.CommandText = dt.TableName;

            da.SelectCommand = _cmd;
            OleDbCommandBuilder builder = new OleDbCommandBuilder(da);
            builder.QuotePrefix = "[";
            builder.QuoteSuffix = "]";
            da.InsertCommand = builder.GetInsertCommand();
            da.FillSchema(dt, SchemaType.Source);

            //check for invalid column names
            for (int colInx = 0; colInx < dt.Columns.Count; colInx++)
            {
                if (dt.Columns[colInx].ColumnName.ToLower() == "value")
                {
                    _msg.Length = 0;
                    _msg.Append("Column name ");
                    _msg.Append(dt.Columns[colInx].ColumnName);
                    _msg.Append(" is invalid. Please choose another name for the column.");
                    throw new DataException(_msg.ToString());
                }
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i].AcceptChanges();
                dt.Rows[i].SetAdded();
            }

            da.UpdateBatchSize = 1;   //only batch size = 1 supported by this class
            da.Update(dt);
            dt.AcceptChanges();


        }

        /// <summary>
        /// Produces data in delimited text format.
        /// </summary>
        /// <param name="rdr">Data reader object.</param>
        /// <param name="columnSeparator">One or more characters to denote end of a column.</param>
        /// <param name="lineTerminator">One or more characters to denote end of a line. "\r\n" is the standard line terminator.</param>
        /// <param name="columnNamesOnFirstLine">If true delimited list of column names is output at top of the data.</param>
        public void ExtractDelimitedDataFromDataReader(DbDataReader rdr,
                                                       string columnSeparator,
                                                       string lineTerminator,
                                                       bool columnNamesOnFirstLine)
        {
            ExtractDelimitedDataFromTable(ConvertDataReaderToDataTable(rdr), columnSeparator, lineTerminator, columnNamesOnFirstLine);
        }

        /// <summary>
        /// Produces data in delimited text format.
        /// </summary>
        /// <param name="ds">DataSet object.</param>
        /// <param name="columnSeparator">One or more characters to denote end of a column.</param>
        /// <param name="lineTerminator">One or more characters to denote end of a line. "\r\n" is the standard line terminator.</param>
        /// <param name="columnNamesOnFirstLine">If true delimited list of column names is output at top of the data.</param>
        public void ExtractDelimitedDataFromDataSet(DataSet ds,
                                                    string columnSeparator,
                                                    string lineTerminator,
                                                    bool columnNamesOnFirstLine)
        {
            PFDataExporter dataExporter = new PFDataExporter();

            dataExporter.returnResultAsString += new PFDataExporter.ResultAsStringDelegate(OutputResultsAsString);

            dataExporter.ExtractDelimitedDataFromDataSet(ds, columnSeparator, lineTerminator, columnNamesOnFirstLine);

        }//end method

        /// <summary>
        /// Produces data in delimited text format.
        /// </summary>
        /// <param name="tab">DataTable object.</param>
        /// <param name="columnSeparator">One or more characters to denote end of a column.</param>
        /// <param name="lineTerminator">One or more characters to denote end of a line. "\r\n" is the standard line terminator.</param>
        /// <param name="columnNamesOnFirstLine">If true delimited list of column names is output at top of the data.</param>
        public void ExtractDelimitedDataFromTable(DataTable tab,
                                                  string columnSeparator,
                                                  string lineTerminator,
                                                  bool columnNamesOnFirstLine)
        {
            PFDataExporter dataExporter = new PFDataExporter();

            dataExporter.returnResultAsString += new PFDataExporter.ResultAsStringDelegate(OutputResultsAsString);

            dataExporter.ExtractDelimitedDataFromTable(tab, columnSeparator, lineTerminator, columnNamesOnFirstLine);

        }//end method

        /// <summary>
        /// Produces data in fixed length text column format.
        /// </summary>
        /// <param name="rdr">Data reader object.</param>
        /// <param name="lineTerminator">If true, the output will be separated into lines using "\r\n" to terminate each line.</param>
        /// <param name="columnNamesOnFirstLine">If true, a list of column names in fixed length column format is output at top of the data.</param>
        /// <param name="allowDataTruncation">If true, data longer than column length will be truncated; otherwise an exception will be thrown.</param>
        public void ExtractFixedLengthDataFromDataReader(DbDataReader rdr,
                                                         bool lineTerminator,
                                                         bool columnNamesOnFirstLine,
                                                         bool allowDataTruncation)
        {
            ExtractFixedLengthDataFromTable(ConvertDataReaderToDataTable(rdr), lineTerminator, columnNamesOnFirstLine, allowDataTruncation);
        }

        /// <summary>
        /// Produces data in fixed length text column format.
        /// </summary>
        /// <param name="ds">DataSet object.</param>
        /// <param name="lineTerminator">If true, the output will be separated into lines using "\r\n" to terminate each line.</param>
        /// <param name="columnNamesOnFirstLine">If true, a list of column names in fixed length column format is output at top of the data.</param>
        /// <param name="allowDataTruncation">If true, data longer than column length will be truncated; otherwise an exception will be thrown.</param>
        public void ExtractFixedLengthDataFromDataSet(DataSet ds,
                                                      bool lineTerminator,
                                                      bool columnNamesOnFirstLine,
                                                      bool allowDataTruncation)
        {
            PFDataExporter dataExporter = new PFDataExporter();

            dataExporter.returnResultAsString += new PFDataExporter.ResultAsStringDelegate(OutputResultsAsString);

            dataExporter.ExtractFixedLengthDataFromDataSet(ds, lineTerminator, columnNamesOnFirstLine, allowDataTruncation);

        }

        /// <summary>
        /// Produces data in fixed length text column format.
        /// </summary>
        /// <param name="tab">DataTable object.</param>
        /// <param name="lineTerminator">If true, the output will be separated into lines using "\r\n" to terminate each line.</param>
        /// <param name="columnNamesOnFirstLine">If true, a list of column names in fixed length column format is output at top of the data.</param>
        /// <param name="allowDataTruncation">If true, data longer than column length will be truncated; otherwise an exception will be thrown.</param>
        public void ExtractFixedLengthDataFromTable(DataTable tab,
                                          bool lineTerminator,
                                          bool columnNamesOnFirstLine,
                                          bool allowDataTruncation)
        {
            PFDataExporter dataExporter = new PFDataExporter();

            dataExporter.returnResultAsString += new PFDataExporter.ResultAsStringDelegate(OutputResultsAsString);

            dataExporter.ExtractFixedLengthDataFromTable(tab, lineTerminator, columnNamesOnFirstLine, allowDataTruncation);

        }

        /// <summary>
        /// Reads contents of xml file into a DataTable
        /// </summary>
        /// <param name="filePath">Location of Xml file.</param>
        /// <returns>DataTable with data.</returns>
        public DataTable LoadXmlFileToDataTable(string filePath)
        {
            PFDataProcessor dataProcessor = new PFDataProcessor();

            return (dataProcessor.LoadXmlFileToDataTable(filePath));
        }

        /// <summary>
        /// Reads contents of xml file into a DataSet
        /// </summary>
        /// <param name="filePath">Location of Xml file.</param>
        /// <returns>DataSet with data.</returns>
        public DataSet LoadXmlFileToDataSet(string filePath)
        {
            PFDataProcessor dataProcessor = new PFDataProcessor();

            return (dataProcessor.LoadXmlFileToDataSet(filePath));
        }

        /// <summary>
        /// Writes contents of DataReader in Xml format to specified output file.
        /// </summary>
        /// <param name="rdr">Data Reader object.</param>
        /// <param name="filePath">Location of the Xml output file.</param>
        public void SaveDataReaderToXmlFile(DbDataReader rdr, string filePath)
        {
            DataTable dt = ConvertDataReaderToDataTable(rdr);

            SaveDataTableToXmlFile(dt, filePath);
        }

        /// <summary>
        /// Writes contents of DataReader plus the data schema in Xml format to specified output file.
        /// </summary>
        /// <param name="rdr">Data Reader object.</param>
        /// <param name="filePath">Location of the Xml output file.</param>
        public void SaveDataReaderWithSchemaToXmlFile(DbDataReader rdr, string filePath)
        {
            DataTable dt = ConvertDataReaderToDataTable(rdr);

            SaveDataTableWithSchemaToXmlFile(dt, filePath);
        }

        /// <summary>
        /// Writes data schema in Xsd format to specified output file.
        /// </summary>
        /// <param name="rdr">Data Reader object.</param>
        /// <param name="filePath">Location of the Xsd output file.</param>
        public void SaveDataReaderToXmlSchemaFile(DbDataReader rdr, string filePath)
        {
            DataTable dt = ConvertDataReaderToDataTable(rdr);

            SaveDataTableToXmlSchemaFile(dt, filePath);
        }

        /// <summary>
        /// Writes contents of DataTable in Xml format to specified output file.
        /// </summary>
        /// <param name="dt">DataTable object.</param>
        /// <param name="filePath">Location of the Xml output file.</param>
        public void SaveDataTableToXmlFile(DataTable dt, string filePath)
        {
            PFDataExporter dataExporter = new PFDataExporter();

            dataExporter.ExportDataTableToXmlFile(dt, filePath);
        }

        /// <summary>
        /// Writes both the contents of DataTable and the assoicated data schema in Xml format to specified output file.
        /// </summary>
        /// <param name="dt">DataTable object.</param>
        /// <param name="filePath">Location of the Xml output file.</param>
        public void SaveDataTableWithSchemaToXmlFile(DataTable dt, string filePath)
        {
            PFDataExporter dataExporter = new PFDataExporter();

            dataExporter.ExportDataTableWithSchemaToXmlFile(dt, filePath);
        }

        /// <summary>
        /// Writes the data schema in Xsd format to specified output file.
        /// </summary>
        /// <param name="dt">DataTable object.</param>
        /// <param name="filePath">Location of the Xsd output file.</param>
        public void SaveDataTableToXmlSchemaFile(DataTable dt, string filePath)
        {
            PFDataExporter dataExporter = new PFDataExporter();

            dataExporter.ExportDataTableToXmlSchemaFile(dt, filePath);
        }

        /// <summary>
        /// Writes contents of DataSet in Xml format to specified output file.
        /// </summary>
        /// <param name="ds">DataSet object.</param>
        /// <param name="filePath">Location of the Xml output file.</param>
        public void SaveDataSetToXmlFile(DataSet ds, string filePath)
        {
            PFDataExporter dataExporter = new PFDataExporter();

            dataExporter.ExportDataSetToXmlFile(ds, filePath);
        }

        /// <summary>
        /// Writes both the contents of DataSet and the assoicated data schema in Xml format to specified output file.
        /// </summary>
        /// <param name="ds">DataSet object.</param>
        /// <param name="filePath">Location of the Xml output file.</param>
        public void SaveDataSetWithSchemaToXmlFile(DataSet ds, string filePath)
        {
            PFDataExporter dataExporter = new PFDataExporter();

            dataExporter.ExportDataSetWithSchemaToXmlFile(ds, filePath);
        }

        /// <summary>
        /// Writes data schema of DataSet in Xsd format to specified output file.
        /// </summary>
        /// <param name="ds">DataSet object.</param>
        /// <param name="filePath">Location of the Xsd output file.</param>
        public void SaveDataSetToXmlSchemaFile(DataSet ds, string filePath)
        {
            PFDataExporter dataExporter = new PFDataExporter();

            dataExporter.ExportDataSetToXmlSchmaFile(ds, filePath);
        }




        //routines to receive results
        private void OutputResults(DataColumnCollection cols, DataRow data, int tableNumber)
        {
            if (this.returnResult != null)
                this.returnResult(cols, data, tableNumber);
        }

        private void OutputResultsAsString(string outputLine, int tableNumber)
        {
            if (this.returnResultAsString != null)
                this.returnResultAsString(outputLine, tableNumber);
        }


        /// <summary>
        /// Retrieves list of tables and their associated schema information contained in the database pointed to by the current connection.
        /// </summary>
        /// <returns>Object containing the list of table definitions.</returns>
        public PFList<PFTableDef> GetTableList()
        {
            return GetTableList(null, null);
        }

        /// <summary>
        /// Retrieves list of tables and their associated schema information contained in the database pointed to by the current connection.
        /// </summary>
        /// <param name="includePatterns">Wildard pattern to use when selecting which tables to include. Specify * or null or empty string for pattern to include all tables.</param>
        /// <param name="excludePatterns">Wildard pattern to use when selecting which tables to exclude. Specify * for pattern to exclude all tables. Specify null or empty string to exclude no tables.</param>
        /// <returns>Object containing the list of table definitions.</returns>
        public PFList<PFTableDef> GetTableList(string[] includePatterns, string[] excludePatterns)
        {
            PFTableDefinitions tabdefs = new PFTableDefinitions();
            return tabdefs.GetTableList(this, includePatterns, excludePatterns);
        }


        /// <summary>
        /// Method to convert table definitions from another database format to the data format supported by this class.
        /// </summary>
        /// <param name="tableDefs">Object containing the list of table definitions to be converted.</param>
        /// <param name="newSchemaName">Specify a new schema (owner) name for the tables when they are recreated in the database managed by the current instance.</param>
        /// <returns>Object containing the list of table definitions after they have been converted to match the data formats of the current instance.</returns>
        public PFList<PFTableDef> ConvertTableDefs(PFList<PFTableDef> tableDefs, string newSchemaName)
        {
            PFTableDefinitions tabdefs = new PFTableDefinitions();
            return tabdefs.ConvertTableDefs(tableDefs, this, newSchemaName);
        }

        /// <summary>
        /// Runs the table create statements contained in the provided tableDefs object.
        /// </summary>
        /// <param name="tableDefs">Object containing list of table definitions.</param>
        /// <returns>Number of tables created.</returns>
        /// <remarks>Will not create table if table already exists.</remarks>
        public int CreateTablesFromTableDefs(PFList<PFTableDef> tableDefs)
        {
            return CreateTablesFromTableDefs(tableDefs, false);
        }

        /// <summary>
        /// Runs the table create statements contained in the provided tableDefs object.
        /// </summary>
        /// <param name="tableDefs">Object containing list of table definitions.</param>
        /// <param name="dropBeforeCreate">If true and table already exists, table will be dropped and then recreated using the table definition in the supplied PFTableDefs list. If false, table create step is bypassed if table already exists.</param>
        /// <returns>Number of tables created.</returns>
        public int CreateTablesFromTableDefs(PFList<PFTableDef> tableDefs, bool dropBeforeCreate)
        {
            //PFTableDefinitions tabdefs = new PFTableDefinitions();
            //return tabdefs.CreateTablesFromTableDefs(this, tableDefs, dropBeforeCreate);

            int numTablesCreated = 0;
            PFTableDef td = null;
            string sqlStatement = string.Empty;

            tableDefs.SetToBOF();

            while ((td = tableDefs.NextItem) != null)
            {
                if (this.TableExists(td) && dropBeforeCreate)
                {
                    this.DropTable(td);
                }

                if (this.TableExists(td) == false)
                {
                    this.CreateTable(td.TableObject);
                    //sqlStatement = td.TableCreateStatement;
                    //this.RunNonQuery(sqlStatement, CommandType.Text);
                    numTablesCreated++;
                }

            }

            //refresh schema by closing and reopening the connection
            this.ReopenConnection();

            return numTablesCreated;

        }

        /// <summary>
        /// Runs the table create statements contained in the provided tableDefs object.
        /// </summary>
        /// <param name="sourceDatabase">Database containing source tables..</param>
        /// <param name="newSchemaName">Schema to use for identifying the destination tables.</param>
        /// <param name="dropBeforeCreate">If true and table already exists, table will be dropped and then recreated using the table definition in the supplied PFTableDefs list. If false, table create step is bypassed if table already exists.</param>
        /// <returns>Number of tables created.</returns>
        public PFList<TableCopyDetails> CopyTableDataFromTableDefs(PFDatabase sourceDatabase, string newSchemaName, bool dropBeforeCreate)
        {
            return CopyTableDataFromTableDefs(sourceDatabase, null, null, newSchemaName, dropBeforeCreate);
        }

        /// <summary>
        /// Runs the table create statements contained in the provided tableDefs object.
        /// </summary>
        /// <param name="sourceDatabase">Database containing source tables..</param>
        /// <param name="tableIncludePatterns">Wildard pattern to use when selecting which tables to include. Specify * or null or empty string for pattern to include all tables.</param>
        /// <param name="tableExcludePatterns">Wildard pattern to use when selecting which tables to exclude. Specify * for pattern to exclude all tables. Specify null or empty string to exclude no tables.</param>
        /// <param name="newSchemaName">Schema to use for identifying the destination tables.</param>
        /// <param name="dropBeforeCreate">If true and table already exists, table will be dropped and then recreated using the table definition in the supplied PFTableDefs list. If false, table create step is bypassed if table already exists.</param>
        /// <returns>Number of tables created.</returns>
        public PFList<TableCopyDetails> CopyTableDataFromTableDefs(PFDatabase sourceDatabase, string[] tableIncludePatterns, string[] tableExcludePatterns,
                                                                   string newSchemaName, bool dropBeforeCreate)
        {
            //PFTableDefinitions tabdefs = new PFTableDefinitions();
            //return tabdefs.CopyTableDataFromTableDefs(sourceDatabase, tableIncludePatterns, tableExcludePatterns, this, newSchemaName, dropBeforeCreate);
            PFList<TableCopyDetails> tableCopyLog = new PFList<TableCopyDetails>();
            PFTableDef td = null;
            string sqlStatement = string.Empty;
            string selectStatement = "select * from <TableName>";
            string newTableName = string.Empty;
            string tdToXml = string.Empty;
            PFTableDef newTd = null;

            PFList<PFTableDef> tableDefs = sourceDatabase.GetTableList(tableIncludePatterns, tableExcludePatterns);
            DataTable sourceData = null;

            tableDefs.SetToBOF();

            while ((td = tableDefs.NextItem) != null)
            {
                newTableName = td.TableName;
                tdToXml = td.ToXmlString();
                newTd = PFTableDef.LoadFromXmlString(tdToXml);

                if (newSchemaName.Trim().Length > 0)
                    newTd.TableObject.TableName = newSchemaName + "." + newTableName;
                else
                    newTd.TableObject.TableName = newTableName;
                newTd.TableFullName = newTd.TableObject.TableName;
                newTd.TableOwner = newSchemaName;
                newTd.TableName = newTableName;

                if (this.TableExists(newTd) && dropBeforeCreate)
                {
                    this.DropTable(newTd);
                }

                if (this.TableExists(newTd) == false)
                {
                    //sqlStatement = this.BuildTableCreateStatement(newTd.TableObject);
                    //this.RunNonQuery(sqlStatement, CommandType.Text);
                    this.CreateTable(newTd.TableObject);
                }

                //close and reopen the connection to make sure instance knows about any schema changes
                this.ReopenConnection();

                TableCopyDetails tcdetails = new TableCopyDetails();
                tcdetails.sourceTableName = td.TableFullName;
                tcdetails.destinationTableName = newTd.TableFullName;
                tcdetails.numSourceRows = -1;
                tcdetails.numRowsCopied = -1;
                try
                {
                    sqlStatement = selectStatement.Replace("<TableName>", td.TableObject.TableName);
                    sourceData = sourceDatabase.RunQueryDataTable(sqlStatement, CommandType.Text);
                    sourceData.TableName = newTd.TableObject.TableName;
                    tcdetails.numSourceRows = sourceData.Rows.Count;
                    if (sourceData.Rows.Count > 0)
                    {
                        this.ImportDataFromDataTable(sourceData);
                        tcdetails.numRowsCopied = sourceData.Rows.Count;
                        tcdetails.result = TableCopyResult.Success;
                        tcdetails.messages = string.Empty;
                    }
                    else
                    {
                        tcdetails.numRowsCopied = 0;
                        tcdetails.result = TableCopyResult.Alert;
                        tcdetails.messages = "The were no rows in the source table.";
                    }
                }
                catch (System.Exception ex)
                {
                    _msg.Length = 0;
                    _msg.Append("Attempt to copy ");
                    _msg.Append(td.TableFullName);
                    _msg.Append(" to ");
                    _msg.Append(newTd.TableFullName);
                    _msg.Append(" failed. Error message: ");
                    _msg.Append(PFTextObjects.PFTextProcessor.FormatErrorMessage(ex));
                    tcdetails.messages = _msg.ToString();
                    tcdetails.numRowsCopied = -1;
                    tcdetails.result = TableCopyResult.Failure;
                }

                tableCopyLog.Add(tcdetails);


                newTd = null;
                sourceData = null;
            }

            return tableCopyLog;

        }




    
    }//end class
}//end namespace
