namespace TestprogMsAccess
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
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdRunTests = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.grpTestsToRun = new System.Windows.Forms.GroupBox();
            this.cmdBuildQuery = new System.Windows.Forms.Button();
            this.chkRunDropTableTest = new System.Windows.Forms.CheckBox();
            this.chkDataSetTest = new System.Windows.Forms.CheckBox();
            this.chkDataTableTest = new System.Windows.Forms.CheckBox();
            this.chkDataReaderTest = new System.Windows.Forms.CheckBox();
            this.chkConnectionTest = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSqlQuery = new System.Windows.Forms.TextBox();
            this.cboDatabase = new System.Windows.Forms.ComboBox();
            this.txtDbPassword = new System.Windows.Forms.TextBox();
            this.txtDbUsername = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboAccessVersion = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkRunQuery = new System.Windows.Forms.CheckBox();
            this.chkOverwriteExistingDb = new System.Windows.Forms.CheckBox();
            this.txtDatabasePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkCreateDatabaseTableTest = new System.Windows.Forms.CheckBox();
            this.chkAdoxAdorTest = new System.Windows.Forms.CheckBox();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.chkGetQueryDataSchema = new System.Windows.Forms.CheckBox();
            this.cmdShowHideOutputLog = new System.Windows.Forms.Button();
            this.MainMenu.SuspendLayout();
            this.grpTestsToRun.SuspendLayout();
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
            this.cmdRunTests.Location = new System.Drawing.Point(510, 65);
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
            this.mnuFileExit.Size = new System.Drawing.Size(92, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // grpTestsToRun
            // 
            this.grpTestsToRun.Controls.Add(this.cmdBuildQuery);
            this.grpTestsToRun.Controls.Add(this.chkRunDropTableTest);
            this.grpTestsToRun.Controls.Add(this.chkDataSetTest);
            this.grpTestsToRun.Controls.Add(this.chkDataTableTest);
            this.grpTestsToRun.Controls.Add(this.chkDataReaderTest);
            this.grpTestsToRun.Controls.Add(this.chkConnectionTest);
            this.grpTestsToRun.Controls.Add(this.label6);
            this.grpTestsToRun.Controls.Add(this.txtSqlQuery);
            this.grpTestsToRun.Controls.Add(this.cboDatabase);
            this.grpTestsToRun.Controls.Add(this.txtDbPassword);
            this.grpTestsToRun.Controls.Add(this.txtDbUsername);
            this.grpTestsToRun.Controls.Add(this.label5);
            this.grpTestsToRun.Controls.Add(this.label4);
            this.grpTestsToRun.Controls.Add(this.cboAccessVersion);
            this.grpTestsToRun.Controls.Add(this.label3);
            this.grpTestsToRun.Controls.Add(this.label2);
            this.grpTestsToRun.Controls.Add(this.chkRunQuery);
            this.grpTestsToRun.Controls.Add(this.chkOverwriteExistingDb);
            this.grpTestsToRun.Controls.Add(this.txtDatabasePath);
            this.grpTestsToRun.Controls.Add(this.label1);
            this.grpTestsToRun.Controls.Add(this.chkCreateDatabaseTableTest);
            this.grpTestsToRun.Controls.Add(this.chkAdoxAdorTest);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 36);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 451);
            this.grpTestsToRun.TabIndex = 0;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // cmdBuildQuery
            // 
            this.cmdBuildQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBuildQuery.Location = new System.Drawing.Point(384, 236);
            this.cmdBuildQuery.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmdBuildQuery.Name = "cmdBuildQuery";
            this.cmdBuildQuery.Size = new System.Drawing.Size(38, 14);
            this.cmdBuildQuery.TabIndex = 61;
            this.cmdBuildQuery.Text = ">>";
            this.cmdBuildQuery.UseVisualStyleBackColor = true;
            this.cmdBuildQuery.Click += new System.EventHandler(this.cmdBuildQuery_Click);
            // 
            // chkRunDropTableTest
            // 
            this.chkRunDropTableTest.AutoSize = true;
            this.chkRunDropTableTest.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRunDropTableTest.Location = new System.Drawing.Point(296, 113);
            this.chkRunDropTableTest.Name = "chkRunDropTableTest";
            this.chkRunDropTableTest.Size = new System.Drawing.Size(126, 17);
            this.chkRunDropTableTest.TabIndex = 31;
            this.chkRunDropTableTest.Text = "Run Drop Table Test";
            this.chkRunDropTableTest.UseVisualStyleBackColor = true;
            // 
            // chkDataSetTest
            // 
            this.chkDataSetTest.AutoSize = true;
            this.chkDataSetTest.Location = new System.Drawing.Point(334, 417);
            this.chkDataSetTest.Name = "chkDataSetTest";
            this.chkDataSetTest.Size = new System.Drawing.Size(89, 17);
            this.chkDataSetTest.TabIndex = 30;
            this.chkDataSetTest.Text = "DataSet Test";
            this.chkDataSetTest.UseVisualStyleBackColor = true;
            // 
            // chkDataTableTest
            // 
            this.chkDataTableTest.AutoSize = true;
            this.chkDataTableTest.Location = new System.Drawing.Point(172, 417);
            this.chkDataTableTest.Name = "chkDataTableTest";
            this.chkDataTableTest.Size = new System.Drawing.Size(103, 17);
            this.chkDataTableTest.TabIndex = 29;
            this.chkDataTableTest.Text = "Data Table Test";
            this.chkDataTableTest.UseVisualStyleBackColor = true;
            // 
            // chkDataReaderTest
            // 
            this.chkDataReaderTest.AutoSize = true;
            this.chkDataReaderTest.Location = new System.Drawing.Point(15, 417);
            this.chkDataReaderTest.Name = "chkDataReaderTest";
            this.chkDataReaderTest.Size = new System.Drawing.Size(111, 17);
            this.chkDataReaderTest.TabIndex = 28;
            this.chkDataReaderTest.Text = "Data Reader Test";
            this.chkDataReaderTest.UseVisualStyleBackColor = true;
            // 
            // chkConnectionTest
            // 
            this.chkConnectionTest.AutoSize = true;
            this.chkConnectionTest.Location = new System.Drawing.Point(319, 146);
            this.chkConnectionTest.Name = "chkConnectionTest";
            this.chkConnectionTest.Size = new System.Drawing.Size(104, 17);
            this.chkConnectionTest.TabIndex = 27;
            this.chkConnectionTest.Text = "Connection Test";
            this.chkConnectionTest.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 235);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Query:";
            // 
            // txtSqlQuery
            // 
            this.txtSqlQuery.Location = new System.Drawing.Point(15, 254);
            this.txtSqlQuery.Multiline = true;
            this.txtSqlQuery.Name = "txtSqlQuery";
            this.txtSqlQuery.Size = new System.Drawing.Size(408, 146);
            this.txtSqlQuery.TabIndex = 25;
            // 
            // cboDatabase
            // 
            this.cboDatabase.FormattingEnabled = true;
            this.cboDatabase.Location = new System.Drawing.Point(18, 164);
            this.cboDatabase.Name = "cboDatabase";
            this.cboDatabase.Size = new System.Drawing.Size(405, 21);
            this.cboDatabase.TabIndex = 24;
            this.cboDatabase.SelectedIndexChanged += new System.EventHandler(this.cboDatabase_SelectedIndexChanged);
            // 
            // txtDbPassword
            // 
            this.txtDbPassword.Location = new System.Drawing.Point(296, 207);
            this.txtDbPassword.Name = "txtDbPassword";
            this.txtDbPassword.Size = new System.Drawing.Size(127, 20);
            this.txtDbPassword.TabIndex = 23;
            // 
            // txtDbUsername
            // 
            this.txtDbUsername.Location = new System.Drawing.Point(143, 207);
            this.txtDbUsername.Name = "txtDbUsername";
            this.txtDbUsername.Size = new System.Drawing.Size(123, 20);
            this.txtDbUsername.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(293, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Password:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(140, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Username:";
            // 
            // cboAccessVersion
            // 
            this.cboAccessVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAccessVersion.FormattingEnabled = true;
            this.cboAccessVersion.Location = new System.Drawing.Point(15, 207);
            this.cboAccessVersion.Name = "cboAccessVersion";
            this.cboAccessVersion.Size = new System.Drawing.Size(103, 21);
            this.cboAccessVersion.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Version:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Database:";
            // 
            // chkRunQuery
            // 
            this.chkRunQuery.AutoSize = true;
            this.chkRunQuery.Location = new System.Drawing.Point(15, 123);
            this.chkRunQuery.Name = "chkRunQuery";
            this.chkRunQuery.Size = new System.Drawing.Size(86, 17);
            this.chkRunQuery.TabIndex = 15;
            this.chkRunQuery.Text = "&3 Run Query";
            this.chkRunQuery.UseVisualStyleBackColor = true;
            // 
            // chkOverwriteExistingDb
            // 
            this.chkOverwriteExistingDb.AutoSize = true;
            this.chkOverwriteExistingDb.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOverwriteExistingDb.Location = new System.Drawing.Point(296, 63);
            this.chkOverwriteExistingDb.Name = "chkOverwriteExistingDb";
            this.chkOverwriteExistingDb.Size = new System.Drawing.Size(127, 17);
            this.chkOverwriteExistingDb.TabIndex = 14;
            this.chkOverwriteExistingDb.Text = "Overwrite Existing Db";
            this.chkOverwriteExistingDb.UseVisualStyleBackColor = true;
            // 
            // txtDatabasePath
            // 
            this.txtDatabasePath.Location = new System.Drawing.Point(125, 86);
            this.txtDatabasePath.Name = "txtDatabasePath";
            this.txtDatabasePath.Size = new System.Drawing.Size(298, 20);
            this.txtDatabasePath.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Database Path:";
            // 
            // chkCreateDatabaseTableTest
            // 
            this.chkCreateDatabaseTableTest.AutoSize = true;
            this.chkCreateDatabaseTableTest.Location = new System.Drawing.Point(15, 62);
            this.chkCreateDatabaseTableTest.Name = "chkCreateDatabaseTableTest";
            this.chkCreateDatabaseTableTest.Size = new System.Drawing.Size(167, 17);
            this.chkCreateDatabaseTableTest.TabIndex = 9;
            this.chkCreateDatabaseTableTest.Text = "&2 Create Database/Table test";
            this.chkCreateDatabaseTableTest.UseVisualStyleBackColor = true;
            // 
            // chkAdoxAdorTest
            // 
            this.chkAdoxAdorTest.AutoSize = true;
            this.chkAdoxAdorTest.Location = new System.Drawing.Point(15, 29);
            this.chkAdoxAdorTest.Name = "chkAdoxAdorTest";
            this.chkAdoxAdorTest.Size = new System.Drawing.Size(163, 17);
            this.chkAdoxAdorTest.TabIndex = 0;
            this.chkAdoxAdorTest.Text = "&1 ADOX/ADOR Access Test";
            this.chkAdoxAdorTest.UseVisualStyleBackColor = true;
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(39, 493);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 8;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogMsAccess\\TestprogMsAccess\\InitWinF" +
    "ormsHelpFile.chm";
            // 
            // chkGetQueryDataSchema
            // 
            this.chkGetQueryDataSchema.AutoSize = true;
            this.chkGetQueryDataSchema.Location = new System.Drawing.Point(319, 493);
            this.chkGetQueryDataSchema.Name = "chkGetQueryDataSchema";
            this.chkGetQueryDataSchema.Size = new System.Drawing.Size(142, 17);
            this.chkGetQueryDataSchema.TabIndex = 77;
            this.chkGetQueryDataSchema.Text = "Get Query Data Schema";
            this.chkGetQueryDataSchema.UseVisualStyleBackColor = true;
            // 
            // cmdShowHideOutputLog
            // 
            this.cmdShowHideOutputLog.Location = new System.Drawing.Point(510, 227);
            this.cmdShowHideOutputLog.Name = "cmdShowHideOutputLog";
            this.cmdShowHideOutputLog.Size = new System.Drawing.Size(93, 44);
            this.cmdShowHideOutputLog.TabIndex = 78;
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
            this.ClientSize = new System.Drawing.Size(638, 530);
            this.Controls.Add(this.cmdShowHideOutputLog);
            this.Controls.Add(this.chkGetQueryDataSchema);
            this.Controls.Add(this.grpTestsToRun);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.cmdRunTests);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.chkEraseOutputBeforeEachTest);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Testprog for PFMsAccess";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.grpTestsToRun.ResumeLayout(false);
            this.grpTestsToRun.PerformLayout();
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
        private System.Windows.Forms.CheckBox chkAdoxAdorTest;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        private System.Windows.Forms.CheckBox chkCreateDatabaseTableTest;
        internal System.Windows.Forms.CheckBox chkOverwriteExistingDb;
        internal System.Windows.Forms.TextBox txtDatabasePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkRunQuery;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.ComboBox cboAccessVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox txtDbPassword;
        internal System.Windows.Forms.TextBox txtDbUsername;
        internal System.Windows.Forms.ComboBox cboDatabase;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox txtSqlQuery;
        private System.Windows.Forms.CheckBox chkDataSetTest;
        private System.Windows.Forms.CheckBox chkDataTableTest;
        private System.Windows.Forms.CheckBox chkDataReaderTest;
        private System.Windows.Forms.CheckBox chkConnectionTest;
        private System.Windows.Forms.CheckBox chkRunDropTableTest;
        private System.Windows.Forms.Button cmdBuildQuery;
        private System.Windows.Forms.CheckBox chkGetQueryDataSchema;
        private System.Windows.Forms.Button cmdShowHideOutputLog;
    }
}

