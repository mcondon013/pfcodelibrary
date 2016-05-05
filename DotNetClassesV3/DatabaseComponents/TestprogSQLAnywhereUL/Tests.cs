using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Data.Common;
using iAnywhere.Data.UltraLite;
using PFTimers;
using PFTextFiles;
using PFDataAccessObjects;
using PFFileSystemObjects;
using System.Xml;
using PFUnitTestDataObjects;
using PFCollectionsObjects;
using PFListObjects;
using PFSQLAnywhereULObjects;

namespace TestprogSQLAnywhereUL
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
        public static void RunShowDateTimeTest()
        {
            int testCounter = 0;
            try
            {
                _msg.Length = 0;
                _msg.Append("RunShowDateTimeTest started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                testCounter++;
                _msg.Length = 0;
                _msg.Append(testCounter.ToString("#,##0"));
                _msg.Append(": ");
                _msg.Append("Current date/time is ");
                _msg.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
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
                _msg.Append("... RunShowDateTimeTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void GetStaticKeys()
        {

            try
            {
                _msg.Length = 0;
                _msg.Append("GetStaticKeys started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("StaticKeySection values:\r\n");
                _msg.Append("MainFormCaption = ");
                _msg.Append(StaticKeysSection.Settings.MainFormCaption);
                _msg.Append("\r\n");
                _msg.Append("MinAppThreads = ");
                _msg.Append(StaticKeysSection.Settings.MinAppThreads.ToString());
                _msg.Append("\r\n");
                _msg.Append("MaxAppThreads = ");
                _msg.Append(StaticKeysSection.Settings.MaxAppThreads.ToString());
                _msg.Append("\r\n");
                _msg.Append("RequireLogon = ");
                _msg.Append(StaticKeysSection.Settings.RequireLogon.ToString());
                _msg.Append("\r\n");
                _msg.Append("ValidBooleanValues = ");
                _msg.Append(StaticKeysSection.Settings.ValidBooleanValues);
                _msg.Append("\r\n");
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
                _msg.Append("\r\n... GetStaticKeys finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void ConnectionStringTest()
        {
            PFSQLAnywhereUL db = new PFSQLAnywhereUL();
            string connectionString = string.Empty;

            try
            {
                db.DatabasePath = _frm.txtDatabaseFile.Text;
                db.DatabaseName = _frm.txtDatabaseName.Text;
                db.Username = _frm.txtUsername.Text;
                db.Password = _frm.txtPassword.Text;

                db.DatabaseKey = _frm.txtDatabaseKey.Text;

                connectionString = db.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                db.OpenConnection();

                _msg.Length = 0;
                if (db.IsConnected)
                    _msg.Append("schemaRoot is connected");
                else
                    _msg.Append("schemaRoot connection failed!");
                Program._messageLog.WriteLine(_msg.ToString());

                Program._messageLog.WriteLine(Environment.NewLine);
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
                _frm.OutputErrorMessageToLog(ex);
            }
            finally
            {
                if (db != null)
                    if (db.IsConnected)
                        db.CloseConnection();
                db = null;
            }

        }


        public static void CreateTableTest()
        {
            PFSQLAnywhereUL sqlu = new PFSQLAnywhereUL();
            string connectionString = string.Empty;
            string createScript = string.Empty;
            string tableName = "TestTable01";
            string tableName2 = "TestTable02";
            StringBuilder sql = new StringBuilder();
            PFUnitTestDataTable unitTestDt01 = null;
            PFUnitTestDataTable unitTestDt02 = null;

            try
            {
                sqlu.DatabasePath = _frm.txtDatabaseFile.Text;
                sqlu.DatabaseName = _frm.txtDatabaseName.Text;
                sqlu.Username = _frm.txtUsername.Text;
                sqlu.Password = _frm.txtPassword.Text;
                sqlu.DatabaseKey = _frm.txtDatabaseKey.Text;

                if (_frm.txtTableName.Text.Length > 0)
                {
                    tableName = _frm.txtTableName.Text;
                    tableName2 = _frm.txtTableName.Text + "_02";
                }

                connectionString = sqlu.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                sqlu.OpenConnection();

                //first delete table if it already exists
                Program._messageLog.WriteLine("\r\nDropping old table if it exists ...");

                _msg.Length = 0;
                _msg.Append("Table ");
                _msg.Append(tableName);
                if (sqlu.TableExists(tableName))
                {
                    sqlu.DropTable(tableName);
                    if (sqlu.TableExists(tableName) == false)
                        _msg.Append(" dropped.");
                    else
                        _msg.Append(" drop failed.");
                }
                else
                {
                    _msg.Append(" does not exist.");
                }

                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("Table ");
                _msg.Append(tableName2);
                if (sqlu.TableExists(tableName2))
                {
                    sqlu.DropTable(tableName2);
                    if (sqlu.TableExists(tableName2) == false)
                        _msg.Append(" dropped.");
                    else
                        _msg.Append(" drop failed.");
                }
                else
                {
                    _msg.Append(" does not exist.");
                }

                Program._messageLog.WriteLine(_msg.ToString());


                IDatabaseProvider db = (IDatabaseProvider)sqlu;

                unitTestDt01 = new PFUnitTestDataTable(db, string.Empty, tableName, true);
                unitTestDt02 = new PFUnitTestDataTable(db, string.Empty, tableName2, true);

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
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Char", "X"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Char[]", "ABCDEFGH"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Byte", "254"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.SByte", "125"));  
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Byte[]", "LMNOPQRSTUVWZYZ"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Boolean", "True"));
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
                Program._messageLog.WriteLine("\r\nCreating a table in the database ...");
                DataTable dt = new DataTable(tableName);
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

                dt.PrimaryKey = new DataColumn[] { k1 };

                createScript = string.Empty;
                bool result = schemaRoot.CreateTable(dt, out createScript);
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
                sqlu.CloseConnection();
                sqlu = null;
            }



        }

        public static void DataReaderTest()
        {
            PFSQLAnywhereUL db = new PFSQLAnywhereUL();
            string connectionString = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {
                db.DatabasePath = _frm.txtDatabaseFile.Text;
                db.DatabaseName = _frm.txtDatabaseName.Text;
                db.Username = _frm.txtUsername.Text;
                db.Password = _frm.txtPassword.Text;
                db.DatabaseKey = _frm.txtDatabaseKey.Text;

                connectionString = db.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                if (_frm.txtSQLQuery.Text.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                sw.Start();

                db.OpenConnection();

                db.SQLQuery = _frm.txtSQLQuery.Text;
                db.CommandType = CommandType.Text;

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Open connection time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());


                Program._messageLog.WriteLine("\r\nRunning data extract tests ...\r\n");
                db.returnResultAsString += new PFSQLAnywhereUL.ResultAsStringDelegate(OutputResultsToFile);
                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\ReaderDelimitedTestExtract.txt", PFFileOpenOperation.OpenFileForWrite);
                sw.Start();
                ULDataReader rdr = (ULDataReader)db.RunQueryDataReader();
                db.ExtractDelimitedDataFromDataReader(rdr, ",", "\r\n", true);
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
                rdr = (ULDataReader)db.RunQueryDataReader();
                db.ExtractFixedLengthDataFromDataReader(rdr, true, true, false);
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Extract Fixed Length Dataset time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                rdr.Close();

                rdr = (ULDataReader)db.RunQueryDataReader();
                db.SaveDataReaderToXmlFile(rdr, @"c:\temp\Testrdr.xml");
                rdr.Close();
                rdr = (ULDataReader)db.RunQueryDataReader();
                db.SaveDataReaderWithSchemaToXmlFile(rdr, @"c:\temp\Testrdrplus.xml");
                rdr.Close();
                rdr = (ULDataReader)db.RunQueryDataReader();
                db.SaveDataReaderToXmlSchemaFile(rdr, @"c:\temp\Testrdr.xsd");
                rdr.Close();


                rdr = (ULDataReader)db.RunQueryDataReader();
                PFDataProcessor dataProcessor = new PFDataProcessor();
                XmlDocument xmlDoc = dataProcessor.CopyDataTableToXmlDocument(db.ConvertDataReaderToDataTable(rdr));
                Program._messageLog.WriteLine("\r\n" + xmlDoc.OuterXml + "\r\n");
                rdr.Close();

            }
            catch (System.Exception ex)
            {
                _frm.OutputErrorMessageToLog(ex);
            }
            finally
            {
                db.CloseConnection();
                db = null;
            }

        }

        public static void DataReaderToDataTableTest()
        {
            PFSQLAnywhereUL db = new PFSQLAnywhereUL();
            string connectionString = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {
                db.DatabasePath = _frm.txtDatabaseFile.Text;
                db.DatabaseName = _frm.txtDatabaseName.Text;
                db.Username = _frm.txtUsername.Text;
                db.Password = _frm.txtPassword.Text;
                db.DatabaseKey = _frm.txtDatabaseKey.Text;

                connectionString = db.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                if (_frm.txtSQLQuery.Text.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                sw.Start();

                db.OpenConnection();

                db.SQLQuery = _frm.txtSQLQuery.Text;
                db.CommandType = CommandType.Text;

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Open connection time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                ULDataReader rdr = (ULDataReader)db.RunQueryDataReader();
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
                _frm.OutputErrorMessageToLog(ex);
            }
            finally
            {
                db.CloseConnection();
                db = null;
            }
        }

        public static void DataTableTest()
        {
            PFSQLAnywhereUL db = new PFSQLAnywhereUL();
            string connectionString = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {
                db.DatabasePath = _frm.txtDatabaseFile.Text;
                db.DatabaseName = _frm.txtDatabaseName.Text;
                db.Username = _frm.txtUsername.Text;
                db.Password = _frm.txtPassword.Text;
                db.DatabaseKey = _frm.txtDatabaseKey.Text;


                connectionString = db.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                if (_frm.txtSQLQuery.Text.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                sw.Start();

                db.OpenConnection();

                db.SQLQuery = _frm.txtSQLQuery.Text;
                db.CommandType = CommandType.Text;

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Open connection time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                DataTable tab1 = db.RunQueryDataTable();
                db.returnResult += new PFSQLAnywhereUL.ResultDelegate(OutputResults);
                db.ProcessDataTable(tab1);

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Process Table time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());


                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\TableTestExtract.txt", PFFileOpenOperation.OpenFileForWrite);

                DataTable tab = db.RunQueryDataTable();
                db.returnResultAsString += new PFSQLAnywhereUL.ResultAsStringDelegate(OutputResultsToFile);
                db.ExtractDelimitedDataFromTable(tab, ",", "\r\n", true);
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Extract Delimiated Table time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());
                tab = null;
                sw.Start();

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\TableTestExtractFXL.txt", PFFileOpenOperation.OpenFileForWrite);
                tab = db.RunQueryDataTable();
                db.ExtractFixedLengthDataFromTable(tab, true, true, false);

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Extract Fixed Length Table time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());



                db.SaveDataTableToXmlSchemaFile(tab, @"c:\temp\Testtab.xsd");
                db.SaveDataTableToXmlFile(tab, @"c:\temp\Testtab.xml");
                db.SaveDataTableWithSchemaToXmlFile(tab, @"c:\temp\Testtabplus.xml");
                DataTable tab2 = db.LoadXmlFileToDataTable(@"c:\temp\Testtabplus.xml"); ;
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
                db.CloseConnection();
                db = null;
                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
            }
        }//end DataTableTest

        public static void DataSetTest()
        {
            PFSQLAnywhereUL db = new PFSQLAnywhereUL();
            string connectionString = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {
                db.DatabasePath = _frm.txtDatabaseFile.Text;
                db.DatabaseName = _frm.txtDatabaseName.Text;
                db.Username = _frm.txtUsername.Text;
                db.Password = _frm.txtPassword.Text;
                db.DatabaseKey = _frm.txtDatabaseKey.Text;

                connectionString = db.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                if (_frm.txtSQLQuery.Text.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                db.OpenConnection();

                db.SQLQuery = _frm.txtSQLQuery.Text;
                db.CommandType = CommandType.Text;

                sw.Start();
                DataSet ds1 = db.RunQueryDataSet();
                db.returnResult += new PFSQLAnywhereUL.ResultDelegate(OutputResults);
                db.ProcessDataSet(ds1);
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Process Dataset time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                //Run data extract test
                Program._messageLog.WriteLine("\r\nRunning data extract tests ...\r\n");
                db.returnResultAsString += new PFSQLAnywhereUL.ResultAsStringDelegate(OutputResultsToFile);
                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\DatasetDelimitedTestExtract.txt", PFFileOpenOperation.OpenFileForWrite);
                sw.Start();
                DataSet ds = db.RunQueryDataSet();
                db.ExtractDelimitedDataFromDataSet(ds, "~", "\r\n", true);
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
                ds = db.RunQueryDataSet();
                db.ExtractFixedLengthDataFromDataSet(ds, true, true, false);
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Extract Fixed Length Dataset time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                db.SaveDataSetToXmlSchemaFile(ds, @"c:\temp\Testds.xsd");
                db.SaveDataSetToXmlFile(ds, @"c:\temp\Testds.xml");
                db.SaveDataSetWithSchemaToXmlFile(ds, @"c:\temp\Testdsplus.xml");
                DataSet ds2 = db.LoadXmlFileToDataSet(@"c:\temp\Testds.xml"); ;
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
                db.CloseConnection();
                db = null;
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
            PFSQLAnywhereUL db = new PFSQLAnywhereUL();
            string connectionString = string.Empty;

            db.DatabasePath = _frm.txtDatabaseFile.Text;
            db.DatabaseName = _frm.txtDatabaseName.Text;
            db.Username = _frm.txtUsername.Text;
            db.Password = _frm.txtPassword.Text;
            db.DatabaseKey = _frm.txtDatabaseKey.Text;

            connectionString = db.ConnectionString;

            _msg.Length = 0;
            _msg.Append("Connection string is:\r\n");
            _msg.Append(db.ConnectionString);
            Program._messageLog.WriteLine(_msg.ToString());

            db = null;
            Program._messageLog.WriteLine("QuickTest2 finished.");

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


        public static void ImportDataTableTest()
        {
            PFSQLAnywhereUL db = new PFSQLAnywhereUL();
            string connectionString = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("ImportDataTableTest started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                db.DatabasePath = _frm.txtDatabaseFile.Text;
                db.DatabaseName = _frm.txtDatabaseName.Text;
                db.Username = _frm.txtUsername.Text;
                db.Password = _frm.txtPassword.Text;
                db.DatabaseKey = _frm.txtDatabaseKey.Text;

                connectionString = db.ConnectionString;



                db.OpenConnection();

                StringBuilder sql = new StringBuilder();


                //first delete table if it already exists
                string tabName = "KeyValTable";


                if (db.TableExists(tabName))
                {
                    bool dropped = db.DropTable(tabName);
                    if (dropped == false)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to drop table ");
                        _msg.Append(tabName);
                        throw new DataException(_msg.ToString());
                    }
                    else
                    {
                        _msg.Length = 0;
                        _msg.Append("\r\n");
                        _msg.Append(tabName);
                        _msg.Append(" dropped.");
                        Program._messageLog.WriteLine(_msg.ToString());
                    }
                }



                Program._messageLog.WriteLine("\r\nCreating a table in the database ...");

                //create the table

                DataTable dt = _frm.keyValsDataSet.Tables["KeyValTable"];
                DataColumn dc = dt.Columns[0];
                dt.PrimaryKey = new DataColumn[] { dc };


                db.CreateTable(dt);

                db.CloseConnection();
                db.OpenConnection();

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



        public static void CreateDatabaseTest(MainForm frm)
        {
            PFSQLAnywhereUL sqlu = new PFSQLAnywhereUL();
            string connectionString = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("CreateDatabaseTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                if (frm.txtDatabaseFile.Text.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify a database file name for Create Database Test.");
                    throw new DataException(_msg.ToString());
                }

                sqlu.DatabasePath = frm.txtDatabaseFile.Text;
                sqlu.DatabaseName = frm.txtDatabaseName.Text;
                sqlu.Username = frm.txtUsername.Text;
                sqlu.Password = frm.txtPassword.Text;

                sqlu.DatabaseKey = frm.txtDatabaseKey.Text;

                connectionString = sqlu.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());


                sqlu.OpenConnection();


                if (File.Exists(@"c:\temp\newSQL.udb"))
                    PFFile.FileDelete(@"c:\temp\newSQL.udb");
                if (File.Exists(@"c:\temp\newSQLenc.udb"))
                    PFFile.FileDelete(@"c:\temp\newSQLenc.udb");

                sqlu.CreateDatabase(@"c:\temp\newSQL.udb", false, string.Empty);
                sqlu.CreateDatabase(@"c:\temp\newSQLenc.udb", true, "testkey");


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
                if (sqlu != null)
                    if (sqlu.IsConnected)
                        sqlu.CloseConnection();
                sqlu = null;
                _msg.Length = 0;
                _msg.Append("\r\n... CreateDatabaseTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void GetSchemaTest()
        {
            PFSQLAnywhereUL db = new PFSQLAnywhereUL();
            string connectionString = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {
                _msg.Length = 0;
                _msg.Append("GetSchemaTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                db.DatabasePath = _frm.txtDatabaseFile.Text;
                db.DatabaseName = _frm.txtDatabaseName.Text;
                db.Username = _frm.txtUsername.Text;
                db.Password = _frm.txtPassword.Text;
                db.DatabaseKey = _frm.txtDatabaseKey.Text;


                connectionString = db.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());


                sw.Start();

                db.OpenConnection();

                db.SQLQuery = _frm.txtSQLQuery.Text;
                db.CommandType = CommandType.Text;

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Open connection time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Get Table List running... ");
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                DataTable tableList = db.Connection.GetSchema(ULMetaDataCollectionNames.Tables);
                db.returnResult += new PFSQLAnywhereUL.ResultDelegate(OutputResults);
                db.ProcessDataTable(tableList);

                sw.Stop();
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Get Table List time: ");
                _msg.Append(sw.FormattedElapsedTime);
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());


                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Get Data Types List Running... ");
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                DataTable datatableList = db.Connection.GetSchema(ULMetaDataCollectionNames.DataTypes);
                db.returnResult += new PFSQLAnywhereUL.ResultDelegate(OutputResults);
                db.ProcessDataTable(datatableList);

                sw.Stop();
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Get Data Types List time: ");
                _msg.Append(sw.FormattedElapsedTime);
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Get Column List Running... ");
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                DataTable columnList = db.Connection.GetSchema(ULMetaDataCollectionNames.Columns);
                db.returnResult += new PFSQLAnywhereUL.ResultDelegate(OutputResults);
                db.ProcessDataTable(columnList);

                sw.Stop();
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Get Column List time: ");
                _msg.Append(sw.FormattedElapsedTime);
                _msg.Append(Environment.NewLine);
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
                _msg.Append("\r\n... GetSchemaTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        


    }//end class

}//end namespace
