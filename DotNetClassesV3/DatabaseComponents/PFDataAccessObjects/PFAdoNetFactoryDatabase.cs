//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2013
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using PFDataAccessObjects;
using PFCollectionsObjects;

namespace PFDataAccessObjects
{
    /// <summary>
    /// Class for generic access to databases for read and write operations. Using ADO.NET factory class routines.
    ///  Also uses IDatabaseProvider interface to access routines in classes that support that interface.
    ///  Does not support MSAccess and SQL Anywhere UltraLite databases because they do not have version 4 ADO.NET providers that support the Factory routines.
    /// </summary>
    public class PFAdoNetFactoryDatabase : IDatabaseProvider 
    {

        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();

        private string _providerInvariantName = string.Empty;
        private DbProviderFactory _factory = null;
        IDatabaseProvider _dbProvider = null;

        private PFList<KeyValuePair<string, string>> _dbProviderInvariantNames = new PFList<KeyValuePair<string, string>>()
               {new KeyValuePair<string,string>(DatabasePlatform.Unknown.ToString(),"System.Data.Odbc"),
                new KeyValuePair<string,string>(DatabasePlatform.MSSQLServer.ToString(),"System.Data.SqlClient"), 
                new KeyValuePair<string,string>(DatabasePlatform.SQLServerCE35.ToString(),"System.Data.SQLServerCE35.3.5"),
                new KeyValuePair<string,string>(DatabasePlatform.SQLServerCE40.ToString(),"System.Data.SQLServerCE35.4.0"),
                new KeyValuePair<string,string>(DatabasePlatform.MSOracle.ToString(),"System.Data.OracleClient"),
                new KeyValuePair<string,string>(DatabasePlatform.OracleNative.ToString(),"Oracle.DataAccess.Client"),
                new KeyValuePair<string,string>(DatabasePlatform.MySQL.ToString(),"MySql.Data.MySqlClient"),
                new KeyValuePair<string,string>(DatabasePlatform.SQLAnywhere.ToString(),"iAnywhere.Data.SQLAnywhere"),
                new KeyValuePair<string,string>(DatabasePlatform.Sybase.ToString(),"Sybase.Data.AseClient"),
                new KeyValuePair<string,string>(DatabasePlatform.DB2.ToString(),"IBM.Data.DB2"),
                new KeyValuePair<string,string>(DatabasePlatform.ODBC.ToString(),"System.Data.Odbc"),
                new KeyValuePair<string,string>(DatabasePlatform.OLEDB.ToString(),"System.Data.OleDb"),
               }; 

        //private variables for properties

        private DatabasePlatform _dbPlatform = DatabasePlatform.Unknown;

        private DbConnection _conn = null;
        private string _connectionString = string.Empty;
        private DbCommand _cmd = null;
        private System.Data.CommandType _commandType = CommandType.Text;
        private string _sqlQuery = string.Empty;


#pragma warning disable 1591
        public delegate void ResultDelegate(DataColumnCollection columns, DataRow data, int tabNumber);
        public event ResultDelegate returnResult;
        public delegate void ResultAsStringDelegate(string outputLine, int tabNumber);
        public event ResultAsStringDelegate returnResultAsString;
#pragma warning restore 1591

        //constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbPlatform"></param>
        public PFAdoNetFactoryDatabase(DatabasePlatform dbPlatform)
        {
            _dbPlatform = dbPlatform;
            InitInstance();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbPlatformDescription"></param>
        public PFAdoNetFactoryDatabase(string dbPlatformDescription)
        {
            _dbPlatform = GetDatabasePlatform(dbPlatformDescription);
            InitInstance();
        }

        private void InitInstance()
        {
            
            _providerInvariantName = GetProviderInvariantName(_dbPlatform);
            _dbProvider = GetProviderObject(_dbPlatform);
            _factory = DbProviderFactories.GetFactory(_providerInvariantName);
            _conn = _factory.CreateConnection();
            _cmd = _factory.CreateCommand();

        }

        private string GetProviderInvariantName(DatabasePlatform dbPlatform)
        {
            string providerInvariantName = string.Empty;
            for (int i = 0; i < _dbProviderInvariantNames.Count; i++)
            {
                if (_dbProviderInvariantNames[i].Key == dbPlatform.ToString())
                {
                    providerInvariantName = _dbProviderInvariantNames[i].Value;
                    break;
                }
            }
            return providerInvariantName;
        }

        private DatabasePlatform GetDatabasePlatform(string dbPlatformDescription)
        {
            DatabasePlatform ret = DatabasePlatform.Unknown;
            var dbPlatforms = Enum.GetValues(typeof(DatabasePlatform));
            foreach (DatabasePlatform dbplat in dbPlatforms)
            {
                if (dbPlatformDescription.ToUpper() == dbplat.ToString().ToUpper())
                {
                    ret = dbplat;
                    break;
                }
            }
            return ret;
        }

        private IDatabaseProvider GetProviderObject(DatabasePlatform dbPlatform)
        {
            IDatabaseProvider dbProviderObject = null;

            switch (dbPlatform)
            {
                case DatabasePlatform.MSSQLServer:
                    dbProviderObject = new PFSQLServer();
                    break;
                case DatabasePlatform.SQLAnywhere:
                    dbProviderObject = new PFSQLAnywhere();
                    break;
                //case DatabasePlatform.Sybase:
                //    dbProviderObject = new PFSybase();
                //    break;
                case DatabasePlatform.OracleNative:
                    dbProviderObject = new  PFOracle();
                    break;
                default:
                    _msg.Length = 0;
                    _msg.Append("Unable to process DbPlatform found in GetTableList: ");
                    _msg.Append(this.DbPlatform.ToString());
                    throw new DataException(_msg.ToString());
            }


            return dbProviderObject;
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

            _dbProvider.ConnectionString = this.ConnectionString;
            _dbProvider.OpenConnection();
            bSuccess = _dbProvider.CreateTable(dt, out createScript);
            _dbProvider.CloseConnection();

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
        /// Method to determine if a table exists in the database.
        /// </summary>
        /// <param name="catalogName">Name of database in which table is located.</param>
        /// <param name="schemaName">Name of the schema to which the table belongs.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table exists; otherwise false.</returns>
        public bool TableExists(string catalogName, string schemaName, string tableName)
        {
            bool bSuccess = true;

            _dbProvider.ConnectionString = this.ConnectionString;
            _dbProvider.OpenConnection();
            bSuccess = _dbProvider.TableExists(catalogName, schemaName, tableName);
            _dbProvider.CloseConnection();

            return bSuccess;
        }

        /// <summary>
        /// Method to determine if a table exists in the database.
        /// </summary>
        /// <param name="schemaName">Name of the schema to which the table belongs.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table exists; otherwise false.</returns>
        public bool TableExists(string schemaName, string tableName)
        {
            bool bSuccess = true;

            _dbProvider.ConnectionString = this.ConnectionString;
            _dbProvider.OpenConnection();
            bSuccess = _dbProvider.TableExists(schemaName, tableName);
            _dbProvider.CloseConnection();

            return bSuccess;
        }

        /// <summary>
        /// Method to determine if a table exists in the database.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table exists; otherwise false.</returns>
        /// <remarks>If schema is important for identifying the table, the table name should be in schemaname.tablename format. Or you can use the version of this method that takes schema and table name parameters.</remarks>
        public bool TableExists(string tableName)
        {
            bool bSuccess = true;

            _dbProvider.ConnectionString = this.ConnectionString;
            _dbProvider.OpenConnection();
            bSuccess = _dbProvider.TableExists(tableName);
            _dbProvider.CloseConnection();

            return bSuccess;
        }

        /// <summary>
        /// Method to drop a table from the database.
        /// </summary>
        /// <param name="catalogName">Name of database in which table is located.</param>
        /// <param name="schemaName">Name of the schema to which the table belongs.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table no longer exists; otherwise returns false if table still exists.</returns>
        public bool DropTable(string catalogName, string schemaName, string tableName)
        {
            bool bSuccess = true;

            _dbProvider.ConnectionString = this.ConnectionString;
            _dbProvider.OpenConnection();
            bSuccess = _dbProvider.DropTable(catalogName, schemaName, tableName);
            _dbProvider.CloseConnection();

            return bSuccess;
        }

        /// <summary>
        /// Method to drop a table from the database.
        /// </summary>
        /// <param name="schemaName">Name of the schema to which the table belongs.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table no longer exists; otherwise returns false if table still exists.</returns>
        public bool DropTable(string schemaName, string tableName)
        {
            bool bSuccess = true;

            _dbProvider.ConnectionString = this.ConnectionString;
            _dbProvider.OpenConnection();
            bSuccess = _dbProvider.DropTable(schemaName, tableName);
            _dbProvider.CloseConnection();

            return bSuccess;
        }

        /// <summary>
        /// Method to drop a table from the database.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if table no longer exists; otherwise returns false if table still exists.</returns>
        /// <remarks>If schema is important for identifying the table, the table name should be in schemaname.tablename format. Or you can use the version of this method that takes schema and table name parameters.</remarks>
        public bool DropTable(string tableName)
        {
            bool bSuccess = true;

            _dbProvider.ConnectionString = this.ConnectionString;
            _dbProvider.OpenConnection();
            bSuccess = _dbProvider.DropTable(tableName);
            _dbProvider.CloseConnection();

            return bSuccess;
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
            _cmd.CommandText = sqlQuery;
            _commandType = pCommandType;
            _sqlQuery = sqlQuery;
            DbDataReader rdr = _cmd.ExecuteReader();
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
            DbDataAdapter da = _factory.CreateDataAdapter();
            DataSet ds = new DataSet();
            _cmd.Connection = _conn;
            _cmd.CommandType = pCommandType;
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
            DbDataAdapter da = _factory.CreateDataAdapter();
            DataTable dt = new DataTable();
            _cmd.Connection = _conn;
            _cmd.CommandType = pCommandType;
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
            DbCommand cmd = _factory.CreateCommand();
            cmd.Connection = _conn;
            cmd.CommandText = sqlText;
            cmd.CommandType = CommandType.Text;
            numRowsAffected = cmd.ExecuteNonQuery();
            return numRowsAffected;
        }


        /// <summary>
        /// Transforms a DataReader object into a DataTable object.
        /// </summary>
        /// <param name="rdr">DbDataReader object.</param>
        /// <returns>DataTable object.</returns>
        public DataTable ConvertDataReaderToDataTable(DbDataReader rdr)
        {
            return ConvertDataReaderToDataTable(rdr, "Table");
        }

        /// <summary>
        /// Transforms a DataReader object into a DataTable object.
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
        /// <param name="rdr">DbDataReader object containing data to be returned to the caller.</param>
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
        /// Loads rows contained in an ADO.NET data table to a database table. Table must already exist. 
        /// </summary>
        /// <param name="dt">DataTable object containing data to load.</param>
        public void ImportDataFromDataTable(DataTable dt)
        {
            StringBuilder sql = new StringBuilder();
            sql.Length = 0;
            sql.Append("select * from ");
            sql.Append(dt.TableName);
            sql.Append(";");
            //DbCommand cmd = new SqlCommand(sql.ToString(), _conn);
            DbCommand cmd = _factory.CreateCommand();
            cmd.Connection = _conn;
            cmd.CommandText = sql.ToString();
            cmd.CommandType = CommandType.Text;
            DataTable dbTable = new DataTable();
            DbDataAdapter da = _factory.CreateDataAdapter();

            da.SelectCommand = cmd;
            //DbCommandBuilder builder = new DbCommandBuilder(da);
            DbCommandBuilder builder = _factory.CreateCommandBuilder();
            builder.DataAdapter = da;
            da.InsertCommand = builder.GetInsertCommand();
            da.FillSchema(dt, SchemaType.Source);
            da.Fill(dbTable);

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

            da.Update(dbTable);
            dbTable.AcceptChanges();


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
            return _dbProvider.TypeIsUserTable(dr);
        }

        /// <summary>
        /// Function to build a qualified table name. Ususally this means attaching schema name in front of table name. In some cases both the catalog name and the schema name will be prepended to the table namne.
        ///  Result depends on the requirements of the database platform implementing this function.
        /// </summary>
        /// <param name="dr">DataRow returned by GetSchema.</param>
        /// <returns>Full table name in either schemaname.tablename or catalogname.schemaname.tablename format.</returns>
        public string GetFullTableName(DataRow dr)
        {
            return _dbProvider.GetFullTableName(dr);
        }

        /// <summary>
        /// Function to return the catalog, schema and name parts of a fully qualified table name. Some databases will only return the schema and name since catalog is not used by those database engines.
        /// </summary>
        /// <param name="dr">DataRow returned by GetSchema that contains Tables information.</param>
        /// <returns>Object containing the different qualifiers in the table name.</returns>
        public TableNameQualifiers GetTableNameQualifiers(DataRow dr)
        {
            return _dbProvider.GetTableNameQualifiers(dr);
        }


        /// <summary>
        /// Routine to recreate with a different schema name. Used when transferring a table definition to a new database.
        /// </summary>
        /// <param name="tabDef">Object containing the table definition to be reformatted.</param>
        /// <param name="newSchemaName">Schema name to apply to the reformatted table name.</param>
        /// <returns>Table name that includes the new schema name.</returns>
        public string RebuildFullTableName(PFTableDef tabDef, string newSchemaName)
        {
            return _dbProvider.RebuildFullTableName(tabDef, newSchemaName);
        }

        /// <summary>
        /// Routine uses table name information supplied by a PFTableDef object to determine if a table exists in the database.
        /// </summary>
        /// <param name="td">Object containing table definition information.</param>
        /// <returns>True if table exists; otherwise false.</returns>
        public bool TableExists(PFTableDef td)
        {
            return _dbProvider.TableExists(td);
        }

        /// <summary>
        /// Routine retrieves table name information supplied by a PFTableDef object to drop (delete) a table in the database.
        /// </summary>
        /// <param name="td">Object containing table definition information.</param>
        /// <returns>True if table no longer exists; otherwise returns false if table still exists.</returns>
        public bool DropTable(PFTableDef td)
        {
            return _dbProvider.DropTable(td);
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
            PFList<PFTableDef> tableDefs = null;

            _dbProvider.ConnectionString = this.ConnectionString;
            _dbProvider.OpenConnection();
            tableDefs = _dbProvider.GetTableList(includePatterns, excludePatterns);
            _dbProvider.CloseConnection();

            return tableDefs;
        }

        /// <summary>
        /// Method to convert table definitions from another database format to the data format supported by this class.
        /// </summary>
        /// <param name="tableDefs">Object containing the list of table definitions to be converted.</param>
        /// <param name="newSchemaName">Specify a new schema (owner) name for the tables when they are recreated in the database managed by the current instance.</param>
        /// <returns>Object containing the list of table definitions after they have been converted to match the data formats of the current instance.</returns>
        public PFList<PFTableDef> ConvertTableDefs(PFList<PFTableDef> tableDefs, string newSchemaName)
        {
            PFList<PFTableDef> newTableDefs = null;

            _dbProvider.ConnectionString = this.ConnectionString;
            _dbProvider.OpenConnection();
            newTableDefs = _dbProvider.ConvertTableDefs(tableDefs, newSchemaName);
            _dbProvider.CloseConnection();

            return newTableDefs;

            
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
            int numTablesCreated = 0;

            _dbProvider.ConnectionString = this.ConnectionString;
            _dbProvider.OpenConnection();
            numTablesCreated = _dbProvider.CreateTablesFromTableDefs(tableDefs, dropBeforeCreate);
            _dbProvider.CloseConnection();

            return numTablesCreated;
        }


        /// <summary>
        /// Runs the table create statements contained in the provided tableDefs object.
        /// </summary>
        /// <param name="sourceDatabase">Database object representing the source database.</param>
        /// <param name="newSchemaName">Schema to use for identifying the destination tables.</param>
        /// <param name="dropBeforeCreate">If true and table already exists, table will be dropped and then recreated using the table definition in the supplied PFTableDefs list. If false, table create step is bypassed if table already exists.</param>
        /// <returns>Number of tables created.</returns>
        public PFList<TableCopyDetails> CopyTableDataFromTableDefs(PFDatabase sourceDatabase, string newSchemaName, bool dropBeforeCreate)
        {
            PFList<TableCopyDetails> tableCopyLog = null;

            _dbProvider.ConnectionString = this.ConnectionString;
            _dbProvider.OpenConnection();
            tableCopyLog = _dbProvider.CopyTableDataFromTableDefs(sourceDatabase,newSchemaName,dropBeforeCreate);
            _dbProvider.CloseConnection();

            return tableCopyLog;
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
            PFList<TableCopyDetails> tableCopyLog = null;

            _dbProvider.ConnectionString = this.ConnectionString;
            _dbProvider.OpenConnection();
            tableCopyLog = _dbProvider.CopyTableDataFromTableDefs(sourceDatabase, tableIncludePatterns, tableExcludePatterns, newSchemaName, dropBeforeCreate);
            _dbProvider.CloseConnection();

            return tableCopyLog;
        }






        //class helpers

        /// <summary>
        /// Routine overrides default ToString method and outputs name, type, scope and value for all class properties and fields.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder data = new StringBuilder();
            data.Append(PropertiesToString());
            data.Append("\r\n");
            data.Append(FieldsToString());
            data.Append("\r\n");


            return data.ToString();
        }


        /// <summary>
        /// Routine outputs name and value for all properties.
        /// </summary>
        /// <returns></returns>
        public string PropertiesToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            data.Append("Class type:");
            data.Append(t.FullName);
            data.Append("\r\nClass properties for");
            data.Append(t.FullName);
            data.Append("\r\n");


            int inx = 0;
            int maxInx = props.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                PropertyInfo prop = props[inx];
                object val = prop.GetValue(this, null);

                /*
                //****************************************************************************************
                //use the following code if you class has an indexer or is derived from an indexed class
                //****************************************************************************************
                object val = null;
                StringBuilder temp = new StringBuilder();
                if (prop.GetIndexParameters().Length == 0)
                {
                    val = prop.GetValue(this, null);
                }
                else if (prop.GetIndexParameters().Length == 1)
                {
                    temp.Length = 0;
                    for (int i = 0; i < this.Count; i++)
                    {
                        temp.Append("Index ");
                        temp.Append(i.ToString());
                        temp.Append(" = ");
                        temp.Append(val = prop.GetValue(this, new object[] { i }));
                        temp.Append("  ");
                    }
                    val = temp.ToString();
                }
                else
                {
                    //this is an indexed property
                    temp.Length = 0;
                    temp.Append("Num indexes for property: ");
                    temp.Append(prop.GetIndexParameters().Length.ToString());
                    temp.Append("  ");
                    val = temp.ToString();
                }
                //****************************************************************************************
                // end code for indexed property
                //****************************************************************************************
                */

                if (prop.GetGetMethod(true) != null)
                {
                    data.Append(" <");
                    if (prop.GetGetMethod(true).IsPublic)
                        data.Append(" public ");
                    if (prop.GetGetMethod(true).IsPrivate)
                        data.Append(" private ");
                    if (!prop.GetGetMethod(true).IsPublic && !prop.GetGetMethod(true).IsPrivate)
                        data.Append(" internal ");
                    if (prop.GetGetMethod(true).IsStatic)
                        data.Append(" static ");
                    data.Append(" get ");
                    data.Append("> ");
                }
                if (prop.GetSetMethod(true) != null)
                {
                    data.Append(" <");
                    if (prop.GetSetMethod(true).IsPublic)
                        data.Append(" public ");
                    if (prop.GetSetMethod(true).IsPrivate)
                        data.Append(" private ");
                    if (!prop.GetSetMethod(true).IsPublic && !prop.GetSetMethod(true).IsPrivate)
                        data.Append(" internal ");
                    if (prop.GetSetMethod(true).IsStatic)
                        data.Append(" static ");
                    data.Append(" set ");
                    data.Append("> ");
                }
                data.Append(" ");
                data.Append(prop.PropertyType.FullName);
                data.Append(" ");

                data.Append(prop.Name);
                data.Append(": ");
                if (val != null)
                    data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (prop.PropertyType.IsArray)
                {
                    System.Collections.IList valueList = (System.Collections.IList)prop.GetValue(this, null);
                    for (int i = 0; i < valueList.Count; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(valueList[i].ToString());
                        data.Append("  ");
                    }
                }

                data.Append("\r\n");

            }

            return data.ToString();
        }

        /// <summary>
        /// Routine outputs name and value for all fields.
        /// </summary>
        /// <returns></returns>
        public string FieldsToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            FieldInfo[] finfos = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.SetProperty | BindingFlags.GetProperty | BindingFlags.FlattenHierarchy);
            bool typeHasFieldsToStringMethod = false;

            data.Append("\r\nClass fields for ");
            data.Append(t.FullName);
            data.Append("\r\n");

            int inx = 0;
            int maxInx = finfos.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                FieldInfo fld = finfos[inx];
                object val = fld.GetValue(this);
                if (fld.IsPublic)
                    data.Append(" public ");
                if (fld.IsPrivate)
                    data.Append(" private ");
                if (!fld.IsPublic && !fld.IsPrivate)
                    data.Append(" internal ");
                if (fld.IsStatic)
                    data.Append(" static ");
                data.Append(" ");
                data.Append(fld.FieldType.FullName);
                data.Append(" ");
                data.Append(fld.Name);
                data.Append(": ");
                typeHasFieldsToStringMethod = UseFieldsToString(fld.FieldType);
                if (val != null)
                    if (typeHasFieldsToStringMethod)
                        data.Append(GetFieldValues(val));
                    else
                        data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (fld.FieldType.IsArray)
                //if (fld.Name == "TestStringArray" || "_testStringArray")
                {
                    System.Collections.IList valueList = (System.Collections.IList)fld.GetValue(this);
                    for (int i = 0; i < valueList.Count; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(valueList[i].ToString());
                        data.Append("  ");
                    }
                }

                data.Append("\r\n");

            }

            return data.ToString();
        }

        private bool UseFieldsToString(Type pType)
        {
            bool retval = false;

            //avoid have this type calling its own FieldsToString and going into an infinite loop
            if (pType.FullName != this.GetType().FullName)
            {
                MethodInfo[] methods = pType.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    if (method.Name == "FieldsToString")
                    {
                        retval = true;
                        break;
                    }
                }
            }

            return retval;
        }

        private string GetFieldValues(object typeInstance)
        {
            Type typ = typeInstance.GetType();
            MethodInfo methodInfo = typ.GetMethod("FieldsToString");
            Object retval = methodInfo.Invoke(typeInstance, null);


            return (string)retval;
        }






    }//end class
}//end namespace
