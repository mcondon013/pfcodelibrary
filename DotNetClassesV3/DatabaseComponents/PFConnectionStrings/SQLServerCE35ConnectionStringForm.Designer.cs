﻿namespace PFConnectionStrings
{
    partial class SQLServerCE35ConnectionStringForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLServerCE35ConnectionStringForm));
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
            this.cmdGetDataSource = new System.Windows.Forms.Button();
            this.txtMaxDatabaseSize = new System.Windows.Forms.TextBox();
            this.txtMaxBufferSize = new System.Windows.Forms.TextBox();
            this.txtMaxTempFileSize = new System.Windows.Forms.TextBox();
            this.MainFormToolbar = new System.Windows.Forms.ToolStrip();
            this.toolbtnNew = new System.Windows.Forms.ToolStripButton();
            this.toolbtnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolbtnSave = new System.Windows.Forms.ToolStripButton();
            this.toolbtnPrint = new System.Windows.Forms.ToolStripButton();
            this.toolbtnPrintPreview = new System.Windows.Forms.ToolStripButton();
            this.toolbtnCancel = new System.Windows.Forms.ToolStripButton();
            this.toolbarHelp = new System.Windows.Forms.ToolStripButton();
            this.txtDatabasePlatform = new System.Windows.Forms.TextBox();
            this.cboEncryptionMode = new System.Windows.Forms.ComboBox();
            this.lblEncryptionMode = new System.Windows.Forms.Label();
            this.chkEncryptionOn = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtDataSource = new System.Windows.Forms.TextBox();
            this.lblDataSource = new System.Windows.Forms.Label();
            this.lblMaxDatabaseSize = new System.Windows.Forms.Label();
            this.lblMaxBufferSize = new System.Windows.Forms.Label();
            this.lblMaxTempFileSize = new System.Windows.Forms.Label();
            this.formMenu.SuspendLayout();
            this.MainFormToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(111, 474);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(192, 26);
            this.cmdCancel.TabIndex = 27;
            this.cmdCancel.Text = "&Cancel";
            this.formToolTips.SetToolTip(this.cmdCancel, "Cancels connection string definition and returns to caller.");
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOK.Location = new System.Drawing.Point(113, 427);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(192, 26);
            this.cmdOK.TabIndex = 26;
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
            this.lblDatabasePlatform.TabIndex = 2;
            this.lblDatabasePlatform.Text = "Database Platform";
            // 
            // lblInputPrompt
            // 
            this.lblInputPrompt.AutoSize = true;
            this.lblInputPrompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInputPrompt.Location = new System.Drawing.Point(26, 130);
            this.lblInputPrompt.Name = "lblInputPrompt";
            this.lblInputPrompt.Size = new System.Drawing.Size(260, 16);
            this.lblInputPrompt.TabIndex = 7;
            this.lblInputPrompt.Text = "Enter Connection String Key Values Below:";
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectionString.Location = new System.Drawing.Point(25, 318);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(112, 16);
            this.lblConnectionString.TabIndex = 23;
            this.lblConnectionString.Text = "Connection String";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(26, 337);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(381, 71);
            this.txtConnectionString.TabIndex = 24;
            // 
            // lblConnectionName
            // 
            this.lblConnectionName.AutoSize = true;
            this.lblConnectionName.Location = new System.Drawing.Point(24, 98);
            this.lblConnectionName.Name = "lblConnectionName";
            this.lblConnectionName.Size = new System.Drawing.Size(92, 13);
            this.lblConnectionName.TabIndex = 5;
            this.lblConnectionName.Text = "Connection Name";
            // 
            // txtConnectionName
            // 
            this.txtConnectionName.Location = new System.Drawing.Point(128, 97);
            this.txtConnectionName.Name = "txtConnectionName";
            this.txtConnectionName.Size = new System.Drawing.Size(279, 20);
            this.txtConnectionName.TabIndex = 6;
            // 
            // cmdBuildConnectionString
            // 
            this.cmdBuildConnectionString.Location = new System.Drawing.Point(309, 130);
            this.cmdBuildConnectionString.Name = "cmdBuildConnectionString";
            this.cmdBuildConnectionString.Size = new System.Drawing.Size(98, 20);
            this.cmdBuildConnectionString.TabIndex = 8;
            this.cmdBuildConnectionString.Text = "Build\r\nConnection\r\nString";
            this.formToolTips.SetToolTip(this.cmdBuildConnectionString, "Builds a connection string based on  properties displayed on this form.");
            this.cmdBuildConnectionString.UseVisualStyleBackColor = true;
            this.cmdBuildConnectionString.Click += new System.EventHandler(this.cmdBuildConnectionString_Click);
            // 
            // cmdParseConnectionString
            // 
            this.cmdParseConnectionString.Location = new System.Drawing.Point(310, 314);
            this.cmdParseConnectionString.Name = "cmdParseConnectionString";
            this.cmdParseConnectionString.Size = new System.Drawing.Size(98, 24);
            this.cmdParseConnectionString.TabIndex = 25;
            this.cmdParseConnectionString.Text = "Verify";
            this.formToolTips.SetToolTip(this.cmdParseConnectionString, "Verifies whether current connection string can connect to the database. ");
            this.cmdParseConnectionString.UseVisualStyleBackColor = true;
            this.cmdParseConnectionString.Click += new System.EventHandler(this.cmdVerifyConnectionString_Click);
            // 
            // formMenu
            // 
            this.formMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuConnectionString});
            this.formMenu.Location = new System.Drawing.Point(0, 0);
            this.formMenu.Name = "formMenu";
            this.formMenu.Size = new System.Drawing.Size(434, 24);
            this.formMenu.TabIndex = 0;
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
            this.cmdSave.Location = new System.Drawing.Point(309, 70);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(98, 20);
            this.cmdSave.TabIndex = 4;
            this.cmdSave.Text = "&Save";
            this.formToolTips.SetToolTip(this.cmdSave, "Saves connection definition to database on disk using the connection name as ID.");
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSaveConnectionDefinition_Click);
            // 
            // cmdGetDataSource
            // 
            this.cmdGetDataSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetDataSource.Location = new System.Drawing.Point(369, 158);
            this.cmdGetDataSource.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmdGetDataSource.Name = "cmdGetDataSource";
            this.cmdGetDataSource.Size = new System.Drawing.Size(38, 14);
            this.cmdGetDataSource.TabIndex = 11;
            this.cmdGetDataSource.Text = "•••";
            this.formToolTips.SetToolTip(this.cmdGetDataSource, "Use File Open Dialog to select path to a database file.");
            this.cmdGetDataSource.UseVisualStyleBackColor = true;
            this.cmdGetDataSource.Click += new System.EventHandler(this.cmdGetDataSource_Click);
            // 
            // txtMaxDatabaseSize
            // 
            this.txtMaxDatabaseSize.Location = new System.Drawing.Point(176, 255);
            this.txtMaxDatabaseSize.Name = "txtMaxDatabaseSize";
            this.txtMaxDatabaseSize.Size = new System.Drawing.Size(52, 20);
            this.txtMaxDatabaseSize.TabIndex = 18;
            this.formToolTips.SetToolTip(this.txtMaxDatabaseSize, "Default is 128 (in megabytes).");
            // 
            // txtMaxBufferSize
            // 
            this.txtMaxBufferSize.Location = new System.Drawing.Point(355, 255);
            this.txtMaxBufferSize.Name = "txtMaxBufferSize";
            this.txtMaxBufferSize.Size = new System.Drawing.Size(52, 20);
            this.txtMaxBufferSize.TabIndex = 20;
            this.formToolTips.SetToolTip(this.txtMaxBufferSize, "Max is 640 (in Kilobytes)");
            // 
            // txtMaxTempFileSize
            // 
            this.txtMaxTempFileSize.Location = new System.Drawing.Point(176, 282);
            this.txtMaxTempFileSize.Name = "txtMaxTempFileSize";
            this.txtMaxTempFileSize.Size = new System.Drawing.Size(52, 20);
            this.txtMaxTempFileSize.TabIndex = 22;
            this.formToolTips.SetToolTip(this.txtMaxTempFileSize, "Default is 128 (in megabytes).");
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
            this.MainFormToolbar.Size = new System.Drawing.Size(434, 25);
            this.MainFormToolbar.TabIndex = 1;
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
            this.toolbtnCancel.ToolTipText = "Cancel";
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
            this.txtDatabasePlatform.TabIndex = 3;
            // 
            // cboEncryptionMode
            // 
            this.cboEncryptionMode.FormattingEnabled = true;
            this.cboEncryptionMode.Items.AddRange(new object[] {
            "EngineDefault",
            "PlatformDefault",
            "PPC2003Compatibility"});
            this.cboEncryptionMode.Location = new System.Drawing.Point(269, 221);
            this.cboEncryptionMode.Name = "cboEncryptionMode";
            this.cboEncryptionMode.Size = new System.Drawing.Size(139, 21);
            this.cboEncryptionMode.TabIndex = 16;
            // 
            // lblEncryptionMode
            // 
            this.lblEncryptionMode.AutoSize = true;
            this.lblEncryptionMode.Location = new System.Drawing.Point(173, 224);
            this.lblEncryptionMode.Name = "lblEncryptionMode";
            this.lblEncryptionMode.Size = new System.Drawing.Size(90, 13);
            this.lblEncryptionMode.TabIndex = 15;
            this.lblEncryptionMode.Text = "Encryption Mode:";
            // 
            // chkEncryptionOn
            // 
            this.chkEncryptionOn.AutoSize = true;
            this.chkEncryptionOn.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkEncryptionOn.Location = new System.Drawing.Point(28, 223);
            this.chkEncryptionOn.Name = "chkEncryptionOn";
            this.chkEncryptionOn.Size = new System.Drawing.Size(96, 17);
            this.chkEncryptionOn.TabIndex = 14;
            this.chkEncryptionOn.Text = "Encryption On:";
            this.chkEncryptionOn.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(114, 199);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(294, 19);
            this.txtPassword.TabIndex = 13;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(28, 198);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 12;
            this.lblPassword.Text = "Password:";
            // 
            // txtDataSource
            // 
            this.txtDataSource.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataSource.Location = new System.Drawing.Point(114, 173);
            this.txtDataSource.Name = "txtDataSource";
            this.txtDataSource.Size = new System.Drawing.Size(294, 19);
            this.txtDataSource.TabIndex = 10;
            // 
            // lblDataSource
            // 
            this.lblDataSource.AutoSize = true;
            this.lblDataSource.Location = new System.Drawing.Point(28, 173);
            this.lblDataSource.Name = "lblDataSource";
            this.lblDataSource.Size = new System.Drawing.Size(70, 13);
            this.lblDataSource.TabIndex = 9;
            this.lblDataSource.Text = "Data Source:";
            // 
            // lblMaxDatabaseSize
            // 
            this.lblMaxDatabaseSize.AutoSize = true;
            this.lblMaxDatabaseSize.Location = new System.Drawing.Point(28, 255);
            this.lblMaxDatabaseSize.Name = "lblMaxDatabaseSize";
            this.lblMaxDatabaseSize.Size = new System.Drawing.Size(124, 13);
            this.lblMaxDatabaseSize.TabIndex = 17;
            this.lblMaxDatabaseSize.Text = "Max Database Size (MB)";
            // 
            // lblMaxBufferSize
            // 
            this.lblMaxBufferSize.AutoSize = true;
            this.lblMaxBufferSize.Location = new System.Drawing.Point(234, 258);
            this.lblMaxBufferSize.Name = "lblMaxBufferSize";
            this.lblMaxBufferSize.Size = new System.Drawing.Size(104, 13);
            this.lblMaxBufferSize.TabIndex = 19;
            this.lblMaxBufferSize.Text = "Max Buffer Size (KB)";
            // 
            // lblMaxTempFileSize
            // 
            this.lblMaxTempFileSize.AutoSize = true;
            this.lblMaxTempFileSize.Location = new System.Drawing.Point(28, 281);
            this.lblMaxTempFileSize.Name = "lblMaxTempFileSize";
            this.lblMaxTempFileSize.Size = new System.Drawing.Size(124, 13);
            this.lblMaxTempFileSize.TabIndex = 21;
            this.lblMaxTempFileSize.Text = "Max Temp File Size (MB)";
            // 
            // SQLServerCE35ConnectionStringForm
            // 
            this.AcceptButton = this.cmdBuildConnectionString;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(434, 519);
            this.Controls.Add(this.txtMaxTempFileSize);
            this.Controls.Add(this.lblMaxTempFileSize);
            this.Controls.Add(this.lblMaxBufferSize);
            this.Controls.Add(this.txtMaxBufferSize);
            this.Controls.Add(this.txtMaxDatabaseSize);
            this.Controls.Add(this.lblMaxDatabaseSize);
            this.Controls.Add(this.cmdGetDataSource);
            this.Controls.Add(this.cboEncryptionMode);
            this.Controls.Add(this.lblEncryptionMode);
            this.Controls.Add(this.chkEncryptionOn);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtDataSource);
            this.Controls.Add(this.lblDataSource);
            this.Controls.Add(this.txtDatabasePlatform);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.MainFormToolbar);
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
            this.Name = "SQLServerCE35ConnectionStringForm";
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
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.TextBox txtDatabasePlatform;
        internal System.Windows.Forms.ComboBox cboEncryptionMode;
        private System.Windows.Forms.Label lblEncryptionMode;
        internal System.Windows.Forms.CheckBox chkEncryptionOn;
        internal System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        internal System.Windows.Forms.TextBox txtDataSource;
        private System.Windows.Forms.Label lblDataSource;
        private System.Windows.Forms.Button cmdGetDataSource;
        private System.Windows.Forms.Label lblMaxDatabaseSize;
        private System.Windows.Forms.TextBox txtMaxDatabaseSize;
        private System.Windows.Forms.Label lblMaxBufferSize;
        private System.Windows.Forms.TextBox txtMaxBufferSize;
        private System.Windows.Forms.Label lblMaxTempFileSize;
        private System.Windows.Forms.TextBox txtMaxTempFileSize;
        private System.Windows.Forms.ToolStripButton toolbtnCancel;
        private System.Windows.Forms.ToolStripSeparator mnuFilePageSetupSeparator;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePageSetup;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrint;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrintPreview;
        private System.Windows.Forms.ToolStripButton toolbtnPrint;
        private System.Windows.Forms.ToolStripButton toolbtnPrintPreview;
        private System.Windows.Forms.ToolStripButton toolbarHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuFileDelete;
        private System.Windows.Forms.ToolStripSeparator mnuFileDeleteSeparator;
    }
}