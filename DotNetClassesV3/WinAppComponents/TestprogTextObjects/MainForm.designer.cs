namespace TestprogTextObjects
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
            this.chkFormatTimeSpanTest = new System.Windows.Forms.CheckBox();
            this.chkQuotedValues = new System.Windows.Forms.CheckBox();
            this.txtDelimiters = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboKeyType = new System.Windows.Forms.ComboBox();
            this.txtCommandLineToParse = new System.Windows.Forms.TextBox();
            this.chkRunCommandLineTest = new System.Windows.Forms.CheckBox();
            this.chkSearchPatternTest = new System.Windows.Forms.CheckBox();
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
            this.grpTestsToRun.Controls.Add(this.chkSearchPatternTest);
            this.grpTestsToRun.Controls.Add(this.chkFormatTimeSpanTest);
            this.grpTestsToRun.Controls.Add(this.chkQuotedValues);
            this.grpTestsToRun.Controls.Add(this.txtDelimiters);
            this.grpTestsToRun.Controls.Add(this.label2);
            this.grpTestsToRun.Controls.Add(this.label1);
            this.grpTestsToRun.Controls.Add(this.cboKeyType);
            this.grpTestsToRun.Controls.Add(this.txtCommandLineToParse);
            this.grpTestsToRun.Controls.Add(this.chkRunCommandLineTest);
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
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogTextObjects\\TestprogTextObjects\\In" +
    "itWinFormsHelpFile.chm";
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
            // chkFormatTimeSpanTest
            // 
            this.chkFormatTimeSpanTest.AutoSize = true;
            this.chkFormatTimeSpanTest.Location = new System.Drawing.Point(16, 39);
            this.chkFormatTimeSpanTest.Name = "chkFormatTimeSpanTest";
            this.chkFormatTimeSpanTest.Size = new System.Drawing.Size(133, 17);
            this.chkFormatTimeSpanTest.TabIndex = 38;
            this.chkFormatTimeSpanTest.Text = "Format TimeSpan Test";
            this.chkFormatTimeSpanTest.UseVisualStyleBackColor = true;
            // 
            // chkQuotedValues
            // 
            this.chkQuotedValues.AutoSize = true;
            this.chkQuotedValues.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkQuotedValues.Location = new System.Drawing.Point(313, 74);
            this.chkQuotedValues.Name = "chkQuotedValues";
            this.chkQuotedValues.Size = new System.Drawing.Size(93, 17);
            this.chkQuotedValues.TabIndex = 37;
            this.chkQuotedValues.Text = "QuotedValues";
            this.chkQuotedValues.UseVisualStyleBackColor = true;
            // 
            // txtDelimiters
            // 
            this.txtDelimiters.Location = new System.Drawing.Point(295, 124);
            this.txtDelimiters.Name = "txtDelimiters";
            this.txtDelimiters.Size = new System.Drawing.Size(111, 20);
            this.txtDelimiters.TabIndex = 36;
            this.txtDelimiters.Text = "-/";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Delimiters:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Key Type:";
            // 
            // cboKeyType
            // 
            this.cboKeyType.FormattingEnabled = true;
            this.cboKeyType.Items.AddRange(new object[] {
            "NoKey",
            "CharKey",
            "NamedKey"});
            this.cboKeyType.Location = new System.Drawing.Point(100, 123);
            this.cboKeyType.Name = "cboKeyType";
            this.cboKeyType.Size = new System.Drawing.Size(95, 21);
            this.cboKeyType.TabIndex = 33;
            // 
            // txtCommandLineToParse
            // 
            this.txtCommandLineToParse.Location = new System.Drawing.Point(33, 97);
            this.txtCommandLineToParse.Name = "txtCommandLineToParse";
            this.txtCommandLineToParse.Size = new System.Drawing.Size(373, 20);
            this.txtCommandLineToParse.TabIndex = 32;
            // 
            // chkRunCommandLineTest
            // 
            this.chkRunCommandLineTest.AutoSize = true;
            this.chkRunCommandLineTest.Location = new System.Drawing.Point(15, 74);
            this.chkRunCommandLineTest.Name = "chkRunCommandLineTest";
            this.chkRunCommandLineTest.Size = new System.Drawing.Size(150, 17);
            this.chkRunCommandLineTest.TabIndex = 31;
            this.chkRunCommandLineTest.Text = "Command Line Parse Test";
            this.chkRunCommandLineTest.UseVisualStyleBackColor = true;
            // 
            // chkSearchPatternTest
            // 
            this.chkSearchPatternTest.AutoSize = true;
            this.chkSearchPatternTest.Location = new System.Drawing.Point(15, 165);
            this.chkSearchPatternTest.Name = "chkSearchPatternTest";
            this.chkSearchPatternTest.Size = new System.Drawing.Size(121, 17);
            this.chkSearchPatternTest.TabIndex = 39;
            this.chkSearchPatternTest.Text = "Search Pattern Test";
            this.chkSearchPatternTest.UseVisualStyleBackColor = true;
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
        private System.Windows.Forms.CheckBox chkFormatTimeSpanTest;
        internal System.Windows.Forms.CheckBox chkQuotedValues;
        internal System.Windows.Forms.TextBox txtDelimiters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ComboBox cboKeyType;
        internal System.Windows.Forms.TextBox txtCommandLineToParse;
        private System.Windows.Forms.CheckBox chkRunCommandLineTest;
        private System.Windows.Forms.CheckBox chkSearchPatternTest;
    }
}

