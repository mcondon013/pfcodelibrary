using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Data;
using System.Data.Common;
using PFDataAccessObjects;
using PFCollectionsObjects;
using PFTimers;

namespace TestprogGetSchemas
{
    public class Tests
    {
        private static StringBuilder _msg = new StringBuilder();
        private static StringBuilder _str = new StringBuilder();
        private static bool _saveErrorMessagesToAppLog = false;

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

        public static void GetProviderList(MainForm frm)
        {
            DataTable dt = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("GetProviderList started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                dt = DbProviderFactories.GetFactoryClasses();

                foreach (DataRow dr in dt.Rows)
                {
                    _msg.Length = 0;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        _msg.Append(dc.ColumnName);
                        _msg.Append("=");
                        _msg.Append(dr[dc.ColumnName].ToString());
                        _msg.Append("  ");
                    }
                    _msg.Append("\r\n");
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
                _msg.Append("\r\n... GetProviderList finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void GetMetadataCollections(MainForm frm)
        {
            string provider = string.Empty;
            string connectionString = string.Empty;
            DbConnection conn = null;

            try
            {

                GetProviderAndConnectionString(frm, ref provider, ref connectionString);

                conn = GetDbConnectionObject(frm, provider, connectionString);

                GetSchema(conn, "MetaDataCollections");


            
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
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Dispose();
                    conn = null;
                }
                _msg.Length = 0;
                _msg.Append("... GetMetadataCollections finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void GetTablesCollection(MainForm frm)
        {
            string provider = string.Empty;
            string connectionString = string.Empty;
            DbConnection conn = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("GetTablesCollection started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                GetProviderAndConnectionString(frm, ref provider, ref connectionString);

                conn = GetDbConnectionObject(frm, provider, connectionString);

                GetSchema(conn, "Tables");

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
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Dispose();
                    conn = null;
                }
                _msg.Length = 0;
                _msg.Append("... GetTablesCollection finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void GetRestrictionsCollection(MainForm frm)
        {
            string provider = string.Empty;
            string connectionString = string.Empty;
            DbConnection conn = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("GetRestrictionsCollection started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                GetProviderAndConnectionString(frm, ref provider, ref connectionString);

                conn = GetDbConnectionObject(frm, provider, connectionString);

                GetSchema(conn, "Restrictions");

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
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Dispose();
                    conn = null;
                }
                _msg.Length = 0;
                _msg.Append("... GetRestrictionsCollection finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void GetDataTypesCollection(MainForm frm)
        {
            string provider = string.Empty;
            string connectionString = string.Empty;
            DbConnection conn = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("GetDataTypesCollection started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                GetProviderAndConnectionString(frm, ref provider, ref connectionString);

                conn = GetDbConnectionObject(frm, provider, connectionString);

                GetSchema(conn, "DataTypes");

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
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Dispose();
                    conn = null;
                }
                _msg.Length = 0;
                _msg.Append("... GetDataTypesCollection finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        private static void GetProviderAndConnectionString(MainForm frm, ref string provider, ref string connectionString)
        {
            string[] parsedString = null;
            if (frm.optUseDotNetProvider.Checked)
            {
                parsedString = frm.cboConnectionString.Text.Split('|');
                provider = parsedString[0];
                connectionString = parsedString[1];
            }
            else if (frm.optUseOdbcDriver.Checked)
            {
                provider = "System.Data.Odbc";
                connectionString = frm.cboConnectionString.Text;
            }
            else if (frm.optUseOleDbProvider.Checked)
            {
                provider = "System.Data.OleDb";
                connectionString = frm.cboConnectionString.Text;
            }
            else
            {
                _msg.Length = 0;
                _msg.Append("You must select .NET, Odbc or OleDb datatype type.");
                throw new System.Exception(_msg.ToString());
            }

            _msg.Length = 0;
            _msg.Append("\r\n");
            _msg.Append("Provider = ");
            _msg.Append(provider);
            _msg.Append("\r\n");
            _msg.Append("ConnectionString = ");
            _msg.Append(connectionString);
            _msg.Append("\r\n");
            Program._messageLog.WriteLine(_msg.ToString());

        }

        private static DbConnection GetDbConnectionObject(MainForm frm, string provider, string connectionString)
        {
            DbConnection conn = null;
            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

            conn = factory.CreateConnection();
            if (conn != null)
                conn.ConnectionString = connectionString;
            else
            {
                _msg.Length = 0;
                _msg.Append("Unable to create connection using .NET factory for ");
                _msg.Append(provider);
                _msg.Append(", ConnectionString: ");
                _msg.Append(connectionString);
                throw new System.Exception(_msg.ToString());
            }

            conn.Open();

            _msg.Length = 0;
            if (conn.State == ConnectionState.Open)
            {
                _msg.Append("Connection opened successfully for ");
                _msg.Append(frm.cboConnectionString.Text);
            }
            else
            {
                _msg.Append("Connection open failed for ");
                _msg.Append(frm.cboConnectionString.Text);
                throw new System.Exception(_msg.ToString());
            }
            Program._messageLog.WriteLine(_msg.ToString());


            return conn;
        }

        private static void GetSchema(DbConnection conn,  string collectionName)
        {
            DataTable dt = null;

            dt = conn.GetSchema(collectionName);


            foreach (DataRow dr in dt.Rows)
            {
                _msg.Length = 0;
                foreach (DataColumn dc in dt.Columns)
                {
                    _msg.Append(dc.ColumnName);
                    _msg.Append(" = ");
                    _msg.Append(dr[dc.ColumnName].ToString());
                    _msg.Append("  ");
                }
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());
            }


        }



        public static void GetSQLServerTables(MainForm frm)
        {
            Stopwatch sw = new Stopwatch();

            string sourceConnectionString = @"Data Source=PROFASTWS3; Initial Catalog=AdventureWorksDW2008R2; Integrated Security=True; Application Name=TestprogGetSchemas; Workstation ID=PROFASTWS5;";
            PFSQLServer sourceDb = new PFSQLServer();

            string destinationConnectionString = @"Data Source=PROFASTSV2; Initial Catalog=AWTest; Integrated Security=True; Application Name=TestprogGetSchemas; Workstation ID=PROFASTWS5;";
            PFSQLServer destDb = new PFSQLServer();

            PFTableDefinitions tabdefs = new PFTableDefinitions();
            
            try
            {
                _msg.Length = 0;
                _msg.Append("GetSQLServerTables started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                sourceDb.ConnectionString = sourceConnectionString;
                sourceDb.OpenConnection();


                Program._messageLog.WriteLine("Get all table names:\r\n");

                string[] includes = new string[4];
                includes[0] = "dbo.DimDate";
                includes[1] = "dbo.DimC*";
                includes[2] = "dbo.DimGeography";
                includes[3] = "dbo.FactCurrencyRate";
                
                //PFList<PFTableDef> tableDefs = sourceDb.GetTableList();
                PFList<PFTableDef> tableDefs = tabdefs.GetTableList(sourceDb);
                //PFList<PFTableDef> tableDefs = tabdefs.GetTableList(sourceDb);
                PFTableDef td = null;

                tableDefs.SetToBOF();

                while ((td = tableDefs.NextItem) != null)
                {
                    _msg.Length = 0;
                    _msg.Append(td.TableFullName);
                    _msg.Append(":\r\n");
                    _msg.Append(td.TableCreateStatement);
                    _msg.Append("\r\n");
                    Program._messageLog.WriteLine(_msg.ToString());
                }


                
                _msg.Length = 0;
                _msg.Append("\r\nConverted Table Defs follow: \r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                destDb.ConnectionString = destinationConnectionString;
                destDb.OpenConnection();

                PFList<PFTableDef> newTableDefs = destDb.ConvertTableDefs(tableDefs, "xyz");
                PFTableDef newtd = null;

                newTableDefs.SetToBOF();

                while ((newtd = newTableDefs.NextItem) != null)
                {
                    _msg.Length = 0;
                    _msg.Append(newtd.TableFullName);
                    _msg.Append(":\r\n");
                    _msg.Append(newtd.TableCreateStatement);
                    _msg.Append("\r\n");
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                _msg.Length = 0;
                _msg.Append("\r\nTesting table creates: \r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                newTableDefs.SetToBOF();
                int numTabsCreated = destDb.CreateTablesFromTableDefs(newTableDefs,true);

                sw.Stop();

                _msg.Length = 0;
                _msg.Append("\r\nNumber of tables created: \r\n");
                _msg.Append(numTabsCreated.ToString());
                _msg.Append("\r\n");
                _msg.Append("Elapsed time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("\r\nTesting table copies: \r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                newTableDefs.SetToBOF();
                PFDatabase dbtemp = new PFDatabase(DatabasePlatform.MSSQLServer);
                dbtemp.ConnectionString = sourceDb.ConnectionString;
                dbtemp.OpenConnection();
                //PFList<TableCopyDetails> tableCopyLog = destDb.CopyTableDataFromTableDefs(dbtemp, includes, null, "xyz", true);
                PFList<TableCopyDetails> tableCopyLog = destDb.CopyTableDataFromTableDefs(dbtemp, null, null, "xyz", true);
                
                dbtemp.CloseConnection();

                sw.Stop();


                _msg.Length = 0;
                _msg.Append("\r\nTable copies finished: \r\n");
                _msg.Append("Elapsed time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                tableCopyLog.SetToBOF();
                TableCopyDetails tcdetails = null;

                while ((tcdetails = tableCopyLog.NextItem) != null)
                {
                    _msg.Length = 0;
                    _msg.Append("Table: ");
                    _msg.Append(tcdetails.destinationTableName);
                    _msg.Append(", NumRowsCopied: ");
                    _msg.Append(tcdetails.numRowsCopied.ToString("#,##0"));
                    if(tcdetails.result != TableCopyResult.Success)
                    {
                        _msg.Append("\r\n    ");
                        _msg.Append("Result: ");
                        _msg.Append(tcdetails.result.ToString());
                        _msg.Append("  Messages: ");
                        _msg.Append(tcdetails.messages);
                    }
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
                if (sourceDb.IsConnected)
                {
                    sourceDb.CloseConnection();
                }
                if (destDb.IsConnected)
                {
                    destDb.CloseConnection();
                }
                sourceDb = null;
                destDb = null;
                _msg.Length = 0;
                _msg.Append("\r\n... GetSQLServerTables finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void TablePatternMatchTest(MainForm frm)
        {
            string sourceConnectionString = @"Data Source=PROFASTWS3; Initial Catalog=AdventureWorksDW2008R2; Integrated Security=True; Application Name=TestprogGetSchemas; Workstation ID=PROFASTWS5;";
            PFSQLServer sourceDb = new PFSQLServer();

            PFTableDefinitions tabdefs = new PFTableDefinitions();

            try
            {
                _msg.Length = 0;
                _msg.Append("TablePatternMatchTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                sourceDb.ConnectionString = sourceConnectionString;
                sourceDb.OpenConnection();

                Program._messageLog.WriteLine("Get all table names:\r\n");

                //PFList<PFTableDef> tableDefs = sourceDb.GetTableList();
                //PFList<PFTableDef> tableDefs = tabdefs.GetTableList(sourceDb, "*.DimC*", string.Empty);
                PFList<PFTableDef> tableDefs = tabdefs.GetTableList(sourceDb);
                PFTableDef td = null;

                tableDefs.SetToBOF();

                while ((td = tableDefs.NextItem) != null)
                {
                    _msg.Length = 0;
                    _msg.Append(td.TableFullName);
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                _msg.Length = 0;
                _msg.Append("\r\nInclude: ");
                _msg.Append("*.DimC*");
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                tableDefs = tabdefs.GetTableList(sourceDb, new string[1] {"*.DimC*"}, null);
                td = null;

                tableDefs.SetToBOF();

                while ((td = tableDefs.NextItem) != null)
                {
                    _msg.Length = 0;
                    _msg.Append(td.TableFullName);
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                _msg.Length = 0;
                _msg.Append("\r\nexclude: ");
                _msg.Append("dbo.Fact*");
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                tableDefs = tabdefs.GetTableList(sourceDb, null, new string[1] {"dbo.Fact*"});
                td = null;

                tableDefs.SetToBOF();

                while ((td = tableDefs.NextItem) != null)
                {
                    _msg.Length = 0;
                    _msg.Append(td.TableFullName);
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                string[] includes = new string[3];
                includes[0] = "dbo.DimDate";
                includes[1] = "dbo.DimC*";
                includes[2] = "dbo.FactFinance";
                string[] excludes = new string[5];
                excludes[0] = "dbo.DimC*";
                excludes[1] = "dbo.DimGeography";
                excludes[2] = "dbo.FactFinance";
                excludes[3] = "dbo.MikeTab01";
                excludes[4] = "dbo.TestTable01";

                _msg.Length = 0;
                _msg.Append("\r\n<includes>: ");
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                tableDefs = tabdefs.GetTableList(sourceDb, includes, null);
                td = null;

                tableDefs.SetToBOF();

                while ((td = tableDefs.NextItem) != null)
                {
                    _msg.Length = 0;
                    _msg.Append(td.TableFullName);
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                _msg.Length = 0;
                _msg.Append("\r\n<excludes>: ");
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                tableDefs = tabdefs.GetTableList(sourceDb, null, excludes);
                td = null;

                tableDefs.SetToBOF();

                while ((td = tableDefs.NextItem) != null)
                {
                    _msg.Length = 0;
                    _msg.Append(td.TableFullName);
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                includes = new string[3];
                includes[0] = "dbo.DimDate";
                includes[1] = "dbo.DimC*";
                includes[2] = "dbo.FactS*";
                excludes = new string[2];
                excludes[0] = "dbo.DimCurrency";
                excludes[1] = "dbo.FactSurveyResponse";

                _msg.Length = 0;
                _msg.Append("\r\n<includes/excludes>: ");
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                tableDefs = tabdefs.GetTableList(sourceDb, includes, excludes);
                td = null;

                tableDefs.SetToBOF();

                while ((td = tableDefs.NextItem) != null)
                {
                    _msg.Length = 0;
                    _msg.Append(td.TableFullName);
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
                _msg.Append("\r\n... TablePatternMatchTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



    }//end class
}//end namespace
