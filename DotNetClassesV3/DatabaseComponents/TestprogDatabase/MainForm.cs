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
using System.Configuration;

namespace TestprogDatabase
{
    public partial class MainForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        private string _helpFilePath = string.Empty;

        private string _defaultQuery = "select 'value1' as F1, 'value2' as F2";
        private string _sqlQuery = string.Empty;
        private string _sqlCE35Query = "select FirstName, min(RowId) as MinRow, count(*) as NumRows\r\n from USFirstNamesTest\r\n group by FirstName\r\n order by FirstName;";
        private string _sqlCE40Query = "select FirstName, min(RowId) as MinRow, count(*) as numRows\r\n from USFirstNamesTest\r\n group by FirstName\r\n order by FirstName;";
        private string _accessQuery = string.Empty;
        private string _oledbQuery = "SELECT EMPNO, LASTNAME, FIRSTNME, WORKDEPT AS WRK, JOB FROM MIKE.EMPLOYEE";
        private string _odbcQuery = "Select COUNTRY_ID as ID, COUNTRY_NAME AS Name, REGION_ID as REGION from HR.COUNTRIES";
        private string _oracleQuery = "Select COUNTRY_ID as ID, COUNTRY_NAME AS Name, REGION_ID as REGION from HR.COUNTRIES";
        private string _db2Query = "SELECT EMPNO, LASTNAME, FIRSTNME, WORKDEPT AS WRK, JOB FROM MIKE.EMPLOYEE";
        private string _informixQuery = "SELECT customer_num as custno, fname, lname, company, address1, address2, city, state as st, zipcode as zip, phone FROM informix.customer";
        private string _mysqlQuery = "SELECT ID as nbr, name, country, city, \"zip code\", phone FROM sakila.customer_list";
        private string _sybaseQuery = string.Empty;
        private string _sqlAnywhereQuery = "Select CurrencyKey as PK ,CurrencyAlternateKey as AK, CurrencyName from DBA.DimCurrency";
        private string _sqlAnywhereULQuery = "Select K1, F1, F2 from TestTab01";

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

        private void cmdRunTest_Click(object sender, EventArgs e)
        {
            RunTests();
        }

        //Menu item clicks
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

                string executableFolder = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;
                string helpFileName = AppConfig.GetStringValueFromConfigFile("HelpFileName", "InitWinFormsHelpFile.chm");
                string helpFilePath = PFFile.FormatFilePath(executableFolder, helpFileName);
                this.appHelpProvider.HelpNamespace = helpFilePath;
                _helpFilePath = helpFilePath;

                this.chkEraseOutputBeforeEachTest.Checked = true;
                
                this.txtUpdateBatchSize.Text = "1";

                LoadDotNetConnectionStrings();

                LoadAppConfigItems();

                InitializeQueries();

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }

        }

        private void LoadDotNetConnectionStrings()
        {
            this.cboConnectionString.Items.Clear();

            this.cboConnectionString.Items.Add(@"SQLServerCE35" + @"|" + @"data source='c:\SQLData\nametestV4_35.sdf';");
            this.cboConnectionString.Items.Add(@"SQLServerCE40" + @"|" + @"data source='c:\SQLData\nametestV4.sdf';");
            this.cboConnectionString.Items.Add(@"OracleNative" + @"|" + @"Data Source=PROFASTSV4ORA;User ID=SYSTEM;Password=NEW1992;");
            this.cboConnectionString.Items.Add(@"MySQL" + @"|" + @"server=PROFASTSV4MYSQL; port=3306; database=SAKILA;User Id=Mike; password=MIKE92;");
            this.cboConnectionString.Items.Add(@"DB2" + @"|" + @"Database=SAMPLE;User ID=DB2ADMIN;Password=DB21992;Server=PROFASTSV4DB2:50000;");
            this.cboConnectionString.Items.Add(@"Informix" + @"|" + @"Database=miketest;User ID=informix;Password=IMX1992;Server=profastsv4imx:9089;");
            this.cboConnectionString.Items.Add(@"Sybase|Data Source=PROFASTSV2SYB;Port=5000;Database=AdventureWorks;Uid=SA;Pwd=SA1992;");
            this.cboConnectionString.Items.Add(@"SQLAnywhere" + @"|" + @"UserID=DBA;Password=sql;DatabaseName=AdventureWorks;DatabaseFile=C:\Testdata\SQLAnywhere\AdventureWorks.Db;ServerName=AdventureWorks");
            this.cboConnectionString.Items.Add(@"SQLAnywhereUltraLite" + @"|" + @"nt_file=C:\Testdata\SQLAnywhere\Test1.udb;dbn=Test1;uid=DBA;pwd=sql");
            this.cboConnectionString.Items.Add(@"MSSQLServer" + @"|" + @"Data Source=PROFASTSV2; Initial Catalog=AdventureWorksDW2008R2; Integrated Security=True; Application Name=TestprogGetSchemas; Workstation ID=PROFASTWS5;");
            this.cboConnectionString.Items.Add(@"MSAccess" + @"|" + @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Testdata\Access\AdventureWorksDW.accdb;User Id=admin;Password=;Jet OLEDB:Engine Type=6");
            this.cboConnectionString.Items.Add(@"MSOracle" + @"|" + @"Data Source=PROFASTSV4ORA;User ID=SYSTEM;Password=NEW1992;");
            this.cboConnectionString.Items.Add(@"ODBC" + @"|" + @"Driver={Oracle in OraClient11g_home1};Dbq=ORASV4;Uid=SYSTEM;Pwd=NEW1992;");
            this.cboConnectionString.Items.Add(@"OLEDB" + @"|" + @"Provider=IBMDADB2;Database=sample;Hostname=profastsv4db2;Port=50000;Protocol=TCPIP;Uid=Mike;Pwd=MIKE92;");

            this.cboConnectionString.SelectedIndex = 0;
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

        private void InitializeQueries()
        {

            //sybase
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
            _str.Append("       left join dbo.DimSalesTerritory st\r\n");
            _str.Append("         on g.SalesTerritoryKey = st.SalesTerritoryKey\r\n");
            _str.Append("");
            _sybaseQuery = _str.ToString();

            //sql server
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
            _sqlQuery = _str.ToString();

            //Access
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
            _str.Append("       left join DimSalesTerritory st\r\n");
            _str.Append("         on g.SalesTerritoryKey = st.SalesTerritoryKey\r\n");
            _str.Append("");
            _accessQuery = _str.ToString();






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

                if (this.chkConnectionTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.ConnectionTest(this);
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

                if (this.chkGetQueryDataSchema.Checked)
                {
                    nNumTestsSelected++;
                    Tests.GetQueryDataSchema(this);
                }

                if (this.chkCreateTableTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.CreateTableTest(this);
                }

                if (this.chkImportDataTable.Checked)
                {
                    nNumTestsSelected++;
                    Tests.ImportTableTest(this);
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

        private void cboConnectionString_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProcessConnectionSelectionChange();
        }

        private void ProcessConnectionSelectionChange()
        {
            string selectedConnection = this.cboConnectionString.Text;
            string[] parsedString = selectedConnection.Split('|');
            string dbPlatformDesc = parsedString[0];

            /*
            "SQLServerCE40"
            "SQLServerCE35"
            "OracleNative"
            "MySQL"
            "DB2"
            "Informix"
            "Sybase"
            "SQLAnywhere"
            "SQLAnywhereUltraLite"
            "MSSQLServer"
            "MSAccess"
            "MSOracle"
            "ODBC"
            "OLEDB"
            */
            switch (dbPlatformDesc)
            {
                case "SQLServerCE40":
                    this.txtSQLQuery.Text = _sqlCE40Query;
                    this.txtTableName.Text = "TestTable40";
                    this.txtKeyValsTableName.Text = "KeyValTable";
                    break;
                case "SQLServerCE35":
                    this.txtSQLQuery.Text = _sqlCE35Query;
                    this.txtKeyValsTableName.Text = "KeyValTable";
                    this.txtTableName.Text = "TestTable35";
                    break;
                case "OracleNative":
                    this.txtSQLQuery.Text = _oracleQuery;
                    this.txtTableName.Text = "hr.TestTableORA";
                    this.txtKeyValsTableName.Text = "hr.KeyValTable";
                   break;
                case "MySQL":
                    this.txtSQLQuery.Text = _mysqlQuery;
                    this.txtTableName.Text = "sakila.TestTableMYSQL";
                    this.txtKeyValsTableName.Text = "SAKILA.KeyValTable";
                    break;
                case "DB2":
                    this.txtSQLQuery.Text = _db2Query;
                    this.txtTableName.Text = "mike.TestTableDB2";
                    this.txtKeyValsTableName.Text = "mike.KeyValTable";
                    break;
                case "Informix":
                    this.txtSQLQuery.Text = _informixQuery;
                    this.txtTableName.Text = "informix.TestTableIFX";
                    this.txtKeyValsTableName.Text = "informix.KeyValTable";
                    break;
                case "Sybase":
                    this.txtSQLQuery.Text = _sybaseQuery;
                    this.txtTableName.Text = "dbo.TestTableSYB";
                    this.txtKeyValsTableName.Text = "dbo.KeyValTable";
                    break;
                case "SQLAnywhere":
                    this.txtSQLQuery.Text = _sqlAnywhereQuery;
                    this.txtTableName.Text = "dba.TestTableSQLA";
                    this.txtKeyValsTableName.Text = "dba.KeyValTable";
                    break;
                case "SQLAnywhereUltraLite":
                    this.txtSQLQuery.Text = _sqlAnywhereULQuery;
                    this.txtTableName.Text = "TestTableUL";
                    this.txtKeyValsTableName.Text = "KeyValTable";
                    break;
                case "MSSQLServer":
                    this.txtSQLQuery.Text = _sqlQuery;
                    this.txtTableName.Text = "dbo.TestTable01";
                    this.txtKeyValsTableName.Text = "dbo.KeyValTable";
                    break;
                case "MSAccess":
                    this.txtSQLQuery.Text = _accessQuery;
                    this.txtTableName.Text = "TestTableACC";
                    this.txtKeyValsTableName.Text = "KeyValTable";
                    break;
                case "MSOracle":
                    this.txtSQLQuery.Text = _oracleQuery;
                    this.txtTableName.Text = "TestTableORA";
                    this.txtKeyValsTableName.Text = "hr.KeyValTable";
                    break;
                case "ODBC":
                    this.txtSQLQuery.Text = _odbcQuery;
                    this.txtTableName.Text = "hr.TestTableODBC";  //oracle odbc driver is used
                    this.txtKeyValsTableName.Text = "hr.KeyValTable";
                    break;
                case "OLEDB":
                    this.txtSQLQuery.Text = _oledbQuery;
                    this.txtTableName.Text = "mike.TestTableOLEDB";  //db2 oledb driver is used
                    this.txtKeyValsTableName.Text = "mike.KeyValTable";
                    break;
                default:
                    this.txtSQLQuery.Text = _defaultQuery;
                    this.txtTableName.Text = "TestTableDEF";
                    this.txtKeyValsTableName.Text = "KeyValTable";
                    break;            
            }

        }


    }//end class
}//end namespace
