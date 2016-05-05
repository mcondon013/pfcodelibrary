namespace TestprogMsOracle
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
            this.chkIsStoredProcedure = new System.Windows.Forms.CheckBox();
            this.cmdBuildQuery = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSQLQuery = new System.Windows.Forms.TextBox();
            this.chkUseIntegratedSecurity = new System.Windows.Forms.CheckBox();
            this.txtDataSource = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblUserId = new System.Windows.Forms.Label();
            this.chkShowStackTraceOnError = new System.Windows.Forms.CheckBox();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.grpTestsToRun = new System.Windows.Forms.GroupBox();
            this.chkImportDataTable = new System.Windows.Forms.CheckBox();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.chkCreateTableTest = new System.Windows.Forms.CheckBox();
            this.chkDataReaderToDataTableTest = new System.Windows.Forms.CheckBox();
            this.chkDataTableTest = new System.Windows.Forms.CheckBox();
            this.chkDataSetTest = new System.Windows.Forms.CheckBox();
            this.chkDataReaderTest = new System.Windows.Forms.CheckBox();
            this.chkConnectionStringTest = new System.Windows.Forms.CheckBox();
            this.chkGetStaticKeysTest = new System.Windows.Forms.CheckBox();
            this.keyValsDataSet = new System.Data.DataSet();
            this.KeyValTable = new System.Data.DataTable();
            this.AppSetting = new System.Data.DataColumn();
            this.SettingValue = new System.Data.DataColumn();
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
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogMsOracle\\InitWinFormsAppWithExtend" +
    "edOptions\\InitWinFormsHelpFile.chm";
            // 
            // cmdRunTests
            // 
            this.appHelpProvider.SetHelpKeyword(this.cmdRunTests, "Run Tests");
            this.appHelpProvider.SetHelpNavigator(this.cmdRunTests, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.appHelpProvider.SetHelpString(this.cmdRunTests, "Help for Run Tests: See Help File.");
            this.cmdRunTests.Location = new System.Drawing.Point(510, 58);
            this.cmdRunTests.Name = "cmdRunTests";
            this.appHelpProvider.SetShowHelp(this.cmdRunTests, true);
            this.cmdRunTests.Size = new System.Drawing.Size(93, 37);
            this.cmdRunTests.TabIndex = 47;
            this.cmdRunTests.Text = "&Run Tests";
            this.cmdRunTests.UseVisualStyleBackColor = true;
            this.cmdRunTests.Click += new System.EventHandler(this.cmdRunTests_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(510, 421);
            this.cmdExit.Name = "cmdExit";
            this.appHelpProvider.SetShowHelp(this.cmdExit, true);
            this.cmdExit.Size = new System.Drawing.Size(93, 37);
            this.cmdExit.TabIndex = 48;
            this.cmdExit.Text = "E&xit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // chkIsStoredProcedure
            // 
            this.chkIsStoredProcedure.AutoSize = true;
            this.chkIsStoredProcedure.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIsStoredProcedure.Location = new System.Drawing.Point(205, 141);
            this.chkIsStoredProcedure.Name = "chkIsStoredProcedure";
            this.chkIsStoredProcedure.Size = new System.Drawing.Size(120, 17);
            this.chkIsStoredProcedure.TabIndex = 61;
            this.chkIsStoredProcedure.Text = "Is Stored Procedure";
            this.chkIsStoredProcedure.UseVisualStyleBackColor = true;
            // 
            // cmdBuildQuery
            // 
            this.cmdBuildQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBuildQuery.Location = new System.Drawing.Point(423, 139);
            this.cmdBuildQuery.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmdBuildQuery.Name = "cmdBuildQuery";
            this.cmdBuildQuery.Size = new System.Drawing.Size(38, 14);
            this.cmdBuildQuery.TabIndex = 60;
            this.cmdBuildQuery.Text = ">>";
            this.cmdBuildQuery.UseVisualStyleBackColor = true;
            this.cmdBuildQuery.Click += new System.EventHandler(this.cmdBuildQuery_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 141);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 59;
            this.label7.Text = "Query to run:";
            // 
            // txtSQLQuery
            // 
            this.txtSQLQuery.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSQLQuery.Location = new System.Drawing.Point(36, 159);
            this.txtSQLQuery.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtSQLQuery.Multiline = true;
            this.txtSQLQuery.Name = "txtSQLQuery";
            this.txtSQLQuery.Size = new System.Drawing.Size(425, 92);
            this.txtSQLQuery.TabIndex = 58;
            // 
            // chkUseIntegratedSecurity
            // 
            this.chkUseIntegratedSecurity.AutoSize = true;
            this.chkUseIntegratedSecurity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUseIntegratedSecurity.Location = new System.Drawing.Point(321, 85);
            this.chkUseIntegratedSecurity.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chkUseIntegratedSecurity.Name = "chkUseIntegratedSecurity";
            this.chkUseIntegratedSecurity.Size = new System.Drawing.Size(140, 17);
            this.chkUseIntegratedSecurity.TabIndex = 57;
            this.chkUseIntegratedSecurity.Text = "Use Integrated Security:";
            this.chkUseIntegratedSecurity.UseVisualStyleBackColor = true;
            // 
            // txtDataSource
            // 
            this.txtDataSource.Location = new System.Drawing.Point(124, 53);
            this.txtDataSource.Name = "txtDataSource";
            this.txtDataSource.Size = new System.Drawing.Size(337, 20);
            this.txtDataSource.TabIndex = 56;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(124, 108);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(153, 20);
            this.txtPassword.TabIndex = 55;
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(124, 82);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(153, 20);
            this.txtUserId.TabIndex = 54;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Data Source:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Password:";
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(36, 82);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(41, 13);
            this.lblUserId.TabIndex = 51;
            this.lblUserId.Text = "UserId:";
            // 
            // chkShowStackTraceOnError
            // 
            this.chkShowStackTraceOnError.AutoSize = true;
            this.chkShowStackTraceOnError.Location = new System.Drawing.Point(39, 484);
            this.chkShowStackTraceOnError.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chkShowStackTraceOnError.Name = "chkShowStackTraceOnError";
            this.chkShowStackTraceOnError.Size = new System.Drawing.Size(155, 17);
            this.chkShowStackTraceOnError.TabIndex = 50;
            this.chkShowStackTraceOnError.Text = "Show Stack Trace on Error";
            this.chkShowStackTraceOnError.UseVisualStyleBackColor = true;
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(269, 484);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 49;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // grpTestsToRun
            // 
            this.grpTestsToRun.Controls.Add(this.chkImportDataTable);
            this.grpTestsToRun.Controls.Add(this.txtTableName);
            this.grpTestsToRun.Controls.Add(this.chkCreateTableTest);
            this.grpTestsToRun.Controls.Add(this.chkDataReaderToDataTableTest);
            this.grpTestsToRun.Controls.Add(this.chkDataTableTest);
            this.grpTestsToRun.Controls.Add(this.chkDataSetTest);
            this.grpTestsToRun.Controls.Add(this.chkDataReaderTest);
            this.grpTestsToRun.Controls.Add(this.chkConnectionStringTest);
            this.grpTestsToRun.Controls.Add(this.chkGetStaticKeysTest);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 268);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 195);
            this.grpTestsToRun.TabIndex = 46;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // chkImportDataTable
            // 
            this.chkImportDataTable.AutoSize = true;
            this.chkImportDataTable.Location = new System.Drawing.Point(230, 127);
            this.chkImportDataTable.Name = "chkImportDataTable";
            this.chkImportDataTable.Size = new System.Drawing.Size(108, 17);
            this.chkImportDataTable.TabIndex = 70;
            this.chkImportDataTable.Text = "Import DataTable";
            this.chkImportDataTable.UseVisualStyleBackColor = true;
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(230, 54);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(172, 20);
            this.txtTableName.TabIndex = 69;
            // 
            // chkCreateTableTest
            // 
            this.chkCreateTableTest.AutoSize = true;
            this.chkCreateTableTest.Location = new System.Drawing.Point(230, 33);
            this.chkCreateTableTest.Name = "chkCreateTableTest";
            this.chkCreateTableTest.Size = new System.Drawing.Size(111, 17);
            this.chkCreateTableTest.TabIndex = 68;
            this.chkCreateTableTest.Text = "Create Table Test";
            this.chkCreateTableTest.UseVisualStyleBackColor = true;
            // 
            // chkDataReaderToDataTableTest
            // 
            this.chkDataReaderToDataTableTest.AutoSize = true;
            this.chkDataReaderToDataTableTest.Location = new System.Drawing.Point(17, 149);
            this.chkDataReaderToDataTableTest.Name = "chkDataReaderToDataTableTest";
            this.chkDataReaderToDataTableTest.Size = new System.Drawing.Size(173, 17);
            this.chkDataReaderToDataTableTest.TabIndex = 67;
            this.chkDataReaderToDataTableTest.Text = "DataReader to DataTable Test";
            this.chkDataReaderToDataTableTest.UseVisualStyleBackColor = true;
            // 
            // chkDataTableTest
            // 
            this.chkDataTableTest.AutoSize = true;
            this.chkDataTableTest.Location = new System.Drawing.Point(17, 103);
            this.chkDataTableTest.Name = "chkDataTableTest";
            this.chkDataTableTest.Size = new System.Drawing.Size(103, 17);
            this.chkDataTableTest.TabIndex = 66;
            this.chkDataTableTest.Text = "Data Table Test";
            this.chkDataTableTest.UseVisualStyleBackColor = true;
            // 
            // chkDataSetTest
            // 
            this.chkDataSetTest.AutoSize = true;
            this.chkDataSetTest.Location = new System.Drawing.Point(17, 126);
            this.chkDataSetTest.Name = "chkDataSetTest";
            this.chkDataSetTest.Size = new System.Drawing.Size(89, 17);
            this.chkDataSetTest.TabIndex = 65;
            this.chkDataSetTest.Text = "DataSet Test";
            this.chkDataSetTest.UseVisualStyleBackColor = true;
            // 
            // chkDataReaderTest
            // 
            this.chkDataReaderTest.AutoSize = true;
            this.chkDataReaderTest.Location = new System.Drawing.Point(17, 80);
            this.chkDataReaderTest.Name = "chkDataReaderTest";
            this.chkDataReaderTest.Size = new System.Drawing.Size(111, 17);
            this.chkDataReaderTest.TabIndex = 64;
            this.chkDataReaderTest.Text = "Data Reader Test";
            this.chkDataReaderTest.UseVisualStyleBackColor = true;
            // 
            // chkConnectionStringTest
            // 
            this.chkConnectionStringTest.AutoSize = true;
            this.chkConnectionStringTest.Location = new System.Drawing.Point(17, 56);
            this.chkConnectionStringTest.Name = "chkConnectionStringTest";
            this.chkConnectionStringTest.Size = new System.Drawing.Size(134, 17);
            this.chkConnectionStringTest.TabIndex = 63;
            this.chkConnectionStringTest.Text = "Connection String Test";
            this.chkConnectionStringTest.UseVisualStyleBackColor = true;
            // 
            // chkGetStaticKeysTest
            // 
            this.chkGetStaticKeysTest.AutoSize = true;
            this.chkGetStaticKeysTest.Location = new System.Drawing.Point(17, 33);
            this.chkGetStaticKeysTest.Name = "chkGetStaticKeysTest";
            this.chkGetStaticKeysTest.Size = new System.Drawing.Size(114, 17);
            this.chkGetStaticKeysTest.TabIndex = 0;
            this.chkGetStaticKeysTest.Text = "GetStaticKeysTest";
            this.chkGetStaticKeysTest.UseVisualStyleBackColor = true;
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
            this.KeyValTable.TableName = "HR.KeyValTable";
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
            this.cmdShowHideOutputLog.Location = new System.Drawing.Point(510, 240);
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
            this.ClientSize = new System.Drawing.Size(638, 527);
            this.Controls.Add(this.cmdShowHideOutputLog);
            this.Controls.Add(this.chkIsStoredProcedure);
            this.Controls.Add(this.cmdBuildQuery);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtSQLQuery);
            this.Controls.Add(this.chkUseIntegratedSecurity);
            this.Controls.Add(this.txtDataSource);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblUserId);
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
        internal System.Windows.Forms.CheckBox chkIsStoredProcedure;
        private System.Windows.Forms.Button cmdBuildQuery;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.TextBox txtSQLQuery;
        internal System.Windows.Forms.CheckBox chkUseIntegratedSecurity;
        internal System.Windows.Forms.TextBox txtDataSource;
        internal System.Windows.Forms.TextBox txtPassword;
        internal System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblUserId;
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
        private System.Windows.Forms.Button cmdRunTests;
        private System.Windows.Forms.Button cmdExit;
        internal System.Data.DataSet keyValsDataSet;
        internal System.Data.DataTable KeyValTable;
        internal System.Data.DataColumn AppSetting;
        internal System.Data.DataColumn SettingValue;
        private System.Windows.Forms.Button cmdShowHideOutputLog;
    }
}

