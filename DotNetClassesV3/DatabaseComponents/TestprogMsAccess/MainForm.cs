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
using PFDataAccessObjects;
//using PFSQLBuilderObjects;

namespace TestprogMsAccess
{
    public partial class MainForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
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

        //Mneu item clicks
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

                this.txtDatabasePath.Text = @"C:\Testfiles\Access\TestDb.accdb";
                this.chkOverwriteExistingDb.Checked = true;
                this.chkRunDropTableTest.Checked = false;

                this.cboAccessVersion.Items.Add("Access 2003");
                this.cboAccessVersion.Items.Add("Access 2007");
                this.cboAccessVersion.SelectedIndex = 0;
                this.cboDatabase.Items.Add(@"C:\Testdata\Access\AdventureWorksDW.mdb");
                this.cboDatabase.Items.Add(@"C:\Testdata\Access\AdventureWorksDW.accdb");
                this.cboDatabase.SelectedIndex = 0;
                this.txtDbUsername.Text = "admin";
                this.txtDbPassword.Text = string.Empty;

                _str.Length = 0;
                _str.Append("select g.GeographyKey as LockKey\r\n");
                _str.Append("      ,g.City\r\n");
                _str.Append("      ,g.StateProvinceName as State\r\n");
                _str.Append("      ,g.EnglishCountryRegionName as Country\r\n");
                _str.Append("      ,g.PostalCode\r\n");
                _str.Append("      ,st.SalesTerritoryRegion\r\n");
                _str.Append("      ,st.SalesTerritoryCountry\r\n");
                _str.Append("      ,st.SalesTerritoryGroup\r\n");
                _str.Append("  from DimGeography g\r\n");
                _str.Append("       inner join DimSalesTerritory st\r\n");
                _str.Append("         on g.SalesTerritoryKey = st.SalesTerritoryKey\r\n");
                _str.Append("");
                this.txtSqlQuery.Text = _str.ToString();


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }

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

            int nNumTestsSelected = 0;

            try
            {
                DisableFormControls();
                Tests.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;

                if (this.chkEraseOutputBeforeEachTest.Checked)
                {
                    Program._messageLog.Clear();
                }

                if (this.chkAdoxAdorTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.RunAdoxAdorTest();
                }

                if (this.chkCreateDatabaseTableTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.RunCreateDatabaseTableTest(this);
                }

                if (this.chkRunDropTableTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.RunDropTableTest(this);
                }

                if (this.chkConnectionTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.ConnectionTest(this);
                }

                if (this.chkRunQuery.Checked)
                {
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
                }

                if (this.chkGetQueryDataSchema.Checked)
                {
                    nNumTestsSelected++;
                    Tests.GetQueryDataSchema(this);
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

        private void cboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (System.IO.Path.GetExtension(this.cboDatabase.Text) == ".mdb")
                this.cboAccessVersion.Text = this.cboAccessVersion.Items[0].ToString();
            else
                this.cboAccessVersion.Text = this.cboAccessVersion.Items[1].ToString();
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

            
            string modifiedQueryText = this.txtSqlQuery.Text;

            // ******************************************************************************
            // Code to activate Query Builder follows
            // ******************************************************************************
            //uncomment next four lines to activate query builder
            //uncomment using PFSQLBuilderObjects; above
            //add reference to PFSQLBuilderObjects.dll in CPLibs\Binaries\ProFast\ClassLibraries\Release (this is default path; substitute path if you changed binaries path
            /*
            PFQueryBuilder qbf = new PFQueryBuilder();
            qbf.ConnectionString = GetConnectionString();
            qbf.DatabasePlatform = QueryBuilderDatabasePlatform.MSAccess;

            modifiedQueryText = qbf.ShowQueryBuilder(this.txtSqlQuery.Text);
            */
            // ******************************************************************************
            // End code to activate Query Builder
            // ******************************************************************************

            this.txtSqlQuery.Text = modifiedQueryText;
        }

        private string GetConnectionString()
        {
            PFMsAccess db = new PFMsAccess();
            string connectionString = string.Empty;

            try
            {
                db.DatabasePath = this.cboDatabase.Text;
                db.DatabaseUsername = this.txtDbUsername.Text;
                db.DatabasePassword = this.txtDbPassword.Text;
                if (this.cboAccessVersion.Text == "Access 2003")
                    db.OleDbProvider = PFAccessOleDbProvider.MicrosoftJetOLEDB_4_0;
                else
                    db.OleDbProvider = PFAccessOleDbProvider.MicrosoftACEOLEDB_12_0;

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
