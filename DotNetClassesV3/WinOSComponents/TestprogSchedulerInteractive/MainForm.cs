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
using PFSchedulerObjects;
using PFCollectionsObjects;
using System.Configuration;

namespace TestprogSchedulerInteractive
{
    public partial class MainForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        private string _helpFilePath = string.Empty;

        //private fields for processing file and folder dialogs
        private OpenFileDialog _openFileDialog = new OpenFileDialog();
        private SaveFileDialog _saveFileDialog = new SaveFileDialog();
        private FolderBrowserDialog _folderBrowserDialog = new FolderBrowserDialog();
        private string _saveSelectionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string _saveSelectionsFile = string.Empty;
        private string[] _saveSelectedFiles = null;
        private bool _saveMultiSelect = false;
        private string _saveFilter = "XML Files|*.xml|Text Files|*.txt|All Files|*.*";
        private int _saveFilterIndex = 1;
        private bool _showCreatePrompt = false;
        private bool _showOverwritePrompt = true;
        private bool _showNewFolderButton = true;

        internal string _taskDefsDbConnectionString = @"data source='C:\Temp\TestAppFolders\Databases\TaskDefinitions.sdf'";


        public MainForm()
        {
            InitializeComponent();
        }

        //button click events
        private void cmdReinit_Click(object sender, EventArgs e)
        {
            InitializeForm();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void cmdRunTest_Click(object sender, EventArgs e)
        {
            RunTests();
        }

        private void cmdNextSkedDate_Click(object sender, EventArgs e)
        {
            RunNextSkedDate();
        }

        private void cmdShowAllScheduledDates_Click(object sender, EventArgs e)
        {
            ShowAllScheduledDates();
        }

        private void cmdSaveSchedule_Click(object sender, EventArgs e)
        {
            if (this.optXmlFiles.Checked)
                SaveScheduleToFile();
            else if (this.optDatabase.Checked)
                SaveScheduleToDatabase();
            else
                SaveScheduleToFile();
        }

        private void cmdLoadSchedule_Click(object sender, EventArgs e)
        {
            if (this.optXmlFiles.Checked)
                LoadScheduleFromFile();
            else if (this.optDatabase.Checked)
                LoadScheduleFromDatabase();
            else
                LoadScheduleFromFile();
        }

        private void cboScheduleFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProcessScheduleFrequencyChange();
        }

        private void optDailyRunOnce_CheckedChanged(object sender, EventArgs e)
        {
            ProcessDailyRunFrequencyChange();
        }

        private void optDailyRecurring_CheckedChanged(object sender, EventArgs e)
        {
            ProcessDailyRunFrequencyChange();
        }

        private void txtScheduleRunsEveryNumDays_Leave(object sender, EventArgs e)
        {
            VerifyScheduleRunsEveryNumDaysValue();
        }

        private void optOccursEveryMonthNum_CheckedChanged(object sender, EventArgs e)
        {
            ProcessMonthlyOccursChange();
        }

        private void optMonthlyDayNumber_CheckedChanged(object sender, EventArgs e)
        {
            ProcessMonthlyDayNumberAndNameChanges();
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
                this.optXmlFiles.Checked = true;

                configValue = AppGlobals.AppConfig.GetStringValueFromConfigFile("TaskDefsDbConnectionString", string.Empty);
                if (configValue.Length > 0)
                {
                    _taskDefsDbConnectionString = configValue;
                }

                InitializeForm();

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }

        }

        internal void InitializeForm()
        {
            this.txtScheduleName.Text = string.Empty;
            this.txtScheduleStart.Text = "01/01/2000 00:00:00";
            this.txtScheduleEnd.Text = "12/31/2999 23:59:59";
            this.txtRunWindowInMinutes.Text = "10";
            this.txtScheduleRunsEveryNumDays.Text = "1";
            this.txtDailyRunOnceAt.Text = string.Empty;
            this.txtDailyOccursEveryIntervalNum.Text = "1";
            this.txtOccursStartingAt.Text = string.Empty;
            this.txtOccursEndsAt.Text = string.Empty;
            this.txtWeeklyRecursEveryNumDays.Text = "1";
            this.txtMonthlyOccursIntervalNum.Text = "1";
            this.txtMonthlyDayNumber.Text = "1";

            this.txtCurrDateTime.Text = string.Empty;

            this.cboExpectedTestResult.Items.Clear();
            foreach (enScheduleLookupResult lkr in Enum.GetValues(typeof(enScheduleLookupResult)))
            {
                this.cboExpectedTestResult.Items.Add(lkr.ToString());
            }

            this.optOccursEveryMonthNum.Checked = true;
            this.optMonthlyDayName.Checked = true;

            this.cboScheduleFrequency.SelectedIndex = 0;
            this.cboMonthlyDayNameOrdinal.SelectedIndex = 0;
            this.cboMonthlyDayName.SelectedIndex = 0;
            this.cboDailyOccursInterval.SelectedIndex = 0;
            this.cboExpectedTestResult.SelectedIndex = 0;
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

                nNumTestsSelected++;
                Tests.RunTest(this);

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
                ProcessScheduleFrequencyChange();
                _msg.Length = 0;
                _msg.Append("\r\n");
                _msg.Append("Number of tests run: ");
                _msg.Append(nNumTestsSelected.ToString("#,##0"));
                Program._messageLog.WriteLine(_msg.ToString());
            }



        }


        private void RunNextSkedDate()
        {

            try
            {

                DisableFormControls();
                Tests.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;

                if (this.chkEraseOutputBeforeEachTest.Checked)
                {
                    Program._messageLog.Clear();
                }

                Tests.GetNextScheduleDateTest(this);

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
                ProcessScheduleFrequencyChange();
            }
                 
        
        }

        private void ShowAllScheduledDates()
        {

            try
            {

                DisableFormControls();
                Tests.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;

                if (this.chkEraseOutputBeforeEachTest.Checked)
                {
                    Program._messageLog.Clear();
                }

                Tests.ShowAllScheduledDates(this);

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
                ProcessScheduleFrequencyChange();
            }
                 
        

        }



        private void SaveScheduleToFile()
        {
            if (this.chkEraseOutputBeforeEachTest.Checked)
            {
                Program._messageLog.Clear();
            }

            try
            {
                string filename = string.Empty;
                string configValue = AppConfig.GetStringValueFromConfigFile("ScheduleFilesFolder", string.Empty);
                if (configValue.Trim().Length > 0)
                    _saveSelectionsFolder = configValue;
                if (this.txtScheduleName.Text.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify a name for the schedule.");
                    throw new System.Exception(_msg.ToString());
                }
                _saveSelectionsFile = this.txtScheduleName.Text + ".xml";
                DialogResult res = ShowSaveFileDialog();
                if (res == DialogResult.OK)
                {
                    filename = _saveSelectionsFile;
                    PFSchedule sked = Tests.CreateScheduleFromScreenInput(this);
                    sked.SaveToXmlFile(filename);
                    if (this.txtScheduleName.Text.Trim().Length == 0)
                        this.txtScheduleName.Text = Path.GetFileNameWithoutExtension(filename);
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

        private void SaveScheduleToDatabase()
        {
            string connectionString = _taskDefsDbConnectionString;

            if (this.chkEraseOutputBeforeEachTest.Checked)
            {
                Program._messageLog.Clear();
            }



            try
            {
                if (this.txtScheduleName.Text.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify a name for the schedule.");
                    throw new System.Exception(_msg.ToString());
                }

                PFSchedule sked = Tests.CreateScheduleFromScreenInput(this);
                sked.SaveToDatabase(connectionString);

                _msg.Length = 0;
                _msg.Append("\r\nSchedule saved to ");
                _msg.Append(connectionString);
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

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

        private void LoadScheduleFromFile()
        {
            if (this.chkEraseOutputBeforeEachTest.Checked)
            {
                Program._messageLog.Clear();
            }

            string filename = string.Empty;
            string configValue = AppConfig.GetStringValueFromConfigFile("ScheduleFilesFolder", string.Empty);
            if (configValue.Trim().Length > 0)
                _saveSelectionsFolder = configValue;
            DialogResult res = ShowOpenFileDialog();
            if (res == DialogResult.OK)
            {
                filename = _saveSelectionsFile;
                PFSchedule sked = PFSchedule.LoadFromXmlFile(filename);
                Tests.CreateScreenInputFromSchedule(this, sked);
                ProcessScheduleFrequencyChange();
            }
        }

        private void LoadScheduleFromDatabase()
        {
            string connectionString = _taskDefsDbConnectionString;
            string scheduleName = this.txtScheduleName.Text.Trim();
            PFList<PFSchedule> skedList = null;
            PFSchedule sked = null;
            PFScheduleManager skedMgr = new PFScheduleManager(enScheduleStorageType.Database, connectionString);
            string inputFileName = string.Empty;

            if (this.chkEraseOutputBeforeEachTest.Checked)
            {
                Program._messageLog.Clear();
            }

            try
            {

                skedList = skedMgr.GetScheduleList();
                NameListPrompt namesPrompt = new NameListPrompt();
                namesPrompt.lblSelect.Text = "Select the name from list below:";
                for (int i = 0; i < skedList.Count; i++)
                {
                    namesPrompt.lstNames.Items.Add(skedList[i].Name);
                }
                if (skedList.Count > 0)
                {
                    namesPrompt.lstNames.SelectedIndex = 0;
                }
                DialogResult res = namesPrompt.ShowDialog();
                if (res == DialogResult.OK)
                {
                    if (namesPrompt.lstNames.SelectedIndex == -1)
                    {
                        _msg.Length = 0;
                        _msg.Append("You did not select any schedule to load");
                        throw new System.Exception(_msg.ToString());
                    }
                    this.InitializeForm();
                    scheduleName = namesPrompt.lstNames.SelectedItem.ToString();
                    if (scheduleName.Length == 0)
                    {
                        _msg.Length = 0;
                        _msg.Append("You must specify a sked name");
                        throw new System.Exception(_msg.ToString());
                    }

                    sked = skedMgr.GetScheduleByName(scheduleName);

                    if (sked == null)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to find schedule ");
                        _msg.Append(scheduleName);
                        _msg.Append(" in the database at ");
                        _msg.Append(_taskDefsDbConnectionString);
                        throw new System.Exception(_msg.ToString());
                    }

                    Tests.CreateScreenInputFromSchedule(this, sked);
                    ProcessScheduleFrequencyChange();
                
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


        private void ProcessScheduleFrequencyChange()
        {
            ProcessDailyRunFrequencyChange();

            if (this.cboScheduleFrequency.Text == enScheduleFrequency.OneTime.ToString())
            {
                this.txtScheduleRunsEveryNumDays.Text = "1";
                this.grpRunOneTimeParameters.Enabled = true;
                this.grpDailyScheduleParameters.Enabled = false;
                this.grpWeeklyScheduleParameters.Enabled = false;
                this.grpMonthlyScheduleParameters.Enabled = false;
            }
            else if (this.cboScheduleFrequency.Text == enScheduleFrequency.Daily.ToString())
            {
                this.grpRunOneTimeParameters.Enabled = false;
                this.grpDailyScheduleParameters.Enabled = true;
                this.grpWeeklyScheduleParameters.Enabled = false;
                this.grpMonthlyScheduleParameters.Enabled = false;
            }
            else if (this.cboScheduleFrequency.Text == enScheduleFrequency.Weekly.ToString())
            {
                this.txtScheduleRunsEveryNumDays.Text = "1";
                this.grpRunOneTimeParameters.Enabled = false;
                this.grpDailyScheduleParameters.Enabled = true;
                this.grpWeeklyScheduleParameters.Enabled = true;
                this.grpMonthlyScheduleParameters.Enabled = false;
            }
            else if (this.cboScheduleFrequency.Text == enScheduleFrequency.Monthly.ToString())
            {
                this.txtScheduleRunsEveryNumDays.Text = "1";
                this.grpRunOneTimeParameters.Enabled = false;
                this.grpDailyScheduleParameters.Enabled = true;
                this.grpWeeklyScheduleParameters.Enabled = false;
                this.grpMonthlyScheduleParameters.Enabled = true;
            }
            else
            {
                this.txtScheduleRunsEveryNumDays.Text = "1";
                this.grpRunOneTimeParameters.Enabled = false;
                this.grpDailyScheduleParameters.Enabled = false;
                this.grpWeeklyScheduleParameters.Enabled = false;
                this.grpMonthlyScheduleParameters.Enabled = false;
                ProcessMonthlyOccursChange();
            }


        }

        private void ProcessDailyRunFrequencyChange()
        {
            if (this.optDailyRunOnce.Checked)
            {
                this.grpDailyRunOnceParameters.Enabled = true;
                this.grpDailyRecurringParameters.Enabled = false;
            }
            else if (this.optDailyRecurring.Checked)
            {
                this.grpDailyRunOnceParameters.Enabled = false;
                this.grpDailyRecurringParameters.Enabled = true;
            }
            else
            {
                this.grpDailyRunOnceParameters.Enabled = false;
                this.grpDailyRecurringParameters.Enabled = false;
            }

        }

        private void VerifyScheduleRunsEveryNumDaysValue()
        {
            if (this.cboScheduleFrequency.Text != enScheduleFrequency.Daily.ToString())
            {
                this.txtScheduleRunsEveryNumDays.Text = "1";
            }
        }

        private void ProcessMonthlyOccursChange()
        {
            if (this.optOccursEveryMonthNum.Checked)
            {
                this.txtMonthlyOccursIntervalNum.Enabled = true;
                SetMonthlyMonthNameSelections(false);
                ResetMonthNameSelectionsEnabled(false);
            }
            else if (this.optOccursDuringMonthName.Checked)
            {
                ResetMonthlyOccursIntervalNum();
                ResetMonthNameSelectionsEnabled(true);
                this.txtMonthlyOccursIntervalNum.Enabled = false;
            }
            else
            {
                ResetMonthlyOccursIntervalNum();
                SetMonthlyMonthNameSelections(false);
                ResetMonthNameSelectionsEnabled(false);
            }
        }

        private void ResetMonthlyOccursIntervalNum()
        {
            this.txtMonthlyOccursIntervalNum.Text = "1";
        }

        private void SetMonthlyMonthNameSelections(bool value)
        {
            this.chkMonthlyJan.Checked = value;
            this.chkMonthlyFeb.Checked = value;
            this.chkMonthlyMar.Checked = value;
            this.chkMonthlyApr.Checked = value;
            this.chkMonthlyMay.Checked = value;
            this.chkMonthlyJun.Checked = value;
            this.chkMonthlyJul.Checked = value;
            this.chkMonthlyAug.Checked = value;
            this.chkMonthlySep.Checked = value;
            this.chkMonthlyOct.Checked = value;
            this.chkMonthlyNov.Checked = value;
            this.chkMonthlyDec.Checked = value;


        }

        private void ResetMonthNameSelectionsEnabled(bool value)
        {
            this.chkMonthlyJan.Enabled = value;
            this.chkMonthlyFeb.Enabled = value;
            this.chkMonthlyMar.Enabled = value;
            this.chkMonthlyApr.Enabled = value;
            this.chkMonthlyMay.Enabled = value;
            this.chkMonthlyJun.Enabled = value;
            this.chkMonthlyJul.Enabled = value;
            this.chkMonthlyAug.Enabled = value;
            this.chkMonthlySep.Enabled = value;
            this.chkMonthlyOct.Enabled = value;
            this.chkMonthlyNov.Enabled = value;
            this.chkMonthlyDec.Enabled = value;
        }

        private void ProcessMonthlyDayNumberAndNameChanges()
        {
            if (this.optMonthlyDayNumber.Checked)
            {
                this.txtMonthlyDayNumber.Enabled = true;
                this.cboMonthlyDayNameOrdinal.SelectedIndex = 0;
                this.cboMonthlyDayName.SelectedIndex = 0;
                this.cboMonthlyDayNameOrdinal.Enabled = false;
                this.cboMonthlyDayName.Enabled = false;
            }
            else if (this.optMonthlyDayName.Checked)
            {
                this.txtMonthlyDayNumber.Text = "1";
                this.txtMonthlyDayNumber.Enabled = false;
                this.cboMonthlyDayNameOrdinal.Enabled = true;
                this.cboMonthlyDayName.Enabled = true;
            }
            else
            {
                this.optMonthlyDayNumber.Checked = true;
                this.txtMonthlyDayNumber.Text = "1";
                this.txtMonthlyDayNumber.Enabled = true;
                this.cboMonthlyDayNameOrdinal.SelectedIndex = 0;
                this.cboMonthlyDayName.SelectedIndex = 0;
                this.cboMonthlyDayNameOrdinal.Enabled = false;
                this.cboMonthlyDayName.Enabled = false;
            }

        }


    }//end class
}//end namespace
