using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppGlobals;

namespace pfEncryptor
{
    public partial class TextDisplayForm : Form
    {
        private StringBuilder _msg = new StringBuilder();

        public TextDisplayForm()
        {
            InitializeComponent();
        }


        public string Caption
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        public string TextToProcess
        {
            get
            {
                return this.txtTextToProcess.Text;
            }
            set
            {
                this.txtTextToProcess.Text = value;
            }
        }

        public bool TextToProcessReadOnly
        {
            get
            {
                return this.txtTextToProcess.ReadOnly;
            }
            set
            {
                this.txtTextToProcess.ReadOnly = value;
                this.txtTextToProcess.BackColor = Color.White;
            }
        }

        public bool ShowCancelCommand
        {
            get
            {
                return this.cmdCancel.Visible;
            }
            set
            {
                this.cmdCancel.Visible = value;
            }
        }


        //button click events
        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            HideForm();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            HideForm();
        }

        private void cmdCopy_Click(object sender, EventArgs e)
        {
            CopyTextToClipboard();
        }

        //form events
        private void WinForm_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }




        //common form processing routines
        public void InitializeForm()
        {
            this.DialogResult = DialogResult.None;
            EnableFormControls();
        }

        public void HideForm()
        {
            this.Hide();
        }

        public void CloseForm()
        {
            this.Close();
        }

        private void CopyTextToClipboard()
        {

            try
            {
                System.Windows.Forms.Clipboard.SetText(this.txtTextToProcess.Text);
                AppMessages.DisplayInfoMessage("Copy to Clipboard succeeded.");
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to copy text to Clipboard failed: ");
                _msg.Append(Environment.NewLine);
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
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

        }

        //Application routines
        private void Test()
        {
            AppMessages.DisplayWarningMessage("No code implemented for this button.");
        }


    }//end class
}//end namespace
