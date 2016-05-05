namespace TestprogOleDb
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
            this.chkGetQueryDataSchema = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCatalogName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSchemaName = new System.Windows.Forms.TextBox();
            this.chkUseCustomSQL = new System.Windows.Forms.CheckBox();
            this.chkUseOleDbBuilder = new System.Windows.Forms.CheckBox();
            this.chkImportDataTable = new System.Windows.Forms.CheckBox();
            this.chkDataReaderToDataTableTest = new System.Windows.Forms.CheckBox();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.chkCreateTableTest = new System.Windows.Forms.CheckBox();
            this.chkRunDataSetTest = new System.Windows.Forms.CheckBox();
            this.chkRunDataTableTest = new System.Windows.Forms.CheckBox();
            this.chkRunDataReaderTest = new System.Windows.Forms.CheckBox();
            this.chkRunConnectionTest = new System.Windows.Forms.CheckBox();
            this.cboConnectionString = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSqlQuery = new System.Windows.Forms.TextBox();
            this.cmdBuildQuery = new System.Windows.Forms.Button();
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
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(523, 445);
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
            this.cmdRunTests.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.appHelpProvider.SetHelpKeyword(this.cmdRunTests, "Run Tests");
            this.appHelpProvider.SetHelpNavigator(this.cmdRunTests, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.appHelpProvider.SetHelpString(this.cmdRunTests, "Help for Run Tests: See Help File.");
            this.cmdRunTests.Location = new System.Drawing.Point(523, 297);
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
            this.grpTestsToRun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTestsToRun.Controls.Add(this.chkGetQueryDataSchema);
            this.grpTestsToRun.Controls.Add(this.label4);
            this.grpTestsToRun.Controls.Add(this.txtCatalogName);
            this.grpTestsToRun.Controls.Add(this.label3);
            this.grpTestsToRun.Controls.Add(this.txtSchemaName);
            this.grpTestsToRun.Controls.Add(this.chkUseCustomSQL);
            this.grpTestsToRun.Controls.Add(this.chkUseOleDbBuilder);
            this.grpTestsToRun.Controls.Add(this.chkImportDataTable);
            this.grpTestsToRun.Controls.Add(this.chkDataReaderToDataTableTest);
            this.grpTestsToRun.Controls.Add(this.txtTableName);
            this.grpTestsToRun.Controls.Add(this.chkCreateTableTest);
            this.grpTestsToRun.Controls.Add(this.chkRunDataSetTest);
            this.grpTestsToRun.Controls.Add(this.chkRunDataTableTest);
            this.grpTestsToRun.Controls.Add(this.chkRunDataReaderTest);
            this.grpTestsToRun.Controls.Add(this.chkRunConnectionTest);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 251);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 245);
            this.grpTestsToRun.TabIndex = 0;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // chkGetQueryDataSchema
            // 
            this.chkGetQueryDataSchema.AutoSize = true;
            this.chkGetQueryDataSchema.Location = new System.Drawing.Point(22, 205);
            this.chkGetQueryDataSchema.Name = "chkGetQueryDataSchema";
            this.chkGetQueryDataSchema.Size = new System.Drawing.Size(142, 17);
            this.chkGetQueryDataSchema.TabIndex = 86;
            this.chkGetQueryDataSchema.Text = "Get Query Data Schema";
            this.chkGetQueryDataSchema.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(238, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 85;
            this.label4.Text = "Catalog";
            // 
            // txtCatalogName
            // 
            this.txtCatalogName.Location = new System.Drawing.Point(290, 194);
            this.txtCatalogName.Name = "txtCatalogName";
            this.txtCatalogName.Size = new System.Drawing.Size(100, 20);
            this.txtCatalogName.TabIndex = 84;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(238, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 83;
            this.label3.Text = "Schema";
            // 
            // txtSchemaName
            // 
            this.txtSchemaName.Location = new System.Drawing.Point(290, 167);
            this.txtSchemaName.Name = "txtSchemaName";
            this.txtSchemaName.Size = new System.Drawing.Size(100, 20);
            this.txtSchemaName.TabIndex = 82;
            // 
            // chkUseCustomSQL
            // 
            this.chkUseCustomSQL.AutoSize = true;
            this.chkUseCustomSQL.Location = new System.Drawing.Point(304, 102);
            this.chkUseCustomSQL.Name = "chkUseCustomSQL";
            this.chkUseCustomSQL.Size = new System.Drawing.Size(107, 17);
            this.chkUseCustomSQL.TabIndex = 81;
            this.chkUseCustomSQL.Text = "Use Custom SQL";
            this.chkUseCustomSQL.UseVisualStyleBackColor = true;
            this.chkUseCustomSQL.CheckedChanged += new System.EventHandler(this.chkUseCustomSQL_CheckedChanged);
            // 
            // chkUseOleDbBuilder
            // 
            this.chkUseOleDbBuilder.AutoSize = true;
            this.chkUseOleDbBuilder.Location = new System.Drawing.Point(304, 79);
            this.chkUseOleDbBuilder.Name = "chkUseOleDbBuilder";
            this.chkUseOleDbBuilder.Size = new System.Drawing.Size(113, 17);
            this.chkUseOleDbBuilder.TabIndex = 80;
            this.chkUseOleDbBuilder.Text = "Use OleDb Builder";
            this.chkUseOleDbBuilder.UseVisualStyleBackColor = true;
            this.chkUseOleDbBuilder.CheckedChanged += new System.EventHandler(this.chkUseOdbcBuilder_CheckedChanged);
            // 
            // chkImportDataTable
            // 
            this.chkImportDataTable.AutoSize = true;
            this.chkImportDataTable.Location = new System.Drawing.Point(241, 136);
            this.chkImportDataTable.Name = "chkImportDataTable";
            this.chkImportDataTable.Size = new System.Drawing.Size(108, 17);
            this.chkImportDataTable.TabIndex = 79;
            this.chkImportDataTable.Text = "Import DataTable";
            this.chkImportDataTable.UseVisualStyleBackColor = true;
            // 
            // chkDataReaderToDataTableTest
            // 
            this.chkDataReaderToDataTableTest.AutoSize = true;
            this.chkDataReaderToDataTableTest.Location = new System.Drawing.Point(22, 170);
            this.chkDataReaderToDataTableTest.Name = "chkDataReaderToDataTableTest";
            this.chkDataReaderToDataTableTest.Size = new System.Drawing.Size(173, 17);
            this.chkDataReaderToDataTableTest.TabIndex = 78;
            this.chkDataReaderToDataTableTest.Text = "DataReader to DataTable Test";
            this.chkDataReaderToDataTableTest.UseVisualStyleBackColor = true;
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(241, 55);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(172, 20);
            this.txtTableName.TabIndex = 77;
            // 
            // chkCreateTableTest
            // 
            this.chkCreateTableTest.AutoSize = true;
            this.chkCreateTableTest.Location = new System.Drawing.Point(241, 34);
            this.chkCreateTableTest.Name = "chkCreateTableTest";
            this.chkCreateTableTest.Size = new System.Drawing.Size(111, 17);
            this.chkCreateTableTest.TabIndex = 76;
            this.chkCreateTableTest.Text = "Create Table Test";
            this.chkCreateTableTest.UseVisualStyleBackColor = true;
            // 
            // chkRunDataSetTest
            // 
            this.chkRunDataSetTest.AutoSize = true;
            this.chkRunDataSetTest.Location = new System.Drawing.Point(22, 136);
            this.chkRunDataSetTest.Name = "chkRunDataSetTest";
            this.chkRunDataSetTest.Size = new System.Drawing.Size(98, 17);
            this.chkRunDataSetTest.TabIndex = 3;
            this.chkRunDataSetTest.Text = "&4 DataSet Test";
            this.chkRunDataSetTest.UseVisualStyleBackColor = true;
            // 
            // chkRunDataTableTest
            // 
            this.chkRunDataTableTest.AutoSize = true;
            this.chkRunDataTableTest.Location = new System.Drawing.Point(22, 102);
            this.chkRunDataTableTest.Name = "chkRunDataTableTest";
            this.chkRunDataTableTest.Size = new System.Drawing.Size(112, 17);
            this.chkRunDataTableTest.TabIndex = 2;
            this.chkRunDataTableTest.Text = "&3 Data Table Test";
            this.chkRunDataTableTest.UseVisualStyleBackColor = true;
            // 
            // chkRunDataReaderTest
            // 
            this.chkRunDataReaderTest.AutoSize = true;
            this.chkRunDataReaderTest.Location = new System.Drawing.Point(22, 69);
            this.chkRunDataReaderTest.Name = "chkRunDataReaderTest";
            this.chkRunDataReaderTest.Size = new System.Drawing.Size(120, 17);
            this.chkRunDataReaderTest.TabIndex = 1;
            this.chkRunDataReaderTest.Text = "&2 Data Reader Test";
            this.chkRunDataReaderTest.UseVisualStyleBackColor = true;
            // 
            // chkRunConnectionTest
            // 
            this.chkRunConnectionTest.AutoSize = true;
            this.chkRunConnectionTest.Location = new System.Drawing.Point(22, 34);
            this.chkRunConnectionTest.Name = "chkRunConnectionTest";
            this.chkRunConnectionTest.Size = new System.Drawing.Size(113, 17);
            this.chkRunConnectionTest.TabIndex = 0;
            this.chkRunConnectionTest.Text = "&1 Connection Test";
            this.chkRunConnectionTest.UseVisualStyleBackColor = true;
            // 
            // cboConnectionString
            // 
            this.cboConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboConnectionString.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboConnectionString.FormattingEnabled = true;
            this.cboConnectionString.Location = new System.Drawing.Point(39, 58);
            this.cboConnectionString.MaxDropDownItems = 15;
            this.cboConnectionString.Name = "cboConnectionString";
            this.cboConnectionString.Size = new System.Drawing.Size(577, 20);
            this.cboConnectionString.TabIndex = 2;
            this.cboConnectionString.SelectedIndexChanged += new System.EventHandler(this.cboConnectionString_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Connection String:";
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(42, 502);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 8;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogOleDb\\TestprogOleDb\\InitWinFormsHe" +
    "lpFile.chm";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "QueryTo Run:";
            // 
            // txtSqlQuery
            // 
            this.txtSqlQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSqlQuery.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSqlQuery.Location = new System.Drawing.Point(42, 107);
            this.txtSqlQuery.Multiline = true;
            this.txtSqlQuery.Name = "txtSqlQuery";
            this.txtSqlQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSqlQuery.Size = new System.Drawing.Size(574, 123);
            this.txtSqlQuery.TabIndex = 10;
            // 
            // cmdBuildQuery
            // 
            this.cmdBuildQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBuildQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBuildQuery.Location = new System.Drawing.Point(578, 90);
            this.cmdBuildQuery.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmdBuildQuery.Name = "cmdBuildQuery";
            this.cmdBuildQuery.Size = new System.Drawing.Size(38, 14);
            this.cmdBuildQuery.TabIndex = 42;
            this.cmdBuildQuery.Text = ">>";
            this.cmdBuildQuery.UseVisualStyleBackColor = true;
            this.cmdBuildQuery.Click += new System.EventHandler(this.cmdBuildQuery_Click);
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
            this.cmdShowHideOutputLog.Location = new System.Drawing.Point(523, 372);
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
            this.ClientSize = new System.Drawing.Size(638, 546);
            this.Controls.Add(this.cmdShowHideOutputLog);
            this.Controls.Add(this.cmdBuildQuery);
            this.Controls.Add(this.txtSqlQuery);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboConnectionString);
            this.Controls.Add(this.chkEraseOutputBeforeEachTest);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpTestsToRun);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.cmdRunTests);
            this.Controls.Add(this.cmdExit);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Testprog for OleDb";
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
        private System.Windows.Forms.GroupBox grpTestsToRun;
        private System.Windows.Forms.CheckBox chkRunConnectionTest;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ComboBox cboConnectionString;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox txtSqlQuery;
        private System.Windows.Forms.Button cmdBuildQuery;
        private System.Windows.Forms.CheckBox chkRunDataReaderTest;
        private System.Windows.Forms.CheckBox chkRunDataSetTest;
        private System.Windows.Forms.CheckBox chkRunDataTableTest;
        internal System.Windows.Forms.CheckBox chkUseCustomSQL;
        internal System.Windows.Forms.CheckBox chkUseOleDbBuilder;
        private System.Windows.Forms.CheckBox chkImportDataTable;
        private System.Windows.Forms.CheckBox chkDataReaderToDataTableTest;
        internal System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.CheckBox chkCreateTableTest;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtCatalogName;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtSchemaName;
        internal System.Data.DataSet keyValsDataSet;
        internal System.Data.DataTable KeyValTable;
        internal System.Data.DataColumn AppSetting;
        internal System.Data.DataColumn SettingValue;
        private System.Windows.Forms.CheckBox chkGetQueryDataSchema;
        private System.Windows.Forms.Button cmdShowHideOutputLog;
    }
}

