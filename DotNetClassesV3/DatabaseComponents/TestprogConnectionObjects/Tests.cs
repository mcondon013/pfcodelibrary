using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Configuration;
using PFListObjects;
using PFDataAccessObjects;
using PFConnectionObjects;
using PFConnectionStrings;
using PFAppUtils;
using PFDatabaseSelector;
using System.Windows.Forms;

namespace TestprogConnectionObjects
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
        public static void CreateProviderObjects(MainForm frm)
        {
            PFConnectionManager connMgr = new PFConnectionManager();

            try
            {
                _msg.Length = 0;
                _msg.Append("CreateProviderObjects started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                connMgr.CreateProviderDefinitions();

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
                _msg.Append("\r\n... CreateProviderObjects finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void CreateConnectionObjects(MainForm frm)
        {
            PFConnectionManager connMgr = new PFConnectionManager();
            
            try
            {
                _msg.Length = 0;
                _msg.Append("CreateConnectionObjects started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                connMgr.CreateTestConnectionDefinitions();

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
                _msg.Append("\r\n... CreateConnectionObjects finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void UpdateProviderObjects(MainForm frm)
        {
            PFConnectionManager connMgr = new PFConnectionManager();

            try
            {
                _msg.Length = 0;
                _msg.Append("UpdateProviderObjects started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                //this will get the installation status
                connMgr.UpdateAllProvidersInstallationStatus();

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
                _msg.Append("\r\n... UpdateProviderObjects finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void ShowConnectionPromptForm(MainForm frm)
        {
            PFConnectionManager connMgr = null;
            DatabasePlatform dbPlat = DatabasePlatform.MSSQLServer;
            bool runTests = false;

            try
            {
                _msg.Length = 0;
                _msg.Append("ShowConnectionPromptForm started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                connMgr = new PFConnectionManager();

                ConnectionStringPrompt cp = new ConnectionStringPrompt(dbPlat, connMgr);
                System.Windows.Forms.DialogResult res = cp.ShowConnectionPrompt();
                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    runTests = true;
                    _msg.Length = 0;
                    _msg.Append("Connection name:   ");
                    _msg.Append(cp.ConnectionName);
                    _msg.Append(Environment.NewLine);
                    _msg.Append("Connection string: ");
                    _msg.Append(cp.ConnectionString);
                    _msg.Append(Environment.NewLine);
                    _msg.Append("Connection Definition: ");
                    _msg.Append(Environment.NewLine);
                    if (cp.ConnectionDefinition != null)
                    {
                        _msg.Append(cp.ConnectionDefinition.ToString());
                        _msg.Append(Environment.NewLine);
                    }
                    Program._messageLog.WriteLine(_msg.ToString());
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("User cancelled connection string request: ");
                    _msg.Append(res.ToString());
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                if (runTests)
                {
                    //cp.ConnectionDefinition.SaveToXmlFile(@"c:\temp\TestConnectionDefinition.xml");

                    //PFConnectionDefinition newCP = PFConnectionDefinition.LoadFromXmlFile(@"c:\temp\TestConnectionDefinition.xml");

                    //cp.ConnectionDefinition.ConnectionKeyElements.SaveToXmlFile(@"c:\temp\ConnectionKeyElements.xml");
                    //PFList<stKeyValuePair<string, string>> elementsList = cp.ConnectionDefinition.ConnectionKeyElements.ConvertThisToPFKeyValueListEx();
                    //elementsList.SaveToXmlFile(@"c:\temp\ConnectionKeyElements_PFList.xml");

                    //Program._messageLog.WriteLine(Environment.NewLine + "newCP TO STRING:" + Environment.NewLine + newCP.ToString() + Environment.NewLine + "END newCP TO STRING" + Environment.NewLine);

                    //PFKeyValueList<string, string> testlist = PFKeyValueList<string, string>.LoadFromXmlString(cp.ConnectionDefinition.DbPlatformConnectionStringProperties[15].Value);
                    //Program._messageLog.WriteLine(testlist.ToXmlString());

                    //cp.ConnectionDefinition.DbPlatformConnectionStringProperties.SaveToXmlFile(@"c:\temp\DbPlatformConnectionStringProperties.xml");

                    //Program._messageLog.WriteLine(Environment.NewLine + "-----------");
                    //string dumpOutput = PFObjectDumper.Write(cp, 0);
                    //Program._messageLog.WriteLine(dumpOutput);
                    //Program._messageLog.WriteLine(Environment.NewLine + "-----------");

                    //cp = null;
                    //newCP = null;
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
                _msg.Append("\r\n... ShowConnectionPromptForm finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void ShowDatabaseSelector(MainForm frm)
        {
            DatabaseSelectorForm dbSelectorForm = new DatabaseSelectorForm();
            try
            {
                _msg.Length = 0;
                _msg.Append("ShowDatabaseSelector started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                DialogResult res = dbSelectorForm.ShowDialog();

                _msg.Length = 0;
                _msg.Append("DialogResult is ");
                _msg.Append(res.ToString());
                Program._messageLog.WriteLine(_msg.ToString());

                if (res == DialogResult.OK)
                {

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
                if (dbSelectorForm.Visible)
                    dbSelectorForm.Hide();
                dbSelectorForm = null;

                _msg.Length = 0;
                _msg.Append("\r\n... ShowDatabaseSelector finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void ShowConnectionStringDefinitionsForm(MainForm frm)
        {
            DialogResult res = DialogResult.None;

            try
            {
                _msg.Length = 0;
                _msg.Append("ShowConnectionStringDefinitionsForm started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                PFConnectionStringManagerForm cnf = new PFConnectionStringManagerForm();
                res = cnf.ShowDialog();

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
                _msg.Append("\r\n... ShowConnectionStringDefinitionsForm finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
            
      

    }//end class
}//end namespace
