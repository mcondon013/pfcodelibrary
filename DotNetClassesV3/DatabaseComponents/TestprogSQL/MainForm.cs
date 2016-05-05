using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppGlobals;
using PFFileSystemObjects;
using PFSQLBuilderObjects;
using PFDataAccessObjects;
using System.Configuration;

namespace TestprogSQL
{
    public partial class MainForm : Form
    {
        StringBuilder _msg = new StringBuilder();
        StringBuilder _str = new StringBuilder();
        bool _outputStackTraceWithErrorMessages = false;
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        private string _helpFilePath = string.Empty;

        public MainForm()
        {
            InitializeComponent();
        }

        //button click events
        private void cmdExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void cmdRunTest_Click(object sender, EventArgs e)
        {
            RunTests();
        }

        //Mneu item clicks
        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void mnuToolsOptions_Click(object sender, EventArgs e)
        {
            ShowToolsOptions();
        }

        private void mnuHelpContents_Click(object sender, EventArgs e)
        {
            ShowHelpContents();
        }

        private void mnuHelpIndex_Click(object sender, EventArgs e)
        {
            ShowHelpIndex();
        }

        private void mnuHelpSearch_Click(object sender, EventArgs e)
        {
            ShowHelpSearch();
        }

        private void mnuHelpTutorial_Click(object sender, EventArgs e)
        {
            ShowHelpTutorial();
        }

        private void mnuHelpContact_Click(object sender, EventArgs e)
        {
            ShowHelpContact();
        }

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            ShowHelpAbout();
        }

        private void cmdQuickTest_Click(object sender, EventArgs e)
        {
            RunQuickTests();
        }

        private void cmdBuildQuery_Click(object sender, EventArgs e)
        {
            RunQueryBuilder();
        }
          


        //Form Routines
        private void CloseForm()
        {
            this.Close();
        }

        private void ShowToolsOptions()
        {
            ApplicationOptionsForm appOptionsForm = new ApplicationOptionsForm();

            try
            {
                appOptionsForm.ShowDialog();
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
                appOptionsForm.Close();
                appOptionsForm = null;
            }

        }

        private void ShowHelpContents()
        {
            Help.ShowHelp(this, _helpFilePath, HelpNavigator.TableOfContents);
        }

        private void ShowHelpIndex()
        {
            Help.ShowHelp(this, _helpFilePath, HelpNavigator.Index);
        }

        private void ShowHelpSearch()
        {
            Help.ShowHelp(this, _helpFilePath, HelpNavigator.Find, "");
        }

        private void ShowHelpTutorial()
        {
            Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Tutorial");
        }

        private void ShowHelpContact()
        {
            Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Contact Information");
        }

        private void ShowHelpAbout()
        {
            HelpAboutForm appHelpAboutForm = new HelpAboutForm();
            appHelpAboutForm.ShowDialog();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string configValue = string.Empty;

            try
            {
                this.Text = AppInfo.AssemblyProduct;

                configValue = AppGlobals.AppConfig.GetConfigValue("SaveErrorMessagesToErrorLog");
                if (configValue.ToUpper() == "TRUE")
                    _saveErrorMessagesToAppLog = true;
                else
                    _saveErrorMessagesToAppLog = false;
                _appLogFileName = AppGlobals.AppConfig.GetConfigValue("AppLogFileName");

                if (_appLogFileName.Trim().Length > 0)
                    AppGlobals.AppMessages.AppLogFilename = _appLogFileName;

                string executableFolder = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;
                string helpFileName = AppConfig.GetStringValueFromConfigFile("HelpFileName", "InitWinFormsHelpFile.chm");
                string helpFilePath = PFFile.FormatFilePath(executableFolder, helpFileName);
                this.appHelpProvider.HelpNamespace = helpFilePath;
                _helpFilePath = helpFilePath;

                this.chkEraseOutputBeforeEachTest.Checked = true;

                this.chkShowStackTraceOnError.Checked = false;
                this.chkUseIntegratedSecurity.Checked = true;
                this.chkUseAsyncProcessing.Checked = false;
                this.chkIsStoredProcedure.Checked = false;
                this.txtServerName.Text = "PROFASTSV2";
                this.txtDatabaseName.Text = "AdventureWorksDW2008R2";
                //this.txtDatabaseName.Text = "Miketest";
                this.txtApplicationName.Text = "TestprogSQL";
                this.txtWorkstationId.Text = Environment.MachineName;
                this.txtUsername.Text = string.Empty;
                this.txtPassword.Text = string.Empty;

                this.txtTableName.Text = "TestTable01";
                this.txtSchemaName.Text = "dbo";

                //_str.Length = 0;
                //_str.Append("select top 25");
                //_str.Append("\r\n");
                //_str.Append("  cGroup, cNumLow, cNumHigh, cCount, cName");
                //_str.Append("\r\n");
                //_str.Append(" from Namelists.dbo.tblUS_LastNames");
                //this.txtSQLQuery.Text = _str.ToString();

                _str.Length = 0;
                _str.Append("select LocKey=g.GeographyKey\r\n");
                _str.Append("      ,g.City\r\n");
                _str.Append("      ,[State]=g.StateProvinceName\r\n");
                _str.Append("      ,Country=g.EnglishCountryRegionName\r\n");
                _str.Append("      ,g.PostalCode\r\n");
                _str.Append("      ,st.SalesTerritoryRegion\r\n");
                _str.Append("      ,st.SalesTerritoryCountry\r\n");
                _str.Append("      ,st.SalesTerritoryGroup\r\n");
                _str.Append("  from dbo.DimGeography g\r\n");
                _str.Append("       join dbo.DimSalesTerritory st\r\n");
                _str.Append("         on g.SalesTerritoryKey = st.SalesTerritoryKey\r\n");
                _str.Append(";"); 
                this.txtSQLQuery.Text = _str.ToString();

                LoadAppConfigItems();

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }

        }

        /// <summary>
        /// keyValDataSet used for testing the import of a data table.
        /// </summary>
        private void LoadAppConfigItems()
        {
            KeyValueConfigurationCollection appKeyVals = AppConfig.GetAllAppSettings();
            DataTable dt = keyValsDataSet.Tables["dbo.KeyValTable"];
            dt.Clear();

            foreach (KeyValueConfigurationElement ce in appKeyVals)
            {
                DataRow dr = dt.NewRow();
                dr["AppSetting"] = ce.Key;
                dr["SettingValue"] = ce.Value;
                dt.Rows.Add(dr);
            }

            dt.AcceptChanges();

        }



        private void EnableFormControls()
        {
            TextBox txt = null;
            CheckBox chk = null;
            Button btn = null;
            MenuStrip mnu = null;
            GroupBox grp = null;
            Panel pnl = null;
            ListBox lst = null;

            foreach (Control ctl in this.Controls)
            {
                if (ctl is MenuStrip)
                {
                    mnu = (MenuStrip)ctl;
                    foreach (ToolStripItem itm in mnu.Items)
                    {
                        itm.Enabled = true;
                    }
                }
                if (ctl is TextBox)
                {
                    txt = (TextBox)ctl;
                    ctl.Enabled = true;
                }
                if (ctl is CheckBox)
                {
                    chk = (CheckBox)ctl;
                    chk.Enabled = true;
                }
                if (ctl is Button)
                {
                    btn = (Button)ctl;
                    btn.Enabled = true;
                }
                if (ctl is GroupBox)
                {
                    grp = (GroupBox)ctl;
                    grp.Enabled = true;
                }
                if (ctl is Panel)
                {
                    pnl = (Panel)ctl;
                    pnl.Enabled = true;
                }
                if (ctl is ListBox)
                {
                    lst = (ListBox)ctl;
                    lst.Enabled = true;
                }

            }//end foreach
        }//end method

        private void DisableFormControls()
        {
            TextBox txt = null;
            CheckBox chk = null;
            Button btn = null;
            MenuStrip mnu = null;
            GroupBox grp = null;
            Panel pnl = null;
            ListBox lst = null;

            foreach (Control ctl in this.Controls)
            {
                if (ctl is MenuStrip)
                {
                    mnu = (MenuStrip)ctl;
                    foreach (ToolStripItem itm in mnu.Items)
                    {
                        itm.Enabled = false;
                    }
                }
                if (ctl is TextBox)
                {
                    txt = (TextBox)ctl;
                    ctl.Enabled = false;
                }
                if (ctl is CheckBox)
                {
                    chk = (CheckBox)ctl;
                    chk.Enabled = false;
                }
                if (ctl is Button)
                {
                    btn = (Button)ctl;
                    btn.Enabled = false;
                }
                if (ctl is GroupBox)
                {
                    grp = (GroupBox)ctl;
                    grp.Enabled = false;
                }
                if (ctl is Panel)
                {
                    pnl = (Panel)ctl;
                    pnl.Enabled = false;
                }
                if (ctl is ListBox)
                {
                    lst = (ListBox)ctl;
                    lst.Enabled = false;
                }

            }//end foreach control

        }//end method

        //application routines
        private void RunTests()
        {
            int numberOfTests = 0;
            _outputStackTraceWithErrorMessages = this.chkShowStackTraceOnError.Checked;
            Tests.MainForm = this;


            try
            {
                DisableFormControls();
                this.Cursor = Cursors.WaitCursor;

                if (this.chkEraseOutputBeforeEachTest.Checked)
                {
                    Program._messageLog.Clear();
                }

                if (this.chkMessageLogTest.Checked)
                {
                    numberOfTests++;
                    OutputTestRunningMessageToLog("MessageLogTest");
                    Tests.MessageLogTest(numberOfTests);
                    OutputTestFinishedMessageToLog("MessageLogTest");
                }
                if (this.chkConnectionStringTest.Checked)
                {
                    numberOfTests++;
                    OutputTestRunningMessageToLog("ConnectionStringTest");
                    Tests.ConnectionStringTest();
                    OutputTestFinishedMessageToLog("ConnectionStringTest");
                }
                if (this.chkCreateTableTest.Checked)
                {
                    numberOfTests++;
                    OutputTestRunningMessageToLog("CreateTableTest");
                    Tests.CreateTableTest();
                    OutputTestFinishedMessageToLog("CreateTableTest");
                }
                if (this.chkDataReaderTest.Checked)
                {
                    numberOfTests++;
                    OutputTestRunningMessageToLog("DataReaderTest");
                    Tests.DataReaderTest();
                    OutputTestFinishedMessageToLog("DataReaderTest");
                }
                if (this.chkDataReaderToDataTableTest.Checked)
                {
                    numberOfTests++;
                    OutputTestRunningMessageToLog("DataReaderToDataTableTest");
                    Tests.DataReaderToDataTableTest();
                    OutputTestFinishedMessageToLog("DataReaderToDataTableTest");
                }
                if (this.chkDataTableTest.Checked)
                {
                    numberOfTests++;
                    OutputTestRunningMessageToLog("DataTableTest");
                    Tests.DataTableTest();
                    OutputTestFinishedMessageToLog("DataTableTest");
                }
                if (this.chkDataSetTest.Checked)
                {
                    numberOfTests++;
                    OutputTestRunningMessageToLog("DataSetTest");
                    Tests.DataSetTest();
                    OutputTestFinishedMessageToLog("DataSetTest");
                }
                if (this.chkImportDataTable.Checked)
                {
                    numberOfTests++;
                    Tests.ImportDataTableTest();
                }
                if (this.chkGetQueryDataSchema.Checked)
                {
                    numberOfTests++;
                    Tests.GetQueryDataSchema();
                }
            }
            catch (System.Exception ex)
            {
                OutputErrorMessageToLog(ex);
            }
            finally
            {
                TestSummaryReport(numberOfTests);
                this.Cursor = Cursors.Default;
                EnableFormControls();
            }

        }//end RunTests

        public void OutputInformationMessageToLog(string message)
        {
            _msg.Length = 0;
            _msg.Append(message);
            Program._messageLog.WriteLine(_msg.ToString());
        }

        public void OutputTestRunningMessageToLog(string test)
        {
            _msg.Length = 0;
            _msg.Append(test);
            _msg.Append(" is running ...");
            Program._messageLog.WriteLine(_msg.ToString());
        }

        public void OutputTestFinishedMessageToLog(string test)
        {
            _msg.Length = 0;
            _msg.Append(test);
            _msg.Append(" is finished!");
            Program._messageLog.WriteLine(_msg.ToString());
        }

        public void OutputErrorMessageToLog(System.Exception ex)
        {
            _msg.Length = 0;
            _msg.Append("***** ERROR *****");
            _msg.Append("\r\n");
            if (_outputStackTraceWithErrorMessages)
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessageWithStackTrace(ex));
            else
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
            Program._messageLog.WriteLine(_msg.ToString());
        }

        public void TestSummaryReport(int numberOfTests)
        {
            _msg.Length = 0;
            if (numberOfTests > 0)
            {
                _msg.Append("+++++ Tests Complete +++++");
                _msg.Append("\r\n");
                _msg.Append("Number of tests completed: ");
                _msg.Append(numberOfTests.ToString("#,##0"));
                _msg.Append("\r\n");
            }
            else
            {
                _msg.Append("+++++ No Tests Selected +++++");
                _msg.Append("\r\n");
            }
            Program._messageLog.WriteLine(_msg.ToString());
        }

        public void RunQuickTests()
        {
            _outputStackTraceWithErrorMessages = this.chkShowStackTraceOnError.Checked;
            try
            {
                DisableFormControls();
                this.Cursor = Cursors.WaitCursor;

                if (this.chkEraseOutputBeforeEachTest.Checked)
                {
                    Program._messageLog.Clear();
                }

                //Tests.RunQuickTest2();
                //Tests.RunQuickTest1();
                Tests.MainForm = this;
                Tests.RunQuickTest3();

            }
            catch (System.Exception ex)
            {
                OutputErrorMessageToLog(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                EnableFormControls();
            }
        }

        //Query Builder
        public void RunQueryBuilder()
        {
            string modifiedQueryText = this.txtSQLQuery.Text;

            // *** TEMPORARY CODE FOR QUERY BUILDER ***
            _msg.Length = 0;
            _msg.Append("*** Query builder functionality has been disabled. ***");
            _msg.Append(Environment.NewLine);
            _msg.Append(Environment.NewLine);
            _msg.Append("You must have installed Active Query Builder for Winforms for this functionality to work.\r\nProduct can be obtained from the following link:");
            _msg.Append(Environment.NewLine);
            _msg.Append(@"http://www.activequerybuilder.com/product_net.html");
            _msg.Append(Environment.NewLine);
            _msg.Append(Environment.NewLine);
            _msg.Append("If you have already installed Active Query Builder for Winforms, you should remove the temporary code found in the RunQueryBuilder() method.");
            _msg.Append(Environment.NewLine);
            _msg.Append(Environment.NewLine);
            AppMessages.DisplayAlertMessage(_msg.ToString());
            return;
            // *** END TEMPORARY CODE FOR QUERY BUILDER ***

            //modifiedQueryText = PFQueryBuilder.RunQueryBuilderTest(this.txtSQLQuery.Text);

            PFQueryBuilder qbf = new PFQueryBuilder();
            qbf.ConnectionString = GetConnectionString();
            qbf.DatabasePlatform = QueryBuilderDatabasePlatform.MSSQLServer;

            modifiedQueryText = qbf.ShowQueryBuilder(this.txtSQLQuery.Text);

            this.txtSQLQuery.Text = modifiedQueryText;
        }

        private string GetConnectionString()
        {
            PFSQLServer sqlserv = new PFSQLServer();
            string connectionString = string.Empty;

            try
            {
                sqlserv.ServerName = this.txtServerName.Text;
                sqlserv.DatabaseName = this.txtDatabaseName.Text;
                sqlserv.UseIntegratedSecurity = this.chkUseIntegratedSecurity.Checked;
                sqlserv.ApplicationName = this.txtApplicationName.Text;
                sqlserv.WorkstationId = this.txtWorkstationId.Text;
                sqlserv.Username = this.txtUsername.Text;
                sqlserv.Password = this.txtPassword.Text;

                connectionString = sqlserv.ConnectionString;


            }
            catch (System.Exception ex)
            {
                this.OutputErrorMessageToLog(ex);
            }
            finally
            {
                if(sqlserv != null)
                    sqlserv = null;
            }

            return connectionString;

        }

    }//end class
}//end namespace
