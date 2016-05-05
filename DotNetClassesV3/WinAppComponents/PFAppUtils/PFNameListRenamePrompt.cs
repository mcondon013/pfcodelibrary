﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppGlobals;
using System.IO;

namespace PFAppUtils
{
    /// <summary>
    /// Class to manage display of lists of string values.
    /// </summary>
    public partial class PFNameListRenamePrompt : Form
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;

        //private variables for properties
        private string _sourceFolder = string.Empty;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFNameListRenamePrompt()
        {
            InitializeComponent();
        }

        //properties

        /// <summary>
        /// Caption Property.
        /// </summary>
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

        /// <summary>
        /// Path to folder that contains files to list.
        /// </summary>
        public string SourceFolder
        {
            get
            {
                return _sourceFolder;
            }
            set
            {
                _sourceFolder = value;
            }
        }


        //button click events
        private void cmdRename_Click(object sender, EventArgs e)
        {
            FileRename();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            HideForm();
        }

        //form events
        private void WinForm_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }




        //common form processing routines
        /// <summary>
        /// Initializes the form values.
        /// </summary>
        public void InitializeForm()
        {
            LoadFileNamesToList();
            EnableFormControls();
        }

        /// <summary>
        /// Hides the form.
        /// </summary>
        public void HideForm()
        {
            this.Hide();
        }


        /// <summary>
        /// Closes form. Do not use this if you plan to use the form to retrieve the selected value.
        /// </summary>
        public void CloseForm()
        {
            this.Close();
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

        private void LoadFileNamesToList()
        {
            this.lstNames.Items.Clear();

            string[] filePaths = Directory.GetFiles(_sourceFolder, "*.xml");

            foreach (string filePath in filePaths)
            {
                this.lstNames.Items.Add(Path.GetFileNameWithoutExtension(filePath));
            }

        }

        private void FileRename()
        {
            PFNameListSpecifyNewNamePrompt frm = new PFNameListSpecifyNewNamePrompt();
            frm.Caption = "Specify new name for item.";
            frm.OriginalTextLabel = "Current Name:";
            frm.InputTextLabel = "Rename to:";
            frm.OkButtonText = "Rename";
            frm.CancelButtonText = "Cancel";
            if(this.lstNames.SelectedItem != null)
                frm.OriginalText = this.lstNames.SelectedItem.ToString();
            else
                return;
            frm.InputText = string.Empty;


            try
            {
                DialogResult res = frm.ShowDialog();
                if (res == DialogResult.OK)
                {
                    if (frm.InputText.Trim().Length > 0)
                    {
                        string oldName = Path.Combine(_sourceFolder, frm.OriginalText + ".xml");
                        if (File.Exists(oldName))
                        {
                            string newName = Path.Combine(_sourceFolder, frm.InputText + ".xml");
                            File.Move(oldName, newName);
                            LoadFileNamesToList();
                        }
                    }
                }
                frm.Close();
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to rename random data request failed. Error message:\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                frm = null;
            }



        }


    }//end class
}//end namespace
