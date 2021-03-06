﻿namespace TestprogConnectionObjects
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
            this.chkShowConnectionStringDefinitionsForm = new System.Windows.Forms.CheckBox();
            this.chkShowDatabaseSelector = new System.Windows.Forms.CheckBox();
            this.chkUpdateProviderObjects = new System.Windows.Forms.CheckBox();
            this.chkShowConnectionPromptForm = new System.Windows.Forms.CheckBox();
            this.chkCreateConnectionObjects = new System.Windows.Forms.CheckBox();
            this.chkCreateProviderObjects = new System.Windows.Forms.CheckBox();
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
            this.grpTestsToRun.Controls.Add(this.chkShowConnectionStringDefinitionsForm);
            this.grpTestsToRun.Controls.Add(this.chkShowDatabaseSelector);
            this.grpTestsToRun.Controls.Add(this.chkUpdateProviderObjects);
            this.grpTestsToRun.Controls.Add(this.chkShowConnectionPromptForm);
            this.grpTestsToRun.Controls.Add(this.chkCreateConnectionObjects);
            this.grpTestsToRun.Controls.Add(this.chkCreateProviderObjects);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 60);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 376);
            this.grpTestsToRun.TabIndex = 0;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // chkShowConnectionStringDefinitionsForm
            // 
            this.chkShowConnectionStringDefinitionsForm.AutoSize = true;
            this.chkShowConnectionStringDefinitionsForm.Location = new System.Drawing.Point(17, 128);
            this.chkShowConnectionStringDefinitionsForm.Name = "chkShowConnectionStringDefinitionsForm";
            this.chkShowConnectionStringDefinitionsForm.Size = new System.Drawing.Size(232, 17);
            this.chkShowConnectionStringDefinitionsForm.TabIndex = 12;
            this.chkShowConnectionStringDefinitionsForm.Text = "&5 Show Connection Strings Definitions Form";
            this.chkShowConnectionStringDefinitionsForm.UseVisualStyleBackColor = true;
            // 
            // chkShowDatabaseSelector
            // 
            this.chkShowDatabaseSelector.AutoSize = true;
            this.chkShowDatabaseSelector.Location = new System.Drawing.Point(17, 104);
            this.chkShowDatabaseSelector.Name = "chkShowDatabaseSelector";
            this.chkShowDatabaseSelector.Size = new System.Drawing.Size(153, 17);
            this.chkShowDatabaseSelector.TabIndex = 11;
            this.chkShowDatabaseSelector.Text = "&4 Show Database Selector";
            this.chkShowDatabaseSelector.UseVisualStyleBackColor = true;
            // 
            // chkUpdateProviderObjects
            // 
            this.chkUpdateProviderObjects.AutoSize = true;
            this.chkUpdateProviderObjects.Location = new System.Drawing.Point(17, 81);
            this.chkUpdateProviderObjects.Name = "chkUpdateProviderObjects";
            this.chkUpdateProviderObjects.Size = new System.Drawing.Size(151, 17);
            this.chkUpdateProviderObjects.TabIndex = 10;
            this.chkUpdateProviderObjects.Text = "&3 Update Provider Objects";
            this.chkUpdateProviderObjects.UseVisualStyleBackColor = true;
            // 
            // chkShowConnectionPromptForm
            // 
            this.chkShowConnectionPromptForm.AutoSize = true;
            this.chkShowConnectionPromptForm.Location = new System.Drawing.Point(17, 263);
            this.chkShowConnectionPromptForm.Name = "chkShowConnectionPromptForm";
            this.chkShowConnectionPromptForm.Size = new System.Drawing.Size(172, 17);
            this.chkShowConnectionPromptForm.TabIndex = 9;
            this.chkShowConnectionPromptForm.Text = "Show Connection Prompt Form";
            this.chkShowConnectionPromptForm.UseVisualStyleBackColor = true;
            // 
            // chkCreateConnectionObjects
            // 
            this.chkCreateConnectionObjects.AutoSize = true;
            this.chkCreateConnectionObjects.Location = new System.Drawing.Point(17, 57);
            this.chkCreateConnectionObjects.Name = "chkCreateConnectionObjects";
            this.chkCreateConnectionObjects.Size = new System.Drawing.Size(162, 17);
            this.chkCreateConnectionObjects.TabIndex = 1;
            this.chkCreateConnectionObjects.Text = "&2 Create Connection Objects";
            this.chkCreateConnectionObjects.UseVisualStyleBackColor = true;
            // 
            // chkCreateProviderObjects
            // 
            this.chkCreateProviderObjects.AutoSize = true;
            this.chkCreateProviderObjects.Location = new System.Drawing.Point(17, 33);
            this.chkCreateProviderObjects.Name = "chkCreateProviderObjects";
            this.chkCreateProviderObjects.Size = new System.Drawing.Size(147, 17);
            this.chkCreateProviderObjects.TabIndex = 0;
            this.chkCreateProviderObjects.Text = "&1 Create Provider Objects";
            this.chkCreateProviderObjects.UseVisualStyleBackColor = true;
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(39, 454);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(317, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 8;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each ShowConnectionStringForm is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogConnectionObjects\\InitWinFormsAppW" +
    "ithExtendedOptions\\InitWinFormsHelpFile.chm";
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
        private System.Windows.Forms.CheckBox chkCreateProviderObjects;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        private System.Windows.Forms.CheckBox chkCreateConnectionObjects;
        private System.Windows.Forms.CheckBox chkShowConnectionPromptForm;
        private System.Windows.Forms.CheckBox chkUpdateProviderObjects;
        private System.Windows.Forms.CheckBox chkShowDatabaseSelector;
        private System.Windows.Forms.CheckBox chkShowConnectionStringDefinitionsForm;
    }
}

