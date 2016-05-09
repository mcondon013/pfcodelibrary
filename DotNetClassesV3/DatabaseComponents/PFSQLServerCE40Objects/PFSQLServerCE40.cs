//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;
using System.Data.Common;
using System.IO;
using PFDataAccessObjects;
using PFCollectionsObjects;
using PFListObjects;
using PFTextObjects;

namespace PFSQLServerCE40Objects
{
    /// <summary>
    /// Class contains functionality for working with SQL Server CE 4.0 databases.
    /// </summary>
    public class PFSQLServerCE40 : IDatabaseProvider, IDesktopDatabaseProvider  
    {

        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private DatabasePlatform _dbPlatform = DatabasePlatform.SQLServerCE40;

        private int _defaultMaxDatabaseSize = (int)enSQLCE40Defaults.MaxDatabaseSize;
        private int _defaultMaxBufferSize = (int)enSQLCE40Defaults.MaxBufferSize;
        private int _defaultMaxTempFileSize = (int)enSQLCE40Defaults.MaxTempFileSize;

        private string _databasePath = string.Empty;
        private string _databasePassword = string.Empty;
        private bool _encryptionOn = false;
        private SQLCE40EncryptionMode _encryptionMode = SQLCE40EncryptionMode.EngineDefault;
        private int _maxDatabaseSize = (int)enSQLCE40Defaults.MaxDatabaseSize;
        private int _maxBufferSize = (int)enSQLCE40Defaults.MaxBufferSize;
        private int _maxTempFileSize = (int)enSQLCE40Defaults.MaxTempFileSize;
        private string _connectionString = string.Empty;
        private PFCollectionsObjects.PFKeyValueList<string, string>  _connectionStringKeyVals = new PFCollectionsObjects.PFKeyValueList<string, string> ();
        private SqlCeConnection _conn = new SqlCeConnection();
        private SqlCeCommand _cmd = new SqlCeCommand();
        private System.Data.CommandType _commandType = CommandType.Text;
        private int _commandTimeout = 0;
        private string _sqlQuery = string.Empty;

#pragma warning disable 1591
        public delegate void ResultDelegate(DataColumnCollection columns, DataRow data, int tabNumber);
        public event ResultDelegate returnResult;
        public delegate void ResultAsStringDelegate(string outputLine, int tabNumber);
        public event ResultAsStringDelegate returnResultAsString;
#pragma warning restore 1591

        ///// <summary>
        ///// Type of encryption for the database.
        ///// </summary>
        //public enum PFEncryptionMode
        //{
        //    /// <summary>
        //    /// Not specified or do not know.
        //    /// </summary>
        //    Unknown = 0,
        //    /// <summary>
        //    /// In this mode, the database is encrypted using AES256_SHA512, where AES256 is the encryption algorithm and SHA512 is the secure hash algorithm. The default key length is used to maintain backward compatibility with SQL Server Compact 3.5.
        //    /// </summary>
        //    EngineDefault = 1,
        //    /// <summary>
        //    /// The algorithms used in this mode are AES128_SHA256, where AES128 is the encryption algorithm with 128-bit key and SHA256 is the hash algorithm with 256-bit key. This is the default encryption mode option on all SQL Server Compact 4.0 supported platforms.
        //    /// </summary>
        //    PlatformDefault = 2,
        //}

        /// <summary>
        /// Convert string description into a valid PFEncryptionMode value.
        /// </summary>
        /// <param name="encMode">String name of encryption mode.</param>
        /// <returns>PFEncryptionMode value.</returns>
        public static SQLCE40EncryptionMode GetEncryptionMode(string encMode)
        {
            SQLCE40EncryptionMode ret = SQLCE40EncryptionMode.EngineDefault;

            switch (encMode)
            {
                case "EngineDefault":
                    ret = SQLCE40EncryptionMode.EngineDefault;
                    break;
                case "PlatformDefault":
                    ret = SQLCE40EncryptionMode.PlatformDefault;
                    break;
                case "Engine Default":
                    ret = SQLCE40EncryptionMode.EngineDefault;
                    break;
                case "Platform Default":
                    ret = SQLCE40EncryptionMode.PlatformDefault;
                    break;
                case "engine default":
                    ret = SQLCE40EncryptionMode.EngineDefault;
                    break;
                case "platform default":
                    ret = SQLCE40EncryptionMode.PlatformDefault;
                    break;
                default:
                    ret = SQLCE40EncryptionMode.EngineDefault;
                    break;
            }

            return ret;
        }

        /// <summary>
        /// Converts PFEncryptionMode value to a string name.
        /// </summary>
        /// <param name="encMode">Encryption mode to convert.</param>
        /// <returns>String containing text description of the encryption mode.</returns>
        public static string GetEncryptionModeDescription(SQLCE40EncryptionMode encMode)
        {
            string ret = "engine default";

            switch (encMode)
            {
                case SQLCE40EncryptionMode.EngineDefault:
                    ret = "engine default";
                    break;
                case SQLCE40EncryptionMode.PlatformDefault:
                    ret = "platform default";
                    break;
                default:
                    ret = "engine default";
                    break;
            }

            return ret;
        }

        
        //constructors

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public PFSQLServerCE40()
        {
            ;
        }

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="databasePath">Path to database that will be represented by this instance.</param>
        public PFSQLServerCE40(string databasePath)
        {
            this.DatabasePath = databasePath;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="databasePath">Path to database that will be represented by this instance.</param>
        /// <param name="databasePassword">Password required to open the database file.</param>
        public PFSQLServerCE40(string databasePath, string databasePassword)
        {
            this.DatabasePath = databasePath;
            this.DatabasePassword = databasePassword;
        }

        //Properties

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
                //sqlce command timeout does not accept non-zero values
                _commandTimeout = 0;
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
        /// Path to database that will be represented by this instance.
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
        /// Password required to open the database file.
        /// </summary>
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
        /// If true, database is encrypted.
        /// </summary>
        public bool EncryptionOn
        {
            get
            {
                return _encryptionOn;
            }
            set
            {
                _encryptionOn = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// Type of database encryption.
        /// </summary>
        public SQLCE40EncryptionMode EncryptionMode
        {
            get
            {
                return _encryptionMode;
            }
            set
            {
                _encryptionMode = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// MaxDatabaseSize Property.
        /// </summary>
        /// <remarks>Specified in Megabytes. Default is 128.</remarks>
        public int MaxDatabaseSize
        {
            get
            {
                return _maxDatabaseSize;
            }
            set
            {
                _maxDatabaseSize = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// MaxBufferSize Property.
        /// </summary>
        /// <remarks>Specified in Kilobytes. Default is 640.</remarks>
        public int MaxBufferSize
        {
            get
            {
                return _maxBufferSize;
            }
            set
            {
                _maxBufferSize = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// MaxTempFileSize Property.
        /// </summary>
        /// <remarks>Specified in Megabytes. Default is 128.</remarks>
        public int MaxTempFileSize
        {
            get
            {
                return _maxTempFileSize;
            }
            set
            {
                _maxTempFileSize = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// Connection string to use when opening the database.
        /// </summary>
        public string ConnectionString
        {
            get
            {
                string cnnStr = _connectionString;
                return cnnStr;
            }
            set
            {
                _connectionString = value;
                ParseConnectionString();
            }
        }

        /// <summary>
        /// Returns list of all the keys and their values contained in the current connection string.
        /// </summary>
        public PFCollectionsObjects.PFKeyValueList<string, string> ConnectionStringKeyVals
        {
            get
            {
                return GetConnectionStringKeyVals();
            }
        }

        /// <summary>
        /// Returns true if the connection is open.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                bool ret = false;
                if (_conn != null)
                    if (_conn.State == ConnectionState.Open)
                        ret = true;
                return ret;
            }
        }


        //private methods

        private void ParseConnectionString()
        {
            SqlCeConnectionStringBuilder sqlConnBuilder;
            if (_connectionString.Trim().Length > 0)
            {
                sqlConnBuilder = new SqlCeConnectionStringBuilder(_connectionString.Trim());
                _databasePath = sqlConnBuilder.DataSource;
                _databasePassword = sqlConnBuilder.Password;
                _encryptionOn = sqlConnBuilder.Encrypt;
                try
                {
                    _encryptionMode = PFSQLServerCE40.GetEncryptionMode(sqlConnBuilder.EncryptionMode);
                }
                catch
                {
                    _encryptionMode = SQLCE40EncryptionMode.Unknown;
                }
                _maxDatabaseSize = sqlConnBuilder.MaxDatabaseSize;
                _maxBufferSize = sqlConnBuilder.MaxBufferSize;
                _maxTempFileSize = sqlConnBuilder.TempFileMaxSize;
            }
            else
            {
                _databasePath = string.Empty;
                _databasePassword = string.Empty;
                _encryptionOn = false;
                _encryptionMode = SQLCE40EncryptionMode.EngineDefault;
                _maxDatabaseSize = (int)enSQLCE40Defaults.MaxDatabaseSize;
                _maxBufferSize = (int)enSQLCE40Defaults.MaxBufferSize;
                _maxTempFileSize = (int)enSQLCE40Defaults.MaxTempFileSize;
            }
        }


        private string BuildConnectionString()
        {
            SqlCeConnectionStringBuilder sqlConnBuilder = new SqlCeConnectionStringBuilder();
            if (this.DatabasePath.Length > 0)
            {
                sqlConnBuilder.DataSource = this._databasePath;
            }
            if (this.DatabasePassword.Length > 0)
            {
                sqlConnBuilder.Password = this.DatabasePassword;
            }
            if (this.EncryptionOn)
            {
                sqlConnBuilder.Encrypt = this.EncryptionOn;
                if (this.EncryptionMode == SQLCE40EncryptionMode.Unknown)
                    this.EncryptionMode = SQLCE40EncryptionMode.EngineDefault;
                sqlConnBuilder.EncryptionMode = PFSQLServerCE40.GetEncryptionModeDescription(this.EncryptionMode);
            }
            if (this.MaxDatabaseSize != _defaultMaxDatabaseSize)
            {
                sqlConnBuilder.MaxDatabaseSize = this.MaxDatabaseSize;
            }
            if (this.MaxBufferSize != _defaultMaxBufferSize)
            {
                sqlConnBuilder.MaxBufferSize = this.MaxBufferSize;
            }
            if (this.MaxTempFileSize != _defaultMaxTempFileSize )
            {
                sqlConnBuilder.TempFileMaxSize = this.MaxTempFileSize;
            }


            _connectionString = sqlConnBuilder.ToString();

            return _connectionString;
        }

        /// <summary>
        /// Returns a list of key/value pairs that contains all the keys and their associated values for the current connection string.
        /// </summary>
        /// <returns>List in Key/Value format.</returns>
        private PFCollectionsObjects.PFKeyValueList<string, string> GetConnectionStringKeyVals()
        {
            SqlCeConnectionStringBuilder sqlConnBuilder = new SqlCeConnectionStringBuilder(this.ConnectionString);
            _connectionStringKeyVals.Clear();

            if (sqlConnBuilder.Keys != null)
            {
                foreach (string key in sqlConnBuilder.Keys)
                {
                    if(sqlConnBuilder[key] != null)
                        _connectionStringKeyVals.Add(new stKeyValuePair<string, string>(key, sqlConnBuilder[key].ToString()));
                    else
                        _connectionStringKeyVals.Add(new stKeyValuePair<string, string>(key, "<null>"));
                }
            }
            else
            {
                PFParseString connBuilder = new PFParseString();

                _connectionStringKeyVals.Clear();

                if (_connectionString.Trim().Length > 0)
                {
                    connBuilder.KeyType = PFParseString.PFArgumentKeyType.NamedKey;
                    connBuilder.Delimiters = ";";
                    connBuilder.StringToParse = _connectionString.Trim();

                    PFParseString.PFKeyValuePair kv = new PFParseString.PFKeyValuePair();
                    kv = connBuilder.GetFirstKeyValue();
                    while (kv.Key != string.Empty)
                    {
                        _connectionStringKeyVals.Add(new stKeyValuePair<string, string>(kv.Key, kv.Value));
                        kv = connBuilder.GetNextKeyValue();
                    }
                }
            }


            return this._connectionStringKeyVals;
        }


        //Public Methods

        /// <summary>
        /// Creates a SQLCE library object.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns>Object representing SQL CE 3.5 database engine.</returns>
        public SqlCeEngine GetSqlCeEngine(string connectionString)
        {
            SqlCeEngine engine = new SqlCeEngine(connectionString);
            return engine; 
        }

        /// <summary>
        /// Creates a new SQLCE 4.0 .sdf database file by copying a template .sdf file.
        /// </summary>
        /// <param name="databasePath">Full path to database file to be created.</param>
        /// <param name="pathToTemplateDatabase">Full path to the database file that will be the template to be copied to the new file name.</param>
        /// <returns>True if database is created. Otherwise false.</returns>
        /// <remarks>Database inherits encryption settings from the template database. The default template database is unencrypted.</remarks>
        public bool CreateDatabase(string databasePath, string pathToTemplateDatabase)
        {
            bool dbCreated = false;

            if (File.Exists(pathToTemplateDatabase) == false)
            {
                _msg.Length = 0;
                _msg.Append("Unable to find template database: ");
                _msg.Append(pathToTemplateDatabase);
                throw new System.Exception(_msg.ToString());
            }

            if (File.Exists(databasePath) == true)
            {
                _msg.Length = 0;
                _msg.Append("Output database file aready exists:  ");
                _msg.Append(databasePath);
                _msg.Append(". You must first delete the existing file before creating a new file with the same name.");
                throw new System.Exception(_msg.ToString());
            }

            File.Copy(pathToTemplateDatabase, databasePath, false);
            dbCreated = true;

            return dbCreated;
        }



        /// <summary>
        /// Creates a SQLCE database file.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns>True if database created.</returns>
        public bool CreateDatabase(string connectionString)
        {
            bool bSuccess = false;
            SqlCeEngine engine = new SqlCeEngine(connectionString);

            try
            {
                engine.CreateDatabase();
                bSuccess = true;
            }
            catch (SqlCeException cex)
            {
                bSuccess = false;
                throw cex;
            }
            catch (System.Exception ex)
            {
                bSuccess = false;
                throw ex;
            }
            finally
            {
                engine = null;
            }

            return bSuccess;
        }

        /// <summary>
        /// Method creates a a table in the database based on the column definitions and table name contained in the DataTable parameter.
        /// </summary>
        /// <param name="dt">DataTable object.</param>
        /// <returns>True if table created; otherwise false.</returns>
        public bool CreateTable(DataTable dt)
        {
            string createScript = string.Empty;

            return CreateTable(dt, out createScript);
        }

        /// <summary>
        /// Method creates a a table in the database based on the column definitions and table name contained in the DataTable parameter.
        /// </summary>
        /// <param name="dt">DataTable object.</param>
        /// <param name="createScript">Copy of the script used to create the table.</param>
        /// <returns>True if table created; otherwise false.</returns>
        public bool CreateTable(DataTable dt, out string createScript)
        {
            bool bSuccess = true;
            string sqlScript = string.Empty;
            int rowsAffected = 0;
            try
            {
                sqlScript = BuildTableCreateStatement(dt);
                createScript = sqlScript;
                if (this._conn.State != ConnectionState.Open)
                    this.OpenConnection();
                rowsAffected = this.RunNonQuery(sqlScript);
                bSuccess = true;
            }
            catch (SqlCeException cex)
            {
                bSuccess = false;
                throw cex;
            }
            catch (System.Exception ex)
            {
                bSuccess = false;
                throw ex;
            }
            finally
            {
                ;
            }

            return bSuccess;

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
        /// Routine uses table name information supplied by a PFTableDef object to determine if a table exists in the database.
        /// </summary>
        /// <param name="td">Object containing table definition information.</param>
        /// <returns>True if table exists; otherwise false.</returns>
        public bool TableExists(PFTableDef td)
        {
            return TableExists(td.TableOwner, td.TableName);
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
        /// Method to determine if a table exists in the database.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table exists; otherwise false.</returns>
        public bool TableExists(string tableName)
        {
            bool ret = false;
            StringBuilder sql = new StringBuilder();
            SqlCeDataReader rdr = null;
            int numRows = -1;

            sql.Length = 0;
            sql.Append("select count(*) as NumRows from INFORMATION_SCHEMA.TABLES where TABLE_NAME = '");
            sql.Append(tableName);
            sql.Append("';");

            rdr = (SqlCeDataReader)RunQueryDataReader(sql.ToString());

            if (rdr.Read())
            {
                numRows = Convert.ToInt32(rdr[0].ToString());
            }

            rdr.Close();

            if (numRows > 0)
                ret = true;
            else
                ret = false;

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
        /// Method to drop a table from the database.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table no longer exists; otherwise returns false if table still exists.</returns>
        public bool DropTable(string tableName)
        {
            bool ret = false;
            StringBuilder sql = new StringBuilder();

            if (TableExists(tableName))
            {
                sql.Length = 0;
                sql.Append("drop table ");
                sql.Append(tableName);
                sql.Append(";");

                RunNonQuery(sql.ToString());

                if (TableExists(tableName))
                    ret = false;
                else
                    ret = true;
            }
            else
            {
                ret = true;
            }

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
        /// Establishes connection to database.
        /// </summary>
        public void OpenConnection()
        {
            _conn.ConnectionString = this.ConnectionString;
            _conn.Open();
        }

        /// <summary>
        /// Closes database connection.
        /// </summary>
        public void CloseConnection()
        {
            if (_conn.State == ConnectionState.Open)
                _conn.Close();
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
            if(pCommandType != CommandType.Text)
            {
                _msg.Length=0;
                _msg.Append("Only text commands accepted by this sql ce object. Invalid command type for this object: ");
                _msg.Append(pCommandType.ToString());
                throw new System.Exception(_msg.ToString());
            }
            return RunNonQuery(sqlText);
        }

        /// <summary>
        /// Runs a non-query SQL statement.
        /// </summary>
        /// <param name="query">SQL statement.</param>
        /// <returns>Number of rows affected.</returns>
        public int RunNonQuery(string query)
        {
            int numRowsAffected = -1;
            SqlCeCommand cmd = new SqlCeCommand(query, _conn);
            cmd.CommandTimeout = _commandTimeout;
            numRowsAffected = cmd.ExecuteNonQuery();
            return numRowsAffected;
        }

        /// <summary>
        /// Runs query specified via properties.
        /// </summary>
        /// <returns>Data reader object.</returns>
        public DbDataReader RunQueryDataReader()
        {
            if (_sqlQuery.Trim().Length == 0)
                throw new Exception("You must specify a SQL query to execute.");

            return RunQueryDataReader(_sqlQuery, _commandType);
        }

        /// <summary>
        /// Runs query specified by the parameters.
        /// </summary>
        /// <param name="sqlQuery">SQL to execute.</param>
        /// <param name="pCommandType">Type of command to run: text or stored procedure.</param>
        /// <returns>Data reader object.</returns>
        public DbDataReader RunQueryDataReader(string sqlQuery, CommandType pCommandType)
        {
            if(pCommandType != CommandType.Text)
            {
                _msg.Length=0;
                _msg.Append("Only text commands accepted by this sql ce object. Invalid command type for this object: ");
                _msg.Append(pCommandType.ToString());
                throw new System.Exception(_msg.ToString());
            }
            return RunQueryDataReader(sqlQuery);
        }

        /// <summary>
        /// Runs query that returns results in DbDataReader format.
        /// </summary>
        /// <param name="query">SQL statement.</param>
        /// <returns>Result rows.</returns>
        public DbDataReader RunQueryDataReader(string query)
        {
            SqlCeCommand cmd = new SqlCeCommand(query, _conn);
            cmd.CommandTimeout = _commandTimeout;
            SqlCeDataReader rdr = cmd.ExecuteReader();
            return (rdr);
        }

        /// <summary>
        /// Runs query that returns results in SqlCeResultSet format.
        /// </summary>
        /// <param name="query">SQL statement.</param>
        /// <returns>Result rows.</returns>
        public SqlCeResultSet RunQueryResultset(string query)
        {
            SqlCeCommand cmd = new SqlCeCommand(query, _conn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = _commandTimeout;
            SqlCeResultSet res = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);
            return res;
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
            if(pCommandType != CommandType.Text)
            {
                _msg.Length=0;
                _msg.Append("Only text commands accepted by this sql ce object. Invalid command type for this object: ");
                _msg.Append(pCommandType.ToString());
                throw new System.Exception(_msg.ToString());
            }
            return RunQueryDataTable(sqlQuery, "query");
        }


        /// <summary>
        /// Runs query that returns results in ADO.NET datatable format.
        /// </summary>
        /// <param name="query">SQL statement.</param>
        /// <param name="queryName">Used to identify results in dataset.</param>
        /// <returns>ADO.NET datatable containing result rows.</returns>
        public DataTable RunQueryDataTable(string query, string queryName)
        {
            SqlCeCommand cmd = new SqlCeCommand(query, _conn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = _commandTimeout;
            DataTable dt = new DataTable();
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            da.FillSchema(dt, SchemaType.Source);
            da.Fill(dt);
            //da.FillSchema(dt, SchemaType.Source);
            return dt;
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
            SqlCeCommand cmd = new SqlCeCommand(sqlQuery, _conn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = _commandTimeout;
            DataTable dt = new DataTable();
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            da.FillSchema(dt, SchemaType.Source);

            return dt;
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
            if(pCommandType != CommandType.Text)
            {
                _msg.Length=0;
                _msg.Append("Only text commands accepted by this sql ce object. Invalid command type for this object: ");
                _msg.Append(pCommandType.ToString());
                throw new System.Exception(_msg.ToString());
            }
            return RunQueryDataset(sqlQuery, "query");

        }


        /// <summary>
        /// Runs query that returns results in ADO.NET dataset format.
        /// </summary>
        /// <param name="query">SQL statement.</param>
        /// <param name="queryName">Used to identify results in dataset.</param>
        /// <returns>ADO.NET dataset containing result rows.</returns>
        public DataSet RunQueryDataset(string query, string queryName)
        {
            SqlCeCommand cmd = new SqlCeCommand(query, _conn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = _commandTimeout;
            DataSet ds = new DataSet();
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            da.Fill(ds, queryName);
            da.FillSchema(ds, SchemaType.Source, queryName);
            return ds;
        }

        /// <summary>
        /// Transforms a DbDataReader object into a DataTable object.
        /// </summary>
        /// <param name="rdr">DbDataReader object.</param>
        /// <returns>DataTable object.</returns>
        public DataTable ConvertDataReaderToDataTable(DbDataReader rdr)
        {
            return ConvertDataReaderToDataTable(rdr, "Table");
        }

        /// <summary>
        /// Transforms a DbDataReader object into a DataTable object.
        /// </summary>
        /// <param name="rdr">DbDataReader object.</param>
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
                    column.Unique = Convert.ToBoolean(drow["IsUnique"] is System.DBNull?bool.FalseString:drow["isUnique"]);
                    column.AllowDBNull = Convert.ToBoolean(drow["AllowDBNull"] is System.DBNull ? bool.FalseString : drow["AllowDBNull"]);
                    column.AutoIncrement = Convert.ToBoolean(drow["IsAutoIncrement"] is System.DBNull ? bool.FalseString : drow["IsAutoIncrement"]);
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
        /// Transforms a SqlCeResultSet object into a DataTable object.
        /// </summary>
        /// <param name="res">SqlCeResultSet object.</param>
        /// <returns>DataTable object.</returns>
        public static DataTable ConvertResultSetToDataTable(SqlCeResultSet res)
        {
            return ConvertResultSetToDataTable(res, "Table");
        }

        /// <summary>
        /// Transforms a SqlCeResultSet object into a DataTable object.
        /// </summary>
        /// <param name="res">SqlCeResultSet object.</param>
        /// <param name="tableName">Name that identifies the table.</param>
        /// <returns>DataTable object.</returns>
        public static DataTable ConvertResultSetToDataTable(SqlCeResultSet res, string tableName)
        {
            DataTable dtSchema = res.GetSchemaTable();
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
                    column.Unique = Convert.ToBoolean(drow["IsUnique"] is System.DBNull ? bool.FalseString : drow["isUnique"]);
                    column.AllowDBNull = Convert.ToBoolean(drow["AllowDBNull"] is System.DBNull ? bool.FalseString : drow["AllowDBNull"]);
                    column.AutoIncrement = Convert.ToBoolean(drow["IsAutoIncrement"] is System.DBNull ? bool.FalseString : drow["IsAutoIncrement"]);
                    if (column.DataType.FullName == "System.String")
                        column.MaxLength = (int)drow["ColumnSize"];
                    else
                        column.MaxLength = -1;
                    listCols.Add(column);
                    dt.Columns.Add(column);
                }
            }

            // Read rows from DataReader and populate the DataTable

            while (res.Read())
            {
                DataRow dataRow = dt.NewRow();
                for (int i = 0; i < listCols.Count; i++)
                {
                    dataRow[((DataColumn)listCols[i])] = res[i];
                }
                dt.Rows.Add(dataRow);
            }

            return dt;

        }//end method

        /// <summary>
        /// Returns data from a DbDataReader object to the caller.
        /// </summary>
        /// <param name="rdr">DbDataReader object containing data to be returned to the caller.</param>
        /// <remarks>Results can be retrieved by subscribing to the ResultDelegate event. </remarks>
        public void ProcessDataReader(DbDataReader rdr)
        {
            ProcessDataTable(ConvertDataReaderToDataTable(rdr));
        }

        /// <summary>
        /// Returns data from a SqlCeResultSet object to the caller.
        /// </summary>
        /// <param name="res">SqlCeResultSet object containing data to be returned to the caller.</param>
        /// <remarks>Results can be retrieved by subscribing to the ResultDelegate event. </remarks>
        public void ProcessResultSet(SqlCeResultSet res)
        {
            ProcessDataTable(ConvertResultSetToDataTable(res));
        }

        /// <summary>
        /// Returns data from a DataSet to the caller.
        /// </summary>
        /// <param name="ds">DataSet object containing data to be returned to the caller.</param>
        /// <remarks>Results can be retrieved by subscribing to ResultDelegate event. </remarks>
        public void ProcessDataSet(DataSet ds)
        {
            int tabInx = 0;
            int maxTabInx = ds.Tables.Count - 1;

            for (tabInx = 0; tabInx <= maxTabInx; tabInx++)
            {
                ProcessDataTable(ds.Tables[tabInx]);
            }
        }

        /// <summary>
        /// Returns data from a DataTable to the caller.
        /// </summary>
        /// <param name="tab">DataTable object containing data to be returned to the caller.</param>
        /// <remarks>Results can be retrieved by subscribing to the ResultDelegate event. </remarks>
        public void ProcessDataTable(DataTable tab)
        {
            ProcessDataTable(tab, (int)1);
        }

        /// <summary>
        /// Returns data from a DataTable to the caller.
        /// </summary>
        /// <param name="tab">DataTable object containing data to be returned to the caller.</param>
        /// <param name="tableNumber">Arbitrary number used for identifying multiple DataTables.</param>
        /// <remarks>Results can be retrieved by subscribing to the ResultDelegate Event. </remarks>
        private void ProcessDataTable(DataTable tab, int tableNumber)
        {
            PFDataProcessor dataProcessor = new PFDataProcessor();

            dataProcessor.returnResult += new PFDataProcessor.ResultDelegate(OutputResults);

            dataProcessor.ProcessDataTable(tab);

        }//end method


        /// <summary>
        /// Produces data in delimited text format.
        /// </summary>
        /// <param name="rdr">Data reader object.</param>
        /// <param name="columnSeparator">One or more characters to denote end of a column.</param>
        /// <param name="lineTerminator">One or more characters to denote end of a line. "\r\n" is the standard line terminator.</param>
        /// <param name="columnNamesOnFirstLine">If true delimited list of column names is output at top of the data.</param>
        /// <remarks>Results can be retrieved by subscribing to the ResultAsStringDelegate Event. </remarks>
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
        /// <param name="res">SqlCeResultSet object.</param>
        /// <param name="columnSeparator">One or more characters to denote end of a column.</param>
        /// <param name="lineTerminator">One or more characters to denote end of a line. "\r\n" is the standard line terminator.</param>
        /// <param name="columnNamesOnFirstLine">If true delimited list of column names is output at top of the data.</param>
        /// <remarks>Results can be retrieved by subscribing to the ResultAsStringDelegate Event. </remarks>
        public void ExtractDelimitedDataFromResultSet(SqlCeResultSet res,
                                               string columnSeparator,
                                               string lineTerminator,
                                               bool columnNamesOnFirstLine)
        {
            ExtractDelimitedDataFromTable(ConvertResultSetToDataTable(res), columnSeparator, lineTerminator, columnNamesOnFirstLine);
        }

        /// <summary>
        /// Produces data in delimited text format.
        /// </summary>
        /// <param name="ds">DataSet object.</param>
        /// <param name="columnSeparator">One or more characters to denote end of a column.</param>
        /// <param name="lineTerminator">One or more characters to denote end of a line. "\r\n" is the standard line terminator.</param>
        /// <param name="columnNamesOnFirstLine">If true delimited list of column names is output at top of the data.</param>
        /// <remarks>Results can be retrieved by subscribing to the ResultAsStringDelegate Event. </remarks>
        public void ExtractDelimitedDataFromDataSet(DataSet ds,
                                                    string columnSeparator,
                                                    string lineTerminator,
                                                    bool columnNamesOnFirstLine)
        {
            int tabInx = 0;
            int maxTabInx = ds.Tables.Count - 1;

            for (tabInx = 0; tabInx <= maxTabInx; tabInx++)
            {
                ExtractDelimitedDataFromTable(ds.Tables[tabInx], columnSeparator, lineTerminator, columnNamesOnFirstLine);
            }
        }

        /// <summary>
        /// Produces data in delimited text format.
        /// </summary>
        /// <param name="tab">DataTable object.</param>
        /// <param name="columnSeparator">One or more characters to denote end of a column.</param>
        /// <param name="lineTerminator">One or more characters to denote end of a line. "\r\n" is the standard line terminator.</param>
        /// <param name="columnNamesOnFirstLine">If true delimited list of column names is output at top of the data.</param>
        /// <remarks>Results can be retrieved by subscribing to the ResultAsStringDelegate Event. </remarks>
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
        /// <remarks>Results can be retrieved by subscribing to the ResultAsStringDelegate Event. </remarks>
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
        /// <param name="res">SqlCeResultSet object.</param>
        /// <param name="lineTerminator">If true, the output will be separated into lines using "\r\n" to terminate each line.</param>
        /// <param name="columnNamesOnFirstLine">If true, a list of column names in fixed length column format is output at top of the data.</param>
        /// <param name="allowDataTruncation">If true, data longer than column length will be truncated; otherwise an exception will be thrown.</param>
        /// <remarks>Results can be retrieved by subscribing to the ResultAsStringDelegate Event. </remarks>
        public void ExtractFixedLengthDataFromResultSet(SqlCeResultSet res,
                                                 bool lineTerminator,
                                                 bool columnNamesOnFirstLine,
                                                 bool allowDataTruncation)
        {
            ExtractFixedLengthDataFromTable(ConvertResultSetToDataTable(res), lineTerminator, columnNamesOnFirstLine, allowDataTruncation);
        }

        /// <summary>
        /// Produces data in fixed length text column format.
        /// </summary>
        /// <param name="ds">DataSet object.</param>
        /// <param name="lineTerminator">If true, the output will be separated into lines using "\r\n" to terminate each line.</param>
        /// <param name="columnNamesOnFirstLine">If true, a list of column names in fixed length column format is output at top of the data.</param>
        /// <param name="allowDataTruncation">If true, data longer than column length will be truncated; otherwise an exception will be thrown.</param>
        /// <remarks>Results can be retrieved by subscribing to the ResultAsStringDelegate Event. </remarks>
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
        /// <remarks>Results can be retrieved by subscribing to the ResultAsStringDelegate Event. </remarks>
        public void ExtractFixedLengthDataFromTable(DataTable tab,
                                          bool lineTerminator,
                                          bool columnNamesOnFirstLine,
                                          bool allowDataTruncation)
        {
            PFDataExporter dataExporter = new PFDataExporter();

            dataExporter.returnResultAsString += new PFDataExporter.ResultAsStringDelegate(OutputResultsAsString);

            dataExporter.ExtractFixedLengthDataFromTable(tab, lineTerminator, columnNamesOnFirstLine, allowDataTruncation);

        }



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
        /// Loads rows contained in an ADO.NET data table to an MSOracle database table. Table must already exist. See CreateTable methods to create a new table.
        /// </summary>
        /// <param name="dt">DataTable object containing data to load.</param>
        public void ImportDataFromDataTable(DataTable dt)
        {
            ImportDataFromDataTable(dt, 1);
        }

        /// <summary>
        /// Loads rows contained in an ADO.NET data table to an MSOracle database table. Table must already exist. See CreateTable methods to create a new table.
        /// </summary>
        /// <param name="dt">DataTable object containing data to load.</param>
        /// <param name="updateBatchSize">Number of individual SQL modification statements to include in a table modification operation. WARNING: This parameter is ignored and UpdateBatchSize is always set to 1 for this class.</param>
        public void ImportDataFromDataTable(DataTable dt, int updateBatchSize)
        {
            SqlCeCommand cmd = new SqlCeCommand(dt.TableName, _conn);
            cmd.CommandType = CommandType.TableDirect;
            cmd.CommandTimeout = _commandTimeout;
            DataTable dbTable = dt.Clone();
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);

            bool dataTypeFixNeeded = FixColumnDataTypes(dt, dbTable);

            da.SelectCommand = cmd;
            SqlCeCommandBuilder builder = new SqlCeCommandBuilder(da);
            da.InsertCommand = builder.GetInsertCommand();
            //da.FillSchema(dt, SchemaType.Source);
            //da.Fill(dbTable);

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


            if (dataTypeFixNeeded == false)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow inrow = dt.Rows[i];
                    DataRow outrow = dbTable.NewRow();
                    outrow.ItemArray = inrow.ItemArray;
                    dbTable.Rows.Add(outrow);
                }
            }
            else
            {
                FixColumnDataValues(dt, dbTable);
            }

            da.UpdateBatchSize = 1;
            da.Update(dbTable);
            dbTable.AcceptChanges();


        }

        private bool FixColumnDataTypes(DataTable dt, DataTable dbTable)
        {
            bool dataTypeFixNeeded = false;

            for (int c = 0; c < dt.Columns.Count; c++)
            {
                if (dt.Columns[c].DataType.FullName == "System.Char[]")
                {
                    dbTable.Columns[c].DefaultValue = null;
                    dbTable.Columns[c].DataType = System.Type.GetType("System.Byte[]");
                    dataTypeFixNeeded = true;
                }
                else
                {
                    ;
                }

            }

            return dataTypeFixNeeded;
        }

        private void FixColumnDataValues(DataTable dt, DataTable dbTable)
        {

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                DataRow dr = dbTable.NewRow();
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    if (dt.Columns[c].DataType.FullName == "System.Char[]")
                    {
                        //dbTable.Columns[c].DataType = System.Type.GetType("System.Byte[]");
                        Char[] chArray = (Char[])dt.Rows[r][c];
                        string str = new string(chArray);
                        byte[] bytes = new byte[str.Length * sizeof(char)];
                        System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
                        dr[c] = bytes;

                    }
                    else
                    {
                        dr[c] = dt.Rows[r][c];
                    }

                }
                dbTable.Rows.Add(dr);
            }


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
        /// Writes contents of ResultSet in Xml format to specified output file.
        /// </summary>
        /// <param name="res">ResultSet object.</param>
        /// <param name="filePath">Location of the Xml output file.</param>
        public void SaveResultSetToXmlFile(SqlCeResultSet res, string filePath)
        {
            DataTable dt = ConvertResultSetToDataTable(res);

            SaveDataTableToXmlFile(dt, filePath);
        }

        /// <summary>
        /// Writes contents of ResultSet plus the data schema in Xml format to specified output file.
        /// </summary>
        /// <param name="res">ResultSet object.</param>
        /// <param name="filePath">Location of the Xml output file.</param>
        public void SaveResultSetWithSchemaToXmlFile(SqlCeResultSet res, string filePath)
        {
            DataTable dt = ConvertResultSetToDataTable(res);

            SaveDataTableWithSchemaToXmlFile(dt, filePath);
        }

        /// <summary>
        /// Writes ResultSet in Xsd format to specified output file.
        /// </summary>
        /// <param name="res">ResultSet object.</param>
        /// <param name="filePath">Location of the Xsd output file.</param>
        public void SaveResultSetToXmlSchemaFile(SqlCeResultSet res, string filePath)
        {
            DataTable dt = ConvertResultSetToDataTable(res);

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
        /// Writes both the contents of DataTable and the associated data schema in Xml format to specified output file.
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
            //select * from information_schema.tables
            PFList<PFTableDef> tabdefList = new PFList<PFTableDef>();
            DataTable dt = null;
            string schemaQuery = "select * from information_schema.tables";
            string sqlSelect = "select * from <tableName> where 1=0";
            PFTableDefinitions tabdefObject = new PFTableDefinitions();
            PFSearchPattern[] regexInclude = null;
            PFSearchPattern[] regexExclude = null;

            regexInclude = tabdefObject.GetSearchPatternRegexObjects(includePatterns, "*");
            regexExclude = tabdefObject.GetSearchPatternRegexObjects(excludePatterns, string.Empty);

            dt = this.RunQueryDataTable(schemaQuery, "GetTablesSchema");

            foreach (DataRow dr in dt.Rows)
            {
                if (this.TypeIsUserTable(dr))
                {
                    string tabName = this.GetFullTableName(dr);
                    if (tabdefObject.IsMatchToPattern(regexInclude, tabName) && tabdefObject.IsMatchToPattern(regexExclude, tabName)==false)
                    {
                        string sql = sqlSelect.Replace("<tableName>", tabName);
                        dt = this.RunQueryDataTable(sql,"GetTabSchema");
                        dt.TableName = tabName;
                        string tabCreateStatement = this.BuildTableCreateStatement(dt);
                        PFTableDef tabDef = new PFTableDef();
                        tabDef.DbPlatform = this.DbPlatform;
                        tabDef.DbConnectionString = this.ConnectionString;
                        tabDef.TableCreateStatement = tabCreateStatement;
                        tabDef.TableObject = dt;
                        tabDef.TableFullName = tabName;
                        TableNameQualifiers tnq = this.GetTableNameQualifiers(dr);
                        tabDef.TableCatalog = tnq.TableCatalog;
                        tabDef.TableOwner = tnq.TableSchema;
                        tabDef.TableName = tnq.TableName;
                        tabdefList.Add(tabDef);
                    }
                }
            }



             return tabdefList;
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
            PFTableDefinitions tabdefs = new PFTableDefinitions();
            return tabdefs.CreateTablesFromTableDefs(this, tableDefs, dropBeforeCreate);
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
            PFTableDefinitions tabdefs = new PFTableDefinitions();
            return tabdefs.CopyTableDataFromTableDefs(sourceDatabase, tableIncludePatterns, tableExcludePatterns, this, newSchemaName, dropBeforeCreate);
        }




    }//end class
}//end namespace
