namespace TestprogSQLAnywhereUL
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
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.cmdGetDatabaseFileName = new System.Windows.Forms.Button();
            this.cmdRunTests = new System.Windows.Forms.Button();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdCreateDb = new System.Windows.Forms.Button();
            this.txtDatabaseKey = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDatabaseFile = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdBuildQuery = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSQLQuery = new System.Windows.Forms.TextBox();
            this.chkShowStackTraceOnError = new System.Windows.Forms.CheckBox();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.grpTestsToRun = new System.Windows.Forms.GroupBox();
            this.chkGetSchemaTest = new System.Windows.Forms.CheckBox();
            this.chkImportDataTable = new System.Windows.Forms.CheckBox();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.chkCreateTableTest = new System.Windows.Forms.CheckBox();
            this.chkDataReaderToDataTableTest = new System.Windows.Forms.CheckBox();
            this.chkDataTableTest = new System.Windows.Forms.CheckBox();
            this.chkDataSetTest = new System.Windows.Forms.CheckBox();
            this.chkDataReaderTest = new System.Windows.Forms.CheckBox();
            this.chkConnectionStringTest = new System.Windows.Forms.CheckBox();
            this.chkGetStaticKeysTest = new System.Windows.Forms.CheckBox();
            this.chkShowDateTimeTest = new System.Windows.Forms.CheckBox();
            this.keyValsDataSet = new System.Data.DataSet();
            this.KeyValTable = new System.Data.DataTable();
            this.AppSetting = new System.Data.DataColumn();
            this.SettingValue = new System.Data.DataColumn();
            this.mainFormToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdShowHideOutputLog = new System.Windows.Forms.Button();
            this.MainMenu.SuspendLayout();
            this.grpTestsToRun.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.keyValsDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeyValTable)).BeginInit();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(640, 24);
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
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogSQLAnywhereUL\\TestprogSQLAnywhereU" +
    "L\\InitWinFormsHelpFile.chm";
            // 
            // cmdGetDatabaseFileName
            // 
            this.cmdGetDatabaseFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appHelpProvider.SetHelpString(this.cmdGetDatabaseFileName, "");
            this.cmdGetDatabaseFileName.Location = new System.Drawing.Point(567, 38);
            this.cmdGetDatabaseFileName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmdGetDatabaseFileName.Name = "cmdGetDatabaseFileName";
            this.appHelpProvider.SetShowHelp(this.cmdGetDatabaseFileName, true);
            this.cmdGetDatabaseFileName.Size = new System.Drawing.Size(38, 20);
            this.cmdGetDatabaseFileName.TabIndex = 89;
            this.cmdGetDatabaseFileName.Text = ">>";
            this.cmdGetDatabaseFileName.UseVisualStyleBackColor = true;
            this.cmdGetDatabaseFileName.Click += new System.EventHandler(this.cmdGetDatabaseFileName_Click);
            // 
            // cmdRunTests
            // 
            this.appHelpProvider.SetHelpKeyword(this.cmdRunTests, "Run Tests");
            this.appHelpProvider.SetHelpNavigator(this.cmdRunTests, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.appHelpProvider.SetHelpString(this.cmdRunTests, "Help for Run Tests: See Help File.");
            this.cmdRunTests.Location = new System.Drawing.Point(512, 143);
            this.cmdRunTests.Name = "cmdRunTests";
            this.appHelpProvider.SetShowHelp(this.cmdRunTests, true);
            this.cmdRunTests.Size = new System.Drawing.Size(93, 37);
            this.cmdRunTests.TabIndex = 69;
            this.cmdRunTests.Text = "&Run Tests";
            this.cmdRunTests.UseVisualStyleBackColor = true;
            this.cmdRunTests.Click += new System.EventHandler(this.cmdRunTests_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(512, 465);
            this.cmdExit.Name = "cmdExit";
            this.appHelpProvider.SetShowHelp(this.cmdExit, true);
            this.cmdExit.Size = new System.Drawing.Size(93, 37);
            this.cmdExit.TabIndex = 70;
            this.cmdExit.Text = "E&xit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdCreateDb
            // 
            this.cmdCreateDb.Location = new System.Drawing.Point(512, 252);
            this.cmdCreateDb.Name = "cmdCreateDb";
            this.cmdCreateDb.Size = new System.Drawing.Size(93, 37);
            this.cmdCreateDb.TabIndex = 96;
            this.cmdCreateDb.Text = "CreateDb";
            this.cmdCreateDb.UseVisualStyleBackColor = true;
            this.cmdCreateDb.Click += new System.EventHandler(this.cmdCreateDb_Click);
            // 
            // txtDatabaseKey
            // 
            this.txtDatabaseKey.Location = new System.Drawing.Point(141, 63);
            this.txtDatabaseKey.Name = "txtDatabaseKey";
            this.txtDatabaseKey.Size = new System.Drawing.Size(175, 20);
            this.txtDatabaseKey.TabIndex = 91;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(39, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 90;
            this.label8.Text = "Database Key:";
            // 
            // txtDatabaseFile
            // 
            this.txtDatabaseFile.Location = new System.Drawing.Point(141, 38);
            this.txtDatabaseFile.Name = "txtDatabaseFile";
            this.txtDatabaseFile.Size = new System.Drawing.Size(390, 20);
            this.txtDatabaseFile.TabIndex = 88;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 87;
            this.label6.Text = "Database File:";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(423, 95);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(182, 19);
            this.txtPassword.TabIndex = 84;
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(141, 95);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(175, 19);
            this.txtUsername.TabIndex = 83;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(340, 95);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 82;
            this.label4.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 95);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 81;
            this.label3.Text = "Username:";
            // 
            // cmdBuildQuery
            // 
            this.cmdBuildQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBuildQuery.Location = new System.Drawing.Point(440, 143);
            this.cmdBuildQuery.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmdBuildQuery.Name = "cmdBuildQuery";
            this.cmdBuildQuery.Size = new System.Drawing.Size(38, 14);
            this.cmdBuildQuery.TabIndex = 75;
            this.cmdBuildQuery.Text = ">>";
            this.cmdBuildQuery.UseVisualStyleBackColor = true;
            this.cmdBuildQuery.Click += new System.EventHandler(this.cmdBuildQuery_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 144);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 74;
            this.label7.Text = "Query to run:";
            // 
            // txtSQLQuery
            // 
            this.txtSQLQuery.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSQLQuery.Location = new System.Drawing.Point(39, 163);
            this.txtSQLQuery.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtSQLQuery.Multiline = true;
            this.txtSQLQuery.Name = "txtSQLQuery";
            this.txtSQLQuery.Size = new System.Drawing.Size(438, 78);
            this.txtSQLQuery.TabIndex = 73;
            // 
            // chkShowStackTraceOnError
            // 
            this.chkShowStackTraceOnError.AutoSize = true;
            this.chkShowStackTraceOnError.Location = new System.Drawing.Point(42, 485);
            this.chkShowStackTraceOnError.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chkShowStackTraceOnError.Name = "chkShowStackTraceOnError";
            this.chkShowStackTraceOnError.Size = new System.Drawing.Size(155, 17);
            this.chkShowStackTraceOnError.TabIndex = 72;
            this.chkShowStackTraceOnError.Text = "Show Stack Trace on Error";
            this.chkShowStackTraceOnError.UseVisualStyleBackColor = true;
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(270, 485);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 71;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // grpTestsToRun
            // 
            this.grpTestsToRun.Controls.Add(this.chkGetSchemaTest);
            this.grpTestsToRun.Controls.Add(this.chkImportDataTable);
            this.grpTestsToRun.Controls.Add(this.txtTableName);
            this.grpTestsToRun.Controls.Add(this.chkCreateTableTest);
            this.grpTestsToRun.Controls.Add(this.chkDataReaderToDataTableTest);
            this.grpTestsToRun.Controls.Add(this.chkDataTableTest);
            this.grpTestsToRun.Controls.Add(this.chkDataSetTest);
            this.grpTestsToRun.Controls.Add(this.chkDataReaderTest);
            this.grpTestsToRun.Controls.Add(this.chkConnectionStringTest);
            this.grpTestsToRun.Controls.Add(this.chkGetStaticKeysTest);
            this.grpTestsToRun.Controls.Add(this.chkShowDateTimeTest);
            this.grpTestsToRun.Location = new System.Drawing.Point(41, 252);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 217);
            this.grpTestsToRun.TabIndex = 68;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // chkGetSchemaTest
            // 
            this.chkGetSchemaTest.AutoSize = true;
            this.chkGetSchemaTest.Location = new System.Drawing.Point(244, 107);
            this.chkGetSchemaTest.Name = "chkGetSchemaTest";
            this.chkGetSchemaTest.Size = new System.Drawing.Size(103, 17);
            this.chkGetSchemaTest.TabIndex = 71;
            this.chkGetSchemaTest.Text = "GetSchemaTest";
            this.chkGetSchemaTest.UseVisualStyleBackColor = true;
            // 
            // chkImportDataTable
            // 
            this.chkImportDataTable.AutoSize = true;
            this.chkImportDataTable.Location = new System.Drawing.Point(241, 176);
            this.chkImportDataTable.Name = "chkImportDataTable";
            this.chkImportDataTable.Size = new System.Drawing.Size(108, 17);
            this.chkImportDataTable.TabIndex = 70;
            this.chkImportDataTable.Text = "Import DataTable";
            this.chkImportDataTable.UseVisualStyleBackColor = true;
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(243, 55);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(172, 20);
            this.txtTableName.TabIndex = 69;
            // 
            // chkCreateTableTest
            // 
            this.chkCreateTableTest.AutoSize = true;
            this.chkCreateTableTest.Location = new System.Drawing.Point(244, 32);
            this.chkCreateTableTest.Name = "chkCreateTableTest";
            this.chkCreateTableTest.Size = new System.Drawing.Size(111, 17);
            this.chkCreateTableTest.TabIndex = 68;
            this.chkCreateTableTest.Text = "Create Table Test";
            this.chkCreateTableTest.UseVisualStyleBackColor = true;
            // 
            // chkDataReaderToDataTableTest
            // 
            this.chkDataReaderToDataTableTest.AutoSize = true;
            this.chkDataReaderToDataTableTest.Location = new System.Drawing.Point(16, 176);
            this.chkDataReaderToDataTableTest.Name = "chkDataReaderToDataTableTest";
            this.chkDataReaderToDataTableTest.Size = new System.Drawing.Size(173, 17);
            this.chkDataReaderToDataTableTest.TabIndex = 67;
            this.chkDataReaderToDataTableTest.Text = "DataReader to DataTable Test";
            this.chkDataReaderToDataTableTest.UseVisualStyleBackColor = true;
            // 
            // chkDataTableTest
            // 
            this.chkDataTableTest.AutoSize = true;
            this.chkDataTableTest.Location = new System.Drawing.Point(16, 130);
            this.chkDataTableTest.Name = "chkDataTableTest";
            this.chkDataTableTest.Size = new System.Drawing.Size(103, 17);
            this.chkDataTableTest.TabIndex = 66;
            this.chkDataTableTest.Text = "Data Table Test";
            this.chkDataTableTest.UseVisualStyleBackColor = true;
            // 
            // chkDataSetTest
            // 
            this.chkDataSetTest.AutoSize = true;
            this.chkDataSetTest.Location = new System.Drawing.Point(16, 153);
            this.chkDataSetTest.Name = "chkDataSetTest";
            this.chkDataSetTest.Size = new System.Drawing.Size(89, 17);
            this.chkDataSetTest.TabIndex = 65;
            this.chkDataSetTest.Text = "DataSet Test";
            this.chkDataSetTest.UseVisualStyleBackColor = true;
            // 
            // chkDataReaderTest
            // 
            this.chkDataReaderTest.AutoSize = true;
            this.chkDataReaderTest.Location = new System.Drawing.Point(16, 107);
            this.chkDataReaderTest.Name = "chkDataReaderTest";
            this.chkDataReaderTest.Size = new System.Drawing.Size(111, 17);
            this.chkDataReaderTest.TabIndex = 64;
            this.chkDataReaderTest.Text = "Data Reader Test";
            this.chkDataReaderTest.UseVisualStyleBackColor = true;
            // 
            // chkConnectionStringTest
            // 
            this.chkConnectionStringTest.AutoSize = true;
            this.chkConnectionStringTest.Location = new System.Drawing.Point(16, 83);
            this.chkConnectionStringTest.Name = "chkConnectionStringTest";
            this.chkConnectionStringTest.Size = new System.Drawing.Size(134, 17);
            this.chkConnectionStringTest.TabIndex = 63;
            this.chkConnectionStringTest.Text = "Connection String Test";
            this.chkConnectionStringTest.UseVisualStyleBackColor = true;
            // 
            // chkGetStaticKeysTest
            // 
            this.chkGetStaticKeysTest.AutoSize = true;
            this.chkGetStaticKeysTest.Location = new System.Drawing.Point(16, 55);
            this.chkGetStaticKeysTest.Name = "chkGetStaticKeysTest";
            this.chkGetStaticKeysTest.Size = new System.Drawing.Size(123, 17);
            this.chkGetStaticKeysTest.TabIndex = 1;
            this.chkGetStaticKeysTest.Text = "&2 GetStaticKeysTest";
            this.chkGetStaticKeysTest.UseVisualStyleBackColor = true;
            // 
            // chkShowDateTimeTest
            // 
            this.chkShowDateTimeTest.AutoSize = true;
            this.chkShowDateTimeTest.Location = new System.Drawing.Point(16, 32);
            this.chkShowDateTimeTest.Name = "chkShowDateTimeTest";
            this.chkShowDateTimeTest.Size = new System.Drawing.Size(140, 17);
            this.chkShowDateTimeTest.TabIndex = 0;
            this.chkShowDateTimeTest.Text = "&1 Show Date/Time Test";
            this.chkShowDateTimeTest.UseVisualStyleBackColor = true;
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
            this.AppSetting.AllowDBNull = false;
            this.AppSetting.Caption = "App Setting";
            this.AppSetting.ColumnName = "AppSetting";
            this.AppSetting.MaxLength = 100;
            // 
            // SettingValue
            // 
            this.SettingValue.AllowDBNull = false;
            this.SettingValue.Caption = "Setting Value";
            this.SettingValue.ColumnName = "SettingValue";
            this.SettingValue.MaxLength = 255;
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Enabled = false;
            this.txtDatabaseName.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDatabaseName.Location = new System.Drawing.Point(440, 64);
            this.txtDatabaseName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(165, 19);
            this.txtDatabaseName.TabIndex = 80;
            this.txtDatabaseName.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(338, 67);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 78;
            this.label2.Text = "Database Name:";
            this.label2.Visible = false;
            // 
            // cmdShowHideOutputLog
            // 
            this.cmdShowHideOutputLog.Location = new System.Drawing.Point(512, 359);
            this.cmdShowHideOutputLog.Name = "cmdShowHideOutputLog";
            this.cmdShowHideOutputLog.Size = new System.Drawing.Size(93, 44);
            this.cmdShowHideOutputLog.TabIndex = 97;
            this.cmdShowHideOutputLog.Text = "Show/Hide\r\nOutput Log";
            this.cmdShowHideOutputLog.UseVisualStyleBackColor = true;
            this.cmdShowHideOutputLog.Click += new System.EventHandler(this.cmdShowHideOutputLog_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 544);
            this.Controls.Add(this.cmdShowHideOutputLog);
            this.Controls.Add(this.cmdCreateDb);
            this.Controls.Add(this.txtDatabaseKey);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmdGetDatabaseFileName);
            this.Controls.Add(this.txtDatabaseFile);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDatabaseName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdBuildQuery);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtSQLQuery);
            this.Controls.Add(this.chkShowStackTraceOnError);
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
            this.grpTestsToRun.ResumeLayout(false);
            this.grpTestsToRun.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.keyValsDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeyValTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.Button cmdCreateDb;
        internal System.Windows.Forms.TextBox txtDatabaseKey;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button cmdGetDatabaseFileName;
        internal System.Windows.Forms.TextBox txtDatabaseFile;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox txtPassword;
        internal System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdBuildQuery;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.TextBox txtSQLQuery;
        internal System.Windows.Forms.CheckBox chkShowStackTraceOnError;
        private System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        private System.Windows.Forms.GroupBox grpTestsToRun;
        private System.Windows.Forms.CheckBox chkImportDataTable;
        internal System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.CheckBox chkCreateTableTest;
        private System.Windows.Forms.CheckBox chkDataReaderToDataTableTest;
        private System.Windows.Forms.CheckBox chkDataTableTest;
        internal System.Windows.Forms.CheckBox chkDataSetTest;
        internal System.Windows.Forms.CheckBox chkDataReaderTest;
        internal System.Windows.Forms.CheckBox chkConnectionStringTest;
        private System.Windows.Forms.CheckBox chkGetStaticKeysTest;
        private System.Windows.Forms.CheckBox chkShowDateTimeTest;
        private System.Windows.Forms.Button cmdRunTests;
        private System.Windows.Forms.Button cmdExit;
        internal System.Data.DataSet keyValsDataSet;
        internal System.Data.DataTable KeyValTable;
        internal System.Data.DataColumn AppSetting;
        internal System.Data.DataColumn SettingValue;
        private System.Windows.Forms.ToolTip mainFormToolTips;
        private System.Windows.Forms.CheckBox chkGetSchemaTest;
        internal System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdShowHideOutputLog;
    }
}

