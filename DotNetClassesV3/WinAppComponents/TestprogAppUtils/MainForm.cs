using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppGlobals;
using PFAppUtils;
using PFTextObjects;
using System.IO;

namespace TestprogAppUtils
{
    public partial class MainForm : Form
    {
        StringBuilder _msg = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";

        //fields for Mru processing
        MruStripMenu _msm;
        private bool _saveMruListToRegistry = true;
        private string _mRUListSaveFileSubFolder = @"PFApps\InitWinFormsAppWithToolbar\Mru\";
        private string _mRUListSaveRegistryKey = @"SOFTWARE\PFApps\InitWinFormsAppWithToolbar";
        private int _maxMruListEntries = 4;
        private bool _useSubMenuForMruList = true;
        private int _maxShortenPathLength = 96;

        //file and folder dialog fields
        private PFOpenFileDialog _openFileDialog = new PFOpenFileDialog();
        private PFSaveFileDialog _saveFileDialog = new PFSaveFileDialog();
        private PFFolderBrowserDialog _folderBrowserDialog = new PFFolderBrowserDialog();

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

        //menu clicks
        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuFileToggleRecentFiles_Click(object sender, EventArgs e)
        {
            ToggleRecentFiles();
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
                this.chkMultiSelect.Checked = false;
                this.chkCreatePrompt.Checked = false;
                this.chkOverwritePrompt.Checked = true;
                this.chkNewFolderButton.Checked = true;
                //this.txtInitialDirectory.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                this.txtInitialDirectory.Text = @"c:\temp";
                this.txtFilter.Text  = "Text Files|*.txt|Xml Files|*.xml|Schema Files|*.xsd|All Files|*.*";
                this.txtFilterIndex.Text = "1";

                this.txtRootFolderPath.Text = @"C:\"; 

                _saveMruListToRegistry = AppConfig.GetBooleanValueFromConfigFile("SaveMruListToRegistry", "True");
                _mRUListSaveFileSubFolder = AppConfig.GetStringValueFromConfigFile("MRUListSaveFileSubFolder", @"PFApps\InitWinFormsAppWithToolbar\Mru\");
                _mRUListSaveRegistryKey = AppConfig.GetStringValueFromConfigFile("MRUListSaveRegistryKey", @"SOFTWARE\PFApps\InitWinFormsAppWithToolbar");
                _maxMruListEntries = AppConfig.GetIntValueFromConfigFile("MaxMruListEntries", (int)4);
                _useSubMenuForMruList = AppConfig.GetBooleanValueFromConfigFile("UseSubMenuForMruList", "true");
                _maxShortenPathLength = AppConfig.GetIntValueFromConfigFile("MaxShortenPathLength", 96);

                if (_saveMruListToRegistry)
                {
                    if (_useSubMenuForMruList)
                    {
                        _msm = new MruStripMenu(mnuFileRecent, new MruStripMenu.ClickedHandler(OnMruFile), _mRUListSaveRegistryKey + "\\MRU", false, _maxMruListEntries);
                    }
                    else
                    {
                        //use inline
                        _msm = new MruStripMenuInline(mnuFile, mnuFileRecent, new MruStripMenu.ClickedHandler(OnMruFile), _mRUListSaveRegistryKey + "\\MRU", _maxMruListEntries);
                    }
                    _msm.MaxShortenPathLength = _maxShortenPathLength;
                    _msm.LoadFromRegistry();
                }
                else
                {
                    //load from and save to the file system
                    if (_useSubMenuForMruList)
                    {
                        _msm = new MruStripMenu(mnuFileRecent, new MruStripMenu.ClickedHandler(OnMruFile), _maxMruListEntries);
                    }
                    else
                    {
                        //use inline
                        _msm = new MruStripMenuInline(mnuFile, mnuFileRecent, new MruStripMenu.ClickedHandler(OnMruFile), _maxMruListEntries);
                    }
                    _msm.MaxShortenPathLength = _maxShortenPathLength;
                    _msm.FileSystemMruPath = _mRUListSaveFileSubFolder;
                    _msm.LoadFromFileSystem();
                }


                EnableFormControls();

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }

        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_msm != null)
            {
                if (_saveMruListToRegistry)
                {
                    _msm.SaveToRegistry();
                }
                else
                {
                    _msm.SaveToFileSystem();
                }
            }
            else
            {
                //do not save
                ;
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

                if (this.chkShowOpenFileDialog.Checked)
                {
                    nNumTestsSelected++;
                    ShowOpenFileDialog();
                }

                if (this.chkShowSaveFileDialog.Checked)
                {
                    nNumTestsSelected++;
                    ShowSaveFileDialog();
                }

                if (this.chkShowFolderBrowserDialog.Checked)
                {
                    nNumTestsSelected++;
                    ShowFolderBrowserDialog();
                }

                if (this.chkRemoveAllMruItems.Checked)
                {
                    nNumTestsSelected++;
                    RemoveAllMruItems();
                }

                if (this.chkShowNamelistPrompt.Checked)
                {
                    nNumTestsSelected++;
                    Tests.ShowNamelistPrompt();
                }

                if (this.chkShowTreeViewFolderBrowser.Checked)
                {
                    nNumTestsSelected++;
                    Tests.ShowTreeViewFolderBrowser(this);
                }

                if (this.chkShowTreeViewFolderBrowserExt.Checked)
                {
                    nNumTestsSelected++;
                    Tests.ShowTreeViewFolderBrowserExt(this);
                }

                if (this.chkPFClassWriterTest.Checked)
                {
                    nNumTestsSelected++;
                    Tests.PFClassWriterTest(this);
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


        private DialogResult ShowOpenFileDialog()
        {
            DialogResult res = DialogResult.None;
            _openFileDialog.InitialDirectory = this.txtInitialDirectory.Text;
            _openFileDialog.FileName = string.Empty;
            _openFileDialog.Filter = this.txtFilter.Text;
            _openFileDialog.FilterIndex = PFTextProcessor.ConvertStringToInt(this.txtFilterIndex.Text ,1);
            _openFileDialog.MultiSelect = this.chkMultiSelect.Checked;
            res = _openFileDialog.ShowOpenFileDialog();
            if (res == DialogResult.OK)
            {
                this.txtInitialDirectory.Text = _openFileDialog.InitialDirectory;
                this.txtFilterIndex.Text = _openFileDialog.FilterIndex.ToString();
                _msg.Length = 0;
                if (chkMultiSelect.Checked)
                {
                    _msg.Append("Open File names: \r\n");
                    if (_openFileDialog.FileNames != null)
                    {
                        for (int i = 0; i < _openFileDialog.FileNames.Length; i++)
                        {
                            _msg.Append(_openFileDialog.FileNames[i]);
                            _msg.Append("\r\n");
                        }
                    }
                    else
                    {
                        _msg.Append("<Null>");
                    }

                }
                else
                {
                    _msg.Append("Open File name: \r\n");
                    _msg.Append(_openFileDialog.FileName);
                }
                Program._messageLog.WriteLine(_msg.ToString());

                if (_openFileDialog.MultiSelect)
                {
                    if (_openFileDialog.FileNames != null)
                    {
                        for (int i = 0; i < _openFileDialog.FileNames.Length; i++)
                        {
                            UpdateMruList(_openFileDialog.FileNames[i]);
                        }
                    }
                    else
                    {
                        ;
                    }
                }
                else
                {
                    UpdateMruList(_openFileDialog.FileName);
                }
                _msg.Length = 0;
                _msg.Append("ToXmlString:\r\n");
                _msg.Append(_openFileDialog.ToXmlString());
                _msg.Append("\r\n");
                _msg.Append("ToXmlDocument:\r\n");
                _msg.Append(_openFileDialog.ToXmlDocument().OuterXml);
                Program._messageLog.WriteLine(_msg.ToString());

                string xmlFileName = @"c:\temp\Dialog.xml";
                _openFileDialog.SaveToXmlFile(xmlFileName);
                PFOpenFileDialog diag2 = PFOpenFileDialog.LoadFromXmlFile(xmlFileName);
                _msg.Length = 0;
                _msg.Append("\r\nXmlFileName: ");
                _msg.Append(xmlFileName);
                _msg.Append("\r\n");
                _msg.Append("\r\ndiag2 ToXmlString:\r\n");
                _msg.Append(diag2.ToXmlString());
                _msg.Append("\r\n");
                _msg.Append("\r\ndiag2 ToXmlDocument:\r\n");
                _msg.Append(diag2.ToXmlDocument().OuterXml);
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("\r\nToString: ");
                _msg.Append(_openFileDialog.ToString());
                Program._messageLog.WriteLine(_msg.ToString());

            }
            else
            {
                _msg.Length = 0;
                _msg.Append("Dialog result is ");
                _msg.Append(res.ToString());
                Program._messageLog.WriteLine(_msg.ToString());
            }

            return res;
        }

        private void UpdateMruList(string filename)
        {
            try
            {
                _msm.AddFile(filename);
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
                ;
            }

        }


        private void OnMruFile(int number, String filename)
        {
            _msg.Length = 0;
            _msg.Append("File # ");
            _msg.Append(number.ToString("#,##0"));
            _msg.Append(" selected from Mru List: ");
            _msg.Append(filename);
            _msg.Append(".\r\n");

            //TODO:
            //if file exists, process it
            //if not, show error message and remove from list
            if (File.Exists(filename))
            {
                //process it
                _msg.Append("File exists. It will be processed.");
                AppMessages.DisplayInfoMessage(_msg.ToString());
            }
            else
            {
                _msg.Append("File does not exist. It will be removed from the Mru List");
                _msm.RemoveFile(number);
                AppMessages.DisplayWarningMessage(_msg.ToString());
            }

        }


        private DialogResult ShowSaveFileDialog()
        {
            DialogResult res = DialogResult.None;
            string filename = string.Empty;
            _saveFileDialog.InitialDirectory = this.txtInitialDirectory.Text;
            _saveFileDialog.FileName = string.Empty;
            _saveFileDialog.Filter = this.txtFilter.Text;
            _saveFileDialog.FilterIndex = PFTextProcessor.ConvertStringToInt(this.txtFilterIndex.Text, 1);
            _saveFileDialog.ShowCreatePrompt = this.chkCreatePrompt.Checked;
            _saveFileDialog.ShowOverwritePrompt = this.chkOverwritePrompt.Checked;

            res = _saveFileDialog.ShowSaveFileDialog();

            _msg.Length = 0;
            if (res == DialogResult.OK)
            {
                this.txtInitialDirectory.Text = _saveFileDialog.InitialDirectory;
                this.txtFilterIndex.Text = _saveFileDialog.FilterIndex.ToString();
                UpdateMruList(_saveFileDialog.FileName);

                _msg.Append("Save File name: \r\n");
                _msg.Append(_saveFileDialog.FileName);
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("ToXmlString:\r\n");
                _msg.Append(_saveFileDialog.ToXmlString());
                _msg.Append("\r\n");
                _msg.Append("ToXmlDocument:\r\n");
                _msg.Append(_saveFileDialog.ToXmlDocument().OuterXml);
                Program._messageLog.WriteLine(_msg.ToString());

                string xmlFileName = @"c:\temp\Dialog.xml";
                _saveFileDialog.SaveToXmlFile(xmlFileName);
                PFSaveFileDialog diag2 = PFSaveFileDialog.LoadFromXmlFile(xmlFileName);
                _msg.Length = 0;
                _msg.Append("\r\nXmlFileName: ");
                _msg.Append(xmlFileName);
                _msg.Append("\r\n");
                _msg.Append("\r\ndiag2 ToXmlString:\r\n");
                _msg.Append(diag2.ToXmlString());
                _msg.Append("\r\n");
                _msg.Append("\r\ndiag2 ToXmlDocument:\r\n");
                _msg.Append(diag2.ToXmlDocument().OuterXml);
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("\r\nToString: ");
                _msg.Append(_saveFileDialog.ToString());
                Program._messageLog.WriteLine(_msg.ToString());

            }
            else
            {
                _msg.Append("SaveFileDiaglog result:\r\n");
                _msg.Append(res.ToString());
                Program._messageLog.WriteLine(_msg.ToString());
            }

            return res;
        }


        private DialogResult ShowFolderBrowserDialog()
        {
            DialogResult res = DialogResult.None;

            _folderBrowserDialog.InitialFolderPath = this.txtInitialDirectory.Text;
            _folderBrowserDialog.ShowNewFolderButton = true;

            res = _folderBrowserDialog.ShowFolderBrowserDialog();

            if (res != DialogResult.Cancel)
            {
                this.txtInitialDirectory.Text = _folderBrowserDialog.InitialFolderPath;
                _msg.Length = 0;
                _msg.Append("SelectedFolderPath:\r\n");
                _msg.Append(_folderBrowserDialog.SelectedFolderPath);
                Program._messageLog.WriteLine(_msg.ToString());
                
                _msg.Length = 0;
                _msg.Append("ToXmlString:\r\n");
                _msg.Append(_folderBrowserDialog.ToXmlString());
                _msg.Append("\r\n");
                _msg.Append("ToXmlDocument:\r\n");
                _msg.Append(_folderBrowserDialog.ToXmlDocument().OuterXml);
                Program._messageLog.WriteLine(_msg.ToString());

                string xmlFileName = @"c:\temp\Dialog.xml";
                _folderBrowserDialog.SaveToXmlFile(xmlFileName);
                PFFolderBrowserDialog diag2 = PFFolderBrowserDialog.LoadFromXmlFile(xmlFileName);
                _msg.Length = 0;
                _msg.Append("\r\nXmlFileName: ");
                _msg.Append(xmlFileName);
                _msg.Append("\r\n");
                _msg.Append("\r\ndiag2 ToXmlString:\r\n");
                _msg.Append(diag2.ToXmlString());
                _msg.Append("\r\n");
                _msg.Append("\r\ndiag2 ToXmlDocument:\r\n");
                _msg.Append(diag2.ToXmlDocument().OuterXml);
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("\r\nToString: ");
                _msg.Append(_folderBrowserDialog.ToString());
                Program._messageLog.WriteLine(_msg.ToString());

            }
            else
            {
                _msg.Length = 0;
                _msg.Append("FolderBrowserDiaglog result:\r\n");
                _msg.Append(res.ToString());
                Program._messageLog.WriteLine(_msg.ToString());
            }
            

            return res;
        }


        private void RemoveAllMruItems()
        {
            _msm.RemoveAll();
            Program._messageLog.WriteLine("All Mru Items removed.");
        }

        private void ToggleRecentFiles()
        {
            if (_saveMruListToRegistry)
            {
                _msm.SaveToRegistry();
            }
            else
            {
                _msm.SaveToFileSystem();
            }

            _msm.RemoveAll();
            _msm = null;


            if (_saveMruListToRegistry)
            {
                if (_useSubMenuForMruList)
                {
                    _useSubMenuForMruList = false;
                    //use inline
                    _msm = new MruStripMenuInline(mnuFile, mnuFileRecent, new MruStripMenu.ClickedHandler(OnMruFile), _mRUListSaveRegistryKey + "\\MRU", _maxMruListEntries);
                }
                else
                {
                    _useSubMenuForMruList = true;
                    _msm = new MruStripMenu(mnuFileRecent, new MruStripMenu.ClickedHandler(OnMruFile), _mRUListSaveRegistryKey + "\\MRU", false, _maxMruListEntries);
                }
                _msm.MaxShortenPathLength = _maxShortenPathLength;
                _msm.LoadFromRegistry();
            }
            else
            {
                //load from and save to the file system
                if (_useSubMenuForMruList)
                {
                    _useSubMenuForMruList = false;
                    //use inline
                    _msm = new MruStripMenuInline(mnuFile, mnuFileRecent, new MruStripMenu.ClickedHandler(OnMruFile), _maxMruListEntries);
                }
                else
                {
                    _useSubMenuForMruList = true;
                    _msm = new MruStripMenu(mnuFileRecent, new MruStripMenu.ClickedHandler(OnMruFile), _maxMruListEntries);
                }
                _msm.MaxShortenPathLength = _maxShortenPathLength;
                _msm.FileSystemMruPath = _mRUListSaveFileSubFolder;
                _msm.LoadFromFileSystem();
            }




        }


    }//end class
}//end namespace
