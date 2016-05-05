using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Xml;
using PFTimers;
using PFTextFiles;
using PFDataAccessObjects;
using PFUnitTestDataObjects;
using PFCollectionsObjects;
using PFListObjects;

namespace TestprogSQL
{
    public class Tests
    {
        private static StringBuilder _msg = new StringBuilder();
        private static StringBuilder _str = new StringBuilder();
        private static bool _saveErrorMessagesToAppLog = false;
        private static MainForm _frm = null;

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

        public static MainForm MainForm
        {
            get
            {
                return Tests._frm;
            }
            set
            {
                Tests._frm = value;
            }
        }

        //tests
        //primary test routines
        public static void MessageLogTest(int testCounter)
        {
            _msg.Length = 0;
            _msg.Append(testCounter.ToString("#,##0"));
            _msg.Append(": ");
            _msg.Append("Current date/time is ");
            _msg.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            Program._messageLog.WriteLine(_msg.ToString());
        }//end MessageLogTest

        public static void ConnectionStringTest()
        {
            PFSQLServer sqlserv = new PFSQLServer();
            string connectionString = string.Empty;

            try
            {
                sqlserv.ServerName = _frm.txtServerName.Text;
                sqlserv.DatabaseName = _frm.txtDatabaseName.Text;
                sqlserv.UseIntegratedSecurity = _frm.chkUseIntegratedSecurity.Checked;
                sqlserv.AsynchronousProcessing = _frm.chkUseAsyncProcessing.Checked;
                sqlserv.ApplicationName = _frm.txtApplicationName.Text;
                sqlserv.WorkstationId = _frm.txtWorkstationId.Text;
                sqlserv.Username = _frm.txtUsername.Text;
                sqlserv.Password = _frm.txtPassword.Text;

                connectionString = sqlserv.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                Program._messageLog.WriteLine(Environment.NewLine);
                foreach (stKeyValuePair<string, string> kv in sqlserv.ConnectionStringKeyVals)
                {
                    _msg.Length = 0;
                    _msg.Append(kv.Key + "=" + kv.Value);
                    Program._messageLog.WriteLine(_msg.ToString());
                }
                Program._messageLog.WriteLine(Environment.NewLine);

            }
            catch (System.Exception ex)
            {
                _frm.OutputErrorMessageToLog(ex);
            }
            finally
            {
                ;
            }

        }

        public static void CreateTableTest()
        {
            PFSQLServer sqlserv = new PFSQLServer();
            string connectionString = string.Empty;
            string createScript = string.Empty;
            string tableName = "TestTable01";
            string schemaName = "dbo";
            StringBuilder sql = new StringBuilder();
            PFUnitTestDataTable unitTestDt01 = null;
            PFUnitTestDataTable unitTestDt02 = null;

            try
            {
                sqlserv.ServerName = _frm.txtServerName.Text;
                sqlserv.DatabaseName = _frm.txtDatabaseName.Text;
                sqlserv.UseIntegratedSecurity = _frm.chkUseIntegratedSecurity.Checked;
                sqlserv.AsynchronousProcessing = _frm.chkUseAsyncProcessing.Checked;
                sqlserv.ApplicationName = _frm.txtApplicationName.Text;
                sqlserv.WorkstationId = _frm.txtWorkstationId.Text;
                sqlserv.Username = _frm.txtUsername.Text;
                sqlserv.Password = _frm.txtPassword.Text;
                if (_frm.txtSchemaName.Text.Length > 0)
                    schemaName = _frm.txtSchemaName.Text;
                if (_frm.txtTableName.Text.Length > 0)
                    tableName = _frm.txtTableName.Text;

                connectionString = sqlserv.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                sqlserv.OpenConnection();

                IDatabaseProvider db = (IDatabaseProvider)sqlserv;

                unitTestDt01 = new PFUnitTestDataTable(db, "dbo", "TestTable01", true);
                unitTestDt02 = new PFUnitTestDataTable(db, "dbo", "TestTable02", true);

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

                unitTestDt01.CreateTableFromTableColumns();
                unitTestDt02.CreateTableFromTableColumns();

                createScript = unitTestDt01.TableCreateScript;

                //DataTable testTab = sqlserv.RunQueryDataTable("select * from TestTable01", CommandType.Text);
                //_msg.Length = 0;
                //_msg.Append("testTab row count: ");
                //_msg.Append(testTab.Rows.Count.ToString());
                //Program._messageLog.WriteLine(_msg.ToString());


                //import data to database

                _msg.Length = 0;
                _msg.Append("Importing data to TestTable01");
                Program._messageLog.WriteLine(_msg.ToString());

                unitTestDt01.ImportTableToDatabase();

                _msg.Length = 0;
                _msg.Append("Importing data to TestTable02");
                Program._messageLog.WriteLine(_msg.ToString());

                unitTestDt02.ImportTableToDatabase();


                //retrieve just created table and see what data types get assigned to data table columns

                Program._messageLog.WriteLine("\r\nRead row just created for TestTable01\r\n");

                sql.Length = 0;
                sql.Append("select * from ");
                sql.Append("dbo.TestTable01");

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
                //first delete table if it already exists
                Program._messageLog.WriteLine("\r\nDropping old table if it exists ...");

                _msg.Length = 0;
                _msg.Append("Table ");
                _msg.Append(schemaName);
                _msg.Append(".");
                _msg.Append(tableName);
                if (sqlserv.TableExists(schemaName, tableName))
                {
                    sqlserv.DropTable(schemaName, tableName);
                    if (sqlserv.TableExists(schemaName, tableName) == false)
                        _msg.Append(" dropped.");
                    else
                        _msg.Append(" drop failed.");
                }
                else
                {
                    _msg.Append(" does not exist.");
                }

                Program._messageLog.WriteLine(_msg.ToString());

                //sql.Length = 0;
                //sql.Append("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'");
                //sql.Append(tableName);
                //sql.Append("') AND type in (N'U'))");
                //sql.Append("\r\n");
                //sql.Append("DROP TABLE ");
                //sql.Append(tableName);
                //sql.Append(";");

                //Program._messageLog.WriteLine(sql.ToString());

                //int numRows = sqlserv.RunNonQuery(sql.ToString(),CommandType.Text);

                //_msg.Length = 0;
                //_msg.Append("Num rows affected on drop table: ");
                //_msg.Append(numRows.ToString());
                //Program._messageLog.WriteLine(_msg.ToString());

                Program._messageLog.WriteLine("\r\nCreating a table in the database ...");
                DataTable dt = new DataTable(schemaName + "." + tableName);
                DataColumn k1 = new DataColumn("K1", Type.GetType("System.Int32"));
                k1.AllowDBNull = false;
                dt.Columns.Add(k1);
                DataColumn f1 = new DataColumn("F1", Type.GetType("System.String"));
                f1.MaxLength = 50;
                dt.Columns.Add(f1);
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
                DataColumn f8a = new DataColumn("F8A", Type.GetType("System.Char[]"));
                dt.Columns.Add(f8a);
                DataColumn f9 = new DataColumn("F9", Type.GetType("System.Byte"));
                dt.Columns.Add(f9);
                DataColumn f9a = new DataColumn("F9A", Type.GetType("System.SByte"));
                dt.Columns.Add(f9a);
                DataColumn f9b = new DataColumn("F9B", Type.GetType("System.Byte[]"));
                dt.Columns.Add(f9b);
                DataColumn f10 = new DataColumn("F10", Type.GetType("System.Boolean"));
                dt.Columns.Add(f10);
                DataColumn f11 = new DataColumn("F11", Type.GetType("System.Object"));
                dt.Columns.Add(f11);
                DataColumn f12 = new DataColumn("F12", Type.GetType("System.DateTime"));
                dt.Columns.Add(f12);
                DataColumn f13 = new DataColumn("F13", Type.GetType("System.Guid"));
                dt.Columns.Add(f13);

                createScript = string.Empty;
                bool result = sqlserv.CreateTable(dt, out createScript);

                int maxColInx = dt.Columns.Count - 1;
                sql.Length = 0;
                sql.Append("insert into ");
                sql.Append(schemaName + "." + tableName);
                sql.Append(" (");
                for (int colInx = 0; colInx <= maxColInx; colInx++)
                {
                    sql.Append(dt.Columns[colInx].ColumnName);
                    if (colInx < maxColInx)
                        sql.Append(", ");
                }
                sql.Append(")");
                sql.Append(" VALUES (");
                sql.Append("1, 'short string value', 'longer string value', 1123456789, 3123456789, ");
                sql.Append("23123456789, 8881234567889, 11123, 52432, 123456.7654, 321.234, 2123456789.22, ");
                sql.Append("'A', 'ABCDEFGH', 254, 125, CONVERT(varbinary(5),'UVWZYZ'), 1, 'This is an object: be careful!', '5/31/2013 13:54:25', '58a4a08d-6101-4393-86dc-b2a8db46ec0f'");
                sql.Append(");");

                _msg.Length = 0;
                _msg.Append("Insert statement: ");
                _msg.Append(sql.ToString());
                Program._messageLog.WriteLine(_msg.ToString());


                int numRows = sqlserv.RunNonQuery(sql.ToString(), CommandType.Text);

                _msg.Length = 0;
                _msg.Append("Num rows inserted: ");
                _msg.Append(numRows.ToString());
                Program._messageLog.WriteLine(_msg.ToString());
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
                _msg.Length = 0;
                _msg.Append("SQL Script: \r\n");
                _msg.Append(createScript);
                Program._messageLog.WriteLine(_msg.ToString());
                sqlserv.CloseConnection();
                sqlserv = null;
            }
                 
        

        }

        public static void DataReaderTest()
        {
            PFSQLServer sqlserv = new PFSQLServer();
            string connectionString = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {
                sqlserv.ServerName = _frm.txtServerName.Text;
                sqlserv.DatabaseName = _frm.txtDatabaseName.Text;
                sqlserv.UseIntegratedSecurity = _frm.chkUseIntegratedSecurity.Checked;
                sqlserv.AsynchronousProcessing = _frm.chkUseAsyncProcessing.Checked;
                sqlserv.ApplicationName = _frm.txtApplicationName.Text;
                sqlserv.WorkstationId = _frm.txtWorkstationId.Text;
                sqlserv.Username = _frm.txtUsername.Text;
                sqlserv.Password = _frm.txtPassword.Text;

                connectionString = sqlserv.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                if (_frm.txtSQLQuery.Text.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                sw.Start();

                sqlserv.OpenConnection();

                sqlserv.SQLQuery = _frm.txtSQLQuery.Text;
                if (_frm.chkIsStoredProcedure.Checked)
                    sqlserv.CommandType = CommandType.StoredProcedure;
                else
                    sqlserv.CommandType = CommandType.Text;

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Open connection time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                
                Program._messageLog.WriteLine("\r\nRunning data extract tests ...\r\n");
                sqlserv.returnResultAsString += new PFSQLServer.ResultAsStringDelegate(OutputResultsToFile);
                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\ReaderDelimitedTestExtract.txt", PFFileOpenOperation.OpenFileForWrite);
                sw.Start();
                SqlDataReader rdr = (SqlDataReader)sqlserv.RunQueryDataReader();
                sqlserv.ExtractDelimitedDataFromDataReader(rdr, ",", "\r\n", true);
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Extract Delimiated Dataset time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\ReaderFixedLengthTestExtract.txt", PFFileOpenOperation.OpenFileForWrite);
                rdr.Close();

                sw.Start();
                rdr = (SqlDataReader)sqlserv.RunQueryDataReader();
                sqlserv.ExtractFixedLengthDataFromDataReader(rdr, true, true, false);
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Extract Fixed Length Dataset time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                rdr.Close();

                rdr = (SqlDataReader)sqlserv.RunQueryDataReader();
                sqlserv.SaveDataReaderToXmlFile(rdr, @"c:\temp\Testrdr.xml");
                rdr.Close();
                rdr = (SqlDataReader)sqlserv.RunQueryDataReader();
                sqlserv.SaveDataReaderWithSchemaToXmlFile(rdr, @"c:\temp\Testrdrplus.xml");
                rdr.Close();
                rdr = (SqlDataReader)sqlserv.RunQueryDataReader();
                sqlserv.SaveDataReaderToXmlSchemaFile (rdr, @"c:\temp\Testrdr.xsd");
                rdr.Close();


                rdr = (SqlDataReader)sqlserv.RunQueryDataReader();
                PFDataProcessor dataProcessor = new PFDataProcessor();
                XmlDocument xmlDoc = dataProcessor.CopyDataTableToXmlDocument(sqlserv.ConvertDataReaderToDataTable(rdr));
                Program._messageLog.WriteLine("\r\n" + xmlDoc.OuterXml + "\r\n");
                rdr.Close();

            }
            catch (System.Exception ex)
            {
                _frm.OutputErrorMessageToLog(ex);
            }
            finally
            {
                sqlserv.CloseConnection();
                sqlserv = null;
            }

        }

        public static void DataReaderToDataTableTest()
        {
            PFSQLServer sqlserv = new PFSQLServer();
            string connectionString = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {
                sqlserv.ServerName = _frm.txtServerName.Text;
                sqlserv.DatabaseName = _frm.txtDatabaseName.Text;
                sqlserv.UseIntegratedSecurity = _frm.chkUseIntegratedSecurity.Checked;
                sqlserv.AsynchronousProcessing = _frm.chkUseAsyncProcessing.Checked;
                sqlserv.ApplicationName = _frm.txtApplicationName.Text;
                sqlserv.WorkstationId = _frm.txtWorkstationId.Text;
                sqlserv.Username = _frm.txtUsername.Text;
                sqlserv.Password = _frm.txtPassword.Text;

                connectionString = sqlserv.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                if (_frm.txtSQLQuery.Text.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                sw.Start();

                sqlserv.OpenConnection();

                sqlserv.SQLQuery = _frm.txtSQLQuery.Text;
                if (_frm.chkIsStoredProcedure.Checked)
                    sqlserv.CommandType = CommandType.StoredProcedure;
                else
                    sqlserv.CommandType = CommandType.Text;

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Open connection time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                SqlDataReader rdr = (SqlDataReader)sqlserv.RunQueryDataReader();
                DataTable tab = sqlserv.ConvertDataReaderToDataTable(rdr);
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
                _frm.OutputErrorMessageToLog(ex);
            }
            finally
            {
                sqlserv.CloseConnection();
                sqlserv = null;
            }
        }

        public static void DataTableTest()
        {
            PFSQLServer sqlserv = new PFSQLServer();
            string connectionString = string.Empty;
            Stopwatch sw = new Stopwatch();
            PFDataExporter dex = new PFDataExporter();
            PFDelimitedDataLine delimitedLine = null;
            PFFixedLengthDataLine fixedLenLine = null;

            try
            {
                sqlserv.ServerName = _frm.txtServerName.Text;
                sqlserv.DatabaseName = _frm.txtDatabaseName.Text;
                sqlserv.UseIntegratedSecurity = _frm.chkUseIntegratedSecurity.Checked;
                sqlserv.AsynchronousProcessing = _frm.chkUseAsyncProcessing.Checked;
                sqlserv.ApplicationName = _frm.txtApplicationName.Text;
                sqlserv.WorkstationId = _frm.txtWorkstationId.Text;
                sqlserv.Username = _frm.txtUsername.Text;
                sqlserv.Password = _frm.txtPassword.Text;

                connectionString = sqlserv.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                if (_frm.txtSQLQuery.Text.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                sw.Start();

                sqlserv.OpenConnection();

                sqlserv.SQLQuery = _frm.txtSQLQuery.Text;
                if (_frm.chkIsStoredProcedure.Checked)
                    sqlserv.CommandType = CommandType.StoredProcedure;
                else
                    sqlserv.CommandType = CommandType.Text;

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Open connection time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                DataTable tab1 = sqlserv.RunQueryDataTable();
                sqlserv.returnResult += new PFSQLServer.ResultDelegate(OutputResults);
                sqlserv.ProcessDataTable(tab1);

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Process Table time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());


                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\TableTestExtract.txt", PFFileOpenOperation.OpenFileForWrite);
                
                //sw.Start();
                DataTable tab = sqlserv.RunQueryDataTable();
                //sqlserv.returnResultAsString += new PFSQLServer.ResultAsStringDelegate(OutputExtractFormattedData);
                sqlserv.returnResultAsString += new PFSQLServer.ResultAsStringDelegate(OutputResultsToFile);
                sqlserv.ExtractDelimitedDataFromTable(tab, ",", "\r\n", true);
                delimitedLine = dex.GetDelimitedLineDefinitionFromTable(tab, ",", "\r\n", true);
                delimitedLine.SaveToXmlFile(@"c:\temp\TableTestExtractLineFormat.xml");
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Extract Delimiated Table time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());
                tab = null;
                sw.Start();

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\TableTestExtractFxl.txt", PFFileOpenOperation.OpenFileForWrite);
                tab = sqlserv.RunQueryDataTable(); 
                sqlserv.ExtractFixedLengthDataFromTable(tab, true, true, false);
                fixedLenLine = dex.GetFixedLengthLineDefinitionFromTable(tab, true, true, false);
                fixedLenLine.SaveToXmlFile(@"c:\temp\TableTestExtractFxlLineFormat.xml");
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Extract Fixed Length Table time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\TableTestExtractFxlNoCrLf.txt", PFFileOpenOperation.OpenFileForWrite);
                tab = sqlserv.RunQueryDataTable();
                sqlserv.ExtractFixedLengthDataFromTable(tab, false, true, false);
                fixedLenLine = dex.GetFixedLengthLineDefinitionFromTable(tab, false, true, false);
                fixedLenLine.SaveToXmlFile(@"c:\temp\TableTestExtractFxlNoCrLfLineFormat.xml");
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Extract Fixed Length (No CrLf) Table time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());


                sqlserv.SaveDataTableToXmlSchemaFile(tab, @"c:\temp\Testtab.xsd");
                sqlserv.SaveDataTableToXmlFile(tab, @"c:\temp\Testtab.xml");
                sqlserv.SaveDataTableWithSchemaToXmlFile(tab, @"c:\temp\Testtabplus.xml");
                DataTable tab2 = sqlserv.LoadXmlFileToDataTable(@"c:\temp\Testtabplus.xml"); ;
                int numRows = tab2.Rows.Count;

                //StringWriter writer = new StringWriter();
                //tab.WriteXml(writer, XmlWriteMode.WriteSchema);
                //XmlDocument xmlDoc = new XmlDocument();
                //xmlDoc.LoadXml(writer.ToString());
                PFDataProcessor dataProcessor = new PFDataProcessor();
                XmlDocument xmlDoc = dataProcessor.CopyDataTableToXmlDocument(tab);
                Program._messageLog.WriteLine("\r\n" + xmlDoc.OuterXml + "\r\n");
            }
            catch (System.Exception ex)
            {
                _frm.OutputErrorMessageToLog(ex);
            }
            finally
            {
                sqlserv.CloseConnection();
                sqlserv = null;
                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
            }
        }//end DataTableTest

        public static void DataSetTest()
        {
            PFSQLServer sqlserv = new PFSQLServer();
            string connectionString = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {
                sqlserv.ServerName = _frm.txtServerName.Text;
                sqlserv.DatabaseName = _frm.txtDatabaseName.Text;
                sqlserv.UseIntegratedSecurity = _frm.chkUseIntegratedSecurity.Checked;
                sqlserv.AsynchronousProcessing = _frm.chkUseAsyncProcessing.Checked;
                sqlserv.ApplicationName = _frm.txtApplicationName.Text;
                sqlserv.WorkstationId = _frm.txtWorkstationId.Text;
                sqlserv.Username = _frm.txtUsername.Text;
                sqlserv.Password = _frm.txtPassword.Text;

                connectionString = sqlserv.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                if (_frm.txtSQLQuery.Text.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                sqlserv.OpenConnection();

                sqlserv.SQLQuery = _frm.txtSQLQuery.Text;
                if (_frm.chkIsStoredProcedure.Checked)
                    sqlserv.CommandType = CommandType.StoredProcedure;
                else
                    sqlserv.CommandType = CommandType.Text;

                sw.Start();
                DataSet ds1 = sqlserv.RunQueryDataSet();
                sqlserv.returnResult +=  new PFSQLServer.ResultDelegate(OutputResults);
                sqlserv.ProcessDataSet(ds1);
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Process Dataset time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                //Run data extract test
                Program._messageLog.WriteLine("\r\nRunning data extract tests ...\r\n");
                sqlserv.returnResultAsString += new PFSQLServer.ResultAsStringDelegate(OutputResultsToFile);
                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\DatasetDelimitedTestExtract.txt", PFFileOpenOperation.OpenFileForWrite);
                sw.Start();
                DataSet ds = sqlserv.RunQueryDataSet();
                sqlserv.ExtractDelimitedDataFromDataSet(ds, "~", "\r\n", true);
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Extract Delimiated Dataset time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\DatasetFixedLengthTestExtract.txt", PFFileOpenOperation.OpenFileForWrite);
                ds = null;

                sw.Start();
                ds = sqlserv.RunQueryDataSet();
                sqlserv.ExtractFixedLengthDataFromDataSet(ds, true, true, false);
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Extract Fixed Length Dataset time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                sqlserv.SaveDataSetToXmlSchemaFile(ds, @"c:\temp\Testds.xsd");
                sqlserv.SaveDataSetToXmlFile(ds, @"c:\temp\Testds.xml");
                sqlserv.SaveDataSetWithSchemaToXmlFile(ds, @"c:\temp\Testdsplus.xml");
                DataSet ds2 = sqlserv.LoadXmlFileToDataSet(@"c:\temp\Testds.xml"); ;
                int numRows = ds2.Tables[0].Rows.Count;


                PFDataProcessor dataProcessor = new PFDataProcessor();
                XmlDocument xmlDoc = dataProcessor.CopyDataSetToXmlDocument(ds);
                Program._messageLog.WriteLine(xmlDoc.OuterXml);
                
                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                ds = null;


            }
            catch (System.Exception ex)
            {
                _frm.OutputErrorMessageToLog(ex);
            }
            finally
            {
                sqlserv.CloseConnection();
                sqlserv = null;
            }

        }

        //Quick test routines

        public static void RunQuickTest1()
        {
            int cnt = 0;
            int maxCnt = 10;
            for (cnt = 1; cnt <= maxCnt; cnt++)
            {
                _msg.Length = 0;
                _msg.Append("Test counter #");
                _msg.Append(cnt.ToString("#,##0"));
                Program._messageLog.WriteLine(_msg.ToString());
            }
            //throw a test error
            _msg.Length = 0;
            _msg.Append("Test error thrown by RunQuickTest1");
            throw new System.Exception(_msg.ToString());
        }

        public static void RunQuickTest2()
        {
            PFSQLServer sqlserv = new PFSQLServer();
            string connectionString = string.Empty;

            sqlserv.ServerName = "profastws1";
            sqlserv.DatabaseName = "Namelists";
            sqlserv.UseIntegratedSecurity = true;
            sqlserv.AsynchronousProcessing = true;
            connectionString = sqlserv.ConnectionString;
            _msg.Length = 0;
            _msg.Append("Connection string is:\r\n");
            _msg.Append(sqlserv.ConnectionString);
            Program._messageLog.WriteLine(_msg.ToString());

            sqlserv.DatabaseName = "AdventureWorksDW";
            sqlserv.AsynchronousProcessing = false;
            connectionString = sqlserv.ConnectionString;
            _msg.Length = 0;
            _msg.Append("Connection string is:\r\n");
            _msg.Append(sqlserv.ConnectionString);
            Program._messageLog.WriteLine(_msg.ToString());

            sqlserv = null;
            Program._messageLog.WriteLine("QuickTest2 finished.");

        }

        public static void RunQuickTest3()
        {
            string sqlQuery = _frm.txtSQLQuery.Text;
            string connectionString = @"Data Source=PROFASTWS3;Initial Catalog=AdventureWorksDW2008R2;Integrated Security=True;Application Name=TestprogSQL;Workstation ID=PROFASTWS5";
            CommandType pCommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            SqlConnection _conn = new SqlConnection(connectionString);
            SqlCommand _cmd = new SqlCommand();
            System.Data.CommandType _commandType = CommandType.Text;
            int _commandTimeout = 300;
            string _sqlQuery = sqlQuery;

            _cmd.Connection = _conn;
            _cmd.CommandType = pCommandType;
            _cmd.CommandTimeout = _commandTimeout;
            _cmd.CommandText = sqlQuery;
            _commandType = pCommandType;
            _sqlQuery = sqlQuery;

            da.SelectCommand = _cmd;
            da.FillSchema(dt, SchemaType.Source);
         
            da.Fill(dt);

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
            if(outputLine.EndsWith("\r\n"))
            {
                Program._messageLog.WriteLine(outputLine.TrimEnd('\r','\n'));
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


        public static void ImportDataTableTest()
        {
            PFSQLServer db = new PFSQLServer();
            string connectionString = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("ImportDataTableTest started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                db.ServerName = _frm.txtServerName.Text;
                db.DatabaseName = _frm.txtDatabaseName.Text;
                db.UseIntegratedSecurity = _frm.chkUseIntegratedSecurity.Checked;
                db.AsynchronousProcessing = _frm.chkUseAsyncProcessing.Checked;
                db.ApplicationName = _frm.txtApplicationName.Text;
                db.WorkstationId = _frm.txtWorkstationId.Text;
                db.Username = _frm.txtUsername.Text;
                db.Password = _frm.txtPassword.Text;

                connectionString = db.ConnectionString;

                

                db.OpenConnection();

                StringBuilder sql = new StringBuilder();
                DataTable dt = _frm.keyValsDataSet.Tables["dbo.KeyValTable"];
                string schemaName = "dbo";
                string tableName = "KeyValTable";
                

                //first delete table if it already exists
                _msg.Length = 0;
                _msg.Append("\r\nTable ");
                _msg.Append(dt.TableName);
                if (db.TableExists(schemaName, tableName))
                {
                    db.DropTable(schemaName, tableName);
                    if (db.TableExists(schemaName, tableName) == false)
                        _msg.Append(" dropped.");
                    else
                        _msg.Append(" drop failed.");
                }
                else
                {
                    _msg.Append(" does not exist.");
                }

                Program._messageLog.WriteLine(_msg.ToString());

                Program._messageLog.WriteLine("\r\nCreating a table in the database ...");

                //create the table
                db.CreateTable(dt);

                db.ImportDataFromDataTable(dt, 1000);

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
                _msg.Length = 0;
                _msg.Append("... ImportDataTableTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void GetQueryDataSchema()
        {
            PFSQLServer sqlserv = new PFSQLServer();
            string connectionString = string.Empty;
            Stopwatch sw = new Stopwatch();
            
            try
            {
                _msg.Length = 0;
                _msg.Append("GetQueryDataSchema started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                sqlserv.ServerName = _frm.txtServerName.Text;
                sqlserv.DatabaseName = _frm.txtDatabaseName.Text;
                sqlserv.UseIntegratedSecurity = _frm.chkUseIntegratedSecurity.Checked;
                sqlserv.AsynchronousProcessing = _frm.chkUseAsyncProcessing.Checked;
                sqlserv.ApplicationName = _frm.txtApplicationName.Text;
                sqlserv.WorkstationId = _frm.txtWorkstationId.Text;
                sqlserv.Username = _frm.txtUsername.Text;
                sqlserv.Password = _frm.txtPassword.Text;

                connectionString = sqlserv.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                if (_frm.txtSQLQuery.Text.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                sw.Start();

                sqlserv.OpenConnection();

                sqlserv.SQLQuery = _frm.txtSQLQuery.Text;
                if (_frm.chkIsStoredProcedure.Checked)
                    sqlserv.CommandType = CommandType.StoredProcedure;
                else
                    sqlserv.CommandType = CommandType.Text;

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Open connection time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                DataTable tab = sqlserv.GetQueryDataSchema();

                foreach (DataColumn col in tab.Columns)
                {
                    _msg.Length = 0;
                    _msg.Append(col.ColumnName);
                    _msg.Append(", ");
                    _msg.Append(col.DataType.ToString());
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Get Query Schema time: ");
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
                _msg.Length = 0;
                _msg.Append("\r\n... GetQueryDataSchema finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        


    }//end class
}//end namespace
