namespace PFConnectionStrings
{
    partial class OdbcConnectionStringForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OdbcConnectionStringForm));
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.lblDatabasePlatform = new System.Windows.Forms.Label();
            this.lblInputPrompt = new System.Windows.Forms.Label();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.lblConnectionName = new System.Windows.Forms.Label();
            this.txtConnectionName = new System.Windows.Forms.TextBox();
            this.cmdBuildConnectionString = new System.Windows.Forms.Button();
            this.cmdParseConnectionString = new System.Windows.Forms.Button();
            this.txtDriverName = new System.Windows.Forms.TextBox();
            this.txtDsnName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.formMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePageSetupSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFilePageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileDeleteSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileCancelSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConnectionString = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConnectionStringAccept = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConnectionStringBuild = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConnectionStringVerify = new System.Windows.Forms.ToolStripMenuItem();
            this.formToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.cmdSave = new System.Windows.Forms.Button();
            this.MainFormToolbar = new System.Windows.Forms.ToolStrip();
            this.toolbtnNew = new System.Windows.Forms.ToolStripButton();
            this.toolbtnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolbtnSave = new System.Windows.Forms.ToolStripButton();
            this.toolbtnPrint = new System.Windows.Forms.ToolStripButton();
            this.toolbtnPrintPreview = new System.Windows.Forms.ToolStripButton();
            this.toolbtnCancel = new System.Windows.Forms.ToolStripButton();
            this.toolbarHelp = new System.Windows.Forms.ToolStripButton();
            this.txtDatabasePlatform = new System.Windows.Forms.TextBox();
            this.formMenu.SuspendLayout();
            this.MainFormToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(128, 395);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(192, 26);
            this.cmdCancel.TabIndex = 12;
            this.cmdCancel.Text = "&Cancel";
            this.formToolTips.SetToolTip(this.cmdCancel, "Cancels connection string definition and returns to caller.");
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOK.Location = new System.Drawing.Point(128, 353);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(192, 26);
            this.cmdOK.TabIndex = 11;
            this.cmdOK.Text = "  &Accept";
            this.formToolTips.SetToolTip(this.cmdOK, "Accepts current connecton definition and returns to caller.");
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // lblDatabasePlatform
            // 
            this.lblDatabasePlatform.AutoSize = true;
            this.lblDatabasePlatform.Location = new System.Drawing.Point(26, 74);
            this.lblDatabasePlatform.Name = "lblDatabasePlatform";
            this.lblDatabasePlatform.Size = new System.Drawing.Size(94, 13);
            this.lblDatabasePlatform.TabIndex = 0;
            this.lblDatabasePlatform.Text = "Database Platform";
            // 
            // lblInputPrompt
            // 
            this.lblInputPrompt.AutoSize = true;
            this.lblInputPrompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInputPrompt.Location = new System.Drawing.Point(26, 130);
            this.lblInputPrompt.Name = "lblInputPrompt";
            this.lblInputPrompt.Size = new System.Drawing.Size(260, 16);
            this.lblInputPrompt.TabIndex = 4;
            this.lblInputPrompt.Text = "Enter Connection String Key Values Below:";
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectionString.Location = new System.Drawing.Point(24, 242);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(112, 16);
            this.lblConnectionString.TabIndex = 9;
            this.lblConnectionString.Text = "Connection String";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(26, 261);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(386, 71);
            this.txtConnectionString.TabIndex = 10;
            // 
            // lblConnectionName
            // 
            this.lblConnectionName.AutoSize = true;
            this.lblConnectionName.Location = new System.Drawing.Point(24, 98);
            this.lblConnectionName.Name = "lblConnectionName";
            this.lblConnectionName.Size = new System.Drawing.Size(92, 13);
            this.lblConnectionName.TabIndex = 2;
            this.lblConnectionName.Text = "Connection Name";
            // 
            // txtConnectionName
            // 
            this.txtConnectionName.Location = new System.Drawing.Point(128, 97);
            this.txtConnectionName.Name = "txtConnectionName";
            this.txtConnectionName.Size = new System.Drawing.Size(284, 20);
            this.txtConnectionName.TabIndex = 3;
            // 
            // cmdBuildConnectionString
            // 
            this.cmdBuildConnectionString.Location = new System.Drawing.Point(314, 130);
            this.cmdBuildConnectionString.Name = "cmdBuildConnectionString";
            this.cmdBuildConnectionString.Size = new System.Drawing.Size(98, 20);
            this.cmdBuildConnectionString.TabIndex = 14;
            this.cmdBuildConnectionString.Text = "Build\r\nConnection\r\nString";
            this.formToolTips.SetToolTip(this.cmdBuildConnectionString, "Builds a connection string based on  properties displayed on this form.");
            this.cmdBuildConnectionString.UseVisualStyleBackColor = true;
            this.cmdBuildConnectionString.Click += new System.EventHandler(this.cmdBuildConnectionString_Click);
            // 
            // cmdParseConnectionString
            // 
            this.cmdParseConnectionString.Location = new System.Drawing.Point(314, 238);
            this.cmdParseConnectionString.Name = "cmdParseConnectionString";
            this.cmdParseConnectionString.Size = new System.Drawing.Size(98, 24);
            this.cmdParseConnectionString.TabIndex = 15;
            this.cmdParseConnectionString.Text = "Verify";
            this.formToolTips.SetToolTip(this.cmdParseConnectionString, "Verifies whether current connection string can connect to the database. ");
            this.cmdParseConnectionString.UseVisualStyleBackColor = true;
            this.cmdParseConnectionString.Click += new System.EventHandler(this.cmdVerifyConnectionString_Click);
            // 
            // txtDriverName
            // 
            this.txtDriverName.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDriverName.Location = new System.Drawing.Point(128, 191);
            this.txtDriverName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtDriverName.Name = "txtDriverName";
            this.txtDriverName.Size = new System.Drawing.Size(284, 19);
            this.txtDriverName.TabIndex = 8;
            // 
            // txtDsnName
            // 
            this.txtDsnName.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDsnName.Location = new System.Drawing.Point(128, 166);
            this.txtDsnName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtDsnName.Name = "txtDsnName";
            this.txtDsnName.Size = new System.Drawing.Size(284, 19);
            this.txtDsnName.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 192);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Driver Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 167);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "DSN Name:";
            // 
            // formMenu
            // 
            this.formMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuConnectionString});
            this.formMenu.Location = new System.Drawing.Point(0, 0);
            this.formMenu.Name = "formMenu";
            this.formMenu.Size = new System.Drawing.Size(446, 24);
            this.formMenu.TabIndex = 16;
            this.formMenu.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.mnuFileSave,
            this.mnuFilePageSetupSeparator,
            this.mnuFilePageSetup,
            this.mnuFilePrint,
            this.mnuFilePrintPreview,
            this.mnuFileDeleteSeparator,
            this.mnuFileDelete,
            this.mnuFileCancelSeparator,
            this.mnuCancel});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.Size = new System.Drawing.Size(143, 22);
            this.mnuFileNew.Tag = "";
            this.mnuFileNew.Text = "&New";
            this.mnuFileNew.ToolTipText = "Erases current input so that new connection can be defined.";
            this.mnuFileNew.Click += new System.EventHandler(this.mnuFileNew_Click);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(143, 22);
            this.mnuFileOpen.Text = "&Open";
            this.mnuFileOpen.ToolTipText = "Opens a previously saved connection definition.";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(143, 22);
            this.mnuFileSave.Text = "&Save";
            this.mnuFileSave.ToolTipText = "Saves connection definition to database on disk using the connection name as ID.";
            this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // mnuFilePageSetupSeparator
            // 
            this.mnuFilePageSetupSeparator.Name = "mnuFilePageSetupSeparator";
            this.mnuFilePageSetupSeparator.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuFilePageSetup
            // 
            this.mnuFilePageSetup.Name = "mnuFilePageSetup";
            this.mnuFilePageSetup.Size = new System.Drawing.Size(143, 22);
            this.mnuFilePageSetup.Text = "Page Set&up";
            this.mnuFilePageSetup.Click += new System.EventHandler(this.mnuFilePageSetup_Click);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Name = "mnuFilePrint";
            this.mnuFilePrint.Size = new System.Drawing.Size(143, 22);
            this.mnuFilePrint.Text = "&Print";
            this.mnuFilePrint.Click += new System.EventHandler(this.mnuFilePrint_Click);
            // 
            // mnuFilePrintPreview
            // 
            this.mnuFilePrintPreview.Name = "mnuFilePrintPreview";
            this.mnuFilePrintPreview.Size = new System.Drawing.Size(143, 22);
            this.mnuFilePrintPreview.Text = "Print Pre&view";
            this.mnuFilePrintPreview.Click += new System.EventHandler(this.mnuFilePrintPreview_Click);
            // 
            // mnuFileDeleteSeparator
            // 
            this.mnuFileDeleteSeparator.Name = "mnuFileDeleteSeparator";
            this.mnuFileDeleteSeparator.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuFileDelete
            // 
            this.mnuFileDelete.Name = "mnuFileDelete";
            this.mnuFileDelete.Size = new System.Drawing.Size(143, 22);
            this.mnuFileDelete.Text = "&Delete";
            this.mnuFileDelete.Click += new System.EventHandler(this.mnuFileDelete_Click);
            // 
            // mnuFileCancelSeparator
            // 
            this.mnuFileCancelSeparator.Name = "mnuFileCancelSeparator";
            this.mnuFileCancelSeparator.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuCancel
            // 
            this.mnuCancel.Name = "mnuCancel";
            this.mnuCancel.Size = new System.Drawing.Size(143, 22);
            this.mnuCancel.Text = "&Cancel";
            this.mnuCancel.ToolTipText = "Cancels connection string definition and returns to caller.";
            this.mnuCancel.Click += new System.EventHandler(this.mnuCancel_Click);
            // 
            // mnuConnectionString
            // 
            this.mnuConnectionString.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuConnectionStringAccept,
            this.mnuConnectionStringBuild,
            this.mnuConnectionStringVerify});
            this.mnuConnectionString.Name = "mnuConnectionString";
            this.mnuConnectionString.Size = new System.Drawing.Size(115, 20);
            this.mnuConnectionString.Text = "&Connection String";
            // 
            // mnuConnectionStringAccept
            // 
            this.mnuConnectionStringAccept.Name = "mnuConnectionStringAccept";
            this.mnuConnectionStringAccept.Size = new System.Drawing.Size(111, 22);
            this.mnuConnectionStringAccept.Text = "&Accept";
            this.mnuConnectionStringAccept.ToolTipText = "Accepts current connecton definition and returns to caller.";
            this.mnuConnectionStringAccept.Click += new System.EventHandler(this.mnuConnectionStringAccept_Click);
            // 
            // mnuConnectionStringBuild
            // 
            this.mnuConnectionStringBuild.Name = "mnuConnectionStringBuild";
            this.mnuConnectionStringBuild.Size = new System.Drawing.Size(111, 22);
            this.mnuConnectionStringBuild.Text = "&Build";
            this.mnuConnectionStringBuild.ToolTipText = "Builds a connection string based on  properties displayed on this form.";
            this.mnuConnectionStringBuild.Click += new System.EventHandler(this.mnuConnectionStringBuild_Click);
            // 
            // mnuConnectionStringVerify
            // 
            this.mnuConnectionStringVerify.Name = "mnuConnectionStringVerify";
            this.mnuConnectionStringVerify.Size = new System.Drawing.Size(111, 22);
            this.mnuConnectionStringVerify.Text = "&Verify";
            this.mnuConnectionStringVerify.ToolTipText = "Verifies whether current connection string can connect to the database.";
            this.mnuConnectionStringVerify.Click += new System.EventHandler(this.mnuConnectionStringVerify_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(314, 74);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(98, 20);
            this.cmdSave.TabIndex = 13;
            this.cmdSave.Text = "&Save";
            this.formToolTips.SetToolTip(this.cmdSave, "Saves connection definition to database on disk using the connection name as ID.");
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSaveConnectionDefinition_Click);
            // 
            // MainFormToolbar
            // 
            this.MainFormToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolbtnNew,
            this.toolbtnOpen,
            this.toolbtnSave,
            this.toolbtnPrint,
            this.toolbtnPrintPreview,
            this.toolbtnCancel,
            this.toolbarHelp});
            this.MainFormToolbar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MainFormToolbar.Location = new System.Drawing.Point(0, 24);
            this.MainFormToolbar.Name = "MainFormToolbar";
            this.MainFormToolbar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.MainFormToolbar.Size = new System.Drawing.Size(446, 25);
            this.MainFormToolbar.TabIndex = 17;
            this.MainFormToolbar.Text = "MainFormToolbar";
            // 
            // toolbtnNew
            // 
            this.toolbtnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnNew.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnNew.Image")));
            this.toolbtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnNew.Name = "toolbtnNew";
            this.toolbtnNew.Size = new System.Drawing.Size(23, 22);
            this.toolbtnNew.Text = "New";
            this.toolbtnNew.ToolTipText = "Erases current input so that new connection can be defined.";
            this.toolbtnNew.Click += new System.EventHandler(this.toolbtnNew_Click);
            // 
            // toolbtnOpen
            // 
            this.toolbtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnOpen.Image")));
            this.toolbtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnOpen.Name = "toolbtnOpen";
            this.toolbtnOpen.Size = new System.Drawing.Size(23, 22);
            this.toolbtnOpen.Text = "Open";
            this.toolbtnOpen.ToolTipText = "Opens a previously saved connection definition.";
            this.toolbtnOpen.Click += new System.EventHandler(this.toolbtnOpen_Click);
            // 
            // toolbtnSave
            // 
            this.toolbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnSave.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnSave.Image")));
            this.toolbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnSave.Name = "toolbtnSave";
            this.toolbtnSave.Size = new System.Drawing.Size(23, 22);
            this.toolbtnSave.Text = "Save";
            this.toolbtnSave.ToolTipText = "Saves connection definition to database on disk using the connection name as ID.";
            this.toolbtnSave.Click += new System.EventHandler(this.toolbtnSave_Click);
            // 
            // toolbtnPrint
            // 
            this.toolbtnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnPrint.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnPrint.Image")));
            this.toolbtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnPrint.Margin = new System.Windows.Forms.Padding(10, 1, 0, 2);
            this.toolbtnPrint.Name = "toolbtnPrint";
            this.toolbtnPrint.Size = new System.Drawing.Size(23, 22);
            this.toolbtnPrint.Text = "Print";
            this.toolbtnPrint.Click += new System.EventHandler(this.toolbtnPrint_Click);
            // 
            // toolbtnPrintPreview
            // 
            this.toolbtnPrintPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnPrintPreview.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnPrintPreview.Image")));
            this.toolbtnPrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnPrintPreview.Name = "toolbtnPrintPreview";
            this.toolbtnPrintPreview.Size = new System.Drawing.Size(23, 22);
            this.toolbtnPrintPreview.Text = "PrintPreview";
            this.toolbtnPrintPreview.ToolTipText = "Print Preview";
            this.toolbtnPrintPreview.Click += new System.EventHandler(this.toolbtnPrintPreview_Click);
            // 
            // toolbtnCancel
            // 
            this.toolbtnCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnCancel.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnCancel.Image")));
            this.toolbtnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnCancel.Margin = new System.Windows.Forms.Padding(50, 1, 25, 2);
            this.toolbtnCancel.Name = "toolbtnCancel";
            this.toolbtnCancel.Size = new System.Drawing.Size(23, 22);
            this.toolbtnCancel.Text = "Cancel";
            this.toolbtnCancel.ToolTipText = "Cancels connection string definition and returns to caller.";
            this.toolbtnCancel.Click += new System.EventHandler(this.toolbtnCancel_Click);
            // 
            // toolbarHelp
            // 
            this.toolbarHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolbarHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbarHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolbarHelp.Image")));
            this.toolbarHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbarHelp.Name = "toolbarHelp";
            this.toolbarHelp.Padding = new System.Windows.Forms.Padding(0, 0, 65, 0);
            this.toolbarHelp.Size = new System.Drawing.Size(85, 22);
            this.toolbarHelp.Text = "Help";
            this.toolbarHelp.Click += new System.EventHandler(this.toolbarHelp_Click);
            // 
            // txtDatabasePlatform
            // 
            this.txtDatabasePlatform.Location = new System.Drawing.Point(128, 71);
            this.txtDatabasePlatform.Name = "txtDatabasePlatform";
            this.txtDatabasePlatform.ReadOnly = true;
            this.txtDatabasePlatform.Size = new System.Drawing.Size(175, 20);
            this.txtDatabasePlatform.TabIndex = 1;
            // 
            // OdbcConnectionStringForm
            // 
            this.AcceptButton = this.cmdBuildConnectionString;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(446, 456);
            this.Controls.Add(this.txtDatabasePlatform);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.MainFormToolbar);
            this.Controls.Add(this.txtDriverName);
            this.Controls.Add(this.txtDsnName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdParseConnectionString);
            this.Controls.Add(this.cmdBuildConnectionString);
            this.Controls.Add(this.txtConnectionName);
            this.Controls.Add(this.lblConnectionName);
            this.Controls.Add(this.txtConnectionString);
            this.Controls.Add(this.lblConnectionString);
            this.Controls.Add(this.lblInputPrompt);
            this.Controls.Add(this.lblDatabasePlatform);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.formMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.formMenu;
            this.Name = "OdbcConnectionStringForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection String Definition Utility";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.formMenu.ResumeLayout(false);
            this.formMenu.PerformLayout();
            this.MainFormToolbar.ResumeLayout(false);
            this.MainFormToolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Label lblDatabasePlatform;
        private System.Windows.Forms.Label lblInputPrompt;
        private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label lblConnectionName;
        private System.Windows.Forms.TextBox txtConnectionName;
        private System.Windows.Forms.Button cmdBuildConnectionString;
        private System.Windows.Forms.Button cmdParseConnectionString;
        internal System.Windows.Forms.TextBox txtDriverName;
        internal System.Windows.Forms.TextBox txtDsnName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip formMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileNew;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
        private System.Windows.Forms.ToolStripSeparator mnuFileCancelSeparator;
        private System.Windows.Forms.ToolStripMenuItem mnuCancel;
        private System.Windows.Forms.ToolStripMenuItem mnuConnectionString;
        private System.Windows.Forms.ToolStripMenuItem mnuConnectionStringAccept;
        private System.Windows.Forms.ToolStripMenuItem mnuConnectionStringBuild;
        private System.Windows.Forms.ToolStripMenuItem mnuConnectionStringVerify;
        private System.Windows.Forms.ToolTip formToolTips;
        private System.Windows.Forms.ToolStrip MainFormToolbar;
        private System.Windows.Forms.ToolStripButton toolbtnNew;
        private System.Windows.Forms.ToolStripButton toolbtnOpen;
        private System.Windows.Forms.ToolStripButton toolbtnSave;
        private System.Windows.Forms.ToolStripButton toolbtnCancel;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.TextBox txtDatabasePlatform;
        private System.Windows.Forms.ToolStripButton toolbtnPrint;
        private System.Windows.Forms.ToolStripButton toolbtnPrintPreview;
        private System.Windows.Forms.ToolStripButton toolbarHelp;
        private System.Windows.Forms.ToolStripSeparator mnuFilePageSetupSeparator;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePageSetup;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrint;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrintPreview;
        private System.Windows.Forms.ToolStripSeparator mnuFileDeleteSeparator;
        private System.Windows.Forms.ToolStripMenuItem mnuFileDelete;
    }
}