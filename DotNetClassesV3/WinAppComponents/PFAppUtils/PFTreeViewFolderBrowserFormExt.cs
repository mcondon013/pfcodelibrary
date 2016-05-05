﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AppGlobals;

namespace PFAppUtils
{
#pragma warning disable 1591
    public partial class PFTreeViewFolderBrowserFormExt : Form
    {
        private StringBuilder _msg = new StringBuilder();

        private List<string> _rootFolders = null;
        private PFTreeViewFolderBrowserExt _browser = null;

        //private variables for properties

        //constructors

        public PFTreeViewFolderBrowserFormExt(List<string> rootFolders)
        {
            InitializeComponent();
            _rootFolders = rootFolders;
        }

        //properties

        public TreeView TreeViewFoldersControl
        {
            get
            {
                return this.treeviewFolders;
            }
        }

        public List<string> SelectedFolders
        {
            get
            {
                return GetSelectedFolders();
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

        //form events
        private void WinForm_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }

        private void treeviewFolders_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            _browser.treeView_BeforeExpand(sender, e);
        }


        //common form processing routines
        public void HideForm()
        {
            this.Hide();
        }

        public void CloseForm()
        {
            this.Close();
        }

        public void InitializeForm()
        {
            InitTreeView();
            EnableFormControls();
        }

        private void InitTreeView()
        {
            _browser = new PFTreeViewFolderBrowserExt(this.treeviewFolders, _rootFolders);
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

        private List<string> GetSelectedFolders()
        {
            List<string> selectedFolders = new List<string>();

            for (int inx = 0; inx < this.treeviewFolders.Nodes.Count; inx++)
            {
                TreeNode tn = this.treeviewFolders.Nodes[inx];
                if (tn.Checked)
                    selectedFolders.Add(tn.FullPath);

                GetNodes(this.treeviewFolders.Nodes[inx], selectedFolders);
            }

            return selectedFolders;
        }

        private void GetNodes(TreeNode tn, List<string> selectedFolders)
        {
            if (tn.Nodes.Count > 0)
            {
                foreach (TreeNode node in tn.Nodes)
                {
                    if (node.Checked)
                    {
                        selectedFolders.Add(node.FullPath);
                    }
                    if (node.Nodes.Count > 0)
                    {
                        GetNodes(node, selectedFolders);
                    }
                }
            }

        }


    }//end class
#pragma warning restore 1591
}//end namespace
