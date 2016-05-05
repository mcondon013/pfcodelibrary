namespace TestprogListObjects
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
            this.chkKeyValueListMergeTest = new System.Windows.Forms.CheckBox();
            this.chkListMergeTest = new System.Windows.Forms.CheckBox();
            this.chkGenericSortedKeyValueListTest = new System.Windows.Forms.CheckBox();
            this.chkGenericKeyValueListTest = new System.Windows.Forms.CheckBox();
            this.chkGenericListTest = new System.Windows.Forms.CheckBox();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.MainMenu.SuspendLayout();
            this.grpTestsToRun.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
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
            this.cmdRunTests.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.grpTestsToRun.Controls.Add(this.chkKeyValueListMergeTest);
            this.grpTestsToRun.Controls.Add(this.chkListMergeTest);
            this.grpTestsToRun.Controls.Add(this.chkGenericSortedKeyValueListTest);
            this.grpTestsToRun.Controls.Add(this.chkGenericKeyValueListTest);
            this.grpTestsToRun.Controls.Add(this.chkGenericListTest);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 60);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 376);
            this.grpTestsToRun.TabIndex = 0;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // chkKeyValueListMergeTest
            // 
            this.chkKeyValueListMergeTest.AutoSize = true;
            this.chkKeyValueListMergeTest.Location = new System.Drawing.Point(22, 183);
            this.chkKeyValueListMergeTest.Name = "chkKeyValueListMergeTest";
            this.chkKeyValueListMergeTest.Size = new System.Drawing.Size(161, 17);
            this.chkKeyValueListMergeTest.TabIndex = 7;
            this.chkKeyValueListMergeTest.Text = "&5 Key/Value List Merge Test";
            this.chkKeyValueListMergeTest.UseVisualStyleBackColor = true;
            // 
            // chkListMergeTest
            // 
            this.chkListMergeTest.AutoSize = true;
            this.chkListMergeTest.Location = new System.Drawing.Point(22, 146);
            this.chkListMergeTest.Name = "chkListMergeTest";
            this.chkListMergeTest.Size = new System.Drawing.Size(108, 17);
            this.chkListMergeTest.TabIndex = 6;
            this.chkListMergeTest.Text = "&4 List Merge Test";
            this.chkListMergeTest.UseVisualStyleBackColor = true;
            // 
            // chkGenericSortedKeyValueListTest
            // 
            this.chkGenericSortedKeyValueListTest.AutoSize = true;
            this.chkGenericSortedKeyValueListTest.Location = new System.Drawing.Point(22, 109);
            this.chkGenericSortedKeyValueListTest.Name = "chkGenericSortedKeyValueListTest";
            this.chkGenericSortedKeyValueListTest.Size = new System.Drawing.Size(200, 17);
            this.chkGenericSortedKeyValueListTest.TabIndex = 5;
            this.chkGenericSortedKeyValueListTest.Text = "&3 Generic Sorted Key Value List Test";
            this.chkGenericSortedKeyValueListTest.UseVisualStyleBackColor = true;
            // 
            // chkGenericKeyValueListTest
            // 
            this.chkGenericKeyValueListTest.AutoSize = true;
            this.chkGenericKeyValueListTest.Location = new System.Drawing.Point(22, 72);
            this.chkGenericKeyValueListTest.Name = "chkGenericKeyValueListTest";
            this.chkGenericKeyValueListTest.Size = new System.Drawing.Size(163, 17);
            this.chkGenericKeyValueListTest.TabIndex = 4;
            this.chkGenericKeyValueListTest.Text = "&2 Generic KeyValue List Test";
            this.chkGenericKeyValueListTest.UseVisualStyleBackColor = true;
            // 
            // chkGenericListTest
            // 
            this.chkGenericListTest.AutoSize = true;
            this.chkGenericListTest.Location = new System.Drawing.Point(22, 37);
            this.chkGenericListTest.Name = "chkGenericListTest";
            this.chkGenericListTest.Size = new System.Drawing.Size(115, 17);
            this.chkGenericListTest.TabIndex = 3;
            this.chkGenericListTest.Text = "&1 Generic List Test";
            this.chkGenericListTest.UseVisualStyleBackColor = true;
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogListObjects\\InitWinFormsAppWithExt" +
    "endedOptions\\InitWinFormsHelpFile.chm";
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
        private System.Windows.Forms.GroupBox grpTestsToRun;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        private System.Windows.Forms.CheckBox chkGenericKeyValueListTest;
        private System.Windows.Forms.CheckBox chkGenericListTest;
        private System.Windows.Forms.CheckBox chkGenericSortedKeyValueListTest;
        private System.Windows.Forms.CheckBox chkKeyValueListMergeTest;
        private System.Windows.Forms.CheckBox chkListMergeTest;
    }
}

