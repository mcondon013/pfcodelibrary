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
using PFDataAccessObjects;
using PFCollectionsObjects;

namespace TestprogTableDefs
{
    public partial class MainForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";

        private PFList<string> _connectionStrings = new PFList<string>();

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

        private void cboSourceDbPlatform_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSourceDatabasePlatformConnectionStrings();
        }

        private void cboDestinationDbPlatform_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDestinationDatabasePlatformConnectionStrings();
        }

        private void txtIncludePatterns_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = null;
        }

        private void txtIncludePatterns_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void txtIncludePatterns_Leave(object sender, EventArgs e)
        {
            this.AcceptButton = this.cmdRunTests;
        }

        private void txtExcludePatterns_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = null;
        }

        private void txtExcludePatterns_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void txtExcludePatterns_Leave(object sender, EventArgs e)
        {
            this.AcceptButton = this.cmdRunTests;
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

                FillConnectionStringsList();

                var dbPlatforms = Enum.GetValues(typeof(DatabasePlatform));
                foreach (DatabasePlatform dbplat in dbPlatforms)
                {
                    if (dbplat.ToString().ToLower() != "unknown")
                    {
                        this.cboSourceDbPlatform.Items.Add(dbplat.ToString());
                        this.cboDestinationDbPlatform.Items.Add(dbplat.ToString());
                    }
                }
                this.cboSourceDbPlatform.SelectedIndex = 0;
                this.cboDestinationDbPlatform.SelectedIndex = 0;


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }

        }

        private void FillConnectionStringsList()
        {
            _connectionStrings.Add(@"MSSQLServer" + @"|" + @"Data Source=PROFASTWS3; Initial Catalog=AdventureWorksDW2008R2; Integrated Security=True; Application Name=TestprogTableDefs; Workstation ID=PROFASTWS5;");
            _connectionStrings.Add(@"MSSQLServer" + @"|" + @"Data Source=PROFASTSV2; Initial Catalog=AdventureWorksDW2008R2; Integrated Security=True; Application Name=TestprogTableDefs; Workstation ID=PROFASTWS5;");
            _connectionStrings.Add(@"MSSQLServer" + @"|" + @"Data Source=PROFASTSV2; Initial Catalog=AWTest; Integrated Security=True; Application Name=TestprogTableDefs; Workstation ID=PROFASTWS5;");
            _connectionStrings.Add(@"MSSQLServer" + @"|" + @"Data Source=PROFASTWS3; Initial Catalog=Namelists; Integrated Security=True; Application Name=TestprogTableDefs; Workstation ID=PROFASTWS5;");
            _connectionStrings.Add(@"MSOracle" + @"|" + @"Data Source=PROFASTSV4ORA;User ID=SYSTEM;Password=ORA1992;");
            _connectionStrings.Add(@"OracleNative" + @"|" + @"Data Source=PROFASTSV4ORA;User ID=SYSTEM;Password=ORA1992;");
            _connectionStrings.Add(@"DB2" + @"|" + @"Database=SAMPLE;User ID=MIKE;Password=MIKE92;Server=PROFASTSV4DB2:50000;");
            _connectionStrings.Add(@"MySQL" + @"|" + @"server=PROFASTSV4MYSQL; port=3306; database=SAKILA;User Id=Mike; password=MIKE92;");
            _connectionStrings.Add(@"Sybase|Data Source=PROFASTSV2SYB;Port=5000;Database=AdventureWorks;Uid=SA;Pwd=SA1992;");
            _connectionStrings.Add(@"SQLAnywhere" + @"|" + @"UserID=DBA;Password=sql;DatabaseName=AdventureWorks;DatabaseFile=C:\Testdata\SQLAnywhere\AdventureWorks.Db;ServerName=AdventureWorks");
            _connectionStrings.Add(@"SQLAnywhere" + @"|" + @"UserID=DBA;Password=sql;DatabaseFile=C:\Temp\newSQL.Db;ServerName=newSQL");
            _connectionStrings.Add(@"SQLAnywhere" + @"|" + @"UserID=DBA;Password=sql;DatabaseFile=C:\Users\Public\Documents\SQL Anywhere 12\Samples\demo.db;ServerName=demo12");
            _connectionStrings.Add(@"SQLAnywhere" + @"|" + @"UserID=DBA;Password=sql;DatabaseFile=C:\Users\Public\Documents\SQL Anywhere 12\Samples\ultralite\custdb\custdb.db;ServerName=custdb");
            _connectionStrings.Add(@"SQLAnywhereUltraLite" + @"|" + @"nt_file=C:\Testdata\SQLAnywhere\Test1.udb;dbn=Test1;uid=DBA;pwd=sql");
            _connectionStrings.Add(@"SQLServerCE40" + @"|" + @"data source='c:\SQLData\nametestV4.sdf';");
            _connectionStrings.Add(@"SQLServerCE35" + @"|" + @"data source='c:\SQLData\nametestV4_35.sdf';");
            _connectionStrings.Add(@"MSAccess" + @"|" + @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Testdata\Access\AdventureWorksDW.accdb;User Id=admin;Password=;Jet OLEDB:Engine Type=6;");
            _connectionStrings.Add(@"MSAccess" + @"|" + @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Testdata\Access\AdventureWorksDW.mdb;User Id=admin;Password=;Jet OLEDB:Engine Type=5;");

            _connectionStrings.Add(@"ODBC" + @"|" + @"Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=C:\Testdata\Access\AdventureWorksDW.accdb;Uid=Admin;Pwd=;");
            _connectionStrings.Add(@"ODBC" + "|" + @"Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=C:\Testdata\Access\AdventureWorksDW.mdb;Uid=Admin;Pwd=;");
            _connectionStrings.Add(@"ODBC" + "|" + @"Driver={SQL Server Native Client 10.0};Server=profastws3;Database=AdventureWorksDW2008R2;Trusted_Connection=yes;");
            _connectionStrings.Add(@"ODBC" + "|" + @"Driver={SQL Server Native Client 10.0};Server=profastws3;Database=Namelists;Trusted_Connection=yes;");
            _connectionStrings.Add(@"ODBC" + "|" + @"Driver={Oracle in OraClient11g_home1};Dbq=ORASV4;Uid=SYSTEM;Pwd=ORA1992;");
            _connectionStrings.Add(@"ODBC" + "|" + @"Driver={Microsoft ODBC for Oracle};Server=ORASV4;Uid=SYSTEM;Pwd=ORA1992;");
            _connectionStrings.Add(@"ODBC" + "|" + @"Driver={IBM DB2 ODBC DRIVER};Database=sample;Hostname=profastsv4db2;Port=50000;Protocol=TCPIP;Uid=Mike;Pwd=MIKE92;");
            _connectionStrings.Add(@"ODBC" + "|" + @"Driver={MySQL ODBC 5.2w Driver};Server=profastsv4mysql;Database=SAKILA;User=Mike;Password=MIKE92;Option=3;");
            _connectionStrings.Add(@"ODBC" + "|" + @"Driver={Adaptive Server Enterprise};server=profastsv2syb;port=5000;sourceDb=AdventureWorks;uid=SA;Pwd=SA1992;");
            _connectionStrings.Add(@"ODBC" + "|" + @"Driver={SQL Anywhere 12};Uid=DBA;Pwd=sql;DBF=C:\Testdata\SQLAnywhere\AdventureWorks.sourceDb;");

            _connectionStrings.Add(@"OLEDB" + "|" + @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Testdata\Access\AdventureWorksDW.accdb;User Id=admin;Password=;Jet OLEDB:Engine Type=6");
            _connectionStrings.Add(@"OLEDB" + "|" + @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Testdata\Access\AdventureWorksDW.mdb;User Id=admin;Password=;Jet OLEDB:Engine Type=6;");
            _connectionStrings.Add(@"OLEDB" + "|" + @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Testdata\Access\AdventureWorksDW.mdb;User Id=admin;Password=;Jet OLEDB:Engine Type=5;");
            _connectionStrings.Add(@"OLEDB" + "|" + @"Provider=SQLOLEDB;Data Source=PROFASTWS3;Initial Catalog=AdventureWorksDW2008R2;Integrated Security=SSPI;");
            _connectionStrings.Add(@"OLEDB" + "|" + @"Provider=SQLOLEDB;Data Source=PROFASTWS3;Initial Catalog=Namelists;Integrated Security=SSPI;");
            _connectionStrings.Add(@"OLEDB" + "|" + @"Provider=Microsoft.SQLSERVER.CE.OLEDB.3.5;Data Source=C:\SQLData\nametest.sdf;");
            _connectionStrings.Add(@"OLEDB" + "|" + @"Provider=msdaora;Data Source=ORASV4;User Id=SYSTEM;Password=ORA1992;");
            _connectionStrings.Add(@"OLEDB" + "|" + @"Provider=OraOLEDB.Oracle;Data Source=ORASV4;User Id=SYSTEM;Password=ORA1992;");
            _connectionStrings.Add(@"OLEDB" + "|" + @"Provider=IBMDADB2;Database=sample;Hostname=profastsv4db2;Port=50000;Protocol=TCPIP;Uid=Mike;Pwd=MIKE92;");
            _connectionStrings.Add(@"OLEDB" + "|" + @"Provider=ASEOLEDB;Data Source=PROFASTSV2SYB:5000;Initial Catalog=AdventureWorks;User Id=SA;Password=SA1992;");
            _connectionStrings.Add(@"OLEDB" + "|" + @"Provider=SAOLEDB;Data Source=SQLAnywhere_AdventureWorks;Initial Catalog=AdventureWorks;User Id=DBA;Password=sql;");


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

                if (this.chkGetTabDefList.Checked)
                {
                    nNumTestsSelected++;
                    Tests.GetTabDefList(this);
                }

                if (this.chkConvertTableDefs.Checked)
                {
                    nNumTestsSelected++;
                    Tests.ConvertTableDefs(this);
                }

                if (this.chkGetSupportedDatabaseList.Checked)
                {
                    nNumTestsSelected++;
                    Tests.GetSupportedDatabasesList(this);
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

        private void GetSourceDatabasePlatformConnectionStrings()
        {
            this.cboSourceDbConnectionString.Items.Clear();
            foreach (string connStr in _connectionStrings)
            {
                string[] parsedStr = connStr.Split('|');
                if (parsedStr[0].ToUpper() == this.cboSourceDbPlatform.Text.ToUpper())
                {
                    this.cboSourceDbConnectionString.Items.Add(parsedStr[1]);
                }
            }
            if(cboSourceDbConnectionString.Items.Count > 0)
                this.cboSourceDbConnectionString.SelectedIndex = 0;
        }

        private void GetDestinationDatabasePlatformConnectionStrings()
        {
            this.cboDestinationDbConnectionString.Items.Clear();
            foreach (string connStr in _connectionStrings)
            {
                string[] parsedStr = connStr.Split('|');
                if (parsedStr[0].ToUpper() == this.cboDestinationDbPlatform.Text.ToUpper())
                {
                    this.cboDestinationDbConnectionString.Items.Add(parsedStr[1]);
                }
            }
            if (cboDestinationDbConnectionString.Items.Count > 0)
                this.cboDestinationDbConnectionString.SelectedIndex = 0;
        }

    }//end class
}//end namespace
