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
using PFInformixObjects;
using PFDB2Objects;
using System.Configuration;

namespace TestprogInformix
{
    public partial class MainForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        bool _outputStackTraceWithErrorMessages = false;

        //private fields for processing file and folder dialogs
        private OpenFileDialog _openFileDialog = new OpenFileDialog();
        private SaveFileDialog _saveFileDialog = new SaveFileDialog();
        private FolderBrowserDialog _folderBrowserDialog = new FolderBrowserDialog();
        private string _saveSelectionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string _saveSelectionsFile = string.Empty;
        private string[] _saveSelectedFiles = null;
        private bool _saveMultiSelect = true;
        private string _saveFilter = "Text Files|*.txt|All Files|*.*";
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

        private void cmdBuildQuery_Click(object sender, EventArgs e)
        {
            RunQueryBuilder();
        }

        private void cmdRunTest_Click(object sender, EventArgs e)
        {
            RunTests();
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
                //this.Text = AppInfo.AssemblyProduct;
                this.Text = StaticKeysSection.Settings.MainFormCaption;

                configValue = AppGlobals.AppConfig.GetConfigValue("SaveErrorMessagesToErrorLog");
                if (configValue.ToUpper() == "TRUE")
                    _saveErrorMessagesToAppLog = true;
                else
                    _saveErrorMessagesToAppLog = false;
                _appLogFileName = AppGlobals.AppConfig.GetConfigValue("AppLogFileName");

                if (_appLogFileName.Trim().Length > 0)
                    AppGlobals.AppMessages.AppLogFilename = _appLogFileName;

                this.chkEraseOutputBeforeEachTest.Checked = true;

                // ********************************************************************************************************************************************************************
                // TROUBLESHOOTING NOTE:
                // OTNET provider for Informix and DB2 was not connecting to informix server on profastsv4imx until entry for profastsv4imx was removed from HOSTS file.
                // With no entry in Hosts file, ping and other access to profastsv4imx is by IP6 protocol instead of IP4. That apparently was what was needed. Do not know wby.
                // ********************************************************************************************************************************************************************

                this.txtServerName.Text = "profastsv4imx";
                this.txtDatabaseName.Text = "miketest";
                this.txtPortNumber.Text = "9089";
                this.txtUsername.Text = "informix";
                this.txtPassword.Text = "IMX1992";
                this.txtConnectionString.Text = string.Empty;



                this.txtSQLQuery.Text = "SELECT customer_num as custno, fname, lname, company, address1, address2, city, state as st, zipcode as zip, phone FROM informix.customer";

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
            DataTable dt = keyValsDataSet.Tables["informix.KeyValTable"];
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

            int nNumTestsSelected = 0;

            try
            {
                DisableFormControls();
                Tests.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;

                if (this.chkEraseOutputBeforeEachTest.Checked)
                {
                    Program._messageLog.Clear();
                }

                if (this.chkGetStaticKeysTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.GetStaticKeys(this);
                }

                if (this.chkConnectionStringTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.ConnectionStringTest(this);
                    //Tests.ConnectionStringTestDB2(this);
                }

                if (this.chkCreateTableTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.CreateTableTest(this);
                }

                if (this.chkDataReaderTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.DataReaderTest(this);
                }

                if (this.chkDataTableTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.DataTableTest(this);
                }

                if (this.chkDataSetTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.DataSetTest(this);
                }

                if (this.chkDataReaderToDataTableTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.DataReaderToDataTableTest(this);
                }

                if (this.chkImportDataTable.Checked)
                {
                    nNumTestsSelected++;
                    Tests.ImportDataTableTest(this);
                }


                if (nNumTestsSelected == 0)
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
                _msg.Append(nNumTestsSelected.ToString("#,##0"));
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

            AppMessages.DisplayAlertMessage("Query Builder does not work with Informix .NET provider. Try ODBC connection to Informix.Z");

            string modifiedQueryText = this.txtSQLQuery.Text;

            // ******************************************************************************
            // Code to activate Query Builder follows
            // ******************************************************************************
            //uncomment next four lines to activate query builder
            //uncomment using PFSQLBuilderObjects; above
            //add reference to PFSQLBuilderObjects.dll in CPLibs\Binaries\ProFast\ClassLibraries\Release (this is default path; substitute path if you changed binaries path          
            /*
            PFQueryBuilder qbf = new PFQueryBuilder();
            qbf.ConnectionString = GetConnectionString();
            qbf.DatabasePlatform = QueryBuilderDatabasePlatform.Informix;
            
            modifiedQueryText = qbf.ShowQueryBuilder(this.txtSQLQuery.Text);
            */
            // ******************************************************************************
            // End code to activate Query Builder
            // ******************************************************************************

            this.txtSQLQuery.Text = modifiedQueryText;
        }

        private string GetConnectionString()
        {
            PFDB2 db = new PFDB2();
            string connectionString = string.Empty;

            try
            {
                db.ServerName = this.txtServerName.Text;
                db.DatabaseName = this.txtDatabaseName.Text;
                db.Username = this.txtUsername.Text;
                db.Password = this.txtPassword.Text;
                db.PortNumber = this.txtPortNumber.Text;
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


    }//end class
}//end namespace
