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

namespace TestprogWinFTP
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

        private void cmdRunTest_Click(object sender, EventArgs e)
        {
            RunTests();
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

                this.chkEraseOutputBeforeEachTest.Checked = true;



                this.txtFtpHost.Text = AppConfig.GetStringValueFromConfigFile("FtpServer", "profastws2");
                this.txtFtpPort.Text = AppConfig.GetStringValueFromConfigFile("FtpPort", "21");
                this.txtFtpUsername.Text = AppConfig.GetStringValueFromConfigFile("FtpUsername", "_ftp");
                this.txtFtpPassword.Text = AppConfig.GetStringValueFromConfigFile("FtpPassword", "_ftp");
                this.chkUseSSL.Checked = AppConfig.GetBooleanValueFromConfigFile("UseSSL", "false");
                this.chkUseBinaryMode.Checked = AppConfig.GetBooleanValueFromConfigFile("UseBinaryMode", "true");
                this.txtBufferSize.Text = AppConfig.GetStringValueFromConfigFile("BufferSize", "32767");
                this.txtLocalSourceFile.Text = AppConfig.GetStringValueFromConfigFile("LocalSourceFile", @"c:\temp\Test1s.txt");
                this.txtRemoteFile.Text = AppConfig.GetStringValueFromConfigFile("RemoteFile", @"/FTPTest/Test1s.txt");
                this.txtRenameTo.Text = AppConfig.GetStringValueFromConfigFile("RenameTo", @"Test1sRENAMED.txt");
                this.txtLocalDestinationFile.Text = AppConfig.GetStringValueFromConfigFile("LocalDestinationFile", @"c:\temp\Testdownload1.txt");

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

                if (this.chkGetStaticKeysTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.GetStaticKeys(this);
                }

                if (this.chkUploadFileTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.UploadFileTest(this);
                }

                if (this.chkDownloadFileTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.DownloadFileTest(this);
                }

                if (this.chkGetRemoteFileInfoTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.GetRemoteFileInfoTest(this);
                }

                if (this.chkRenameRemoteFileTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.RenameRemoteFileTest(this);
                }

                if (this.chkDeleteRemoteFileTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.DeleteRemoteFileTest(this);
                }

                if (this.chkListRemoveFolderFilesTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.ListRemoteFolderFilesTest(this);
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


    }//end class
}//end namespace
