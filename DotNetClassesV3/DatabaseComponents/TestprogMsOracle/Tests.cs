using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Configuration;
using System.Windows.Forms;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using PFTimers;
using PFTextFiles;
using PFDataAccessObjects;
using System.Xml;
using PFUnitTestDataObjects;
using PFCollectionsObjects;
using PFListObjects;

namespace TestprogMsOracle
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
        public static void GetStaticKeys(MainForm frm)
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

        public static void ConnectionStringTest(MainForm frm)
        {
            PFMsOracle oracle = new PFMsOracle();
            string connectionString = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("ConnectionStringTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                oracle.DataSource = frm.txtDataSource.Text;
                oracle.Username = frm.txtUserId.Text;
                oracle.Password = frm.txtPassword.Text;
                oracle.UseIntegratedSecurity = frm.chkUseIntegratedSecurity.Checked;

                connectionString = oracle.ConnectionString;

                //test
                oracle.ConnectionString = connectionString;
                //end test

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                oracle.OpenConnection();

                _msg.Length = 0;
                if (oracle.IsConnected)
                {
                    _msg.Append("Connection succeeded!");
                }
                else
                    _msg.Append("ERROR: ***Connection failed***");
                _msg.Append("\r\nConnectionState = ");
                _msg.Append(oracle.CurrentConnectionState);
                Program._messageLog.WriteLine(_msg.ToString());

                foreach (stKeyValuePair<string, string> kv in oracle.ConnectionStringKeyVals)
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
                if (oracle != null)
                    if (oracle.IsConnected)
                        oracle.CloseConnection();
                oracle = null;
                _msg.Length = 0;
                _msg.Append("\r\n... ConnectionStringTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void CreateTableTest(MainForm frm)
        {
            PFMsOracle oracle = new PFMsOracle();
            string connectionString = string.Empty;
            string createScript = string.Empty;
            string tableName = "HR.TestTable01";
            StringBuilder sql = new StringBuilder();
            PFUnitTestDataTable unitTestDt01 = null;
            PFUnitTestDataTable unitTestDt02 = null;

            try
            {
                oracle.DataSource = frm.txtDataSource.Text;
                oracle.UseIntegratedSecurity = frm.chkUseIntegratedSecurity.Checked;
                oracle.Username = frm.txtUserId.Text;
                oracle.Password = frm.txtPassword.Text;
                if (frm.txtTableName.Text.Length > 0)
                    tableName = frm.txtTableName.Text;

                connectionString = oracle.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                oracle.OpenConnection();

                IDatabaseProvider db = (IDatabaseProvider)oracle;

                string schemaName = "SYS";
                string tabName = "InvalidTableName";
                string[] parsedTableName = tableName.Split('.');
                if (parsedTableName.Length == 2)
                {
                    schemaName = parsedTableName[0];
                    tabName = parsedTableName[1];
                }
                else if (parsedTableName.Length == 1)
                    tabName = parsedTableName[0];
                else
                    tabName = "InvalidTableName";

                if (oracle.TableExists(schemaName, tabName))
                {
                    bool dropped = oracle.DropTable(schemaName, tabName);
                    if (dropped == false)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to drop table ");
                        _msg.Append(schemaName);
                        _msg.Append(".");
                        _msg.Append(tabName);
                        throw new DataException(_msg.ToString());
                    }
                }

                if (oracle.TableExists(schemaName, tabName + "_02"))
                {
                    bool dropped = oracle.DropTable(schemaName, tabName + "_02");
                    if (dropped == false)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to drop table ");
                        _msg.Append(schemaName);
                        _msg.Append(".");
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
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Guid", "58a4a08d-6101-4393-86dc-b2a8db46ec0f"));  //MsOracle provider does not support writing guids to Oracle database4; first convert to string
                                                                                                                                  //PFMsOracle will convert the data type to a string when it creates the table

                unitTestDt01.SetDataTypesToInclude(dataTypesToInclude);
                unitTestDt01.SetDataTypeOptions("System.String", false, true, 75);
                unitTestDt01.SetDataTypeOptions("F13", "System.String", false, true, 36);


                unitTestDt02.SetDataTypesToInclude(dataTypesToInclude);
                unitTestDt02.SetDataTypeOptions("System.String", false, true, 75000);
                unitTestDt02.SetDataTypeOptions("F13", "System.String", false, true, 36);

                //create the table

                _msg.Length = 0;
                _msg.Append("Creating tables");
                Program._messageLog.WriteLine(_msg.ToString());

                unitTestDt01.CreateTableFromTableColumns();
                unitTestDt02.CreateTableFromTableColumns();

                createScript = unitTestDt01.TableCreateScript;


                //import data to database

                _msg.Length = 0;
                _msg.Append("Importing data to Test Table 01");
                Program._messageLog.WriteLine(_msg.ToString());

                unitTestDt01.ImportTableToDatabase();

                _msg.Length = 0;
                _msg.Append("Importing data to Test Table 02");
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
                //first delete table if it already exists
                Program._messageLog.WriteLine("\r\nDropping old table if it exists ...");

                string schemaName = "SYS";
                string tabName = "InvalidTableName";
                string[] parsedTableName = tableName.Split('.');
                if (parsedTableName.Length == 2)
                {
                    schemaName = parsedTableName[0];
                    tabName = parsedTableName[1];
                }
                else if (parsedTableName.Length == 1)
                    tabName = parsedTableName[0];
                else
                    ;

                if (oracle.TableExists(schemaName, tabName))
                {
                    bool dropped = oracle.DropTable(schemaName, tabName);
                    if (dropped == false)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to drop table ");
                        _msg.Append(schemaName);
                        _msg.Append(".");
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

                createScript = string.Empty;
                bool result = oracle.CreateTable(dt, out createScript);
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
                oracle.CloseConnection();
                oracle = null;
            }



        }

        public static void DataReaderTest(MainForm frm)
        {
            PFMsOracle oracle = new PFMsOracle();
            string connectionString = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {
                oracle.DataSource = frm.txtDataSource.Text;
                oracle.UseIntegratedSecurity = frm.chkUseIntegratedSecurity.Checked;
                oracle.Username = frm.txtUserId.Text;
                oracle.Password = frm.txtPassword.Text;

                connectionString = oracle.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                if (frm.txtSQLQuery.Text.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                sw.Start();

                oracle.OpenConnection();

                oracle.SQLQuery = frm.txtSQLQuery.Text;
                if (frm.chkIsStoredProcedure.Checked)
                    oracle.CommandType = CommandType.StoredProcedure;
                else
                    oracle.CommandType = CommandType.Text;

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Open connection time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());


                Program._messageLog.WriteLine("\r\nRunning data extract tests ...\r\n");
                oracle.returnResultAsString += new PFMsOracle.ResultAsStringDelegate(OutputResultsToFile);
                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\ReaderDelimitedTestExtract.txt", PFFileOpenOperation.OpenFileForWrite);
                sw.Start();
                OracleDataReader rdr = (OracleDataReader)oracle.RunQueryDataReader();
                oracle.ExtractDelimitedDataFromDataReader(rdr, ",", "\r\n", true);
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
                rdr = (OracleDataReader)oracle.RunQueryDataReader();
                oracle.ExtractFixedLengthDataFromDataReader(rdr, true, true, false);
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Extract Fixed Length Dataset time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                rdr.Close();

                rdr = (OracleDataReader)oracle.RunQueryDataReader();
                oracle.SaveDataReaderToXmlFile(rdr, @"c:\temp\Testrdr.xml");
                rdr.Close();
                rdr = (OracleDataReader)oracle.RunQueryDataReader();
                oracle.SaveDataReaderWithSchemaToXmlFile(rdr, @"c:\temp\Testrdrplus.xml");
                rdr.Close();
                rdr = (OracleDataReader)oracle.RunQueryDataReader();
                oracle.SaveDataReaderToXmlSchemaFile(rdr, @"c:\temp\Testrdr.xsd");
                rdr.Close();


                rdr = (OracleDataReader)oracle.RunQueryDataReader();
                PFDataProcessor dataProcessor = new PFDataProcessor();
                XmlDocument xmlDoc = dataProcessor.CopyDataTableToXmlDocument(oracle.ConvertDataReaderToDataTable(rdr));
                Program._messageLog.WriteLine("\r\n" + xmlDoc.OuterXml + "\r\n");
                rdr.Close();

            }
            catch (System.Exception ex)
            {
                frm.OutputErrorMessageToLog(ex);
            }
            finally
            {
                oracle.CloseConnection();
                oracle = null;
            }

        }

        public static void DataReaderToDataTableTest(MainForm frm)
        {
            PFMsOracle oracle = new PFMsOracle();
            string connectionString = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {
                oracle.DataSource = frm.txtDataSource.Text;
                oracle.UseIntegratedSecurity = frm.chkUseIntegratedSecurity.Checked;
                oracle.Username = frm.txtUserId.Text;
                oracle.Password = frm.txtPassword.Text;

                connectionString = oracle.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                if (frm.txtSQLQuery.Text.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                sw.Start();

                oracle.OpenConnection();

                oracle.SQLQuery = frm.txtSQLQuery.Text;
                if (frm.chkIsStoredProcedure.Checked)
                    oracle.CommandType = CommandType.StoredProcedure;
                else
                    oracle.CommandType = CommandType.Text;

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Open connection time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                OracleDataReader rdr = (OracleDataReader)oracle.RunQueryDataReader();
                DataTable tab = oracle.ConvertDataReaderToDataTable(rdr);
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
                frm.OutputErrorMessageToLog(ex);
            }
            finally
            {
                oracle.CloseConnection();
                oracle = null;
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

        public static void DataTableTest(MainForm frm)
        {
            PFMsOracle oracle = new PFMsOracle();
            string connectionString = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {
                oracle.DataSource = frm.txtDataSource.Text;
                oracle.UseIntegratedSecurity = frm.chkUseIntegratedSecurity.Checked;
                oracle.Username = frm.txtUserId.Text;
                oracle.Password = frm.txtPassword.Text;

                connectionString = oracle.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                if (frm.txtSQLQuery.Text.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                sw.Start();

                oracle.OpenConnection();

                oracle.SQLQuery = frm.txtSQLQuery.Text;
                if (frm.chkIsStoredProcedure.Checked)
                    oracle.CommandType = CommandType.StoredProcedure;
                else
                    oracle.CommandType = CommandType.Text;

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Open connection time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                DataTable tab1 = oracle.RunQueryDataTable();
                oracle.returnResult += new PFMsOracle.ResultDelegate(OutputResults);
                oracle.ProcessDataTable(tab1);

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Process Table time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());


                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\TableTestExtract.txt", PFFileOpenOperation.OpenFileForWrite);

                //sw.Start();
                DataTable tab = oracle.RunQueryDataTable();
                //oracle.returnResultAsString += new PFMsOracle.ResultAsStringDelegate(OutputExtractFormattedData);
                oracle.returnResultAsString += new PFMsOracle.ResultAsStringDelegate(OutputResultsToFile);
                oracle.ExtractDelimitedDataFromTable(tab, ",", "\r\n", true);
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
                tab = oracle.RunQueryDataTable();
                oracle.ExtractFixedLengthDataFromTable(tab, true, true, false);

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Extract Fixed Length Table time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());



                oracle.SaveDataTableToXmlSchemaFile(tab, @"c:\temp\Testtab.xsd");
                oracle.SaveDataTableToXmlFile(tab, @"c:\temp\Testtab.xml");
                oracle.SaveDataTableWithSchemaToXmlFile(tab, @"c:\temp\Testtabplus.xml");
                DataTable tab2 = oracle.LoadXmlFileToDataTable(@"c:\temp\Testtabplus.xml"); ;
                int numRows = tab2.Rows.Count;

                PFDataProcessor dataProcessor = new PFDataProcessor();
                XmlDocument xmlDoc = dataProcessor.CopyDataTableToXmlDocument(tab);
                Program._messageLog.WriteLine("\r\n" + xmlDoc.OuterXml + "\r\n");
            }
            catch (System.Exception ex)
            {
                frm.OutputErrorMessageToLog(ex);
            }
            finally
            {
                oracle.CloseConnection();
                oracle = null;
                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
            }
        }//end DataTableTest

        public static void DataSetTest(MainForm frm)
        {
            PFMsOracle oracle = new PFMsOracle();
            string connectionString = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {
                oracle.DataSource = frm.txtDataSource.Text;
                oracle.UseIntegratedSecurity = frm.chkUseIntegratedSecurity.Checked;
                oracle.Username = frm.txtUserId.Text;
                oracle.Password = frm.txtPassword.Text;

                connectionString = oracle.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());

                if (frm.txtSQLQuery.Text.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                oracle.OpenConnection();

                oracle.SQLQuery = frm.txtSQLQuery.Text;
                if (frm.chkIsStoredProcedure.Checked)
                    oracle.CommandType = CommandType.StoredProcedure;
                else
                    oracle.CommandType = CommandType.Text;

                sw.Start();
                DataSet ds1 = oracle.RunQueryDataSet();
                oracle.returnResult += new PFMsOracle.ResultDelegate(OutputResults);
                oracle.ProcessDataSet(ds1);
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Process Dataset time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                //Run data extract test
                Program._messageLog.WriteLine("\r\nRunning data extract tests ...\r\n");
                oracle.returnResultAsString += new PFMsOracle.ResultAsStringDelegate(OutputResultsToFile);
                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\DatasetDelimitedTestExtract.txt", PFFileOpenOperation.OpenFileForWrite);
                sw.Start();
                DataSet ds = oracle.RunQueryDataSet();
                oracle.ExtractDelimitedDataFromDataSet(ds, "~", "\r\n", true);
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
                ds = oracle.RunQueryDataSet();
                oracle.ExtractFixedLengthDataFromDataSet(ds, true, true, false);
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Extract Fixed Length Dataset time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                oracle.SaveDataSetToXmlSchemaFile(ds, @"c:\temp\Testds.xsd");
                oracle.SaveDataSetToXmlFile(ds, @"c:\temp\Testds.xml");
                oracle.SaveDataSetWithSchemaToXmlFile(ds, @"c:\temp\Testdsplus.xml");
                DataSet ds2 = oracle.LoadXmlFileToDataSet(@"c:\temp\Testds.xml"); ;
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
                frm.OutputErrorMessageToLog(ex);
            }
            finally
            {
                oracle.CloseConnection();
                oracle = null;
            }

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

        public static void ImportDataTableTest(MainForm frm)
        {
            PFMsOracle oracle = new PFMsOracle();
            string connectionString = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("ImportDataTableTest started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                oracle.DataSource = frm.txtDataSource.Text;
                oracle.UseIntegratedSecurity = frm.chkUseIntegratedSecurity.Checked;
                oracle.Username = frm.txtUserId.Text;
                oracle.Password = frm.txtPassword.Text;

                connectionString = oracle.ConnectionString;



                oracle.OpenConnection();

                StringBuilder sql = new StringBuilder();
                DataTable dt = frm.keyValsDataSet.Tables["HR.KeyValTable"];
                string tableName = dt.TableName;


                //first delete table if it already exists
                Program._messageLog.WriteLine("\r\nDropping old table if it exists ...");



                string schemaName = "SYS";
                string tabName = "InvalidTableName";
                string[] parsedTableName = tableName.Split('.');
                if (parsedTableName.Length == 2)
                {
                    schemaName = parsedTableName[0];
                    tabName = parsedTableName[1];
                }
                else if (parsedTableName.Length == 1)
                    tabName = parsedTableName[0];
                else
                    tabName = "InvalidTableName";

                if (oracle.TableExists(schemaName, tabName))
                {
                    bool dropped = oracle.DropTable(schemaName, tabName);
                    if (dropped == false)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to drop table ");
                        _msg.Append(schemaName);
                        _msg.Append(".");
                        _msg.Append(tabName);
                        throw new DataException(_msg.ToString());
                    }
                }


                Program._messageLog.WriteLine("\r\nCreating a table in the database ...");

                //create the table
                oracle.CreateTable(dt);

                oracle.ImportDataFromDataTable(dt, 1000);

                oracle.CloseConnection();


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
                 






    }//end class
}//end namespace
