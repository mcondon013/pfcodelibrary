using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Xml;
using PFTimers;
using PFTextFiles;
using PFDataAccessObjects;
using PFUnitTestDataObjects;
using PFCollectionsObjects;
using PFListObjects;

namespace TestprogOdbc
{
    public class Tests
    {
        private static StringBuilder _msg = new StringBuilder();
        private static StringBuilder _str = new StringBuilder();
        private static bool _saveErrorMessagesToAppLog = false;

        private static PFTextFile _textFile = new PFTextFile();

        //properties
        public static bool SaveErrorMessagesToAppLog
        {
            get
            {
                return Tests._saveErrorMessagesToAppLog;
            }
            set
            {
                Tests._saveErrorMessagesToAppLog = value;
            }
        }

        //tests
        public static void RunConnectionTest(MainForm frm)
        {
            PFOdbc db = new PFOdbc();

            Program._messageLog.WriteLine("RunConnectionTest started ...");
            try
            {
                db.ConnectionString = frm.cboConnectionString.Text;
                db.OpenConnection();

                _msg.Length = 0;
                _msg.Append("Connection string is ");
                _msg.Append(db.ConnectionString);
                _msg.Append("\r\n");
                _msg.Append("Connection state is  ");
                _msg.Append(db.Connection.State.ToString());
                _msg.Append("\r\n");
                _msg.Append("Driver is  ");
                _msg.Append(db.Driver);
                _msg.Append("\r\n");
                _msg.Append("Dsn is  ");
                _msg.Append(db.Dsn);
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                foreach (stKeyValuePair<string, string> kv in db.ConnectionStringKeyVals)
                {
                    _msg.Length = 0;
                    _msg.Append(kv.Key + "=" + kv.Value);
                    Program._messageLog.WriteLine(_msg.ToString());
                }
                Program._messageLog.WriteLine(Environment.NewLine);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if (db.Connection.State == ConnectionState.Open)
                    db.CloseConnection();
                Program._messageLog.WriteLine("... RunConnectionTest finished.");

            }
        }

        public static void RunDataReaderTest(MainForm frm)
        {
            PFOdbc db = new PFOdbc();

            Program._messageLog.WriteLine("RunDataReaderTest started ...");
            try
            {
                db.ConnectionString = frm.cboConnectionString.Text;
                db.OpenConnection();

                _msg.Length = 0;
                _msg.Append("Connection string is ");
                _msg.Append(db.ConnectionString);
                _msg.Append("\r\n");
                _msg.Append("Connection state is  ");
                _msg.Append(db.Connection.State.ToString());
                _msg.Append("\r\n");
                _msg.Append("Query text is:\r\n");
                _msg.Append(frm.txtSqlQuery.Text.ToString());
                Program._messageLog.WriteLine(_msg.ToString());

                db.returnResult += new PFOdbc.ResultDelegate(OutputResults);
                db.SQLQuery = frm.txtSqlQuery.Text;
                db.CommandType = CommandType.Text;
                OdbcDataReader rdr = (OdbcDataReader)db.RunQueryDataReader();
                db.ProcessDataReader(rdr);
                rdr.Close();
                db.returnResult -= OutputResults;

                db.returnResultAsString += new PFOdbc.ResultAsStringDelegate(OutputResultsToFile);
                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\OdbcRdrDelimited.txt", PFFileOpenOperation.OpenFileForWrite);
                rdr = (OdbcDataReader)db.RunQueryDataReader();
                db.ExtractDelimitedDataFromDataReader(rdr, ",", "\r\n", true);
                rdr.Close();

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\OdbcRdrFixedLength.txt", PFFileOpenOperation.OpenFileForWrite);
                rdr = (OdbcDataReader)db.RunQueryDataReader();
                db.ExtractFixedLengthDataFromDataReader(rdr, true, true, false);
                rdr.Close();

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();

                rdr = (OdbcDataReader)db.RunQueryDataReader();
                db.SaveDataReaderToXmlFile(rdr, @"c:\temp\OdbcTestrdr.xml");
                rdr.Close();
                rdr = (OdbcDataReader)db.RunQueryDataReader();
                db.SaveDataReaderWithSchemaToXmlFile(rdr, @"c:\temp\OdbcTestrdrplus.xml");
                rdr.Close();
                rdr = (OdbcDataReader)db.RunQueryDataReader();
                db.SaveDataReaderToXmlSchemaFile(rdr, @"c:\temp\OdbcTestrdr.xsd");
                rdr.Close();


                rdr = (OdbcDataReader)db.RunQueryDataReader();
                PFDataProcessor dataProcessor = new PFDataProcessor();
                XmlDocument xmlDoc = dataProcessor.CopyDataTableToXmlDocument(db.ConvertDataReaderToDataTable(rdr));
                Program._messageLog.WriteLine("\r\n" + xmlDoc.OuterXml + "\r\n");
                rdr.Close();


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                if (db.Connection.State == ConnectionState.Open)
                    db.CloseConnection();
                Program._messageLog.WriteLine("... RunDataReaderTest finished.");

            }
        }


        public static void RunDataTableTest(MainForm frm)
        {
            PFOdbc db = new PFOdbc();

            Program._messageLog.WriteLine("RunDataTableTest started ...");
            try
            {
                db.ConnectionString = frm.cboConnectionString.Text;
                db.OpenConnection();

                _msg.Length = 0;
                _msg.Append("Connection string is ");
                _msg.Append(db.ConnectionString);
                _msg.Append("\r\n");
                _msg.Append("Connection state is  ");
                _msg.Append(db.Connection.State.ToString());
                _msg.Append("\r\n");
                _msg.Append("Query text is:\r\n");
                _msg.Append(frm.txtSqlQuery.Text.ToString());
                Program._messageLog.WriteLine(_msg.ToString());

                db.returnResult += new PFOdbc.ResultDelegate(OutputResults);
                db.SQLQuery = frm.txtSqlQuery.Text;
                db.CommandType = CommandType.Text;
                DataTable tab = db.RunQueryDataTable();
                db.ProcessDataTable(tab);
                db.returnResult -= OutputResults;

                db.returnResultAsString += new PFOdbc.ResultAsStringDelegate(OutputResultsToFile);
                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\OdbcDtDelimited.txt", PFFileOpenOperation.OpenFileForWrite);
                tab = db.RunQueryDataTable();
                db.ExtractDelimitedDataFromTable(tab, "\t", "\r\n", true);

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\OdbcDtFixedLength.txt", PFFileOpenOperation.OpenFileForWrite);
                tab = db.RunQueryDataTable();
                db.ExtractFixedLengthDataFromTable(tab, true, true, false);

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();

                tab = db.RunQueryDataTable();
                tab.TableName = "TestTab007z";
                db.SaveDataTableToXmlFile(tab, @"c:\temp\OdbcTestDt.xml");
                tab = db.RunQueryDataTable();
                db.SaveDataTableWithSchemaToXmlFile(tab, @"c:\temp\OdbcTestDtPlus.xml");
                tab = db.RunQueryDataTable();
                db.SaveDataTableToXmlSchemaFile(tab, @"c:\temp\OdbcTestDt.xsd");


                tab = db.RunQueryDataTable();
                PFDataProcessor dataProcessor = new PFDataProcessor();
                tab.TableName = "TestTab001x";
                XmlDocument xmlDoc = dataProcessor.CopyDataTableToXmlDocument(tab);
                Program._messageLog.WriteLine("\r\n" + xmlDoc.OuterXml + "\r\n");


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                if (db.Connection.State == ConnectionState.Open)
                    db.CloseConnection();
                Program._messageLog.WriteLine("... RunDataTableTest finished.");

            }
        }

        public static void RunDataSetTest(MainForm frm)
        {
            PFOdbc db = new PFOdbc();

            Program._messageLog.WriteLine("RunDataSetTest started ...");
            try
            {
                db.ConnectionString = frm.cboConnectionString.Text;
                db.OpenConnection();

                _msg.Length = 0;
                _msg.Append("Connection string is ");
                _msg.Append(db.ConnectionString);
                _msg.Append("\r\n");
                _msg.Append("Connection state is  ");
                _msg.Append(db.Connection.State.ToString());
                _msg.Append("\r\n");
                _msg.Append("Query text is:\r\n");
                _msg.Append(frm.txtSqlQuery.Text.ToString());
                Program._messageLog.WriteLine(_msg.ToString());

                db.returnResult += new PFOdbc.ResultDelegate(OutputResults);
                db.SQLQuery = frm.txtSqlQuery.Text;
                db.CommandType = CommandType.Text;
                DataSet ds = db.RunQueryDataSet();
                db.ProcessDataSet(ds);
                db.returnResult -= OutputResults;

                db.returnResultAsString += new PFOdbc.ResultAsStringDelegate(OutputResultsToFile);
                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\OdbcDsDelimited.txt", PFFileOpenOperation.OpenFileForWrite);
                ds = db.RunQueryDataSet();
                db.ExtractDelimitedDataFromDataSet(ds, ",", "\r\n", true);

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\OdbcDsFixedLength.txt", PFFileOpenOperation.OpenFileForWrite);
                ds = db.RunQueryDataSet();
                db.ExtractFixedLengthDataFromDataSet(ds, true, true, false);

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();

                ds = db.RunQueryDataSet();
                db.SaveDataSetToXmlFile(ds, @"c:\temp\OdbcTestDs.xml");
                ds = db.RunQueryDataSet();
                db.SaveDataSetWithSchemaToXmlFile(ds, @"c:\temp\OdbcTestDsPlus.xml");
                ds = db.RunQueryDataSet();
                db.SaveDataSetToXmlSchemaFile(ds, @"c:\temp\OdbcTestDs.xsd");


                ds = db.RunQueryDataSet();
                PFDataProcessor dataProcessor = new PFDataProcessor();
                XmlDocument xmlDoc = dataProcessor.CopyDataSetToXmlDocument(ds);
                Program._messageLog.WriteLine("\r\n" + xmlDoc.OuterXml + "\r\n");


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                //_msg.Append(AppGlobals.AppMessages.FormatErrorMessageWithStackTrace(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                if (db.Connection.State == ConnectionState.Open)
                    db.CloseConnection();
                Program._messageLog.WriteLine("... RunDataSetTest finished.");

            }
        }


        public static void OutputResults(DataColumnCollection cols, DataRow data, int tableNumber)
        {
            int inx = 0;
            int maxInx = cols.Count - 1;
            _msg.Length = 0;
            for (inx = 0; inx <= maxInx; inx++)
            {
                _msg.Append(cols[inx].ColumnName);
                _msg.Append(" (");
                _msg.Append(cols[inx].DataType.ToString());
                _msg.Append(")");
                _msg.Append(" = ");
                _msg.Append(data[inx].ToString());
                if (inx < maxInx)
                    _msg.Append(", ");
            }
            Program._messageLog.WriteLine(_msg.ToString());
        }


        public static void OutputResultsAsString(string outputLine, int tableNumber)
        {
            Program._messageLog.WriteLine(outputLine);
        }

        public static void OutputExtractFormattedData(string outputLine, int tableNumber)
        {
            if (outputLine.EndsWith("\r\n"))
            {
                Program._messageLog.WriteLine(outputLine.TrimEnd('\r', '\n'));
            }
            else
            {
                Program._messageLog.WriteLine(outputLine);
            }
        }

        public static void OutputResultsToFile(string outputLine, int tableNumber)
        {
            _textFile.WriteData(outputLine);
        }

        public static void CreateTableTest(MainForm frm)
        {
            PFOdbc odbc = new PFOdbc();
            IDatabaseProvider db = odbc;
            string connectionString = string.Empty;
            string createScript = string.Empty;
            string tableName = "TestTable01";
            StringBuilder sql = new StringBuilder();
            PFUnitTestDataTable unitTestDt01 = null;
            PFUnitTestDataTable unitTestDt02 = null;
            DataTable dt = null;
            DataTable dt2 = null;

            try
            {
                db.ConnectionString = frm.cboConnectionString.Text;
                if (frm.txtTableName.Text.Length > 0)
                    tableName = frm.txtTableName.Text;
                else
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify a table name for the CreateTableTest.");
                    throw new System.Exception(_msg.ToString());
                }

                connectionString = db.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                db.OpenConnection();


                //Get a list of the data mappings supported by the selected driver
                PFTableBuilder tabBuilder = new PFTableBuilder(DatabasePlatform.ODBC);
                List<DataTypeMapping> dataTypeMappings = tabBuilder.GetGenericDataTypeMappingsEx(db.ConnectionString);

                Program._messageLog.WriteLine("\r\nData Type Mappings:\r\n");
                if (dataTypeMappings != null)
                {
                    for (int i = 0; i < dataTypeMappings.Count; i++)
                    {
                        _msg.Length = 0;
                        _msg.Append(dataTypeMappings[i].DatabaseDataType);
                        _msg.Append(" is mapped to ");
                        _msg.Append(dataTypeMappings[i].DotNetDataType);
                        _msg.Append(" (");
                        _msg.Append(dataTypeMappings[i].dataTypeCategory);
                        _msg.Append(") MaxLen = ");
                        _msg.Append(dataTypeMappings[i].MaxLength.ToString());
                        Program._messageLog.WriteLine(_msg.ToString());
                    }
                }

                Program._messageLog.WriteLine(string.Empty);

               
                //first delete table if it already exists
                Program._messageLog.WriteLine("\r\nDropping old table if it exists ...");

                string catalogName = string.Empty;
                string schemaName = string.Empty;
                string tabName = string.Empty;
                string[] parsedTableName = tableName.Split('.');
                if (parsedTableName.Length == 2)
                {
                    schemaName = parsedTableName[0];
                    tabName = parsedTableName[1];
                }
                else if (parsedTableName.Length == 1)
                    tabName = parsedTableName[0];
                else if (parsedTableName.Length == 3)
                {
                    catalogName = parsedTableName[0];
                    schemaName = parsedTableName[1];
                    tabName = parsedTableName[2];
                }
                else
                    tabName = string.Empty;


                if (db.TableExists(catalogName, schemaName, tabName))
                {
                    bool dropped = db.DropTable(catalogName, schemaName, tabName);
                    if (dropped == false)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to drop table ");
                        if (catalogName != string.Empty)
                        {
                            _msg.Append(catalogName);
                            _msg.Append(".");
                        }
                        if (schemaName != string.Empty)
                        {
                            _msg.Append(schemaName);
                            _msg.Append(".");
                        }
                        _msg.Append(tabName);
                        throw new DataException(_msg.ToString());
                    }
                }

                if (db.TableExists(catalogName, schemaName, tabName + "_02"))
                {
                    bool dropped = db.DropTable(catalogName, schemaName, tabName + "_02");
                    if (dropped == false)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to drop table ");
                        if (catalogName != string.Empty)
                        {
                            _msg.Append(catalogName);
                            _msg.Append(".");
                        }
                        if (schemaName != string.Empty)
                        {
                            _msg.Append(schemaName);
                            _msg.Append(".");
                        }
                        _msg.Append(tabName + "_02");
                        throw new DataException(_msg.ToString());
                    }
                }


                unitTestDt01 = new PFUnitTestDataTable(db, schemaName, tabName, true);
                unitTestDt02 = new PFUnitTestDataTable(db, schemaName, tabName + "_02", true);

                _msg.Length = 0;
                _msg.Append("Initializing TableColumns");
                Program._messageLog.WriteLine(_msg.ToString());

                //select which data types to include
                List<KeyValuePair<string, string>> dataTypesToInclude = new List<KeyValuePair<string, string>>();

                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Int32", "1"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.String", "this is a string value"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Int32", "1123456789"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.UInt32", "3123456789"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Int64", "23123456789"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.UInt64", "8881234567889"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Int16", "11123"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.UInt16", "52432"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Double", "123456.7654"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Single", "321.234"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Decimal", "2123456789.22"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Char", "A"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Char[]", "ABCDEFGH"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Byte", "254"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.SByte", "125"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Byte[]", "UVWZYZ));"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Boolean", "true"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Object", "This is an object: be careful!"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.DateTime", "5/31/2013 13:54:25"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Guid", "58a4a08d-6101-4393-86dc-b2a8db46ec0f"));

                unitTestDt01.SetDataTypesToInclude(dataTypesToInclude);
                unitTestDt01.SetDataTypeOptions("System.String", false, true, 75);


                unitTestDt02.SetDataTypesToInclude(dataTypesToInclude);
                unitTestDt02.SetDataTypeOptions("System.String", false, true, 75000);

                //create the table

                _msg.Length = 0;
                _msg.Append("Creating tables");
                Program._messageLog.WriteLine(_msg.ToString());

                bool result = false;
                if (frm.chkUseOdbcBuilder.Checked)
                {
                    dt = unitTestDt01.GetDataTableFromTableColumns(true);
                    result = odbc.CreateTableUsingOdbcSyntax(dt, out createScript);
                    dt2 = unitTestDt02.GetDataTableFromTableColumns(true);
                    result = odbc.CreateTableUsingOdbcSyntax(dt2, out createScript);
                }
                else if (frm.chkUseCustomSQL.Checked)
                {
                    result = odbc.CreateTableUsingCustomScript(frm.txtSqlQuery.Text);
                    createScript = frm.txtSqlQuery.Text;
                }
                else
                {
                    unitTestDt01.CreateTableFromTableColumns();
                    unitTestDt02.CreateTableFromTableColumns();
                    createScript = unitTestDt01.TableCreateScript;
                }



                //import data to database

                _msg.Length = 0;
                _msg.Append("Importing data to TestTable01");
                Program._messageLog.WriteLine(_msg.ToString());

                if (frm.chkUseOdbcBuilder.Checked)
                {
                    odbc.ImportDataFromDataTable(dt);
                }
                else if (frm.chkUseCustomSQL.Checked)
                {
                    _msg.Length = 0;
                    _msg.Append("No import done for test table 01 created by custom script");
                    Program._messageLog.WriteLine(_msg.ToString());
                }
                else
                {
                    unitTestDt01.ImportTableToDatabase();
                }

                _msg.Length = 0;
                _msg.Append("Importing data to TestTable02");
                Program._messageLog.WriteLine(_msg.ToString());

                if (frm.chkUseOdbcBuilder.Checked)
                {
                    odbc.ImportDataFromDataTable(dt2);
                }
                else if (frm.chkUseCustomSQL.Checked)
                {
                    _msg.Length = 0;
                    _msg.Append("No import done for test table 02 created by custom script");
                    Program._messageLog.WriteLine(_msg.ToString());
                }
                else
                {
                    unitTestDt02.ImportTableToDatabase();
                }



                //retrieve just created table and see what data types get assigned to data table columns

                Program._messageLog.WriteLine("\r\nRead row just created for " + tableName + "\r\n");

                sql.Length = 0;
                sql.Append("select * from ");
                sql.Append(tableName);

                DataTable testTab = db.RunQueryDataTable(sql.ToString(), CommandType.Text);

                for (int c = 0; c < testTab.Columns.Count; c++)
                {
                    _msg.Length = 0;
                    _msg.Append(testTab.Columns[c].ColumnName);
                    _msg.Append(", ");
                    _msg.Append(testTab.Columns[c].DataType.FullName);
                    _msg.Append(", ");
                    _msg.Append(testTab.Columns[c].MaxLength.ToString());
                    Program._messageLog.WriteLine(_msg.ToString());
                }



                /*
                if (schemaRoot.TableExists(catalogName, schemaName, tabName))
                {
                    bool dropped = schemaRoot.DropTable(catalogName, schemaName, tabName);
                    if (dropped == false)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to drop table ");
                        if (catalogName != string.Empty)
                        {
                            _msg.Append(catalogName);
                            _msg.Append(".");
                        }
                        if (schemaName != string.Empty)
                        {
                            _msg.Append(schemaName);
                            _msg.Append(".");
                        }
                        _msg.Append(tabName);
                        throw new DataException(_msg.ToString());
                    }
                }



                Program._messageLog.WriteLine("\r\nCreating a table in the database ...");
                DataTable dt = new DataTable(tableName);
                DataColumn k1 = new DataColumn("K1", Type.GetType("System.Int32"));
                k1.AllowDBNull = false;
                dt.Columns.Add(k1);
                DataColumn f1 = new DataColumn("F1", Type.GetType("System.String"));
                f1.MaxLength = 50;
                dt.Columns.Add(f1);
                DataColumn f1x = new DataColumn("F1X", Type.GetType("System.String"));
                f1x.MaxLength = 500;
                dt.Columns.Add(f1x);
                DataColumn f1a = new DataColumn("F1A", Type.GetType("System.String"));
                f1a.MaxLength = 50000;
                dt.Columns.Add(f1a);
                DataColumn f2 = new DataColumn("F2", Type.GetType("System.Int32"));
                dt.Columns.Add(f2);
                DataColumn f2a = new DataColumn("F2A", Type.GetType("System.UInt32"));
                dt.Columns.Add(f2a);
                DataColumn f3 = new DataColumn("F3", Type.GetType("System.Int64"));
                dt.Columns.Add(f3);
                DataColumn f3a = new DataColumn("F3A", Type.GetType("System.UInt64"));
                dt.Columns.Add(f3a);
                DataColumn f4 = new DataColumn("F4", Type.GetType("System.Int16"));
                dt.Columns.Add(f4);
                DataColumn f4a = new DataColumn("F4A", Type.GetType("System.UInt16"));
                dt.Columns.Add(f4a);
                DataColumn f5 = new DataColumn("F5", Type.GetType("System.Double"));
                dt.Columns.Add(f5);
                DataColumn f6 = new DataColumn("F6", Type.GetType("System.Single"));
                dt.Columns.Add(f6);
                DataColumn f7 = new DataColumn("F7", Type.GetType("System.Decimal"));
                dt.Columns.Add(f7);
                DataColumn f8 = new DataColumn("F8", Type.GetType("System.Char"));
                dt.Columns.Add(f8);
                DataColumn f9 = new DataColumn("F9", Type.GetType("System.Byte"));
                dt.Columns.Add(f9);
                DataColumn f9a = new DataColumn("F9A", Type.GetType("System.SByte"));
                dt.Columns.Add(f9a);
                DataColumn f10 = new DataColumn("F10", Type.GetType("System.Boolean"));
                dt.Columns.Add(f10);
                DataColumn f11 = new DataColumn("F11", Type.GetType("System.Object"));
                dt.Columns.Add(f11);
                DataColumn f12 = new DataColumn("F12", Type.GetType("System.DateTime"));
                dt.Columns.Add(f12);
                DataColumn f13 = new DataColumn("F13", Type.GetType("System.Guid"));
                dt.Columns.Add(f13);
                DataColumn f14 = new DataColumn("F14", Type.GetType("System.Char[]"));
                dt.Columns.Add(f14);
                DataColumn f15 = new DataColumn("F15", Type.GetType("System.Byte[]"));
                dt.Columns.Add(f15);
                DataColumn f16 = new DataColumn("F16", Type.GetType("System.Char[]"));
                dt.Columns.Add(f16);
                DataColumn f17 = new DataColumn("F17", Type.GetType("System.Byte[]"));
                dt.Columns.Add(f17);


                //DatabasePlatform dbplat = DatabasePlatform.Unknown;

                //if (connectionString.Contains("Driver={Microsoft Access Driver"))
                //{
                //    dbplat = DatabasePlatform.MSAccess;
                //}
                //else if (connectionString.Contains("Driver={Oracle"))
                //{
                //    dbplat = DatabasePlatform.OracleNative;
                //}
                //else if (connectionString.Contains("Driver={Microsoft ODBC for Oracle"))
                //{
                //    dbplat = DatabasePlatform.MSOracle;
                //}
                //else if (connectionString.Contains("Driver={SQL Server")
                //         || connectionString.Contains("SQL Server"))
                //{
                //    dbplat = DatabasePlatform.MSSQLServer;
                //}
                //else if (connectionString.Contains("Driver={IBM DB2")
                //        || connectionString.Contains("IBM DB2"))
                //{
                //    dbplat = DatabasePlatform.DB2;
                //}
                //else if (connectionString.Contains("Driver={MySQL"))
                //{
                //    dbplat = DatabasePlatform.MySQL;
                //}
                //else
                //{
                //    dbplat = DatabasePlatform.ODBC;
                //}



                //PFTableBuilder tb = null;
                //if (frm.chkUseOdbcBuilder.Checked)
                //{
                //    tb = new PFTableBuilder(DatabasePlatform.ODBC);
                //    createScript = tb.GenericTableCreateStatement(dt, connectionString);
                //}
                //else if (dbplat != DatabasePlatform.MSAccess)
                //{
                //    tb = new PFTableBuilder(dbplat);
                //    createScript = tb.BuildTableCreateStatement(dt, connectionString);
                //}
                //else
                //{
                //    createScript = string.Empty;
                //}

                 
                bool result = false;
                if (frm.chkUseOdbcBuilder.Checked)
                {
                    result = odbc.CreateTableUsingOdbcSyntax(dt, out createScript);
                }
                else if (frm.chkUseCustomSQL.Checked)
                {
                    result = odbc.CreateTableUsingCustomScript(frm.txtSqlQuery.Text);
                }
                else
                {
                    result = odbc.CreateTable(dt, out createScript);
                }

 
                 
                 */


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if (db != null)
                    if (db.IsConnected)
                        db.CloseConnection();
                db = null;
                _msg.Length = 0;
                _msg.Append("SQL Script: \r\n");
                _msg.Append(createScript);
                Program._messageLog.WriteLine(_msg.ToString());
            }



        }

        public static void DataReaderToDataTableTest(MainForm frm)
        {
            PFOdbc db = new PFOdbc();
            string connectionString = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {

                connectionString = frm.cboConnectionString.Text;

                db.ConnectionString = connectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                if (frm.txtSqlQuery.Text.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                sw.Start();

                db.OpenConnection();

                db.SQLQuery = frm.txtSqlQuery.Text;
                db.CommandType = CommandType.Text;

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Open connection time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                OdbcDataReader rdr = (OdbcDataReader)db.RunQueryDataReader();
                DataTable tab = db.ConvertDataReaderToDataTable(rdr);
                Program._messageLog.WriteLine("Table columns count: " + tab.Columns.Count.ToString());
                rdr.Close();

                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    DataRow r = tab.Rows[i];
                    _msg.Length = 0;
                    int maxColInx = tab.Columns.Count - 1;
                    for (int ci = 0; ci <= maxColInx; ci++)
                    {
                        _msg.Append(tab.Columns[ci].ColumnName);
                        _msg.Append(": ");
                        _msg.Append(r[ci].ToString());
                        if (ci < maxColInx)
                            _msg.Append(", ");
                    }
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Table read time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                db.CloseConnection();
                db = null;
            }
        }

        public static void ImportDataTableTest(MainForm frm)
        {
            PFOdbc db = new PFOdbc();
            string connectionString = string.Empty;
            string originalTableName = string.Empty;
            DataTable dt = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("ImportDataTableTest started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                connectionString = frm.cboConnectionString.Text;

                db.ConnectionString = connectionString;


                db.OpenConnection();

                StringBuilder sql = new StringBuilder();
                dt = frm.keyValsDataSet.Tables["KeyValTable"];
                originalTableName = dt.TableName;
                string tableName = frm.txtTableName.Text;


                //first delete table if it already exists
                Program._messageLog.WriteLine("\r\nDropping old table if it exists ...");


                string catalogName = string.Empty;
                string schemaName = string.Empty;
                string tabName = string.Empty;

                catalogName = frm.txtCatalogName.Text;
                schemaName = frm.txtSchemaName.Text;
                tabName = "KeyValTable";
                if (schemaName.Trim().Length > 0)
                    dt.TableName = schemaName + "." + tabName;

                if (db.TableExists(catalogName, schemaName, tabName))
                {
                    bool dropped = db.DropTable(catalogName, schemaName, tabName);
                    if (dropped == false)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to drop table ");
                        if (catalogName.Trim().Length > 0)
                        {
                            _msg.Append(catalogName);
                            _msg.Append(".");
                        }
                        if (schemaName.Trim().Length > 0)
                        {
                            _msg.Append(schemaName);
                            _msg.Append(".");
                        }
                        _msg.Append(tabName);
                        throw new DataException(_msg.ToString());
                    }
                    else
                    {
                        _msg.Length = 0;
                        _msg.Append("Old table dropped: ");
                        if (catalogName.Trim().Length > 0)
                        {
                            _msg.Append(catalogName);
                            _msg.Append(".");
                        }
                        if (schemaName.Trim().Length > 0)
                        {
                            _msg.Append(schemaName);
                            _msg.Append(".");
                        }
                        _msg.Append(tabName);
                        Program._messageLog.WriteLine(_msg.ToString());
                    }
                }


                Program._messageLog.WriteLine("\r\nCreating a table in the database ...");

                //create the table
                bool tableCreated = db.CreateTable(dt);

                if (tableCreated)
                {
                    db.ImportDataFromDataTable(dt);
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("CreateTable for ");
                    _msg.Append(dt.TableName);
                    _msg.Append(" failed.");
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                db.CloseConnection();


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if (dt != null)
                    if (originalTableName.Length > 0)
                        dt.TableName = originalTableName;
                _msg.Length = 0;
                _msg.Append("... ImportDataTableTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void GetQueryDataSchema(MainForm frm)
        {
            PFOdbc db = new PFOdbc();

            Program._messageLog.WriteLine("GetQueryDataSchema started ...");
            try
            {
                db.ConnectionString = frm.cboConnectionString.Text;
                db.OpenConnection();

                _msg.Length = 0;
                _msg.Append("Connection string is ");
                _msg.Append(db.ConnectionString);
                _msg.Append("\r\n");
                _msg.Append("Connection state is  ");
                _msg.Append(db.Connection.State.ToString());
                _msg.Append("\r\n");
                _msg.Append("Query text is:\r\n");
                _msg.Append(frm.txtSqlQuery.Text.ToString());
                Program._messageLog.WriteLine(_msg.ToString());

                db.SQLQuery = frm.txtSqlQuery.Text;
                db.CommandType = CommandType.Text;
                
                DataTable tab = db.GetQueryDataSchema();
                foreach (DataColumn col in tab.Columns)
                {
                    _msg.Length = 0;
                    _msg.Append(col.ColumnName);
                    _msg.Append(", ");
                    _msg.Append(col.DataType.ToString());
                    Program._messageLog.WriteLine(_msg.ToString());
                }



            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if (db.Connection.State == ConnectionState.Open)
                    db.CloseConnection();
                Program._messageLog.WriteLine("... GetQueryDataSchema finished.");

            }
        }


    }//end class
}//end namespace
