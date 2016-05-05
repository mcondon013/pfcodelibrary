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

namespace PFAppUtils
{
#pragma warning disable 1591
    public partial class PFTreeViewFolderBrowserForm : Form
    {
        private StringBuilder _msg = new StringBuilder();

        private string _rootFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private PFTreeViewFolderBrowser _browser = null;

        //private variables for properties

        //constructors

        public PFTreeViewFolderBrowserForm(string rootFolder)
        {
            InitializeComponent();
            _rootFolder = rootFolder;
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

        public string RootFolderPath
        {
            get
            {
                string retval = string.Empty;
                if (this.treeviewFolders.Nodes.Count > 0)
                {
                    retval = this.treeviewFolders.Nodes[0].FullPath;
                }
                return retval;
            }
        }

        //button click events
        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            HideForm();
        }

        private void cmdChangeRoot_Click(object sender, EventArgs e)
        {
            ChangeRootFolder();
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
            _browser = new PFTreeViewFolderBrowser(this.treeviewFolders, _rootFolder);
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

            TreeNode tn = this.treeviewFolders.Nodes[0];
            if (tn.Checked)
                selectedFolders.Add(tn.FullPath);

            GetNodes(this.treeviewFolders.Nodes[0], selectedFolders);

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

        private void ChangeRootFolder()
        {
            PFFolderBrowserDialog diag = new PFFolderBrowserDialog();
            DialogResult res = diag.ShowFolderBrowserDialog();
            diag.InitialFolderPath = _rootFolder;
            diag.ShowNewFolderButton = false;

            if (res == System.Windows.Forms.DialogResult.OK)
            {
                _rootFolder = diag.SelectedFolderPath;
                _browser = null;
                InitTreeView();
            }
        }

    }//end class
#pragma warning restore 1591
}//end namespace
