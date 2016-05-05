using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppGlobals;
using PFDataAccessObjects;
using PFConnectionObjects;
using PFListObjects;
using PFTextObjects;

namespace PFConnectionStrings
{
    //See TODO comments in code for what must be changed.

    /// <summary>
    /// Class for displaying a form for the input of values for connection string keys.
    /// </summary>
    public partial class PFConnectionStringForm : Form, IConnectionStringForm
    {
        private StringBuilder _msg = new StringBuilder();

        private PFDatabase _db = null;        //TODO: Change this

        //TODO
        //private string _saveDataSource = string.Empty;
        //private string _saveUdlFileName = string.Empty;
        //private bool _savePersistSecurityInfo = false;
        //private string _saveEncryptionMode = string.Empty;
        //END TODO
 
        //private variables for properties
        DatabasePlatform _dbPlatform = DatabasePlatform.Unknown;    //TODO: Change this
        private string _connectionName = string.Empty;
        private string _connectionString = string.Empty;
        private ConnectionStringPrompt _csp = null;
        private enConnectionAccessStatus _connectionAccessStatus = enConnectionAccessStatus.Unknown;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFConnectionStringForm()
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

        private void toolbtnCancel_Click(object sender, EventArgs e)
        {
            cmdCancel_Click(null, null);
        }

        private void toolbarHelp_Click(object sender, EventArgs e)
        {
            ShowConnectionDefinitionHelp();
        }


        //form events
        private void WinForm_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }




        //common form processing routines
        public void InitializeForm()
        {
            EnableFormControls();
            this.txtDatabasePlatform.Text = _dbPlatform.ToString();
            InitDatabaseObject();
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

        private void InitDatabaseObject()
        {
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;

            string configValue = AppConfig.GetStringValueFromConfigFile(_dbPlatform.ToString(), string.Empty);
            if (configValue.Length > 0)
            {
                string[] parsedConfig = configValue.Split('|');
                if (parsedConfig.Length != 3)
                {
                    _msg.Length = 0;
                    _msg.Append("Invalid config entry items for ");
                    _msg.Append(_dbPlatform.ToString());
                    _msg.Append(". Number of items after parse: ");
                    _msg.Append(parsedConfig.Length.ToString());
                    _msg.Append(".");
                    throw new System.Exception(_msg.ToString());
                }

                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                _db = new PFDatabase(_dbPlatform.ToString(), dllPath, nmSpace + "." + clsName);

            }
            else
            {
                _db = new PFDatabase(_dbPlatform);
            }
        }
        

        //TODO
        private void SaveFormFields()
        {
            //_saveDataSource = this.txtDataSource.Text;
            //_saveUdlFileName = this.txtPassword.Text;
            //_savePersistSecurityInfo = this.chkEncryptionOn.Checked;
            //_saveEncryptionMode = this.cboEncryptionMode.Text;
        }

        private bool FormFieldsHaveChanged()
        {
            bool retval = false;

            //if (_saveDataSource != this.txtDataSource.Text
            //    || _saveUdlFileName != this.txtPassword.Text
            //    || _savePersistSecurityInfo != this.chkEncryptionOn.Checked
            //    || _saveEncryptionMode != this.cboEncryptionMode.Text)
            //{
            //    retval = true;
            //}

            return retval;
        }
        //END TODO

        
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

        //Application routines

        private bool UpdateConnectionProperties()
        {
            bool updateCompleted = true;
            _connectionName = this.txtConnectionName.Text;
            if (FormFieldsHaveChanged())
            {
                DialogResult res = PromptForConnectionStringRebuild();
                if (res == DialogResult.Yes)
                {
                    this.ConnectionString = BuildConnectionString();
                    _connectionString = this.txtConnectionString.Text;
                }
                else if (res == DialogResult.Cancel)
                    updateCompleted = false;
                else
                {
                    _connectionString = this.txtConnectionString.Text;
                }
            }

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

            //TODO: Change this
            //_db.SetPropertyValue("ServerName", this.txtServerName.Text);
            //END TODO 
            connStr = _db.ConnectionString;
            
            SaveFormFields();

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
                        if (ctl.Name == "chkUseIntegratedSecurity")
                            chk.Checked = true;
                        ;
                    }
                }
            }//end foreach
            SaveFormFields();
        }


        private void FillPropertiesFromConnectionString()
        {
            _db.ConnectionString = this.ConnectionString;
            //TODO: Chamge
            //this.txtServerName.Text = _db.GetPropertyValue("ServerName").ToString();
            //END TODO:
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

                //TODO
                //this.txtServerName.Text = _db.GetPropertyValue("ServerName");
                //END TODO

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
            else
            {
                tempConnectionString = BuildConnectionString();
                if (this.ConnectionString != tempConnectionString)
                {
                    _msg.Length = 0;
                    _msg.Append("One or more connection properties have changes since connection string was last built.\r\n");
                    _msg.Append("Do you want to rebuild the connection string before saving it?");
                    DialogResult res = AppMessages.DisplayMessage(_msg.ToString(), "Connection string save ...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                    if (res == DialogResult.Yes)
                    {
                        this.ConnectionString = tempConnectionString;
                    }
                    else
                    {
                        if (res == DialogResult.Cancel)
                            return;
                        else
                        {
                            //continue with save operation even though connection string has not been updated with latest connection properties
                            ;
                        }
                    }
                }
            }


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

        private void ShowConnectionDefinitionHelp()
        {
            AppMessages.DisplayAlertMessage("Help not yet implemented.");
        }



#pragma warning restore 1591

    }//end class
}//end namespace
