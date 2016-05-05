namespace TestprogGetSchemas
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
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.cmdRunTests = new System.Windows.Forms.Button();
            this.cmdExit = new System.Windows.Forms.Button();
            this.grpDatabaseToUse = new System.Windows.Forms.GroupBox();
            this.cboConnectionString = new System.Windows.Forms.ComboBox();
            this.optUseOleDbProvider = new System.Windows.Forms.RadioButton();
            this.optUseOdbcDriver = new System.Windows.Forms.RadioButton();
            this.optUseDotNetProvider = new System.Windows.Forms.RadioButton();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.grpTestsToRun = new System.Windows.Forms.GroupBox();
            this.chkGetDataTypesCollection = new System.Windows.Forms.CheckBox();
            this.chkTablePatternMatchTests = new System.Windows.Forms.CheckBox();
            this.chkGetSQLServerTables = new System.Windows.Forms.CheckBox();
            this.chkGetRestrictionsCollection = new System.Windows.Forms.CheckBox();
            this.chkGetTablesCollection = new System.Windows.Forms.CheckBox();
            this.chkGetProviderList = new System.Windows.Forms.CheckBox();
            this.chkGetMetadataCollections = new System.Windows.Forms.CheckBox();
            this.cmdShowHideOutputLog = new System.Windows.Forms.Button();
            this.MainMenu.SuspendLayout();
            this.grpDatabaseToUse.SuspendLayout();
            this.grpTestsToRun.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(756, 24);
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
            this.mnuFileExit.Size = new System.Drawing.Size(92, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogGetSchemas\\TestprogGetSchemas\\Init" +
    "WinFormsHelpFile.chm";
            // 
            // cmdRunTests
            // 
            this.appHelpProvider.SetHelpKeyword(this.cmdRunTests, "Run Tests");
            this.appHelpProvider.SetHelpNavigator(this.cmdRunTests, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.appHelpProvider.SetHelpString(this.cmdRunTests, "Help for Run Tests: See Help File.");
            this.cmdRunTests.Location = new System.Drawing.Point(615, 47);
            this.cmdRunTests.Name = "cmdRunTests";
            this.appHelpProvider.SetShowHelp(this.cmdRunTests, true);
            this.cmdRunTests.Size = new System.Drawing.Size(93, 37);
            this.cmdRunTests.TabIndex = 11;
            this.cmdRunTests.Text = "&Run Tests";
            this.cmdRunTests.UseVisualStyleBackColor = true;
            this.cmdRunTests.Click += new System.EventHandler(this.cmdRunTests_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(615, 427);
            this.cmdExit.Name = "cmdExit";
            this.appHelpProvider.SetShowHelp(this.cmdExit, true);
            this.cmdExit.Size = new System.Drawing.Size(93, 37);
            this.cmdExit.TabIndex = 12;
            this.cmdExit.Text = "E&xit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // grpDatabaseToUse
            // 
            this.grpDatabaseToUse.Controls.Add(this.cboConnectionString);
            this.grpDatabaseToUse.Controls.Add(this.optUseOleDbProvider);
            this.grpDatabaseToUse.Controls.Add(this.optUseOdbcDriver);
            this.grpDatabaseToUse.Controls.Add(this.optUseDotNetProvider);
            this.grpDatabaseToUse.Location = new System.Drawing.Point(55, 47);
            this.grpDatabaseToUse.Name = "grpDatabaseToUse";
            this.grpDatabaseToUse.Size = new System.Drawing.Size(554, 140);
            this.grpDatabaseToUse.TabIndex = 14;
            this.grpDatabaseToUse.TabStop = false;
            this.grpDatabaseToUse.Text = "Select Database";
            // 
            // cboConnectionString
            // 
            this.cboConnectionString.FormattingEnabled = true;
            this.cboConnectionString.Location = new System.Drawing.Point(10, 88);
            this.cboConnectionString.Name = "cboConnectionString";
            this.cboConnectionString.Size = new System.Drawing.Size(510, 21);
            this.cboConnectionString.TabIndex = 3;
            // 
            // optUseOleDbProvider
            // 
            this.optUseOleDbProvider.AutoSize = true;
            this.optUseOleDbProvider.Location = new System.Drawing.Point(395, 31);
            this.optUseOleDbProvider.Name = "optUseOleDbProvider";
            this.optUseOleDbProvider.Size = new System.Drawing.Size(125, 17);
            this.optUseOleDbProvider.TabIndex = 2;
            this.optUseOleDbProvider.TabStop = true;
            this.optUseOleDbProvider.Text = "Use OLEDB Provider";
            this.optUseOleDbProvider.UseVisualStyleBackColor = true;
            this.optUseOleDbProvider.CheckedChanged += new System.EventHandler(this.optUseOleDbProvider_CheckedChanged);
            // 
            // optUseOdbcDriver
            // 
            this.optUseOdbcDriver.AutoSize = true;
            this.optUseOdbcDriver.Location = new System.Drawing.Point(218, 31);
            this.optUseOdbcDriver.Name = "optUseOdbcDriver";
            this.optUseOdbcDriver.Size = new System.Drawing.Size(108, 17);
            this.optUseOdbcDriver.TabIndex = 1;
            this.optUseOdbcDriver.TabStop = true;
            this.optUseOdbcDriver.Text = "Use ODBC Driver";
            this.optUseOdbcDriver.UseVisualStyleBackColor = true;
            this.optUseOdbcDriver.CheckedChanged += new System.EventHandler(this.optUseOdbcDriver_CheckedChanged);
            // 
            // optUseDotNetProvider
            // 
            this.optUseDotNetProvider.AutoSize = true;
            this.optUseDotNetProvider.Location = new System.Drawing.Point(10, 31);
            this.optUseDotNetProvider.Name = "optUseDotNetProvider";
            this.optUseDotNetProvider.Size = new System.Drawing.Size(114, 17);
            this.optUseDotNetProvider.TabIndex = 0;
            this.optUseDotNetProvider.TabStop = true;
            this.optUseDotNetProvider.Text = "Use .NET Provider";
            this.optUseDotNetProvider.UseVisualStyleBackColor = true;
            this.optUseDotNetProvider.CheckedChanged += new System.EventHandler(this.optUseDotNetProvider_CheckedChanged);
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(48, 495);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 13;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // grpTestsToRun
            // 
            this.grpTestsToRun.Controls.Add(this.chkGetDataTypesCollection);
            this.grpTestsToRun.Controls.Add(this.chkTablePatternMatchTests);
            this.grpTestsToRun.Controls.Add(this.chkGetSQLServerTables);
            this.grpTestsToRun.Controls.Add(this.chkGetRestrictionsCollection);
            this.grpTestsToRun.Controls.Add(this.chkGetTablesCollection);
            this.grpTestsToRun.Controls.Add(this.chkGetProviderList);
            this.grpTestsToRun.Controls.Add(this.chkGetMetadataCollections);
            this.grpTestsToRun.Location = new System.Drawing.Point(48, 207);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(527, 257);
            this.grpTestsToRun.TabIndex = 10;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // chkGetDataTypesCollection
            // 
            this.chkGetDataTypesCollection.AutoSize = true;
            this.chkGetDataTypesCollection.Location = new System.Drawing.Point(17, 175);
            this.chkGetDataTypesCollection.Name = "chkGetDataTypesCollection";
            this.chkGetDataTypesCollection.Size = new System.Drawing.Size(147, 17);
            this.chkGetDataTypesCollection.TabIndex = 7;
            this.chkGetDataTypesCollection.Text = "Get DataTypes Collection";
            this.chkGetDataTypesCollection.UseVisualStyleBackColor = true;
            // 
            // chkTablePatternMatchTests
            // 
            this.chkTablePatternMatchTests.AutoSize = true;
            this.chkTablePatternMatchTests.Location = new System.Drawing.Point(302, 70);
            this.chkTablePatternMatchTests.Name = "chkTablePatternMatchTests";
            this.chkTablePatternMatchTests.Size = new System.Drawing.Size(152, 17);
            this.chkTablePatternMatchTests.TabIndex = 5;
            this.chkTablePatternMatchTests.Text = "Table Pattern Match Tests";
            this.chkTablePatternMatchTests.UseVisualStyleBackColor = true;
            // 
            // chkGetSQLServerTables
            // 
            this.chkGetSQLServerTables.AutoSize = true;
            this.chkGetSQLServerTables.Location = new System.Drawing.Point(302, 36);
            this.chkGetSQLServerTables.Name = "chkGetSQLServerTables";
            this.chkGetSQLServerTables.Size = new System.Drawing.Size(136, 17);
            this.chkGetSQLServerTables.TabIndex = 4;
            this.chkGetSQLServerTables.Text = "Get SQL Server Tables";
            this.chkGetSQLServerTables.UseVisualStyleBackColor = true;
            // 
            // chkGetRestrictionsCollection
            // 
            this.chkGetRestrictionsCollection.AutoSize = true;
            this.chkGetRestrictionsCollection.Location = new System.Drawing.Point(17, 141);
            this.chkGetRestrictionsCollection.Name = "chkGetRestrictionsCollection";
            this.chkGetRestrictionsCollection.Size = new System.Drawing.Size(150, 17);
            this.chkGetRestrictionsCollection.TabIndex = 3;
            this.chkGetRestrictionsCollection.Text = "Get Restrictions Collection";
            this.chkGetRestrictionsCollection.UseVisualStyleBackColor = true;
            // 
            // chkGetTablesCollection
            // 
            this.chkGetTablesCollection.AutoSize = true;
            this.chkGetTablesCollection.Location = new System.Drawing.Point(17, 105);
            this.chkGetTablesCollection.Name = "chkGetTablesCollection";
            this.chkGetTablesCollection.Size = new System.Drawing.Size(127, 17);
            this.chkGetTablesCollection.TabIndex = 2;
            this.chkGetTablesCollection.Text = "Get Tables Collection";
            this.chkGetTablesCollection.UseVisualStyleBackColor = true;
            // 
            // chkGetProviderList
            // 
            this.chkGetProviderList.AutoSize = true;
            this.chkGetProviderList.Location = new System.Drawing.Point(17, 36);
            this.chkGetProviderList.Name = "chkGetProviderList";
            this.chkGetProviderList.Size = new System.Drawing.Size(104, 17);
            this.chkGetProviderList.TabIndex = 1;
            this.chkGetProviderList.Text = "Get Provider List";
            this.chkGetProviderList.UseVisualStyleBackColor = true;
            // 
            // chkGetMetadataCollections
            // 
            this.chkGetMetadataCollections.AutoSize = true;
            this.chkGetMetadataCollections.Location = new System.Drawing.Point(17, 70);
            this.chkGetMetadataCollections.Name = "chkGetMetadataCollections";
            this.chkGetMetadataCollections.Size = new System.Drawing.Size(145, 17);
            this.chkGetMetadataCollections.TabIndex = 0;
            this.chkGetMetadataCollections.Text = "Get Metadata Collections";
            this.chkGetMetadataCollections.UseVisualStyleBackColor = true;
            // 
            // cmdShowHideOutputLog
            // 
            this.cmdShowHideOutputLog.Location = new System.Drawing.Point(615, 243);
            this.cmdShowHideOutputLog.Name = "cmdShowHideOutputLog";
            this.cmdShowHideOutputLog.Size = new System.Drawing.Size(93, 44);
            this.cmdShowHideOutputLog.TabIndex = 79;
            this.cmdShowHideOutputLog.Text = "Show/Hide\r\nOutput Log";
            this.cmdShowHideOutputLog.UseVisualStyleBackColor = true;
            this.cmdShowHideOutputLog.Click += new System.EventHandler(this.cmdShowHideOutputLog_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 558);
            this.Controls.Add(this.cmdShowHideOutputLog);
            this.Controls.Add(this.grpDatabaseToUse);
            this.Controls.Add(this.chkEraseOutputBeforeEachTest);
            this.Controls.Add(this.grpTestsToRun);
            this.Controls.Add(this.cmdRunTests);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.MainMenu);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Form";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.grpDatabaseToUse.ResumeLayout(false);
            this.grpDatabaseToUse.PerformLayout();
            this.grpTestsToRun.ResumeLayout(false);
            this.grpTestsToRun.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.GroupBox grpDatabaseToUse;
        private System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        private System.Windows.Forms.GroupBox grpTestsToRun;
        private System.Windows.Forms.CheckBox chkGetProviderList;
        private System.Windows.Forms.CheckBox chkGetMetadataCollections;
        private System.Windows.Forms.Button cmdRunTests;
        private System.Windows.Forms.Button cmdExit;
        internal System.Windows.Forms.ComboBox cboConnectionString;
        internal System.Windows.Forms.RadioButton optUseOleDbProvider;
        internal System.Windows.Forms.RadioButton optUseOdbcDriver;
        internal System.Windows.Forms.RadioButton optUseDotNetProvider;
        private System.Windows.Forms.CheckBox chkGetTablesCollection;
        private System.Windows.Forms.CheckBox chkGetRestrictionsCollection;
        private System.Windows.Forms.CheckBox chkGetSQLServerTables;
        private System.Windows.Forms.CheckBox chkTablePatternMatchTests;
        private System.Windows.Forms.CheckBox chkGetDataTypesCollection;
        private System.Windows.Forms.Button cmdShowHideOutputLog;
    }
}

