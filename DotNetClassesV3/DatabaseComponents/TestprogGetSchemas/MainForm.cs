using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AppGlobals;
using PFFileSystemObjects;

namespace TestprogGetSchemas
{
    public partial class MainForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
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

        private void cmdRunTests_Click(object sender, EventArgs e)
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

        private void optUseDotNetProvider_CheckedChanged(object sender, EventArgs e)
        {
            if (optUseDotNetProvider.Checked)
                LoadDotNetConnectionStrings();
        }

        private void optUseOdbcDriver_CheckedChanged(object sender, EventArgs e)
        {
            if (optUseOdbcDriver.Checked)
                LoadOdbcDriverConnectionStrings();
        }

        private void optUseOleDbProvider_CheckedChanged(object sender, EventArgs e)
        {
            if (optUseOleDbProvider.Checked)
                LoadOleDbProviderConnectionStrings();
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

                this.optUseDotNetProvider.Checked = true;


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

                if (this.chkGetProviderList.Checked)
                {
                    nNumTestsSelected++;
                    Tests.GetProviderList(this);
                }

                if (this.chkGetMetadataCollections.Checked)
                {
                    nNumTestsSelected++;
                    Tests.GetMetadataCollections(this);
                }

                if (this.chkGetTablesCollection.Checked)
                {
                    nNumTestsSelected++;
                    Tests.GetTablesCollection(this);
                }

                if (this.chkGetRestrictionsCollection.Checked)
                {
                    nNumTestsSelected++;
                    Tests.GetRestrictionsCollection(this);
                }

                if (this.chkGetDataTypesCollection.Checked)
                {
                    nNumTestsSelected++;
                    Tests.GetDataTypesCollection(this);
                }

                if (this.chkGetSQLServerTables.Checked)
                {
                    nNumTestsSelected++;
                    Tests.GetSQLServerTables(this);
                }

                if (this.chkTablePatternMatchTests.Checked)
                {
                    nNumTestsSelected++;
                    Tests.TablePatternMatchTest(this);
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
        private void LoadDotNetConnectionStrings()
        {
            this.cboConnectionString.Items.Clear();

            this.cboConnectionString.Items.Add(@"System.Data.SqlClient" + @"|" + @"Data Source=PROFASTSV2; Initial Catalog=AdventureWorksDW2008R2; Integrated Security=True; Application Name=TestprogGetSchemas; Workstation ID=PROFASTWS5;");
            this.cboConnectionString.Items.Add(@"System.Data.OracleClient" + @"|" + @"Data Source=PROFASTSV4ORA;User ID=SYSTEM;Password=ORA1992;");
            this.cboConnectionString.Items.Add(@"Oracle.DataAccess.Client" + @"|" + @"Data Source=PROFASTSV4ORA;User ID=SYSTEM;Password=ORA1992;");
            this.cboConnectionString.Items.Add(@"IBM.Data.DB2" + @"|" + @"Database=SAMPLE;User ID=DB2ADMIN;Password=DB21992;Server=PROFASTSV4DB2:50000;");
            this.cboConnectionString.Items.Add(@"MySql.Data.MySqlClient" + @"|" + @"server=PROFASTSV4MYSQL; port=3306; database=SAKILA;User Id=Mike; password=MIKE92;");
            this.cboConnectionString.Items.Add(@"Sybase.Data.AseClient|Data Source=PROFASTSV2SYB;Port=5000;Database=AdventureWorks;Uid=SA;Pwd=SA1992;");
            this.cboConnectionString.Items.Add(@"iAnywhere.Data.SQLAnywhere" + @"|" + @"UserID=DBA;Password=sql;DatabaseName=AdventureWorks;DatabaseFile=C:\Testdata\SQLAnywhere\AdventureWorks.Db;ServerName=AdventureWorks");
            this.cboConnectionString.Items.Add(@"System.Data.SqlServerCe.4.0" + @"|" + @"data source='c:\SQLData\nametestV4.sdf';");
            this.cboConnectionString.Items.Add(@"System.Data.SqlServerCe.3.5" + @"|" + @"data source='c:\SQLData\nametestV4_35.sdf';");

            this.cboConnectionString.SelectedIndex = 0;
        }

        private void LoadOdbcDriverConnectionStrings()
        {
            this.cboConnectionString.Items.Clear();

            this.cboConnectionString.Items.Add(@"Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=C:\Testdata\Access\AdventureWorksDW.accdb;Uid=Admin;Pwd=;");
            this.cboConnectionString.Items.Add(@"Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=C:\Testdata\Access\AdventureWorksDW.mdb;Uid=Admin;Pwd=;");
            this.cboConnectionString.Items.Add(@"Driver={SQL Server Native Client 10.0};Server=profastws1;Database=AdventureWorksDW2008R2;Trusted_Connection=yes;");
            this.cboConnectionString.Items.Add(@"Driver={SQL Server Native Client 10.0};Server=profastws1;Database=Namelists;Trusted_Connection=yes;");
            this.cboConnectionString.Items.Add(@"Driver={Oracle in OraClient11g_home1};Dbq=ORASV4;Uid=SYSTEM;Pwd=NEW1992;");
            this.cboConnectionString.Items.Add(@"Driver={Microsoft ODBC for Oracle};Server=ORASV4;Uid=SYSTEM;Pwd=NEW1992;");
            this.cboConnectionString.Items.Add(@"Driver={IBM DB2 ODBC DRIVER};Database=sample;Hostname=profastsv4db2;Port=50000;Protocol=TCPIP;Uid=Mike;Pwd=MIKE92;");
            this.cboConnectionString.Items.Add(@"Driver={MySQL ODBC 5.2w Driver};Server=profastsv4mysql;Database=SAKILA;User=Mike;Password=MIKE92;Option=3;");
            this.cboConnectionString.Items.Add(@"Driver={Adaptive Server Enterprise};server=profastsv2syb;port=5000;sourceDb=AdventureWorks;uid=SA;Pwd=SA1992;");
            this.cboConnectionString.Items.Add(@"Driver={SQL Anywhere 12};Uid=DBA;Pwd=sql;DBF=C:\Testdata\SQLAnywhere\AdventureWorks.Db;");

            this.cboConnectionString.SelectedIndex = 0;
        }

        public void LoadOleDbProviderConnectionStrings()
        {
            this.cboConnectionString.Items.Clear();

            this.cboConnectionString.Items.Add(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Testdata\Access\AdventureWorksDW.accdb;User Id=admin;Password=;Jet OLEDB:Engine Type=6");
            this.cboConnectionString.Items.Add(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Testdata\Access\AdventureWorksDW.mdb;User Id=admin;Password=;Jet OLEDB:Engine Type=6;");
            this.cboConnectionString.Items.Add(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Testdata\Access\AdventureWorksDW.mdb;User Id=admin;Password=;Jet OLEDB:Engine Type=5;");
            this.cboConnectionString.Items.Add(@"Provider=SQLOLEDB;Data Source=PROFASTWS1;Initial Catalog=AdventureWorksDW2008R2;Integrated Security=SSPI;");
            this.cboConnectionString.Items.Add(@"Provider=SQLOLEDB;Data Source=PROFASTWS1;Initial Catalog=Namelists;Integrated Security=SSPI;");
            this.cboConnectionString.Items.Add(@"Provider=Microsoft.SQLSERVER.CE.OLEDB.3.5;Data Source=C:\SQLData\nametest.sdf;");
            this.cboConnectionString.Items.Add(@"Provider=msdaora;Data Source=ORASV4;User Id=SYSTEM;Password=NEW1992;");
            this.cboConnectionString.Items.Add(@"Provider=OraOLEDB.Oracle;Data Source=ORASV4;User Id=SYSTEM;Password=NEW1992;");
            this.cboConnectionString.Items.Add(@"Provider=IBMDADB2;Database=sample;Hostname=profastsv4db2;Port=50000;Protocol=TCPIP;Uid=Mike;Pwd=MIKE92;");
            this.cboConnectionString.Items.Add(@"Provider=ASEOLEDB;Data Source=PROFASTSV2SYB:5000;Initial Catalog=AdventureWorks;User Id=SA;Password=SA1992;");
            this.cboConnectionString.Items.Add(@"Provider=SAOLEDB;Data Source=SQLAnywhere_AdventureWorks;Initial Catalog=AdventureWorks;User Id=DBA;Password=sql;");

            this.cboConnectionString.SelectedIndex = 0;
        }

    }//end class
}//end namespace
