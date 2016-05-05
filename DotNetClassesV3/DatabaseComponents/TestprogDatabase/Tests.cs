using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.Common;
using PFDataAccessObjects;
using PFTextFiles;
using PFTimers;
using System.Xml;
using PFUnitTestDataObjects;
using PFCollectionsObjects;
using PFListObjects;

namespace TestprogDatabase
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

        //helper routines
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



        //test routines
        public static void ConnectionTest(MainForm frm)
        {
            string dbPlatformDesc = DatabasePlatform.Unknown.ToString();
            PFDatabase db = null;
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("ConnectionTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                string[] parsedConnectionInfo = frm.cboConnectionString.Text.Split('|');
                dbPlatformDesc = parsedConnectionInfo[0];
                connStr = parsedConnectionInfo[1];

                string configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc,string.Empty);
                if (configValue.Trim() == string.Empty)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find config entry for ");
                    _msg.Append(dbPlatformDesc);
                    throw new System.Exception(_msg.ToString());
                }
                string[] parsedConfig = configValue.Split('|');
                if (parsedConfig.Length != 3)
                {
                    _msg.Length = 0;
                    _msg.Append("Invalid config entry items for ");
                    _msg.Append(dbPlatformDesc);
                    _msg.Append(". Number of items after parse: ");
                    _msg.Append(parsedConfig.Length.ToString());
                    _msg.Append(".");
                    throw new System.Exception(_msg.ToString());
                }

                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                _msg.Length = 0;
                _msg.Append("Platform: ");
                _msg.Append(dbPlatformDesc);
                _msg.Append("\r\nConnectionString: ");
                _msg.Append(connStr);
                _msg.Append("\r\nNamespace: ");
                _msg.Append(nmSpace);
                _msg.Append("\r\nClassName: ");
                _msg.Append(clsName);
                _msg.Append("\r\nDLL Path: ");
                _msg.Append(dllPath);
                Program._messageLog.WriteLine(_msg.ToString());


                _msg.Length = 0;
                _msg.Append("Attempting to connect to ");
                _msg.Append(dbPlatformDesc);
                Program._messageLog.WriteLine(_msg.ToString());

                db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace+"."+clsName);
                db.ConnectionString = connStr;
                db.OpenConnection();

                _msg.Length = 0;
                if (db.IsConnected)
                    _msg.Append("Connection successful.");
                else
                    _msg.Append("**Connection failed.");
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
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if (db != null)
                {
                    if (db.IsConnected)
                        db.CloseConnection();
                    db = null;
                }
                _msg.Length = 0;
                _msg.Append("\r\n... ConnectionTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void DataReaderTest(MainForm frm)
        {
            string dbPlatformDesc = DatabasePlatform.Unknown.ToString();
            PFDatabase db = null;
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {
                _msg.Length = 0;
                _msg.Append("DataReaderTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                string[] parsedConnectionInfo = frm.cboConnectionString.Text.Split('|');
                dbPlatformDesc = parsedConnectionInfo[0];
                connStr = parsedConnectionInfo[1];

                string configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
                string[] parsedConfig = configValue.Split('|');
                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                if (frm.txtSQLQuery.Text.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                _msg.Length = 0;
                _msg.Append("Connecting to ");
                _msg.Append(dbPlatformDesc);
                Program._messageLog.WriteLine(_msg.ToString());


                sw.Start();

                db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace + "." + clsName);
                db.ConnectionString = connStr;
                db.OpenConnection();

                db.SQLQuery = frm.txtSQLQuery.Text;
                db.CommandType = CommandType.Text;

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Open connection time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());


                Program._messageLog.WriteLine("\r\nRunning data extract tests ...\r\n");
                db.returnResultAsString += new PFDatabase.ResultAsStringDelegate(OutputResultsToFile);
                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\ReaderDelimitedTestExtract.txt", PFFileOpenOperation.OpenFileForWrite);
                sw.Start();
                DbDataReader rdr = (DbDataReader)db.RunQueryDataReader();
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
                rdr = (DbDataReader)db.RunQueryDataReader();
                db.ExtractFixedLengthDataFromDataReader(rdr, true, true, false);
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Extract Fixed Length Dataset time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                rdr.Close();

                rdr = (DbDataReader)db.RunQueryDataReader();
                db.SaveDataReaderToXmlFile(rdr, @"c:\temp\Testrdr.xml");
                rdr.Close();
                rdr = (DbDataReader)db.RunQueryDataReader();
                db.SaveDataReaderWithSchemaToXmlFile(rdr, @"c:\temp\Testrdrplus.xml");
                rdr.Close();
                rdr = (DbDataReader)db.RunQueryDataReader();
                db.SaveDataReaderToXmlSchemaFile(rdr, @"c:\temp\Testrdr.xsd");
                rdr.Close();


                rdr = (DbDataReader)db.RunQueryDataReader();
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
                if (db != null)
                {
                    if (db.IsConnected)
                        db.CloseConnection();
                    db = null;
                }
                _msg.Length = 0;
                _msg.Append("\r\n... DataReaderTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void DataTableTest(MainForm frm)
        {
            string dbPlatformDesc = DatabasePlatform.Unknown.ToString();
            PFDatabase db = null;
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {
                _msg.Length = 0;
                _msg.Append("DataTableTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                string[] parsedConnectionInfo = frm.cboConnectionString.Text.Split('|');
                dbPlatformDesc = parsedConnectionInfo[0];
                connStr = parsedConnectionInfo[1];

                string configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
                string[] parsedConfig = configValue.Split('|');
                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                if (frm.txtSQLQuery.Text.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                _msg.Length = 0;
                _msg.Append("Connecting to ");
                _msg.Append(dbPlatformDesc);
                Program._messageLog.WriteLine(_msg.ToString());


                sw.Start();

                db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace + "." + clsName);
                db.ConnectionString = connStr;
                db.OpenConnection();

                db.SQLQuery = frm.txtSQLQuery.Text;
                db.CommandType = CommandType.Text;

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Open connection time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                DataTable tab1 = db.RunQueryDataTable();
                db.returnResult += new PFDatabase.ResultDelegate(OutputResults);
                db.ProcessDataTable(tab1);

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Process Table time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());


                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\TableTestExtract.txt", PFFileOpenOperation.OpenFileForWrite);

                //sw.Start();
                DataTable tab = db.RunQueryDataTable();
                //db.returnResultAsString += new PFdb.ResultAsStringDelegate(OutputExtractFormattedData);
                db.returnResultAsString += new PFDatabase.ResultAsStringDelegate(OutputResultsToFile);
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

                PFDataProcessor dataProcessor = new PFDataProcessor();
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
                if (db != null)
                {
                    if (db.IsConnected)
                        db.CloseConnection();
                    db = null;
                }
                _msg.Length = 0;
                _msg.Append("\r\n... DataTableTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void DataSetTest(MainForm frm)
        {
            string dbPlatformDesc = DatabasePlatform.Unknown.ToString();
            PFDatabase db = null;
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {
                _msg.Length = 0;
                _msg.Append("DataSetTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                string[] parsedConnectionInfo = frm.cboConnectionString.Text.Split('|');
                dbPlatformDesc = parsedConnectionInfo[0];
                connStr = parsedConnectionInfo[1];

                string configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
                string[] parsedConfig = configValue.Split('|');
                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                if (frm.txtSQLQuery.Text.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                _msg.Length = 0;
                _msg.Append("Connecting to ");
                _msg.Append(dbPlatformDesc);
                Program._messageLog.WriteLine(_msg.ToString());


                sw.Start();

                db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace + "." + clsName);
                db.ConnectionString = connStr;
                db.OpenConnection();

                db.SQLQuery = frm.txtSQLQuery.Text;
                db.CommandType = CommandType.Text;

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Open connection time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();
                DataSet ds1 = db.RunQueryDataSet();
                db.returnResult += new PFDatabase.ResultDelegate(OutputResults);
                db.ProcessDataSet(ds1);
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Process Dataset time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                //Run data extract test
                Program._messageLog.WriteLine("\r\nRunning data extract tests ...\r\n");
                db.returnResultAsString += new PFDatabase.ResultAsStringDelegate(OutputResultsToFile);
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
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if (db != null)
                {
                    if (db.IsConnected)
                        db.CloseConnection();
                    db = null;
                }
                _msg.Length = 0;
                _msg.Append("\r\n... DataSetTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void DataReaderToDataTableTest(MainForm frm)
        {
            string dbPlatformDesc = DatabasePlatform.Unknown.ToString();
            PFDatabase db = null;
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {
                _msg.Length = 0;
                _msg.Append("DataReaderToDataTableTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                string[] parsedConnectionInfo = frm.cboConnectionString.Text.Split('|');
                dbPlatformDesc = parsedConnectionInfo[0];
                connStr = parsedConnectionInfo[1];

                string configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
                string[] parsedConfig = configValue.Split('|');
                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                if (frm.txtSQLQuery.Text.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                _msg.Length = 0;
                _msg.Append("Connecting to ");
                _msg.Append(dbPlatformDesc);
                Program._messageLog.WriteLine(_msg.ToString());


                sw.Start();

                db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace + "." + clsName);
                db.ConnectionString = connStr;
                db.OpenConnection();

                db.SQLQuery = frm.txtSQLQuery.Text;
                db.CommandType = CommandType.Text;

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Open connection time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                DbDataReader rdr = (DbDataReader)db.RunQueryDataReader();
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
                if (db != null)
                {
                    if (db.IsConnected)
                        db.CloseConnection();
                    db = null;
                }
                _msg.Length = 0;
                _msg.Append("\r\n... DataReaderToDataTableTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void CreateTableTest(MainForm frm)
        {
            string dbPlatformDesc = DatabasePlatform.Unknown.ToString();
            DatabasePlatform dbPlatform = DatabasePlatform.Unknown;
            PFDatabase db = null;
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;
            Stopwatch sw = new Stopwatch();

            string createScript = string.Empty;
            string tableName = string.Empty;
            StringBuilder sql = new StringBuilder();
            PFUnitTestDataTable unitTestDt01 = null;
            PFUnitTestDataTable unitTestDt02 = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("CreateTableTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                string[] parsedConnectionInfo = frm.cboConnectionString.Text.Split('|');
                dbPlatformDesc = parsedConnectionInfo[0];
                connStr = parsedConnectionInfo[1];

                string configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
                string[] parsedConfig = configValue.Split('|');
                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                if (frm.txtTableName.Text.Length == 0)
                {
                    throw new System.Exception("You must specify a table name.");
                }

                _msg.Length = 0;
                _msg.Append("Connecting to ");
                _msg.Append(dbPlatformDesc);
                Program._messageLog.WriteLine(_msg.ToString());


                sw.Start();

                db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace + "." + clsName);
                dbPlatform = db.DbPlatform;
                db.ConnectionString = connStr;
                db.OpenConnection();

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Open connection time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());


                tableName = frm.txtTableName.Text;

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


                //---
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
                {
                    if (db.IsConnected)
                        db.CloseConnection();
                    db = null;
                }
                _msg.Length = 0;
                _msg.Append("\r\n... CreateTableTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void ImportTableTest(MainForm frm)
        {
            string dbPlatformDesc = DatabasePlatform.Unknown.ToString();
            PFDatabase db = null;
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {
                sw.Start();
                _msg.Length = 0;
                _msg.Append("ImportTableTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                string[] parsedConnectionInfo = frm.cboConnectionString.Text.Split('|');
                dbPlatformDesc = parsedConnectionInfo[0];
                connStr = parsedConnectionInfo[1];

                string configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
                string[] parsedConfig = configValue.Split('|');
                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace + "." + clsName);
                db.ConnectionString = connStr;
                db.OpenConnection();

                string tableName = frm.txtKeyValsTableName.Text;

                DataTable tab = frm.KeyValTable;
                tab.TableName = tableName;

                _msg.Length = 0;
                _msg.Append(tableName);
                if (db.TableExists(tableName))
                {
                    db.DropTable(tableName);
                    if (db.TableExists(tableName) == false)
                        _msg.Append(" dropped.");
                    else
                        _msg.Append(" drop failed.");
                }
                else
                {
                    _msg.Append(" does not exist.");
                }

                Program._messageLog.WriteLine("\r\nCreating a table in the database ...");

                //create the table
                db.CreateTable(tab);

                Program._messageLog.WriteLine("\r\nImporting data table to the database ...");

                int batchSize = Convert.ToInt32(frm.txtUpdateBatchSize.Text);
                if (batchSize == 1)
                    db.ImportDataFromDataTable(tab);
                else
                    db.ImportDataFromDataTable(tab, batchSize);

                _msg.Length = 0;
                _msg.Append("Table imported: ");
                _msg.Append(tab.TableName);
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
                if (sw.StopwatchIsRunning)
                {
                    sw.Stop();
                    sw.ShowMilliseconds = false;
                    _msg.Length = 0;
                    _msg.Append("Elapsed Time: ");
                    _msg.Append(sw.FormattedElapsedTime);
                    Program._messageLog.WriteLine(_msg.ToString());
                    sw = null;
                }
                if (db != null)
                {
                    if (db.IsConnected)
                        db.CloseConnection();
                    db = null;
                }
                _msg.Length = 0;
                _msg.Append("\r\n... ImportTableTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void GetQueryDataSchema(MainForm frm)
        {
            string dbPlatformDesc = DatabasePlatform.Unknown.ToString();
            PFDatabase db = null;
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {
                _msg.Length = 0;
                _msg.Append("GetQueryDataSchema started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                string[] parsedConnectionInfo = frm.cboConnectionString.Text.Split('|');
                dbPlatformDesc = parsedConnectionInfo[0];
                connStr = parsedConnectionInfo[1];

                string configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
                string[] parsedConfig = configValue.Split('|');
                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                if (frm.txtSQLQuery.Text.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                _msg.Length = 0;
                _msg.Append("Connecting to ");
                _msg.Append(dbPlatformDesc);
                Program._messageLog.WriteLine(_msg.ToString());


                sw.Start();

                db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace + "." + clsName);
                db.ConnectionString = connStr;
                db.OpenConnection();

                db.SQLQuery = frm.txtSQLQuery.Text;
                db.CommandType = CommandType.Text;

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Open connection time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                DataTable tab = db.RunQueryDataTable();

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
                _msg.Append("Get Query Data Schema time: ");
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
                if (db != null)
                {
                    if (db.IsConnected)
                        db.CloseConnection();
                    db = null;
                }
                _msg.Length = 0;
                _msg.Append("\r\n... GetQueryDataSchema finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
        


    }//end class
}//end namespace
