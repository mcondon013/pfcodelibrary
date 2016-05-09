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
using System.Data.Common;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using PFDataAccessObjects;
using PFCollectionsObjects;
using PFListObjects;

namespace PFOracleObjects
{
    /// <summary>
    /// Class wrapper for the MSOracle .NET provider.
    /// Requires Oracle Native client to be installed on the machine using this class. (e.g. Oracle.DataAccess)
    /// </summary>
    public class PFOracle : IDatabaseProvider 
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private DatabasePlatform _dbPlatform = DatabasePlatform.OracleNative;

        OracleConnection _conn = new OracleConnection();
        private OracleCommand _cmd = new OracleCommand();
        private System.Data.CommandType _commandType = CommandType.Text;
        private int _commandTimeout = 300;
        private string _sqlQuery = string.Empty;

        //private variables for properties

        private string _dataSource = string.Empty;
        private bool _useIntegratedSecurity = false;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _connectionString = string.Empty;
        private PFCollectionsObjects.PFKeyValueList<string, string> _connectionStringKeyVals = new PFCollectionsObjects.PFKeyValueList<string, string>();
        private bool _isConnected = false;

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
        public PFOracle()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dataSource"></param>
        public PFOracle(string dataSource)
        {
            this.DataSource = dataSource;
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
        /// Determines type of command to run.
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
        /// SQL to be executed.
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
        /// The name or network address of the instance of MSOracle to which to connect. 
        /// </summary>
        public string DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// Determines whether or not Windows integrated security will be used.
        /// </summary>
        public bool UseIntegratedSecurity
        {
            get
            {
                return _useIntegratedSecurity;
            }
            set
            {
                _useIntegratedSecurity = value;
                BuildConnectionString();
            }
        }

        /// <summary>
        /// Logon username.
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
        /// Logon password.
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
        /// String used to open the database.
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


        //methods

        private void ParseConnectionString()
        {
            OracleConnectionStringBuilder oracleConnBuilder;
            if (_connectionString.Trim().Length > 0)
            {
                oracleConnBuilder = new OracleConnectionStringBuilder(_connectionString.Trim());
                _dataSource = oracleConnBuilder.DataSource.Trim();
                if (oracleConnBuilder.UserID.Trim() != @"/")
                {
                    
                    _username = oracleConnBuilder.UserID.Trim();
                    _useIntegratedSecurity = false;
                }
                else
                {
                    _username = @"/";
                    _useIntegratedSecurity = true;
                }
                _password = oracleConnBuilder.Password;
            }
            else
            {
                _dataSource = string.Empty;
                _useIntegratedSecurity = false;
                _username = string.Empty;
                _password = string.Empty;
            }
        }

        private void BuildConnectionString()
        {
            OracleConnectionStringBuilder oracleConnBuilder = new OracleConnectionStringBuilder();

            if (this.DataSource.Trim().Length > 0)
                oracleConnBuilder.DataSource = this.DataSource;
            else
                oracleConnBuilder.DataSource = string.Empty;
            if (this.UseIntegratedSecurity == false)
            {
                if (this.Username.Trim().Length > 0)
                    oracleConnBuilder.UserID = this.Username.Trim();
                else
                    oracleConnBuilder.UserID = string.Empty;
            }
            else
            {
                oracleConnBuilder.UserID = "/";
            }
            if (this.Password.Length > 0)
                oracleConnBuilder.Password = this.Password;



            _connectionString = oracleConnBuilder.ToString();
        }


        /// <summary>
        /// Returns a list of key/value pairs that contains all the keys and their associated values for the current connection string.
        /// </summary>
        /// <returns>List in Key/Value format.</returns>
        private PFCollectionsObjects.PFKeyValueList<string, string> GetConnectionStringKeyVals()
        {
            OracleConnectionStringBuilder oracleConnBuilder = new OracleConnectionStringBuilder(this.ConnectionString);
            _connectionStringKeyVals.Clear();

            foreach (string key in oracleConnBuilder.Keys)
            {
                _connectionStringKeyVals.Add(new stKeyValuePair<string, string>(key, oracleConnBuilder[key].ToString()));
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
            try
            {
                //convert column names to all upper case & get rid of embedded spaces
                for (int colInx = 0; colInx < dt.Columns.Count; colInx++)
                {
                    dt.Columns[colInx].ColumnName = dt.Columns[colInx].ColumnName.Replace(" ", "_").Replace("-","_").Replace("/","_").ToUpper();
                }
                sqlScript = BuildTableCreateStatement(dt);
                createScript = sqlScript;
                if (this._conn.State != ConnectionState.Open)
                    this.OpenConnection();
                rowsAffected = this.RunNonQuery(sqlScript, CommandType.Text);
                bSuccess = true;
            }
            catch (OracleException cex)
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
        /// Method to determine if a table exists in the database.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table exists; otherwise false.</returns>
        public bool TableExists(string tableName)
        {
            StringBuilder sql = new StringBuilder();
            int numRows = -1;
            OracleDataReader rdr = null;
            //if the schema (owner) name is not embedded in the table name then table will show as not existing
            sql.Length = 0;
            sql.Append("select count(*) as numRows");
            sql.Append(" from all_objects");
            sql.Append(" where object_type in ('TABLE','VIEW')");
            sql.Append(" and (OWNER || '.' || OBJECT_NAME) = upper('");
            sql.Append(tableName);
            sql.Append("')");

            rdr = (OracleDataReader)RunQueryDataReader(sql.ToString(), System.Data.CommandType.Text);
            if (rdr.HasRows)
            {
                rdr.Read();
                numRows = Convert.ToInt32(rdr[0].ToString());
            }

            if (numRows > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Method to determine if a table exists in the database.
        /// </summary>
        /// <param name="schemaName">Schema or owner name for the table.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table exists; otherwise false.</returns>
        public bool TableExists(string schemaName, string tableName)
        {
            StringBuilder sql = new StringBuilder();
            int numRows = -1;
            OracleDataReader rdr = null;

            sql.Length = 0;
            sql.Append("select count(*) as numRows");
            sql.Append(" from all_objects");
            sql.Append(" where object_type in ('TABLE','VIEW')");
            sql.Append(" and object_name = upper('");
            sql.Append(tableName);
            sql.Append("') and owner = upper('");
            sql.Append(schemaName);
            sql.Append("')");

            rdr = (OracleDataReader)RunQueryDataReader(sql.ToString(), System.Data.CommandType.Text);
            if (rdr.HasRows)
            {
                rdr.Read();
                numRows = Convert.ToInt32(rdr[0].ToString());
            }

            if (numRows > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Method to determine if a table exists in the database.
        /// </summary>
        /// <param name="catalogName">Catalog name is ignored by this version of method.</param>
        /// <param name="schemaName">Schema or owner name for the table.</param>
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
            bool ret = false;
            StringBuilder sql = new StringBuilder();
            string tabName = string.Empty;

            tabName = tableName.ToUpper();

            if (TableExists(tabName))
            {
                sql.Length = 0;
                sql.Append("drop table ");
                sql.Append(tabName);

                RunNonQuery(sql.ToString(), System.Data.CommandType.Text);

                if (TableExists(tabName))
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
        /// <param name="schemaName">Schema or owner name for the table.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table no longer exists; otherwise returns false if table still exists.</returns>
        public bool DropTable(string schemaName, string tableName)
        {
            bool ret = false;
            StringBuilder sql = new StringBuilder();
            string tabName = string.Empty;
            if (schemaName.Length > 0)
                tabName = schemaName.ToUpper() + "." + tableName.ToUpper();
            else
                tabName = tableName.ToUpper();

            if (TableExists(schemaName, tableName))
            {
                sql.Length = 0;
                sql.Append("drop table ");
                sql.Append(tabName);

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
        /// Opens connection to database.
        /// </summary>
        public void OpenConnection()
        {
            if (this.ConnectionString.Trim().Length == 0)
               BuildConnectionString();
            _conn.ConnectionString = this.ConnectionString;
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
            _cmd.CommandText = sqlQuery;
            _cmd.CommandTimeout = _commandTimeout;
            _commandType = pCommandType;
            _sqlQuery = sqlQuery;
            OracleDataReader rdr = _cmd.ExecuteReader();
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
            OracleDataAdapter da = new OracleDataAdapter();
            DataSet ds = new DataSet();
            _cmd.Connection = _conn;
            _cmd.CommandType = pCommandType;
            _cmd.CommandText = sqlQuery;
            _cmd.CommandTimeout = _commandTimeout;
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
            OracleDataAdapter da = new OracleDataAdapter();
            DataTable dt = new DataTable();
            _cmd.Connection = _conn;
            _cmd.CommandType = pCommandType;
            _cmd.CommandText = sqlQuery;
            _cmd.CommandTimeout = _commandTimeout;
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
            OracleCommand cmd = new OracleCommand(sqlText, _conn);
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
            OracleDataAdapter da = new OracleDataAdapter();
            DataTable dt = new DataTable();
            _cmd.Connection = _conn;
            _cmd.CommandType = pCommandType;
            _cmd.CommandText = sqlQuery;
            _cmd.CommandTimeout = _commandTimeout;
            _commandType = pCommandType;
            _sqlQuery = sqlQuery;

            da.SelectCommand = _cmd;
            da.FillSchema(dt, SchemaType.Source);

            return dt;
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
                    if (drow["IsUnique"] != DBNull.Value)
                        column.Unique = (bool)drow["IsUnique"];
                    else
                        column.Unique = false;
                    if(drow["AllowDBNull"]!=DBNull.Value)
                        column.AllowDBNull = (bool)drow["AllowDBNull"];
                    else
                        column.AllowDBNull=false;
                    column.AutoIncrement = false;
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
            //fix any column names that are too long
            for (int colInx = 0; colInx < dt.Columns.Count; colInx++)
            {
                string colName = dt.Columns[colInx].ColumnName;
                if (colName.Length > 30)
                    dt.Columns[colInx].ColumnName = colName.Substring(0, 20) + "_Field" + colInx.ToString("0000");

            }
            //convert column names to all upper case & get rid of embedded spaces
            for (int colInx = 0; colInx < dt.Columns.Count; colInx++)
            {
                dt.Columns[colInx].ColumnName = dt.Columns[colInx].ColumnName.Replace(" ", "_").Replace("-", "_").Replace("/", "_").ToUpper();
            }

            sql.Length = 0;
            sql.Append(BuildSelectStatment(dt));
 
            //sql.Append("select * from ");
            //sql.Append(dt.TableName);
            //sql.Append(";");
            OracleCommand cmd = new OracleCommand(sql.ToString(), _conn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = _commandTimeout;
            //DataTable dbTable = dt.Clone();
            OracleDataAdapter da = new OracleDataAdapter(cmd);

            da.SelectCommand = cmd;
            OracleCommandBuilder builder = new OracleCommandBuilder(da);
            builder.QuotePrefix = "\"";
            builder.QuoteSuffix = "\"";
            da.InsertCommand = builder.GetInsertCommand();
            //da.FillSchema(dt, SchemaType.Source);     //can cause an exception
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


            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    DataRow inrow = dt.Rows[i];
            //    DataRow outrow = dbTable.NewRow();
            //    outrow.ItemArray = inrow.ItemArray;
            //    dbTable.Rows.Add(outrow);
            //}

            //da.UpdateBatchSize = updateBatchSize;
            //da.Update(dbTable);
            //dbTable.AcceptChanges();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i].AcceptChanges();
                dt.Rows[i].SetAdded();
            }

            da.UpdateBatchSize = updateBatchSize;
            da.Update(dt);
            dt.AcceptChanges();


        }


        private string BuildSelectStatment(DataTable dt)
        {
            string colName = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.Length = 0;
            sql.Append("select ");
            

            int maxColInx = dt.Columns.Count - 1;
            for (int colInx = 0; colInx <= maxColInx; colInx++)
            {
                colName = dt.Columns[colInx].ColumnName;
                sql.Append(colName);
                if (colInx < maxColInx)
                    sql.Append(", ");
            }

            sql.Append(" from ");
            sql.Append(dt.TableName);
            sql.Append(";");



            return sql.ToString();
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
        /// Function to determine if the type of table object is a user (base) table. This function is used to eliminate system tables from the output.
        /// </summary>
        /// <param name="dr">DataRow returned by GetSchema.</param>
        /// <returns>True if the table is a user or base table.</returns>
        public bool TypeIsUserTable(DataRow dr)
        {
            bool result = false;

            if (dr["TYPE"].ToString().ToUpper() == "USER")
                result = true;

            return result;
        }

        /// <summary>
        /// Function to build a qualified table name. Ususally this means attaching schema name in front of table name. In some cases both the catalog name and the schema name will be prepended to the table namne.
        ///  Result depends on the requirements of the database platform implementing this function.
        /// </summary>
        /// <param name="dr">DataRow returned by GetSchema.</param>
        /// <returns>Full table name in either schemaname.tablename or catalogname.schemaname.tablename format.</returns>
        public string GetFullTableName(DataRow dr)
        {
            return dr["OWNER"].ToString() + "." + dr["TABLE_NAME"].ToString(); ;
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
            tnq.TableSchema = dr["OWNER"].ToString();
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
