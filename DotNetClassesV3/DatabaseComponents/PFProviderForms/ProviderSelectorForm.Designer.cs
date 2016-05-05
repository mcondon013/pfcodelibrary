namespace PFProviderForms
{
    partial class ProviderSelectorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.lstSupportedProviders = new System.Windows.Forms.ListBox();
            this.lblSupportedProviders = new System.Windows.Forms.Label();
            this.lstSelectedProviders = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdSelectRange = new System.Windows.Forms.Button();
            this.cmdSelectAll = new System.Windows.Forms.Button();
            this.selectFormToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cmdDeselectRange = new System.Windows.Forms.Button();
            this.cmdDeselectAll = new System.Windows.Forms.Button();
            this.chkShowInstalledProvidersOnly = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.Location = new System.Drawing.Point(461, 279);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(98, 34);
            this.cmdExit.TabIndex = 0;
            this.cmdExit.Text = "&Exit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(461, 62);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(98, 34);
            this.cmdSave.TabIndex = 1;
            this.cmdSave.Text = "&Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // lstSupportedProviders
            // 
            this.lstSupportedProviders.FormattingEnabled = true;
            this.lstSupportedProviders.Items.AddRange(new object[] {
            "SQL Server",
            "DB2",
            "SQLCE 3.5"});
            this.lstSupportedProviders.Location = new System.Drawing.Point(41, 62);
            this.lstSupportedProviders.MultiColumn = true;
            this.lstSupportedProviders.Name = "lstSupportedProviders";
            this.lstSupportedProviders.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSupportedProviders.Size = new System.Drawing.Size(158, 251);
            this.lstSupportedProviders.TabIndex = 2;
            // 
            // lblSupportedProviders
            // 
            this.lblSupportedProviders.AutoSize = true;
            this.lblSupportedProviders.Location = new System.Drawing.Point(41, 43);
            this.lblSupportedProviders.Name = "lblSupportedProviders";
            this.lblSupportedProviders.Size = new System.Drawing.Size(103, 13);
            this.lblSupportedProviders.TabIndex = 3;
            this.lblSupportedProviders.Text = "Supported Providers";
            // 
            // lstSelectedProviders
            // 
            this.lstSelectedProviders.FormattingEnabled = true;
            this.lstSelectedProviders.Location = new System.Drawing.Point(297, 62);
            this.lstSelectedProviders.Name = "lstSelectedProviders";
            this.lstSelectedProviders.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSelectedProviders.Size = new System.Drawing.Size(149, 251);
            this.lstSelectedProviders.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(297, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Selected Providers";
            // 
            // cmdSelectRange
            // 
            this.cmdSelectRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSelectRange.Location = new System.Drawing.Point(217, 68);
            this.cmdSelectRange.Name = "cmdSelectRange";
            this.cmdSelectRange.Size = new System.Drawing.Size(56, 28);
            this.cmdSelectRange.TabIndex = 6;
            this.cmdSelectRange.Text = ">";
            this.cmdSelectRange.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.selectFormToolTip.SetToolTip(this.cmdSelectRange, "Select one or more supported providers.");
            this.cmdSelectRange.UseVisualStyleBackColor = true;
            this.cmdSelectRange.Click += new System.EventHandler(this.cmdSelectRange_Click);
            // 
            // cmdSelectAll
            // 
            this.cmdSelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSelectAll.Location = new System.Drawing.Point(217, 112);
            this.cmdSelectAll.Name = "cmdSelectAll";
            this.cmdSelectAll.Size = new System.Drawing.Size(56, 28);
            this.cmdSelectAll.TabIndex = 7;
            this.cmdSelectAll.Text = ">>";
            this.selectFormToolTip.SetToolTip(this.cmdSelectAll, "Select all supported providers.");
            this.cmdSelectAll.UseVisualStyleBackColor = true;
            this.cmdSelectAll.Click += new System.EventHandler(this.cmdSelectAll_Click);
            // 
            // cmdDeselectRange
            // 
            this.cmdDeselectRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDeselectRange.Location = new System.Drawing.Point(217, 232);
            this.cmdDeselectRange.Name = "cmdDeselectRange";
            this.cmdDeselectRange.Size = new System.Drawing.Size(56, 28);
            this.cmdDeselectRange.TabIndex = 8;
            this.cmdDeselectRange.Text = "<";
            this.selectFormToolTip.SetToolTip(this.cmdDeselectRange, "Remove one or more providers from selected list.");
            this.cmdDeselectRange.UseVisualStyleBackColor = true;
            this.cmdDeselectRange.Click += new System.EventHandler(this.cmdDeselectRange_Click);
            // 
            // cmdDeselectAll
            // 
            this.cmdDeselectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDeselectAll.Location = new System.Drawing.Point(217, 279);
            this.cmdDeselectAll.Name = "cmdDeselectAll";
            this.cmdDeselectAll.Size = new System.Drawing.Size(56, 28);
            this.cmdDeselectAll.TabIndex = 9;
            this.cmdDeselectAll.Text = "<<";
            this.selectFormToolTip.SetToolTip(this.cmdDeselectAll, "Remove all providers from selected list.");
            this.cmdDeselectAll.UseVisualStyleBackColor = true;
            this.cmdDeselectAll.Click += new System.EventHandler(this.cmdDeselectAll_Click);
            // 
            // chkShowInstalledProvidersOnly
            // 
            this.chkShowInstalledProvidersOnly.AutoSize = true;
            this.chkShowInstalledProvidersOnly.Location = new System.Drawing.Point(168, 319);
            this.chkShowInstalledProvidersOnly.Name = "chkShowInstalledProvidersOnly";
            this.chkShowInstalledProvidersOnly.Size = new System.Drawing.Size(166, 17);
            this.chkShowInstalledProvidersOnly.TabIndex = 10;
            this.chkShowInstalledProvidersOnly.Text = "Show Installed Providers Only";
            this.chkShowInstalledProvidersOnly.UseVisualStyleBackColor = true;
            this.chkShowInstalledProvidersOnly.CheckedChanged += new System.EventHandler(this.chkShowInstalledProvidersOnly_CheckedChanged);
            // 
            // ProviderSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 366);
            this.Controls.Add(this.chkShowInstalledProvidersOnly);
            this.Controls.Add(this.cmdDeselectAll);
            this.Controls.Add(this.cmdDeselectRange);
            this.Controls.Add(this.cmdSelectAll);
            this.Controls.Add(this.cmdSelectRange);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstSelectedProviders);
            this.Controls.Add(this.lblSupportedProviders);
            this.Controls.Add(this.lstSupportedProviders);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ProviderSelectorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Providers to Use with Application";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PFWindowsForm_FormClosing);
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.ListBox lstSupportedProviders;
        private System.Windows.Forms.Label lblSupportedProviders;
        private System.Windows.Forms.ListBox lstSelectedProviders;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdSelectRange;
        private System.Windows.Forms.ToolTip selectFormToolTip;
        private System.Windows.Forms.Button cmdSelectAll;
        private System.Windows.Forms.Button cmdDeselectRange;
        private System.Windows.Forms.Button cmdDeselectAll;
        private System.Windows.Forms.CheckBox chkShowInstalledProvidersOnly;
    }
}