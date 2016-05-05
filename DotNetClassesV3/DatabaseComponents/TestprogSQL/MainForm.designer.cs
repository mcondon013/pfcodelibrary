namespace TestprogSQL
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
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpContents = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHelpTutorial = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHelpContact = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.cmdQuickTest = new System.Windows.Forms.Button();
            this.chkIsStoredProcedure = new System.Windows.Forms.CheckBox();
            this.cmdBuildQuery = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSQLQuery = new System.Windows.Forms.TextBox();
            this.txtWorkstationId = new System.Windows.Forms.TextBox();
            this.txtApplicationName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.chkUseIntegratedSecurity = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkShowStackTraceOnError = new System.Windows.Forms.CheckBox();
            this.grpTestsToRun = new System.Windows.Forms.GroupBox();
            this.txtSchemaName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkImportDataTable = new System.Windows.Forms.CheckBox();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.chkCreateTableTest = new System.Windows.Forms.CheckBox();
            this.chkDataReaderToDataTableTest = new System.Windows.Forms.CheckBox();
            this.chkDataTableTest = new System.Windows.Forms.CheckBox();
            this.chkDataSetTest = new System.Windows.Forms.CheckBox();
            this.chkDataReaderTest = new System.Windows.Forms.CheckBox();
            this.chkConnectionStringTest = new System.Windows.Forms.CheckBox();
            this.chkMessageLogTest = new System.Windows.Forms.CheckBox();
            this.chkUseAsyncProcessing = new System.Windows.Forms.CheckBox();
            this.keyValsDataSet = new System.Data.DataSet();
            this.KeyValTable = new System.Data.DataTable();
            this.AppSetting = new System.Data.DataColumn();
            this.SettingValue = new System.Data.DataColumn();
            this.chkGetQueryDataSchema = new System.Windows.Forms.CheckBox();
            this.MainMenu.SuspendLayout();
            this.grpTestsToRun.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.keyValsDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeyValTable)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(443, 421);
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
            this.cmdRunTests.Location = new System.Drawing.Point(443, 167);
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
            this.mnuFile,
            this.mnuTools,
            this.mnuHelp});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(580, 24);
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
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsOptions});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(48, 20);
            this.mnuTools.Text = "&Tools";
            // 
            // mnuToolsOptions
            // 
            this.mnuToolsOptions.Name = "mnuToolsOptions";
            this.mnuToolsOptions.Size = new System.Drawing.Size(116, 22);
            this.mnuToolsOptions.Text = "&Options";
            this.mnuToolsOptions.Click += new System.EventHandler(this.mnuToolsOptions_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpContents,
            this.mnuHelpIndex,
            this.mnuHelpSearch,
            this.toolStripSeparator2,
            this.mnuHelpTutorial,
            this.toolStripSeparator3,
            this.mnuHelpContact,
            this.toolStripSeparator4,
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpContents
            // 
            this.mnuHelpContents.Name = "mnuHelpContents";
            this.mnuHelpContents.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpContents.Text = "Contents";
            this.mnuHelpContents.Click += new System.EventHandler(this.mnuHelpContents_Click);
            // 
            // mnuHelpIndex
            // 
            this.mnuHelpIndex.Name = "mnuHelpIndex";
            this.mnuHelpIndex.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpIndex.Text = "Index";
            this.mnuHelpIndex.Click += new System.EventHandler(this.mnuHelpIndex_Click);
            // 
            // mnuHelpSearch
            // 
            this.mnuHelpSearch.Name = "mnuHelpSearch";
            this.mnuHelpSearch.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpSearch.Text = "Search";
            this.mnuHelpSearch.Click += new System.EventHandler(this.mnuHelpSearch_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(219, 6);
            // 
            // mnuHelpTutorial
            // 
            this.mnuHelpTutorial.Name = "mnuHelpTutorial";
            this.mnuHelpTutorial.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpTutorial.Text = "Tutorial";
            this.mnuHelpTutorial.Click += new System.EventHandler(this.mnuHelpTutorial_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(219, 6);
            // 
            // mnuHelpContact
            // 
            this.mnuHelpContact.Name = "mnuHelpContact";
            this.mnuHelpContact.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpContact.Text = "Contact ProFast Computing";
            this.mnuHelpContact.Click += new System.EventHandler(this.mnuHelpContact_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(219, 6);
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpAbout.Text = "&About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(217, 507);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 8;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogSQL\\TestprogSQL\\InitWinFormsHelpFi" +
    "le.chm";
            // 
            // cmdQuickTest
            // 
            this.cmdQuickTest.Location = new System.Drawing.Point(442, 289);
            this.cmdQuickTest.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmdQuickTest.Name = "cmdQuickTest";
            this.cmdQuickTest.Size = new System.Drawing.Size(92, 38);
            this.cmdQuickTest.TabIndex = 9;
            this.cmdQuickTest.Text = "&Quick Test";
            this.cmdQuickTest.UseVisualStyleBackColor = true;
            this.cmdQuickTest.Click += new System.EventHandler(this.cmdQuickTest_Click);
            // 
            // chkIsStoredProcedure
            // 
            this.chkIsStoredProcedure.AutoSize = true;
            this.chkIsStoredProcedure.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIsStoredProcedure.Location = new System.Drawing.Point(162, 165);
            this.chkIsStoredProcedure.Name = "chkIsStoredProcedure";
            this.chkIsStoredProcedure.Size = new System.Drawing.Size(120, 17);
            this.chkIsStoredProcedure.TabIndex = 42;
            this.chkIsStoredProcedure.Text = "Is Stored Procedure";
            this.chkIsStoredProcedure.UseVisualStyleBackColor = true;
            // 
            // cmdBuildQuery
            // 
            this.cmdBuildQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBuildQuery.Location = new System.Drawing.Point(360, 167);
            this.cmdBuildQuery.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmdBuildQuery.Name = "cmdBuildQuery";
            this.cmdBuildQuery.Size = new System.Drawing.Size(38, 14);
            this.cmdBuildQuery.TabIndex = 41;
            this.cmdBuildQuery.Text = ">>";
            this.cmdBuildQuery.UseVisualStyleBackColor = true;
            this.cmdBuildQuery.Click += new System.EventHandler(this.cmdBuildQuery_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 167);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 40;
            this.label7.Text = "Query to run:";
            // 
            // txtSQLQuery
            // 
            this.txtSQLQuery.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSQLQuery.Location = new System.Drawing.Point(35, 185);
            this.txtSQLQuery.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtSQLQuery.Multiline = true;
            this.txtSQLQuery.Name = "txtSQLQuery";
            this.txtSQLQuery.Size = new System.Drawing.Size(364, 78);
            this.txtSQLQuery.TabIndex = 39;
            // 
            // txtWorkstationId
            // 
            this.txtWorkstationId.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWorkstationId.Location = new System.Drawing.Point(162, 132);
            this.txtWorkstationId.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtWorkstationId.Name = "txtWorkstationId";
            this.txtWorkstationId.Size = new System.Drawing.Size(146, 19);
            this.txtWorkstationId.TabIndex = 38;
            // 
            // txtApplicationName
            // 
            this.txtApplicationName.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApplicationName.Location = new System.Drawing.Point(162, 102);
            this.txtApplicationName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtApplicationName.Name = "txtApplicationName";
            this.txtApplicationName.Size = new System.Drawing.Size(146, 19);
            this.txtApplicationName.TabIndex = 37;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 138);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "Workstation Id:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 102);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Application Name:";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(408, 68);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(126, 19);
            this.txtPassword.TabIndex = 34;
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(408, 35);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(128, 19);
            this.txtUsername.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(346, 68);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(344, 35);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Username:";
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDatabaseName.Location = new System.Drawing.Point(162, 68);
            this.txtDatabaseName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(146, 19);
            this.txtDatabaseName.TabIndex = 30;
            // 
            // txtServerName
            // 
            this.txtServerName.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerName.Location = new System.Drawing.Point(162, 35);
            this.txtServerName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(146, 19);
            this.txtServerName.TabIndex = 29;
            // 
            // chkUseIntegratedSecurity
            // 
            this.chkUseIntegratedSecurity.AutoSize = true;
            this.chkUseIntegratedSecurity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUseIntegratedSecurity.Location = new System.Drawing.Point(347, 102);
            this.chkUseIntegratedSecurity.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chkUseIntegratedSecurity.Name = "chkUseIntegratedSecurity";
            this.chkUseIntegratedSecurity.Size = new System.Drawing.Size(140, 17);
            this.chkUseIntegratedSecurity.TabIndex = 28;
            this.chkUseIntegratedSecurity.Text = "Use Integrated Security:";
            this.chkUseIntegratedSecurity.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 68);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Database Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Server Name:";
            // 
            // chkShowStackTraceOnError
            // 
            this.chkShowStackTraceOnError.AutoSize = true;
            this.chkShowStackTraceOnError.Location = new System.Drawing.Point(30, 507);
            this.chkShowStackTraceOnError.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chkShowStackTraceOnError.Name = "chkShowStackTraceOnError";
            this.chkShowStackTraceOnError.Size = new System.Drawing.Size(155, 17);
            this.chkShowStackTraceOnError.TabIndex = 25;
            this.chkShowStackTraceOnError.Text = "Show Stack Trace on Error";
            this.chkShowStackTraceOnError.UseVisualStyleBackColor = true;
            // 
            // grpTestsToRun
            // 
            this.grpTestsToRun.Controls.Add(this.chkGetQueryDataSchema);
            this.grpTestsToRun.Controls.Add(this.txtSchemaName);
            this.grpTestsToRun.Controls.Add(this.label8);
            this.grpTestsToRun.Controls.Add(this.chkImportDataTable);
            this.grpTestsToRun.Controls.Add(this.txtTableName);
            this.grpTestsToRun.Controls.Add(this.chkCreateTableTest);
            this.grpTestsToRun.Controls.Add(this.chkDataReaderToDataTableTest);
            this.grpTestsToRun.Controls.Add(this.chkDataTableTest);
            this.grpTestsToRun.Controls.Add(this.chkDataSetTest);
            this.grpTestsToRun.Controls.Add(this.chkDataReaderTest);
            this.grpTestsToRun.Controls.Add(this.chkConnectionStringTest);
            this.grpTestsToRun.Controls.Add(this.chkMessageLogTest);
            this.grpTestsToRun.Location = new System.Drawing.Point(38, 289);
            this.grpTestsToRun.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.grpTestsToRun.Size = new System.Drawing.Size(364, 212);
            this.grpTestsToRun.TabIndex = 24;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Tests to Run";
            // 
            // txtSchemaName
            // 
            this.txtSchemaName.Location = new System.Drawing.Point(187, 88);
            this.txtSchemaName.Name = "txtSchemaName";
            this.txtSchemaName.Size = new System.Drawing.Size(172, 20);
            this.txtSchemaName.TabIndex = 75;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(187, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 74;
            this.label8.Text = "Schema Name:";
            // 
            // chkImportDataTable
            // 
            this.chkImportDataTable.AutoSize = true;
            this.chkImportDataTable.Location = new System.Drawing.Point(187, 147);
            this.chkImportDataTable.Name = "chkImportDataTable";
            this.chkImportDataTable.Size = new System.Drawing.Size(108, 17);
            this.chkImportDataTable.TabIndex = 62;
            this.chkImportDataTable.Text = "Import DataTable";
            this.chkImportDataTable.UseVisualStyleBackColor = true;
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(187, 51);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(172, 20);
            this.txtTableName.TabIndex = 7;
            // 
            // chkCreateTableTest
            // 
            this.chkCreateTableTest.AutoSize = true;
            this.chkCreateTableTest.Location = new System.Drawing.Point(187, 30);
            this.chkCreateTableTest.Name = "chkCreateTableTest";
            this.chkCreateTableTest.Size = new System.Drawing.Size(111, 17);
            this.chkCreateTableTest.TabIndex = 6;
            this.chkCreateTableTest.Text = "Create Table Test";
            this.chkCreateTableTest.UseVisualStyleBackColor = true;
            // 
            // chkDataReaderToDataTableTest
            // 
            this.chkDataReaderToDataTableTest.AutoSize = true;
            this.chkDataReaderToDataTableTest.Location = new System.Drawing.Point(13, 147);
            this.chkDataReaderToDataTableTest.Name = "chkDataReaderToDataTableTest";
            this.chkDataReaderToDataTableTest.Size = new System.Drawing.Size(173, 17);
            this.chkDataReaderToDataTableTest.TabIndex = 5;
            this.chkDataReaderToDataTableTest.Text = "DataReader to DataTable Test";
            this.chkDataReaderToDataTableTest.UseVisualStyleBackColor = true;
            // 
            // chkDataTableTest
            // 
            this.chkDataTableTest.AutoSize = true;
            this.chkDataTableTest.Location = new System.Drawing.Point(13, 101);
            this.chkDataTableTest.Name = "chkDataTableTest";
            this.chkDataTableTest.Size = new System.Drawing.Size(103, 17);
            this.chkDataTableTest.TabIndex = 4;
            this.chkDataTableTest.Text = "Data Table Test";
            this.chkDataTableTest.UseVisualStyleBackColor = true;
            // 
            // chkDataSetTest
            // 
            this.chkDataSetTest.AutoSize = true;
            this.chkDataSetTest.Location = new System.Drawing.Point(13, 124);
            this.chkDataSetTest.Name = "chkDataSetTest";
            this.chkDataSetTest.Size = new System.Drawing.Size(89, 17);
            this.chkDataSetTest.TabIndex = 3;
            this.chkDataSetTest.Text = "DataSet Test";
            this.chkDataSetTest.UseVisualStyleBackColor = true;
            // 
            // chkDataReaderTest
            // 
            this.chkDataReaderTest.AutoSize = true;
            this.chkDataReaderTest.Location = new System.Drawing.Point(13, 78);
            this.chkDataReaderTest.Name = "chkDataReaderTest";
            this.chkDataReaderTest.Size = new System.Drawing.Size(111, 17);
            this.chkDataReaderTest.TabIndex = 2;
            this.chkDataReaderTest.Text = "Data Reader Test";
            this.chkDataReaderTest.UseVisualStyleBackColor = true;
            // 
            // chkConnectionStringTest
            // 
            this.chkConnectionStringTest.AutoSize = true;
            this.chkConnectionStringTest.Location = new System.Drawing.Point(13, 54);
            this.chkConnectionStringTest.Name = "chkConnectionStringTest";
            this.chkConnectionStringTest.Size = new System.Drawing.Size(134, 17);
            this.chkConnectionStringTest.TabIndex = 1;
            this.chkConnectionStringTest.Text = "Connection String Test";
            this.chkConnectionStringTest.UseVisualStyleBackColor = true;
            // 
            // chkMessageLogTest
            // 
            this.chkMessageLogTest.AutoSize = true;
            this.chkMessageLogTest.Location = new System.Drawing.Point(13, 30);
            this.chkMessageLogTest.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chkMessageLogTest.Name = "chkMessageLogTest";
            this.chkMessageLogTest.Size = new System.Drawing.Size(114, 17);
            this.chkMessageLogTest.TabIndex = 0;
            this.chkMessageLogTest.Text = "Message Log Test";
            this.chkMessageLogTest.UseVisualStyleBackColor = true;
            // 
            // chkUseAsyncProcessing
            // 
            this.chkUseAsyncProcessing.AutoSize = true;
            this.chkUseAsyncProcessing.Location = new System.Drawing.Point(349, 134);
            this.chkUseAsyncProcessing.Name = "chkUseAsyncProcessing";
            this.chkUseAsyncProcessing.Size = new System.Drawing.Size(132, 17);
            this.chkUseAsyncProcessing.TabIndex = 43;
            this.chkUseAsyncProcessing.Text = "Use Async Processing";
            this.chkUseAsyncProcessing.UseVisualStyleBackColor = true;
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
            this.KeyValTable.TableName = "dbo.KeyValTable";
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
            // chkGetQueryDataSchema
            // 
            this.chkGetQueryDataSchema.AutoSize = true;
            this.chkGetQueryDataSchema.Location = new System.Drawing.Point(13, 171);
            this.chkGetQueryDataSchema.Name = "chkGetQueryDataSchema";
            this.chkGetQueryDataSchema.Size = new System.Drawing.Size(142, 17);
            this.chkGetQueryDataSchema.TabIndex = 76;
            this.chkGetQueryDataSchema.Text = "Get Query Data Schema";
            this.chkGetQueryDataSchema.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AcceptButton = this.cmdRunTests;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(580, 556);
            this.Controls.Add(this.chkUseAsyncProcessing);
            this.Controls.Add(this.chkIsStoredProcedure);
            this.Controls.Add(this.cmdBuildQuery);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtSQLQuery);
            this.Controls.Add(this.txtWorkstationId);
            this.Controls.Add(this.txtApplicationName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDatabaseName);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.chkUseIntegratedSecurity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkShowStackTraceOnError);
            this.Controls.Add(this.grpTestsToRun);
            this.Controls.Add(this.cmdQuickTest);
            this.Controls.Add(this.chkEraseOutputBeforeEachTest);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.cmdRunTests);
            this.Controls.Add(this.cmdExit);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestprogSQL";
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

        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdRunTests;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpContents;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpIndex;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpTutorial;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpContact;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        internal System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        private System.Windows.Forms.Button cmdQuickTest;
        internal System.Windows.Forms.CheckBox chkIsStoredProcedure;
        private System.Windows.Forms.Button cmdBuildQuery;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.TextBox txtSQLQuery;
        internal System.Windows.Forms.TextBox txtWorkstationId;
        internal System.Windows.Forms.TextBox txtApplicationName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox txtPassword;
        internal System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtDatabaseName;
        internal System.Windows.Forms.TextBox txtServerName;
        internal System.Windows.Forms.CheckBox chkUseIntegratedSecurity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.CheckBox chkShowStackTraceOnError;
        private System.Windows.Forms.GroupBox grpTestsToRun;
        internal System.Windows.Forms.CheckBox chkDataSetTest;
        internal System.Windows.Forms.CheckBox chkDataReaderTest;
        internal System.Windows.Forms.CheckBox chkConnectionStringTest;
        internal System.Windows.Forms.CheckBox chkMessageLogTest;
        internal System.Windows.Forms.CheckBox chkUseAsyncProcessing;
        private System.Windows.Forms.CheckBox chkDataReaderToDataTableTest;
        private System.Windows.Forms.CheckBox chkDataTableTest;
        internal System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.CheckBox chkCreateTableTest;
        private System.Windows.Forms.CheckBox chkImportDataTable;
        internal System.Data.DataSet keyValsDataSet;
        internal System.Data.DataTable KeyValTable;
        internal System.Data.DataColumn AppSetting;
        internal System.Data.DataColumn SettingValue;
        internal System.Windows.Forms.TextBox txtSchemaName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkGetQueryDataSchema;
    }
}

