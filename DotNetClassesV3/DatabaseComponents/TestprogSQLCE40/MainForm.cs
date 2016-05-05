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
//using PFSQLBuilderObjects;
using PFSQLServerCE40Objects;
using System.IO;
using System.Configuration;
using PFDataAccessObjects;

namespace TestprogSQLCE40
{
    public partial class MainForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private StringBuilder _path = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";

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

        private void cmdShowHideOutputLog_Click(object sender, EventArgs e)
        {
            if (Program._messageLog.FormIsVisible)
                Program._messageLog.HideWindow();
            else
                Program._messageLog.ShowWindow();
        }

        //Mneu item clicks
        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void cmdSetDataSourceFolder_Click(object sender, EventArgs e)
        {
            SetDataSourceFolder();
        }

        private void cmdSetDataSourceFilePath_Click(object sender, EventArgs e)
        {
            SetDatabaseFilePath();
        }

        private void txtQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void txtQuery_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = null;
        }

        private void txtQuery_Leave(object sender, EventArgs e)
        {
            this.AcceptButton = this.cmdRunTests;
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
                this.txtDataSource.Text = AppConfig.GetStringValueFromConfigFile("DefaultDataSourceFolderPath", @"c:\temp\");
                this.txtPassword.Text = string.Empty;
                this.chkEncryptionOn.Checked = false;
                this.cboEncryptionMode.Text = (string)this.cboEncryptionMode.Items[0];

                this.txtQuery.Text = "select FirstName, min(RowId) as MinRow, count(*) as numRows\r\n from USFirstNamesTest\r\n group by FirstName\r\n order by FirstName;";

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
        private void SetDataSourceFolder()
        {
            try
            {
                string folderPath = string.Empty;
                string temp = string.Empty;
                DialogResult res;

                if (this.txtDataSource.Text.Trim().Length > 0)
                {
                    temp = Path.GetDirectoryName(this.txtDataSource.Text.Trim());
                    if (temp.Length > 0)
                        folderPath = temp;
                    else
                        folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }
                else
                    folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                this.mainMenuFolderBrowserDialog.SelectedPath = folderPath;
                res = this.mainMenuFolderBrowserDialog.ShowDialog();
                if (res != DialogResult.Cancel)
                {
                    folderPath = this.mainMenuFolderBrowserDialog.SelectedPath;
                    _path.Length = 0;
                    _path.Append(folderPath);
                    if (folderPath.EndsWith(@"\") == false)
                        _path.Append(@"\");
                    this.txtDataSource.Text = _path.ToString();
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
                ;
            }


        }

        private void SetDatabaseFilePath()
        {

            try
            {
                if (this.txtDataSource.Text.Trim().Length == 0)
                    this.mainMenuOpenFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                else
                {
                    this.mainMenuOpenFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(this.txtDataSource.Text.Trim());
                    this.mainMenuOpenFileDialog.FileName = System.IO.Path.GetFileName(this.txtDataSource.Text.Trim());
                }
                DialogResult res = this.mainMenuOpenFileDialog.ShowDialog();
                if (res != DialogResult.Cancel)
                {
                    this.txtDataSource.Text = this.mainMenuOpenFileDialog.FileName;
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
                ;
            }
                 

        }//end method


        private void RunTests()
        {

            int numTestsSelected = 0;

            try
            {
                DisableFormControls();
                Tests.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;

                if (this.chkEraseOutputBeforeEachTest.Checked)
                {
                    Program._messageLog.Clear();
                }

                if (this.chkCreateDatabaseTest.Checked)
                {
                    numTestsSelected++;
                    Tests.CreateDatabaseTest(this);
                }

                if (this.chkImportDataTable.Checked)
                {
                    numTestsSelected++;
                    Tests.ImportDataTableTest(this);
                }

                if (this.chkCreateTable.Checked)
                {
                    numTestsSelected++;
                    Tests.CreateTableTest(this);
                }

                if (this.chkConnectionTest.Checked)
                {
                    numTestsSelected++;
                    Tests.ConnectionTest(this);
                }

                if (this.chkRunQuery.Checked)
                {
                    int saveNumTestsSelected = numTestsSelected;

                    if (this.optNonQuery.Checked)
                    {
                        numTestsSelected++;
                    }
                    if (this.optReader.Checked)
                    {
                        numTestsSelected++;
                    }
                    if (this.optRdrToDt.Checked)
                    {
                        numTestsSelected++;
                    }
                    if (this.optResultset.Checked)
                    {
                        numTestsSelected++;
                    }
                    if (this.optRsToDt.Checked)
                    {
                        numTestsSelected++;
                    }
                    if (this.optDataset.Checked)
                    {
                        numTestsSelected++;
                    }
                    if (this.optDataTable.Checked)
                    {
                        numTestsSelected++;
                    }

                    if (numTestsSelected == saveNumTestsSelected)
                        throw new System.Exception("You must selected type of query to run.");

                    Tests.RunQueryTest(this);
                }

                if (numTestsSelected == 0)
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
                _msg.Append(numTestsSelected.ToString("#,##0"));
                Program._messageLog.WriteLine(_msg.ToString());
            }



        }


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

            string modifiedQueryText = this.txtQuery.Text;

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
            qbf.DatabasePlatform = QueryBuilderDatabasePlatform.SQLServerCE_V4;

            modifiedQueryText = qbf.ShowQueryBuilder(this.txtQuery.Text);
            */
            // ******************************************************************************
            // End code to activate Query Builder
            // ******************************************************************************

            this.txtQuery.Text = modifiedQueryText;
        }

        private string GetConnectionString()
        {
            PFSQLServerCE40 db = new PFSQLServerCE40();
            string connectionString = string.Empty;

            try
            {
                db.DatabasePath = this.txtDataSource.Text;
                db.DatabasePassword = this.txtPassword.Text;
                connectionString = db.ConnectionString;
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
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
