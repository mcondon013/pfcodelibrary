using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AppGlobals;
using PFFileSystemObjects;
//using PFSQLBuilderObjects;
using PFDataAccessObjects;
using PFSQLAnywhereULObjects;
using System.Configuration;

namespace TestprogSQLAnywhereUL
{
    public partial class MainForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        bool _outputStackTraceWithErrorMessages = false;
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";

        //private fields for processing file and folder dialogs
        private OpenFileDialog _openFileDialog = new OpenFileDialog();
        private SaveFileDialog _saveFileDialog = new SaveFileDialog();
        private FolderBrowserDialog _folderBrowserDialog = new FolderBrowserDialog();
        private string _saveSelectionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string _saveSelectionsFile = string.Empty;
        private string[] _saveSelectedFiles = null;
        private bool _saveMultiSelect = true;
        private string _saveFilter = "UltraLite Database Files|*.udb|DataFiles|*txt;*.dat|All Files|*.*";
        private int _saveFilterIndex = 1;
        private bool _showCreatePrompt = true;
        private bool _showOverwritePrompt = true;
        private bool _showNewFolderButton = true;

        public MainForm()
        {
            InitializeComponent();
        }

        //button click events
        private void cmdExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void cmdRunTests_Click(object sender, EventArgs e)
        {
            RunTests();
        }

        private void cmdCreateDb_Click(object sender, EventArgs e)
        {
            CreateDatabaseTest();
        }

        private void cmdGetDatabaseFileName_Click(object sender, EventArgs e)
        {
            GetDatabaseFileName();
        }

        private void cmdBuildQuery_Click(object sender, EventArgs e)
        {
            RunQueryBuilder();
        }

        private void cmdShowHideOutputLog_Click(object sender, EventArgs e)
        {
            if (Program._messageLog.FormIsVisible)
                Program._messageLog.HideWindow();
            else
                Program._messageLog.ShowWindow();
        }

        //Menu item clicks
        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }


        //Form Routines
        private void CloseForm()
        {
            this.Close();
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

                this.chkEraseOutputBeforeEachTest.Checked = true;
                this.chkShowStackTraceOnError.Checked = false;
                this.txtDatabaseFile.Text = @"C:\Testdata\SQLAnywhere\Test1.udb";
                _saveSelectionsFolder = Path.GetDirectoryName(this.txtDatabaseFile.Text);
                this.txtDatabaseName.Text = string.Empty; //databasename not used for UltraLite: file name is sufficient
                this.txtUsername.Text = "DBA";
                this.txtPassword.Text = "sql";

                this.txtTableName.Text = "TestTable01";

                _str.Length = 0;
                _str.Append("Select K1, F1, F2");
                _str.Append("  from TestTab01");
                _str.Append("");

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
            DataTable dt = keyValsDataSet.Tables["KeyValTable"];
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
            ComboBox cbo = null;

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
                if (ctl is ComboBox)
                {
                    cbo = (ComboBox)ctl;
                    cbo.Enabled = true;
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
            ComboBox cbo = null;

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
                if (ctl is ComboBox)
                {
                    cbo = (ComboBox)ctl;
                    cbo.Enabled = false;
                }

            }//end foreach control

        }//end method

        //routines for processing file open, file save and folder browser dialogs
        private void GetDatabaseFileName()
        {
            DialogResult res = ShowOpenFileDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
                this.txtDatabaseFile.Text = _saveSelectionsFile;
        }

        private DialogResult ShowOpenFileDialog()
        {
            DialogResult res = DialogResult.None;
            _openFileDialog.InitialDirectory = _saveSelectionsFolder;
            _openFileDialog.FileName = _saveSelectionsFile;
            _openFileDialog.Filter = _saveFilter;
            _openFileDialog.FilterIndex = _saveFilterIndex;
            _openFileDialog.Multiselect = _saveMultiSelect;
            _saveSelectionsFile = string.Empty;
            _saveSelectedFiles = null;
            res = _openFileDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                _saveSelectionsFolder = Path.GetDirectoryName(_openFileDialog.FileName);
                _saveSelectionsFile = _openFileDialog.FileName;
                _saveFilterIndex = _openFileDialog.FilterIndex;
                if (_openFileDialog.Multiselect)
                {
                    _saveSelectedFiles = _openFileDialog.FileNames;
                }
            }
            return res;
        }

        private DialogResult ShowSaveFileDialog()
        {
            DialogResult res = DialogResult.None;
            _saveFileDialog.InitialDirectory = _saveSelectionsFolder;
            _saveFileDialog.FileName = _saveSelectionsFile;
            _saveFileDialog.Filter = _saveFilter;
            _saveFileDialog.FilterIndex = _saveFilterIndex;
            _saveFileDialog.CreatePrompt = _showCreatePrompt;
            _saveFileDialog.OverwritePrompt = _showOverwritePrompt;
            res = _saveFileDialog.ShowDialog();
            _saveSelectionsFile = string.Empty;
            if (res == DialogResult.OK)
            {
                _saveSelectionsFolder = Path.GetDirectoryName(_saveFileDialog.FileName);
                _saveSelectionsFile = _saveFileDialog.FileName;
                _saveFilterIndex = _saveFileDialog.FilterIndex;
            }
            return res;
        }

        private DialogResult ShowFolderBrowserDialog()
        {
            DialogResult res = DialogResult.None;

            string folderPath = string.Empty;

            if (_saveSelectionsFolder.Length > 0)
                folderPath = _saveSelectionsFolder;
            else
                folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _folderBrowserDialog.ShowNewFolderButton = _showNewFolderButton;
            //_folderBrowserDialog.RootFolder = 
            _folderBrowserDialog.SelectedPath = folderPath;
            res = _folderBrowserDialog.ShowDialog();
            if (res != DialogResult.Cancel)
            {
                folderPath = _folderBrowserDialog.SelectedPath;
                _str.Length = 0;
                _str.Append(folderPath);
                if (folderPath.EndsWith(@"\") == false)
                    _str.Append(@"\");
                _saveSelectionsFolder = folderPath;
            }


            return res;
        }

        //application routines
        private void RunTests()
        {

            int numberOfTests = 0;
            _outputStackTraceWithErrorMessages = this.chkShowStackTraceOnError.Checked;
            Tests.MainForm = this;

            try
            {
                DisableFormControls();
                Tests.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;

                if (this.chkEraseOutputBeforeEachTest.Checked)
                {
                    Program._messageLog.Clear();
                }

                if (this.chkShowDateTimeTest.Checked)
                {
                    numberOfTests++;
                    Tests.RunShowDateTimeTest();
                }

                if (this.chkGetStaticKeysTest.Checked)
                {
                    numberOfTests++;
                    OutputTestRunningMessageToLog("GetStaticKeysTest");
                    Tests.GetStaticKeys();
                    OutputTestFinishedMessageToLog("GetStaticKeysTest");
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
                if (this.chkGetSchemaTest.Checked)
                {
                    numberOfTests++;
                    Tests.GetSchemaTest();
                }
                if (this.chkImportDataTable.Checked)
                {
                    numberOfTests++;
                    Tests.ImportDataTableTest();
                }


                if (numberOfTests == 0)
                {
                    AppMessages.DisplayInfoMessage("No tests selected ...", false);
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
                EnableFormControls();
                _msg.Length = 0;
                _msg.Append("\r\n");
                _msg.Append("Number of tests run: ");
                _msg.Append(numberOfTests.ToString("#,##0"));
                Program._messageLog.WriteLine(_msg.ToString());
            }



        }

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


        //Query Builder
        public void RunQueryBuilder()
        {
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

            string modifiedQueryText = this.txtSQLQuery.Text;


            // ******************************************************************************
            // Code to activate Query Builder follows
            // ******************************************************************************
            //uncomment next four lines to activate query builder
            //uncomment using PFSQLBuilderObjects; above
            //add reference to PFSQLBuilderObjects.dll in CPLibs\Binaries\ProFast\ClassLibraries\Release (this is default path; substitute path if you changed binaries path
            /*
                        //modifiedQueryText = PFQueryBuilder.RunQueryBuilderTest(this.txtSQLQuery.Text);

                        PFQueryBuilder qbf = new PFQueryBuilder();
                        qbf.ConnectionString = GetConnectionString();
                        qbf.DatabasePlatform = QueryBuilderDatabasePlatform.SQLAnywhereUL;

                        modifiedQueryText = qbf.ShowQueryBuilder(this.txtSQLQuery.Text);
                        */
            // ******************************************************************************
            // End code to activate Query Builder
            // ******************************************************************************

            this.txtSQLQuery.Text = modifiedQueryText;
        }

        private string GetConnectionString()
        {
            PFSQLAnywhereUL db = new PFSQLAnywhereUL();
            string connectionString = string.Empty;

            try
            {
                db.DatabasePath = this.txtDatabaseFile.Text;
                db.DatabaseName = this.txtDatabaseName.Text;
                db.Username = this.txtUsername.Text;
                db.Password = this.txtPassword.Text;

                db.DatabaseKey = this.txtDatabaseKey.Text;

                connectionString = db.ConnectionString;


            }
            catch (System.Exception ex)
            {
                this.OutputErrorMessageToLog(ex);
            }
            finally
            {
                if (db != null)
                    db = null;
            }

            return connectionString;

        }

        private void CreateDatabaseTest()
        {
            Tests.CreateDatabaseTest(this);
        }



    }//end class
}//end namespace
