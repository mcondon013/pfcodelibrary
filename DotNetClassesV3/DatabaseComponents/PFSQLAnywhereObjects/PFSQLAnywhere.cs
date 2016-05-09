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
using System.IO;
using iAnywhere.Data.SQLAnywhere;
using System.Data.Common;
using PFDataAccessObjects;
using PFCollectionsObjects;
using PFListObjects;

namespace PFSQLAnywhereObjects
{
    /// <summary>
    /// Class to manage .NET access to SQL Anywhere databases.
    /// Requires SQLAnywhere client to be installed on the machine using this class. (e.g. iAnywhere.Data.SQLAnywhere.v4.0)
    /// </summary>
    public class PFSQLAnywhere : IDatabaseProvider, IDesktopDatabaseProvider  
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private DatabasePlatform _dbPlatform = DatabasePlatform.SQLAnywhere;

        private SAConnection _conn = new SAConnection();
        private SACommand _cmd = new SACommand();
        private System.Data.CommandType _commandType = CommandType.Text;
        private int _commandTimeout = 300;
        private string _sqlQuery = string.Empty;

        private string _dataSourceName = string.Empty;
        private string _databaseFile = string.Empty;
        private string _serverName = string.Empty;
        private string _databaseName = string.Empty;
        private string _databaseKey = string.Empty;
        private string _encryption = string.Empty;
        private string _encryptedPassword = string.Empty;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _connectionString = string.Empty;
        private PFCollectionsObjects.PFKeyValueList<string, string> _connectionStringKeyVals = new PFCollectionsObjects.PFKeyValueList<string, string>();

#pragma warning disable 1591
        public delegate void ResultDelegate(DataColumnCollection columns, DataRow data, int tabNumber);
        public event ResultDelegate returnResult;
        public delegate void ResultAsStringDelegate(string outputLine, int tabNumber);
        public event ResultAsStringDelegate returnResultAsString;
#pragma warning restore 1591


        //constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        public PFSQLAnywhere()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="databaseFileName">Name of the SQL Anywhere database files that will be used.</param>
        public PFSQLAnywhere(string databaseFileName)
        {
            this.ServerName = databaseFileName;
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
                _commandTimeout = value;
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
        /// Data source value for connection string. Leave blank if Databasefile specified.
        /// </summary>
        public string DataSourceName
        {
            get
            {
                return _dataSourceName;
            }
            set
            {
                _dataSourceName = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// Path to the file containing the data.
        /// </summary>
        public string DatabasePath
        {
            get
            {
                return _databaseFile;
            }
            set
            {
                _databaseFile = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// Name of the server containing the data.
        /// </summary>
        public string ServerName
        {
            get
            {
                return _serverName;
            }
            set
            {
                _serverName = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// Name of the database containing the data.
        /// </summary>
        public string DatabaseName
        {
            get
            {
                return _databaseName;
            }
            set
            {
                _databaseName = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// Key used to access database.
        /// </summary>
        public string DatabaseKey
        {
            get
            {
                return _databaseKey;
            }
            set
            {
                _databaseKey = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// Type of encryption used for the database.
        /// </summary>
        public string Encryption
        {
            get
            {
                return _encryption;
            }
            set
            {
                _encryption = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// Encrypted password used for the database.
        /// </summary>
        public string EncryptedPassword
        {
            get
            {
                return _encryptedPassword;
            }
            set
            {
                _encryptedPassword = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// Name of user.
        /// </summary>
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// Password for user.
        /// </summary>
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// Connection string to be used for this instance. Is built automatically if server, database, etc. are specified separately via this instance's properties. If a complete connection string is passed into this property, then it is parsed and the various components are stored in this instance's various properties (server, database, etc.).
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


        //methods

        /// <summary>
        /// Creates a new SQL Anywhere .db database file by copying a template .db file.
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
        /// Creates a SQL Anywhere .db database file.
        /// </summary>
        /// <param name="databasePath">Full path to database file to be created.</param>
        /// <returns>True if database is created. Otherwise false.</returns>
        /// <remarks>Database is created without encryption.</remarks>
        /// <remarks>You must have an open connection to an existing SQLAnywhere database in order to create the new database.</remarks>
        public bool CreateDatabase(string databasePath)
        {
            return CreateDatabase(databasePath, false, string.Empty);
        }

        /// <summary>
        /// Creates a SQL Anywhere .db database file.
        /// </summary>
        /// <param name="databasePath">Full path to database file to be created.</param>
        /// <param name="enableEncryption">If true, encryption will be enabled on the new database.</param>
        /// <param name="encryptionKey">Encryption key for the database.</param>
        /// <returns>True if database is created. Otherwise false.</returns>
        /// <remarks>You must have an open connection to an existing SQLAnywhere database in order to create the new database.</remarks>
        public bool CreateDatabase(string databasePath, bool enableEncryption, string encryptionKey)
        {
            bool dbCreated = false;
            string sqlCreateDatabaseTemplate = @"CREATE DATABASE '<DATABASEFILEPATH>' ENCRYPTED ON KEY '<ENCRYPTIONKEY>';";
            string sqlStatement = string.Empty;

            if (this.IsConnected == false)
            {
                _msg.Length = 0;
                _msg.Append("You must have an existing open connection to a SQL Anywhere database in order to create a new database.");
                throw new System.Exception(_msg.ToString());
            }

            sqlStatement = sqlCreateDatabaseTemplate.Replace("<DATABASEFILEPATH>", databasePath);
            if (enableEncryption && String.IsNullOrEmpty(encryptionKey) == false)
                sqlStatement = sqlStatement.Replace("<ENCRYPTIONKEY>", encryptionKey);
            else if (enableEncryption && String.IsNullOrEmpty(encryptionKey))
                sqlStatement = sqlStatement.Replace("ENCRYPTED ON KEY '<ENCRYPTIONKEY>'", "ENCRYPTED ON");
            else
                sqlStatement = sqlStatement.Replace(" ENCRYPTED ON KEY '<ENCRYPTIONKEY>'", string.Empty);

            sqlStatement = sqlStatement.Replace(@"\", @"\\");

            this.RunNonQuery(sqlStatement, CommandType.Text);

            dbCreated = true;


            return dbCreated;
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
                rowsAffected = this.RunNonQuery(sqlScript, CommandType.Text);
                bSuccess = true;
            }
            catch (SAException cex)
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

            if (dr["TABLE_TYPE"].ToString().ToUpper() == "BASE")
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
            return dr["TABLE_SCHEMA"].ToString() + "." + dr["TABLE_NAME"].ToString();
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
            tnq.TableSchema = dr["TABLE_SCHEMA"].ToString();
            tnq.TableName = dr["TABLE_NAME"].ToString();


            return tnq;
        }

        /// <summary>
        /// Routine to recreate with a different schema name. Used when transferring a table definition to a new database.
        /// </summary>
        /// <param name="tabDef">Object containing the table definition to be reformatted.</param>
        /// <param name="newSchemaName">Schema name to apply to the reformatted table name.</param>
        /// <returns>Table name that includes the new schema name.</returns>
        public string RebuildFullTableName(PFTableDef tabDef, string newSchemaName)
        {
            string tabName = string.Empty;
            string schemaName = string.Empty;

            schemaName = String.IsNullOrEmpty(newSchemaName) == false ? newSchemaName : tabDef.TableOwner;
            tabName = schemaName + '.' + tabDef.TableName;


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
            bool res = false;
            string[] tableNameParts = tableName.Split('.');
            if (tableNameParts.Length == 2)
            {
                res = TableExists(tableNameParts[0], tableNameParts[1]);
            }
            else
            {
                res = TableExists(string.Empty, tableName);
            }
            return res;
        }

        /// <summary>
        /// Method to determine if a table exists in the database.
        /// </summary>
        /// <param name="schemaName">Name of the schema to which the table belongs.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table exists; otherwise false.</returns>
        public bool TableExists(string schemaName, string tableName)
        {
            bool ret = false;
            string schema = String.IsNullOrEmpty(schemaName) ? string.Empty : schemaName;
            string sql = string.Empty;
            string tabName = String.IsNullOrEmpty(tableName) ? string.Empty : tableName;
            DbDataReader rdr = null;
            int numRows = -1;

            if (this.IsConnected == false)
                this.OpenConnection();

            _str.Length = 0;
            _str.Append("select count(*) as NumRows");
            _str.Append("  from sys.systable");
            _str.Append(" where user_name(creator) = '");
            _str.Append(schema);
            _str.Append("'");
            _str.Append(" and table_name = '");
            _str.Append(tabName);
            _str.Append("'");

            sql = _str.ToString();
            rdr = RunQueryDataReader(sql.ToString(), System.Data.CommandType.Text);

            if (rdr.HasRows)
            {
                rdr.Read();
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
        /// <param name="catalogName">Name of database. This parameter is ignored and only schema and table name is used for the existence check.</param>
        /// <param name="schemaName">Name of the schema to which the table belongs.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table exists; otherwise false.</returns>
        public bool TableExists(string catalogName, string schemaName, string tableName)
        {
            return TableExists(schemaName, tableName);
        }


        /// <summary>
        /// Method to drop a table from the database.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table no longer exists; otherwise returns false if table still exists.</returns>
        /// <remarks>There could be unexpected results from this method because schema name is not specified.</remarks>
        public bool DropTable(string tableName)
        {
            //return DropTable(string.Empty, tableName);
            bool res = false;
            string[] tableNameParts = tableName.Split('.');
            if (tableNameParts.Length == 2)
            {
                res = DropTable(tableNameParts[0], tableNameParts[1]);
            }
            else
            {
                res = DropTable(string.Empty, tableName);
            }
            return res;
        }


        /// <summary>
        /// Method to drop a table from the database.
        /// </summary>
        /// <param name="schemaName">Name of the schema to which the table belongs.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table no longer exists; otherwise returns false if table still exists.</returns>
        public bool DropTable(string schemaName, string tableName)
        {
            bool ret = false;
            StringBuilder sql = new StringBuilder();

            if (TableExists(schemaName, tableName))
            {
                sql.Length = 0;
                sql.Append("drop table ");
                if (String.IsNullOrEmpty(schemaName) == false)
                {
                    sql.Append(schemaName);
                    sql.Append(".");
                }
                sql.Append(tableName);
                sql.Append("");

                RunNonQuery(sql.ToString(), System.Data.CommandType.Text);

                if (TableExists(schemaName, tableName))
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
        /// <param name="catalogName">Catalog name is ignored by this version of method.</param>
        /// <param name="schemaName">Schema or owner name for the table.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table no longer exists; otherwise returns false if table still exists.</returns>
        public bool DropTable(string catalogName, string schemaName, string tableName)
        {
            return DropTable(schemaName, tableName);
        }



        /// <summary>
        /// Opens connection to database.
        /// </summary>
        public void OpenConnection()
        {
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
            _cmd.Connection = _conn;
            _cmd.CommandType = pCommandType;
            _cmd.CommandTimeout = _commandTimeout;
            _cmd.CommandText = sqlQuery;
            _commandType = pCommandType;
            _sqlQuery = sqlQuery;
            SADataReader rdr = _cmd.ExecuteReader();
            return rdr;
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
            SADataAdapter da = new SADataAdapter();
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
            SADataAdapter da = new SADataAdapter();
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
            SACommand cmd = new SACommand(sqlText, _conn);
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
            SADataAdapter da = new SADataAdapter();
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




        //private methods
        private void ParseConnectionString()
        {
            SAConnectionStringBuilder saConnBuilder;
            if (_connectionString.Trim().Length > 0)
            {
                saConnBuilder = new SAConnectionStringBuilder(_connectionString.Trim());
                _dataSourceName = saConnBuilder.DataSourceName.Trim();
                _databaseFile = saConnBuilder.DatabaseFile.Trim();
                _databaseName = saConnBuilder.DatabaseName.Trim();
                _serverName = saConnBuilder.ServerName.Trim();
                _username = saConnBuilder.UserID.Trim();
                _password = saConnBuilder.Password;
                _encryption = saConnBuilder.Encryption;
                _encryptedPassword = saConnBuilder.EncryptedPassword;
                _databaseKey = saConnBuilder.DatabaseKey;
            }
            else
            {
                _dataSourceName = string.Empty;
                _databaseFile = string.Empty;
                _serverName = string.Empty;
                _databaseName = string.Empty;
                _databaseKey = string.Empty;
                _encryption = string.Empty;
                _encryptedPassword = string.Empty;
                _username = string.Empty;
                _password = string.Empty;
            }
        }

        private void BuildConnectionString()
        {
            SAConnectionStringBuilder sqlConnBuilder = new SAConnectionStringBuilder();

            if (this.DataSourceName.Trim().Length > 0)
                sqlConnBuilder.DataSourceName = this.DataSourceName.Trim();
            if (this.DatabasePath.Trim().Length > 0)
                sqlConnBuilder.DatabaseFile = this.DatabasePath.Trim();
            if (this.ServerName.Trim().Length > 0)
                sqlConnBuilder.ServerName = this.ServerName.Trim();
            if (this.DatabaseName.Trim().Length > 0)
                sqlConnBuilder.DatabaseName = this.DatabaseName.Trim();


            if (this.Username.Trim().Length > 0)
                sqlConnBuilder.UserID = this.Username.Trim();
            if (this.Password.Length > 0)
                sqlConnBuilder.Password = this.Password;


            if (this.DatabaseKey.Trim().Length > 0)
                sqlConnBuilder.DatabaseKey = this.DatabaseKey;
            if (this.Encryption.Trim().Length > 0)
                sqlConnBuilder.Encryption = this.Encryption;
            if (this.EncryptedPassword.Trim().Length > 0)
                sqlConnBuilder.EncryptedPassword = this.EncryptedPassword;


            _connectionString = sqlConnBuilder.ToString();
        }

        /// <summary>
        /// Returns a list of key/value pairs that contains all the keys and their associated values for the current connection string.
        /// </summary>
        /// <returns>List in Key/Value format.</returns>
        private PFCollectionsObjects.PFKeyValueList<string, string> GetConnectionStringKeyVals()
        {
            SAConnectionStringBuilder sqlConnBuilder = new SAConnectionStringBuilder(this.ConnectionString);
            _connectionStringKeyVals.Clear();

            foreach (string key in sqlConnBuilder.Keys)
            {
                _connectionStringKeyVals.Add(new stKeyValuePair<string, string>(key, sqlConnBuilder[key].ToString()));
            }
            return this._connectionStringKeyVals;
        }



        /// <summary>
        /// Transforms a SqlDataReader object into a DataTable object.
        /// </summary>
        /// <param name="rdr">SqlDataReader object.</param>
        /// <returns>DataTable object.</returns>
        public DataTable ConvertDataReaderToDataTable(DbDataReader rdr)
        {
            return ConvertDataReaderToDataTable(rdr, "Table");
        }

        /// <summary>
        /// Transforms a SqlDataReader object into a DataTable object.
        /// </summary>
        /// <param name="rdr">SqlDataReader object.</param>
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
        /// Returns data from a SqlDataReader object to the caller.
        /// </summary>
        /// <param name="rdr">SqlDataReader object containing data to be returned to the caller.</param>
        /// <remarks>Results can be retrieved by subscribing to the ResultDelegate event. </remarks>
        public void ProcessDataReader(DbDataReader rdr)
        {
            ProcessDataTable(ConvertDataReaderToDataTable(rdr), (int)1);
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
                ProcessDataTable(ds.Tables[tabInx], tabInx);
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
        /// <param name="updateBatchSize">Number of individual SQL modification statements to include in a table modification operation.</param>
        public void ImportDataFromDataTable(DataTable dt, int updateBatchSize)
        {
            StringBuilder sql = new StringBuilder();
            sql.Length = 0;
            sql.Append("select * from ");
            sql.Append(dt.TableName);
            sql.Append(";");
            SACommand cmd = new SACommand(sql.ToString(), _conn);
            cmd.CommandTimeout = _commandTimeout;
            cmd.CommandType = CommandType.Text;
            DataTable dbTable = dt.Clone();
            SADataAdapter da = new SADataAdapter(cmd);

            //No column fixes needed for SQL Anywhere .NET Provider
            //bool dataTypeFixNeeded = FixColumnDataTypes(dt, dbTable);


            da.SelectCommand = cmd;
            SACommandBuilder builder = new SACommandBuilder(da);
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


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow inrow = dt.Rows[i];
                DataRow outrow = dbTable.NewRow();
                outrow.ItemArray = inrow.ItemArray;
                dbTable.Rows.Add(outrow);
            }

            //No data type fixes needed for SQL Anywhere .NET provider
            /*
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
            */

            da.UpdateBatchSize = updateBatchSize;
            da.Update(dbTable);
            dbTable.AcceptChanges();

            da = null;

        }

        //No data type fixes needed for SQL Anywhere .NET provider
        /*
        private bool FixColumnDataTypes(DataTable dt, DataTable dbTable)
        {
            bool dataTypeFixNeeded = false;

            for (int c = 0; c < dt.Columns.Count; c++)
            {
                if (dt.Columns[c].DataType.FullName == "System.Guid")
                {
                    dbTable.Columns[c].DataType = System.Type.GetType("System.String");
                    dbTable.Columns[c].MaxLength = 36;
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
                    if (dt.Columns[c].DataType.FullName == "System.Guid")
                    {
                        //dbTable.Columns[c].DataType = System.Type.GetType("System.String");
                        Guid g = (Guid)dt.Rows[r][c];
                        dr[c] = g.ToString();
                    }
                    else
                    {
                        dr[c] = dt.Rows[r][c];
                    }

                }
                dbTable.Rows.Add(dr);
            }


        }

        */



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
                string outputSchemaName = String.IsNullOrEmpty(newSchemaName) ? td.TableOwner : newSchemaName;
                newTableName = td.TableName;
                tdToXml = td.ToXmlString();
                newTd = PFTableDef.LoadFromXmlString(tdToXml);

                if (outputSchemaName.Trim().Length > 0)
                    newTd.TableObject.TableName = outputSchemaName + "." + newTableName;
                else
                    newTd.TableObject.TableName = newTableName;
                newTd.TableFullName = newTd.TableObject.TableName;
                newTd.TableOwner = outputSchemaName;
                newTd.TableName = newTableName;

                if (this.TableExists(outputSchemaName, newTableName) && dropBeforeCreate)
                {
                    this.DropTable(outputSchemaName, newTableName);
                }

                if (this.TableExists(outputSchemaName, newTableName) == false)
                {
                    sqlStatement = this.BuildTableCreateStatement(newTd.TableObject);
                    this.RunNonQuery(sqlStatement, CommandType.Text);
                }

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
            }//end while

            return tableCopyLog;



        }




    }//end class
}//end namespace
