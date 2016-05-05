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

namespace PFConnectionStrings
{
    /// <summary>
    /// Class for displaying a form for the input of values for connection string keys.
    /// </summary>
    public partial class PFConnectionStringFormOLD : Form, IConnectionStringForm 
    {
        private StringBuilder _msg = new StringBuilder();

        private PFSQLServer _db = new PFSQLServer();

        //private variables for properties
        private PFConnectionManager _connectionManager = null;
        private DatabasePlatform _dbPlatform = DatabasePlatform.MSSQLServer;
        private string _connectionName = string.Empty;
        private string _connectionString = string.Empty;
        private ConnectionStringPrompt _csp = null;
        private enConnectionAccessStatus _connectionAccessStatus = enConnectionAccessStatus.Unknown;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFConnectionStringFormOLD()
        {
            InitializeComponent();
        }

        //properties

        /// <summary>
        /// ConnectionManager object to use for processing connection information.
        /// </summary>
        public PFConnectionManager ConnectionManager
        {
            get
            {
                return _connectionManager;
            }
            set
            {
                _connectionManager = value;
            }
        }

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
            UpdateConnectionProperties();
            this.DialogResult = DialogResult.OK;
            HideForm();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            HideForm();
        }

        private void cmdNewDefinition_Click(object sender, EventArgs e)
        {
            NewConnectionDefinition();
        }

        private void cmdOpenExistingDefinition_Click(object sender, EventArgs e)
        {
            OpenConnectionDefinition();
        }

        private void cmdSaveCurrentDefinition_Click(object sender, EventArgs e)
        {
            SaveConnectionDefinition();
        }

        private void cmdBuildConnectionString_Click(object sender, EventArgs e)
        {
            BuildConnectionString();
        }

        private void cmdVerifyConnectionString_Click(object sender, EventArgs e)
        {
            VerifyConnectionString();
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

        private void UpdateConnectionProperties()
        {
            _connectionName = this.txtConnectionName.Text;
            _connectionString = this.txtConnectionString.Text;
        }

        private void NewConnectionDefinition()
        {
            AppMessages.DisplayAlertMessage("NewConnectionDefinition not yet implemented.");
        }

        private void OpenConnectionDefinition()
        {
            AppMessages.DisplayAlertMessage("OpenConnectionDefinition not yet implemented.");
        }

        private void SaveConnectionDefinition()
        {
            AppMessages.DisplayAlertMessage("SaveConnectionDefinition not yet implemented.");
        }

        private void BuildConnectionString()
        {
            AppMessages.DisplayAlertMessage("BuildConnectionString not yet implemented.");
        }

        private void VerifyConnectionString()
        {
            AppMessages.DisplayAlertMessage("VerifyConnectionString not yet implemented.");
        }


#pragma warning restore 1591

    }//end class
}//end namespace
