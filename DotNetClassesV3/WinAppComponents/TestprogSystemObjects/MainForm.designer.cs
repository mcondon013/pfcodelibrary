namespace TestprogSystemObjects
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
            this.chkRegistryTest = new System.Windows.Forms.CheckBox();
            this.chkPrinterInfoTest = new System.Windows.Forms.CheckBox();
            this.chkWriteToEventLogTest = new System.Windows.Forms.CheckBox();
            this.chkDynamicLoadTests = new System.Windows.Forms.CheckBox();
            this.chkWinAppConsoleTest = new System.Windows.Forms.CheckBox();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
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
            this.grpTestsToRun.Controls.Add(this.chkRegistryTest);
            this.grpTestsToRun.Controls.Add(this.chkPrinterInfoTest);
            this.grpTestsToRun.Controls.Add(this.chkWriteToEventLogTest);
            this.grpTestsToRun.Controls.Add(this.chkDynamicLoadTests);
            this.grpTestsToRun.Controls.Add(this.chkWinAppConsoleTest);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 60);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 376);
            this.grpTestsToRun.TabIndex = 0;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // chkRegistryTest
            // 
            this.chkRegistryTest.AutoSize = true;
            this.chkRegistryTest.Location = new System.Drawing.Point(17, 176);
            this.chkRegistryTest.Name = "chkRegistryTest";
            this.chkRegistryTest.Size = new System.Drawing.Size(97, 17);
            this.chkRegistryTest.TabIndex = 4;
            this.chkRegistryTest.Text = "&5 Registry Test";
            this.chkRegistryTest.UseVisualStyleBackColor = true;
            // 
            // chkPrinterInfoTest
            // 
            this.chkPrinterInfoTest.AutoSize = true;
            this.chkPrinterInfoTest.Location = new System.Drawing.Point(17, 139);
            this.chkPrinterInfoTest.Name = "chkPrinterInfoTest";
            this.chkPrinterInfoTest.Size = new System.Drawing.Size(110, 17);
            this.chkPrinterInfoTest.TabIndex = 3;
            this.chkPrinterInfoTest.Text = "&4 Printer Info Test";
            this.chkPrinterInfoTest.UseVisualStyleBackColor = true;
            // 
            // chkWriteToEventLogTest
            // 
            this.chkWriteToEventLogTest.AutoSize = true;
            this.chkWriteToEventLogTest.Location = new System.Drawing.Point(17, 103);
            this.chkWriteToEventLogTest.Name = "chkWriteToEventLogTest";
            this.chkWriteToEventLogTest.Size = new System.Drawing.Size(152, 17);
            this.chkWriteToEventLogTest.TabIndex = 2;
            this.chkWriteToEventLogTest.Text = "&3 Write To Event Log Test";
            this.chkWriteToEventLogTest.UseVisualStyleBackColor = true;
            // 
            // chkDynamicLoadTests
            // 
            this.chkDynamicLoadTests.AutoSize = true;
            this.chkDynamicLoadTests.Location = new System.Drawing.Point(17, 68);
            this.chkDynamicLoadTests.Name = "chkDynamicLoadTests";
            this.chkDynamicLoadTests.Size = new System.Drawing.Size(132, 17);
            this.chkDynamicLoadTests.TabIndex = 1;
            this.chkDynamicLoadTests.Text = "&2 Dynamic Load Tests";
            this.chkDynamicLoadTests.UseVisualStyleBackColor = true;
            // 
            // chkWinAppConsoleTest
            // 
            this.chkWinAppConsoleTest.AutoSize = true;
            this.chkWinAppConsoleTest.Location = new System.Drawing.Point(17, 33);
            this.chkWinAppConsoleTest.Name = "chkWinAppConsoleTest";
            this.chkWinAppConsoleTest.Size = new System.Drawing.Size(135, 17);
            this.chkWinAppConsoleTest.TabIndex = 0;
            this.chkWinAppConsoleTest.Text = "&1 WinAppConsole Test";
            this.chkWinAppConsoleTest.UseVisualStyleBackColor = true;
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
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogSystemObjects\\TestprogSystemObject" +
    "s\\InitWinFormsHelpFile.chm";
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
        private System.Windows.Forms.CheckBox chkWinAppConsoleTest;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        private System.Windows.Forms.CheckBox chkDynamicLoadTests;
        private System.Windows.Forms.CheckBox chkWriteToEventLogTest;
        private System.Windows.Forms.CheckBox chkPrinterInfoTest;
        private System.Windows.Forms.CheckBox chkRegistryTest;
    }
}

