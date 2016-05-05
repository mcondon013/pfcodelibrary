using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlServerCe;
using System.Data.Common;
using PFSQLServerCE35Objects;
using PFDataAccessObjects;
using System.Xml;
using PFUnitTestDataObjects;
using PFCollectionsObjects;
using PFListObjects;

namespace TestprogSQLCE35
{
    public class Tests35
    {
        private static StringBuilder _msg = new StringBuilder();
        private static StringBuilder _str = new StringBuilder();
        private static bool _saveErrorMessagesToAppLog = false;

        //properties
        public static bool SaveErrorMessagesToAppLog
        {
            get
            {
                return Tests35._saveErrorMessagesToAppLog;
            }
            set
            {
                Tests35._saveErrorMessagesToAppLog = value;
            }
        }

        //tests


        public static void ConnectionTest(MainForm frm)
        {
            PFSQLServerCE35 db = new PFSQLServerCE35();

            try
            {
                _msg.Length = 0;
                _msg.Append("ConnectionTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                db.DatabasePath = frm.txtDataSource.Text;
                db.DatabasePassword = frm.txtPassword.Text;
                db.EncryptionOn = frm.chkEncryptionOn.Checked;
                db.EncryptionMode = (SQLCE35EncryptionMode)Enum.Parse(typeof(SQLCE35EncryptionMode), frm.cboEncryptionMode.Text);
                db.OpenConnection();

                _msg.Length = 0;
                _msg.Append("Connection string is ");
                _msg.Append(db.ConnectionString);
                _msg.Append("\r\n");
                _msg.Append("Connection state is  ");
                _msg.Append(db.Connection.State.ToString());
                _msg.Append("\r\n");
                _msg.Append("Data Source is  ");
                _msg.Append(db.DatabasePath);
                _msg.Append("\r\n");
                _msg.Append("Password is  ");
                _msg.Append(db.DatabasePassword);
                _msg.Append("\r\n");
                _msg.Append("EncryptionOn is  ");
                _msg.Append(db.EncryptionOn.ToString());
                _msg.Append("\r\n");
                _msg.Append("EncryptionMode is  ");
                _msg.Append(db.EncryptionMode.ToString());
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
                 
        

        public static void CreateDatabaseTest(MainForm frm)
        {
            PFSQLServerCE35 db = new PFSQLServerCE35();
            string filename = string.Empty;
            string fileext = string.Empty;
            string defaultFileExt = AppConfig.GetStringValueFromConfigFile("DefaultDatabaseFileExtension",".sdf");
            string dataSource = frm.txtDataSource.Text;
            string connectionString = string.Empty;
            string createScript = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("Running CreateDatabaseTest ...");
                Program._messageLog.WriteLine(_msg.ToString());

                if (frm.txtDataSource.Text.Trim().Length == 0)
                {
                    throw new System.Exception("You must specify the data source for CreateDatabaseTest.");
                }
                if (Path.GetFileName(frm.txtDataSource.Text) == string.Empty)
                {
                    throw new System.Exception("You must specify the file name for CreateDatabaseTest.");
                }

                filename = Path.GetFileName(frm.txtDataSource.Text);
                fileext = Path.GetExtension(filename);
                if (fileext != defaultFileExt )
                {
                    dataSource = dataSource + defaultFileExt;
                }

                db.DatabasePath = dataSource;
                db.DatabasePassword = frm.txtPassword.Text;
                if (frm.chkEncryptionOn.Checked)
                {
                    db.EncryptionOn = true;
                    db.EncryptionMode = PFSQLServerCE35.GetEncryptionMode(frm.cboEncryptionMode.Text);
                }

                //if sdf file already exists, prompt user on whether or not delete
                if(File.Exists(dataSource))
                {
                    _msg.Length=0;
                    _msg.Append(dataSource);
                    _msg.Append(" already exists.\r\n");
                    DialogResult res = MessageBox.Show(_msg.ToString(),"Delete File?",MessageBoxButtons.YesNo);
                    if(res==DialogResult.Yes)
                    {
                        File.Delete(dataSource);
                    }
                }

                connectionString = db.ConnectionString;
                bool dbCreated = db.CreateDatabase(connectionString);
                _msg.Length=0;
                if(dbCreated)
                {
                    _msg.Append("Database create successful.");
                }
                else
                {
                    _msg.Append("Database create failed.");
                }
                
                Program._messageLog.WriteLine(_msg.ToString());

                Program._messageLog.WriteLine("\r\nCreating a table in the new database ...");
                DataTable dt = new DataTable("TestTabX1");
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
                bool result = db.CreateTable(dt, out createScript);


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
                _msg.Length = 0;
                _msg.Append("...CreateDatabaseTest Finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void ImportDataTableTest(MainForm frm)
        {
            PFSQLServerCE35 db = new PFSQLServerCE35();
            string fileName = @"c:\temp\AppSettings_35.sdf";
            string dataSource = fileName;
            string connectionString = string.Empty;
            string createScript = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("ImportDataTableTest started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                db.DatabasePath = dataSource;
                connectionString = db.ConnectionString;

                //create the database
                bool dbCreated = db.CreateDatabase(connectionString);
                if (dbCreated == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Attempt to create database ");
                    _msg.Append(fileName);
                    _msg.Append(" failed.");
                    throw new DataException(_msg.ToString());
                }

                db.OpenConnection();

                //create the table
                DataTable dt = frm.keyValsDataSet.Tables["KeyValTable"];
                db.CreateTable(dt);

                db.ImportDataFromDataTable(dt);

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

        public static void CreateTableTest(MainForm frm)
        {
            PFSQLServerCE35 sqlce = new PFSQLServerCE35();
            string dataSource = frm.txtDataSource.Text;
            string connectionString = string.Empty;
            string tableName = "TestTabX1";
            string tableName2 = "TestTabX2";
            string createScript = string.Empty;
            PFUnitTestDataTable unitTestDt01 = null;
            PFUnitTestDataTable unitTestDt02 = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("CreateTableTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                sqlce.DatabasePath = dataSource;
                sqlce.DatabasePassword = frm.txtPassword.Text;

                connectionString = sqlce.ConnectionString;
                if (connectionString.Length == 0)
                {
                    throw new System.Exception("sqlce.ConnectionString is empty.");
                }

                if (File.Exists(dataSource) == false)
                {
                    _msg.Length = 0;
                    _msg.Append(dataSource);
                    _msg.Append(" does not exist.\r\n");
                    throw new System.Exception(_msg.ToString());
                }

                sqlce.OpenConnection();

                _msg.Length = 0;
                _msg.Append("Table ");
                _msg.Append(tableName);
                if (sqlce.TableExists(tableName))
                {
                    if (sqlce.DropTable(tableName))
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
                _msg.Append("Table2 ");
                _msg.Append(tableName2);
                if (sqlce.TableExists(tableName2))
                {
                    if (sqlce.DropTable(tableName2))
                        _msg.Append(" dropped.");
                    else
                        _msg.Append(" drop failed.");
                }
                else
                {
                    _msg.Append(" does not exist.");
                }
                Program._messageLog.WriteLine(_msg.ToString());



                IDatabaseProvider db = (IDatabaseProvider)sqlce;

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
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Char", "A"));
                dataTypesToInclude.Add(new KeyValuePair<string, string>("System.Char[]", "ABCDEFGHIJKLMNOPQRSTUVWXYZ"));
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



                /*
                Program._messageLog.WriteLine("\r\nCreating table in the database ...");
                DataTable dt = new DataTable(tableName);
                DataColumn k1 = new DataColumn("K1", Type.GetType("System.Int32"));
                k1.AllowDBNull = false;
                dt.Columns.Add(k1);
                DataColumn f1 = new DataColumn("F1", Type.GetType("System.String"));
                f1.MaxLength = 50;
                dt.Columns.Add(f1);

                createScript = string.Empty;
                bool result = sqlce.CreateTable(dt, out createScript);

                _msg.Length = 0;
                _msg.Append("Table ");
                _msg.Append(tableName);
                if (sqlce.TableExists(tableName))
                {
                    _msg.Append(" created.");
                }
                else
                {
                    _msg.Append(" create failed.");
                }
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
                if (sqlce != null)
                    if (sqlce.IsConnected)
                        sqlce.CloseConnection();
                sqlce = null;
                _msg.Length = 0;
                _msg.Append("Create Table Script: \r\n");
                _msg.Append(createScript);
                Program._messageLog.WriteLine(_msg.ToString());
                _msg.Length = 0;
                _msg.Append("\r\n... CreateTableTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        
        

        public static void RunQueryTest(MainForm frm)
        {
            PFSQLServerCE35 db = new PFSQLServerCE35();
            string filename = string.Empty;
            string fileext = string.Empty;
            string defaultFileExt = AppConfig.GetStringValueFromConfigFile("DefaultDatabaseFileExtension", ".sdf");
            string dataSource = frm.txtDataSource.Text;
            string connectionString = string.Empty;

            _msg.Length = 0;
            _msg.Append("Run query test ...");
            Program._messageLog.WriteLine(_msg.ToString());

            try
            {
                if (frm.txtDataSource.Text.Trim().Length == 0)
                {
                    throw new System.Exception("You must specify the data source.");
                }
                if (Path.GetFileName(frm.txtDataSource.Text) == string.Empty)
                {
                    throw new System.Exception("You must specify the file name.");
                }
                if (frm.txtQuery.Text.Trim().Length == 0)
                {
                    throw new System.Exception("You must specify a query to run.");
                }

                filename = Path.GetFileName(frm.txtDataSource.Text);
                fileext = Path.GetExtension(filename);
                if (fileext != defaultFileExt)
                {
                    dataSource = dataSource + defaultFileExt;
                }

                db.DatabasePath = dataSource;
                db.DatabasePassword = frm.txtPassword.Text;

                connectionString = db.ConnectionString;
                if (connectionString.Length == 0)
                {
                    throw new System.Exception("sqlce.ConnectionString is empty.");
                }

                if (File.Exists(dataSource)==false)
                {
                    _msg.Length = 0;
                    _msg.Append(dataSource);
                    _msg.Append(" does not exist.\r\n");
                    throw new System.Exception(_msg.ToString());
                }


                if (frm.optNonQuery.Checked)
                {
                    int numRowsAffected = RunNonQueryTest(frm, db);
                }
                if (frm.optReader.Checked)
                {
                    RunReaderTest(frm, db);
                }
                if (frm.optRdrToDt.Checked)
                {
                    RunReaderToDataTableTest(frm, db);
                }
                if (frm.optResultset.Checked)
                {
                    RunResultsetTest(frm, db);
                }
                if (frm.optRsToDt.Checked)
                {
                    RunResultsetToDataTableTest(frm, db);
                }
                if (frm.optDataset.Checked)
                {
                    RunDatasetTest(frm, db);
                }
                if (frm.optDataTable.Checked)
                {
                    RunDataTableTest(frm, db);
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
                _msg.Append("...RunQuery Test Finished.");
                Program._messageLog.WriteLine(_msg.ToString());
            }

        }



        public static int RunNonQueryTest(MainForm frm, PFSQLServerCE35 db)
        {
            int numRowsAffected = -1;
            string query = string.Empty;

            _msg.Length = 0;
            _msg.Append("Running RunNonQueryTest ...");
            Program._messageLog.WriteLine(_msg.ToString());

            try
            {
                query = frm.txtQuery.Text;
                db.OpenConnection();
                numRowsAffected = db.RunNonQuery(query);
                db.CloseConnection();
                _msg.Length = 0;
                _msg.Append("Query: ");
                _msg.Append(frm.txtQuery.Text);
                _msg.Append("\r\n");
                _msg.Append("Num rows affected: ");
                _msg.Append(numRowsAffected.ToString("#,##0"));
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
                _msg.Append("...RunNonQueryTest Finished.");
                Program._messageLog.WriteLine(_msg.ToString());
            }

            return numRowsAffected;

        }

        public static void RunReaderTest(MainForm frm, PFSQLServerCE35 db)
        {

            string query = string.Empty;

            _msg.Length = 0;
            _msg.Append("Running RunReaderTest ...");
            Program._messageLog.WriteLine(_msg.ToString());

            try
            {
                query = frm.txtQuery.Text;
                db.OpenConnection();
                SqlCeDataReader rdr = (SqlCeDataReader)db.RunQueryDataReader(query);

                db.returnResult += new PFSQLServerCE35.ResultDelegate(OutputResults);
                //sqlce.returnResultAsString += new PFSQLServerCE35.ResultAsStringDelegate(OutputResultsAsString);
                db.ProcessDataReader(rdr);
                rdr.Close();


                rdr = (SqlCeDataReader)db.RunQueryDataReader(query);
                db.SaveDataReaderToXmlFile(rdr, @"c:\temp\TestCe35Rdr.xml");
                rdr.Close();

                rdr = (SqlCeDataReader)db.RunQueryDataReader(query);
                db.SaveDataReaderWithSchemaToXmlFile(rdr, @"c:\temp\TestCe35RdrPlus.xml");
                rdr.Close();

                rdr = (SqlCeDataReader)db.RunQueryDataReader(query);
                db.SaveDataReaderToXmlSchemaFile(rdr, @"c:\temp\TestCe35Rdr.xsd");
                rdr.Close();

                rdr = (SqlCeDataReader)db.RunQueryDataReader(query);
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
                _msg.Length = 0;
                _msg.Append("...RunReaderTest Finished.");
                Program._messageLog.WriteLine(_msg.ToString());
            }

        }

        public static void RunReaderToDataTableTest(MainForm frm, PFSQLServerCE35 db)
        {

            string query = string.Empty;

            _msg.Length = 0;
            _msg.Append("Running RunReaderToDataTableTest ...");
            Program._messageLog.WriteLine(_msg.ToString());

            try
            {
                query = frm.txtQuery.Text;
                db.OpenConnection();
                SqlCeDataReader rdr = (SqlCeDataReader)db.RunQueryDataReader(query);

                DataTable tab = db.ConvertDataReaderToDataTable(rdr);

                _msg.Length = 0;
                _msg.Append("Number of rows in table: ");
                _msg.Append(tab.Rows.Count);
                Program._messageLog.WriteLine(_msg.ToString());

                int inx = 0;
                int maxInx = tab.Rows.Count - 1;
                int inxCol = 0;
                int maxInxCol = tab.Columns.Count - 1;
                for (inx = 0; inx <= maxInx; inx++)
                {
                    _msg.Length = 0;
                    DataRow row = tab.Rows[inx];
                    for (inxCol = 0; inxCol <= maxInxCol; inxCol++)
                    {
                        DataColumn col = tab.Columns[inxCol];
                        _msg.Append(col.ColumnName);
                        _msg.Append(": ");
                        _msg.Append(row[inxCol].ToString());
                        if (inxCol < maxInxCol)
                            _msg.Append(", ");
                    }
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
                _msg.Length = 0;
                _msg.Append("...RunReaderToDataTableTest Finished.");
                Program._messageLog.WriteLine(_msg.ToString());
            }

        }

        
        public static void RunResultsetTest(MainForm frm, PFSQLServerCE35 db)
        {
            SqlCeResultSet res = null;
            string query = string.Empty;

            _msg.Length = 0;
            _msg.Append("Running RunResultsetTest ...");
            Program._messageLog.WriteLine(_msg.ToString());

            try
            {
                query = frm.txtQuery.Text;
                db.OpenConnection();
                res = db.RunQueryResultset(query);
                if (res.HasRows)
                {
                    db.returnResult += new PFSQLServerCE35.ResultDelegate(OutputResults);
                    //sqlce.returnResultAsString += new PFSQLServerCE.ResultAsStringDelegate(OutputResultsAsString);
                    db.ProcessResultSet(res);
                    res.Close();
                    res = db.RunQueryResultset(query);
                    db.returnResultAsString += new PFSQLServerCE35.ResultAsStringDelegate(OutputExtractFormattedData);
                    db.ExtractDelimitedDataFromResultSet(res, "~", "\r\n", true);
                    res.Close();

                    res = db.RunQueryResultset(query);
                    db.SaveResultSetToXmlFile(res, @"c:\temp\TestCe35Res.xml");
                    res.Close();

                    res = db.RunQueryResultset(query);
                    db.SaveResultSetWithSchemaToXmlFile(res, @"c:\temp\TestCe35ResPlus.xml");
                    res.Close();

                    res = db.RunQueryResultset(query);
                    db.SaveResultSetToXmlSchemaFile(res, @"c:\temp\TestCe35Res.xsd");
                    res.Close();

                    res = db.RunQueryResultset(query);
                    PFDataProcessor dataProcessor = new PFDataProcessor();
                    XmlDocument xmlDoc = dataProcessor.CopyDataTableToXmlDocument(PFSQLServerCE35.ConvertResultSetToDataTable(res, "ResultTable"));
                    Program._messageLog.WriteLine("\r\n" + xmlDoc.OuterXml + "\r\n");
                    res.Close();

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
                _msg.Length = 0;
                _msg.Append("...RunResultsetTest Finished.");
                Program._messageLog.WriteLine(_msg.ToString());
            }

        }

        public static void RunResultsetToDataTableTest(MainForm frm, PFSQLServerCE35 db)
        {
            SqlCeResultSet res = null;
            string query = string.Empty;

            _msg.Length = 0;
            _msg.Append("Running RunResultsetToDataTableTest ...");
            Program._messageLog.WriteLine(_msg.ToString());

            try
            {
                query = frm.txtQuery.Text;
                db.OpenConnection();
                res = db.RunQueryResultset(query);

                if (res.HasRows)
                {
                    DataTable tab = PFSQLServerCE35.ConvertResultSetToDataTable(res);

                    _msg.Length = 0;
                    _msg.Append("Number of rows in table: ");
                    _msg.Append(tab.Rows.Count);
                    Program._messageLog.WriteLine(_msg.ToString());

                    int inx = 0;
                    int maxInx = tab.Rows.Count - 1;
                    int inxCol = 0;
                    int maxInxCol = tab.Columns.Count - 1;
                    for (inx = 0; inx <= maxInx; inx++)
                    {
                        _msg.Length = 0;
                        DataRow row = tab.Rows[inx];
                        for (inxCol = 0; inxCol <= maxInxCol; inxCol++)
                        {
                            DataColumn col = tab.Columns[inxCol];
                            _msg.Append(col.ColumnName);
                            _msg.Append(": ");
                            _msg.Append(row[inxCol].ToString());
                            if (inxCol < maxInxCol)
                                _msg.Append(", ");
                        }
                        Program._messageLog.WriteLine(_msg.ToString());
                    }
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("No rows in resultset");
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
                _msg.Length = 0;
                _msg.Append("...RunResultsetToDataTableTest Finished.");
                Program._messageLog.WriteLine(_msg.ToString());
            }

        }

        public static void RunDatasetTest(MainForm frm, PFSQLServerCE35 db)
        {
            string query = string.Empty;
            DataSet ds;
            int numRows = -1;

            _msg.Length = 0;
            _msg.Append("Running RunDatasetTest ...");
            Program._messageLog.WriteLine(_msg.ToString());

            try
            {
                query = frm.txtQuery.Text;
                db.OpenConnection();
                ds = db.RunQueryDataset(query, "dstest");
                if (ds.Tables["dstest"] != null)
                    if (ds.Tables["dstest"].Rows.Count > 0)
                        numRows = ds.Tables["dstest"].Rows.Count;
                _msg.Length = 0;
                _msg.Append("Number of rows in ");
                _msg.Append(ds.Tables[0].TableName);
                _msg.Append(": ");
                _msg.Append(numRows.ToString("#,##0"));
                Program._messageLog.WriteLine(_msg.ToString());

                db.returnResult += new PFSQLServerCE35.ResultDelegate(OutputResults);
                db.ProcessDataSet(ds);
                db.returnResultAsString += new PFSQLServerCE35.ResultAsStringDelegate(OutputExtractFormattedData);
                db.ExtractDelimitedDataFromDataSet(ds, "\t", "\r\n", true);

                db.SaveDataSetToXmlFile(ds, @"c:\Temp\TestCe35Ds.xml");
                db.SaveDataSetWithSchemaToXmlFile(ds, @"c:\Temp\TestCe35DsPlus.xml");
                db.SaveDataSetToXmlSchemaFile(ds, @"c:\Temp\TestCe35Ds.xsd");

                PFDataProcessor dataProcessor = new PFDataProcessor();
                XmlDocument xmlDoc = dataProcessor.CopyDataSetToXmlDocument(ds);
                Program._messageLog.WriteLine("\r\n" + xmlDoc.OuterXml + "\r\n");

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
                _msg.Append("...RunDatasetTest Finished.");
                Program._messageLog.WriteLine(_msg.ToString());
            }

        }

        public static void RunDataTableTest(MainForm frm, PFSQLServerCE35 db)
        {
            string query = string.Empty;
            DataTable tab = null;

            _msg.Length = 0;
            _msg.Append("Running RunDataTableTest ...");
            Program._messageLog.WriteLine(_msg.ToString());

            try
            {
                query = frm.txtQuery.Text;
                db.OpenConnection();
                tab = db.RunQueryDataTable(query, "tabtest");
                _msg.Length = 0;
                _msg.Append("Number of rows in table: ");
                _msg.Append(tab.Rows.Count.ToString("#,##0"));
                Program._messageLog.WriteLine(_msg.ToString());

                db.returnResult += new PFSQLServerCE35.ResultDelegate(OutputResults);
                db.ProcessDataTable(tab);
                db.returnResultAsString += new PFSQLServerCE35.ResultAsStringDelegate(OutputExtractFormattedData);
                db.ExtractDelimitedDataFromTable(tab, ",", "\r\n", true);

                db.SaveDataTableToXmlFile(tab, @"c:\temp\TestCe35Tab.xml");
                db.SaveDataTableWithSchemaToXmlFile(tab, @"c:\temp\TestCe35TabPlus.xml");
                db.SaveDataTableToXmlSchemaFile(tab, @"c:\temp\TestCe35Tab.xsd");

                PFDataProcessor dataProcessor = new PFDataProcessor();
                XmlDocument xmlDoc = dataProcessor.CopyDataTableToXmlDocument(tab);
                Program._messageLog.WriteLine("\r\n" + xmlDoc.OuterXml + "\r\n");

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
                _msg.Append("...RunDataTableTest Finished.");
                Program._messageLog.WriteLine(_msg.ToString());
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



    }//end class
}//end namespace
