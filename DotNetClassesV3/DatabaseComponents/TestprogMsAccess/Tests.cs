using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Xml;
using ADOX;
using PFTextFiles;
using PFDataAccessObjects;
using PFUnitTestDataObjects;
using PFCollectionsObjects;
using PFListObjects;

namespace TestprogMsAccess
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
        public static void RunAdoxAdorTest()
        {
            
            try
            {
                ADOX.Catalog cat = new ADOX.Catalog();
                ADOX.Table tab = new ADOX.Table();

                _msg.Length = 0;
                _msg.Append("RunAdoxAdorTest started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                tab.Name = "TestTab1";
                tab.Columns.Append("PK1", ADOX.DataTypeEnum.adInteger);
                tab.Columns.Append("F1", ADOX.DataTypeEnum.adVarWChar,30);
                tab.Columns.Append("F2", ADOX.DataTypeEnum.adDouble);
                tab.Columns.Append("F3", ADOX.DataTypeEnum.adVarBinary);
                tab.Columns.Append("F4", ADOX.DataTypeEnum.adBoolean);
                tab.Columns.Append("F5", ADOX.DataTypeEnum.adCurrency);
                tab.Columns.Append("F6", ADOX.DataTypeEnum.adWChar);
                tab.Columns.Append("F7", ADOX.DataTypeEnum.adSmallInt);
                tab.Columns.Append("F8", ADOX.DataTypeEnum.adSingle);
                //tab.Columns.Append("F9", ADOX.DataTypeEnum.adDecimal,18); //invalid, use double instead
                tab.Columns.Append("F9", ADOX.DataTypeEnum.adLongVarBinary);
                tab.Columns.Append("F10", ADOX.DataTypeEnum.adLongVarWChar);
                tab.Columns.Append("F11", ADOX.DataTypeEnum.adBoolean);
                tab.Columns.Append("F12", ADOX.DataTypeEnum.adVarWChar, 1);
                tab.Columns.Append("F13", ADOX.DataTypeEnum.adUnsignedTinyInt);
                tab.Columns.Append("F14", ADOX.DataTypeEnum.adDate);

                //if (File.Exists(@"C:\Testfiles\Access\NewMDB.mdb"))
                //    File.Delete(@"C:\Testfiles\Access\NewMDB.mdb");
                //cat.Create(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Testfiles\Access\NewMDB.mdb;User Id=admin;Password=;Jet OLEDB:Engine Type=5");
                if (File.Exists(@"C:\Testfiles\Access\NewMDB.accdb"))
                    File.Delete(@"C:\Testfiles\Access\NewMDB.accdb");
                cat.Create(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Testfiles\Access\NewMDB.accdb;User Id=admin;Password=;Jet OLEDB:Engine Type=6");
                cat.Tables.Append(tab);

                

            
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
                _msg.Append("... RunAdoxAdorTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void RunCreateDatabaseTableTest(MainForm frm)
        {
            PFMsAccess access = null;
            string dbPath = frm.txtDatabasePath.Text;
            string createScript = string.Empty;
            string tableName = "TestTabX1";
            string tableName2 = "TestTabX2";
            AccessVersion dbVersion = AccessVersion.Access2003;
            PFUnitTestDataTable unitTestDt01 = null;
            PFUnitTestDataTable unitTestDt02 = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("RunAdoxAdorTest started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                if(Path.GetExtension(dbPath)==".accdb")
                    dbVersion = AccessVersion.Access2007;
                else
                    dbVersion = AccessVersion.Access2003;

                access = PFMsAccess.CreateDatabase(dbPath, dbVersion, frm.chkOverwriteExistingDb.Checked, "admin", string.Empty);

                /*
                DataTable dt = new DataTable(tableName);
                DataColumn k1 = new DataColumn("K1",Type.GetType("System.Int32"));
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

                access.CreateTable(dt);
 
                 */

                if (access.IsConnected == false)
                    access.OpenConnection();

                IDatabaseProvider db = (IDatabaseProvider)access;

                unitTestDt01 = new PFUnitTestDataTable(db, "", tableName, true);
                unitTestDt02 = new PFUnitTestDataTable(db, "", tableName2, true);

                access.ReopenConnection();
                
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

                _msg.Length = 0;
                _msg.Append("CreateScript:\r\n");
                _msg.Append(createScript);
                Program._messageLog.WriteLine(_msg.ToString());

                //import data to database

                access.ReopenConnection();

                _msg.Length = 0;
                _msg.Append("Importing data to TestTable01");
                Program._messageLog.WriteLine(_msg.ToString());

                unitTestDt01.ImportTableToDatabase();

                access.ReopenConnection();

                _msg.Length = 0;
                _msg.Append("Importing data to TestTable02");
                Program._messageLog.WriteLine(_msg.ToString());

                unitTestDt02.ImportTableToDatabase();

                //retrieve just created table and see what data types get assigned to data table columns

                StringBuilder sql = new StringBuilder();

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
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessageWithStackTrace(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if (access != null)
                    if (access.IsConnected)
                        access.CloseConnection();
                access = null;
                System.GC.Collect();
                _msg.Length = 0;
                _msg.Append("... RunCreateDatabaseTableTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());
            }
                 
        
        }


        public static void RunDropTableTest(MainForm frm)
        {
            PFMsAccess db = new PFMsAccess(frm.txtDatabasePath.Text,
                                           frm.txtDbUsername.Text,
                                           frm.txtDbPassword.Text);
            if (Path.GetExtension(frm.txtDatabasePath.Text) == ".mdb")
                db.OleDbProvider = PFAccessOleDbProvider.MicrosoftJetOLEDB_4_0;
            else
                db.OleDbProvider = PFAccessOleDbProvider.MicrosoftACEOLEDB_12_0;

            string tableName = "TestTabX1";
            bool tableDropped = false;

            try
            {
                _msg.Length = 0;
                _msg.Append("RunDropTableTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                db.OpenConnection();

                _msg.Length = 0;
                _msg.Append("Table ");
                _msg.Append(tableName);
                if (db.TableExists(tableName))
                {
                    tableDropped = db.DropTable(tableName);
                    if (tableDropped)
                    {
                        _msg.Append(" was dropped.");
                    }
                    else
                    {
                        _msg.Append(" drop failed.");
                    }
                }
                else
                {
                    _msg.Append(" does not exist.");
                }
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
                    if (db.IsConnected)
                        db.CloseConnection();
                db = null;
                _msg.Length = 0;
                _msg.Append("\r\n... RunDropTableTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        

        public static void ConnectionTest(MainForm frm)
        {
            PFMsAccess db = new PFMsAccess(frm.cboDatabase.Text,
                                           frm.txtDbUsername.Text,
                                           frm.txtDbPassword.Text);
            if (frm.cboAccessVersion.Text == "Access 2003")
                db.OleDbProvider = PFAccessOleDbProvider.MicrosoftJetOLEDB_4_0;
            else
                db.OleDbProvider = PFAccessOleDbProvider.MicrosoftACEOLEDB_12_0;
            
            try
            {
                db.OpenConnection();

                
                _msg.Length = 0;
                if (db.IsConnected)
                    _msg.Append("Connection established!");
                else
                    _msg.Append("*** Connection failed! ***");
                Program._messageLog.WriteLine(_msg.ToString());

                db.CloseConnection();

                _msg.Length = 0;
                _msg.Append("Connection string = ");
                _msg.Append(db.ConnectionString);
                _msg.Append(Environment.NewLine);
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
                ;
            }
        
        }


        public static void DataReaderTest(MainForm frm)
        {
            PFMsAccess db = new PFMsAccess(frm.cboDatabase.Text,
                                           frm.txtDbUsername.Text,
                                           frm.txtDbPassword.Text);
            if (frm.cboAccessVersion.Text == "Access 2003")
                db.OleDbProvider = PFAccessOleDbProvider.MicrosoftJetOLEDB_4_0;
            else
                db.OleDbProvider = PFAccessOleDbProvider.MicrosoftACEOLEDB_12_0;

            try
            {
                db.OpenConnection();

                _msg.Length = 0;
                if (db.IsConnected)
                {
                    _msg.Append("Connection established!\r\n");
                    _msg.Append("Connection string: ");
                    _msg.Append(db.ConnectionString);
                }
                else
                    _msg.Append("*** Connection failed! ***");
                Program._messageLog.WriteLine(_msg.ToString());

                db.returnResult += new PFMsAccess.ResultDelegate(OutputResults);
                db.SQLQuery = frm.txtSqlQuery.Text;
                OleDbDataReader rdr = (OleDbDataReader)db.RunQueryDataReader();
                db.ProcessDataReader(rdr);
                rdr.Close();
                db.returnResult -= OutputResults;

                db.returnResultAsString += new PFMsAccess.ResultAsStringDelegate(OutputResultsToFile);
                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\MsAccessRdrDelimited.txt", PFFileOpenOperation.OpenFileForWrite);
                rdr = (OleDbDataReader)db.RunQueryDataReader();
                db.ExtractDelimitedDataFromDataReader(rdr, ",", "\r\n", true);
                rdr.Close();

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\MsAccessRdrFixedLength.txt", PFFileOpenOperation.OpenFileForWrite);
                rdr = (OleDbDataReader)db.RunQueryDataReader();
                db.ExtractFixedLengthDataFromDataReader(rdr, true, true, false);
                rdr.Close();

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();

                rdr = (OleDbDataReader)db.RunQueryDataReader();
                db.SaveDataReaderToXmlFile(rdr, @"c:\temp\MsAccessTestrdr.xml");
                rdr.Close();
                rdr = (OleDbDataReader)db.RunQueryDataReader();
                db.SaveDataReaderWithSchemaToXmlFile(rdr, @"c:\temp\MsAccessTestrdrplus.xml");
                rdr.Close();
                rdr = (OleDbDataReader)db.RunQueryDataReader();
                db.SaveDataReaderToXmlSchemaFile(rdr, @"c:\temp\MsAccessTestrdr.xsd");
                rdr.Close();


                rdr = (OleDbDataReader)db.RunQueryDataReader();
                PFDataProcessor dataProcessor = new PFDataProcessor();
                XmlDocument xmlDoc = dataProcessor.CopyDataTableToXmlDocument(db.ConvertDataReaderToDataTable(rdr));
                Program._messageLog.WriteLine("\r\n" + xmlDoc.OuterXml + "\r\n");
                rdr.Close();



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
                if(db.IsConnected)
                    db.CloseConnection();
                if (db != null)
                    db = null;
            }
        }


        //routines for receiving results
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
            PFMsAccess db = new PFMsAccess();

            Program._messageLog.WriteLine("DataTableTest started ...");
            try
            {
                db.DatabasePath = frm.cboDatabase.Text;
                db.DatabaseUsername = frm.txtDbUsername.Text;
                db.DatabasePassword = frm.txtDbPassword.Text;
                if (frm.cboAccessVersion.Text == "Access 2003")
                    db.OleDbProvider = PFAccessOleDbProvider.MicrosoftJetOLEDB_4_0;
                else
                    db.OleDbProvider = PFAccessOleDbProvider.MicrosoftACEOLEDB_12_0;

                
                db.OpenConnection();

                _msg.Length = 0;
                _msg.Append("Connection string is ");
                _msg.Append(db.ConnectionString);
                _msg.Append("\r\n");
                _msg.Append("Connection state is  ");
                _msg.Append(db.ConnectionState.ToString());
                _msg.Append("\r\n");
                _msg.Append("Query text is:\r\n");
                _msg.Append(frm.txtSqlQuery.Text.ToString());
                Program._messageLog.WriteLine(_msg.ToString());

                db.returnResult += new PFMsAccess.ResultDelegate(OutputResults);
                db.SQLQuery = frm.txtSqlQuery.Text;
                DataTable tab = db.RunQueryDataTable();
                db.ProcessDataTable(tab);
                db.returnResult -= OutputResults;

                db.returnResultAsString += new PFMsAccess.ResultAsStringDelegate(OutputResultsToFile);
                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\MsAccessTabDelimited.txt", PFFileOpenOperation.OpenFileForWrite);
                tab = db.RunQueryDataTable();
                db.ExtractDelimitedDataFromTable(tab, ",", "\r\n", true);

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\MsAccessTabFixedLength.txt", PFFileOpenOperation.OpenFileForWrite);
                tab = db.RunQueryDataTable();
                db.ExtractFixedLengthDataFromTable(tab, true, true, false);

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();

                tab = db.RunQueryDataTable();
                db.SaveDataTableToXmlFile(tab, @"c:\temp\MsAccessTestTab.xml");
                tab = db.RunQueryDataTable();
                db.SaveDataTableWithSchemaToXmlFile(tab, @"c:\temp\MsAccessTestTabPlus.xml");
                tab = db.RunQueryDataTable();
                db.SaveDataTableToXmlSchemaFile(tab, @"c:\temp\MsAccessTestTab.xsd");


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
                if (db.ConnectionState == ConnectionState.Open)
                    db.CloseConnection();
                Program._messageLog.WriteLine("... DataTableTest finished.");

            }
        }

        public static void DataSetTest(MainForm frm)
        {
            PFMsAccess db = new PFMsAccess();

            Program._messageLog.WriteLine("DataSetTest started ...");
            try
            {
                db.DatabasePath = frm.cboDatabase.Text;
                db.DatabaseUsername = frm.txtDbUsername.Text;
                db.DatabasePassword = frm.txtDbPassword.Text;
                if (frm.cboAccessVersion.Text == "Access 2003")
                    db.OleDbProvider = PFAccessOleDbProvider.MicrosoftJetOLEDB_4_0;
                else
                    db.OleDbProvider = PFAccessOleDbProvider.MicrosoftACEOLEDB_12_0;

                db.OpenConnection();

                _msg.Length = 0;
                _msg.Append("Connection string is ");
                _msg.Append(db.ConnectionString);
                _msg.Append("\r\n");
                _msg.Append("Connection state is  ");
                _msg.Append(db.ConnectionState.ToString());
                _msg.Append("\r\n");
                _msg.Append("Query text is:\r\n");
                _msg.Append(frm.txtSqlQuery.Text.ToString());
                Program._messageLog.WriteLine(_msg.ToString());

                db.returnResult += new PFMsAccess.ResultDelegate(OutputResults);
                db.SQLQuery = frm.txtSqlQuery.Text;
                DataSet ds = db.RunQueryDataSet();
                db.ProcessDataSet(ds);
                db.returnResult -= OutputResults;

                db.returnResultAsString += new PFMsAccess.ResultAsStringDelegate(OutputResultsToFile);
                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\MsAccessDsDelimited.txt", PFFileOpenOperation.OpenFileForWrite);
                ds = db.RunQueryDataSet();
                db.ExtractDelimitedDataFromDataSet(ds, ",", "\r\n", true);

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();
                _textFile.OpenFile(@"c:\temp\MsAccessDsFixedLength.txt", PFFileOpenOperation.OpenFileForWrite);
                ds = db.RunQueryDataSet();
                db.ExtractFixedLengthDataFromDataSet(ds, true, true, false);

                if (_textFile.FileIsOpen)
                    _textFile.CloseFile();

                ds = db.RunQueryDataSet();
                db.SaveDataSetToXmlFile(ds, @"c:\temp\MsAccessTestDs.xml");
                ds = db.RunQueryDataSet();
                db.SaveDataSetWithSchemaToXmlFile(ds, @"c:\temp\MsAccessTestDsPlus.xml");
                ds = db.RunQueryDataSet();
                db.SaveDataSetToXmlSchemaFile(ds, @"c:\temp\MsAccessTestDs.xsd");


                ds = db.RunQueryDataSet();
                PFDataProcessor dataProcessor = new PFDataProcessor();
                XmlDocument xmlDoc = dataProcessor.CopyDataSetToXmlDocument(ds);
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
                if (db.ConnectionState == ConnectionState.Open)
                    db.CloseConnection();
                Program._messageLog.WriteLine("... DataSetTest finished.");

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


        public static void GetQueryDataSchema(MainForm frm)
        {
            PFMsAccess db = new PFMsAccess();

            try
            {
                _msg.Length = 0;
                _msg.Append("GetQueryDataSchema started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                db.DatabasePath = frm.cboDatabase.Text;
                db.DatabaseUsername = frm.txtDbUsername.Text;
                db.DatabasePassword = frm.txtDbPassword.Text;
                if (frm.cboAccessVersion.Text == "Access 2003")
                    db.OleDbProvider = PFAccessOleDbProvider.MicrosoftJetOLEDB_4_0;
                else
                    db.OleDbProvider = PFAccessOleDbProvider.MicrosoftACEOLEDB_12_0;


                db.OpenConnection();

                _msg.Length = 0;
                _msg.Append("Connection string is ");
                _msg.Append(db.ConnectionString);
                _msg.Append("\r\n");
                _msg.Append("Connection state is  ");
                _msg.Append(db.ConnectionState.ToString());
                _msg.Append("\r\n");
                _msg.Append("Query text is:\r\n");
                _msg.Append(frm.txtSqlQuery.Text.ToString());
                Program._messageLog.WriteLine(_msg.ToString());

                db.returnResult += new PFMsAccess.ResultDelegate(OutputResults);
                db.SQLQuery = frm.txtSqlQuery.Text;

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
                _msg.Length = 0;
                _msg.Append("\r\n... GetQueryDataSchema finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        



    }//end class
}//end namespace
