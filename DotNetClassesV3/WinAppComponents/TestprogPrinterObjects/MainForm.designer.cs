namespace TestprogPrinterObjects
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
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.cmdShowHideOutputLog = new System.Windows.Forms.Button();
            this.chkPFReportPrinterTest = new System.Windows.Forms.CheckBox();
            this.chkPFTextPrinterTest = new System.Windows.Forms.CheckBox();
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
            this.grpTestsToRun.Controls.Add(this.chkPFReportPrinterTest);
            this.grpTestsToRun.Controls.Add(this.chkPFTextPrinterTest);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 60);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 376);
            this.grpTestsToRun.TabIndex = 0;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
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
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogPrinterObjects\\TestprogPrinterObje" +
    "cts\\InitWinFormsHelpFile.chm";
            // 
            // cmdShowHideOutputLog
            // 
            this.cmdShowHideOutputLog.Location = new System.Drawing.Point(510, 225);
            this.cmdShowHideOutputLog.Name = "cmdShowHideOutputLog";
            this.cmdShowHideOutputLog.Size = new System.Drawing.Size(93, 44);
            this.cmdShowHideOutputLog.TabIndex = 10;
            this.cmdShowHideOutputLog.Text = "Show/Hide\r\nOutput Log";
            this.cmdShowHideOutputLog.UseVisualStyleBackColor = true;
            this.cmdShowHideOutputLog.Click += new System.EventHandler(this.cmdShowHideOutputLog_Click);
            // 
            // chkPFReportPrinterTest
            // 
            this.chkPFReportPrinterTest.AutoSize = true;
            this.chkPFReportPrinterTest.Location = new System.Drawing.Point(34, 75);
            this.chkPFReportPrinterTest.Name = "chkPFReportPrinterTest";
            this.chkPFReportPrinterTest.Size = new System.Drawing.Size(125, 17);
            this.chkPFReportPrinterTest.TabIndex = 41;
            this.chkPFReportPrinterTest.Text = "PFReportPrinter Test";
            this.chkPFReportPrinterTest.UseVisualStyleBackColor = true;
            // 
            // chkPFTextPrinterTest
            // 
            this.chkPFTextPrinterTest.AutoSize = true;
            this.chkPFTextPrinterTest.Location = new System.Drawing.Point(34, 39);
            this.chkPFTextPrinterTest.Name = "chkPFTextPrinterTest";
            this.chkPFTextPrinterTest.Size = new System.Drawing.Size(114, 17);
            this.chkPFTextPrinterTest.TabIndex = 40;
            this.chkPFTextPrinterTest.Text = "PFTextPrinter Test";
            this.chkPFTextPrinterTest.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AcceptButton = this.cmdRunTests;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(638, 500);
            this.Controls.Add(this.cmdShowHideOutputLog);
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
        private System.Windows.Forms.Button cmdShowHideOutputLog;
        private System.Windows.Forms.CheckBox chkPFReportPrinterTest;
        private System.Windows.Forms.CheckBox chkPFTextPrinterTest;
    }
}

