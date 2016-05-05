//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2013
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;
using System.Data.Common;
using PFDataAccessObjects;

namespace PFSQLServerCE40Objects
{
    /// <summary>
    /// Class contains functionality for working with SQL Server CE 4.0 databases.
    /// </summary>
    public class PFSQLServerCE40
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private DatabasePlatform _dbPlatform = DatabasePlatform.SQLServerCE_V4;

        private string _databasePath = string.Empty;
        private string _databasePassword = string.Empty;
        private bool _encryptionOn = false;
        private PFEncryptionMode _encryptionMode = PFEncryptionMode.EngineDefault;
        private string _connectionString = string.Empty;
        private SqlCeConnection _conn = new SqlCeConnection();

#pragma warning disable 1591
        public delegate void ResultDelegate(DataColumnCollection columns, DataRow data, int tabNumber);
        public event ResultDelegate returnResult;
        public delegate void ResultAsStringDelegate(string outputLine, int tabNumber);
        public event ResultAsStringDelegate returnResultAsString;
#pragma warning restore 1591

        /// <summary>
        /// Type of encryption for the database.
        /// </summary>
        public enum PFEncryptionMode
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
        /// Convert string description into a valid PFEncryptionMode value.
        /// </summary>
        /// <param name="encMode">String name of encryption mode.</param>
        /// <returns>PFEncryptionMode value.</returns>
        public static PFEncryptionMode GetEncryptionMode(string encMode)
        {
            PFEncryptionMode ret = PFEncryptionMode.EngineDefault;

            switch (encMode)
            {
                case "EngineDefault":
                    ret = PFEncryptionMode.EngineDefault;
                    break;
                case "PlatformDefault":
                    ret = PFEncryptionMode.PlatformDefault;
                    break;
                case "PPC2003Compatibility":
                    ret = PFEncryptionMode.PPC2003Compatibility;
                    break;
                default:
                    ret = PFEncryptionMode.EngineDefault;
                    break;
            }

            return ret;
        }

        /// <summary>
        /// Converts PFEncryptionMode value to a string name.
        /// </summary>
        /// <param name="encMode">Encryption mode to convert.</param>
        /// <returns>String containing text description of the encryption mode.</returns>
        public static string GetEncryptionModeDescription(PFEncryptionMode encMode)
        {
            string ret = "engine default";

            switch (encMode)
            {
                case PFEncryptionMode.EngineDefault:
                    ret = "engine default";
                    break;
                case PFEncryptionMode.PlatformDefault:
                    ret = "platform default";
                    break;
                case PFEncryptionMode.PPC2003Compatibility:
                    ret = "ppc2003 compatibility";
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
                _connectionString = string.Empty;

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
                _connectionString = string.Empty;
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
            }
        }

        /// <summary>
        /// Type of database encryption.
        /// </summary>
        public PFEncryptionMode EncryptionMode
        {
            get
            {
                return _encryptionMode;
            }
            set
            {
                _encryptionMode = value;
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
                if (cnnStr.Length == 0)
                    cnnStr = BuildConnectionString();
                return cnnStr;
            }
            set
            {
                _connectionString = value;
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
        private string BuildConnectionString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Length = 0;
            if (this.DatabasePath.Length > 0)
            {
                sb.Append("data source='");
                sb.Append(this.DatabasePath);
                sb.Append("';");
            }
            if (this.DatabasePassword.Length > 0)
            {
                sb.Append("database password='");
                sb.Append(this.DatabasePassword);
                sb.Append("';");
            }
            if (this.EncryptionOn)
            {
                sb.Append("Encrypt=True");
                sb.Append(";");
                sb.Append("encryption mode='");
                if (this.EncryptionMode == PFEncryptionMode.Unknown)
                    this.EncryptionMode = PFEncryptionMode.EngineDefault;
                sb.Append(PFSQLServerCE40.GetEncryptionModeDescription(this.EncryptionMode));
                sb.Append("';");
            }
            return sb.ToString();
        }


        //Public Methods

        /// <summary>
        /// Creates a SQLCE library object.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public SqlCeEngine GetSqlCeEngine(string connectionString)
        {
            SqlCeEngine engine = new SqlCeEngine(connectionString);
            return engine;
        }

        /// <summary>
        /// Creates a SQLCE database file.
        /// </summary>
        /// <returns>True if database created; otherwise false.</returns>
        /// <remarks>You must fill in DatabasePath property plus optional password and encryption properties to define the database to be created.</remarks>
        public bool CreateDatabase()
        {
            if (this.ConnectionString.Length == 0)
                throw new Exception("You must specify properties in order to create a database.");
            return CreateDatabase(this.ConnectionString);
        }
        /// <summary>
        /// Creates a SQLCE database file.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns>True if database created; otherwise false.</returns>
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



        private string BuildTableCreateStatement(DataTable dt)
        {
            PFTableBuilder tableBuilder = new PFTableBuilder(this.DbPlatform);

            return tableBuilder.BuildTableCreateStatement(dt);

            //StringBuilder sql = new StringBuilder();
            //string tableName = dt.TableName.Length > 0 ? dt.TableName : "Table01";
            //string colName = string.Empty;
            //string colDataType = string.Empty;
            //string colLength = string.Empty;
            //bool allowNull = false;
            //int colInx = 0;
            //int maxColInx = dt.Columns.Count - 1;

            //sql.Length = 0;
            //sql.Append("create table ");
            //sql.Append(tableName);
            //sql.Append("\r\n");
            //sql.Append("(\r\n");
            ////build columns
            //for (colInx = 0; colInx <= maxColInx; colInx++)
            //{
            //    colName = dt.Columns[colInx].ColumnName;
            //    if (dt.Columns[colInx].AllowDBNull)
            //        allowNull = true;
            //    else
            //        allowNull = false;

            //    switch (dt.Columns[colInx].DataType.FullName)
            //    {
            //        case "System.String":
            //            int maxLen = dt.Columns[colInx].MaxLength;
            //            if (maxLen < 1)
            //                maxLen = 255;
            //            if (maxLen <= 4000)
            //            {
            //                colDataType = "nvarchar";
            //                colLength = maxLen.ToString();
            //            }
            //            else
            //            {
            //                colDataType = "ntext";
            //                colLength = string.Empty;
            //            }
            //            break;
            //        case "System.Int32":
            //            colDataType = "integer";
            //            colLength = string.Empty;
            //            break;
            //        case "System.UInt32":
            //            colDataType = "bigint";
            //            colLength = string.Empty;
            //            break;
            //        case "System.Int64":
            //            colDataType = "bigint";
            //            colLength = string.Empty;
            //            break;
            //        case "System.UInt64":
            //            colDataType = "float";
            //            colLength = string.Empty;
            //            break;
            //        case "System.Int16":
            //            colDataType = "smallint";
            //            colLength = string.Empty;
            //            break;
            //        case "System.UInt16":
            //            colDataType = "integer";
            //            colLength = string.Empty;
            //            break;
            //        case "System.Double":
            //            colDataType = "float";
            //            colLength = string.Empty;
            //            break;
            //        case "System.Single":
            //            colDataType = "real";
            //            colLength = string.Empty;
            //            break;
            //        case "System.Decimal":
            //            colDataType = "decimal";
            //            colLength = string.Empty;
            //            break;
            //        case "System.DateTime":
            //            colDataType = "datetime";
            //            colLength = string.Empty;
            //            break;
            //        case "System.Guid":
            //            colDataType = "uniqueidentifier";
            //            colLength = string.Empty;
            //            break;
            //        case "System.Boolean":
            //            colDataType = "bit";
            //            colLength = string.Empty;
            //            break;
            //        case "System.Char":
            //            colDataType = "nchar";
            //            colLength = "1";
            //            break;
            //        case "System.Byte":
            //            colDataType = "tinyint";
            //            colLength = string.Empty;
            //            break;
            //        case "System.SByte":
            //            colDataType = "smallint";
            //            colLength = string.Empty;
            //            break;
            //        case "System.Object":
            //            colDataType = "image";
            //            colLength = string.Empty;
            //            break;
            //        default:
            //            colDataType = "ntext";
            //            colLength = string.Empty;
            //            break;
            //    }
            //    sql.Append("\t[");
            //    sql.Append(colName);
            //    sql.Append("] ");
            //    sql.Append(colDataType);
            //    if (colLength.Length > 0)
            //    {
            //        sql.Append("(");
            //        sql.Append(colLength);
            //        sql.Append(")");
            //    }
            //    if (allowNull)
            //        sql.Append(" null");
            //    else
            //        sql.Append(" not null");
            //    if (colInx < maxColInx)
            //        sql.Append(",");
            //    sql.Append("\r\n");
            //}
            //sql.Append(")\r\n");
            //sql.Append(";");

            //return sql.ToString();
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

            rdr = RunQueryReader(sql.ToString());

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
        /// Runs a non-query SQL statement.
        /// </summary>
        /// <param name="query">SQL statement.</param>
        /// <returns>Number of rows affected.</returns>
        public int RunNonQuery(string query)
        {
            int numRowsAffected = -1;
            SqlCeCommand cmd = new SqlCeCommand(query, _conn);
            numRowsAffected = cmd.ExecuteNonQuery();
            return numRowsAffected;
        }

        /// <summary>
        /// Runs query that returns results in SqlCeDataReader format.
        /// </summary>
        /// <param name="query">SQL statement.</param>
        /// <returns>Result rows.</returns>
        public SqlCeDataReader RunQueryReader(string query)
        {
            SqlCeCommand cmd = new SqlCeCommand(query, _conn);
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
            SqlCeResultSet res = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);
            return res;
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
            DataTable dt = new DataTable();
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            da.Fill(dt);
            da.FillSchema(dt, SchemaType.Source);
            return dt;
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
            DataSet ds = new DataSet();
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            da.Fill(ds, queryName);
            da.FillSchema(ds, SchemaType.Source, queryName);
            return ds;
        }

        /// <summary>
        /// Transforms a SqlCeDataReader object into a DataTable object.
        /// </summary>
        /// <param name="rdr">SqlCeDataReader object.</param>
        /// <returns>DataTable object.</returns>
        public static DataTable ConvertDataReaderToDataTable(SqlCeDataReader rdr)
        {
            return ConvertDataReaderToDataTable(rdr, "Table");
        }

        /// <summary>
        /// Transforms a SqlCeDataReader object into a DataTable object.
        /// </summary>
        /// <param name="rdr">SqlCeDataReader object.</param>
        /// <param name="tableName">Name that identifies the table.</param>
        /// <returns>DataTable object.</returns>
        public static DataTable ConvertDataReaderToDataTable(SqlCeDataReader rdr, string tableName)
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
        /// Returns data from a SqlCeDataReader object to the caller.
        /// </summary>
        /// <param name="rdr">SqlCeDataReader object containing data to be returned to the caller.</param>
        /// <remarks>Results can be retrieved by subscribing to the ResultDelegate event. </remarks>
        public void ProcessDataReader(SqlCeDataReader rdr)
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
        public void ExtractDelimitedDataFromDataReader(SqlCeDataReader rdr,
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
        public void ExtractFixedLengthDataFromDataReader(SqlCeDataReader rdr,
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
        /// Loads rows contained in an ADO.NET data table to a SQLCE database table. Table must already exist. See CreateTable methods to create a new table.
        /// </summary>
        /// <param name="dt">DataTable object containing data to load.</param>
        public void ImportDataFromDataTable(DataTable dt)
        {
            SqlCeCommand cmd = new SqlCeCommand(dt.TableName, _conn);
            cmd.CommandType = CommandType.TableDirect;
            DataTable dbTable = new DataTable();
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);

            da.SelectCommand = cmd;
            SqlCeCommandBuilder builder = new SqlCeCommandBuilder(da);
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
        public void SaveDataReaderToXmlFile(SqlCeDataReader rdr, string filePath)
        {
            DataTable dt = ConvertDataReaderToDataTable(rdr);

            SaveDataTableToXmlFile(dt, filePath);
        }

        /// <summary>
        /// Writes contents of DataReader plus the data schema in Xml format to specified output file.
        /// </summary>
        /// <param name="rdr">Data Reader object.</param>
        /// <param name="filePath">Location of the Xml output file.</param>
        public void SaveDataReaderWithSchemaToXmlFile(SqlCeDataReader rdr, string filePath)
        {
            DataTable dt = ConvertDataReaderToDataTable(rdr);

            SaveDataTableWithSchemaToXmlFile(dt, filePath);
        }

        /// <summary>
        /// Writes data schema in Xsd format to specified output file.
        /// </summary>
        /// <param name="rdr">Data Reader object.</param>
        /// <param name="filePath">Location of the Xsd output file.</param>
        public void SaveDataReaderToXmlSchemaFile(SqlCeDataReader rdr, string filePath)
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


    }//end class
}//end namespace
