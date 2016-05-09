//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;
using System.Data;
using System.Data.Common;
using PFCollectionsObjects;
using PFDataAccessObjects;
using PFTextObjects;

namespace PFUnitTestDataObjects
{
    /// <summary>
    /// Class used in testing database classes. Contains table definition that includes all data types.
    /// </summary>
    public class PFUnitTestDataTable
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();

        IDatabaseProvider _db = null;

#pragma warning disable 1591
        public class TableColumn
        {
            public string ColumnName;
            public string ColumnType;
            public string ColumnValue;
            public int MaxLength;
            public bool IncludeInOutput;
            public bool IsPrimaryKey;
            public bool AllowsNulls;

            public TableColumn()
            {
                ColumnName = string.Empty;
                ColumnType = "System.String";
                ColumnValue = string.Empty;
                MaxLength = 1;
                IncludeInOutput = false;
                IsPrimaryKey = false;
                AllowsNulls = true;
            }

            public TableColumn(string colName, string colType, string colValue, int maxLen, bool include, bool isPK, bool allowNull)
            {
                ColumnName = colName;
                ColumnType = colType;
                ColumnValue = colValue;
                MaxLength = maxLen;
                IncludeInOutput = include;
                IsPrimaryKey = isPK;
                AllowsNulls = allowNull;
            }
        }

#pragma warning restore 1591

        //private variables for properties
        private string _schemaName = "dbo";
        private string _tableName = "TestTable01";
        private PFList<TableColumn> _tableColumns = new PFList<TableColumn>();
        private string _tableCreateScript = string.Empty;
        private bool _dropOldTable = false;

        //constructors


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db">Database object for database to be used.</param>
        /// <param name="schemaName">Name of schema for the table. Leave blank (empty string) if no schema name is required.</param>
        /// <param name="tableName">Name of the table. Can be the full qualified name (schemaname.tablename) if you need a schema and the schema name parameter was not specified.</param>
        /// <param name="dropOldTable">Specify true if you wish to delete the table if it already exists in the database. You can change this value later by setting the DropOldTable property.</param>
        /// <remarks>If the table already exists and the DropOldTable option is set to false, the constructor for this class will throw a System.Exception.</remarks>
        public PFUnitTestDataTable(IDatabaseProvider db, string schemaName, string tableName, bool dropOldTable)
        {
            InitInstance(db, schemaName, tableName, dropOldTable);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db">Database object for database to be used.</param>
        /// <param name="schemaName">Name of schema for the table. Leave blank (empty string) if no schema name is required.</param>
        /// <param name="tableName">Name of the table. Can be the full qualified name (schemaname.tablename) if you need a schema and the schema name parameter was not specified.</param>
        /// <remarks>Default is False for the dropOldTable option. Use the DropOldTable property to revise the value to True if you wish to delete the old table before creating a new one.
        ///  If the table already exists and the DropOldTable option is set to false, the constructor for this class will throw a System.Exception.</remarks>
        public PFUnitTestDataTable(IDatabaseProvider db, string schemaName, string tableName)
        {
            InitInstance(db, schemaName, tableName, false);
        }

        private void InitInstance(IDatabaseProvider db, string schemaName, string tableName, bool dropOldTable)
        {
            _db = db;
            _schemaName = schemaName;
            _dropOldTable = dropOldTable;
            if (tableName.Trim().Length > 0)
                _tableName = tableName;
            else
                _tableName = "TestTable01";

            if (_db.IsConnected == false)
            {
                _msg.Length = 0;
                _msg.Append("Database connection is closed. It must be open before passing it to this instance of PFUnitTestDataTable class.");
                throw new System.Exception(_msg.ToString());
            }

            TableColumn k1 = new TableColumn("K1", "System.Int32", "1", -1, true, false, false);
            TableColumn f1 = new TableColumn("F1", "System.String", "Short string value", 50, true, false, true);
            //TableColumn f1a = new TableColumn("F1A", "System.String", "Much longer string value goes into this field", 50000, true, false, true);
            TableColumn f2 = new TableColumn("F2", "System.Int32", "1123456789", -1, true, false, true);
            TableColumn f2a = new TableColumn("F2A", "System.UInt32", "3123456789", -1, true, false, true);
            TableColumn f3 = new TableColumn("F3", "System.Int64", "23123456789", -1, true, false, true);
            TableColumn f3a = new TableColumn("F3A", "System.UInt64", "8881234567889", -1, true, false, true);
            TableColumn f4 = new TableColumn("F4", "System.Int16", "11123", -1, true, false, true);
            TableColumn f4a = new TableColumn("F4A", "System.UInt16", "52432", -1, true, false, true);
            TableColumn f5 = new TableColumn("F5", "System.Double", "123456.7654", -1, true, false, true);
            TableColumn f6 = new TableColumn("F6", "System.Single", "321.234", -1, true, false, true);
            TableColumn f7 = new TableColumn("F7", "System.Decimal", "2123456789.22", -1, true, false, true);
            TableColumn f8 = new TableColumn("F8", "System.Char", "A", -1, true, false, true);
            TableColumn f8a = new TableColumn("F8A", "System.Char[]", "ABCDEFGH", -1, true, false, true);
            TableColumn f9 = new TableColumn("F9", "System.Byte", "254", -1, true, false, true);
            TableColumn f9a = new TableColumn("F9A", "System.SByte", "125", -1, true, false, true);
            TableColumn f9b = new TableColumn("F9B", "System.Byte[]", "UVWZYZ)", -1, true, false, true);
            TableColumn f10 = new TableColumn("F10", "System.Boolean", "true", -1, true, false, true);
            TableColumn f11 = new TableColumn("F11", "System.Object", "This is an object: be careful!", -1, true, false, true);
            TableColumn f12 = new TableColumn("F12", "System.DateTime", "5/31/2013 13:54:25", -1, true, false, true);
            TableColumn f13 = new TableColumn("F13", "System.Guid", "58a4a08d-6101-4393-86dc-b2a8db46ec0f", -1, true, false, true);


            _tableColumns.Add(k1);
            _tableColumns.Add(f1);
            _tableColumns.Add(f2);
            _tableColumns.Add(f2a);
            _tableColumns.Add(f3);
            _tableColumns.Add(f3a);
            _tableColumns.Add(f4);
            _tableColumns.Add(f4a);
            _tableColumns.Add(f5);
            _tableColumns.Add(f6);
            _tableColumns.Add(f7);
            _tableColumns.Add(f8);
            _tableColumns.Add(f8a);
            _tableColumns.Add(f9);
            _tableColumns.Add(f9a);
            _tableColumns.Add(f9b);
            _tableColumns.Add(f10);
            _tableColumns.Add(f11);
            _tableColumns.Add(f12);
            _tableColumns.Add(f13);

        
        }

        //properties

        /// <summary>
        /// Specifies name of schema for which the table will be associated. Specify empty string if there will be no schema name for the table.
        /// </summary>
        public string SchemaName
        {
            get
            {
                return _schemaName;
            }
        }

        /// <summary>
        /// Specified name of table to be created. Name should be fully qualified if schema and/or catalog are also required as part of the name.
        /// </summary>
        public string TableName
        {
            get
            {
                return _tableName;
            }
        }

        /// <summary>
        /// Object containing the definitions of the columns that will be included in the table to be created.
        /// </summary>
        public PFList<TableColumn> TableColumns
        {
            get
            {
                return _tableColumns;
            }
            set
            {
                _tableColumns = value;
            }
        }


        /// <summary>
        /// Returns text of the create script to be used for the specified database.
        /// </summary>
        public string TableCreateScript
        {
            get
            {
                DataTable dt = GetDataTableFromTableColumns();
                return _db.BuildTableCreateStatement(dt);
            }
        }

        /// <summary>
        /// Set to true if you wish any older copy of a table deleted before it is recreated by this instance.
        /// </summary>
        public bool DropOldTable
        {
            get
            {
                return _dropOldTable;
            }
            set
            {
                _dropOldTable = value;
            }
        }




        //methods

        /// <summary>
        /// Routine to identify which data types to include in the table to be created.
        /// </summary>
        /// <param name="dataTypesToInclude">A list of KeyValuePair values. Key = data type fullname, Value = the value to assign to the column of that data type.</param>
        public void SetDataTypesToInclude(List<KeyValuePair<string, string>> dataTypesToInclude)
        {
            this.TableColumns.SetToBOF();
            TableColumn tc = null;

            //reinitialize to exclude all
            while ((tc = this.TableColumns.NextItem) != null)
            {
                tc.IncludeInOutput = false;
            }

            //set columns to include based on caller's input
            this.TableColumns.SetToBOF();

            while ((tc = this.TableColumns.NextItem) != null)
            {
                for (int i = 0; i < dataTypesToInclude.Count; i++)
                {
                    if (dataTypesToInclude[i].Key == tc.ColumnType)
                    {
                        tc.IncludeInOutput = true;
                        tc.ColumnValue = dataTypesToInclude[i].Value;
                        break;
                    }
                }
            }

        }

        /// <summary>
        /// Routine to set the PrimaryKey, AllowNulls and MaximumLength properties of a data type to be included in the table to be created.
        /// </summary>
        /// <param name="dataTypeFullName">Full name of data type. For example, System.String or System.Int32.</param>
        /// <param name="isPrimaryKey">If true, the column will be defined as a primary key. Default is false.</param>
        /// <param name="isNullable">If false the column will not allow nulls. By default this value is true.</param>
        public void SetDataTypeOptions(string dataTypeFullName, bool isPrimaryKey, bool isNullable)
        {
            SetDataTypeOptions(dataTypeFullName, isPrimaryKey, isNullable, -1);
        }

        /// <summary>
        /// Routine to set the PrimaryKey, AllowNulls and MaximumLength properties of a data type to be included in the table to be created.
        /// </summary>
        /// <param name="dataTypeFullName">Full name of data type. For example, System.String or System.Int32.</param>
        /// <param name="isPrimaryKey">If true, the column will be defined as a primary key. Default is false.</param>
        /// <param name="isNullable">If false the column will not allow nulls. By default this value is true.</param>
        /// <param name="maxLength">Only set this for System.String.</param>
        public void SetDataTypeOptions(string dataTypeFullName, bool isPrimaryKey, bool isNullable, int maxLength)
        {
            this.TableColumns.SetToBOF();
            TableColumn tc = null;

            while ((tc = this.TableColumns.NextItem) != null)
            {
                if (tc.ColumnType == dataTypeFullName)
                {
                    tc.IsPrimaryKey = isPrimaryKey;
                    tc.AllowsNulls = isNullable;
                    if (maxLength > 0)
                        tc.MaxLength = maxLength;
                }
            }
        }

        /// <summary>
        /// Routine to set the PrimaryKey, AllowNulls and MaximumLength properties of a data type to be included in the table to be created.
        /// </summary>
        /// <param name="colName">Name of the column which will have its data type changed by the routine.</param>
        /// <param name="dataTypeFullName">Full name of data type. For example, System.String or System.Int32.</param>
        /// <param name="isPrimaryKey">If true, the column will be defined as a primary key. Default is false.</param>
        /// <param name="isNullable">If false the column will not allow nulls. By default this value is true.</param>
        /// <param name="maxLength">Only set this for System.String.</param>
        public void SetDataTypeOptions(string colName, string dataTypeFullName, bool isPrimaryKey, bool isNullable, int maxLength)
        {
            this.TableColumns.SetToBOF();
            TableColumn tc = null;

            while ((tc = this.TableColumns.NextItem) != null)
            {
                if (tc.ColumnName == colName)
                {
                    tc.ColumnType = dataTypeFullName;
                    tc.IsPrimaryKey = isPrimaryKey;
                    tc.AllowsNulls = isNullable;
                    if (maxLength > 0)
                        tc.MaxLength = maxLength;
                }
            }
        }

        /// <summary>
        /// Creates an ADO.NET DataTable object using the table definition contained in the collection of column definitions encapsulated by this instance.
        /// </summary>
        /// <returns>DataTable object.</returns>
        public DataTable GetDataTableFromTableColumns()
        {
            return GetDataTableFromTableColumns(false);
        }

        /// <summary>
        /// Creates an ADO.NET DataTable object using the table definition contained in the collection of column definitions encapsulated by this instance.
        /// </summary>
        /// <param name="useDataTypeSubset">If true, then only a small subset of .NET data types will be used. This is needed when trying to create a table via generic OLEDB or ODBC syntax.</param>
        /// <returns>DataTable object.</returns>
        public DataTable GetDataTableFromTableColumns(bool useDataTypeSubset)
        {
            DataTable dt = new DataTable();
            List<DataColumn> primaryKeys = new List<DataColumn>();

            if (_schemaName == null)
                _schemaName = string.Empty;
            if (String.IsNullOrEmpty(_tableName))
                _tableName = "TestTable01";

            if (_schemaName.Trim().Length > 0)
            {
                dt.TableName = _schemaName.Trim() + "." + _tableName.Trim();
            }
            else
            {
                dt.TableName = TableName.Trim();
            }

            _tableColumns.SetToBOF();
            TableColumn tc = null;
            tc = _tableColumns.FirstItem;
            while (tc != null)
            {
                if (tc.IncludeInOutput)
                {
                    DataColumn dc = new DataColumn();
                    dc.ColumnName = tc.ColumnName;
                    //dc.DataType = Type.GetType(tc.ColumnType);
                    //if (tc.MaxLength > 0)
                    //    dc.MaxLength = tc.MaxLength;
                    if (useDataTypeSubset == false)
                    {
                        dc.DataType = Type.GetType(tc.ColumnType);
                        if (tc.MaxLength > 0)
                            dc.MaxLength = tc.MaxLength;
                    }
                    else
                    {
                        SetDataTypeFromSubset(tc.ColumnType, tc.MaxLength, dc);
                    }
                    dc.AllowDBNull = tc.AllowsNulls;
                    if (tc.IsPrimaryKey)
                    {
                        primaryKeys.Add(dc);
                    }
                    dc.DefaultValue = GetColumnValue(dc.DataType, tc.ColumnValue);
                    dt.Columns.Add(dc);
                }
                tc = _tableColumns.NextItem;
            }
            if (primaryKeys.Count > 0)
            {
                dt.PrimaryKey = primaryKeys.ToArray();
            }

            // create a data row for the data table
            DataRow dr = dt.NewRow();
            dr.ItemArray = new object[dt.Columns.Count];
            int maxColInx = dt.Columns.Count - 1;
            for (int colInx = 0; colInx <= maxColInx; colInx++)
            {
                //dr.ItemArray[colInx] = GetColumnValue(dt.Columns[colInx]);
                dr.ItemArray[colInx] = dt.Columns[colInx].DefaultValue;
            }
            dt.Rows.Add(dr);


            return dt;
        }

        private object GetColumnValue(System.Type typ, string colValue)
        {
            object val = null;

            switch (typ.FullName)
            {
                case "System.String":
                    val = (object)Convert.ToString(colValue);
                    break;
                case "System.Int32":
                    val = (object)Convert.ToInt32(colValue);
                    break;
                case "System.UInt32":
                    val = (object)Convert.ToUInt32(colValue);
                    break;
                case "System.Int64":
                    val = (object)Convert.ToInt64(colValue);
                    break;
                case "System.UInt64":
                    val = (object)Convert.ToUInt64(colValue);
                    break;
                case "System.Int16":
                    val = (object)Convert.ToInt16(colValue);
                    break;
                case "System.UInt16":
                    val = (object)Convert.ToUInt16(colValue);
                    break;
                case "System.Double":
                    val = (object)Convert.ToDouble(colValue);
                    break;
                case "System.Single":
                    val = (object)Convert.ToSingle(colValue);
                    break;
                case "System.Decimal":
                    val = (object)Convert.ToDecimal(colValue);
                    break;
                case "System.Boolean":
                    val = (object)Convert.ToBoolean(colValue);
                    break;
                case "System.Char":
                    val = (object)Convert.ToChar(colValue);
                    break;
                case "System.Char[]":
                    val = (object)colValue.ToCharArray();
                    break;
                case "System.Byte":
                    val = (object)Convert.ToByte(colValue);
                    break;
                case "System.Byte[]":
                    val = (object)PFTextProcessor.ConvertStringToByteArray(colValue);
                    break;
                case "System.SByte":
                    val = (object)Convert.ToSByte(colValue);
                    break;
                case "System.DateTime":
                    val = (object)Convert.ToDateTime(colValue);
                    break;
                case "System.Guid":
                    val = (object)new Guid(colValue);
                    break;
                case "System.Object":
                    val = (object)PFTextProcessor.ConvertStringToByteArray(colValue);
                    break;
                default:
                    val = (object)PFTextProcessor.ConvertStringToByteArray(colValue);
                    break;
            }


            return val;
        }

        private void SetDataTypeFromSubset(string colType, int maxLen,  DataColumn dc)
        {
        
            switch (colType)
            {
                case "System.String":
                    dc.DataType = System.Type.GetType("System.String");
                    dc.MaxLength = maxLen;
                    break;
                case "System.Int32":
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.MaxLength = -1;
                    break;
                case "System.UInt32":
                    dc.DataType = System.Type.GetType("System.Double");
                    dc.MaxLength = -1;
                    break;
                case "System.Int64":
                    dc.DataType = System.Type.GetType("System.Double");
                    dc.MaxLength = -1;
                    break;
                case "System.UInt64":
                    dc.DataType = System.Type.GetType("System.Double");
                    dc.MaxLength = -1;
                    break;
                case "System.Int16":
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.MaxLength = -1;
                    break;
                case "System.UInt16":
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.MaxLength = -1;
                    break;
                case "System.Double":
                    dc.DataType = System.Type.GetType("System.Double");
                    dc.MaxLength = -1;
                    break;
                case "System.Single":
                    dc.DataType = System.Type.GetType("System.Double");
                    dc.MaxLength = -1;
                    break;
                case "System.Decimal":
                    dc.DataType = System.Type.GetType("System.Double");
                    dc.MaxLength = -1;
                    break;
                case "System.Boolean":
                    if (_db.ConnectionString.Contains("SQLOLEDB") || _db.ConnectionString.Contains("SQL Server"))
                    {
                        dc.DataType = System.Type.GetType("System.Boolean");
                        dc.MaxLength = -1;
                    }
                    else
                    {
                        dc.DataType = System.Type.GetType("System.String");
                        dc.MaxLength = 5;
                    }
                    break;
                case "System.Char":
                    dc.DataType = System.Type.GetType("System.String");
                    dc.MaxLength = 1;
                    break;
                case "System.Char[]":
                    dc.DataType = System.Type.GetType("System.String");
                    dc.MaxLength = Convert.ToInt32(UInt16.MaxValue);
                    break;
                case "System.Byte":
                    //dc.DataType = System.Type.GetType("System.Byte[]");
                    //dc.MaxLength = -1;                    
                    dc.DataType = System.Type.GetType("System.String");
                    dc.MaxLength = Convert.ToInt32(UInt16.MaxValue);
                    break;
                case "System.Byte[]":
                    dc.DataType = System.Type.GetType("System.Byte[]");
                    dc.MaxLength = -1;
                    //dc.DataType = System.Type.GetType("System.String");
                    //dc.MaxLength = Convert.ToInt32(UInt16.MaxValue);
                    break;
                case "System.SByte":
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.MaxLength = -1;
                    break;
                case "System.DateTime":
                    if (_db.ConnectionString.Contains("SQLOLEDB") || _db.ConnectionString.Contains("SQL Server"))
                    {
                        dc.DataType = System.Type.GetType("System.DateTime");
                        dc.MaxLength = -1;
                    }
                    else
                    {
                        dc.DataType = System.Type.GetType("System.String");
                        dc.MaxLength = 60;
                    }
                    break;
                case "System.Guid":
                    dc.DataType = System.Type.GetType("System.String");
                    dc.MaxLength = 36;
                    break;
                case "System.Object":
                    dc.DataType = System.Type.GetType("System.Byte[]");
                    dc.MaxLength = -1;
                    break;
                default:
                    dc.DataType = System.Type.GetType("System.Byte[]");
                    dc.MaxLength = -1;
                    break;
            }

        }

        /// <summary>
        /// Creates a table in the database specified for this instance. Data is loaded from the column definitions encapsulated by this instance.
        /// </summary>
        /// <returns>True if table is created.</returns>
        /// <remarks>Table create will fail if table already exists and you specify false for the DropOldTable option. Specify true for dropOldTable if you
        ///  want to create the table even if an older version of the table already exists.</remarks>
        public bool CreateTableFromTableColumns()
        {
            bool tableCreated = false;
            DataTable dt = null;

            try
            {
                if (_db.DbPlatform == DatabasePlatform.ODBC || _db.DbPlatform == DatabasePlatform.OLEDB || _db.DbPlatform == DatabasePlatform.MSAccess)
                    dt = GetDataTableFromTableColumns(true);
                else
                    dt = GetDataTableFromTableColumns();
                if (_dropOldTable)
                {
                    if (this.SchemaName.Trim().Length > 0)
                    {
                        if (_db.TableExists(this.SchemaName, this.TableName))
                            _db.DropTable(this.SchemaName, this.TableName);
                    }
                    else
                    {
                        if (_db.TableExists(this.TableName))
                            _db.DropTable(this.TableName);
                    }
                }
                tableCreated = _db.CreateTable(dt);
            }
            catch (System.Exception ex)
            {
                tableCreated = false;
                _msg.Length = 0;
                _msg.Append("Error while trying to create table from DataTable object ");
                _msg.Append(this.TableName);
                _msg.Append(": ");
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new DataException(_msg.ToString());
            }
            finally
            {
                ;
            }

            return tableCreated;
        }

        /// <summary>
        /// Routine to import the data table encapsulated in this instance to a corresponding table in the database.
        /// </summary>
        /// <returns>True if data import was successful.</returns>
        public bool ImportTableToDatabase()
        {
            bool dataImportSuccessful = false;
            DataTable dt = null;

            try
            {

                if (_db.DbPlatform == DatabasePlatform.ODBC || _db.DbPlatform == DatabasePlatform.OLEDB || _db.DbPlatform == DatabasePlatform.MSAccess)
                    dt = GetDataTableFromTableColumns(true);
                else
                    dt = GetDataTableFromTableColumns();
                //dt = GetDataTableFromTableColumns();
                if (this.SchemaName.Trim().Length > 0)
                {
                    if (_db.TableExists(this.SchemaName, this.TableName) == false)
                        _db.CreateTable(dt);
                }
                else
                {
                    if (_db.TableExists(this.TableName) == false)
                        _db.CreateTable(dt);
                }
                _db.ImportDataFromDataTable(dt);
                dataImportSuccessful = true;
            }
            catch (System.Exception ex)
            {
                dataImportSuccessful = false;
                _msg.Length = 0;
                _msg.Append("Error while trying to create table from DataTable object ");
                _msg.Append(this.TableName);
                _msg.Append(": ");
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new DataException(_msg.ToString());
            }
            finally
            {
                ;
            }

            return dataImportSuccessful;
        }


        //************************************
        //           class helpers
        //************************************
        //*

        /// <summary>
        /// Saves the column definitions contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFUnitTestDataTable));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFInitClassExtended.</returns>
        public static PFUnitTestDataTable LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFUnitTestDataTable));
            TextReader textReader = new StreamReader(filePath);
            PFUnitTestDataTable columnDefinitions;
            columnDefinitions = (PFUnitTestDataTable)deserializer.Deserialize(textReader);
            textReader.Close();
            return columnDefinitions;
        }


        /// <summary>
        /// Routine overrides default ToString method and outputs name, type, scope and value for all class properties and fields.
        /// </summary>
        /// <returns>String value.</returns>
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
        /// <returns>String value.</returns>
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
        /// <returns>String value.</returns>
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


        /// <summary>
        /// Returns a string containing the contents of the object in XML format.
        /// </summary>
        /// <returns>String value in xml format.</returns>
        /// ///<remarks>XML Serialization is used for the transformation.</remarks>
        public string ToXmlString()
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFUnitTestDataTable));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Converts instance of this class into an XML document.
        /// </summary>
        /// <returns>XmlDocument</returns>
        /// ///<remarks>XML Serialization and XmlDocument class are used for the transformation.</remarks>
        public XmlDocument ToXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(this.ToXmlString());
            return doc;
        }


    }//end class
}//end namespace
