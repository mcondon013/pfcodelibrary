using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppGlobals;
using PFConnectionObjects;
using PFCollectionsObjects;
using PFListObjects;

namespace PFProviderForms
{
    /// <summary>
    /// Form for selecting which database providers to display to applications.
    /// </summary>
    public partial class ProviderSelectorForm : Form
    {
        private StringBuilder _msg = new StringBuilder();

        private bool _userExitButtonPressed = false;
        private bool _dataHasPendingChanges = false;

        PFConnectionManager _connMgr = new PFConnectionManager();
        private PFKeyValueList<string, PFProviderDefinition> _providerDefinitions = new PFKeyValueList<string, PFProviderDefinition>();

        private bool _providerListHasBeenChanged = false;

        //properties
        /// <summary>
        /// ProviderListHasBeenChanged Property.
        /// </summary>
        public bool ProviderListHasBeenChanged
        {
            get
            {
                return _providerListHasBeenChanged;
            }
        }

        /// <summary>
        /// ShowInstalledProvidersOnly Property.
        /// </summary>
        public bool ShowInstalledProvidersOnly
        {
            get
            {
                return this.chkShowInstalledProvidersOnly.Checked;
            }
            set
            {
                this.chkShowInstalledProvidersOnly.Checked = value;
            }
        }



        /// <summary>
        /// Constructor.
        /// </summary>
        public ProviderSelectorForm()
        {
            InitializeComponent();
        }

        //button click events
        private void cmdSave_Click(object sender, EventArgs e)
        {
            SaveLists();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            DialogResult res = System.Windows.Forms.DialogResult.OK;
            _userExitButtonPressed = true;
            if (_dataHasPendingChanges)
            {
                res = CheckCancelRequest();
                if (res == DialogResult.Yes)
                {
                    SaveLists();
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    HideForm();
                }
                else if (res == DialogResult.Cancel)
                {
                    _userExitButtonPressed = false;
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                    //don't hide the form
                }
                else
                {
                    //DialogResult.No
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    HideForm();
                }
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                HideForm();
            }
        }

        private void chkShowInstalledProvidersOnly_CheckedChanged(object sender, EventArgs e)
        {
            ShowInstalledProvidersOnly_CheckedChanged();
        }

        //form events
        private void WinForm_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }

        private void PFWindowsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && _userExitButtonPressed == false && _dataHasPendingChanges == true)
            {
                DialogResult res = CheckCancelRequest();
                if (res == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                    this.DialogResult = DialogResult.Ignore;
                }
                else if (res == DialogResult.Yes)
                {
                    SaveLists();
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    //res == DialogResult.No
                    //exit without saving
                    this.DialogResult = DialogResult.OK;
                }
            }

        }

        private DialogResult CheckCancelRequest()
        {
            DialogResult res = AppMessages.DisplayMessage("Do you wish to save changes made to the list of provider selections.", "Save or discard selection list updates? ...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            return res;
        }




        //common form processing routines
        /// <summary>
        /// Routines to initialize form.
        /// </summary>
        public void InitializeForm()
        {
            _userExitButtonPressed = false;
            VerifyProviderLists();
            EnableFormControls();
            InitListBoxes();
        }

        private void VerifyProviderLists()
        {
            PFKeyValueList<string, PFProviderDefinition> provDefs = _connMgr.GetListOfProviderDefinitions();

            if (provDefs.Count == 0)
            {
                _connMgr.CreateProviderDefinitions();
            }
            else
            {
                _connMgr.UpdateAllProvidersInstallationStatus();
            }
            //provDefs = _connMgr.GetListOfProviderDefinitions();
        }

        private void InitListBoxes()
        {
            //this.chkShowInstalledProvidersOnly.Checked = false;
            ShowInstalledProvidersOnly_CheckedChanged();
            //RefreshListBoxes(); //list boxes refreshed in CheckedChanged routine
        }

        /// <summary>
        /// Hides the form. 
        /// </summary>
        public void HideForm()
        {
            this.Hide();
        }

        /// <summary>
        /// Closes form.
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

            ResetSelectorControls();

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

        private void cmdSelectRange_Click(object sender, EventArgs e)
        {
            SelectRange();
        }

        private void cmdSelectAll_Click(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void cmdDeselectRange_Click(object sender, EventArgs e)
        {
            DeselectRange();
        }

        private void cmdDeselectAll_Click(object sender, EventArgs e)
        {
            DeselectAll();
        }

        //Application routines
        private void SaveLists()
        {

            try
            {
                DisableFormControls();
                this.Cursor = Cursors.WaitCursor;
                
                for (int i = 0; i < _connMgr.ProviderDefinitions.Count; i++)
                {
                    _connMgr.ProviderDefinitions[i].Value.AvailableForSelection = GetAvailableForSelectionValue(_connMgr.ProviderDefinitions[i].Key);
                    _connMgr.SaveProvider(_connMgr.ProviderDefinitions[i].Value);
                }
                _connMgr.SaveProvidersListToFile();

                _dataHasPendingChanges = false;

                _providerListHasBeenChanged = true;

                AppMessages.DisplayInfoMessage("Provider selections have been saved.");
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                EnableFormControls();
                this.Cursor = Cursors.Default;
            }
                 
        
        }

        private bool GetAvailableForSelectionValue(string dbPlatDesc)
        {
            bool availableForSelection = false;

            if (this.lstSelectedProviders.Items.Count > 0)
            {
                for (int i = 0; i < this.lstSelectedProviders.Items.Count; i++)
                {
                    if (dbPlatDesc == this.lstSelectedProviders.Items[i].ToString())
                    {
                        availableForSelection = true;
                        break;
                    }
                }
            }

            return availableForSelection;
        }

        private void SelectRange()
        {
            if (this.lstSupportedProviders.SelectedIndices.Count > 0)
            {
                for(int i = 0; i < this.lstSupportedProviders.SelectedItems.Count; i++)
                {
                    string itm = this.lstSupportedProviders.SelectedItems[i].ToString();
                    this.lstSelectedProviders.Items.Add(itm);
                }
                for (int i = this.lstSupportedProviders.SelectedItems.Count - 1; i >= 0; i--)
                {
                    this.lstSupportedProviders.Items.Remove(this.lstSupportedProviders.SelectedItems[i]);
                }
                _dataHasPendingChanges = true;
            }
            ResetSelectorControls();
        }

        private void SelectAll()
        {
            if (this.lstSupportedProviders.Items.Count > 0)
            {
                for (int i = 0; i < this.lstSupportedProviders.Items.Count; i++)
                {
                    string itm = this.lstSupportedProviders.Items[i].ToString();
                    this.lstSelectedProviders.Items.Add(itm);
                }
                for (int i = this.lstSupportedProviders.Items.Count - 1; i >= 0; i--)
                {
                    this.lstSupportedProviders.Items.Remove(this.lstSupportedProviders.Items[i]);
                }
                _dataHasPendingChanges = true;
            }
            ResetSelectorControls();
        }

        private void DeselectRange()
        {
            if (this.lstSelectedProviders.SelectedIndices.Count > 0)
            {
                for (int i = 0; i < this.lstSelectedProviders.SelectedItems.Count; i++)
                {
                    string itm = this.lstSelectedProviders.SelectedItems[i].ToString();
                    this.lstSupportedProviders.Items.Add(itm);
                }
                for (int i = this.lstSelectedProviders.SelectedItems.Count - 1; i >= 0; i--)
                {
                    this.lstSelectedProviders.Items.Remove(this.lstSelectedProviders.SelectedItems[i]);
                }
                _dataHasPendingChanges = true;
            }
            ResetSelectorControls();
        }

        private void DeselectAll()
        {
            if (this.lstSelectedProviders.Items.Count > 0)
            {
                for (int i = 0; i < this.lstSelectedProviders.Items.Count; i++)
                {
                    string itm = this.lstSelectedProviders.Items[i].ToString();
                    this.lstSupportedProviders.Items.Add(itm);
                }
                for (int i = this.lstSelectedProviders.Items.Count - 1; i >= 0; i--)
                {
                    this.lstSelectedProviders.Items.Remove(this.lstSelectedProviders.Items[i]);
                }
                _dataHasPendingChanges = true;
            }
            ResetSelectorControls();
        }

        /// <summary>
        /// Sets or disables selector controls.
        /// </summary>
        public void ResetSelectorControls()
        {
            if (this.lstSupportedProviders.Items.Count > 0)
            {
                this.cmdSelectRange.Enabled = true;
                this.cmdSelectAll.Enabled = true;
            }
            else
            {
                this.cmdSelectRange.Enabled = false;
                this.cmdSelectAll.Enabled = false;
            }

            if (this.lstSelectedProviders.Items.Count > 0)
            {
                this.cmdDeselectRange.Enabled = true;
                this.cmdDeselectAll.Enabled = true;
            }
            else
            {
                this.cmdDeselectRange.Enabled = false;
                this.cmdDeselectAll.Enabled = false;
            }
        }

        private void ShowInstalledProvidersOnly_CheckedChanged()
        {
            if (this.chkShowInstalledProvidersOnly.Checked)
            {
                this.lblSupportedProviders.Text = "Installed Providers";
            }
            else
            {
                this.lblSupportedProviders.Text = "All Providers";
            }
            RefreshListBoxes();
        }


        private void RefreshListBoxes()
        {
            this.lstSelectedProviders.Items.Clear();
            this.lstSupportedProviders.Items.Clear();
            for (int i = 0; i < _connMgr.ProviderDefinitions.Count; i++)
            {
                PFProviderDefinition provDef = _connMgr.ProviderDefinitions[i].Value;
                if (this.chkShowInstalledProvidersOnly.Checked)
                {
                    if (provDef.InstallationStatus == enProviderInstallationStatus.IsInstalled)
                    {
                        if (provDef.AvailableForSelection)
                            this.lstSelectedProviders.Items.Add(_connMgr.ProviderDefinitions[i].Key);
                        else
                            this.lstSupportedProviders.Items.Add(_connMgr.ProviderDefinitions[i].Key);
                    }
                    else
                    {
                        //update provdef to make it not selected
                        RefreshProviderDefinition(provDef, false);
                    }
                }
                else
                {
                    if (provDef.AvailableForSelection)
                        this.lstSelectedProviders.Items.Add(_connMgr.ProviderDefinitions[i].Key);
                    else
                        this.lstSupportedProviders.Items.Add(_connMgr.ProviderDefinitions[i].Key);
                }
            }
            ResetSelectorControls();
        }


        private void RefreshProviderDefinition(PFProviderDefinition provDef, bool availableForSelection)
        {
            _connMgr.UpdateProviderAvailableForSelectionStatus(provDef.ProviderName, availableForSelection); 
        }

    }//end class
}//end namespace
