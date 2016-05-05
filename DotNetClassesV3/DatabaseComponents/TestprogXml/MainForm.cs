using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using AppGlobals;
using PFFileSystemObjects;

namespace TestprogXml
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

        private void cmdTest1_Click(object sender, EventArgs e)
        {
            if (this.chkEraseOutputBeforeEachTest.Checked)
                Program._messageLog.Clear();
            Tests.ValidateXmlTest();
            //Tests.LoadBadXMLTest();
        }

        private void cmdCreateXMLDoc_Click(object sender, EventArgs e)
        {
            if (this.chkEraseOutputBeforeEachTest.Checked)
                Program._messageLog.Clear();
            Tests.CreateXMLDoc(this.txtFilename.Text, this.txtRootNode.Text);
        }

        private void cmdOpenXMLDocument_Click(object sender, EventArgs e)
        {
            if (this.chkEraseOutputBeforeEachTest.Checked)
                Program._messageLog.Clear();
            Tests.OpenXMLDoc(this.txtFilename.Text);
        }

        private void cmdRunTest_Click(object sender, EventArgs e)
        {
            if (this.chkEraseOutputBeforeEachTest.Checked)
                Program._messageLog.Clear();
            Tests.RunTest_Memory(this.txtFilename.Text, this.txtRootNode.Text);
        }

        private void cmdAddNodes_Click(object sender, EventArgs e)
        {
            if (this.chkEraseOutputBeforeEachTest.Checked)
                Program._messageLog.Clear();
            Tests.AddNodes(this);
        }

        private void cmdDeleteNode_Click(object sender, EventArgs e)
        {
            if (this.chkEraseOutputBeforeEachTest.Checked)
                Program._messageLog.Clear();
            Tests.DeleteNode(this);
        }

        private void cmdDeleteChildNodes_Click(object sender, EventArgs e)
        {
            if (this.chkEraseOutputBeforeEachTest.Checked)
                Program._messageLog.Clear();
            Tests.DeleteChildNodes(this);
        }

        private void cmdDeleteAllNodes_Click(object sender, EventArgs e)
        {
            if (this.chkEraseOutputBeforeEachTest.Checked)
                Program._messageLog.Clear();
            Tests.DeleteAllNodes(this);
        }

        private void cmdUpdateNodes_Click(object sender, EventArgs e)
        {
            if (this.chkEraseOutputBeforeEachTest.Checked)
                Program._messageLog.Clear();
            Tests.UpdateNodes(this);
        }

        private void cmdFindNode_Click(object sender, EventArgs e)
        {
            if (this.chkEraseOutputBeforeEachTest.Checked)
                Program._messageLog.Clear();
            Tests.FindNode(this);
        }

        private void cmdFindNodes_Click(object sender, EventArgs e)
        {
            if (this.chkEraseOutputBeforeEachTest.Checked)
                Program._messageLog.Clear();
            Tests.FindNodes_Memory();
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

                this.txtFilename.Text = @"c:\temp\TestXmlDoc.xml";
                this.txtRootNode.Text = "TestRoot";


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

        }



        //application routines




    }//end class
}//end namespace
