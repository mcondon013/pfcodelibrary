using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppGlobals;
using PFConnectionObjects;
using PFDataAccessObjects;

namespace PFConnectionStrings
{
#pragma warning disable 1591
    public partial class PFConnectionStringManagerForm : Form
    {
        private StringBuilder _msg = new StringBuilder();

        private bool _showInstalledProvidersOnly = true;

        public PFConnectionStringManagerForm()
        {
            InitializeComponent();
        }

        //properties

        /// <summary>
        /// ShowInstalledProvidersOnly Property.
        /// </summary>
        public bool ShowInstalledProvidersOnly
        {
            get
            {
                return _showInstalledProvidersOnly;
            }
            set
            {
                _showInstalledProvidersOnly = value;
            }
        }


        //button click events
        private void cmdOK_Click(object sender, EventArgs e)
        {
            ShowConnectionStringForm();
        }

        private void cmdExitClick(object sender, EventArgs e)
        {
            HideForm();
        }

        private void chkShowInstalledProvidersOnly_CheckedChanged(object sender, EventArgs e)
        {
            this.ShowInstalledProvidersOnly = this.chkShowInstalledProvidersOnly.Checked;
            RefreshProvidersDisplay();
        }


        //form events
        private void WinForm_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }




        //common form processing routines
        public void InitializeForm()
        {
            if(_showInstalledProvidersOnly != this.chkShowInstalledProvidersOnly.Checked)
                this.chkShowInstalledProvidersOnly.Checked = _showInstalledProvidersOnly;
            else
                RefreshProvidersDisplay();
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
        private void ShowConnectionStringForm()
        {
            PFConnectionManager connMgr = null;
            DatabasePlatform dbPlat = DatabasePlatform.Unknown;
            ConnectionStringPrompt cp = null;


            try
            {
                if (this.optSQLServer.Checked)
                    dbPlat = DatabasePlatform.MSSQLServer;
                else if (this.optSQLServerCE35.Checked)
                    dbPlat = DatabasePlatform.SQLServerCE35;
                else if (this.optSQLServerCE40.Checked)
                    dbPlat = DatabasePlatform.SQLServerCE40;
                else if (this.optMicrosoftAccess.Checked)
                    dbPlat = DatabasePlatform.MSAccess;
                else if (this.optODBC.Checked)
                    dbPlat = DatabasePlatform.ODBC;
                else if (this.optOLEDB.Checked)
                    dbPlat = DatabasePlatform.OLEDB;
                else if (this.optOracleNative.Checked)
                    dbPlat = DatabasePlatform.OracleNative;
                else if (this.optMySQL.Checked)
                    dbPlat = DatabasePlatform.MySQL;
                else if (this.optDB2.Checked)
                    dbPlat = DatabasePlatform.DB2;
                else if (this.optInformix.Checked)
                    dbPlat = DatabasePlatform.Informix;
                else if (this.optSybase.Checked)
                    dbPlat = DatabasePlatform.Sybase;
                else if (this.optSQLAnywhere.Checked)
                    dbPlat = DatabasePlatform.SQLAnywhere;
                else if (this.optSQLAnywhereUL.Checked)
                    dbPlat = DatabasePlatform.SQLAnywhereUltraLite;
                else if (this.optMSOracle.Checked)
                    dbPlat = DatabasePlatform.MSOracle;
                else
                    dbPlat = DatabasePlatform.SQLServerCE35;

                connMgr = new PFConnectionManager();
                cp = new ConnectionStringPrompt(dbPlat, connMgr);
                cp.ConnectionString = string.Empty;
                System.Windows.Forms.DialogResult res = cp.ShowConnectionPrompt();
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                ;
            }
        }

        private void RefreshProvidersDisplay()
        {
            RefreshProviderOption(DatabasePlatform.MSSQLServer, this.optSQLServer);
            RefreshProviderOption(DatabasePlatform.SQLServerCE35, this.optSQLServerCE35);
            RefreshProviderOption(DatabasePlatform.SQLServerCE40, this.optSQLServerCE40);
            RefreshProviderOption(DatabasePlatform.MSAccess, this.optMicrosoftAccess);
            RefreshProviderOption(DatabasePlatform.ODBC, this.optODBC);
            RefreshProviderOption(DatabasePlatform.OLEDB, this.optOLEDB);
            RefreshProviderOption(DatabasePlatform.OracleNative, this.optOracleNative);
            RefreshProviderOption(DatabasePlatform.MySQL, this.optMySQL);
            RefreshProviderOption(DatabasePlatform.DB2, this.optDB2);
            RefreshProviderOption(DatabasePlatform.Informix, this.optInformix);
            RefreshProviderOption(DatabasePlatform.Sybase, this.optSybase);
            RefreshProviderOption(DatabasePlatform.SQLAnywhere, this.optSQLAnywhere);
            RefreshProviderOption(DatabasePlatform.SQLAnywhereUltraLite, this.optSQLAnywhereUL);
            RefreshProviderOption(DatabasePlatform.MSOracle, this.optMSOracle);

        }

        private void RefreshProviderOption(DatabasePlatform dbPlat, RadioButton opt)
        {
            PFConnectionManager connMgr = new PFConnectionManager();

            //test
            //if (this.chkShowInstalledProvidersOnly.Checked)
            //{
            //    if (dbPlat == DatabasePlatform.Informix
            //        || dbPlat == DatabasePlatform.SQLAnywhereUltraLite
            //        || dbPlat == DatabasePlatform.SQLServerCE40)
            //    {
            //        opt.Visible = false;
            //        return;
            //    }
            //}
            //end test

            if (connMgr.ProviderDefinitions.Count == 0)
            {
                connMgr.CreateProviderDefinitions();
            }
            else
            {
                connMgr.UpdateAllProvidersInstallationStatus();
            }

            if (this.chkShowInstalledProvidersOnly.Checked)
            {
                for (int i = 0; i < connMgr.ProviderDefinitions.Count; i++)
                {
                    PFProviderDefinition provDef = connMgr.ProviderDefinitions[i].Value;
                    if (provDef.DbPlatform == dbPlat)
                    {
                        if (provDef.InstallationStatus == enProviderInstallationStatus.IsInstalled)
                            opt.Visible = true;
                        else
                            opt.Visible = false;
                        break;
                    }
                }//end for loop
            }
            else
            {
                opt.Visible = true;
            }
        }

    }//end class
#pragma warning restore 1591

}//end namespace
