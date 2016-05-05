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

namespace TestprogRandomWords
{
    public partial class MainForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";

        PFAppProcessor _appProcessor = new PFAppProcessor();

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

        private void cmdShowHideOutputLog_Click(object sender, EventArgs e)
        {
            if (Program._messageLog.FormIsVisible)
                Program._messageLog.HideWindow();
            else
                Program._messageLog.ShowWindow();
        }

        //Form Routines
        private void CloseForm()
        {
            this.Close();
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

            this.chkEraseOutputBeforeEachTest.Checked = true;

            this.cboWordType.Items.Clear();
            foreach(object o in Enum.GetValues(typeof(PFRandomData.enWordType)))
            {
                this.cboWordType.Items.Add(o);
            }
            this.cboWordType.Text = this.cboWordType.Items[1].ToString();

            this.optDocument.Checked = true;

            this.txtNumWords.Text = "7";
            this.txtMinNumSentences.Text = "2";
            this.txtMaxNumSentences.Text = "5";
            this.txtMinNumParagraphs.Text = "2";
            this.txtMaxNumParagraphs.Text = "4";
            this.txtDocumentSubject.Text = "NEW DOCUMENT FOR REVIEW";
            this.txtNumChapters.Text = "3";
            this.txtBookTitle.Text = "My Random Book";
            this.txtChapterHeadings.Text = "Random Chapter 1" + Environment.NewLine
                                         + "A Second Chapter" + Environment.NewLine
                                         + "Third Chapter is This";
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

                if (this.chkRandomWordsTest.Checked)
                {
                    nNumTestsSelected++;
                    _appProcessor.RandomWordsTest((PFRandomData.enWordType)Enum.Parse(typeof(PFRandomData.enWordType),this.cboWordType.Text), 
                                                  Convert.ToInt32(this.txtNumWords.Text));
                }

                if (this.chkRandomSentencesTest.Checked)
                {
                    nNumTestsSelected++;
                    _appProcessor.RandomSentencesTest(Convert.ToInt32(this.txtMinNumSentences.Text), Convert.ToInt32(this.txtMaxNumSentences.Text));
                }

                if (this.chkRandomDocumentTest.Checked)
                {
                    if (optParagraphs.Checked)
                    {
                        nNumTestsSelected++;
                        _appProcessor.RandomParagraphsTest(Convert.ToInt32(this.txtMinNumParagraphs.Text), Convert.ToInt32(this.txtMaxNumParagraphs.Text),
                                                           Convert.ToInt32(this.txtMinNumSentences.Text), Convert.ToInt32(this.txtMaxNumSentences.Text));
                    }
                    if (optDocument.Checked)
                    {
                        nNumTestsSelected++;
                        _appProcessor.RandomDocumentTest(Convert.ToInt32(this.txtMinNumParagraphs.Text), Convert.ToInt32(this.txtMaxNumParagraphs.Text),
                                                         Convert.ToInt32(this.txtMinNumSentences.Text), Convert.ToInt32(this.txtMaxNumSentences.Text),
                                                         this.txtDocumentSubject.Text);
                    }
                    if (optChapters.Checked)
                    {
                        nNumTestsSelected++;
                        _appProcessor.RandomChaptersTest(Convert.ToInt32(this.txtNumChapters.Text), Convert.ToInt32(this.txtMinNumParagraphs.Text), Convert.ToInt32(this.txtMaxNumParagraphs.Text),
                                                          Convert.ToInt32(this.txtMinNumSentences.Text), Convert.ToInt32(this.txtMaxNumSentences.Text),
                                                          this.txtChapterHeadings.Text);
                    }
                    if (optBook.Checked)
                    {
                        nNumTestsSelected++;
                        _appProcessor.RandomBookTest(this.txtBookTitle.Text, Convert.ToInt32(this.txtNumChapters.Text), this.txtChapterHeadings.Text,
                                                     Convert.ToInt32(this.txtMinNumParagraphs.Text), Convert.ToInt32(this.txtMaxNumParagraphs.Text),
                                                     Convert.ToInt32(this.txtMinNumSentences.Text), Convert.ToInt32(this.txtMaxNumSentences.Text));
                    }
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



    }//end class
}//end namespace
