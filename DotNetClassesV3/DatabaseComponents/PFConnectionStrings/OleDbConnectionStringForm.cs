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
using PFDataAccessObjects;
using PFConnectionObjects;
using PFListObjects;
using PFTextObjects;
using PFAppDataObjects;

namespace PFConnectionStrings
{
    //See TODO comments in code for what must be changed.

    /// <summary>
    /// Class for displaying a form for the input of values for connection string keys.
    /// </summary>
    public partial class OleDbConnectionStringForm : Form, IConnectionStringForm
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private FormPrinter _printer = null;

        private string _helpFilePath = string.Empty;

        private PFOleDb _db = new PFOleDb();        //TODO: Change this (done)

        private string _udlFileFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"PFApps\UDL\");

        private string _saveProviderName = string.Empty;
        private string _saveDataSource = string.Empty;
        private string _saveUdlFileName = string.Empty;
        private bool _savePersistSecurityInfo = false;

        //private variables for properties
        DatabasePlatform _dbPlatform = DatabasePlatform.OLEDB;    //TODO: Changed
        private string _connectionName = string.Empty;
        private string _connectionString = string.Empty;
        private ConnectionStringPrompt _csp = null;
        private enConnectionAccessStatus _connectionAccessStatus = enConnectionAccessStatus.Unknown;

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

        /// <summary>
        /// Constructor.
        /// </summary>
        public OleDbConnectionStringForm()
        {
            InitializeComponent();
        }

        //properties

        /// <summary>
        /// Database platform represented by current instance.
        /// </summary>
        public DatabasePlatform DbPlatform
        {
            get
            {
                _dbPlatform = (DatabasePlatform)Enum.Parse(typeof(DatabasePlatform), this.txtDatabasePlatform.Text);
                return _dbPlatform;
            }
            set
            {
                _dbPlatform = value;
                this.txtDatabasePlatform.Text = _dbPlatform.ToString();
            }
        }

        /// <summary>
        /// Name that will be used to identify the connection.
        /// </summary>
        public string ConnectionName
        {
            get
            {
                _connectionName = this.txtConnectionName.Text;
                return _connectionName;
            }
            set
            {
                _connectionName = value;
                this.txtConnectionName.Text = _connectionName;
            }
        }

        /// <summary>
        /// Connection string that can be used to connect to the database.
        /// </summary>
        public string ConnectionString
        {
            get
            {
                _connectionString = this.txtConnectionString.Text;
                return _connectionString;
            }
            set
            {
                _connectionString = value;
                this.txtConnectionString.Text = _connectionString;
            }
        }

        /// <summary>
        /// ConnectionStringPrompt object that instantiated this instance of the connection string form.
        /// </summary>
        public ConnectionStringPrompt CSP
        {
            get
            {
                return _csp;
            }
            set
            {
                _csp = value;
            }
        }

        /// <summary>
        /// Connection status returned when the connection string was verified: IsAccessible, NotAccessible or Unknown.
        /// </summary>
        /// <remarks>Verification involved attempting to logon using the connection string.</remarks>
        public enConnectionAccessStatus ConnectionAccessStatus
        {
            get
            {
                return _connectionAccessStatus;
            }
            set
            {
                _connectionAccessStatus = value;
            }
        }



#pragma warning disable 1591
        //button click events
        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (UpdateConnectionProperties() == true)
            {
                this.DialogResult = DialogResult.OK;
                HideForm();
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            HideForm();
        }

        private void cmdSaveConnectionDefinition_Click(object sender, EventArgs e)
        {
            SaveConnectionDefinition();
        }

        private void cmdBuildConnectionString_Click(object sender, EventArgs e)
        {
            this.ConnectionString = BuildConnectionString();
        }

        private void cmdVerifyConnectionString_Click(object sender, EventArgs e)
        {
            VerifyConnectionString();
        }

        private void cmdGetUdlFile_Click(object sender, EventArgs e)
        {
            GetUdlFile();
        }


        //menu click events

        private void mnuFileNew_Click(object sender, EventArgs e)
        {
            NewConnectionDefinition();
        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            OpenConnectionDefinition();
        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            SaveConnectionDefinition();
        }

        private void mnuFilePageSetup_Click(object sender, EventArgs e)
        {
            ShowPageSettings();
        }

        private void mnuFilePrint_Click(object sender, EventArgs e)
        {
            FilePrint(false, true);
        }

        private void mnuFilePrintPreview_Click(object sender, EventArgs e)
        {
            FilePrint(true, false);
        }

        private void mnuFileDelete_Click(object sender, EventArgs e)
        {
            DeleteConnectionDefinition();
        }

        private void mnuCancel_Click(object sender, EventArgs e)
        {
            cmdCancel_Click(null, null);
        }


        private void mnuConnectionStringAccept_Click(object sender, EventArgs e)
        {
            cmdOK_Click(null, null);
        }

        private void mnuConnectionStringBuild_Click(object sender, EventArgs e)
        {
            cmdBuildConnectionString_Click(null, null);
        }

        private void mnuConnectionStringVerify_Click(object sender, EventArgs e)
        {
            cmdVerifyConnectionString_Click(null, null);
        }


        //toolbar click events

        private void toolbtnNew_Click(object sender, EventArgs e)
        {
            NewConnectionDefinition();
        }

        private void toolbtnOpen_Click(object sender, EventArgs e)
        {
            OpenConnectionDefinition();
        }

        private void toolbtnSave_Click(object sender, EventArgs e)
        {
            SaveConnectionDefinition();
        }

        private void toolbtnPrint_Click(object sender, EventArgs e)
        {
            FilePrint(false, false);
        }

        private void toolbtnPrintPreview_Click(object sender, EventArgs e)
        {
            FilePrint(true, false);
        }

        private void toolbtnCancel_Click(object sender, EventArgs e)
        {
            cmdCancel_Click(null, null);
        }

        private void toolbarHelp_Click(object sender, EventArgs e)
        {
            ShowHelpFile();
        }


        //form events
        private void WinForm_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }




        //common form processing routines
        public void InitializeForm()
        {
            _printer = new FormPrinter(this);
            EnableFormControls();
            SetHelpFileValues();
            this.txtDatabasePlatform.Text = _dbPlatform.ToString();
            if (this.ConnectionString.Trim().Length > 0)
            {
                InitPropertiesFromConnectionString();
            }
            else
            {
                NewConnectionDefinition();
            }
            SaveFormFields();
        }

        private void SetHelpFileValues()
        {
            string configValue = string.Empty;

            string executableFolder = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;
            string helpFileName = AppConfig.GetStringValueFromConfigFile("ConnStrMgrHelpFileName", "ConnectionStringsManager.chm");
            string helpFilePath = Path.Combine(executableFolder, helpFileName);
            _helpFilePath = helpFilePath;

        }


        private void SaveFormFields()
        {
            _saveProviderName = this.txtProviderName.Text;
            _saveDataSource = this.txtDataSource.Text;
            _saveUdlFileName = this.txtUdlFileName.Text;
            _savePersistSecurityInfo = this.chkPersistSecurityInfo.Checked;
        }

        private bool FormFieldsHaveChanged()
        {
            bool retval = false;

            if (_saveDataSource != this.txtDataSource.Text
                || _saveUdlFileName != this.txtUdlFileName.Text
                || _savePersistSecurityInfo != this.chkPersistSecurityInfo.Checked
                || _saveProviderName != this.txtProviderName.Text)
            {
                retval = true;
            }

            return retval;
        }

        private void InitPropertiesFromConnectionString()
        {
            FillPropertiesFromConnectionString();
        }

        public void HideForm()
        {
            this.Hide();
        }

        public void CloseForm()
        {
            this.Close();
        }

        private void EnableFormControls()
        {
            TextBox txt = null;
            CheckBox chk = null;
            Button btn = null;
            MenuStrip mnu = null;
            GroupBox grp = null;
            Panel pnl = null;

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

            }//end foreach control

        }

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

        //Application routines

        private bool UpdateConnectionProperties()
        {
            bool updateCompleted = true;
            _connectionName = this.txtConnectionName.Text;
            _connectionString = this.txtConnectionString.Text;
            //DO NOT PROMPT TO REBUILD FOR OLEDB, even if either dsn or driver parameter has changed. User must specifically use Build button. In most cases connection string must be manually input for OLEDB: parameters on from are not enough.
            //if (FormFieldsHaveChanged())
            //{
            //    DialogResult res = PromptForConnectionStringRebuild();
            //    if (res == DialogResult.Yes)
            //    {
            //        this.ConnectionString = BuildConnectionString();
            //        _connectionString = this.txtConnectionString.Text;
            //    }
            //    else if (res == DialogResult.Cancel)
            //        updateCompleted = false;
            //    else
            //    {
            //        _connectionString = this.txtConnectionString.Text;
            //    }
            //}

            return updateCompleted;
        }

        private DialogResult PromptForConnectionStringRebuild()
        {
            DialogResult res = AppMessages.DisplayMessage("Connection string and properties on form do not match. Do you want to rebuild the connection string?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            return res;
        }

        private string BuildConnectionString()
        {
            string connStr = string.Empty;

            _db.DataSource = this.txtDataSource.Text;
            _db.Provider = this.txtProviderName.Text;
            _db.UDLFileName = this.txtUdlFileName.Text;
            _db.PersistSecurityInfo = this.chkPersistSecurityInfo.Checked;
            connStr = _db.ConnectionString;

            SaveFormFields();  //save field values so that Accept processing can determine if there are differences between connection string and the property values on the form.

            return connStr;
        }

        private void VerifyConnectionString()
        {
            if (this.txtConnectionString.Text.Trim() == string.Empty)
            {
                this.ConnectionString = BuildConnectionString();
                if (this.txtConnectionString.Text.Trim() == string.Empty)
                {
                    AppMessages.DisplayWarningMessage("You must specify a connection string. Fill in input fields and then use Build button to transform input fields into a connection string.");
                    return;
                }
            }

            try
            {
                DisableFormControls();
                this.Cursor = Cursors.WaitCursor;

                _db.ConnectionString = this.ConnectionString;
                _db.OpenConnection();
                _msg.Length = 0;
                if (_db.IsConnected)
                {
                    this._connectionAccessStatus = enConnectionAccessStatus.IsAccessible;
                    _msg.Append("Connection successful!");
                    AppMessages.DisplayInfoMessage(_msg.ToString());
                }
                else
                {
                    this._connectionAccessStatus = enConnectionAccessStatus.NotAccessible;
                    _msg.Append("Connection failed.");
                    AppMessages.DisplayErrorMessage(_msg.ToString());
                }
            }
            catch (System.Exception ex)
            {
                this._connectionAccessStatus = enConnectionAccessStatus.Unknown;
                _msg.Length = 0;
                _msg.Append("Attempt to connect to database failed.");
                _msg.Append(Environment.NewLine);
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                if (_db.IsConnected)
                    _db.CloseConnection();

                EnableFormControls();
                this.Cursor = Cursors.Default;
                this.Focus();
            }


        }

        private void NewConnectionDefinition()
        {
            TextBox txt = null;
            CheckBox chk = null;

            foreach (Control ctl in this.Controls)
            {
                this.ConnectionName = string.Empty;
                this.ConnectionString = string.Empty;

                if (ctl.Name != "txtDatabasePlatform")
                {
                    if (ctl is TextBox)
                    {
                        txt = (TextBox)ctl;
                        txt.Text = string.Empty;
                    }
                    if (ctl is CheckBox)
                    {
                        chk = (CheckBox)ctl;
                        chk.Checked = false;
                        if (ctl.Name == "chkPersistSecurityInfo")
                            chk.Checked = false;
                        ;
                    }
                }
            }//end foreach
            SaveFormFields();
        }


        private void FillPropertiesFromConnectionString()
        {
            _db.ConnectionString = this.ConnectionString;
            this.txtProviderName.Text = _db.Provider;
            this.txtDataSource.Text = _db.DataSource;
            this.txtUdlFileName.Text = _db.UDLFileName;
            this.chkPersistSecurityInfo.Checked = _db.PersistSecurityInfo;
        }

        private void OpenConnectionDefinition()
        {
            PFConnectionDefinition conndef = _csp.GetConnectionDefinition(_dbPlatform);
            if (conndef != default(PFConnectionDefinition))
            {
                LoadConnectionDefinitionToForm(conndef);
            }
            this.Focus();
        }

        private void LoadConnectionDefinitionToForm(PFConnectionDefinition conndef)
        {

            try
            {
                NewConnectionDefinition();
                this.ConnectionName = conndef.ConnectionName;
                this.ConnectionString = conndef.ConnectionString;
                _db.ConnectionString = conndef.ConnectionString;

                this.txtDataSource.Text = _db.DataSource;
                this.txtUdlFileName.Text = _db.UDLFileName;
                this.chkPersistSecurityInfo.Checked = _db.PersistSecurityInfo;
                this.txtProviderName.Text = _db.Provider;

                SaveFormFields();
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error occurred loading saved connection definition. External XML file format may contain errors. ErrorMessage: \r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                ;
            }

        }

        private void SaveConnectionDefinition()
        {
            string tempConnectionString = string.Empty;

            if (this.ConnectionName.Trim().Length == 0)
            {
                AppMessages.DisplayErrorMessage("You must specify a connection name.");
                return;
            }

            if (this.ConnectionString.Trim().Length == 0)
            {
                this.ConnectionString = BuildConnectionString();
                if (this.ConnectionString.Trim().Length == 0)
                {
                    AppMessages.DisplayErrorMessage("You must specify connection properties and build a connection string.");
                    return;
                }
            }
            //DO NOT PROMPT TO REBUILD FOR OLEDB, even if either dsn or driver parameter has changed. User must specifically use Build button. In most cases connection string must be manually input for OLEDB: parameters on from are not enough.
            //else
            //{
            //    tempConnectionString = BuildConnectionString();
            //    if (this.ConnectionString != tempConnectionString)
            //    {
            //        _msg.Length = 0;
            //        _msg.Append("One or more connection properties have changes since connection string was last built.\r\n");
            //        _msg.Append("Do you want to rebuild the connection string before saving it?");
            //        DialogResult res = AppMessages.DisplayMessage(_msg.ToString(), "Connection string save ...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            //        if (res == DialogResult.Yes)
            //        {
            //            this.ConnectionString = tempConnectionString;
            //        }
            //        else
            //        {
            //            if (res == DialogResult.Cancel)
            //                return;
            //            else
            //            {
            //                //continue with save operation even though connection string has not been updated with latest connection properties
            //                ;
            //            }
            //        }
            //    }
            //}


            PFConnectionDefinition conndef = _csp.BuildConnectionDefinition(this.ConnectionName, this.ConnectionString, this.ConnectionAccessStatus);
            string filename = _csp.SaveConnectionDefinition(conndef);
            if (filename != string.Empty)
            {
                _msg.Length = 0;
                _msg.Append("Connection definition saved to ");
                _msg.Append(filename);
                _msg.Append(".");
                AppMessages.DisplayInfoMessage(_msg.ToString());
            }
        }

        private void GetUdlFile()
        {
            string configValue = AppGlobals.AppConfig.GetStringValueFromConfigFile("UdlFileFolder", string.Empty);
            if (configValue == string.Empty)
            {
                if (this.txtUdlFileName.Text.Trim() == string.Empty)
                {
                    _saveSelectionsFolder = _udlFileFolder;
                    _saveSelectionsFile = string.Empty;
                }
                else
                {
                    _saveSelectionsFolder = Path.GetDirectoryName(this.txtUdlFileName.Text);
                    _saveSelectionsFile = Path.GetFileName(this.txtUdlFileName.Text);
                }
            }
            else
            {
                if (this.txtUdlFileName.Text.Trim() == string.Empty)
                {
                    _saveSelectionsFolder = configValue;
                    _saveSelectionsFile = string.Empty;
                }
                else
                {
                    _saveSelectionsFolder = Path.GetDirectoryName(this.txtUdlFileName.Text);
                    _saveSelectionsFile = Path.GetFileName(this.txtUdlFileName.Text);
                }
            }
            if (Directory.Exists(_saveSelectionsFolder) == false)
            {
                Directory.CreateDirectory(_saveSelectionsFolder);
            }
            _saveFilter = "UDL Files|*.udl|All Files|*.*";
            DialogResult res = ShowOpenFileDialog();
            if (res == DialogResult.OK)
                this.txtUdlFileName.Text = _saveSelectionsFile;
        }


        private void DeleteConnectionDefinition()
        {
            _csp.DeleteConnectionDefinition(_dbPlatform);
        }

        private void ShowPageSettings()
        {
            _printer.ShowPageSettings();
        }

        private void FilePrint(bool preview, bool showPrintDialog)
        {
            _printer.PageTitle = AppGlobals.AppInfo.AssemblyDescription;
            _printer.PageSubTitle = "Application Form";
            _printer.PageFooter = AppGlobals.AppInfo.AssemblyProduct;
            _printer.ShowPageNumbers = true;
            _printer.ShowTotalPageNumber = true;

            _printer.Print(preview, showPrintDialog);

        }

        private void ShowHelpFile()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "OLEDB Connection Strings");
        }

        private bool HelpFileExists()
        {
            bool ret = false;

            if (File.Exists(_helpFilePath))
            {
                ret = true;
            }
            else
            {
                _msg.Length = 0;
                _msg.Append("Unable to find Help File: ");
                _msg.Append(_helpFilePath);
                AppMessages.DisplayWarningMessage(_msg.ToString());
            }

            return ret;
        }




#pragma warning restore 1591

    }//end class
}//end namespace
