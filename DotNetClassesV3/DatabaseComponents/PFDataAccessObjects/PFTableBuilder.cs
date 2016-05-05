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
using System.Data.OleDb;
using System.Data.Odbc;
using PFTextObjects;
using PFSystemObjects;
using PFCollectionsObjects;

namespace PFDataAccessObjects
{
    /// <summary>
    /// Class to support creation of table create statements by the various data access objects.
    ///  Actual creation of tables in done by specific database provider objects.
    /// </summary>
    public class PFTableBuilder
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties
        private DatabasePlatform _databaseType = DatabasePlatform.Unknown;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFTableBuilder(DatabasePlatform dbType)
        {
            _databaseType = dbType;
        }

        //properties

        /// <summary>
        /// Specifies database in which the data operations will take place.
        /// </summary>
        public DatabasePlatform DatabaseType
        {
            get
            {
                return _databaseType;
            }
            set
            {
                _databaseType = value;
            }
        }

        //methods

        private bool ColumnIsPrimaryKey (DataTable dt, string columnName)
        {
            bool ret = false;

            if(dt.PrimaryKey != null)
            {
                if (dt.PrimaryKey.Length > 0)
                {
                    for (int i = 0; i < dt.PrimaryKey.Length; i++)
                    {
                        if (columnName.ToUpper() == dt.PrimaryKey[i].ColumnName.ToUpper())
                        {
                            ret = true;
                            break;
                        }
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Routine for building a table create statement based on the schema of an ADO.NET DataTable object.
        /// </summary>
        /// <param name="dt">DataTable containing schema to use.</param>
        /// <returns>String containing a CREATE TABLE statement.</returns>
        public string BuildTableCreateStatement(DataTable dt)
        {
            return BuildTableCreateStatement(dt, null);
        }



        /// <summary>
        /// Routine for building a table create statement based on the schema of an ADO.NET DataTable object.
        /// </summary>
        /// <param name="dt">DataTable containing schema to use.</param>
        /// <param name="connectionString">Only needed for the ODBC and OleDB database platforms. Is used to make a connection to the database to get datatype schema information.</param>
        /// <returns>String containing a CREATE TABLE statement.</returns>
        public string BuildTableCreateStatement(DataTable dt, string connectionString)
        {
            string sqlScript = string.Empty;

            if (dt == null)
            {
                _msg.Length = 0;
                _msg.Append("You must specify a DataTable object for the BuildTableCreateStatement method.");
                throw new ArgumentNullException(_msg.ToString());
            }

            if (connectionString == null && (this.DatabaseType == DatabasePlatform.ODBC || this.DatabaseType == DatabasePlatform.OLEDB) )
            {
                _msg.Length = 0;
                _msg.Append("You must specify a connection string for the BuildTableCreateStatement method for ODBC and OLEDB database providers.");
                throw new ArgumentNullException(_msg.ToString());
            }


            switch(this.DatabaseType)
            {
                case DatabasePlatform.MSSQLServer:
                    sqlScript = SQLServerTableCreateStatement(dt);
                    break;
                case DatabasePlatform.MSAccess:
                    sqlScript = MsAccessTableCreateStatement(dt);
                    break;
                case DatabasePlatform.SQLServerCE35:
                    sqlScript = SQLServerCETableCreateStatement(dt);
                    break;
                case DatabasePlatform.SQLServerCE40:
                    sqlScript = SQLServerCE_V4TableCreateStatement(dt);
                    break;
                case DatabasePlatform.OracleNative:
                    sqlScript = OracleTableCreateStatement(dt);
                    break;
                case DatabasePlatform.MSOracle:
                    sqlScript = MsOracleTableCreateStatement(dt);
                    break;
                case DatabasePlatform.DB2:
                    sqlScript = DB2TableCreateStatement(dt);
                    break;
                case DatabasePlatform.Sybase:
                    sqlScript = SybaseTableCreateStatement(dt);
                    break;
                case DatabasePlatform.SQLAnywhere:
                    sqlScript = SQLAnywhereTableCreateStatement(dt);
                    break;
                case DatabasePlatform.SQLAnywhereUltraLite:
                    sqlScript = SQLAnywhereUltraLiteTableCreateStatement(dt);
                    break;
                case DatabasePlatform.MySQL:
                    sqlScript = MySQLTableCreateStatement(dt);
                    break;
                case DatabasePlatform.Informix:
                    sqlScript = InformixTableCreateStatement(dt);
                    break;
                case DatabasePlatform.OLEDB:
                    sqlScript = GenericTableCreateStatementRestricted(dt, connectionString);
                    break;
                case DatabasePlatform.ODBC:
                    sqlScript = GenericTableCreateStatementRestricted(dt, connectionString);
                    break;
                case DatabasePlatform.Unknown:
                    NotYetImplemented(DatabasePlatform.Unknown);
                    break;
                default:
                    break;
            }


            return sqlScript;
        }

        private void NotYetImplemented(DatabasePlatform dbType)
        {
            _msg.Length = 0;
            _msg.Append("Table create statement builder for ");
            _msg.Append(dbType.ToString());
            _msg.Append(" not yet implemented.");
            throw new DataException(_msg.ToString());
        }


        private string SQLServerTableCreateStatement(DataTable dt)
        {
            StringBuilder sql = new StringBuilder();
            string tableName = dt.TableName.Length > 0 ? dt.TableName : "Table01";
            string colName = string.Empty;
            string colDataType = string.Empty;
            string colLength = string.Empty;
            bool allowNull = false;
            int colInx = 0;
            int maxColInx = dt.Columns.Count - 1;
            int maxLen = 255;

            sql.Length = 0;
            sql.Append("create table ");
            sql.Append(tableName);
            sql.Append("\r\n");
            sql.Append("(\r\n");
            //build columns
            for (colInx = 0; colInx <= maxColInx; colInx++)
            {
                colName = dt.Columns[colInx].ColumnName;
                if (dt.Columns[colInx].AllowDBNull)
                    allowNull = true;
                else
                    allowNull = false;

                switch (dt.Columns[colInx].DataType.FullName)
                {
                    case "System.String":
                        maxLen = dt.Columns[colInx].MaxLength;
                        if (maxLen < 1)
                            maxLen = 255;
                        if (maxLen <= 4000)
                        {
                            colDataType = "nvarchar";
                            colLength = maxLen.ToString();
                        }
                        else
                        {
                            colDataType = "nvarchar";
                            colLength = "max";
                        }
                        break;
                    case "System.Int32":
                        colDataType = "integer";
                        colLength = string.Empty;
                        break;
                    case "System.UInt32":
                        colDataType = "bigint";
                        colLength = string.Empty;
                        break;
                    case "System.Int64":
                        colDataType = "bigint";
                        colLength = string.Empty;
                        break;
                    case "System.UInt64":
                        colDataType = "float";
                        colLength = string.Empty;
                        break;
                    case "System.Int16":
                        colDataType = "smallint";
                        colLength = string.Empty;
                        break;
                    case "System.UInt16":
                        colDataType = "integer";
                        colLength = string.Empty;
                        break;
                    case "System.Double":
                        colDataType = "float";
                        colLength = string.Empty;
                        break;
                    case "System.Single":
                        colDataType = "real";
                        colLength = string.Empty;
                        break;
                    case "System.Decimal":
                        colDataType = "decimal";
                        colLength = string.Empty;
                        break;
                    case "System.DateTime":
                        colDataType = "datetime";
                        colLength = string.Empty;
                        break;
                    case "System.Guid":
                        colDataType = "uniqueidentifier";
                        colLength = string.Empty;
                        break;
                    case "System.Boolean":
                        colDataType = "bit";
                        colLength = string.Empty;
                        break;
                    case "System.Char":
                        colDataType = "nchar";
                        colLength = "1";
                        break;
                    case "System.Char[]":
                        colDataType = "nvarchar";
                        colLength = "max";
                        break;
                    case "System.Byte":
                        colDataType = "tinyint";
                        colLength = string.Empty;
                        break;
                    case "System.Byte[]":
                        colDataType = "varbinary";
                        colLength = "max";
                        break;
                    case "System.SByte":
                        colDataType = "smallint";
                        colLength = string.Empty;
                        break;
                    case "System.Object":
                        colDataType = "image";
                        colLength = string.Empty;
                        break;
                    default:
                        colDataType = "ntext";
                        colLength = string.Empty;
                        break;
                }
                sql.Append("\t[");
                sql.Append(colName);
                sql.Append("] ");
                sql.Append(colDataType);
                if (colLength.Length > 0)
                {
                    sql.Append("(");
                    sql.Append(colLength);
                    sql.Append(")");
                }
                if (allowNull)
                    sql.Append(" null");
                else
                    sql.Append(" not null");
                if (colInx < maxColInx)
                    sql.Append(",");
                sql.Append("\r\n");
            }
            sql.Append(")\r\n");
            sql.Append(";");

            return sql.ToString();
        }

        private string MsAccessTableCreateStatement(DataTable dt)
        {
            StringBuilder sql = new StringBuilder();
            string tableName = dt.TableName.Length > 0 ? dt.TableName : "Table01";
            string colName = string.Empty;
            string colDataType = string.Empty;
            string colLength = string.Empty;
            bool allowNull = false;
            int colInx = 0;
            int maxColInx = dt.Columns.Count - 1;

            sql.Length = 0;
            sql.Append("create table ");
            sql.Append(tableName);
            sql.Append("\r\n");
            sql.Append("(\r\n");
            sql.Append("\t'/* You must use ADODB and ADOX routines to create tables in Access. Use the PFMsAccess or the PFDatabase object to create an Access table. */',\r\n");
            //build columns
            for (colInx = 0; colInx <= maxColInx; colInx++)
            {
                colName = dt.Columns[colInx].ColumnName;
                if (dt.Columns[colInx].AllowDBNull)
                    allowNull = true;
                else
                    allowNull = false;

                switch (dt.Columns[colInx].DataType.FullName)
                {
                    case "System.String":
                        int maxLen = dt.Columns[colInx].MaxLength;
                        if (maxLen < 1)
                            maxLen = 255;
                        if (maxLen <= 255)
                        {
                            colDataType = "VarChar";
                            colLength = maxLen.ToString();
                        }
                        else
                        {
                            colDataType = "LongText";
                            colLength = string.Empty;
                        }
                        break;
                    case "System.Int32":
                        colDataType = "Long";
                        colLength = string.Empty;
                        break;
                    case "System.UInt32":
                        colDataType = "Double";
                        colLength = string.Empty;
                        break;
                    case "System.Int64":
                        colDataType = "Double";
                        colLength = string.Empty;
                        break;
                    case "System.UInt64":
                        colDataType = "Double";
                        colLength = string.Empty;
                        break;
                    case "System.Int16":
                        colDataType = "Short";
                        colLength = string.Empty;
                        break;
                    case "System.UInt16":
                        colDataType = "Long";
                        colLength = string.Empty;
                        break;
                    case "System.Double":
                        colDataType = "Double";
                        colLength = string.Empty;
                        break;
                    case "System.Single":
                        colDataType = "Double";
                        colLength = string.Empty;
                        break;
                    case "System.Decimal":
                        colDataType = "Decimal";
                        colLength = string.Empty;
                        break;
                    case "System.DateTime":
                        colDataType = "DateTime";
                        colLength = string.Empty;
                        break;
                    case "System.Guid":
                        colDataType = "GUID";
                        colLength = string.Empty;
                        break;
                    case "System.Boolean":
                        colDataType = "Bit";
                        colLength = string.Empty;
                        break;
                    case "System.Char":
                        colDataType = "VarChar";
                        colLength = "1";
                        break;
                    case "System.Char[]":
                        colDataType = "LongText";
                        colLength = string.Empty;
                        break;
                    case "System.Byte":
                        colDataType = "Byte";
                        colLength = string.Empty;
                        break;
                    case "System.Byte[]":
                        colDataType = "LongBinary";
                        colLength = string.Empty;
                        break;
                    case "System.SByte":
                        colDataType = "Short";
                        colLength = string.Empty;
                        break;
                    case "System.Object":
                        colDataType = "LongBinary";
                        colLength = string.Empty;
                        break;
                    default:
                        colDataType = "LongBinary";
                        colLength = string.Empty;
                        break;
                }
                if (colName.Contains(" "))
                {
                    sql.Append("\t\"");
                    sql.Append(colName);
                    sql.Append("\" ");
                }
                else
                {
                    sql.Append("\t");
                    sql.Append(colName);
                    sql.Append(" ");
                }
                sql.Append(colDataType);
                if (colLength.Length > 0)
                {
                    sql.Append("(");
                    sql.Append(colLength);
                    sql.Append(")");
                }
                if (allowNull)
                    sql.Append(" null");
                else
                    sql.Append(" not null");
                if (colInx < maxColInx)
                    sql.Append(",");
                sql.Append("\r\n");
            }
            sql.Append(")\r\n");
            sql.Append(";");

            return sql.ToString();

            //if (dt.TableName.Length > 0)
            //{
            //    _msg.Length = 0;
            //    _msg.Append("You must use ADODB and ADOX routines to create tables in Access. Use the PFMsAccess or the PFDatabase object to create an Access table.");
            //    throw new DataException(_msg.ToString());
            //}
            //return string.Empty;
        }

        private string SQLServerCETableCreateStatement(DataTable dt)
        {
            StringBuilder sql = new StringBuilder();
            string tableName = dt.TableName.Length > 0 ? dt.TableName : "Table01";
            string colName = string.Empty;
            string colDataType = string.Empty;
            string colLength = string.Empty;
            bool allowNull = false;
            int colInx = 0;
            int maxColInx = dt.Columns.Count - 1;

            sql.Length = 0;
            sql.Append("create table ");
            sql.Append(tableName);
            sql.Append("\r\n");
            sql.Append("(\r\n");
            //build columns
            for (colInx = 0; colInx <= maxColInx; colInx++)
            {
                colName = dt.Columns[colInx].ColumnName;
                if (dt.Columns[colInx].AllowDBNull)
                    allowNull = true;
                else
                    allowNull = false;

                switch (dt.Columns[colInx].DataType.FullName)
                {
                    case "System.String":
                        int maxLen = dt.Columns[colInx].MaxLength;
                        if (maxLen < 1)
                            maxLen = 255;
                        if (maxLen <= 4000)
                        {
                            colDataType = "nvarchar";
                            colLength = maxLen.ToString();
                        }
                        else
                        {
                            colDataType = "ntext";
                            colLength = string.Empty;
                        }
                        break;
                    case "System.Int32":
                        colDataType = "integer";
                        colLength = string.Empty;
                        break;
                    case "System.UInt32":
                        colDataType = "bigint";
                        colLength = string.Empty;
                        break;
                    case "System.Int64":
                        colDataType = "bigint";
                        colLength = string.Empty;
                        break;
                    case "System.UInt64":
                        colDataType = "float";
                        colLength = string.Empty;
                        break;
                    case "System.Int16":
                        colDataType = "smallint";
                        colLength = string.Empty;
                        break;
                    case "System.UInt16":
                        colDataType = "integer";
                        colLength = string.Empty;
                        break;
                    case "System.Double":
                        colDataType = "float";
                        colLength = string.Empty;
                        break;
                    case "System.Single":
                        colDataType = "real";
                        colLength = string.Empty;
                        break;
                    case "System.Decimal":
                        colDataType = "decimal";
                        colLength = string.Empty;
                        break;
                    case "System.DateTime":
                        colDataType = "datetime";
                        colLength = string.Empty;
                        break;
                    case "System.Guid":
                        colDataType = "uniqueidentifier";
                        colLength = string.Empty;
                        break;
                    case "System.Boolean":
                        colDataType = "bit";
                        colLength = string.Empty;
                        break;
                    case "System.Char":
                        colDataType = "nchar";
                        colLength = "1";
                        break;
                    case "System.Char[]":
                        colDataType = "image";
                        colLength = string.Empty;
                        break;
                    case "System.Byte":
                        colDataType = "tinyint";
                        colLength = string.Empty;
                        break;
                    case "System.Byte[]":
                        colDataType = "image";
                        colLength = string.Empty;
                        break;
                    case "System.SByte":
                        colDataType = "smallint";
                        colLength = string.Empty;
                        break;
                    case "System.Object":
                        colDataType = "image";
                        colLength = string.Empty;
                        break;
                    default:
                        colDataType = "ntext";
                        colLength = string.Empty;
                        break;
                }
                sql.Append("\t[");
                sql.Append(colName);
                sql.Append("] ");
                sql.Append(colDataType);
                if (colLength.Length > 0)
                {
                    sql.Append("(");
                    sql.Append(colLength);
                    sql.Append(")");
                }
                if (allowNull)
                    sql.Append(" null");
                else
                    sql.Append(" not null");
                if (colInx < maxColInx)
                    sql.Append(",");
                sql.Append("\r\n");
            }
            sql.Append(")\r\n");
            sql.Append(";");

            return sql.ToString();
        }


        private string SQLServerCE_V4TableCreateStatement(DataTable dt)
        {
            StringBuilder sql = new StringBuilder();
            string tableName = dt.TableName.Length > 0 ? dt.TableName : "Table01";
            string colName = string.Empty;
            string colDataType = string.Empty;
            string colLength = string.Empty;
            bool allowNull = false;
            int colInx = 0;
            int maxColInx = dt.Columns.Count - 1;

            sql.Length = 0;
            sql.Append("create table ");
            sql.Append(tableName);
            sql.Append("\r\n");
            sql.Append("(\r\n");
            //build columns
            for (colInx = 0; colInx <= maxColInx; colInx++)
            {
                colName = dt.Columns[colInx].ColumnName;
                if (dt.Columns[colInx].AllowDBNull)
                    allowNull = true;
                else
                    allowNull = false;

                switch (dt.Columns[colInx].DataType.FullName)
                {
                    case "System.String":
                        int maxLen = dt.Columns[colInx].MaxLength;
                        if (maxLen < 1)
                            maxLen = 255;
                        if (maxLen <= 4000)
                        {
                            colDataType = "nvarchar";
                            colLength = maxLen.ToString();
                        }
                        else
                        {
                            colDataType = "ntext";
                            colLength = string.Empty;
                        }
                        break;
                    case "System.Int32":
                        colDataType = "integer";
                        colLength = string.Empty;
                        break;
                    case "System.UInt32":
                        colDataType = "bigint";
                        colLength = string.Empty;
                        break;
                    case "System.Int64":
                        colDataType = "bigint";
                        colLength = string.Empty;
                        break;
                    case "System.UInt64":
                        colDataType = "float";
                        colLength = string.Empty;
                        break;
                    case "System.Int16":
                        colDataType = "smallint";
                        colLength = string.Empty;
                        break;
                    case "System.UInt16":
                        colDataType = "integer";
                        colLength = string.Empty;
                        break;
                    case "System.Double":
                        colDataType = "float";
                        colLength = string.Empty;
                        break;
                    case "System.Single":
                        colDataType = "real";
                        colLength = string.Empty;
                        break;
                    case "System.Decimal":
                        colDataType = "decimal";
                        colLength = string.Empty;
                        break;
                    case "System.DateTime":
                        colDataType = "datetime";
                        colLength = string.Empty;
                        break;
                    case "System.Guid":
                        colDataType = "uniqueidentifier";
                        colLength = string.Empty;
                        break;
                    case "System.Boolean":
                        colDataType = "bit";
                        colLength = string.Empty;
                        break;
                    case "System.Char":
                        colDataType = "nchar";
                        colLength = "1";
                        break;
                    case "System.Char[]":
                        colDataType = "image";
                        colLength = string.Empty;
                        break;
                    case "System.Byte":
                        colDataType = "tinyint";
                        colLength = string.Empty;
                        break;
                    case "System.Byte[]":
                        colDataType = "image";
                        colLength = string.Empty;
                        break;
                    case "System.SByte":
                        colDataType = "smallint";
                        colLength = string.Empty;
                        break;
                    case "System.Object":
                        colDataType = "image";
                        colLength = string.Empty;
                        break;
                    default:
                        colDataType = "ntext";
                        colLength = string.Empty;
                        break;
                }
                sql.Append("\t[");
                sql.Append(colName);
                sql.Append("] ");
                sql.Append(colDataType);
                if (colLength.Length > 0)
                {
                    sql.Append("(");
                    sql.Append(colLength);
                    sql.Append(")");
                }
                if (allowNull)
                    sql.Append(" null");
                else
                    sql.Append(" not null");
                if (colInx < maxColInx)
                    sql.Append(",");
                sql.Append("\r\n");
            }
            sql.Append(")\r\n");
            sql.Append(";");

            return sql.ToString();
        }

        private string OracleTableCreateStatement(DataTable dt)
        {
            StringBuilder sql = new StringBuilder();
            string tableName = dt.TableName.Length > 0 ? dt.TableName : "Table01";
            string colName = string.Empty;
            string colDataType = string.Empty;
            string colLength = string.Empty;
            bool allowNull = false;
            int colInx = 0;
            int maxColInx = dt.Columns.Count - 1;

            sql.Length = 0;
            sql.Append("create table ");
            sql.Append(tableName);
            sql.Append(" ");
            sql.Append("(\"");      //quoted identifier added 5/15
            //build columns
            for (colInx = 0; colInx <= maxColInx; colInx++)
            {
                colName = dt.Columns[colInx].ColumnName;
                if (colName.Length > 30)
                {
                    colName = BuildShortColName(colName, colInx);
                }
                if (dt.Columns[colInx].AllowDBNull)
                    allowNull = true;
                else
                    allowNull = false;

                switch (dt.Columns[colInx].DataType.FullName)
                {
                    case "System.String":
                        int maxLen = dt.Columns[colInx].MaxLength;
                        if (maxLen < 1)
                            maxLen = 255;
                        if (maxLen <= 4000)
                        {
                            colDataType = "NVARCHAR2";
                            colLength = maxLen.ToString();
                        }
                        else
                        {
                            colDataType = "NCLOB";
                            colLength = string.Empty;
                        }
                        break;
                    case "System.Int32":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.UInt32":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.Int64":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.UInt64":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.Int16":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.UInt16":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.Double":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.Single":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.Decimal":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.DateTime":
                        colDataType = "DATE";
                        colLength = string.Empty;
                        break;
                    case "System.Guid":
                        colDataType = "RAW";
                        colLength = "16";
                        break;
                    case "System.Boolean":
                        //colDataType = "NCHAR";
                        //colLength = "1";
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.Char":
                        colDataType = "NCHAR";
                        colLength = "1";
                        break;
                    case "System.Byte":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.SByte":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.Object":
                        colDataType = "BLOB";
                        colLength = string.Empty;
                        break;
                    default:
                        colDataType = "NCLOB";
                        colLength = string.Empty;
                        break;
                }
                if (colInx > 0)
                    sql.Append(" \"");     //added quoted identifiers (5/15)
                sql.Append(colName);
                sql.Append("\" ");
                sql.Append(colDataType);   //added quoted identifiers
                if (colLength.Length > 0)
                {
                    sql.Append("(");
                    sql.Append(colLength);
                    sql.Append(")");
                }
                if (allowNull)
                    sql.Append(" null");
                else
                    sql.Append(" not null");
                if (colInx < maxColInx)
                {
                    sql.Append(",");
                }
                sql.Append("\r\n");
            }
            sql.Append(")");

            return sql.ToString();
        }

        private string BuildShortColName(string colName, int colInx)
        {
            return colName.Substring(0, 20) + "_Field" + colInx.ToString("0000");
        }

        private string MsOracleTableCreateStatement(DataTable dt)
        {
            StringBuilder sql = new StringBuilder();
            string tableName = dt.TableName.Length > 0 ? dt.TableName : "Table01";
            string colName = string.Empty;
            string colDataType = string.Empty;
            string colLength = string.Empty;
            bool allowNull = false;
            int colInx = 0;
            int maxColInx = dt.Columns.Count - 1;

            sql.Length = 0;
            sql.Append("create table ");
            sql.Append(tableName);
            sql.Append(" ");
            sql.Append("(\"");          //quoted identifier added 5/15
            //build columns
            for (colInx = 0; colInx <= maxColInx; colInx++)
            {
                colName = dt.Columns[colInx].ColumnName;
                if (colName.Length > 30)
                {
                    colName = BuildShortColName(colName, colInx);
                }
                if (dt.Columns[colInx].AllowDBNull)
                    allowNull = true;
                else
                    allowNull = false;

                switch (dt.Columns[colInx].DataType.FullName)
                {
                    case "System.String":
                        int maxLen = dt.Columns[colInx].MaxLength;
                        if (maxLen < 1)
                            maxLen = 255;
                        if (maxLen <= 4000)
                        {
                            colDataType = "NVARCHAR2";
                            colLength = maxLen.ToString();
                        }
                        else
                        {
                            colDataType = "NCLOB";
                            colLength = string.Empty;
                        }
                        break;
                    case "System.Int32":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.UInt32":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.Int64":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.UInt64":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.Int16":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.UInt16":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.Double":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.Single":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.Decimal":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.DateTime":
                        colDataType = "DATE";
                        colLength = string.Empty;
                        break;
                    case "System.Guid":
                        //MsOracle provider does not handle guids correctly; attempt to use this datatype will blow up a data load
                        //colDataType = "RAW";
                        //colLength = "16";
                        colDataType = "NCHAR";
                        colLength = "36";
                        break;
                    case "System.Boolean":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.Char":
                        colDataType = "NCHAR";
                        colLength = "1";
                        break;
                    case "System.Char[]":
                        colDataType = "NCLOB";
                        colLength = string.Empty;
                        break;
                    case "System.Byte":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.Byte[]":
                        colDataType = "BLOB";
                        colLength = string.Empty;
                        break;
                    case "System.SByte":
                        colDataType = "NUMBER";
                        colLength = string.Empty;
                        break;
                    case "System.Object":
                        colDataType = "BLOB";
                        colLength = string.Empty;
                        break;
                    default:
                        colDataType = "NCLOB";
                        colLength = string.Empty;
                        break;
                }
                if (colInx > 0)
                    sql.Append(" \"");      //added quoted identifiers (5/15)
                sql.Append(colName);
                sql.Append("\" ");          //added quoted identifiers
                sql.Append(colDataType);
                if (colLength.Length > 0)
                {
                    sql.Append("(");
                    sql.Append(colLength);
                    sql.Append(")");
                }
                if (allowNull)
                    sql.Append(" null");
                else
                    sql.Append(" not null");
                if (colInx < maxColInx)
                {
                    sql.Append(",");
                }
                sql.Append("\r\n");
            }
            sql.Append(")");

            return sql.ToString();
        }

        private string DB2TableCreateStatement(DataTable dt)
        {
            StringBuilder sql = new StringBuilder();
            string tableName = dt.TableName.Length > 0 ? dt.TableName : "Table01";
            string colName = string.Empty;
            string colDataType = string.Empty;
            string colLength = string.Empty;
            bool allowNull = false;
            int colInx = 0;
            int maxColInx = dt.Columns.Count - 1;

            sql.Length = 0;
            sql.Append("create table ");
            sql.Append(tableName);
            sql.Append("\r\n");
            sql.Append("(\r\n");
            //build columns
            for (colInx = 0; colInx <= maxColInx; colInx++)
            {
                colName = dt.Columns[colInx].ColumnName;
                if (dt.Columns[colInx].AllowDBNull)
                    allowNull = true;
                else
                    allowNull = false;

                switch (dt.Columns[colInx].DataType.FullName)
                {
                    case "System.String":
                        int maxLen = dt.Columns[colInx].MaxLength;
                        if (maxLen < 1)
                            maxLen = 255;
                        if (maxLen <= 16352)
                        {
                            colDataType = "VARGRAPHIC";
                            colLength = maxLen.ToString();
                        }
                        else
                        {
                            colDataType = "DBCLOB";
                            colLength = string.Empty;
                        }
                        break;
                    case "System.Int32":
                        colDataType = "INTEGER";
                        colLength = string.Empty;
                        break;
                    case "System.UInt32":
                        colDataType = "BIGINT";
                        colLength = string.Empty;
                        break;
                    case "System.Int64":
                        colDataType = "BIGINT";
                        colLength = string.Empty;
                        break;
                    case "System.UInt64":
                        colDataType = "DOUBLE";
                        colLength = string.Empty;
                        break;
                    case "System.Int16":
                        colDataType = "SMALLINT";
                        colLength = string.Empty;
                        break;
                    case "System.UInt16":
                        colDataType = "INTEGER";
                        colLength = string.Empty;
                        break;
                    case "System.Double":
                        colDataType = "DOUBLE";
                        colLength = string.Empty;
                        break;
                    case "System.Single":
                        colDataType = "FLOAT";
                        colLength = string.Empty;
                        break;
                    case "System.Decimal":
                        colDataType = "DECIMAL(31,7)";
                        colLength = string.Empty;
                        break;
                    case "System.DateTime":
                        colDataType = "TIMESTAMP";
                        colLength = string.Empty;
                        break;
                    case "System.Guid":
                        //colDataType = "CHAR(13) FOR BIT Data";
                        //colLength = string.Empty;
                        colDataType = "GRAPHIC";
                        colLength = "36";
                        break;
                    case "System.Boolean":
                        //colDataType = "CHAR(1) FOR BIT Data";
                        //colLength = string.Empty;
                        colDataType = "SMALLINT";
                        colLength = string.Empty;
                        break;
                    case "System.Char":
                        //colDataType = "GRAPHIC";
                        colDataType = "CHAR";
                        colLength = "1";
                        break;
                    case "System.Char[]":
                        colDataType = "DBCLOB";
                        colLength = string.Empty;
                        break;
                    case "System.Byte":
                        //colDataType = "CHAR(1) FOR BIT Data";
                        //colLength = string.Empty;
                        //colDataType = "SMALLINT";  //CAUSES ERROR
                        //colLength = string.Empty;
                        colDataType = "GRAPHIC";
                        colLength = "1";
                        break;
                    case "System.Byte[]":
                        colDataType = "BLOB";
                        colLength = string.Empty;
                        break;
                    case "System.SByte":
                        //colDataType = "SMALLINT";  //CAUSES ERROR
                        //colLength = string.Empty;
                        colDataType = "GRAPHIC";
                        colLength = "1";
                        break;
                    case "System.Object":
                        colDataType = "BLOB";
                        colLength = string.Empty;
                        break;
                    default:
                        colDataType = "DBCLOB";
                        colLength = string.Empty;
                        break;
                }
                sql.Append("\t");
                sql.Append(colName);
                sql.Append(" ");
                sql.Append(colDataType);
                if (colLength.Length > 0)
                {
                    sql.Append("(");
                    sql.Append(colLength);
                    sql.Append(")");
                }
                if (colDataType.Contains("FOR BIT Data") == false)
                {
                    if (allowNull)
                        sql.Append(" null");
                    else
                        sql.Append(" not null");
                }
                else
                {
                    sql.Append(string.Empty);
                }
                if (colInx < maxColInx)
                    sql.Append(",");
                sql.Append("\r\n");
            }
            sql.Append(")\r\n");
            sql.Append(";");

            return sql.ToString();
        }

        private string InformixTableCreateStatement(DataTable dt)
        {
            StringBuilder sql = new StringBuilder();
            string tableName = dt.TableName.Length > 0 ? dt.TableName : "Table01";
            string colName = string.Empty;
            string colDataType = string.Empty;
            string colLength = string.Empty;
            bool allowNull = false;
            int colInx = 0;
            int maxColInx = dt.Columns.Count - 1;

            sql.Length = 0;
            sql.Append("create table ");
            sql.Append(tableName);
            sql.Append("\r\n");
            sql.Append("(\r\n");
            //build columns
            for (colInx = 0; colInx <= maxColInx; colInx++)
            {
                colName = dt.Columns[colInx].ColumnName;
                if (dt.Columns[colInx].AllowDBNull)
                    allowNull = true;
                else
                    allowNull = false;

                switch (dt.Columns[colInx].DataType.FullName)
                {
                    case "System.String":
                        int maxLen = dt.Columns[colInx].MaxLength;
                        if (maxLen < 1)
                            maxLen = 255;
                        if (maxLen <= 255)
                        {
                            colDataType = "NVARCHAR";
                            colLength = maxLen.ToString();
                        }
                        else if (maxLen <= 32767)
                        {
                            colDataType = "LVARCHAR";
                            colLength = maxLen.ToString();
                        }
                        else
                        {
                            colDataType = "CLOB";
                            colLength = string.Empty;
                        }
                        break;
                    case "System.Int32":
                        colDataType = "INTEGER";
                        colLength = string.Empty;
                        break;
                    case "System.UInt32":
                        colDataType = "BIGINT";
                        colLength = string.Empty;
                        break;
                    case "System.Int64":
                        colDataType = "BIGINT";
                        colLength = string.Empty;
                        break;
                    case "System.UInt64":
                        colDataType = "DOUBLE PRECISION";
                        colLength = string.Empty;
                        break;
                    case "System.Int16":
                        colDataType = "SMALLINT";
                        colLength = string.Empty;
                        break;
                    case "System.UInt16":
                        colDataType = "INTEGER";
                        colLength = string.Empty;
                        break;
                    case "System.Double":
                        colDataType = "DOUBLE PRECISION";
                        colLength = string.Empty;
                        break;
                    case "System.Single":
                        colDataType = "DOUBLE PRECISION";
                        colLength = string.Empty;
                        break;
                    case "System.Decimal":
                        colDataType = "DECIMAL(31,7)";
                        colLength = string.Empty;
                        break;
                    case "System.DateTime":
                        colDataType = "DATETIME YEAR TO FRACTION";
                        colLength = string.Empty;
                        break;
                    case "System.Guid":
                        //colDataType = "CHAR(13) FOR BIT Data";
                        //colLength = string.Empty;
                        colDataType = "CHAR";
                        colLength = "36";
                        break;
                    case "System.Boolean":
                        //colDataType = "CHAR(1) FOR BIT Data";
                        //colLength = string.Empty;
                        colDataType = "SMALLINT";
                        colLength = string.Empty;
                        break;
                    case "System.Char":
                        colDataType = "CHAR";
                        colLength = "1";
                        break;
                    case "System.Char[]":
                        colDataType = "CLOB";
                        colLength = string.Empty;
                        break;
                    case "System.Byte":
                        //colDataType = "CHAR(1) FOR BIT Data";
                        //colLength = string.Empty;
                        colDataType = "CHAR";
                        colLength = "1";
                        break;
                    case "System.Byte[]":
                        colDataType = "BLOB";
                        colLength = string.Empty;
                        break;
                    case "System.SByte":
                        //colDataType = "SMALLINT";
                        //colLength = string.Empty;
                        colDataType = "CHAR";
                        colLength = "1";
                        break;
                    case "System.Object":
                        colDataType = "BLOB";
                        colLength = string.Empty;
                        break;
                    default:
                        colDataType = "BLOB";
                        colLength = string.Empty;
                        break;
                }
                sql.Append("\t");
                sql.Append(colName);
                sql.Append(" ");
                sql.Append(colDataType);
                if (colLength.Length > 0)
                {
                    sql.Append("(");
                    sql.Append(colLength);
                    sql.Append(")");
                }
                if (colDataType.Contains("FOR BIT Data") == false)
                {
                    if (allowNull)
                        sql.Append(" null");
                    else
                        sql.Append(" not null");
                }
                else
                {
                    sql.Append(string.Empty);
                }
                if (colInx < maxColInx)
                    sql.Append(",");
                sql.Append("\r\n");
            }
            sql.Append(")\r\n");
            sql.Append(";");

            return sql.ToString();
        }

        private string MySQLTableCreateStatement(DataTable dt)
        {
            StringBuilder sql = new StringBuilder();
            string tableName = dt.TableName.Length > 0 ? dt.TableName : "Table01";
            string colName = string.Empty;
            string colDataType = string.Empty;
            string colLength = string.Empty;
            bool allowNull = false;
            int colInx = 0;
            int maxColInx = dt.Columns.Count - 1;

            sql.Length = 0;
            sql.Append("create table ");
            sql.Append(tableName);
            sql.Append("\r\n");
            sql.Append("(\r\n");
            //build columns
            for (colInx = 0; colInx <= maxColInx; colInx++)
            {
                colName = dt.Columns[colInx].ColumnName;
                if (dt.Columns[colInx].AllowDBNull)
                    allowNull = true;
                else
                    allowNull = false;

                switch (dt.Columns[colInx].DataType.FullName)
                {
                    case "System.String":
                        int maxLen = dt.Columns[colInx].MaxLength;
                        if (maxLen < 1)
                            maxLen = 255;
                        if (maxLen <= 65000)
                        {
                            colDataType = "TEXT";
                            colLength = maxLen.ToString();
                        }
                        else
                        {
                            colDataType = "LONGTEXT";
                            colLength = string.Empty;
                        }
                        break;
                    case "System.Int32":
                        colDataType = "INTEGER";
                        colLength = string.Empty;
                        break;
                    case "System.UInt32":
                        colDataType = "BIGINT";
                        colLength = string.Empty;
                        break;
                    case "System.Int64":
                        colDataType = "BIGINT";
                        colLength = string.Empty;
                        break;
                    case "System.UInt64":
                        colDataType = "DOUBLE";
                        colLength = string.Empty;
                        break;
                    case "System.Int16":
                        colDataType = "SMALLINT";
                        colLength = string.Empty;
                        break;
                    case "System.UInt16":
                        colDataType = "INTEGER";
                        colLength = string.Empty;
                        break;
                    case "System.Double":
                        colDataType = "DOUBLE";
                        colLength = string.Empty;
                        break;
                    case "System.Single":
                        colDataType = "FLOAT";
                        colLength = string.Empty;
                        break;
                    case "System.Decimal":
                        colDataType = "DECIMAL";
                        colLength = string.Empty;
                        break;
                    case "System.DateTime":
                        colDataType = "DATETIME";
                        colLength = string.Empty;
                        break;
                    case "System.Guid":
                        colDataType = "TEXT";
                        colLength = "36";
                        break;
                    case "System.Boolean":
                        colDataType = "BOOL";
                        colLength = string.Empty;
                        break;
                    case "System.Char":
                        colDataType = "VARCHAR";
                        colLength = "1";
                        break;
                    case "System.Char[]":
                        colDataType = "LONGBLOB";
                        colLength = string.Empty;
                        break;
                    case "System.Byte":
                        colDataType = "SMALLINT";
                        colLength = string.Empty;
                        break;
                    case "System.Byte[]":
                        colDataType = "LONGBLOB";
                        colLength = string.Empty;
                        break;
                    case "System.SByte":
                        colDataType = "SMALLINT";
                        colLength = string.Empty;
                        break;
                    case "System.Object":
                        colDataType = "LONGBLOB";
                        colLength = string.Empty;
                        break;
                    default:
                        colDataType = "LONGBLOB";
                        colLength = string.Empty;
                        break;
                }
                if (colName.Contains(" "))
                    sql.Append("\t`");
                else
                    sql.Append("\t");
                sql.Append(colName);
                if (colName.Contains(" "))
                    sql.Append("` ");
                else
                    sql.Append(" ");
                sql.Append(colDataType);
                if (colLength.Length > 0)
                {
                    sql.Append("(");
                    sql.Append(colLength);
                    sql.Append(")");
                }
                if (allowNull)
                    sql.Append(" null");
                else
                    sql.Append(" not null");
                if (colInx < maxColInx)
                    sql.Append(",");
                sql.Append("\r\n");
            }
            sql.Append(")\r\n");
            sql.Append(";");

            return sql.ToString();
        }


        private string SybaseTableCreateStatement(DataTable dt)
        {
            StringBuilder sql = new StringBuilder();
            string tableName = dt.TableName.Length > 0 ? dt.TableName : "Table01";
            string colName = string.Empty;
            string colDataType = string.Empty;
            string colLength = string.Empty;
            bool allowNull = false;
            int colInx = 0;
            int maxColInx = dt.Columns.Count - 1;

            sql.Length = 0;
            sql.Append("create table ");
            sql.Append(tableName);
            sql.Append("\r\n");
            sql.Append("(\r\n");
            //build columns
            for (colInx = 0; colInx <= maxColInx; colInx++)
            {
                colName = dt.Columns[colInx].ColumnName;
                if (dt.Columns[colInx].AllowDBNull)
                    allowNull = true;
                else
                    allowNull = false;

                switch (dt.Columns[colInx].DataType.FullName)
                {
                    case "System.String":
                        int maxLen = dt.Columns[colInx].MaxLength;
                        if (maxLen < 1)
                            maxLen = 255;
                        if (maxLen <= 4000)
                        {
                            colDataType = "nvarchar";
                            colLength = maxLen.ToString();
                        }
                        else
                        {
                            colDataType = "text";
                            colLength = string.Empty;
                        }
                        break;
                    case "System.Int32":
                        colDataType = "int";
                        colLength = string.Empty;
                        break;
                    case "System.UInt32":
                        colDataType = "bigint";
                        colLength = string.Empty;
                        break;
                    case "System.Int64":
                        colDataType = "bigint";
                        colLength = string.Empty;
                        break;
                    case "System.UInt64":
                        colDataType = "float";
                        colLength = string.Empty;
                        break;
                    case "System.Int16":
                        colDataType = "smallint";
                        colLength = string.Empty;
                        break;
                    case "System.UInt16":
                        colDataType = "int";
                        colLength = string.Empty;
                        break;
                    case "System.Double":
                        colDataType = "float";
                        colLength = string.Empty;
                        break;
                    case "System.Single":
                        colDataType = "real";
                        colLength = string.Empty;
                        break;
                    case "System.Decimal":
                        colDataType = "float";
                        colLength = string.Empty;
                        break;
                    case "System.DateTime":
                        colDataType = "datetime";
                        colLength = string.Empty;
                        break;
                    case "System.Guid":
                        colDataType = "nchar";
                        colLength = "36";
                        break;
                    case "System.Boolean":
                        colDataType = "bit";
                        colLength = string.Empty;
                        allowNull = false;         //override needed for Sybase bit data type; null not allowed but column definition says it is (wrong)
                        break;
                    case "System.Char":
                        colDataType = "nchar";
                        colLength = "1";
                        break;
                    case "System.Char[]":
                        colDataType = "text";
                        colLength = string.Empty;
                        break;
                    case "System.Byte":
                        colDataType = "tinyint";
                        colLength = string.Empty;
                        break;
                    case "System.Byte[]":
                        colDataType = "image";
                        colLength = string.Empty;
                        break;
                    case "System.SByte":
                        colDataType = "tinyint";
                        colLength = string.Empty;
                        break;
                    case "System.Object":
                        colDataType = "image";
                        colLength = string.Empty;
                        break;
                    default:
                        colDataType = "image";
                        colLength = string.Empty;
                        break;
                }
                sql.Append("\t[");
                sql.Append(colName);
                sql.Append("] ");
                sql.Append(colDataType);
                if (colLength.Length > 0)
                {
                    sql.Append("(");
                    sql.Append(colLength);
                    sql.Append(")");
                }
                if (allowNull)
                    sql.Append(" null");
                else
                    sql.Append(" not null");
                if (colInx < maxColInx)
                    sql.Append(",");
                sql.Append("\r\n");
            }
            sql.Append(")\r\n");
            sql.Append("");

            return sql.ToString();
        }

        private string SQLAnywhereTableCreateStatement(DataTable dt)
        {
            StringBuilder sql = new StringBuilder();
            string tableName = dt.TableName.Length > 0 ? dt.TableName : "Table01";
            string colName = string.Empty;
            string colDataType = string.Empty;
            string colLength = string.Empty;
            bool allowNull = false;
            int colInx = 0;
            int maxColInx = dt.Columns.Count - 1;

            sql.Length = 0;
            sql.Append("create table ");
            sql.Append(tableName);
            sql.Append("\r\n");
            sql.Append("(\r\n");
            //build columns
            for (colInx = 0; colInx <= maxColInx; colInx++)
            {
                colName = dt.Columns[colInx].ColumnName;
                if (dt.Columns[colInx].AllowDBNull)
                    allowNull = true;
                else
                    allowNull = false;

                switch (dt.Columns[colInx].DataType.FullName)
                {
                    case "System.String":
                        int maxLen = dt.Columns[colInx].MaxLength;
                        if (maxLen < 1)
                            maxLen = 255;
                        if (maxLen <= 4000)
                        {
                            colDataType = "nvarchar";
                            colLength = maxLen.ToString();
                        }
                        else if (maxLen <= 8000)
                        {
                            colDataType = "varchar";
                            colLength = maxLen.ToString();
                        }
                        else
                        {
                            colDataType = "long nvarchar";
                            colLength = string.Empty;
                        }
                        break;
                    case "System.Int32":
                        colDataType = "int";
                        colLength = string.Empty;
                        break;
                    case "System.UInt32":
                        colDataType = "bigint";
                        colLength = string.Empty;
                        break;
                    case "System.Int64":
                        colDataType = "bigint";
                        colLength = string.Empty;
                        break;
                    case "System.UInt64":
                        colDataType = "float";
                        colLength = string.Empty;
                        break;
                    case "System.Int16":
                        colDataType = "smallint";
                        colLength = string.Empty;
                        break;
                    case "System.UInt16":
                        colDataType = "int";
                        colLength = string.Empty;
                        break;
                    case "System.Double":
                        colDataType = "double";
                        colLength = string.Empty;
                        break;
                    case "System.Single":
                        colDataType = "float";
                        colLength = string.Empty;
                        break;
                    case "System.Decimal":
                        colDataType = "decimal";
                        colLength = string.Empty;
                        break;
                    case "System.DateTime":
                        colDataType = "datetime";
                        colLength = string.Empty;
                        break;
                    case "System.Guid":
                        colDataType = "uniqueidentifier";
                        colLength = string.Empty;
                        break;
                    case "System.Boolean":
                        colDataType = "bit";
                        colLength = string.Empty;
                        break;
                    case "System.Char":
                        colDataType = "nvarchar";
                        colLength = "1";
                        break;
                    case "System.Char[]":
                        if (dt.Columns[colInx].MaxLength < 1 || dt.Columns[colInx].MaxLength > 32767)
                        {
                            colDataType = "long nvarchar";
                            colLength = string.Empty;
                        }
                        else
                        {
                            colDataType = "nvarchar";
                            colLength = dt.Columns[colInx].MaxLength.ToString();
                        }
                        break;
                    case "System.Byte":
                        colDataType = "tinyint";
                        colLength = string.Empty;
                        break;
                    case "System.Byte[]":
                        if (dt.Columns[colInx].MaxLength < 1 || dt.Columns[colInx].MaxLength > 32767)
                        {
                            colDataType = "long binary";
                            colLength = string.Empty;
                        }
                        else
                        {
                            colDataType = "varbinary";
                            colLength = dt.Columns[colInx].MaxLength.ToString();
                        }
                        break;
                    case "System.SByte":
                        colDataType = "smallint";
                        colLength = string.Empty;
                        break;
                    case "System.Object":
                        colDataType = "long binary";
                        colLength = string.Empty;
                        break;
                    default:
                        colDataType = "long nvarchar";
                        colLength = string.Empty;
                        break;
                }
                sql.Append("\t[");
                sql.Append(colName);
                sql.Append("] ");
                sql.Append(colDataType);
                if (colLength.Length > 0)
                {
                    sql.Append("(");
                    sql.Append(colLength);
                    sql.Append(")");
                }
                if (allowNull)
                    sql.Append(" null");
                else
                    sql.Append(" not null");
                if (colInx < maxColInx)
                    sql.Append(",");
                sql.Append("\r\n");
            }
            sql.Append(")\r\n");
            sql.Append("");

            return sql.ToString();
        }


        private string SQLAnywhereUltraLiteTableCreateStatement(DataTable dt)
        {
            StringBuilder sql = new StringBuilder();
            string tableName = dt.TableName.Length > 0 ? dt.TableName : "Table01";
            string colName = string.Empty;
            string colDataType = string.Empty;
            string colLength = string.Empty;
            bool allowNull = false;
            int colInx = 0;
            int maxColInx = dt.Columns.Count - 1;
            bool createPrimaryKey = true;

            sql.Length = 0;
            sql.Append("create table ");
            sql.Append(tableName);
            sql.Append("\r\n");
            sql.Append("(\r\n");

            //create a primary key column, if necessary
            for (colInx = 0; colInx <= maxColInx; colInx++)
            {
                colName = dt.Columns[colInx].ColumnName;
                if (ColumnIsPrimaryKey(dt, colName))
                {
                    createPrimaryKey = false;
                    break;
                }
            }
            if (createPrimaryKey)
            {
                sql.Append("\tPFTempPK bigint PRIMARY KEY DEFAULT AUTOINCREMENT,\r\n");
            }
            //build columns
            for (colInx = 0; colInx <= maxColInx; colInx++)
            {
                colName = dt.Columns[colInx].ColumnName;
                if (dt.Columns[colInx].AllowDBNull)
                    allowNull = true;
                else
                    allowNull = false;

                switch (dt.Columns[colInx].DataType.FullName)
                {
                    case "System.String":
                        int maxLen = dt.Columns[colInx].MaxLength;
                        if (maxLen < 1)
                            maxLen = 255;
                        if (maxLen <= 32767)
                        {
                            colDataType = "varchar";
                            colLength = maxLen.ToString();
                        }
                        else
                        {
                            colDataType = "long varchar";
                            colLength = string.Empty;
                        }
                        break;
                    case "System.Int32":
                        colDataType = "int";
                        colLength = string.Empty;
                        break;
                    case "System.UInt32":
                        colDataType = "bigint";
                        colLength = string.Empty;
                        break;
                    case "System.Int64":
                        colDataType = "bigint";
                        colLength = string.Empty;
                        break;
                    case "System.UInt64":
                        colDataType = "float";
                        colLength = string.Empty;
                        break;
                    case "System.Int16":
                        colDataType = "smallint";
                        colLength = string.Empty;
                        break;
                    case "System.UInt16":
                        colDataType = "int";
                        colLength = string.Empty;
                        break;
                    case "System.Double":
                        colDataType = "double";
                        colLength = string.Empty;
                        break;
                    case "System.Single":
                        colDataType = "float";
                        colLength = string.Empty;
                        break;
                    case "System.Decimal":
                        colDataType = "decimal";
                        colLength = string.Empty;
                        break;
                    case "System.DateTime":
                        colDataType = "datetime";
                        colLength = string.Empty;
                        break;
                    case "System.Guid":
                        colDataType = "uniqueidentifier";
                        colLength = string.Empty;
                        break;
                    case "System.Boolean":
                        colDataType = "bit";
                        colLength = string.Empty;
                        break;
                    case "System.Char":
                        colDataType = "char";
                        colLength = "1";
                        break;
                    case "System.Char[]":
                        if (dt.Columns[colInx].MaxLength < 1 || dt.Columns[colInx].MaxLength > 32767)
                        {
                            colDataType = "long varchar";
                            colLength = string.Empty;
                        }
                        else
                        {
                            colDataType = "varchar";
                            colLength = dt.Columns[colInx].MaxLength.ToString();
                        }
                        break;
                    case "System.Byte":
                        colDataType = "tinyint";
                        colLength = string.Empty;
                        break;
                    case "System.Byte[]":
                        if (dt.Columns[colInx].MaxLength < 1 || dt.Columns[colInx].MaxLength > 32767)
                        {
                            colDataType = "long binary";
                            colLength = string.Empty;
                        }
                        else
                        {
                            colDataType = "varbinary";
                            colLength = dt.Columns[colInx].MaxLength.ToString();
                        }
                        break;
                    case "System.SByte":
                        colDataType = "smallint";
                        colLength = string.Empty;
                        break;
                    case "System.Object":
                        colDataType = "long binary";
                        colLength = string.Empty;
                        break;
                    default:
                        colDataType = "long varchar";
                        colLength = string.Empty;
                        break;
                }
                sql.Append("\t");
                sql.Append(colName);
                sql.Append(" ");
                sql.Append(colDataType);
                if (colLength.Length > 0)
                {
                    sql.Append("(");
                    sql.Append(colLength);
                    sql.Append(")");
                }
                if (allowNull)
                    sql.Append(" null");
                else
                    sql.Append(" not null");
                
                if(ColumnIsPrimaryKey(dt, colName))
                {
                    sql.Append(" PRIMARY KEY");
                }

                if (colInx < maxColInx)
                    sql.Append(",");
                sql.Append("\r\n");
            }
            sql.Append(")\r\n");
            sql.Append("");

            return sql.ToString();
        }


        /// <summary>
        /// Builds a table create statement for any odbc driver or oledb provider. The routine restricts the table definitions to string, integer, double, and byte array values.
        ///  By converting all values to a small subset of types, the should be a higher chance of matching the driver/provider data type support minimum.
        /// </summary>
        /// <param name="dt">Data table containing the data definition for the table.</param>
        /// <param name="connectionString">Connection string for this particular database connection.</param>
        /// <returns>String containing the create table statement.</returns>
        public string GenericTableCreateStatementRestricted(DataTable dt, string connectionString)
        {
            string tableCreateStatement = string.Empty;
            DataTable dbTable = dt.Clone();

            if (connectionString == null)
            {
                _msg.Length = 0;
                _msg.Append("You must specify a connection string for the GenericTableCreateStatement method.");
                throw new ArgumentNullException(_msg.ToString());
            }

            //GenericModifyDataTableColumnTypes(dt, dbTable);

            tableCreateStatement = BuildGenericTableCreateStatementExt(dbTable, connectionString);

            return tableCreateStatement;
        }

        /*
        private void GenericModifyDataTableColumnTypes(DataTable dt, DataTable dbTable)
        {
            for (int colInx = 0; colInx < dt.Columns.Count; colInx++)
            {
                dbTable.Columns[colInx].DefaultValue = null;
                switch (dt.Columns[colInx].DataType.FullName)
                {
                    case "System.String":
                        int maxLen = dt.Columns[colInx].MaxLength;
                        if (maxLen < 1)
                            maxLen = 255;
                        if (maxLen <= 4000)
                        {
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.String");
                            dbTable.Columns[colInx].MaxLength = maxLen;
                        }
                        else
                        {
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.String");
                            dbTable.Columns[colInx].MaxLength = Convert.ToInt32(UInt16.MaxValue);
                        }
                        dbTable.Columns[colInx].DefaultValue = "This is a revised string";
                        break;
                    case "System.Int32":
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.Int32");
                            dbTable.Columns[colInx].MaxLength = -1;
                            dbTable.Columns[colInx].DefaultValue = Int32.MaxValue;
                            break;
                    case "System.UInt32":
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.Double");
                            dbTable.Columns[colInx].MaxLength = -1;
                            dbTable.Columns[colInx].DefaultValue = Convert.ToDouble(Int32.MaxValue);
                            break;
                    case "System.Int64":
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.Double");
                            dbTable.Columns[colInx].MaxLength = -1;
                            dbTable.Columns[colInx].DefaultValue = Convert.ToDouble(Int32.MaxValue);
                            break;
                    case "System.UInt64":
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.Double");
                            dbTable.Columns[colInx].DefaultValue = Convert.ToDouble(Int32.MaxValue);
                            dbTable.Columns[colInx].MaxLength = -1;
                            break;
                    case "System.Int16":
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.Int32");
                            dbTable.Columns[colInx].MaxLength = -1;
                            dbTable.Columns[colInx].DefaultValue = Int16.MaxValue;
                            break;
                    case "System.UInt16":
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.Int32");
                            dbTable.Columns[colInx].MaxLength = -1;
                            dbTable.Columns[colInx].DefaultValue = Int16.MaxValue;
                            break;
                    case "System.Double":
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.Double");
                            dbTable.Columns[colInx].MaxLength = -1;
                            dbTable.Columns[colInx].DefaultValue = Convert.ToDouble(Int32.MaxValue);
                            break;
                    case "System.Single":
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.Double");
                            dbTable.Columns[colInx].MaxLength = -1;
                            dbTable.Columns[colInx].DefaultValue = Convert.ToDouble(Int32.MaxValue);
                            break;
                    case "System.Decimal":
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.Double");
                            dbTable.Columns[colInx].MaxLength = -1;
                            dbTable.Columns[colInx].DefaultValue = Convert.ToDouble(Int32.MaxValue);
                            break;
                    case "System.DateTime":
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.String");
                            dbTable.Columns[colInx].MaxLength = 60;
                            dbTable.Columns[colInx].DefaultValue = Convert.ToString("6/16/2013 15:30:45");
                          break;
                    case "System.Guid":
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.String");
                            dbTable.Columns[colInx].MaxLength = 36;
                            dbTable.Columns[colInx].DefaultValue = new Guid().ToString();
                            break;
                    case "System.Boolean":
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.String");
                            dbTable.Columns[colInx].MaxLength = 5;
                            dbTable.Columns[colInx].DefaultValue = "true";
                            break;
                    case "System.Char":
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.String");
                            dbTable.Columns[colInx].MaxLength = 1;
                            break;
                    case "System.Char[]":
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.String");
                            dbTable.Columns[colInx].MaxLength = Convert.ToInt32(UInt16.MaxValue);
                            dbTable.Columns[colInx].DefaultValue = "A";
                            break;
                    case "System.Byte":
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.String");
                            dbTable.Columns[colInx].MaxLength = 1;
                            dbTable.Columns[colInx].DefaultValue = "F";
                            break;
                    case "System.Byte[]":
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.Byte[]");
                            dbTable.Columns[colInx].MaxLength = -1;
                            dbTable.Columns[colInx].DefaultValue = new byte[] {0x65, 0x66, 0x67};
                            break;
                    case "System.SByte":
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.Int32");
                            dbTable.Columns[colInx].MaxLength = -1;
                            dbTable.Columns[colInx].DefaultValue = 125;
                            break;
                    case "System.Object":
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.Byte[]");
                            dbTable.Columns[colInx].MaxLength = -1;
                            dbTable.Columns[colInx].DefaultValue = new byte[] { 0x65, 0x66, 0x67, 0x68, 0x69, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x76, 0x77, 0x78, 0x79, 0x80};
                        break;
                    default:
                            dbTable.Columns[colInx].DataType = System.Type.GetType("System.Byte[]");
                            dbTable.Columns[colInx].MaxLength = -1;
                            dbTable.Columns[colInx].DefaultValue = new byte[] { 0x65, 0x66, 0x67, 0x68, 0x69, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x76, 0x77, 0x78, 0x79, 0x80};
                        break;
                }

            }
        }
        */

        /// <summary>
        /// Builds a table create statement for any odbc or oledb driver. The routine tries to match as closely as possible the .NET data type to a database datatype.
        /// </summary>
        /// <param name="dt">Data table containing the data definition for the table.</param>
        /// <param name="connectionString">Connection string for this particular database connection.</param>
        /// <returns>String containing the create table statement.</returns>
        public string GenericTableCreateStatementExt(DataTable dt, string connectionString)
        {
            string tableCreateStatement = string.Empty;

            if (connectionString == null)
            {
                _msg.Length = 0;
                _msg.Append("You must specify a connection string for the GenericTableCreateStatement method.");
                throw new ArgumentNullException(_msg.ToString());
            }

            //first check of connection string contains drivers for well-known databases
            //if so can use above create routines (e.g. for sql server, oracle, db2, etc.

            tableCreateStatement = BuildGenericTableCreateStatementExt(dt, connectionString);

            return tableCreateStatement;
        }

        private string BuildGenericTableCreateStatementExt(DataTable dt, string connectionString)
        {
            StringBuilder sql = new StringBuilder();
            List<DataTypeMapping> dataTypeMappings = GetGenericDataTypeMappingsEx(connectionString);
            bool allowNull = false;
            //bool isSQLServer = false;
            //bool isOracleNative = false;
            //bool isMsOracle = false;
            //bool isSQLAnywhere = false;
            bool isIBMDB2 = false;

            string longestDataType = string.Empty;
            string longestStringDataType = string.Empty;
            string smallestStringDataType = string.Empty;
            string longestIntegerDataType = string.Empty;
            string smallestIntegerDataType = string.Empty;
            string longestFloatingPointDataType = string.Empty;
            string smallestFloatingPointDataType = string.Empty;
            string longestDecimalDataType = string.Empty;
            string smallestDecimalDataType = string.Empty;
            string longestDateTimeDataType = string.Empty;
            string longestByteDataType = string.Empty;
            string smallestByteDataType = string.Empty;
            string longestCharDataType = string.Empty;
            string smallestCharDataType = string.Empty;
            string longestByteArrayDataType = string.Empty;
            string longestCharArrayDataType = string.Empty;
            string longestBooleanDataType = string.Empty;
            string longestGuidDataType = string.Empty;
            string longestObjectDataType = string.Empty;
            long maxDataTypeLength = -1;
            long maxStringLength = -1;
            long minStringLength = 1073741823;
            long maxIntegerLength = -1;
            long minIntegerLength = System.Int64.MaxValue.ToString().Length;
            long maxFloatingPointLength = -1;
            long minFloatingPointLength = System.Int64.MaxValue.ToString().Length; 
            long maxDecimalLength = -1;
            long minDecimalLength = System.Int64.MaxValue.ToString().Length; 
            long maxDateTimeLength = -1;
            long maxByteLength = -1;
            long minByteLength = System.Char.MaxValue.ToString().Length;
            long maxCharLength = -1;
            long minCharLength = System.Char.MaxValue.ToString().Length;
            long maxByteArrayLength = -1;
            long maxCharArrayLength = -1;
            long maxBooleanLength = -1;
            long maxGuidLength = -1;
            long maxObjectLength = -1;


            //if (connectionString.ToUpper().Contains("SQLOLEDB") || connectionString.ToUpper().Contains("{SQL SERVER"))
            //    isSQLServer = true;
            //if (connectionString.ToUpper().Contains("ORAOLEDB") || connectionString.ToUpper().Contains("{ORACLE IN ORACLIENT"))
            //    isOracleNative = true;
            //if (connectionString.ToUpper().Contains("MSDAORA") || connectionString.ToUpper().Contains("{MICROSOFT ODBC FOR ORACLE"))
            //    isMsOracle = true;
            //if (connectionString.ToUpper().Contains("{SQL ANYWHERE"))
            //    isSQLAnywhere = true;
            if (connectionString.ToUpper().Contains("IBMDADB2") || connectionString.ToUpper().Contains("{IBM DB2 ODBC DRIVER"))
                isIBMDB2 = true;

            
            foreach (DataTypeMapping map in dataTypeMappings)
            {
                System.Type typ = PFSystemTypeInfo.ConvertNameToType(map.DotNetDataType);

                if (map.dataTypeCategory == DataCategory.String)
                {
                    if (map.MaxLength > maxStringLength)
                    {
                        maxStringLength = map.MaxLength;
                        longestStringDataType = map.DatabaseDataType;
                    }
                    if (map.MaxLength < minStringLength)
                    {
                        minStringLength = map.MaxLength;
                        smallestStringDataType = map.DatabaseDataType;
                    }
                }
                else if (map.dataTypeCategory == DataCategory.Integer)
                {
                    if (map.MaxLength > maxIntegerLength)
                    {
                        maxIntegerLength = map.MaxLength;
                        longestIntegerDataType = map.DatabaseDataType;
                    }
                    if (map.MaxLength < minIntegerLength)
                    {
                        minIntegerLength = map.MaxLength;
                        smallestIntegerDataType = map.DatabaseDataType;
                    }
                }
                else if (map.dataTypeCategory == DataCategory.FloatingPoint)
                {
                    if (map.MaxLength > maxFloatingPointLength)
                    {
                        maxFloatingPointLength = map.MaxLength;
                        longestFloatingPointDataType = map.DatabaseDataType;
                    }
                    if (map.MaxLength < minFloatingPointLength)
                    {
                        minFloatingPointLength = map.MaxLength;
                        smallestFloatingPointDataType = map.DatabaseDataType;
                    }
                }
                else if (map.dataTypeCategory == DataCategory.Decimal)
                {
                    if (map.MaxLength > maxDecimalLength)
                    {
                        maxDecimalLength = map.MaxLength;
                        longestDecimalDataType = map.DatabaseDataType;
                    }
                    if (map.MaxLength < minDecimalLength)
                    {
                        minDecimalLength = map.MaxLength;
                        smallestDecimalDataType = map.DatabaseDataType;
                    }
                }
                else if (map.dataTypeCategory == DataCategory.DateTime)
                {
                    if (map.MaxLength > maxDateTimeLength)
                    {
                        maxDateTimeLength = map.MaxLength;
                        longestDateTimeDataType = map.DatabaseDataType;
                    }
                }
                else if (map.dataTypeCategory == DataCategory.Byte)
                {
                    if (map.MaxLength > maxByteLength)
                    {
                        maxByteLength = map.MaxLength;
                        longestByteDataType = map.DatabaseDataType;
                    }
                    if (map.MaxLength < minByteLength)
                    {
                        minByteLength = map.MaxLength;
                        smallestByteDataType = map.DatabaseDataType;
                    }
                }
                else if (map.dataTypeCategory == DataCategory.Char)
                {
                    if (map.MaxLength > maxCharLength)
                    {
                        maxCharLength = map.MaxLength;
                        longestCharDataType = map.DatabaseDataType;
                    }
                    if (map.MaxLength < minCharLength)
                    {
                        minCharLength = map.MaxLength;
                        smallestCharDataType = map.DatabaseDataType;
                    }
                }
                else if (map.dataTypeCategory == DataCategory.ByteArray)
                {
                    if (map.MaxLength > maxByteArrayLength)
                    {
                        maxByteArrayLength = map.MaxLength;
                        longestByteArrayDataType = map.DatabaseDataType;
                    }
                }
                else if (map.dataTypeCategory == DataCategory.CharArray)
                {
                    if (map.MaxLength > maxCharArrayLength)
                    {
                        maxCharArrayLength = map.MaxLength;
                        longestCharArrayDataType = map.DatabaseDataType;
                    }
                }
                else if (map.dataTypeCategory == DataCategory.Boolean)
                {
                    if (map.MaxLength > maxBooleanLength)
                    {
                        maxBooleanLength = map.MaxLength;
                        longestBooleanDataType = map.DatabaseDataType;
                    }
                }
                else if (map.dataTypeCategory == DataCategory.Guid)
                {
                    if (map.MaxLength > maxGuidLength)
                    {
                        maxGuidLength = map.MaxLength;
                        longestGuidDataType = map.DatabaseDataType;
                    }
                }
                else //is an object
                {
                    if (map.MaxLength > maxObjectLength)
                    {
                        maxObjectLength = map.MaxLength;
                        longestObjectDataType = map.DatabaseDataType;
                    }
                }
                if (map.MaxLength > maxDataTypeLength)
                {
                    maxDataTypeLength = map.MaxLength;
                    longestDataType = map.DatabaseDataType;
                }
            }

            sql.Length = 0;
            sql.Append("CREATE TABLE ");
            sql.Append(dt.TableName);
            sql.Append("\r\n");

            int maxColInx = dt.Columns.Count - 1;
            for (int i = 0; i <= maxColInx; i++)
            {
                if (dt.Columns[i].AllowDBNull)
                    allowNull = true;
                else
                    allowNull = false;

                if (i == 0)
                    sql.Append("(");
                DataColumn dc = dt.Columns[i];
                long dcColLength = dc.MaxLength;
                if (dcColLength < 1)
                    dcColLength = PFSystemTypeInfo.GetDataTypeMaxLength(dc.DataType.FullName);

                string dotnetDataType = dc.DataType.FullName;
                string databaseDataType = string.Empty;
                long databaseColLength = -1;
                string databaseDataTypeCreateParameters = string.Empty;

                DataCategory dcTypeCat = GetDataCategory(dc.DataType);

                if (dcTypeCat == DataCategory.Boolean
                    && (connectionString.ToLower().Contains("adaptive server")
                        || connectionString.ToLower().Contains("aseoledb")))
                    allowNull = false;
                    

                int maxMapInx = dataTypeMappings.Count - 1;
                for (int mapInx = 0; mapInx <= maxMapInx; mapInx++)
                {
                    if (dataTypeMappings[mapInx].dataTypeCategory == dcTypeCat)
                    {
                        if (databaseColLength == -1
                            && dcColLength <= dataTypeMappings[mapInx].MaxLength)
                        {
                            databaseDataType = dataTypeMappings[mapInx].DatabaseDataType;
                            databaseColLength = dataTypeMappings[mapInx].MaxLength;
                            databaseDataTypeCreateParameters = dataTypeMappings[mapInx].CreateParameters;
                        }
                        else if (dcColLength <= dataTypeMappings[mapInx].MaxLength
                            && dataTypeMappings[mapInx].MaxLength < databaseColLength)
                        {
                            databaseDataType = dataTypeMappings[mapInx].DatabaseDataType;
                            databaseColLength = dataTypeMappings[mapInx].MaxLength;
                            databaseDataTypeCreateParameters = dataTypeMappings[mapInx].CreateParameters;
                        }
                        else
                        {
                            ;
                        }
                    }

                }

                if ((dcTypeCat == DataCategory.Integer || dcTypeCat == DataCategory.FloatingPoint)
                    && dcColLength > databaseColLength)
                {
                    for (int mapInx = 0; mapInx <= maxMapInx; mapInx++)
                    {
                        if (dataTypeMappings[mapInx].dataTypeCategory == DataCategory.FloatingPoint)
                        {
                            if (databaseColLength == -1)
                            {
                                databaseDataType = dataTypeMappings[mapInx].DatabaseDataType;
                                databaseColLength = dataTypeMappings[mapInx].MaxLength;
                            }
                            else if (dcColLength <= dataTypeMappings[mapInx].MaxLength)
                            {
                                databaseDataType = dataTypeMappings[mapInx].DatabaseDataType;
                                databaseColLength = dataTypeMappings[mapInx].MaxLength;
                            }
                            else
                            {
                                ;
                            }
                        }
                    }

                }

                if ((dcTypeCat == DataCategory.Integer || dcTypeCat == DataCategory.FloatingPoint)
                    && dcColLength > databaseColLength)
                {
                    for (int mapInx = 0; mapInx <= maxMapInx; mapInx++)
                    {
                        if (dataTypeMappings[mapInx].dataTypeCategory == DataCategory.Decimal)
                        {
                            if (databaseColLength == -1)
                            {
                                databaseDataType = dataTypeMappings[mapInx].DatabaseDataType;
                                databaseColLength = dataTypeMappings[mapInx].MaxLength;
                            }
                            else if (dcColLength <= dataTypeMappings[mapInx].MaxLength)
                            {
                                databaseDataType = dataTypeMappings[mapInx].DatabaseDataType;
                                databaseColLength = dataTypeMappings[mapInx].MaxLength;
                            }
                            else
                            {
                                ;
                            }
                        }
                    }

                }

                if (dcTypeCat == DataCategory.DateTime
                    && dcColLength > databaseColLength)
                {
                    for (int mapInx = 0; mapInx <= maxMapInx; mapInx++)
                    {
                        if (dataTypeMappings[mapInx].dataTypeCategory == DataCategory.DateTime)
                        {
                            if (databaseColLength == -1)
                            {
                                databaseDataType = dataTypeMappings[mapInx].DatabaseDataType;
                                databaseColLength = dataTypeMappings[mapInx].MaxLength;
                            }
                            else if (dcColLength <= dataTypeMappings[mapInx].MaxLength)
                            {
                                databaseDataType = dataTypeMappings[mapInx].DatabaseDataType;
                                databaseColLength = dataTypeMappings[mapInx].MaxLength;
                            }
                            else
                            {
                                ;
                            }
                        }
                    }

                }

                if (databaseDataType == string.Empty)
                {
                    if (dcTypeCat == DataCategory.String)
                        databaseDataType = longestStringDataType;
                    else if (dcTypeCat == DataCategory.Integer)
                        databaseDataType = longestIntegerDataType;
                    else if (dcTypeCat == DataCategory.FloatingPoint)
                        databaseDataType = longestFloatingPointDataType;
                    else if (dcTypeCat == DataCategory.Decimal)
                        databaseDataType = longestDecimalDataType;
                    else if (dcTypeCat == DataCategory.DateTime)
                        databaseDataType = longestDateTimeDataType;
                    else if (dcTypeCat == DataCategory.Boolean)
                    {
                        databaseDataType = longestBooleanDataType;
                        if (databaseDataType == string.Empty)
                            databaseDataType = smallestIntegerDataType;
                        if (databaseDataType == string.Empty)
                            databaseDataType = smallestCharDataType;
                        if (databaseDataType == string.Empty)
                            databaseDataType = smallestByteDataType;
                        if (databaseDataType == string.Empty)
                            databaseDataType = smallestFloatingPointDataType;
                        if (databaseDataType == string.Empty)
                            databaseDataType = smallestDecimalDataType;
                    }
                    else if (dcTypeCat == DataCategory.Char)
                    {
                        databaseDataType = smallestByteDataType;
                        if (databaseDataType == string.Empty)
                            databaseDataType = smallestIntegerDataType;
                        if (databaseDataType == string.Empty)
                            databaseDataType = smallestStringDataType;
                    }
                    else if (dcTypeCat == DataCategory.CharArray)
                    {
                        databaseDataType = longestCharArrayDataType;
                        if (databaseDataType == string.Empty)
                        {
                            databaseDataType = longestStringDataType;
                        }
                    }
                    else if (dcTypeCat == DataCategory.Byte)
                    {
                        databaseDataType = smallestIntegerDataType;
                        if (databaseDataType == string.Empty)
                            databaseDataType = smallestCharDataType;
                        if (databaseDataType == string.Empty)
                            databaseDataType = smallestStringDataType;
                    }
                    else if (dcTypeCat == DataCategory.ByteArray)
                        databaseDataType = longestByteArrayDataType;
                    else if (dcTypeCat == DataCategory.Guid)
                        databaseDataType = longestGuidDataType;
                    else if (dcTypeCat == DataCategory.Object)
                        databaseDataType = longestObjectDataType;
                    else
                        databaseDataType = longestByteArrayDataType;
                    if (databaseDataType.Trim().Length == 0)
                        databaseDataType = longestDataType;
                }

                sql.Append(dc.ColumnName);
                sql.Append(" ");
                sql.Append(databaseDataType);
                if (dc.MaxLength != 0)
                {
                    //if (dc.MaxLength > 0 && dc.MaxLength <= 8000)
                    if (dc.MaxLength > 0 && databaseDataTypeCreateParameters.Trim().Length>0)
                    {
                        sql.Append(" (");
                        sql.Append(dc.MaxLength.ToString());
                        sql.Append(")");
                    }
                    //sql.Append(" --.NET DataType: ");
                    //sql.Append(dc.DataType.FullName);
                    //sql.Append(", ");
                    //sql.Append(dc.MaxLength.ToString());
                    //sql.Append(", ");
                    //sql.Append(dcColLength.ToString());
                    //sql.Append("  --DB DataType: ");
                    //sql.Append(databaseDataType);
                    //sql.Append(", ");
                    //sql.Append(databaseColLength.ToString());
                }
                if (isIBMDB2 && databaseDataType.ToLower() == "long varchar for bit data")
                    sql.Append(string.Empty);
                else if (allowNull)
                    sql.Append(" null");
                else
                    sql.Append(" not null");

                if (i == maxColInx)
                    sql.Append(")");
                else
                    sql.Append(",\r\n");

            }



            return sql.ToString();

        }

        /// <summary>
        /// Routine that returns all the data type mappings supported by the ODBC driver or the OLEDB provider that is encapsulated by the instance of PFTableBuilder.
        /// </summary>
        /// <param name="connectionString">Contains connection parameters needed to connect to the database.</param>
        /// <returns>A list of DataTypeMapping structs that contain the data mappings supported by the current database driver/provider.</returns>
        public List<DataTypeMapping> GetGenericDataTypeMappingsAll(string connectionString)
        {
            List<DataTypeMapping> dataTypeMappings = new List<DataTypeMapping>();
            System.Data.Common.DbConnection conn = null;
            //OdbcConnection conn = null;


            try
            {
                if (this.DatabaseType == DatabasePlatform.ODBC)
                    conn = new OdbcConnection(connectionString);
                else if (this.DatabaseType == DatabasePlatform.OLEDB)
                    conn = new OleDbConnection(connectionString);
                else
                {
                    //error: DatabaseType must be odbc or oledb for a generic connection
                    _msg.Length = 0;
                    _msg.Append("Generic table create statements are built only for ODBC or OLEDB connections. Please specify DatabaseType as either ODBC or OLEDB.");
                    throw new DataException(_msg.ToString());
                }

                conn.Open();

                DataTable dt = conn.GetSchema("DataTypes");


                foreach (DataRow dr in dt.Rows)
                {
                    string dbDataType = dr["TypeName"].ToString();
                    string dotnetDataType = dr["DataType"].ToString();
                    string columnSize = dr["ColumnSize"].ToString();
                    string createFormat = dr["CreateFormat"].ToString();
                    string createParameters = dr["CreateParameters"].ToString();

                    if (dotnetDataType == "System.Double")
                        columnSize = "15";
                    if (dotnetDataType == "System.Single")
                        columnSize = "7";


                    if (PFSystemTypeInfo.DataTypeIsValid(dotnetDataType))
                    {
                        DataTypeMapping map = new DataTypeMapping();
                        map.DotNetDataType = dotnetDataType;
                        map.DatabaseDataType = dbDataType;
                        map.MaxLength = Convert.ToInt64(columnSize);
                        //get the data type's category
                        System.Type typ = PFSystemTypeInfo.ConvertNameToType(dotnetDataType);
                        if (PFSystemTypeInfo.DataTypeIsString(typ))
                            map.dataTypeCategory = DataCategory.String;
                        else if (PFSystemTypeInfo.DataTypeIsInteger(typ))
                            map.dataTypeCategory = DataCategory.Integer;
                        else if (PFSystemTypeInfo.DataTypeIsFloatingPoint(typ))
                            map.dataTypeCategory = DataCategory.FloatingPoint;
                        else if (PFSystemTypeInfo.DataTypeIsDecimal(typ))
                            map.dataTypeCategory = DataCategory.Decimal;
                        else if (PFSystemTypeInfo.DataTypeIsDateTime(typ))
                            map.dataTypeCategory = DataCategory.DateTime;
                        else if (PFSystemTypeInfo.DataTypeIsBoolean(typ))
                            map.dataTypeCategory = DataCategory.Boolean;
                        else if (PFSystemTypeInfo.DataTypeIsChar(typ))
                            map.dataTypeCategory = DataCategory.Char;
                        else if (PFSystemTypeInfo.DataTypeIsByte(typ))
                            map.dataTypeCategory = DataCategory.Byte;
                        else if (PFSystemTypeInfo.DataTypeIsCharArray(typ))
                            map.dataTypeCategory = DataCategory.CharArray;
                        else if (PFSystemTypeInfo.DataTypeIsByteArray(typ))
                            map.dataTypeCategory = DataCategory.ByteArray;
                        else if (PFSystemTypeInfo.DataTypeIsGuid(typ))
                            map.dataTypeCategory = DataCategory.Guid;
                        else
                            map.dataTypeCategory = DataCategory.Object;
                        map.CreateFormat = createFormat;
                        map.CreateParameters = string.Empty;
                        if (Convert.ToInt64(columnSize) > 0)
                            if (createParameters.Trim().Length > 0)
                                if (createParameters.ToLower().Contains("length"))
                                    map.CreateParameters = createParameters;
                        dataTypeMappings.Add(map);
                    }

                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error during attempt to retrieve data types schema from ODBC driver: ");
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new DataException(_msg.ToString());
            }
            finally
            {
                if (conn != null)
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                conn.Dispose();
                conn = null;
            }


            return dataTypeMappings;
        }


        /// <summary>
        /// Routine that returns the data type mappings supported by the ODBC driver or the OLEDB provider that is encapsulated by the instance of PFTableBuilder.
        /// </summary>
        /// <param name="connectionString">Contains connection parameters needed to connect to the database.</param>
        /// <returns>A list of DataTypeMapping structs that contain the data mappings supported by the current database driver/provider.</returns>
        public List<DataTypeMapping> GetGenericDataTypeMappingsEx(string connectionString)
        {
            List<DataTypeMapping> dataTypeMappings = new List<DataTypeMapping>();
            System.Data.Common.DbConnection conn = null;
            //OdbcConnection conn = null;
            bool isSQLServer = false;
            bool isOracleNative = false;
            bool isMsOracle = false;
            bool isSQLAnywhere = false;
            bool isIBMDB2 = false;


            try
            {
                if(this.DatabaseType == DatabasePlatform.ODBC)
                    conn = new OdbcConnection(connectionString);
                else if (this.DatabaseType == DatabasePlatform.OLEDB)
                    conn = new OleDbConnection(connectionString);
                else
                {
                    //error: DatabaseType must be odbc or oledb for a generic connection
                    _msg.Length = 0;
                    _msg.Append("Generic table create statements are built only for ODBC or OLEDB connections. Please specify DatabaseType as either ODBC or OLEDB.");
                    throw new DataException(_msg.ToString());
                }

                conn.Open();

                DataTable dt = conn.GetSchema("DataTypes");

                if (connectionString.ToUpper().Contains("SQLOLEDB") || connectionString.ToUpper().Contains("{SQL SERVER"))
                    isSQLServer = true;
                if (connectionString.ToUpper().Contains("ORAOLEDB") || connectionString.ToUpper().Contains("{ORACLE IN ORACLIENT"))
                    isOracleNative = true;
                if (connectionString.ToUpper().Contains("MSDAORA") || connectionString.ToUpper().Contains("{MICROSOFT ODBC FOR ORACLE"))
                    isMsOracle = true;
                if (connectionString.ToUpper().Contains("{SQL ANYWHERE"))
                    isSQLAnywhere = true;
                if (connectionString.ToUpper().Contains("IBMDADB2") || connectionString.ToUpper().Contains("{IBM DB2 ODBC DRIVER"))
                    isIBMDB2 = true;


                foreach (DataRow dr in dt.Rows)
                {
                    string dbDataType = dr["TypeName"].ToString();
                    string dotnetDataType = dr["DataType"].ToString();
                    string columnSize = dr["ColumnSize"].ToString();
                    string createFormat = dr["CreateFormat"].ToString();
                    string createParameters = dr["CreateParameters"].ToString();

                    if (dotnetDataType == "System.Double")
                        columnSize = "15";
                    if (dotnetDataType == "System.Single")
                        columnSize = "7";


                    if (PFSystemTypeInfo.DataTypeIsValid(dotnetDataType))
                    {
                        if (dbDataType.ToLower().Contains("identity") == false
                            && dbDataType.ToLower().Contains("counter") == false
                            && dbDataType.ToLower().Contains("auto_increment") == false
                            && dbDataType.ToLower().Contains("auto") == false
                            && dbDataType.ToLower().Contains("increment") == false
                            && dbDataType.ToLower().Contains("sysname") == false
                            && dbDataType.ToLower().Contains("xml")==false
                            && dbDataType.ToLower().Contains("sql_variant")==false)
                        {
                            DataTypeMapping map = new DataTypeMapping();
                            map.DotNetDataType = dotnetDataType;
                            map.DatabaseDataType = dbDataType;
                            map.MaxLength = Convert.ToInt64(columnSize);
                            //get the data type's category
                            System.Type typ = PFSystemTypeInfo.ConvertNameToType(dotnetDataType);
                            if (PFSystemTypeInfo.DataTypeIsString(typ))
                                map.dataTypeCategory = DataCategory.String;
                            else if (PFSystemTypeInfo.DataTypeIsInteger(typ))
                                map.dataTypeCategory = DataCategory.Integer;
                            else if (PFSystemTypeInfo.DataTypeIsFloatingPoint(typ))
                                map.dataTypeCategory = DataCategory.FloatingPoint;
                            else if (PFSystemTypeInfo.DataTypeIsDecimal(typ))
                                map.dataTypeCategory = DataCategory.Decimal;
                            else if (PFSystemTypeInfo.DataTypeIsDateTime(typ))
                                map.dataTypeCategory = DataCategory.DateTime;
                            else if (PFSystemTypeInfo.DataTypeIsBoolean(typ))
                                map.dataTypeCategory = DataCategory.Boolean;
                            else if (PFSystemTypeInfo.DataTypeIsChar(typ))
                                map.dataTypeCategory = DataCategory.Char;
                            else if (PFSystemTypeInfo.DataTypeIsByte(typ))
                                map.dataTypeCategory = DataCategory.Byte;
                            else if (PFSystemTypeInfo.DataTypeIsCharArray(typ))
                                map.dataTypeCategory = DataCategory.CharArray;
                            else if (PFSystemTypeInfo.DataTypeIsByteArray(typ))
                                map.dataTypeCategory = DataCategory.ByteArray;
                            else if (PFSystemTypeInfo.DataTypeIsGuid(typ))
                                map.dataTypeCategory = DataCategory.Guid;
                            else
                                map.dataTypeCategory = DataCategory.Object;
                            map.CreateFormat = createFormat;
                            map.CreateParameters=string.Empty;
                            if(Convert.ToInt64(columnSize)>0)
                                if(createParameters.Trim().Length>0)
                                    if(createParameters.ToLower().Contains("length"))
                                        map.CreateParameters = createParameters;
                            //dont add certain sql server data types
                            bool addMapping = true;
                            if(isSQLServer)
                                if (dbDataType.ToLower().StartsWith("date")  //next four conditions remove some troublesome SQL Server data types: They map to System.String instead of System.DateTime
                                   || dbDataType.ToLower().StartsWith("time")
                                   || dbDataType.ToLower().StartsWith("datetime2")
                                   || dbDataType.ToLower().StartsWith("datetimeoffset"))
                                    addMapping = false;
                            if (isOracleNative)
                                if (dbDataType.ToLower() == "long"   //next seven conditions are designed to remove some troublesome Oracle data types
                                   || dbDataType.ToLower() == "long raw"
                                   || dbDataType.ToLower() == "timestamp with time zone"
                                   || dbDataType.ToLower() == "timestamp with local time zone"
                                   || dbDataType.ToLower() == "interval year to month"
                                   || dbDataType.ToLower() == "interval day to second"
                                   || dbDataType.ToLower() == "bfile")
                                    addMapping = false;
                            if (isMsOracle)
                                if (dbDataType.ToLower() == "long")   
                                    addMapping = false;
                            if (isSQLAnywhere)
                                if (dbDataType.ToLower() == "long"
                                   || dbDataType.ToLower() == "long raw"
                                   || dbDataType.ToLower() == "timestamp with time zone"
                                   || dbDataType.ToLower() == "long varbit"
                                   || dbDataType.ToLower() == "st_geometry"
                                   || dbDataType.ToLower() == "varbit"
                                   || dbDataType.ToLower() == "date")
                                    addMapping = false;
                            if (isIBMDB2)
                                if (dbDataType.ToLower() == "varchar () for bit data"
                                   //|| dbDataType.ToLower() == "long varchar for bit data"
                                   || dbDataType.ToLower() == "char () for bit data")
                                    addMapping = false;

                            if (addMapping)
                                dataTypeMappings.Add(map);
                        }
                    }

                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error during attempt to retrieve data types schema from ODBC driver: ");
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new DataException(_msg.ToString());
            }
            finally
            {
                if (conn != null)
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                conn.Dispose();
                conn = null;
            }
                 

            return dataTypeMappings;
        }


        /// <summary>
        /// Routine that categorizes a data type.
        /// </summary>
        /// <param name="typ">The system type being categorized.</param>
        /// <returns>Returns a DataCategory enumeration value.</returns>
        public DataCategory GetDataCategory(System.Type typ)
        {
            DataCategory cat = DataCategory.Unknown;

            try
            {
                if (PFSystemTypeInfo.DataTypeIsString(typ))
                    cat = DataCategory.String;
                else if (PFSystemTypeInfo.DataTypeIsInteger(typ))
                    cat = DataCategory.Integer;
                else if (PFSystemTypeInfo.DataTypeIsFloatingPoint(typ))
                    cat = DataCategory.FloatingPoint;
                else if (PFSystemTypeInfo.DataTypeIsDecimal(typ))
                    cat = DataCategory.Decimal;
                else if (PFSystemTypeInfo.DataTypeIsDateTime(typ))
                    cat = DataCategory.DateTime;
                else if (PFSystemTypeInfo.DataTypeIsBoolean(typ))
                    cat = DataCategory.Boolean;
                else if (PFSystemTypeInfo.DataTypeIsChar(typ))
                    cat = DataCategory.Char;
                else if (PFSystemTypeInfo.DataTypeIsByte(typ))
                    cat = DataCategory.Byte;
                else if (PFSystemTypeInfo.DataTypeIsCharArray(typ))
                    cat = DataCategory.CharArray;
                else if (PFSystemTypeInfo.DataTypeIsByteArray(typ))
                    cat = DataCategory.ByteArray;
                else if (PFSystemTypeInfo.DataTypeIsGuid(typ))
                    cat = DataCategory.Guid;
                else 
                    cat = DataCategory.Object;
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error during attempt to retrieve data category for ");
                _msg.Append(typ.FullName);
                _msg.Append(": ");
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new DataException(_msg.ToString());
            }
            finally
            {
                ;
            }

            return cat;
        }



    }//end class
}//end namespace
