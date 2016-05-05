namespace TestprogTableDefs
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
            this.chkGetSupportedDatabaseList = new System.Windows.Forms.CheckBox();
            this.chkImportDataFromSourceToDestination = new System.Windows.Forms.CheckBox();
            this.txtNewSchema = new System.Windows.Forms.TextBox();
            this.lblNewSchema = new System.Windows.Forms.Label();
            this.chkRunConvertedTableCreateStatements = new System.Windows.Forms.CheckBox();
            this.chkShowTableCreateStatements = new System.Windows.Forms.CheckBox();
            this.chkConvertTableDefs = new System.Windows.Forms.CheckBox();
            this.chkGetTabDefList = new System.Windows.Forms.CheckBox();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboSourceDbPlatform = new System.Windows.Forms.ComboBox();
            this.cboSourceDbConnectionString = new System.Windows.Forms.ComboBox();
            this.cboDestinationDbPlatform = new System.Windows.Forms.ComboBox();
            this.cboDestinationDbConnectionString = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtIncludePatterns = new System.Windows.Forms.TextBox();
            this.txtExcludePatterns = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.MainMenu.SuspendLayout();
            this.grpTestsToRun.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(768, 491);
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
            this.cmdRunTests.Location = new System.Drawing.Point(768, 60);
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
            this.MainMenu.Size = new System.Drawing.Size(909, 24);
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
            this.grpTestsToRun.Controls.Add(this.panel3);
            this.grpTestsToRun.Controls.Add(this.panel1);
            this.grpTestsToRun.Controls.Add(this.panel2);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 309);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(701, 215);
            this.grpTestsToRun.TabIndex = 0;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // chkGetSupportedDatabaseList
            // 
            this.chkGetSupportedDatabaseList.AutoSize = true;
            this.chkGetSupportedDatabaseList.Location = new System.Drawing.Point(14, 16);
            this.chkGetSupportedDatabaseList.Name = "chkGetSupportedDatabaseList";
            this.chkGetSupportedDatabaseList.Size = new System.Drawing.Size(168, 17);
            this.chkGetSupportedDatabaseList.TabIndex = 7;
            this.chkGetSupportedDatabaseList.Text = "Get Supported Databases List";
            this.chkGetSupportedDatabaseList.UseVisualStyleBackColor = true;
            // 
            // chkImportDataFromSourceToDestination
            // 
            this.chkImportDataFromSourceToDestination.AutoSize = true;
            this.chkImportDataFromSourceToDestination.Location = new System.Drawing.Point(233, 52);
            this.chkImportDataFromSourceToDestination.Name = "chkImportDataFromSourceToDestination";
            this.chkImportDataFromSourceToDestination.Size = new System.Drawing.Size(216, 17);
            this.chkImportDataFromSourceToDestination.TabIndex = 6;
            this.chkImportDataFromSourceToDestination.Text = "Import Data From Source To Destination";
            this.chkImportDataFromSourceToDestination.UseVisualStyleBackColor = true;
            // 
            // txtNewSchema
            // 
            this.txtNewSchema.Location = new System.Drawing.Point(115, 50);
            this.txtNewSchema.Name = "txtNewSchema";
            this.txtNewSchema.Size = new System.Drawing.Size(100, 20);
            this.txtNewSchema.TabIndex = 5;
            // 
            // lblNewSchema
            // 
            this.lblNewSchema.AutoSize = true;
            this.lblNewSchema.Location = new System.Drawing.Point(38, 50);
            this.lblNewSchema.Name = "lblNewSchema";
            this.lblNewSchema.Size = new System.Drawing.Size(74, 13);
            this.lblNewSchema.TabIndex = 4;
            this.lblNewSchema.Text = "New Schema:";
            // 
            // chkRunConvertedTableCreateStatements
            // 
            this.chkRunConvertedTableCreateStatements.AutoSize = true;
            this.chkRunConvertedTableCreateStatements.Location = new System.Drawing.Point(233, 18);
            this.chkRunConvertedTableCreateStatements.Name = "chkRunConvertedTableCreateStatements";
            this.chkRunConvertedTableCreateStatements.Size = new System.Drawing.Size(218, 17);
            this.chkRunConvertedTableCreateStatements.TabIndex = 3;
            this.chkRunConvertedTableCreateStatements.Text = "Run Converted Table Create Statements";
            this.chkRunConvertedTableCreateStatements.UseVisualStyleBackColor = true;
            // 
            // chkShowTableCreateStatements
            // 
            this.chkShowTableCreateStatements.AutoSize = true;
            this.chkShowTableCreateStatements.Location = new System.Drawing.Point(234, 12);
            this.chkShowTableCreateStatements.Name = "chkShowTableCreateStatements";
            this.chkShowTableCreateStatements.Size = new System.Drawing.Size(173, 17);
            this.chkShowTableCreateStatements.TabIndex = 2;
            this.chkShowTableCreateStatements.Text = "Show Table Create Statements";
            this.chkShowTableCreateStatements.UseVisualStyleBackColor = true;
            // 
            // chkConvertTableDefs
            // 
            this.chkConvertTableDefs.AutoSize = true;
            this.chkConvertTableDefs.Location = new System.Drawing.Point(16, 18);
            this.chkConvertTableDefs.Name = "chkConvertTableDefs";
            this.chkConvertTableDefs.Size = new System.Drawing.Size(127, 17);
            this.chkConvertTableDefs.TabIndex = 1;
            this.chkConvertTableDefs.Text = "&2 Convert Table Defs";
            this.chkConvertTableDefs.UseVisualStyleBackColor = true;
            // 
            // chkGetTabDefList
            // 
            this.chkGetTabDefList.AutoSize = true;
            this.chkGetTabDefList.Location = new System.Drawing.Point(17, 12);
            this.chkGetTabDefList.Name = "chkGetTabDefList";
            this.chkGetTabDefList.Size = new System.Drawing.Size(153, 17);
            this.chkGetTabDefList.TabIndex = 0;
            this.chkGetTabDefList.Text = "&1 Get Table Definitions List";
            this.chkGetTabDefList.UseVisualStyleBackColor = true;
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(66, 547);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 8;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogTableDefs\\TestprogTableDefs\\InitWi" +
    "nFormsHelpFile.chm";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Source DB Platform:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Connection String:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Destination DB Platform:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Connection String:";
            // 
            // cboSourceDbPlatform
            // 
            this.cboSourceDbPlatform.FormattingEnabled = true;
            this.cboSourceDbPlatform.Location = new System.Drawing.Point(171, 60);
            this.cboSourceDbPlatform.Name = "cboSourceDbPlatform";
            this.cboSourceDbPlatform.Size = new System.Drawing.Size(560, 21);
            this.cboSourceDbPlatform.TabIndex = 13;
            this.cboSourceDbPlatform.SelectedIndexChanged += new System.EventHandler(this.cboSourceDbPlatform_SelectedIndexChanged);
            // 
            // cboSourceDbConnectionString
            // 
            this.cboSourceDbConnectionString.FormattingEnabled = true;
            this.cboSourceDbConnectionString.Location = new System.Drawing.Point(171, 88);
            this.cboSourceDbConnectionString.Name = "cboSourceDbConnectionString";
            this.cboSourceDbConnectionString.Size = new System.Drawing.Size(560, 21);
            this.cboSourceDbConnectionString.TabIndex = 14;
            // 
            // cboDestinationDbPlatform
            // 
            this.cboDestinationDbPlatform.FormattingEnabled = true;
            this.cboDestinationDbPlatform.Location = new System.Drawing.Point(171, 123);
            this.cboDestinationDbPlatform.Name = "cboDestinationDbPlatform";
            this.cboDestinationDbPlatform.Size = new System.Drawing.Size(560, 21);
            this.cboDestinationDbPlatform.TabIndex = 15;
            this.cboDestinationDbPlatform.SelectedIndexChanged += new System.EventHandler(this.cboDestinationDbPlatform_SelectedIndexChanged);
            // 
            // cboDestinationDbConnectionString
            // 
            this.cboDestinationDbConnectionString.FormattingEnabled = true;
            this.cboDestinationDbConnectionString.Location = new System.Drawing.Point(171, 152);
            this.cboDestinationDbConnectionString.Name = "cboDestinationDbConnectionString";
            this.cboDestinationDbConnectionString.Size = new System.Drawing.Size(560, 21);
            this.cboDestinationDbConnectionString.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Include Patterns:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(410, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Exclude Patterns:";
            // 
            // txtIncludePatterns
            // 
            this.txtIncludePatterns.Location = new System.Drawing.Point(39, 208);
            this.txtIncludePatterns.Multiline = true;
            this.txtIncludePatterns.Name = "txtIncludePatterns";
            this.txtIncludePatterns.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtIncludePatterns.Size = new System.Drawing.Size(316, 77);
            this.txtIncludePatterns.TabIndex = 19;
            this.txtIncludePatterns.Enter += new System.EventHandler(this.txtIncludePatterns_Enter);
            this.txtIncludePatterns.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIncludePatterns_KeyDown);
            this.txtIncludePatterns.Leave += new System.EventHandler(this.txtIncludePatterns_Leave);
            // 
            // txtExcludePatterns
            // 
            this.txtExcludePatterns.Location = new System.Drawing.Point(413, 208);
            this.txtExcludePatterns.Multiline = true;
            this.txtExcludePatterns.Name = "txtExcludePatterns";
            this.txtExcludePatterns.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtExcludePatterns.Size = new System.Drawing.Size(319, 77);
            this.txtExcludePatterns.TabIndex = 20;
            this.txtExcludePatterns.Enter += new System.EventHandler(this.txtExcludePatterns_Enter);
            this.txtExcludePatterns.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExcludePatterns_KeyDown);
            this.txtExcludePatterns.Leave += new System.EventHandler(this.txtExcludePatterns_Leave);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.chkImportDataFromSourceToDestination);
            this.panel1.Controls.Add(this.chkConvertTableDefs);
            this.panel1.Controls.Add(this.txtNewSchema);
            this.panel1.Controls.Add(this.chkRunConvertedTableCreateStatements);
            this.panel1.Controls.Add(this.lblNewSchema);
            this.panel1.Location = new System.Drawing.Point(18, 71);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(675, 88);
            this.panel1.TabIndex = 21;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.chkShowTableCreateStatements);
            this.panel2.Controls.Add(this.chkGetTabDefList);
            this.panel2.Location = new System.Drawing.Point(18, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(675, 46);
            this.panel2.TabIndex = 22;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.chkGetSupportedDatabaseList);
            this.panel3.Location = new System.Drawing.Point(18, 165);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(675, 46);
            this.panel3.TabIndex = 21;
            // 
            // MainForm
            // 
            this.AcceptButton = this.cmdRunTests;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(909, 587);
            this.Controls.Add(this.txtExcludePatterns);
            this.Controls.Add(this.txtIncludePatterns);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboDestinationDbConnectionString);
            this.Controls.Add(this.cboDestinationDbPlatform);
            this.Controls.Add(this.cboSourceDbConnectionString);
            this.Controls.Add(this.cboSourceDbPlatform);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkEraseOutputBeforeEachTest);
            this.Controls.Add(this.grpTestsToRun);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.cmdRunTests);
            this.Controls.Add(this.cmdExit);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Form";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.grpTestsToRun.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
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
        private System.Windows.Forms.CheckBox chkGetTabDefList;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        private System.Windows.Forms.CheckBox chkConvertTableDefs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.ComboBox cboSourceDbPlatform;
        internal System.Windows.Forms.ComboBox cboSourceDbConnectionString;
        internal System.Windows.Forms.ComboBox cboDestinationDbPlatform;
        internal System.Windows.Forms.ComboBox cboDestinationDbConnectionString;
        internal System.Windows.Forms.TextBox txtIncludePatterns;
        internal System.Windows.Forms.TextBox txtExcludePatterns;
        internal System.Windows.Forms.CheckBox chkShowTableCreateStatements;
        internal System.Windows.Forms.CheckBox chkRunConvertedTableCreateStatements;
        internal System.Windows.Forms.TextBox txtNewSchema;
        private System.Windows.Forms.Label lblNewSchema;
        internal System.Windows.Forms.CheckBox chkImportDataFromSourceToDestination;
        private System.Windows.Forms.CheckBox chkGetSupportedDatabaseList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
    }
}

