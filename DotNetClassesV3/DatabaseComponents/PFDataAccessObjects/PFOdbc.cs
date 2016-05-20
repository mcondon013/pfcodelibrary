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
using PFDataAccessObjects;
using PFTextObjects;
using System.Data.Odbc;
using PFCollectionsObjects;
using PFListObjects;

namespace PFDataAccessObjects
{
    /// <summary>
    /// Class contains functionality for accessing databases using System.Data.Odbc namespace.
    /// </summary>
    public class PFOdbc : IDatabaseProvider 
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private DatabasePlatform dbPlatform = DatabasePlatform.ODBC;  //changed from DatabasePlatform.ODBC to Unknown 5/28/13 (MC)  //changed back to DatabasePlatform.ODBC 7/6/13 (MC)

        //private variables for properties
        private OdbcConnection _conn = new OdbcConnection();
        private OdbcCommand _cmd = new OdbcCommand();
        private System.Data.CommandType _commandType = CommandType.Text;
        private int _commandTimeout = 300;
        private string _sqlQuery = string.Empty;
        private string _connectionString = string.Empty;
        private string _driver = string.Empty;
        private string _dsn = string.Empty;
        private PFKeyValueList<string, string> _connectionStringKeyVals = new PFKeyValueList<string, string>();
        private bool _isConnected = false;
        

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
        public PFOdbc()
        {
            ;
        }

        //properties

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
        /// Connection string to be used for this instance.
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
                this.DbPlatform = GetDatabasePlatform();
            }
        }

        /// <summary>
        /// Name of ODBC driver to use for this connection.
        /// </summary>
        public string Driver
        {
            get
            {
                return _driver;
            }
            set
            {
                _driver = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// Name of DSN containing connection properties. Usually defined via the Windows system ODBC Administrator program. DataSources (ODBC) or odbcadm32.exe applications.
        /// </summary>
        public string Dsn
        {
            get
            {
                return _dsn;
            }
            set
            {
                _dsn = value;
                BuildConnectionString();
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

        /// <summary>
        /// Returns true if current connection state is Open.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                _isConnected = false;
                if (_conn != null)
                    if (_conn.State == ConnectionState.Open)
                        _isConnected = true;
                return _isConnected;
            }
        }

        /// <summary>
        /// Returns the current connection state of this instance.
        /// </summary>
        public string CurrentConnectionState
        {
            get
            {
                ConnectionState cs = ConnectionState.Closed;
                if (_conn != null)
                    cs = _conn.State;
                return cs.ToString();
            }
        }

        /// <summary>
        /// Specifies the underlying database platform supported by the ODBC driver (if known)
        /// </summary>
        public DatabasePlatform DbPlatform
        {
            get
            {
                return dbPlatform;
            }
            set
            {
                dbPlatform = value;
            }
        }

        //methods

        private void ParseConnectionString()
        {
            OdbcConnectionStringBuilder odbcConnBuilder;
            odbcConnBuilder = new OdbcConnectionStringBuilder(_connectionString.Trim());
            _driver = odbcConnBuilder.Driver;
            _dsn = odbcConnBuilder.Dsn;
            GetConnectionStringKeyVals();
        }

        private void BuildConnectionString()
        {
            OdbcConnectionStringBuilder odbcConnBuilder = new OdbcConnectionStringBuilder();

            if (_dsn != string.Empty)
            {
                odbcConnBuilder.Dsn = _dsn;
            }
            if (_driver != string.Empty)
            {
                odbcConnBuilder.Driver = _driver;
            }
            _connectionString = odbcConnBuilder.ToString();
        }

        /// <summary>
        /// Returns a list of key/value pairs that contains all the keys and their associated values for the current connection string.
        /// </summary>
        /// <returns>List in Key/Value format.</returns>
        private PFKeyValueList<string, string> GetConnectionStringKeyVals()
        {
            OdbcConnectionStringBuilder odbcConnBuilder = new OdbcConnectionStringBuilder(this.ConnectionString);
            _connectionStringKeyVals.Clear();

            foreach (string key in odbcConnBuilder.Keys)
            {
                _connectionStringKeyVals.Add(new stKeyValuePair<string,string>(key, odbcConnBuilder[key].ToString()));
            }
            return this._connectionStringKeyVals;
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
            DatabasePlatform dbplat = this.DbPlatform;

            try
            {
                dbplat = GetDatabasePlatform();

                if (dbplat == DatabasePlatform.ODBC)
                {
                    bSuccess = CreateTableUsingOdbcSyntax(dt, out createScript);
                }
                else
                {
                    PFTableBuilder tb = null;
                    tb = new PFTableBuilder(dbplat);
                    if (dbplat == DatabasePlatform.MSAccess)
                    {
                        this.CloseConnection();
                        createScript = "*ALERT: ADODB/ADOX routines used to create an Access table.";
                        OdbcConnectionStringBuilder cnb = new OdbcConnectionStringBuilder(this.ConnectionString);
                        string fileName = cnb["Dbq"].ToString();
                        PFMsAccess acc = new PFMsAccess(fileName);
                        acc.CreateTable(dt);
                        acc = null;
                        this.OpenConnection();
                        bSuccess = true;
                    }
                    else
                    {
                        sqlScript = tb.BuildTableCreateStatement(dt, this.ConnectionString);
                        createScript = sqlScript;
                        if (this._conn.State != ConnectionState.Open)
                            this.OpenConnection();
                        rowsAffected = this.RunNonQuery(sqlScript, CommandType.Text);
                        bSuccess = true;
                    }

                }

            }
            catch (OdbcException cex)
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
        /// Method creates a a table in the database based on the column definitions and table name contained in the DataTable parameter.
        /// </summary>
        /// <param name="dt">DataTable object.</param>
        /// <param name="createScript">Copy of the script used to create the table.</param>
        /// <returns>True if table created; otherwise false.</returns>
        /// <remarks>This method is used for testing only. Use the CreateTable method for application work.</remarks>
        public bool CreateTableUsingOdbcSyntax(DataTable dt, out string createScript)
        {
            bool bSuccess = true;
            string sqlScript = string.Empty;
            int rowsAffected = 0;
            DatabasePlatform dbplat = DatabasePlatform.ODBC;

            try
            {
                if (this.ConnectionString.Contains("Driver={Microsoft Access Driver"))
                {
                    dbplat = DatabasePlatform.MSAccess;
                }

                PFTableBuilder tb = null;
                tb = new PFTableBuilder(dbplat);

                sqlScript = tb.BuildTableCreateStatement(dt, this.ConnectionString);
                createScript = sqlScript;
                if (this._conn.State != ConnectionState.Open)
                    this.OpenConnection();
                rowsAffected = this.RunNonQuery(sqlScript, CommandType.Text);
                bSuccess = true;


            }
            catch (OdbcException cex)
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
        /// Method creates a table in the database using a custom SQL create table script built by the caller.
        /// </summary>
        /// <param name="createScript">Copy of the script used to create the table.</param>
        /// <returns>True if table created; otherwise false.</returns>
        /// <remarks>This method runs an ODBC ExecuteNonQuery command.</remarks>
        public bool CreateTableUsingCustomScript(string createScript)
        {
            bool bSuccess = true;
            string sqlScript = string.Empty;
            int rowsAffected = 0;

            try
            {

                sqlScript = createScript;
                if (this._conn.State != ConnectionState.Open)
                    this.OpenConnection();
                rowsAffected = this.RunNonQuery(sqlScript, CommandType.Text);
                bSuccess = true;


            }
            catch (OdbcException cex)
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
            string sqlScript = string.Empty;
            DatabasePlatform dbplat = this.DbPlatform;

            dbplat = GetDatabasePlatform();
            PFTableBuilder tb = null;
            tb = new PFTableBuilder(dbplat);
            sqlScript = tb.BuildTableCreateStatement(dt, this.ConnectionString);

            return sqlScript;

        }

        /// <summary>
        /// Routine to determine what database platform is supported by the current ODBC driver.
        /// </summary>
        /// <returns>Type of DatabasePlatform.</returns>
        public DatabasePlatform GetDatabasePlatform()
        {
            DatabasePlatform dbplat = this.DbPlatform;

            //see if database platform can be guessed at from the connection string
            if (this.ConnectionString.ToLower().Contains("driver={microsoft access driver"))
            {
                dbplat = DatabasePlatform.MSAccess;
            }
            else if (this.ConnectionString.ToLower().Contains("driver={oracle"))
            {
                dbplat = DatabasePlatform.OracleNative;
            }
            else if (this.ConnectionString.ToLower().Contains("driver={microsoft odbc for oracle"))
            {
                dbplat = DatabasePlatform.MSOracle;
            }
            else if (this.ConnectionString.ToLower().Contains("driver={sql server")
                     || this.ConnectionString.ToLower().Contains("sql server"))
            {
                dbplat = DatabasePlatform.MSSQLServer;
            }
            else if (this.ConnectionString.ToLower().Contains("driver={ibm db2")
                    || this.ConnectionString.ToLower().Contains("ibm db2"))
            {
                dbplat = DatabasePlatform.DB2;
            }
            else if (this.ConnectionString.ToLower().Contains("driver={mysql"))
            {
                dbplat = DatabasePlatform.MySQL;
            }
            else if (this.ConnectionString.ToLower().Contains("driver={adaptive server")
                     || this.ConnectionString.ToLower().Contains("driver={sybase ase"))
            {
                dbplat = DatabasePlatform.Sybase;
            }
            else if (this.ConnectionString.ToLower().Contains("driver={sql anywhere"))
            {
                dbplat = DatabasePlatform.SQLAnywhere;
            }
            else
            {
                dbplat = DatabasePlatform.ODBC;
            }

            return dbplat;
        
        }//end method


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
            bool colNameError = false;
            string tabName = string.Empty;
            try
            {
                if(dr["TABLE_SCHEM"].ToString().Trim().Length > 0)
                    tabName = dr["TABLE_SCHEM"].ToString() + "." + dr["TABLE_NAME"].ToString();
                else
                    tabName = dr["TABLE_NAME"].ToString();
            }
            catch 
            {
                colNameError = true;
            }

            if (colNameError)
            {
                if (dr[1].ToString().Trim().Length > 0)
                    tabName = dr[1].ToString() + "." + dr[2].ToString();
                else
                    tabName = dr[2].ToString();
            }
            
            return tabName;
        }

        /// <summary>
        /// Function to return the catalog, schema and name parts of a fully qualified table name. Some databases will only return the schema and name since catalog is not used by those database engines.
        /// </summary>
        /// <param name="dr">DataRow returned by GetSchema that contains Tables information.</param>
        /// <returns>Object containing the different qualifiers in the table name.</returns>
        public TableNameQualifiers GetTableNameQualifiers(DataRow dr)
        {
            bool colNameError = false;
            TableNameQualifiers tnq = new TableNameQualifiers();

            try
            {
                tnq.TableCatalog = dr["TABLE_CAT"].ToString();
                tnq.TableSchema = dr["TABLE_SCHEM"].ToString();
                tnq.TableName = dr["TABLE_NAME"].ToString();
            }
            catch
            {
                colNameError = true;
            }

            if (colNameError)
            {
                tnq.TableCatalog = dr[0].ToString();
                tnq.TableSchema = dr[1].ToString();
                tnq.TableName = dr[2].ToString();
            }


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
            //return TableExists(string.Empty, string.Empty, tableName);
            bool res = false;
            string[] tableNameParts = tableName.Split('.');
            if (tableNameParts.Length == 2)
            {
                res = TableExists(string.Empty, tableNameParts[0], tableNameParts[1]);
            }
            else if (tableNameParts.Length == 3)
            {
                res = TableExists(tableNameParts[0], tableNameParts[1], tableNameParts[2]);
            }
            else
            {
                res = TableExists(string.Empty, string.Empty, tableName);
            }
            return res;
        }

        /// <summary>
        /// Method to determine if a table exists in the database.
        /// </summary>
        /// <param name="schemaName">Name of the schema, if there is one. If not, set to empty string.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table exists; otherwise false.</returns>
        public bool TableExists(string schemaName, string tableName)
        {
            return TableExists(string.Empty, schemaName, tableName);
        }

        /// <summary>
        /// Method to determine if a table exists in the database.
        /// </summary>
        /// <param name="catalogName">Name of the database, if necessary for lookup. Otherwise, set to string.empty.</param>
        /// <param name="schemaName">Name of the schema, if there is one. If not, set to empty string.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table exists; otherwise false.</returns>
        public bool TableExists(string catalogName, string schemaName, string tableName)
        {
            bool ret = false;
            string cat = String.IsNullOrEmpty(catalogName) ? string.Empty : catalogName;
            string schema = String.IsNullOrEmpty(schemaName) ? string.Empty : schemaName;
            string tab = String.IsNullOrEmpty(tableName) ? string.Empty : tableName;

            string catColName = "TABLE_CAT";
            string schemaColName = "TABLE_SCHEM";
            string tabColName = "TABLE_NAME";
            string temp = string.Empty;

            if (this.IsConnected == false)
                this.OpenConnection();

            DataTable dt = _conn.GetSchema("Tables");

            for (int c = 0; c < dt.Columns.Count; c++)
            {
                DataColumn dc = dt.Columns[c];
                if (c == 0)
                    catColName = dc.ColumnName;
                else if (c == 1)
                    schemaColName = dc.ColumnName;
                else if (c == 2)
                    tabColName = dc.ColumnName;
                else
                   temp=string.Empty;
            }

            foreach (DataRow dr in dt.Rows)
            {
                if ((dr[tabColName].ToString().ToLower() == tableName.ToLower())
                    && (schema == string.Empty || dr[schemaColName].ToString().ToLower() == schemaName.ToLower())
                    && (cat == string.Empty || dr[catColName].ToString().ToLower() == catalogName.ToLower()))
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }

        /// <summary>
        /// Method to drop a table from the database.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table no longer exists; otherwise returns false if table still exists.</returns>
        public bool DropTable(string tableName)
        {
            //return DropTable(string.Empty, string.Empty, tableName);
            bool res = false;
            string[] tableNameParts = tableName.Split('.');
            if (tableNameParts.Length == 2)
            {
                res = DropTable(string.Empty, tableNameParts[0], tableNameParts[1]);
            }
            else if (tableNameParts.Length == 3)
            {
                res = DropTable(tableNameParts[0], tableNameParts[1], tableNameParts[2]);
            }
            else
            {
                res = DropTable(string.Empty, string.Empty, tableName);
            }
            return res;
        }

        /// <summary>
        /// Method to drop a table from the database.
        /// </summary>
        /// <param name="schemaName">Name of the schema, if there is one for the table to delete. If not, set to empty string.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table no longer exists; otherwise returns false if table still exists.</returns>
        public bool DropTable(string schemaName, string tableName)
        {
            return DropTable(string.Empty, schemaName, tableName);
        }

        /// <summary>
        /// Method to drop a table from the database.
        /// </summary>
        /// <param name="catalogName">Name of the database, if necessary to identify table to delete. Otherwise, set to string.empty.</param>
        /// <param name="schemaName">Name of the schema, if there is one for the table to delete. If not, set to empty string.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table no longer exists; otherwise returns false if table still exists.</returns>
        public bool DropTable(string catalogName, string schemaName, string tableName)
        {
            bool ret = false;
            StringBuilder sql = new StringBuilder();
            string cat = String.IsNullOrEmpty(catalogName) ? string.Empty : catalogName;
            string schema = String.IsNullOrEmpty(schemaName) ? string.Empty : schemaName;
            string tab = String.IsNullOrEmpty(tableName) ? string.Empty : tableName;


            try
            {
                if (TableExists(catalogName, schemaName, tableName))
                {
                    sql.Length = 0;
                    sql.Append("drop table ");
                    if (cat != string.Empty)
                    {
                        sql.Append(cat);
                        sql.Append(".");
                    }
                    if (schema != string.Empty)
                    {
                        sql.Append(schema);
                        sql.Append(".");
                    }
                    sql.Append(tab);

                    RunNonQuery(sql.ToString(), CommandType.Text);

                    if (TableExists(catalogName, schemaName, tableName))
                        ret = false;
                    else
                        ret = true;
                }
                else
                {
                    ret = true;
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Drop table operation invalid or syntax incorrect for this ODBC driver: Error Message: ");
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new DataException(_msg.ToString());
            }
            finally
            {
                ;
            }
                 
        

            return ret;
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
            OdbcDataReader rdr = _cmd.ExecuteReader();
            return rdr;
        }

        /// <summary>
        /// Runs query.
        /// </summary>
        /// <returns>Returns dataset object.</returns>
        /// <remarks>This implementation limits result to one table in the dataset that is returned.</remarks>
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
        /// <remarks>This implementation limits result to one table in the dataset that is returned.</remarks>
        public DataSet RunQueryDataSet(string sqlQuery, CommandType pCommandType)
        {
            OdbcDataAdapter da = new OdbcDataAdapter();
            DataSet ds = new DataSet();
            _cmd.Connection = _conn;
            _cmd.CommandType = pCommandType;
            _cmd.CommandTimeout = _commandTimeout;
            _cmd.CommandText = sqlQuery;
            _commandType = pCommandType;
            _sqlQuery = sqlQuery;

            //workaround for case where MySQL ODBC driver blows up on FillSchema when first parameter is a dataset instead of a table
            //blows up on an HY010 error (function sequence error) in NextResult method of the driver.
            DataTable dt = RunQueryDataTable(sqlQuery, pCommandType);
            ds.Tables.Add(dt);
            //da.SelectCommand = _cmd;
            //da.FillSchema(ds, SchemaType.Source);
            //da.Fill(ds);



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
            OdbcDataAdapter da = new OdbcDataAdapter();
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
            OdbcCommand cmd = new OdbcCommand(sqlText, _conn);
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
            OdbcDataAdapter da = new OdbcDataAdapter();
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
        /// Loads rows contained in an ADO.NET data table to an ODBC database table. Table must already exist. See CreateTable methods to create a new table.
        /// </summary>
        /// <param name="dt">DataTable object containing data to load.</param>
        public void ImportDataFromDataTable(DataTable dt)
        {
            ImportDataFromDataTable(dt, 1);
        }
        
        /// <summary>
        /// Loads rows contained in an ADO.NET data table to an ODBC database table. Table must already exist. See CreateTable methods to create a new table.
        /// </summary>
        /// <param name="dt">DataTable object containing data to load.</param>
        /// <param name="updateBatchSize">Number of individual SQL modification statements to include in a table modification operation.</param>
        public void ImportDataFromDataTable(DataTable dt, int updateBatchSize)
        {
            StringBuilder sql = new StringBuilder();
            sql.Length = 0;
            sql.Append("select * from ");
            sql.Append(dt.TableName);
            //sql.Append(";");
            OdbcCommand cmd = new OdbcCommand(sql.ToString(), _conn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = _commandTimeout;
            DataTable dbTable = dt.Clone();

            OdbcDataAdapter da = new OdbcDataAdapter(cmd);


            string[] supportedTypes = GetSupportedDataTypes();

            bool dataTypeFixNeeded = FixColumnDataTypes(dt, dbTable, supportedTypes);


            da.SelectCommand = cmd;
            OdbcCommandBuilder builder = new OdbcCommandBuilder(da);
            da.InsertCommand = builder.GetInsertCommand();
            //da.FillSchema(dbTable, SchemaType.Source);
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

            //only UpdateBatchSize = 1 supported
            da.UpdateBatchSize = 1;
            da.Update(dbTable);
            dbTable.AcceptChanges();


        }

        private string[] GetSupportedDataTypes()
        {
            string[] supportedTypes = null;
            OdbcConnection conn = new OdbcConnection(this.ConnectionString);
            int inx = -1;

            try
            {
                conn.Open();

                DataTable dt = conn.GetSchema("DataTypes");

                if (dt.Rows != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        supportedTypes = new string[dt.Rows.Count];
                        foreach (DataRow dr in dt.Rows)
                        {
                            //string dbDataType = dr["TypeName"].ToString();
                            //string dotnetDataType = dr["DataType"].ToString();
                            inx++;
                            supportedTypes[inx] = dr["DataType"].ToString();
                        }
                    }
                }


            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (supportedTypes == null)
                {
                    supportedTypes = new string[1];
                    supportedTypes[0] = "System.String";
                }
                if (conn != null)
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                conn.Dispose();
                conn = null;
            }

            return supportedTypes;
        }

        private bool DataTypeIsSupported(string dataTypeFullName, string[] supportedTypes)
        {
            bool isSupported = false;

            for (int i = 0; i < supportedTypes.Length; i++)
            {
                if (dataTypeFullName == supportedTypes[i])
                {
                    isSupported = true;
                    break;
                }
            }

            return isSupported;
        }

        private bool FixColumnDataTypes(DataTable dt, DataTable dbTable, string[] supportedTypes)
        {
            bool dataTypeFixNeeded = false;

            for (int c = 0; c < dt.Columns.Count; c++)
            {
                if (DataTypeIsSupported(dt.Columns[c].DataType.FullName, supportedTypes) == false)
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
                else
                {
                    ;
                }
            }

            return dataTypeFixNeeded;
        }

        private void FixColumnDataValues(DataTable dt, DataTable dbTable, string[] supportedTypes)
        {

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                DataRow dr = dbTable.NewRow();
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    if (DataTypeIsSupported(dt.Columns[c].DataType.FullName, supportedTypes) == false)
                    {
                        //dbTable.Columns[c].DataType = System.Type.GetType("System.String");
                        //dbTable.Columns[c].MaxLength = Int32.MaxValue;
                        dr[c] = Convert.ToString((object)dt.Rows[r][c]);
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
            DatabasePlatform dbplat = this.DbPlatform;

            dbplat = GetDatabasePlatform();
            if (dbplat == DatabasePlatform.MSOracle || dbplat == DatabasePlatform.OracleNative)
            {
                if (excludePatterns == null)
                {
                    excludePatterns = GetOracleExcludePatterns();
                }
                else if (excludePatterns.Length == 1 && excludePatterns[0].Trim() == string.Empty)
                {
                    excludePatterns = GetOracleExcludePatterns();
                }
                else
                {
                    ;
                }
            }
            return tabdefs.GetTableList(this, includePatterns, excludePatterns);
        }

        private string[] GetOracleExcludePatterns()
        {
            string[] excludePatterns = new string[8];

            excludePatterns[0] = "SYS.*";
            excludePatterns[1] = "SYSTEM.*";
            excludePatterns[2] = "XDB.*";
            excludePatterns[3] = "OUTLN.*";
            excludePatterns[4] = "MDSYS.*";
            excludePatterns[5] = "FLOWS_FILES.*";
            excludePatterns[6] = "CTXSYS.*";
            excludePatterns[7] = "APEX_040000.*";

            return excludePatterns;
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
