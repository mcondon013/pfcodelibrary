using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppGlobals;
using PFMessageLogs;

namespace TestprogMessageLogs
{
    public partial class MainForm : Form
    {
        StringBuilder _msg = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        private Tests _tests = new Tests();

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

        private void cmdResetForm_Click(object sender, EventArgs e)
        {
            RestoreDefaultScreenLocations();
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

                configValue = AppGlobals.AppConfig.GetConfigValue("SaveErrorMessagesToErrorLog");
                if (configValue.ToUpper() == "TRUE")
                    _saveErrorMessagesToAppLog = true;
                else
                    _saveErrorMessagesToAppLog = false;
                _appLogFileName = AppGlobals.AppConfig.GetConfigValue("AppLogFileName");

                if (_appLogFileName.Trim().Length > 0)
                    AppGlobals.AppMessages.AppLogFilename = _appLogFileName;

                this.txtMinNumGetSum.Text = "1";
                this.txtMaxNumGetSum.Text = "100";
                this.txtOutputInterval.Text = "5";
                this.chkShowDateTimeInOutput.Checked = false;

                this.txtTextLogFilePath.Text = @"c:\temp\OutputLog.txt";
                this.chkAppendMessagesIfFileExists.Checked = false;
                this.chkShowMessageTypeWithEachMessage.Checked = false;
                this.chkShowApplicationNameWithEachMessage.Checked = false;
                this.chkShowMachineNameWithEachMessage.Checked = false;

                this.txtEventSource.Text = "TESTPROG_MSGLOGS";
                this.txtNumInformationMessagesToWrite.Text = "1";
                this.txtNumWarningMessagesToWrite.Text = "1";
                this.txtNumErrorMessagesToWrite.Text = "1";

                this.optRunTestprogApplicationMessageLogTest.Checked = true;
                _tests.MessageLog = Program._messageLog;

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

        private void RunTests()
        {

            int nNumTestsSelected = 0;

            try
            {
                DisableFormControls();

                Program._messageLog.Clear();

                _tests.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;

                if (optRunTestprogApplicationMessageLogTest.Checked)
                {
                    nNumTestsSelected++;
                    _tests.RunApplicationMessageLogTest(Convert.ToInt64(this.txtMinNumGetSum.Text),
                                                        Convert.ToInt64(this.txtMaxNumGetSum.Text),
                                                        Convert.ToInt64(this.txtOutputInterval.Text),
                                                        this.chkShowDateTimeInOutput.Checked);
                }
                
                if (optRunGetSumFromDLL.Checked)
                {
                    nNumTestsSelected++;
                    _tests.RunDllGetSumTest(Convert.ToInt64(this.txtMinNumGetSum.Text),
                                         Convert.ToInt64(this.txtMaxNumGetSum.Text),
                                         Convert.ToInt64(this.txtOutputInterval.Text),
                                         this.chkShowDateTimeInOutput.Checked);
                }

                if (optOutputMessagesToTextLogFile.Checked)
                {
                    nNumTestsSelected++;
                    _tests.OutputMessagesToTextLogFile(this.txtTextLogFilePath.Text,
                                                       this.chkAppendMessagesIfFileExists.Checked,
                                                       this.chkShowMessageTypeWithEachMessage.Checked,
                                                       this.chkShowApplicationNameWithEachMessage.Checked,
                                                       this.chkShowMachineNameWithEachMessage.Checked,
                                                       Convert.ToInt64(this.txtMinNumGetSum.Text),
                                                       Convert.ToInt64(this.txtMaxNumGetSum.Text),
                                                       Convert.ToInt64(this.txtOutputInterval.Text),
                                                       this.chkShowDateTimeInOutput.Checked);
                }

                if (optOutputMessagesToWindowsApplicationEventLog.Checked)
                {
                    nNumTestsSelected++;
                    _tests.OutputMessagesToWindowsApplicationEventLog(this.txtEventSource.Text,
                                                                      Convert.ToInt32(this.txtNumInformationMessagesToWrite.Text),
                                                                      Convert.ToInt32(this.txtNumWarningMessagesToWrite.Text),
                                                                      Convert.ToInt32(this.txtNumErrorMessagesToWrite.Text));
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

        private void cmdHideAppLog_Click(object sender, EventArgs e)
        {
            Program._messageLog.HideWindow();
        }

        private void cmdShowAppLog_Click(object sender, EventArgs e)
        {
            Program._messageLog.ShowWindow();
        }

        private void cmdHideDLLMessageLog_Click(object sender, EventArgs e)
        {
            _tests.HideDllWindow();
        }

        private void cmdShowDLLMessageLog_Click(object sender, EventArgs e)
        {
            _tests.ShowDllWindow();
        }

        private void cmdDeleteEventSource_Click(object sender, EventArgs e)
        {
            _tests.DeleteEventSource(this.txtEventSource.Text);
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
