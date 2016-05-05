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
using PFDataAccessObjects;
using System.Configuration;

namespace TestprogOleDb
{
    public partial class MainForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";

        private string _defaultQuery = string.Empty;
        private string _namelistsQuery = "select StateCode as CD, StateName as Name from dbo.tblUS_StateCodes where StateCode <> ' '";
        private string _oracleQuery = "Select COUNTRY_ID as ID, COUNTRY_NAME AS Name, REGION_ID as REGION from HR.COUNTRIES";
        private string _db2Query = "SELECT EMPNO, LASTNAME, FIRSTNME, WORKDEPT AS WRK, JOB FROM MIKE.EMPLOYEE";
        private string _informixQuery = "SELECT customer_num as custno, fname, lname, company, address1, address2, city, state as st, zipcode as zip, phone FROM informix.customer";
        private string _sqlce35Query = "select FirstName, min(RowId) as MinRow, count(*) as numRows\r\n from USFirstNamesTest\r\n group by FirstName\r\n order by FirstName";
        private string _sqlAnywhereQuery = "Select CurrencyKey as PK ,CurrencyAlternateKey as AK, CurrencyName from DBA.DimCurrency";

        private string _customCreateTableScript = "Create Table MikeTab01 (F1 INTEGER, F2 NVARCHAR(50));";
        private string _oracleCreateTableScript = "Create Table MikeTab01 (F1 NUMBER not null, F2 NVARCHAR2(50) null);";

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

        private void chkCreateTableTest_CheckedChanged(object sender, EventArgs e)
        {
            cboConnectionString_SelectedIndexChanged(this.cboConnectionString, new EventArgs());
        }

        private void chkUseOdbcBuilder_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkUseOleDbBuilder.Checked)
            {
                this.chkUseCustomSQL.Checked = false;
            }
            cboConnectionString_SelectedIndexChanged(this.cboConnectionString, new EventArgs());
        }

        private void chkUseCustomSQL_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkUseCustomSQL.Checked)
            {
                this.chkUseOleDbBuilder.Checked = false;
            }
            cboConnectionString_SelectedIndexChanged(this.cboConnectionString, new EventArgs());
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

                this.cboConnectionString.Items.Add(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Testdata\Access\AdventureWorksDW.accdb;User Id=admin;Password=;Jet OLEDB:Engine Type=6");
                this.cboConnectionString.Items.Add(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Testdata\Access\AdventureWorksDW.mdb;User Id=admin;Password=;Jet OLEDB:Engine Type=6;");
                this.cboConnectionString.Items.Add(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Testdata\Access\AdventureWorksDW.mdb;User Id=admin;Password=;Jet OLEDB:Engine Type=5;");
                this.cboConnectionString.Items.Add(@"Provider=SQLOLEDB;Data Source=PROFASTSV2;Initial Catalog=AdventureWorksDW2008R2;Integrated Security=SSPI;");
                this.cboConnectionString.Items.Add(@"Provider=SQLOLEDB;Data Source=PROFASTWS1;Initial Catalog=Namelists;Integrated Security=SSPI;");
                this.cboConnectionString.Items.Add(@"Provider=Microsoft.SQLSERVER.CE.OLEDB.3.5;Data Source=C:\SQLData\nametest.sdf;");
                this.cboConnectionString.Items.Add(@"Provider=Microsoft.SQLSERVER.CE.OLEDB.3.5;Data Source=C:\Users\Mike\Documents\PFApps\pfRandomNamesGenerator\DataExports\DataExports.sdf;");
                this.cboConnectionString.Items.Add(@"Provider=msdaora;Data Source=ORASV4;User Id=SYSTEM;Password=NEW1992;");
                this.cboConnectionString.Items.Add(@"Provider=OraOLEDB.Oracle;Data Source=ORASV4;User Id=SYSTEM;Password=NEW1992;");
                this.cboConnectionString.Items.Add(@"Provider=IBMDADB2;Database=sample;Hostname=profastsv4db2;Port=50000;Protocol=TCPIP;Uid=Mike;Pwd=MIKE92;");
                this.cboConnectionString.Items.Add(@"Provider=Ifxoledbc;Data Source=miketest@ol_informix1210;User Id=informix;Password=IMX1992;");
                this.cboConnectionString.Items.Add(@"Provider=ASEOLEDB;Data Source=PROFASTSV2SYB:5000;Initial Catalog=AdventureWorks;User Id=SA;Password=SA1992;");
                this.cboConnectionString.Items.Add(@"Provider=SAOLEDB;Data Source=SQLAnywhere_AdventureWorks;Initial Catalog=AdventureWorks;User Id=DBA;Password=sql;");

                this.cboConnectionString.SelectedIndex = 0;

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
                _defaultQuery = _str.ToString();

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

                if (this.chkRunConnectionTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.RunConnectionTest(this);
                }

                if (this.chkRunDataReaderTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.RunDataReaderTest(this);
                }

                if (this.chkRunDataTableTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.RunDataTableTest(this);
                }

                if (this.chkRunDataSetTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.RunDataSetTest(this);
                }

                if (this.chkCreateTableTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.CreateTableTest(this);
                }

                if (this.chkDataReaderToDataTableTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.DataReaderToDataTableTest(this);
                }

                if (this.chkGetQueryDataSchema.Checked)
                {
                    nNumTestsSelected++;
                    Tests.GetQueryDataSchema(this);
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

            string modifiedQueryText = this.txtSqlQuery.Text;

            // ******************************************************************************
            // Code to activate Query Builder follows
            // ******************************************************************************
            //uncomment next four lines to activate query builder
            //uncomment using PFSQLBuilderObjects; above
            //add reference to PFSQLBuilderObjects.dll in CPLibs\Binaries\ProFast\ClassLibraries\Release (this is default path; substitute path if you changed binaries path
            /*
            PFQueryBuilder qbf = new PFQueryBuilder();
            qbf.ConnectionString = this.cboConnectionString.Text;
            qbf.DatabasePlatform = QueryBuilderDatabasePlatform.OLEDB;
            //qbf.DatabasePlatform = QueryBuilderDatabasePlatform.MSAccess;
            qbf.AnsiSQLVersion = AnsiSQLLevel.SQL92;

            modifiedQueryText = qbf.ShowQueryBuilder(this.txtSqlQuery.Text);
            */
            // ******************************************************************************
            // End code to activate Query Builder
            // ******************************************************************************

            this.txtSqlQuery.Text = modifiedQueryText;
        }

        private void cboConnectionString_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboConnectionString.Text.ToLower().Contains("oracle")
                || this.cboConnectionString.Text.ToLower().Contains("msdaora"))
                this.txtSqlQuery.Text = _oracleQuery;
            else if (this.cboConnectionString.Text.ToLower().Contains("sqlserver.ce"))
                this.txtSqlQuery.Text = _sqlce35Query;
            else if (this.cboConnectionString.Text.ToLower().Contains("namelists")
                     && this.cboConnectionString.Text.ToLower().Contains("sqloledb"))
                this.txtSqlQuery.Text = _namelistsQuery;
            else if (this.cboConnectionString.Text.ToLower().Contains("db2"))
                this.txtSqlQuery.Text = _db2Query;
            else if (this.cboConnectionString.Text.ToLower().Contains("ifxoledbc"))
                this.txtSqlQuery.Text = _informixQuery;
            else if (this.cboConnectionString.Text.ToLower().Contains("sql anywhere")
                     || this.cboConnectionString.Text.ToLower().Contains("saoledb"))
                this.txtSqlQuery.Text = _sqlAnywhereQuery;
            else
                this.txtSqlQuery.Text = _defaultQuery;

            if (this.cboConnectionString.Text.ToLower().Contains("sqlserver.ce"))
                this.cmdBuildQuery.Visible = false;
            else
                this.cmdBuildQuery.Visible = true;
            //test
            this.cmdBuildQuery.Visible = true;

            if (this.chkCreateTableTest.Checked && this.chkUseCustomSQL.Checked)
            {
                if (this.cboConnectionString.Text.ToLower().Contains("oracle")
                    || this.cboConnectionString.Text.ToLower().Contains("msdaora"))
                    this.txtSqlQuery.Text = _oracleCreateTableScript;
                else
                    this.txtSqlQuery.Text = _customCreateTableScript;
            }
            else
            {
                ;
            }
        }



    }//end class
}//end namespace
