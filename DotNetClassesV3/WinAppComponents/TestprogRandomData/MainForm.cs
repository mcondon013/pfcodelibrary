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

namespace TestprogRandomData
{
    public partial class MainForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";

        PFAppProcessor _appProcessor = new PFAppProcessor();

        private string _defaultFolderContainingNamesFiles = System.IO.Path.GetFullPath(@"..\..\..\..\..\WordLists\Top100NameLists\");
        private string _defaultFolderContainingLocationsFiles = System.IO.Path.GetFullPath(@"..\..\..\..\..\WordLists\TopLocationLists\");

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

        private void cmdGetCustomFirstNamesFemaleFile_Click(object sender, EventArgs e)
        {
            GetCustomFirstNamesFemaleFile();
        }

        private void cmdGetCustomFirstNamesMaleFile_Click(object sender, EventArgs e)
        {
            GetCustomFirstNamesMaleFile();
        }

        private void cmdGetCustomLastNamesFile_Click(object sender, EventArgs e)
        {
            GetCustomLastNamesFile();
        }

        private void cmdGetCustomValuesFile_Click(object sender, EventArgs e)
        {
            GetCustomValuesFile();
        }

        private void cmdGetCustomLocationFile_Click(object sender, EventArgs e)
        {
            GetCustomLocationFile();
        }

        private void cmdShowHideLog_Click(object sender, EventArgs e)
        {
            ShowHideLog();
        }

        //Menu item clicks
        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void optBooleanOutput_CheckedChanged(object sender, EventArgs e)
        {
            BooleanOutputCheckedChanged();
        }


        //Form Routines
        private void CloseForm()
        {
            this.Close();
        }

        private void BooleanOutputCheckedChanged()
        {
            if (this.optBoolean.Checked)
            {
                this.txtTrueVal.Text = string.Empty;
                this.txtFalseVal.Text = string.Empty;
                this.txtTrueVal.Enabled = false;
                this.txtFalseVal.Enabled = false;
            }
            else if (this.optInteger.Checked)
            {
                this.txtTrueVal.Text = "1";
                this.txtTrueVal.Enabled = true;
                this.txtFalseVal.Enabled = true;
                this.txtFalseVal.Text = "0";
            }
            else if (this.optString.Checked)
            {
                this.txtTrueVal.Text = "true";
                this.txtFalseVal.Text = "false";
                this.txtTrueVal.Enabled = true;
                this.txtFalseVal.Enabled = true;
            }
            else
            {
                this.optBoolean.Checked = true;
                this.txtTrueVal.Text = string.Empty;
                this.txtFalseVal.Text = string.Empty;
                this.txtTrueVal.Enabled = false;
                this.txtFalseVal.Enabled = false;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Properties.Settings.Default.SaveFormLocationsOnExit)
                SaveScreenLocations();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string configValue = string.Empty;

            try
            {
                this.Text = AppInfo.AssemblyProduct;

                SetFormLocationAndSize();

                SetLoggingValues();

                SetFormValues();

                InitAppProcessor();

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }

        }

        internal void SetFormLocationAndSize()
        {
            if (Properties.Settings.Default.MainFormX != 0 || Properties.Settings.Default.MainFormY != 0)
            {
                this.Location = new Point(Properties.Settings.Default.MainFormX, Properties.Settings.Default.MainFormY);
            }

            if (Properties.Settings.Default.MessageLogX != 0
                || Properties.Settings.Default.MessageLogY != 0)
            {
                Program._messageLog.Form.Location = new Point(Properties.Settings.Default.MessageLogX, Properties.Settings.Default.MessageLogY);
            }

            if ((Properties.Settings.Default.MessageLogWidth != Properties.Settings.Default.MessageLogDefaultWidth)
                || (Properties.Settings.Default.MessageLogHeight != Properties.Settings.Default.MessageLogDefaultHeight))
            {
                if (Properties.Settings.Default.MessageLogWidth > 0 && Properties.Settings.Default.MessageLogHeight > 0)
                {
                    Program._messageLog.Form.Width = Properties.Settings.Default.MessageLogWidth;
                    Program._messageLog.Form.Height = Properties.Settings.Default.MessageLogHeight;
                }
            }

        }

        internal void SetLoggingValues()
        {
            string configValue = string.Empty;

            configValue = AppGlobals.AppConfig.GetConfigValue("SaveErrorMessagesToErrorLog");
            if (configValue.ToUpper() == "TRUE")
                _saveErrorMessagesToAppLog = true;
            else
                _saveErrorMessagesToAppLog = false;
            _appLogFileName = AppGlobals.AppConfig.GetConfigValue("AppLogFileName");

            if (_appLogFileName.Trim().Length > 0)
                AppGlobals.AppMessages.AppLogFilename = _appLogFileName;

            _appProcessor.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
        }

        private void SetFormValues()
        {
            string configValue = string.Empty;

            this.chkEraseOutputBeforeEachTest.Checked = true;
            this.txtNumOutputValues.Text = "10";

            configValue = Properties.Settings.Default.DefaultCustomRandomNamesFolder;
            if (configValue.Trim().Length > 0)
            {
                _defaultFolderContainingNamesFiles = configValue;
            }
            configValue = Properties.Settings.Default.DefaultFolderContainingLocationsFiles;
            if (configValue.Trim().Length > 0)
            {
                _defaultFolderContainingLocationsFiles = configValue;
            }


            this.cboNameLocation.Text = this.cboNameLocation.Items[0].ToString();

            this.txtFromDate.Text = "01/01/2000";
            this.txtToDate.Text = "01/01/2009";
            this.txtFromTime.Text = "02:00:00.00";
            this.txtToTime.Text = "22:59:59.99";
            this.chkShowResultAsInteger.Checked = false;

            this.optYears.Checked = true;
            this.txtDateToModify.Text = "1/1/2010 00:00:00";
            this.txtMinOffset.Text = "-10";
            this.txtMaxOffset.Text = "10";

            this.cboNumberType.Items.Clear();
            foreach (string s in Enum.GetNames(typeof(PFRandomData.enRandomNumberType)))
            {
                if(s != "enUnknown")
                    this.cboNumberType.Items.Add(s);
            }
            this.cboNumberType.Text = this.cboNumberType.Items[0].ToString();
            this.txtMinNum.Text = "1";
            this.txtMaxNum.Text = "1000";

            this.txtNumToModify.Text = "100";
            this.txtMinOffsetPercent.Text = "-0.50";
            this.txtMaxOffsetPercent.Text = "+0.50";

            this.cboLocation.Text = this.cboLocation.Items[0].ToString();
            this.cboLocType.Text = this.cboLocType.Items[0].ToString();
            this.txtCustomLocationFile.Text = System.IO.Path.GetFullPath(@"..\..\..\..\..\WordLists\TopLocationLists\Mexico_NeighborhoodNames.txt");

            this.txtTrueVal.Text = string.Empty;
            this.txtFalseVal.Text = string.Empty;
            this.optBoolean.Checked = true;
            this.txtNumTrue.Text = "50";
            this.txtNumFalse.Text = "50";
        }
        
        private void InitAppProcessor()
        {
            _appProcessor.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
            _appProcessor.MessageLogUI = Program._messageLog;
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

        private void ShowHideLog()
        {
            if (Program._messageLog.FormIsVisible)
            {
                Program._messageLog.HideWindow();
            }
            else
            {
                Program._messageLog.ShowWindow();
            }
        }

        //application routines
        private void RunTests()
        {

            int nNumTestsSelected = 0;

            try
            {
                DisableFormControls();
                _appProcessor.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
                this.Cursor = Cursors.WaitCursor;

                if (this.chkEraseOutputBeforeEachTest.Checked)
                {
                    Program._messageLog.Clear();
                }

                if (this.chkRandomNamesTest.Checked)
                {
                    nNumTestsSelected++;
                    _appProcessor.RandomNamesTest(this.cboNameLocation.Text, this.txtNumOutputValues.Text);
                }

                if (this.chkCustomRandomNamesTest.Checked)
                {
                    nNumTestsSelected++;
                    _appProcessor.CustomRandomNamesTest(this.txtCustomFirstNamesFemaleFile.Text, this.txtCustomFirstNamesMaleFile .Text, this.txtCustomLastNamesFile.Text,  this.txtNumOutputValues.Text);
                }

                if (this.chkCustomRandomValuesTest.Checked)
                {
                    nNumTestsSelected++;
                    _appProcessor.CustomRandomValuesTest(this.txtCustomValuesFile.Text, this.txtNumOutputValues.Text);
                }

                if (this.chkRandomBusinessNamesTest.Checked)
                {
                    nNumTestsSelected++;
                    _appProcessor.RandomBusinessNamesTest(this.cboNameLocation.Text, this.txtNumOutputValues.Text);
                }

                if (this.chkRandomDateTimeTest.Checked)
                {
                    nNumTestsSelected++;
                    _appProcessor.RandomDateTimeTest(this.txtFromDate.Text, this.txtToDate.Text, this.txtFromTime.Text, this.txtToTime.Text, this.chkShowResultAsInteger.Checked, this.txtNumOutputValues.Text, this.chkGenerateResultsArray.Checked);
                }

                if (this.chkRandomDateTest.Checked)
                {
                    nNumTestsSelected++;
                    _appProcessor.RandomDateTest(this.txtFromDate.Text, this.txtToDate.Text, this.chkShowResultAsInteger.Checked, this.txtNumOutputValues.Text, this.chkGenerateResultsArray.Checked);
                }

                if (this.chkRandomTimeTest.Checked)
                {
                    nNumTestsSelected++;
                    _appProcessor.RandomTimeTest(this.txtFromTime.Text, this.txtToTime.Text, this.chkShowResultAsInteger.Checked, this.txtNumOutputValues.Text, this.chkGenerateResultsArray.Checked);
                }

                if (this.chkDateTimeOffsetTest.Checked)
                {
                    nNumTestsSelected++;
                    RadioButton rbutton = grpOffsetInterval.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked);  //linq query to get selected radio button
                    _appProcessor.DateTimeOffsetTest(this.txtDateToModify.Text, rbutton, this.txtMinOffset.Text, this.txtMaxOffset.Text, this.chkShowResultAsInteger.Checked, this.txtNumOutputValues.Text, this.chkGenerateResultsArray.Checked);
                }

                if (this.chkDateOffsetTest.Checked)
                {
                    nNumTestsSelected++;
                    RadioButton rbutton = grpOffsetInterval.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked);  //linq query to get selected radio button
                    _appProcessor.DateOffsetTest(this.txtDateToModify.Text, rbutton, this.txtMinOffset.Text, this.txtMaxOffset.Text, this.chkShowResultAsInteger.Checked, this.txtNumOutputValues.Text, this.chkGenerateResultsArray.Checked);
                }

                if (this.chkTimeOffsetTest.Checked)
                {
                    nNumTestsSelected++;
                    RadioButton rbutton = grpOffsetInterval.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked);  //linq query to get selected radio button
                    _appProcessor.TimeOffsetTest(this.txtDateToModify.Text, rbutton, this.txtMinOffset.Text, this.txtMaxOffset.Text, this.chkShowResultAsInteger.Checked, this.txtNumOutputValues.Text, this.chkGenerateResultsArray.Checked);
                }

                if (this.chkRandomNumberTest.Checked)
                {
                    nNumTestsSelected++;
                    PFRandomData.enRandomNumberType numType =  (PFRandomData.enRandomNumberType)Enum.Parse(typeof(PFRandomData.enRandomNumberType),this.cboNumberType.Text);
                    _appProcessor.RandomNumberTest(numType, this.txtMinNum.Text, this.txtMaxNum.Text, this.txtNumOutputValues.Text, this.chkGenerateResultsArray.Checked);
                }

                if (this.chkRandomOffsetTest.Checked)
                {
                    nNumTestsSelected++;
                    PFRandomData.enRandomNumberType numType = (PFRandomData.enRandomNumberType)Enum.Parse(typeof(PFRandomData.enRandomNumberType), this.cboNumberType.Text);
                    _appProcessor.RandomOffsetTest(numType, this.txtNumToModify.Text, this.txtMinOffsetPercent.Text, this.txtMaxOffsetPercent.Text,this.txtNumOutputValues.Text, this.chkGenerateResultsArray.Checked);
                }

                if (this.chkRandomLocationTest.Checked)
                {
                    nNumTestsSelected++;
                    _appProcessor.RandomLocationTest(this.cboLocation.Text, this.txtNumOutputValues.Text);
                }

                if (this.chkCustomLocationTest.Checked)
                {
                    nNumTestsSelected++;
                    _appProcessor.CustomLocationTest(this.cboLocation.Text, this.cboLocType.Text, this.txtCustomLocationFile.Text, this.txtNumOutputValues.Text);
                }

                if (this.chkRandomBooleanTest.Checked)
                {
                    nNumTestsSelected++;
                    RadioButton rbutton = grpBooleanOutput.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked);  //linq query to get selected radio button
                    _appProcessor.RandomBooleanTest(this.txtNumTrue.Text, this.txtNumFalse.Text, rbutton, this.txtTrueVal.Text, this.txtFalseVal.Text, this.txtNumOutputValues.Text);
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
                this.Cursor = Cursors.Default;
                _msg.Length = 0;
                _msg.Append("\r\n");
                _msg.Append("Number of tests run: ");
                _msg.Append(nNumTestsSelected.ToString("#,##0"));
                Program._messageLog.WriteLine(_msg.ToString());
            }

        }


        private void SaveScreenLocations()
        {
            Properties.Settings.Default["MessageLogX"] = Program._messageLog.Form.Location.X;
            Properties.Settings.Default["MessageLogY"] = Program._messageLog.Form.Location.Y;
            Properties.Settings.Default["MessageLogWidth"] = Program._messageLog.Form.Width;
            Properties.Settings.Default["MessageLogHeight"] = Program._messageLog.Form.Height;

            Properties.Settings.Default["MainFormX"] = this.Location.X;
            Properties.Settings.Default["MainFormY"] = this.Location.Y;
            Properties.Settings.Default.Save();
        }

        private void RestoreDefaultScreenLocations()
        {
            Program._messageLog.Form.Location = new Point(0, 0);
            Program._messageLog.Form.Width = Properties.Settings.Default.MessageLogDefaultWidth;
            Program._messageLog.Form.Height = Properties.Settings.Default.MessageLogDefaultHeight;
            Program._messageLog.Form.Refresh();

            Properties.Settings.Default["MessageLogX"] = Program._messageLog.Form.Location.X;
            Properties.Settings.Default["MessageLogY"] = Program._messageLog.Form.Location.Y;
            Properties.Settings.Default["MessageLogWidth"] = Program._messageLog.Form.Width;
            Properties.Settings.Default["MessageLogHeight"] = Program._messageLog.Form.Height;

            this.CenterToScreen();
            Properties.Settings.Default["MainFormX"] = 0;
            Properties.Settings.Default["MainFormY"] = 0;
            Properties.Settings.Default.Save();
        }

        private void GetCustomFirstNamesFemaleFile()
        {

            try
            {
                if (this.txtCustomFirstNamesFemaleFile.Text.Trim().Length == 0)
                {
                    _saveSelectionsFolder = _defaultFolderContainingNamesFiles;
                    _saveSelectionsFile = string.Empty;
                }
                else
                {
                    _saveSelectionsFolder = Path.GetDirectoryName(this.txtCustomFirstNamesFemaleFile.Text);
                    _saveSelectionsFile = Path.GetFileName(this.txtCustomFirstNamesFemaleFile.Text);
                }

                DialogResult res = ShowOpenFileDialog();

                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    this.txtCustomFirstNamesFemaleFile.Text = _saveSelectionsFile;
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
                ;
            }
                 
        
        
        }


        private void GetCustomFirstNamesMaleFile()
        {

            try
            {
                if (this.txtCustomFirstNamesMaleFile.Text.Trim().Length == 0)
                {
                    _saveSelectionsFolder = _defaultFolderContainingNamesFiles;
                    _saveSelectionsFile = string.Empty;
                }
                else
                {
                    _saveSelectionsFolder = Path.GetDirectoryName(this.txtCustomFirstNamesMaleFile.Text);
                    _saveSelectionsFile = Path.GetFileName(this.txtCustomFirstNamesMaleFile.Text);
                }

                DialogResult res = ShowOpenFileDialog();

                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    this.txtCustomFirstNamesMaleFile.Text = _saveSelectionsFile;
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
                ;
            }

        }

        private void GetCustomLastNamesFile()
        {

            try
            {
                if (this.txtCustomLastNamesFile.Text.Trim().Length == 0)
                {
                    _saveSelectionsFolder = _defaultFolderContainingNamesFiles;
                    _saveSelectionsFile = string.Empty;
                }
                else
                {
                    _saveSelectionsFolder = Path.GetDirectoryName(this.txtCustomLastNamesFile.Text);
                    _saveSelectionsFile = Path.GetFileName(this.txtCustomLastNamesFile.Text);
                }

                DialogResult res = ShowOpenFileDialog();

                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    this.txtCustomLastNamesFile.Text = _saveSelectionsFile;
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
                ;
            }

        }

        private void GetCustomValuesFile()
        {

            try
            {
                if (this.txtCustomValuesFile.Text.Trim().Length == 0)
                {
                    _saveSelectionsFolder = _defaultFolderContainingNamesFiles;
                    _saveSelectionsFile = string.Empty;
                }
                else
                {
                    _saveSelectionsFolder = Path.GetDirectoryName(this.txtCustomValuesFile.Text);
                    _saveSelectionsFile = Path.GetFileName(this.txtCustomValuesFile.Text);
                }

                DialogResult res = ShowOpenFileDialog();

                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    this.txtCustomValuesFile.Text = _saveSelectionsFile;
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
                ;
            }

        }


        private void GetCustomLocationFile()
        {

            try
            {
                if (this.txtCustomLocationFile.Text.Trim().Length == 0)
                {
                    _saveSelectionsFolder = _defaultFolderContainingLocationsFiles;
                    _saveSelectionsFile = string.Empty;
                }
                else
                {
                    _saveSelectionsFolder = Path.GetDirectoryName(this.txtCustomLocationFile.Text);
                    _saveSelectionsFile = Path.GetFileName(this.txtCustomLocationFile.Text);
                }

                DialogResult res = ShowOpenFileDialog();

                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    this.txtCustomLocationFile.Text = _saveSelectionsFile;
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
                ;
            }

        }

    }//end class
}//end namespace
