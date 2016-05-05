namespace TestprogAppUtils
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
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.grpTestsToRun = new System.Windows.Forms.GroupBox();
            this.chkShowTreeViewFolderBrowserExt = new System.Windows.Forms.CheckBox();
            this.txtRootFolderPath = new System.Windows.Forms.TextBox();
            this.chkShowTreeViewFolderBrowser = new System.Windows.Forms.CheckBox();
            this.chkShowNamelistPrompt = new System.Windows.Forms.CheckBox();
            this.chkRemoveAllMruItems = new System.Windows.Forms.CheckBox();
            this.chkNewFolderButton = new System.Windows.Forms.CheckBox();
            this.chkShowFolderBrowserDialog = new System.Windows.Forms.CheckBox();
            this.chkOverwritePrompt = new System.Windows.Forms.CheckBox();
            this.chkCreatePrompt = new System.Windows.Forms.CheckBox();
            this.chkShowSaveFileDialog = new System.Windows.Forms.CheckBox();
            this.txtFilterIndex = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInitialDirectory = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkMultiSelect = new System.Windows.Forms.CheckBox();
            this.chkShowOpenFileDialog = new System.Windows.Forms.CheckBox();
            this.mnuMainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileToggleRecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.chkPFClassWriterTest = new System.Windows.Forms.CheckBox();
            this.grpTestsToRun.SuspendLayout();
            this.mnuMainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Location = new System.Drawing.Point(506, 409);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(93, 37);
            this.cmdExit.TabIndex = 0;
            this.cmdExit.Text = "E&xit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdRunTests
            // 
            this.cmdRunTests.Location = new System.Drawing.Point(506, 68);
            this.cmdRunTests.Name = "cmdRunTests";
            this.cmdRunTests.Size = new System.Drawing.Size(93, 37);
            this.cmdRunTests.TabIndex = 1;
            this.cmdRunTests.Text = "&Run Tests";
            this.cmdRunTests.UseVisualStyleBackColor = true;
            this.cmdRunTests.Click += new System.EventHandler(this.cmdRunTest_Click);
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(38, 429);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 10;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // grpTestsToRun
            // 
            this.grpTestsToRun.Controls.Add(this.chkPFClassWriterTest);
            this.grpTestsToRun.Controls.Add(this.chkShowTreeViewFolderBrowserExt);
            this.grpTestsToRun.Controls.Add(this.txtRootFolderPath);
            this.grpTestsToRun.Controls.Add(this.chkShowTreeViewFolderBrowser);
            this.grpTestsToRun.Controls.Add(this.chkShowNamelistPrompt);
            this.grpTestsToRun.Controls.Add(this.chkRemoveAllMruItems);
            this.grpTestsToRun.Controls.Add(this.chkNewFolderButton);
            this.grpTestsToRun.Controls.Add(this.chkShowFolderBrowserDialog);
            this.grpTestsToRun.Controls.Add(this.chkOverwritePrompt);
            this.grpTestsToRun.Controls.Add(this.chkCreatePrompt);
            this.grpTestsToRun.Controls.Add(this.chkShowSaveFileDialog);
            this.grpTestsToRun.Controls.Add(this.txtFilterIndex);
            this.grpTestsToRun.Controls.Add(this.label3);
            this.grpTestsToRun.Controls.Add(this.txtFilter);
            this.grpTestsToRun.Controls.Add(this.label2);
            this.grpTestsToRun.Controls.Add(this.txtInitialDirectory);
            this.grpTestsToRun.Controls.Add(this.label1);
            this.grpTestsToRun.Controls.Add(this.chkMultiSelect);
            this.grpTestsToRun.Controls.Add(this.chkShowOpenFileDialog);
            this.grpTestsToRun.Location = new System.Drawing.Point(38, 56);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 376);
            this.grpTestsToRun.TabIndex = 9;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // chkShowTreeViewFolderBrowserExt
            // 
            this.chkShowTreeViewFolderBrowserExt.AutoSize = true;
            this.chkShowTreeViewFolderBrowserExt.Location = new System.Drawing.Point(17, 291);
            this.chkShowTreeViewFolderBrowserExt.Name = "chkShowTreeViewFolderBrowserExt";
            this.chkShowTreeViewFolderBrowserExt.Size = new System.Drawing.Size(192, 17);
            this.chkShowTreeViewFolderBrowserExt.TabIndex = 17;
            this.chkShowTreeViewFolderBrowserExt.Text = "&7 Show TreeViewFolderBrowserExt";
            this.chkShowTreeViewFolderBrowserExt.UseVisualStyleBackColor = true;
            // 
            // txtRootFolderPath
            // 
            this.txtRootFolderPath.Location = new System.Drawing.Point(202, 257);
            this.txtRootFolderPath.Name = "txtRootFolderPath";
            this.txtRootFolderPath.Size = new System.Drawing.Size(210, 20);
            this.txtRootFolderPath.TabIndex = 16;
            // 
            // chkShowTreeViewFolderBrowser
            // 
            this.chkShowTreeViewFolderBrowser.AutoSize = true;
            this.chkShowTreeViewFolderBrowser.Location = new System.Drawing.Point(17, 260);
            this.chkShowTreeViewFolderBrowser.Name = "chkShowTreeViewFolderBrowser";
            this.chkShowTreeViewFolderBrowser.Size = new System.Drawing.Size(171, 17);
            this.chkShowTreeViewFolderBrowser.TabIndex = 15;
            this.chkShowTreeViewFolderBrowser.Text = "&6 TreeView Folder Browser For";
            this.chkShowTreeViewFolderBrowser.UseVisualStyleBackColor = true;
            // 
            // chkShowNamelistPrompt
            // 
            this.chkShowNamelistPrompt.AutoSize = true;
            this.chkShowNamelistPrompt.Location = new System.Drawing.Point(17, 226);
            this.chkShowNamelistPrompt.Name = "chkShowNamelistPrompt";
            this.chkShowNamelistPrompt.Size = new System.Drawing.Size(141, 17);
            this.chkShowNamelistPrompt.TabIndex = 14;
            this.chkShowNamelistPrompt.Text = "&5 Show Namelist Prompt";
            this.chkShowNamelistPrompt.UseVisualStyleBackColor = true;
            // 
            // chkRemoveAllMruItems
            // 
            this.chkRemoveAllMruItems.AutoSize = true;
            this.chkRemoveAllMruItems.Location = new System.Drawing.Point(17, 192);
            this.chkRemoveAllMruItems.Name = "chkRemoveAllMruItems";
            this.chkRemoveAllMruItems.Size = new System.Drawing.Size(137, 17);
            this.chkRemoveAllMruItems.TabIndex = 13;
            this.chkRemoveAllMruItems.Text = "&4 Remove all Mru Items";
            this.chkRemoveAllMruItems.UseVisualStyleBackColor = true;
            // 
            // chkNewFolderButton
            // 
            this.chkNewFolderButton.AutoSize = true;
            this.chkNewFolderButton.Location = new System.Drawing.Point(181, 92);
            this.chkNewFolderButton.Name = "chkNewFolderButton";
            this.chkNewFolderButton.Size = new System.Drawing.Size(114, 17);
            this.chkNewFolderButton.TabIndex = 12;
            this.chkNewFolderButton.Text = "New Folder Button";
            this.chkNewFolderButton.UseVisualStyleBackColor = true;
            // 
            // chkShowFolderBrowserDialog
            // 
            this.chkShowFolderBrowserDialog.AutoSize = true;
            this.chkShowFolderBrowserDialog.Location = new System.Drawing.Point(17, 92);
            this.chkShowFolderBrowserDialog.Name = "chkShowFolderBrowserDialog";
            this.chkShowFolderBrowserDialog.Size = new System.Drawing.Size(162, 17);
            this.chkShowFolderBrowserDialog.TabIndex = 11;
            this.chkShowFolderBrowserDialog.Text = "&3 Show FolderBrowserDialog";
            this.chkShowFolderBrowserDialog.UseVisualStyleBackColor = true;
            // 
            // chkOverwritePrompt
            // 
            this.chkOverwritePrompt.AutoSize = true;
            this.chkOverwritePrompt.Location = new System.Drawing.Point(305, 62);
            this.chkOverwritePrompt.Name = "chkOverwritePrompt";
            this.chkOverwritePrompt.Size = new System.Drawing.Size(107, 17);
            this.chkOverwritePrompt.TabIndex = 10;
            this.chkOverwritePrompt.Text = "Overwrite Prompt";
            this.chkOverwritePrompt.UseVisualStyleBackColor = true;
            // 
            // chkCreatePrompt
            // 
            this.chkCreatePrompt.AutoSize = true;
            this.chkCreatePrompt.Location = new System.Drawing.Point(181, 62);
            this.chkCreatePrompt.Name = "chkCreatePrompt";
            this.chkCreatePrompt.Size = new System.Drawing.Size(93, 17);
            this.chkCreatePrompt.TabIndex = 9;
            this.chkCreatePrompt.Text = "Create Prompt";
            this.chkCreatePrompt.UseVisualStyleBackColor = true;
            // 
            // chkShowSaveFileDialog
            // 
            this.chkShowSaveFileDialog.AutoSize = true;
            this.chkShowSaveFileDialog.Location = new System.Drawing.Point(17, 62);
            this.chkShowSaveFileDialog.Name = "chkShowSaveFileDialog";
            this.chkShowSaveFileDialog.Size = new System.Drawing.Size(136, 17);
            this.chkShowSaveFileDialog.TabIndex = 8;
            this.chkShowSaveFileDialog.Text = "&2 Show SaveFileDialog";
            this.chkShowSaveFileDialog.UseVisualStyleBackColor = true;
            // 
            // txtFilterIndex
            // 
            this.txtFilterIndex.Location = new System.Drawing.Point(369, 155);
            this.txtFilterIndex.Name = "txtFilterIndex";
            this.txtFilterIndex.Size = new System.Drawing.Size(43, 20);
            this.txtFilterIndex.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(302, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Filter Index:";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(88, 155);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(208, 20);
            this.txtFilter.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Filter:";
            // 
            // txtInitialDirectory
            // 
            this.txtInitialDirectory.Location = new System.Drawing.Point(136, 123);
            this.txtInitialDirectory.Name = "txtInitialDirectory";
            this.txtInitialDirectory.Size = new System.Drawing.Size(276, 20);
            this.txtInitialDirectory.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Initial Directory:";
            // 
            // chkMultiSelect
            // 
            this.chkMultiSelect.AutoSize = true;
            this.chkMultiSelect.Location = new System.Drawing.Point(181, 33);
            this.chkMultiSelect.Name = "chkMultiSelect";
            this.chkMultiSelect.Size = new System.Drawing.Size(81, 17);
            this.chkMultiSelect.TabIndex = 1;
            this.chkMultiSelect.Text = "Multi-Select";
            this.chkMultiSelect.UseVisualStyleBackColor = true;
            // 
            // chkShowOpenFileDialog
            // 
            this.chkShowOpenFileDialog.AutoSize = true;
            this.chkShowOpenFileDialog.Location = new System.Drawing.Point(17, 33);
            this.chkShowOpenFileDialog.Name = "chkShowOpenFileDialog";
            this.chkShowOpenFileDialog.Size = new System.Drawing.Size(137, 17);
            this.chkShowOpenFileDialog.TabIndex = 0;
            this.chkShowOpenFileDialog.Text = "&1 Show OpenFileDialog";
            this.chkShowOpenFileDialog.UseVisualStyleBackColor = true;
            // 
            // mnuMainMenu
            // 
            this.mnuMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
            this.mnuMainMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMainMenu.Name = "mnuMainMenu";
            this.mnuMainMenu.Size = new System.Drawing.Size(644, 24);
            this.mnuMainMenu.TabIndex = 12;
            this.mnuMainMenu.Text = "MainMenu";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileRecent,
            this.toolStripSeparator1,
            this.mnuFileToggleRecentFiles,
            this.toolStripSeparator2,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileRecent
            // 
            this.mnuFileRecent.Name = "mnuFileRecent";
            this.mnuFileRecent.Size = new System.Drawing.Size(176, 22);
            this.mnuFileRecent.Text = "Recent Files";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(173, 6);
            // 
            // mnuFileToggleRecentFiles
            // 
            this.mnuFileToggleRecentFiles.Name = "mnuFileToggleRecentFiles";
            this.mnuFileToggleRecentFiles.Size = new System.Drawing.Size(176, 22);
            this.mnuFileToggleRecentFiles.Text = "Toggle Recent Files";
            this.mnuFileToggleRecentFiles.Click += new System.EventHandler(this.mnuFileToggleRecentFiles_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(173, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(176, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // chkPFClassWriterTest
            // 
            this.chkPFClassWriterTest.AutoSize = true;
            this.chkPFClassWriterTest.Location = new System.Drawing.Point(17, 325);
            this.chkPFClassWriterTest.Name = "chkPFClassWriterTest";
            this.chkPFClassWriterTest.Size = new System.Drawing.Size(132, 17);
            this.chkPFClassWriterTest.TabIndex = 18;
            this.chkPFClassWriterTest.Text = "&8  PFCLassWriter Test";
            this.chkPFClassWriterTest.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AcceptButton = this.cmdRunTests;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(644, 500);
            this.Controls.Add(this.chkEraseOutputBeforeEachTest);
            this.Controls.Add(this.grpTestsToRun);
            this.Controls.Add(this.cmdRunTests);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.mnuMainMenu);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.grpTestsToRun.ResumeLayout(false);
            this.grpTestsToRun.PerformLayout();
            this.mnuMainMenu.ResumeLayout(false);
            this.mnuMainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdRunTests;
        private System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        private System.Windows.Forms.GroupBox grpTestsToRun;
        private System.Windows.Forms.CheckBox chkShowOpenFileDialog;
        private System.Windows.Forms.CheckBox chkMultiSelect;
        private System.Windows.Forms.TextBox txtInitialDirectory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFilterIndex;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip mnuMainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileRecent;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.CheckBox chkShowSaveFileDialog;
        private System.Windows.Forms.CheckBox chkOverwritePrompt;
        private System.Windows.Forms.CheckBox chkCreatePrompt;
        private System.Windows.Forms.CheckBox chkNewFolderButton;
        private System.Windows.Forms.CheckBox chkShowFolderBrowserDialog;
        private System.Windows.Forms.CheckBox chkRemoveAllMruItems;
        private System.Windows.Forms.ToolStripMenuItem mnuFileToggleRecentFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.CheckBox chkShowNamelistPrompt;
        private System.Windows.Forms.CheckBox chkShowTreeViewFolderBrowser;
        internal System.Windows.Forms.TextBox txtRootFolderPath;
        private System.Windows.Forms.CheckBox chkShowTreeViewFolderBrowserExt;
        private System.Windows.Forms.CheckBox chkPFClassWriterTest;
    }
}

