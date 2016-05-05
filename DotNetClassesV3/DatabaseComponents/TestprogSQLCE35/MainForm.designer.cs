namespace TestprogSQLCE35
{
    partial class MainForm
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
            this.cmdRunTests = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.grpTestsToRun = new System.Windows.Forms.GroupBox();
            this.cmdBuildQuery = new System.Windows.Forms.Button();
            this.chkConnectionTest = new System.Windows.Forms.CheckBox();
            this.chkCreateTable = new System.Windows.Forms.CheckBox();
            this.chkImportDataTable = new System.Windows.Forms.CheckBox();
            this.cmdSetDataSourceFilePath = new System.Windows.Forms.Button();
            this.grpQueryType = new System.Windows.Forms.GroupBox();
            this.optRsToDt = new System.Windows.Forms.RadioButton();
            this.optDataTable = new System.Windows.Forms.RadioButton();
            this.optRdrToDt = new System.Windows.Forms.RadioButton();
            this.optDataset = new System.Windows.Forms.RadioButton();
            this.optResultset = new System.Windows.Forms.RadioButton();
            this.optReader = new System.Windows.Forms.RadioButton();
            this.optNonQuery = new System.Windows.Forms.RadioButton();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.chkRunQuery = new System.Windows.Forms.CheckBox();
            this.cboEncryptionMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkEncryptionOn = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdSetDataSourceFolder = new System.Windows.Forms.Button();
            this.txtDataSource = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.chkCreateDatabaseTest = new System.Windows.Forms.CheckBox();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.mainMenuFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.mainFormToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.mainMenuOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.keyValsDataSet = new System.Data.DataSet();
            this.KeyValTable = new System.Data.DataTable();
            this.AppSetting = new System.Data.DataColumn();
            this.SettingValue = new System.Data.DataColumn();
            this.cmdShowHideOutputLog = new System.Windows.Forms.Button();
            this.MainMenu.SuspendLayout();
            this.grpTestsToRun.SuspendLayout();
            this.grpQueryType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.keyValsDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeyValTable)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(510, 399);
            this.cmdExit.Name = "cmdExit";
            this.appHelpProvider.SetShowHelp(this.cmdExit, true);
            this.cmdExit.Size = new System.Drawing.Size(93, 37);
            this.cmdExit.TabIndex = 2;
            this.cmdExit.Text = "E&xit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdRunTests
            // 
            this.appHelpProvider.SetHelpKeyword(this.cmdRunTests, "Run Tests");
            this.appHelpProvider.SetHelpNavigator(this.cmdRunTests, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.appHelpProvider.SetHelpString(this.cmdRunTests, "Help for Run Tests: See Help File.");
            this.cmdRunTests.Location = new System.Drawing.Point(510, 60);
            this.cmdRunTests.Name = "cmdRunTests";
            this.appHelpProvider.SetShowHelp(this.cmdRunTests, true);
            this.cmdRunTests.Size = new System.Drawing.Size(93, 37);
            this.cmdRunTests.TabIndex = 1;
            this.cmdRunTests.Text = "&Run Tests";
            this.cmdRunTests.UseVisualStyleBackColor = true;
            this.cmdRunTests.Click += new System.EventHandler(this.cmdRunTest_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(638, 24);
            this.MainMenu.TabIndex = 3;
            this.MainMenu.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(152, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // grpTestsToRun
            // 
            this.grpTestsToRun.Controls.Add(this.cmdBuildQuery);
            this.grpTestsToRun.Controls.Add(this.chkConnectionTest);
            this.grpTestsToRun.Controls.Add(this.chkCreateTable);
            this.grpTestsToRun.Controls.Add(this.chkImportDataTable);
            this.grpTestsToRun.Controls.Add(this.cmdSetDataSourceFilePath);
            this.grpTestsToRun.Controls.Add(this.grpQueryType);
            this.grpTestsToRun.Controls.Add(this.txtQuery);
            this.grpTestsToRun.Controls.Add(this.chkRunQuery);
            this.grpTestsToRun.Controls.Add(this.cboEncryptionMode);
            this.grpTestsToRun.Controls.Add(this.label3);
            this.grpTestsToRun.Controls.Add(this.chkEncryptionOn);
            this.grpTestsToRun.Controls.Add(this.txtPassword);
            this.grpTestsToRun.Controls.Add(this.label2);
            this.grpTestsToRun.Controls.Add(this.cmdSetDataSourceFolder);
            this.grpTestsToRun.Controls.Add(this.txtDataSource);
            this.grpTestsToRun.Controls.Add(this.label1);
            this.grpTestsToRun.Controls.Add(this.chkEraseOutputBeforeEachTest);
            this.grpTestsToRun.Controls.Add(this.chkCreateDatabaseTest);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 60);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 408);
            this.grpTestsToRun.TabIndex = 0;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // cmdBuildQuery
            // 
            this.cmdBuildQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBuildQuery.Location = new System.Drawing.Point(373, 189);
            this.cmdBuildQuery.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmdBuildQuery.Name = "cmdBuildQuery";
            this.cmdBuildQuery.Size = new System.Drawing.Size(38, 14);
            this.cmdBuildQuery.TabIndex = 65;
            this.cmdBuildQuery.Text = ">>";
            this.mainFormToolTips.SetToolTip(this.cmdBuildQuery, "Modify version 4.0 query with the Query Builder Form");
            this.cmdBuildQuery.UseVisualStyleBackColor = true;
            this.cmdBuildQuery.Click += new System.EventHandler(this.cmdBuildQuery_Click);
            // 
            // chkConnectionTest
            // 
            this.chkConnectionTest.AutoSize = true;
            this.chkConnectionTest.Location = new System.Drawing.Point(171, 189);
            this.chkConnectionTest.Name = "chkConnectionTest";
            this.chkConnectionTest.Size = new System.Drawing.Size(104, 17);
            this.chkConnectionTest.TabIndex = 64;
            this.chkConnectionTest.Text = "Connection Test";
            this.chkConnectionTest.UseVisualStyleBackColor = true;
            // 
            // chkCreateTable
            // 
            this.chkCreateTable.AutoSize = true;
            this.chkCreateTable.Location = new System.Drawing.Point(317, 150);
            this.chkCreateTable.Name = "chkCreateTable";
            this.chkCreateTable.Size = new System.Drawing.Size(96, 17);
            this.chkCreateTable.TabIndex = 63;
            this.chkCreateTable.Text = "&4 Create Table";
            this.chkCreateTable.UseVisualStyleBackColor = true;
            // 
            // chkImportDataTable
            // 
            this.chkImportDataTable.AutoSize = true;
            this.chkImportDataTable.Location = new System.Drawing.Point(171, 150);
            this.chkImportDataTable.Name = "chkImportDataTable";
            this.chkImportDataTable.Size = new System.Drawing.Size(117, 17);
            this.chkImportDataTable.TabIndex = 60;
            this.chkImportDataTable.Text = "&3 Import DataTable";
            this.chkImportDataTable.UseVisualStyleBackColor = true;
            // 
            // cmdSetDataSourceFilePath
            // 
            this.cmdSetDataSourceFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetDataSourceFilePath.Location = new System.Drawing.Point(321, 45);
            this.cmdSetDataSourceFilePath.Name = "cmdSetDataSourceFilePath";
            this.cmdSetDataSourceFilePath.Size = new System.Drawing.Size(38, 20);
            this.cmdSetDataSourceFilePath.TabIndex = 59;
            this.cmdSetDataSourceFilePath.Text = "@";
            this.mainFormToolTips.SetToolTip(this.cmdSetDataSourceFilePath, "Select file");
            this.cmdSetDataSourceFilePath.UseVisualStyleBackColor = true;
            this.cmdSetDataSourceFilePath.Click += new System.EventHandler(this.cmdSetDataSourceFilePath_Click);
            // 
            // grpQueryType
            // 
            this.grpQueryType.Controls.Add(this.optRsToDt);
            this.grpQueryType.Controls.Add(this.optDataTable);
            this.grpQueryType.Controls.Add(this.optRdrToDt);
            this.grpQueryType.Controls.Add(this.optDataset);
            this.grpQueryType.Controls.Add(this.optResultset);
            this.grpQueryType.Controls.Add(this.optReader);
            this.grpQueryType.Controls.Add(this.optNonQuery);
            this.grpQueryType.Location = new System.Drawing.Point(35, 341);
            this.grpQueryType.Name = "grpQueryType";
            this.grpQueryType.Size = new System.Drawing.Size(378, 77);
            this.grpQueryType.TabIndex = 58;
            this.grpQueryType.TabStop = false;
            this.grpQueryType.Text = "Query Type:";
            // 
            // optRsToDt
            // 
            this.optRsToDt.AutoSize = true;
            this.optRsToDt.Location = new System.Drawing.Point(197, 40);
            this.optRsToDt.Name = "optRsToDt";
            this.optRsToDt.Size = new System.Drawing.Size(62, 17);
            this.optRsToDt.TabIndex = 9;
            this.optRsToDt.TabStop = true;
            this.optRsToDt.Text = "RsToDt";
            this.optRsToDt.UseVisualStyleBackColor = true;
            // 
            // optDataTable
            // 
            this.optDataTable.AutoSize = true;
            this.optDataTable.Location = new System.Drawing.Point(301, 40);
            this.optDataTable.Name = "optDataTable";
            this.optDataTable.Size = new System.Drawing.Size(75, 17);
            this.optDataTable.TabIndex = 8;
            this.optDataTable.TabStop = true;
            this.optDataTable.Text = "DataTable";
            this.optDataTable.UseVisualStyleBackColor = true;
            // 
            // optRdrToDt
            // 
            this.optRdrToDt.AutoSize = true;
            this.optRdrToDt.Location = new System.Drawing.Point(109, 40);
            this.optRdrToDt.Name = "optRdrToDt";
            this.optRdrToDt.Size = new System.Drawing.Size(66, 17);
            this.optRdrToDt.TabIndex = 7;
            this.optRdrToDt.TabStop = true;
            this.optRdrToDt.Text = "RdrToDt";
            this.optRdrToDt.UseVisualStyleBackColor = true;
            // 
            // optDataset
            // 
            this.optDataset.AutoSize = true;
            this.optDataset.Location = new System.Drawing.Point(301, 17);
            this.optDataset.Name = "optDataset";
            this.optDataset.Size = new System.Drawing.Size(62, 17);
            this.optDataset.TabIndex = 3;
            this.optDataset.TabStop = true;
            this.optDataset.Text = "Dataset";
            this.optDataset.UseVisualStyleBackColor = true;
            // 
            // optResultset
            // 
            this.optResultset.AutoSize = true;
            this.optResultset.Location = new System.Drawing.Point(197, 17);
            this.optResultset.Name = "optResultset";
            this.optResultset.Size = new System.Drawing.Size(69, 17);
            this.optResultset.TabIndex = 2;
            this.optResultset.TabStop = true;
            this.optResultset.Text = "Resultset";
            this.optResultset.UseVisualStyleBackColor = true;
            // 
            // optReader
            // 
            this.optReader.AutoSize = true;
            this.optReader.Location = new System.Drawing.Point(109, 17);
            this.optReader.Name = "optReader";
            this.optReader.Size = new System.Drawing.Size(60, 17);
            this.optReader.TabIndex = 1;
            this.optReader.TabStop = true;
            this.optReader.Text = "Reader";
            this.optReader.UseVisualStyleBackColor = true;
            // 
            // optNonQuery
            // 
            this.optNonQuery.AutoSize = true;
            this.optNonQuery.Location = new System.Drawing.Point(16, 17);
            this.optNonQuery.Name = "optNonQuery";
            this.optNonQuery.Size = new System.Drawing.Size(73, 17);
            this.optNonQuery.TabIndex = 0;
            this.optNonQuery.TabStop = true;
            this.optNonQuery.Text = "NonQuery";
            this.optNonQuery.UseVisualStyleBackColor = true;
            // 
            // txtQuery
            // 
            this.txtQuery.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuery.Location = new System.Drawing.Point(35, 212);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtQuery.Size = new System.Drawing.Size(378, 116);
            this.txtQuery.TabIndex = 57;
            this.txtQuery.WordWrap = false;
            this.txtQuery.Enter += new System.EventHandler(this.txtQuery_Enter);
            this.txtQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuery_KeyDown);
            this.txtQuery.Leave += new System.EventHandler(this.txtQuery_Leave);
            // 
            // chkRunQuery
            // 
            this.chkRunQuery.AutoSize = true;
            this.chkRunQuery.Location = new System.Drawing.Point(16, 189);
            this.chkRunQuery.Name = "chkRunQuery";
            this.chkRunQuery.Size = new System.Drawing.Size(86, 17);
            this.chkRunQuery.TabIndex = 56;
            this.chkRunQuery.Text = "&2 Run Query";
            this.chkRunQuery.UseVisualStyleBackColor = true;
            // 
            // cboEncryptionMode
            // 
            this.cboEncryptionMode.FormattingEnabled = true;
            this.cboEncryptionMode.Items.AddRange(new object[] {
            "EngineDefault",
            "PlatformDefault",
            "PPC2003Compatibility"});
            this.cboEncryptionMode.Location = new System.Drawing.Point(274, 119);
            this.cboEncryptionMode.Name = "cboEncryptionMode";
            this.cboEncryptionMode.Size = new System.Drawing.Size(139, 21);
            this.cboEncryptionMode.TabIndex = 55;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(168, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 54;
            this.label3.Text = "Encryption Mode:";
            // 
            // chkEncryptionOn
            // 
            this.chkEncryptionOn.AutoSize = true;
            this.chkEncryptionOn.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkEncryptionOn.Location = new System.Drawing.Point(35, 117);
            this.chkEncryptionOn.Name = "chkEncryptionOn";
            this.chkEncryptionOn.Size = new System.Drawing.Size(96, 17);
            this.chkEncryptionOn.TabIndex = 53;
            this.chkEncryptionOn.Text = "Encryption On:";
            this.chkEncryptionOn.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(118, 91);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(295, 19);
            this.txtPassword.TabIndex = 52;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 51;
            this.label2.Text = "Password:";
            // 
            // cmdSetDataSourceFolder
            // 
            this.cmdSetDataSourceFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetDataSourceFolder.Location = new System.Drawing.Point(375, 45);
            this.cmdSetDataSourceFolder.Name = "cmdSetDataSourceFolder";
            this.cmdSetDataSourceFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdSetDataSourceFolder.TabIndex = 50;
            this.cmdSetDataSourceFolder.Text = "#";
            this.mainFormToolTips.SetToolTip(this.cmdSetDataSourceFolder, "Select Folder");
            this.cmdSetDataSourceFolder.UseVisualStyleBackColor = true;
            this.cmdSetDataSourceFolder.Click += new System.EventHandler(this.cmdSetDataSourceFolder_Click);
            // 
            // txtDataSource
            // 
            this.txtDataSource.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataSource.Location = new System.Drawing.Point(118, 65);
            this.txtDataSource.Name = "txtDataSource";
            this.txtDataSource.Size = new System.Drawing.Size(295, 19);
            this.txtDataSource.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Data Source:";
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(16, 32);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 8;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // chkCreateDatabaseTest
            // 
            this.chkCreateDatabaseTest.AutoSize = true;
            this.chkCreateDatabaseTest.Location = new System.Drawing.Point(16, 150);
            this.chkCreateDatabaseTest.Name = "chkCreateDatabaseTest";
            this.chkCreateDatabaseTest.Size = new System.Drawing.Size(115, 17);
            this.chkCreateDatabaseTest.TabIndex = 0;
            this.chkCreateDatabaseTest.Text = "&1 Create Database";
            this.chkCreateDatabaseTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogSQLCE\\TestprogSQLCE\\InitWinFormsHe" +
    "lpFile.chm";
            // 
            // mainMenuOpenFileDialog
            // 
            this.mainMenuOpenFileDialog.FileName = "openFileDialog1";
            this.mainMenuOpenFileDialog.Filter = "SQLCE Files|*.sdf|All Files|*.*";
            // 
            // keyValsDataSet
            // 
            this.keyValsDataSet.DataSetName = "appKeysDataSet";
            this.keyValsDataSet.Tables.AddRange(new System.Data.DataTable[] {
            this.KeyValTable});
            // 
            // KeyValTable
            // 
            this.KeyValTable.Columns.AddRange(new System.Data.DataColumn[] {
            this.AppSetting,
            this.SettingValue});
            this.KeyValTable.TableName = "KeyValTable";
            // 
            // AppSetting
            // 
            this.AppSetting.Caption = "App Setting";
            this.AppSetting.ColumnName = "AppSetting";
            this.AppSetting.MaxLength = 100;
            // 
            // SettingValue
            // 
            this.SettingValue.Caption = "Setting Value";
            this.SettingValue.ColumnName = "SettingValue";
            this.SettingValue.MaxLength = 255;
            // 
            // cmdShowHideOutputLog
            // 
            this.cmdShowHideOutputLog.Location = new System.Drawing.Point(510, 257);
            this.cmdShowHideOutputLog.Name = "cmdShowHideOutputLog";
            this.cmdShowHideOutputLog.Size = new System.Drawing.Size(93, 44);
            this.cmdShowHideOutputLog.TabIndex = 79;
            this.cmdShowHideOutputLog.Text = "Show/Hide\r\nOutput Log";
            this.cmdShowHideOutputLog.UseVisualStyleBackColor = true;
            this.cmdShowHideOutputLog.Click += new System.EventHandler(this.cmdShowHideOutputLog_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.cmdRunTests;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(638, 500);
            this.Controls.Add(this.cmdShowHideOutputLog);
            this.Controls.Add(this.grpTestsToRun);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.cmdRunTests);
            this.Controls.Add(this.cmdExit);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQLCE 3.5 Testprog";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.grpTestsToRun.ResumeLayout(false);
            this.grpTestsToRun.PerformLayout();
            this.grpQueryType.ResumeLayout(false);
            this.grpQueryType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.keyValsDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeyValTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdRunTests;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.GroupBox grpTestsToRun;
        private System.Windows.Forms.CheckBox chkCreateDatabaseTest;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        private System.Windows.Forms.Button cmdSetDataSourceFolder;
        internal System.Windows.Forms.TextBox txtDataSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog mainMenuFolderBrowserDialog;
        internal System.Windows.Forms.ComboBox cboEncryptionMode;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.CheckBox chkEncryptionOn;
        internal System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.CheckBox chkRunQuery;
        private System.Windows.Forms.GroupBox grpQueryType;
        internal System.Windows.Forms.RadioButton optNonQuery;
        internal System.Windows.Forms.RadioButton optReader;
        internal System.Windows.Forms.RadioButton optDataset;
        internal System.Windows.Forms.RadioButton optResultset;
        private System.Windows.Forms.ToolTip mainFormToolTips;
        private System.Windows.Forms.Button cmdSetDataSourceFilePath;
        private System.Windows.Forms.OpenFileDialog mainMenuOpenFileDialog;
        internal System.Windows.Forms.RadioButton optRsToDt;
        internal System.Windows.Forms.RadioButton optDataTable;
        internal System.Windows.Forms.RadioButton optRdrToDt;
        private System.Windows.Forms.CheckBox chkImportDataTable;
        internal System.Data.DataSet keyValsDataSet;
        internal System.Data.DataTable KeyValTable;
        internal System.Data.DataColumn AppSetting;
        internal System.Data.DataColumn SettingValue;
        private System.Windows.Forms.CheckBox chkCreateTable;
        private System.Windows.Forms.CheckBox chkConnectionTest;
        private System.Windows.Forms.Button cmdBuildQuery;
        private System.Windows.Forms.Button cmdShowHideOutputLog;
    }
}

