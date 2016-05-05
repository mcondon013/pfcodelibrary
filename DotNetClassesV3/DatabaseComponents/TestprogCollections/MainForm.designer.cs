namespace TestprogCollections
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
            this.grpTestsToRun = new System.Windows.Forms.GroupBox();
            this.chkGenericSortedKeyValueListTest = new System.Windows.Forms.CheckBox();
            this.chkKeyValueListMergeTest = new System.Windows.Forms.CheckBox();
            this.chkListMergeTest = new System.Windows.Forms.CheckBox();
            this.chkToXmlTest = new System.Windows.Forms.CheckBox();
            this.chkGenericKeyValueListTest = new System.Windows.Forms.CheckBox();
            this.chkGenericListTest = new System.Windows.Forms.CheckBox();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.chkSortedKeyValueListMergeTest = new System.Windows.Forms.CheckBox();
            this.chkRandomizeListsTest = new System.Windows.Forms.CheckBox();
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
            this.mnuFile,
            this.mnuTools,
            this.mnuHelp});
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
            // grpTestsToRun
            // 
            this.grpTestsToRun.Controls.Add(this.chkRandomizeListsTest);
            this.grpTestsToRun.Controls.Add(this.chkSortedKeyValueListMergeTest);
            this.grpTestsToRun.Controls.Add(this.chkGenericSortedKeyValueListTest);
            this.grpTestsToRun.Controls.Add(this.chkKeyValueListMergeTest);
            this.grpTestsToRun.Controls.Add(this.chkListMergeTest);
            this.grpTestsToRun.Controls.Add(this.chkToXmlTest);
            this.grpTestsToRun.Controls.Add(this.chkGenericKeyValueListTest);
            this.grpTestsToRun.Controls.Add(this.chkGenericListTest);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 60);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 376);
            this.grpTestsToRun.TabIndex = 0;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // chkGenericSortedKeyValueListTest
            // 
            this.chkGenericSortedKeyValueListTest.AutoSize = true;
            this.chkGenericSortedKeyValueListTest.Location = new System.Drawing.Point(16, 221);
            this.chkGenericSortedKeyValueListTest.Name = "chkGenericSortedKeyValueListTest";
            this.chkGenericSortedKeyValueListTest.Size = new System.Drawing.Size(200, 17);
            this.chkGenericSortedKeyValueListTest.TabIndex = 6;
            this.chkGenericSortedKeyValueListTest.Text = "&6 Generic Sorted Key Value List Test";
            this.chkGenericSortedKeyValueListTest.UseVisualStyleBackColor = true;
            // 
            // chkKeyValueListMergeTest
            // 
            this.chkKeyValueListMergeTest.AutoSize = true;
            this.chkKeyValueListMergeTest.Location = new System.Drawing.Point(16, 184);
            this.chkKeyValueListMergeTest.Name = "chkKeyValueListMergeTest";
            this.chkKeyValueListMergeTest.Size = new System.Drawing.Size(161, 17);
            this.chkKeyValueListMergeTest.TabIndex = 5;
            this.chkKeyValueListMergeTest.Text = "&5 Key/Value List Merge Test";
            this.chkKeyValueListMergeTest.UseVisualStyleBackColor = true;
            // 
            // chkListMergeTest
            // 
            this.chkListMergeTest.AutoSize = true;
            this.chkListMergeTest.Location = new System.Drawing.Point(16, 147);
            this.chkListMergeTest.Name = "chkListMergeTest";
            this.chkListMergeTest.Size = new System.Drawing.Size(108, 17);
            this.chkListMergeTest.TabIndex = 4;
            this.chkListMergeTest.Text = "&4 List Merge Test";
            this.chkListMergeTest.UseVisualStyleBackColor = true;
            // 
            // chkToXmlTest
            // 
            this.chkToXmlTest.AutoSize = true;
            this.chkToXmlTest.Location = new System.Drawing.Point(16, 110);
            this.chkToXmlTest.Name = "chkToXmlTest";
            this.chkToXmlTest.Size = new System.Drawing.Size(89, 17);
            this.chkToXmlTest.TabIndex = 3;
            this.chkToXmlTest.Text = "&3 ToXml Test";
            this.chkToXmlTest.UseVisualStyleBackColor = true;
            // 
            // chkGenericKeyValueListTest
            // 
            this.chkGenericKeyValueListTest.AutoSize = true;
            this.chkGenericKeyValueListTest.Location = new System.Drawing.Point(16, 77);
            this.chkGenericKeyValueListTest.Name = "chkGenericKeyValueListTest";
            this.chkGenericKeyValueListTest.Size = new System.Drawing.Size(163, 17);
            this.chkGenericKeyValueListTest.TabIndex = 2;
            this.chkGenericKeyValueListTest.Text = "&2 Generic KeyValue List Test";
            this.chkGenericKeyValueListTest.UseVisualStyleBackColor = true;
            // 
            // chkGenericListTest
            // 
            this.chkGenericListTest.AutoSize = true;
            this.chkGenericListTest.Location = new System.Drawing.Point(16, 42);
            this.chkGenericListTest.Name = "chkGenericListTest";
            this.chkGenericListTest.Size = new System.Drawing.Size(115, 17);
            this.chkGenericListTest.TabIndex = 1;
            this.chkGenericListTest.Text = "&1 Generic List Test";
            this.chkGenericListTest.UseVisualStyleBackColor = true;
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(39, 454);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 8;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogCollections\\TestprogCollections\\In" +
    "itWinFormsHelpFile.chm";
            // 
            // chkSortedKeyValueListMergeTest
            // 
            this.chkSortedKeyValueListMergeTest.AutoSize = true;
            this.chkSortedKeyValueListMergeTest.Location = new System.Drawing.Point(16, 258);
            this.chkSortedKeyValueListMergeTest.Name = "chkSortedKeyValueListMergeTest";
            this.chkSortedKeyValueListMergeTest.Size = new System.Drawing.Size(193, 17);
            this.chkSortedKeyValueListMergeTest.TabIndex = 7;
            this.chkSortedKeyValueListMergeTest.Text = "&7 Sorted Key Value List Merge Test";
            this.chkSortedKeyValueListMergeTest.UseVisualStyleBackColor = true;
            // 
            // chkRandomizeListsTest
            // 
            this.chkRandomizeListsTest.AutoSize = true;
            this.chkRandomizeListsTest.Location = new System.Drawing.Point(16, 296);
            this.chkRandomizeListsTest.Name = "chkRandomizeListsTest";
            this.chkRandomizeListsTest.Size = new System.Drawing.Size(136, 17);
            this.chkRandomizeListsTest.TabIndex = 8;
            this.chkRandomizeListsTest.Text = "&8 Randomize Lists Test";
            this.chkRandomizeListsTest.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AcceptButton = this.cmdRunTests;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(638, 500);
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
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.GroupBox grpTestsToRun;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpContents;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpIndex;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpTutorial;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpContact;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        private System.Windows.Forms.CheckBox chkGenericListTest;
        private System.Windows.Forms.CheckBox chkGenericKeyValueListTest;
        private System.Windows.Forms.CheckBox chkToXmlTest;
        private System.Windows.Forms.CheckBox chkKeyValueListMergeTest;
        private System.Windows.Forms.CheckBox chkListMergeTest;
        private System.Windows.Forms.CheckBox chkGenericSortedKeyValueListTest;
        private System.Windows.Forms.CheckBox chkSortedKeyValueListMergeTest;
        private System.Windows.Forms.CheckBox chkRandomizeListsTest;
    }
}

