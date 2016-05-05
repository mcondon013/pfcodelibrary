using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using PFDataAccessObjects;
using PFCollectionsObjects;
using PFTimers;

namespace TestprogTableDefs
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
        public static void GetTabDefList(MainForm frm)
        {
            //PFTableDefinitions tabDefs = new PFTableDefinitions();
            PFList<PFTableDef> tabDefList = null;
            PFDatabase db = null;
            string dbAssemblyPath = string.Empty;
            string[] includes = null;
            string[] excludes = null;
            string[] lineTerminators = { "\r\n", Environment.NewLine };
            string dbPlatformDesc = frm.cboSourceDbPlatform.Text;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("GetTabDefList started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                string configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
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


                if (frm.cboSourceDbPlatform.Text == DatabasePlatform.SQLServerCE35.ToString())
                {
                    dbAssemblyPath = AppConfig.GetStringValueFromConfigFile("SQLServerCE_V35_AssemblyPath",string.Empty);
                    db = new PFDatabase(frm.cboSourceDbPlatform.Text, dbAssemblyPath, nmSpace + "." + clsName);
                }
                else if (frm.cboSourceDbPlatform.Text == DatabasePlatform.SQLServerCE40.ToString())
                {
                    dbAssemblyPath = AppConfig.GetStringValueFromConfigFile("SQLServerCE_V40_AssemblyPath",string.Empty);
                    db = new PFDatabase(frm.cboSourceDbPlatform.Text, dbAssemblyPath, nmSpace + "." + clsName);
                }
                else
                {
                    db = new PFDatabase(frm.cboSourceDbPlatform.Text, dllPath, nmSpace + "." + clsName);
                }

                db.ConnectionString = frm.cboSourceDbConnectionString.Text;
                db.OpenConnection();

                if (frm.txtIncludePatterns.Text.Trim().Length > 0)
                {
                    includes = frm.txtIncludePatterns.Text.Split(lineTerminators, StringSplitOptions.None);
                    if (includes.Length > 0)
                    {
                        for (int i = 0; i < includes.Length; i++)
                        {
                            if (includes[i].Length == 0)
                                includes[i] = "ignore this include";
                        }
                    }
                }
                if (frm.txtExcludePatterns.Text.Trim().Length > 0)
                {
                    excludes = frm.txtExcludePatterns.Text.Split(lineTerminators, StringSplitOptions.None);
                    if (excludes.Length > 1)
                    {
                        for (int i = 0; i < excludes.Length; i++)
                        {
                            if (excludes[i].Length == 0)
                                excludes[i] = "ignore this exclude";
                        }
                    }
                }

                tabDefList = db.GetTableList(includes, excludes);

                PFTableDef td = null;

                tabDefList.SetToBOF();

                while ((td = tabDefList.NextItem) != null)
                {
                    _msg.Length = 0;
                    _msg.Append(td.TableFullName);
                    if (frm.chkShowTableCreateStatements.Checked)
                    {
                        _msg.Append(":\r\n");
                        _msg.Append(td.TableCreateStatement);
                        _msg.Append("\r\n");
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
                //tabDefs = null;
                tabDefList = null;
                if (db.IsConnected)
                {
                    db.CloseConnection();
                }
                db = null;
                _msg.Length = 0;
                _msg.Append("\r\n... GetTabDefList finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void ConvertTableDefs(MainForm frm)
        {
            Stopwatch sw = new Stopwatch();
            string dbAssemblyPath = string.Empty;
            PFDatabase sourceDb = null;
            string sourceConnectionString = frm.cboSourceDbConnectionString.Text;
            PFDatabase destDb = null;
            string destinationConnectionString = frm.cboDestinationDbConnectionString.Text;
            PFList<PFTableDef> tabDefList = null;
            string[] includes = null;
            string[] excludes = null;
            string[] lineTerminators = { "\r\n", Environment.NewLine };

            try
            {
                _msg.Length = 0;
                _msg.Append("ConvertTableDefs started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                if (frm.cboSourceDbPlatform.Text == DatabasePlatform.SQLServerCE35.ToString())
                {
                    dbAssemblyPath = AppConfig.GetStringValueFromConfigFile("SQLServerCE_V35_AssemblyPath", string.Empty);
                    sourceDb = new PFDatabase(frm.cboSourceDbPlatform.Text, dbAssemblyPath);
                }
                else if (frm.cboSourceDbPlatform.Text == DatabasePlatform.SQLServerCE40.ToString())
                {
                    dbAssemblyPath = AppConfig.GetStringValueFromConfigFile("SQLServerCE_V40_AssemblyPath", string.Empty);
                    sourceDb = new PFDatabase(frm.cboSourceDbPlatform.Text, dbAssemblyPath);
                }
                else
                {
                    sourceDb = new PFDatabase(frm.cboSourceDbPlatform.Text);
                }

                if (frm.cboDestinationDbPlatform.Text == DatabasePlatform.SQLServerCE35.ToString())
                {
                    dbAssemblyPath = AppConfig.GetStringValueFromConfigFile("SQLServerCE_V35_AssemblyPath", string.Empty);
                    destDb = new PFDatabase(frm.cboDestinationDbPlatform.Text, dbAssemblyPath);
                }
                else if (frm.cboDestinationDbPlatform.Text == DatabasePlatform.SQLServerCE40.ToString())
                {
                    dbAssemblyPath = AppConfig.GetStringValueFromConfigFile("SQLServerCE_V40_AssemblyPath", string.Empty);
                    destDb = new PFDatabase(frm.cboDestinationDbPlatform.Text, dbAssemblyPath);
                }
                else
                {
                    destDb = new PFDatabase(frm.cboDestinationDbPlatform.Text);
                }



                sourceDb.ConnectionString = sourceConnectionString;
                sourceDb.OpenConnection();

                if (frm.txtIncludePatterns.Text.Trim().Length > 0)
                {
                    includes = frm.txtIncludePatterns.Text.Split(lineTerminators, StringSplitOptions.None);
                }
                if (frm.txtExcludePatterns.Text.Trim().Length > 0)
                {
                    excludes = frm.txtExcludePatterns.Text.Split(lineTerminators, StringSplitOptions.None);
                }

                tabDefList = sourceDb.GetTableList(includes, excludes);

                destDb.ConnectionString = destinationConnectionString;
                destDb.OpenConnection();

                PFList<PFTableDef> newTableDefs = destDb.ConvertTableDefs(tabDefList, frm.txtNewSchema.Text.Trim());
                PFTableDef newtd = null;

                newTableDefs.SetToBOF();

                while ((newtd = newTableDefs.NextItem) != null)
                {
                    _msg.Length = 0;
                    _msg.Append(newtd.TableFullName);
                    if (frm.chkShowTableCreateStatements.Checked)
                    {
                        _msg.Append(":\r\n");
                        _msg.Append(newtd.TableCreateStatement);
                        _msg.Append("\r\n");
                    }
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                if (frm.chkRunConvertedTableCreateStatements.Checked)
                {
                    sw.Start();

                    newTableDefs.SetToBOF();
                    int numTabsCreated = destDb.CreateTablesFromTableDefs(newTableDefs, true);

                    sw.Stop();

                    _msg.Length = 0;
                    _msg.Append("\r\nNumber of tables created: \r\n");
                    _msg.Append(numTabsCreated.ToString());
                    _msg.Append("\r\n");
                    _msg.Append("Elapsed time: ");
                    _msg.Append(sw.FormattedElapsedTime);
                    Program._messageLog.WriteLine(_msg.ToString());


                    sw.Stop();


                    if (frm.chkImportDataFromSourceToDestination.Checked)
                    {
                        sw.Start();

                        PFList<TableCopyDetails> tableCopyLog = destDb.CopyTableDataFromTableDefs(sourceDb, includes, null, frm.txtNewSchema.Text.Trim(), true);

                        sw.Stop();

                        tableCopyLog.SetToBOF();
                        TableCopyDetails tcdetails = null;

                        while ((tcdetails = tableCopyLog.NextItem) != null)
                        {
                            _msg.Length = 0;
                            _msg.Append("Table: ");
                            _msg.Append(tcdetails.destinationTableName);
                            _msg.Append(", NumRowsCopied: ");
                            _msg.Append(tcdetails.numRowsCopied.ToString("#,##0"));
                            if (tcdetails.result != TableCopyResult.Success)
                            {
                                _msg.Append("\r\n    ");
                                _msg.Append("Result: ");
                                _msg.Append(tcdetails.result.ToString());
                                _msg.Append("  Messages: ");
                                _msg.Append(tcdetails.messages);
                            }
                            Program._messageLog.WriteLine(_msg.ToString());
                        }
                        _msg.Length = 0;
                        _msg.Append("Elapsed time: ");
                        _msg.Append(sw.FormattedElapsedTime);
                        Program._messageLog.WriteLine(_msg.ToString());


                    }//end import data routine


                }//end table create routine

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
                if(sourceDb != null)
                    if (sourceDb.IsConnected)
                        sourceDb.CloseConnection();
                if(destDb != null)
                    if (destDb.IsConnected)
                        destDb.CloseConnection();
                sourceDb = null;
                destDb = null;
                _msg.Length = 0;
                _msg.Append("\r\n... ConvertTableDefs finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void GetSupportedDatabasesList(MainForm frm)
        {
            PFList<string> dblist = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("GetSupportedDatabasesList started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                dblist = PFDatabase.GetListOfSupportedDatabases();

                string tab = null;

                dblist.SetToBOF();

                while ((tab = dblist.NextItem) != null)
                {
                    if (tab.ToUpper() != "UNKNOWN")
                    {
                        _msg.Length = 0;
                        _msg.Append(tab);
                        Program._messageLog.WriteLine(_msg.ToString());
                    }
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
                _msg.Append("\r\n... GetSupportedDatabasesList finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        

    }//end class
}//end namespace
