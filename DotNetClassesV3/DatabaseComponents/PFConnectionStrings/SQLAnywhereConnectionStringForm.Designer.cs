namespace PFConnectionStrings
{
    partial class SQLAnywhereConnectionStringForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLAnywhereConnectionStringForm));
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
            this.cmdGetDatabaseFileName = new System.Windows.Forms.Button();
            this.MainFormToolbar = new System.Windows.Forms.ToolStrip();
            this.toolbtnNew = new System.Windows.Forms.ToolStripButton();
            this.toolbtnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolbtnSave = new System.Windows.Forms.ToolStripButton();
            this.toolbtnPrint = new System.Windows.Forms.ToolStripButton();
            this.toolbtnPrintPreview = new System.Windows.Forms.ToolStripButton();
            this.toolbtnCancel = new System.Windows.Forms.ToolStripButton();
            this.toolbarHelp = new System.Windows.Forms.ToolStripButton();
            this.txtDatabasePlatform = new System.Windows.Forms.TextBox();
            this.txtEncryptedPassword = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtEncryption = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDatabaseKey = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDatabaseFile = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDataSource = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.formMenu.SuspendLayout();
            this.MainFormToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(170, 478);
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
            this.cmdOK.Location = new System.Drawing.Point(170, 429);
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
            this.lblDatabasePlatform.Location = new System.Drawing.Point(23, 77);
            this.lblDatabasePlatform.Name = "lblDatabasePlatform";
            this.lblDatabasePlatform.Size = new System.Drawing.Size(94, 13);
            this.lblDatabasePlatform.TabIndex = 0;
            this.lblDatabasePlatform.Text = "Database Platform";
            // 
            // lblInputPrompt
            // 
            this.lblInputPrompt.AutoSize = true;
            this.lblInputPrompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInputPrompt.Location = new System.Drawing.Point(23, 130);
            this.lblInputPrompt.Name = "lblInputPrompt";
            this.lblInputPrompt.Size = new System.Drawing.Size(260, 16);
            this.lblInputPrompt.TabIndex = 4;
            this.lblInputPrompt.Text = "Enter Connection String Key Values Below:";
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectionString.Location = new System.Drawing.Point(23, 321);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(112, 16);
            this.lblConnectionString.TabIndex = 24;
            this.lblConnectionString.Text = "Connection String";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(25, 340);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(456, 71);
            this.txtConnectionString.TabIndex = 25;
            // 
            // lblConnectionName
            // 
            this.lblConnectionName.AutoSize = true;
            this.lblConnectionName.Location = new System.Drawing.Point(23, 97);
            this.lblConnectionName.Name = "lblConnectionName";
            this.lblConnectionName.Size = new System.Drawing.Size(92, 13);
            this.lblConnectionName.TabIndex = 2;
            this.lblConnectionName.Text = "Connection Name";
            // 
            // txtConnectionName
            // 
            this.txtConnectionName.Location = new System.Drawing.Point(128, 97);
            this.txtConnectionName.Name = "txtConnectionName";
            this.txtConnectionName.Size = new System.Drawing.Size(353, 20);
            this.txtConnectionName.TabIndex = 3;
            // 
            // cmdBuildConnectionString
            // 
            this.cmdBuildConnectionString.Location = new System.Drawing.Point(383, 128);
            this.cmdBuildConnectionString.Name = "cmdBuildConnectionString";
            this.cmdBuildConnectionString.Size = new System.Drawing.Size(98, 20);
            this.cmdBuildConnectionString.TabIndex = 29;
            this.cmdBuildConnectionString.Text = "Build\r\nConnection\r\nString";
            this.formToolTips.SetToolTip(this.cmdBuildConnectionString, "Builds a connection string based on  properties displayed on this form.");
            this.cmdBuildConnectionString.UseVisualStyleBackColor = true;
            this.cmdBuildConnectionString.Click += new System.EventHandler(this.cmdBuildConnectionString_Click);
            // 
            // cmdParseConnectionString
            // 
            this.cmdParseConnectionString.Location = new System.Drawing.Point(383, 317);
            this.cmdParseConnectionString.Name = "cmdParseConnectionString";
            this.cmdParseConnectionString.Size = new System.Drawing.Size(98, 24);
            this.cmdParseConnectionString.TabIndex = 30;
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
            this.formMenu.Size = new System.Drawing.Size(518, 24);
            this.formMenu.TabIndex = 31;
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
            this.cmdSave.Location = new System.Drawing.Point(383, 70);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(98, 20);
            this.cmdSave.TabIndex = 28;
            this.cmdSave.Text = "&Save";
            this.formToolTips.SetToolTip(this.cmdSave, "Saves connection definition to database on disk using the connection name as ID.");
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSaveConnectionDefinition_Click);
            // 
            // cmdGetDatabaseFileName
            // 
            this.cmdGetDatabaseFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetDatabaseFileName.Location = new System.Drawing.Point(443, 160);
            this.cmdGetDatabaseFileName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmdGetDatabaseFileName.Name = "cmdGetDatabaseFileName";
            this.cmdGetDatabaseFileName.Size = new System.Drawing.Size(38, 14);
            this.cmdGetDatabaseFileName.TabIndex = 7;
            this.cmdGetDatabaseFileName.Text = "•••";
            this.formToolTips.SetToolTip(this.cmdGetDatabaseFileName, "Use File Open Dialog to select path to a database file.");
            this.cmdGetDatabaseFileName.UseVisualStyleBackColor = true;
            this.cmdGetDatabaseFileName.Click += new System.EventHandler(this.cmdGetDatabaseFileName_Click);
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
            this.MainFormToolbar.Size = new System.Drawing.Size(518, 25);
            this.MainFormToolbar.TabIndex = 32;
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
            this.txtDatabasePlatform.Size = new System.Drawing.Size(234, 20);
            this.txtDatabasePlatform.TabIndex = 1;
            // 
            // txtEncryptedPassword
            // 
            this.txtEncryptedPassword.Location = new System.Drawing.Point(361, 278);
            this.txtEncryptedPassword.Name = "txtEncryptedPassword";
            this.txtEncryptedPassword.Size = new System.Drawing.Size(120, 20);
            this.txtEncryptedPassword.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(271, 282);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Encrypted Pwd:";
            // 
            // txtEncryption
            // 
            this.txtEncryption.Location = new System.Drawing.Point(127, 278);
            this.txtEncryption.Name = "txtEncryption";
            this.txtEncryption.Size = new System.Drawing.Size(120, 20);
            this.txtEncryption.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 282);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Encryption:";
            // 
            // txtDatabaseKey
            // 
            this.txtDatabaseKey.Location = new System.Drawing.Point(361, 250);
            this.txtDatabaseKey.Name = "txtDatabaseKey";
            this.txtDatabaseKey.Size = new System.Drawing.Size(120, 20);
            this.txtDatabaseKey.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(270, 254);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Database Key:";
            // 
            // txtDatabaseFile
            // 
            this.txtDatabaseFile.Location = new System.Drawing.Point(126, 173);
            this.txtDatabaseFile.Name = "txtDatabaseFile";
            this.txtDatabaseFile.Size = new System.Drawing.Size(355, 20);
            this.txtDatabaseFile.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Database File:";
            // 
            // txtDataSource
            // 
            this.txtDataSource.Location = new System.Drawing.Point(126, 198);
            this.txtDataSource.Name = "txtDataSource";
            this.txtDataSource.Size = new System.Drawing.Size(121, 20);
            this.txtDataSource.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Data Source:";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(359, 225);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(120, 19);
            this.txtPassword.TabIndex = 19;
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(359, 199);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(122, 19);
            this.txtUsername.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(270, 227);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(268, 202);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Username:";
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDatabaseName.Location = new System.Drawing.Point(126, 251);
            this.txtDatabaseName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(121, 19);
            this.txtDatabaseName.TabIndex = 13;
            // 
            // txtServerName
            // 
            this.txtServerName.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerName.Location = new System.Drawing.Point(126, 225);
            this.txtServerName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(121, 19);
            this.txtServerName.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 254);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Database Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 225);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Server Name:";
            // 
            // SQLAnywhereConnectionStringForm
            // 
            this.AcceptButton = this.cmdBuildConnectionString;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(518, 530);
            this.Controls.Add(this.cmdGetDatabaseFileName);
            this.Controls.Add(this.txtEncryptedPassword);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtEncryption);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtDatabaseKey);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtDatabaseFile);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDataSource);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDatabaseName);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
            this.Name = "SQLAnywhereConnectionStringForm";
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
        private System.Windows.Forms.ToolStripButton toolbtnCancel;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.TextBox txtDatabasePlatform;
        internal System.Windows.Forms.TextBox txtEncryptedPassword;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.TextBox txtEncryption;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.TextBox txtDatabaseKey;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.TextBox txtDatabaseFile;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox txtDataSource;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox txtPassword;
        internal System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtDatabaseName;
        internal System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdGetDatabaseFileName;
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