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

namespace TestprogTextFiles
{
    public partial class MainForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";

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

        private void cmdEraseResults_Click(object sender, EventArgs e)
        {
            EraseResults();
        }

        private void cmdShowHideOutputLog_Click(object sender, EventArgs e)
        {
            if (Program._messageLog.FormIsVisible)
                Program._messageLog.HideWindow();
            else
                Program._messageLog.ShowWindow();
        }

        //Men item clicks
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

                this.txtDelimitedNumRows.Text = "10";
                this.txtFixedLengthNumRows.Text = "15";
                this.chkFixedLengthCrLf.Checked = true;
                this.optDelimitedExtract.Checked = true;
                this.chkEraseOutputBeforeEachTest.Checked = true;


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

            }//end foreach control

        }//end method

        public void EraseResults()
        {
            Program._messageLog.Clear();
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


                if (this.chkRunPFTextFileTests.Checked)
                {
                    nNumTestsSelected++;
                    Tests.RunPFTextFileTests(this.chkAppend.Checked,
                                             this.chkDeleteAfterWrite.Checked);
                }

                if (this.chkDelimitedLineTextFileTests.Checked)
                {
                    nNumTestsSelected++;
                    Tests.DelimitedLineTextFileTests(Convert.ToInt32(this.txtDelimitedNumRows.Text));
                }


                if (this.chkFixedLengthTextFileTests.Checked)
                {
                    nNumTestsSelected++;
                    Tests.FixedLengthLineTextFileTests(Convert.ToInt32(this.txtFixedLengthNumRows.Text), this.chkFixedLengthCrLf.Checked, this.chkAllowDataTruncation.Checked);
                }

                if (this.chkExtractFileTest.Checked)
                {
                    nNumTestsSelected++;
                    if (this.optDelimitedExtract.Checked)
                        Tests.DelimitedExtractFileTest(this);
                    else
                        Tests.FixedLengthExtractFileTest(this);
                }

                if (this.chkShowDataDelimiterPrompt.Checked)
                {
                    nNumTestsSelected++;
                    Tests.ShowDataDelimiterPrompt();
                    this.Focus();
                }

                if (this.chkShowFixedLengthDataPrompt.Checked)
                {
                    nNumTestsSelected++;
                    Tests.ShowFixedLengthDataPrompt();
                    this.Focus();
                }

                if (this.chkImportXmlDocument.Checked)
                {
                    nNumTestsSelected++;
                    Tests.ImportXmlDocument(this);
                    this.Focus();
                }

                if (this.chkImportTextData.Checked)
                {
                    nNumTestsSelected++;
                    Tests.ImportTextData(this);
                    this.Focus();
                }

                if (this.chkShowTextFileViewer.Checked)
                {
                    nNumTestsSelected++;
                    Tests.ShowTextFileViewer(this);
                    this.Focus();
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

        private void cmdQuickTest_Click(object sender, EventArgs e)
        {

            try
            {
                //QuickTests.QuickTest1();
                //QuickTests.QuickTest2();
                QuickTests.QuickTest3();
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
