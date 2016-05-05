namespace TestprogTextFiles
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
            this.chkShowTextFileViewer = new System.Windows.Forms.CheckBox();
            this.chkShowFixedLengthDataPrompt = new System.Windows.Forms.CheckBox();
            this.chkImportTextData = new System.Windows.Forms.CheckBox();
            this.chkImportXmlDocument = new System.Windows.Forms.CheckBox();
            this.chkShowDataDelimiterPrompt = new System.Windows.Forms.CheckBox();
            this.chkAllowDataTruncation = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.optFixedLengthExtract = new System.Windows.Forms.RadioButton();
            this.optDelimitedExtract = new System.Windows.Forms.RadioButton();
            this.chkExtractFileTest = new System.Windows.Forms.CheckBox();
            this.chkFixedLengthCrLf = new System.Windows.Forms.CheckBox();
            this.txtFixedLengthNumRows = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkFixedLengthTextFileTests = new System.Windows.Forms.CheckBox();
            this.txtDelimitedNumRows = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkDelimitedLineTextFileTests = new System.Windows.Forms.CheckBox();
            this.chkDeleteAfterWrite = new System.Windows.Forms.CheckBox();
            this.chkAppend = new System.Windows.Forms.CheckBox();
            this.chkRunPFTextFileTests = new System.Windows.Forms.CheckBox();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.cmdQuickTest = new System.Windows.Forms.Button();
            this.cmdEraseResults = new System.Windows.Forms.Button();
            this.cmdShowHideOutputLog = new System.Windows.Forms.Button();
            this.MainMenu.SuspendLayout();
            this.grpTestsToRun.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(510, 449);
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
            this.grpTestsToRun.Controls.Add(this.chkShowTextFileViewer);
            this.grpTestsToRun.Controls.Add(this.chkShowFixedLengthDataPrompt);
            this.grpTestsToRun.Controls.Add(this.chkImportTextData);
            this.grpTestsToRun.Controls.Add(this.chkImportXmlDocument);
            this.grpTestsToRun.Controls.Add(this.chkShowDataDelimiterPrompt);
            this.grpTestsToRun.Controls.Add(this.chkAllowDataTruncation);
            this.grpTestsToRun.Controls.Add(this.panel1);
            this.grpTestsToRun.Controls.Add(this.chkExtractFileTest);
            this.grpTestsToRun.Controls.Add(this.chkFixedLengthCrLf);
            this.grpTestsToRun.Controls.Add(this.txtFixedLengthNumRows);
            this.grpTestsToRun.Controls.Add(this.label2);
            this.grpTestsToRun.Controls.Add(this.chkFixedLengthTextFileTests);
            this.grpTestsToRun.Controls.Add(this.txtDelimitedNumRows);
            this.grpTestsToRun.Controls.Add(this.label1);
            this.grpTestsToRun.Controls.Add(this.chkDelimitedLineTextFileTests);
            this.grpTestsToRun.Controls.Add(this.chkDeleteAfterWrite);
            this.grpTestsToRun.Controls.Add(this.chkAppend);
            this.grpTestsToRun.Controls.Add(this.chkRunPFTextFileTests);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 60);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 384);
            this.grpTestsToRun.TabIndex = 0;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // chkShowTextFileViewer
            // 
            this.chkShowTextFileViewer.AutoSize = true;
            this.chkShowTextFileViewer.Location = new System.Drawing.Point(16, 327);
            this.chkShowTextFileViewer.Name = "chkShowTextFileViewer";
            this.chkShowTextFileViewer.Size = new System.Drawing.Size(131, 17);
            this.chkShowTextFileViewer.TabIndex = 17;
            this.chkShowTextFileViewer.Text = "Show Text File Viewer";
            this.chkShowTextFileViewer.UseVisualStyleBackColor = true;
            // 
            // chkShowFixedLengthDataPrompt
            // 
            this.chkShowFixedLengthDataPrompt.AutoSize = true;
            this.chkShowFixedLengthDataPrompt.Location = new System.Drawing.Point(245, 226);
            this.chkShowFixedLengthDataPrompt.Name = "chkShowFixedLengthDataPrompt";
            this.chkShowFixedLengthDataPrompt.Size = new System.Drawing.Size(179, 17);
            this.chkShowFixedLengthDataPrompt.TabIndex = 16;
            this.chkShowFixedLengthDataPrompt.Text = "Show Fixed Length Data Prompt";
            this.chkShowFixedLengthDataPrompt.UseVisualStyleBackColor = true;
            // 
            // chkImportTextData
            // 
            this.chkImportTextData.AutoSize = true;
            this.chkImportTextData.Location = new System.Drawing.Point(16, 293);
            this.chkImportTextData.Name = "chkImportTextData";
            this.chkImportTextData.Size = new System.Drawing.Size(105, 17);
            this.chkImportTextData.TabIndex = 15;
            this.chkImportTextData.Text = "Import Text Data";
            this.chkImportTextData.UseVisualStyleBackColor = true;
            // 
            // chkImportXmlDocument
            // 
            this.chkImportXmlDocument.AutoSize = true;
            this.chkImportXmlDocument.Location = new System.Drawing.Point(16, 258);
            this.chkImportXmlDocument.Name = "chkImportXmlDocument";
            this.chkImportXmlDocument.Size = new System.Drawing.Size(132, 17);
            this.chkImportXmlDocument.TabIndex = 14;
            this.chkImportXmlDocument.Text = "Import XML Document";
            this.chkImportXmlDocument.UseVisualStyleBackColor = true;
            // 
            // chkShowDataDelimiterPrompt
            // 
            this.chkShowDataDelimiterPrompt.AutoSize = true;
            this.chkShowDataDelimiterPrompt.Location = new System.Drawing.Point(16, 225);
            this.chkShowDataDelimiterPrompt.Name = "chkShowDataDelimiterPrompt";
            this.chkShowDataDelimiterPrompt.Size = new System.Drawing.Size(158, 17);
            this.chkShowDataDelimiterPrompt.TabIndex = 13;
            this.chkShowDataDelimiterPrompt.Text = "Show Data Delimiter Prompt";
            this.chkShowDataDelimiterPrompt.UseVisualStyleBackColor = true;
            // 
            // chkAllowDataTruncation
            // 
            this.chkAllowDataTruncation.AutoSize = true;
            this.chkAllowDataTruncation.Location = new System.Drawing.Point(245, 149);
            this.chkAllowDataTruncation.Name = "chkAllowDataTruncation";
            this.chkAllowDataTruncation.Size = new System.Drawing.Size(131, 17);
            this.chkAllowDataTruncation.TabIndex = 12;
            this.chkAllowDataTruncation.Text = "Allow Data Truncation";
            this.chkAllowDataTruncation.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.optFixedLengthExtract);
            this.panel1.Controls.Add(this.optDelimitedExtract);
            this.panel1.Location = new System.Drawing.Point(143, 186);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(242, 22);
            this.panel1.TabIndex = 11;
            // 
            // optFixedLengthExtract
            // 
            this.optFixedLengthExtract.AutoSize = true;
            this.optFixedLengthExtract.Location = new System.Drawing.Point(135, 2);
            this.optFixedLengthExtract.Name = "optFixedLengthExtract";
            this.optFixedLengthExtract.Size = new System.Drawing.Size(86, 17);
            this.optFixedLengthExtract.TabIndex = 1;
            this.optFixedLengthExtract.TabStop = true;
            this.optFixedLengthExtract.Text = "Fixed Length";
            this.optFixedLengthExtract.UseVisualStyleBackColor = true;
            // 
            // optDelimitedExtract
            // 
            this.optDelimitedExtract.AutoSize = true;
            this.optDelimitedExtract.Location = new System.Drawing.Point(3, 2);
            this.optDelimitedExtract.Name = "optDelimitedExtract";
            this.optDelimitedExtract.Size = new System.Drawing.Size(68, 17);
            this.optDelimitedExtract.TabIndex = 0;
            this.optDelimitedExtract.TabStop = true;
            this.optDelimitedExtract.Text = "Delimited";
            this.optDelimitedExtract.UseVisualStyleBackColor = true;
            // 
            // chkExtractFileTest
            // 
            this.chkExtractFileTest.AutoSize = true;
            this.chkExtractFileTest.Location = new System.Drawing.Point(16, 189);
            this.chkExtractFileTest.Name = "chkExtractFileTest";
            this.chkExtractFileTest.Size = new System.Drawing.Size(102, 17);
            this.chkExtractFileTest.TabIndex = 10;
            this.chkExtractFileTest.Text = "Extract File Test";
            this.chkExtractFileTest.UseVisualStyleBackColor = true;
            // 
            // chkFixedLengthCrLf
            // 
            this.chkFixedLengthCrLf.AutoSize = true;
            this.chkFixedLengthCrLf.Location = new System.Drawing.Point(40, 149);
            this.chkFixedLengthCrLf.Name = "chkFixedLengthCrLf";
            this.chkFixedLengthCrLf.Size = new System.Drawing.Size(175, 17);
            this.chkFixedLengthCrLf.TabIndex = 9;
            this.chkFixedLengthCrLf.Text = "Insert CR/LF at end of data line";
            this.chkFixedLengthCrLf.UseVisualStyleBackColor = true;
            // 
            // txtFixedLengthNumRows
            // 
            this.txtFixedLengthNumRows.Location = new System.Drawing.Point(325, 126);
            this.txtFixedLengthNumRows.Name = "txtFixedLengthNumRows";
            this.txtFixedLengthNumRows.Size = new System.Drawing.Size(60, 20);
            this.txtFixedLengthNumRows.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "# of rows to output";
            // 
            // chkFixedLengthTextFileTests
            // 
            this.chkFixedLengthTextFileTests.AutoSize = true;
            this.chkFixedLengthTextFileTests.Location = new System.Drawing.Point(16, 126);
            this.chkFixedLengthTextFileTests.Name = "chkFixedLengthTextFileTests";
            this.chkFixedLengthTextFileTests.Size = new System.Drawing.Size(159, 17);
            this.chkFixedLengthTextFileTests.TabIndex = 6;
            this.chkFixedLengthTextFileTests.Text = "Fixed Length Text File Tests";
            this.chkFixedLengthTextFileTests.UseVisualStyleBackColor = true;
            // 
            // txtDelimitedNumRows
            // 
            this.txtDelimitedNumRows.Location = new System.Drawing.Point(325, 92);
            this.txtDelimitedNumRows.Name = "txtDelimitedNumRows";
            this.txtDelimitedNumRows.Size = new System.Drawing.Size(60, 20);
            this.txtDelimitedNumRows.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(221, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "# of rows to output:";
            // 
            // chkDelimitedLineTextFileTests
            // 
            this.chkDelimitedLineTextFileTests.AutoSize = true;
            this.chkDelimitedLineTextFileTests.Location = new System.Drawing.Point(16, 92);
            this.chkDelimitedLineTextFileTests.Name = "chkDelimitedLineTextFileTests";
            this.chkDelimitedLineTextFileTests.Size = new System.Drawing.Size(164, 17);
            this.chkDelimitedLineTextFileTests.TabIndex = 3;
            this.chkDelimitedLineTextFileTests.Text = "Delimited Line Text File Tests";
            this.chkDelimitedLineTextFileTests.UseVisualStyleBackColor = true;
            // 
            // chkDeleteAfterWrite
            // 
            this.chkDeleteAfterWrite.AutoSize = true;
            this.chkDeleteAfterWrite.Location = new System.Drawing.Point(269, 50);
            this.chkDeleteAfterWrite.Name = "chkDeleteAfterWrite";
            this.chkDeleteAfterWrite.Size = new System.Drawing.Size(116, 17);
            this.chkDeleteAfterWrite.TabIndex = 2;
            this.chkDeleteAfterWrite.Text = "Delete After Write?";
            this.chkDeleteAfterWrite.UseVisualStyleBackColor = true;
            // 
            // chkAppend
            // 
            this.chkAppend.AutoSize = true;
            this.chkAppend.Location = new System.Drawing.Point(171, 50);
            this.chkAppend.Name = "chkAppend";
            this.chkAppend.Size = new System.Drawing.Size(69, 17);
            this.chkAppend.TabIndex = 1;
            this.chkAppend.Text = "Append?";
            this.chkAppend.UseVisualStyleBackColor = true;
            // 
            // chkRunPFTextFileTests
            // 
            this.chkRunPFTextFileTests.AutoSize = true;
            this.chkRunPFTextFileTests.Location = new System.Drawing.Point(16, 50);
            this.chkRunPFTextFileTests.Name = "chkRunPFTextFileTests";
            this.chkRunPFTextFileTests.Size = new System.Drawing.Size(114, 17);
            this.chkRunPFTextFileTests.TabIndex = 0;
            this.chkRunPFTextFileTests.Text = "&1 PFTextFile Tests";
            this.chkRunPFTextFileTests.UseVisualStyleBackColor = true;
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(55, 460);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 9;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogTextFiles\\TestprogTextFiles\\InitWi" +
    "nFormsHelpFile.chm";
            // 
            // cmdQuickTest
            // 
            this.cmdQuickTest.Location = new System.Drawing.Point(510, 143);
            this.cmdQuickTest.Name = "cmdQuickTest";
            this.cmdQuickTest.Size = new System.Drawing.Size(93, 37);
            this.cmdQuickTest.TabIndex = 4;
            this.cmdQuickTest.Text = "Quick &Test";
            this.cmdQuickTest.UseVisualStyleBackColor = true;
            this.cmdQuickTest.Click += new System.EventHandler(this.cmdQuickTest_Click);
            // 
            // cmdEraseResults
            // 
            this.cmdEraseResults.Location = new System.Drawing.Point(510, 209);
            this.cmdEraseResults.Name = "cmdEraseResults";
            this.cmdEraseResults.Size = new System.Drawing.Size(93, 36);
            this.cmdEraseResults.TabIndex = 5;
            this.cmdEraseResults.Text = "&Erase Results";
            this.cmdEraseResults.UseVisualStyleBackColor = true;
            this.cmdEraseResults.Click += new System.EventHandler(this.cmdEraseResults_Click);
            // 
            // cmdShowHideOutputLog
            // 
            this.cmdShowHideOutputLog.Location = new System.Drawing.Point(510, 318);
            this.cmdShowHideOutputLog.Name = "cmdShowHideOutputLog";
            this.cmdShowHideOutputLog.Size = new System.Drawing.Size(93, 44);
            this.cmdShowHideOutputLog.TabIndex = 18;
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
            this.ClientSize = new System.Drawing.Size(638, 544);
            this.Controls.Add(this.cmdShowHideOutputLog);
            this.Controls.Add(this.cmdEraseResults);
            this.Controls.Add(this.cmdQuickTest);
            this.Controls.Add(this.grpTestsToRun);
            this.Controls.Add(this.chkEraseOutputBeforeEachTest);
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.CheckBox chkRunPFTextFileTests;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.CheckBox chkAppend;
        private System.Windows.Forms.CheckBox chkDeleteAfterWrite;
        private System.Windows.Forms.Button cmdQuickTest;
        private System.Windows.Forms.CheckBox chkDelimitedLineTextFileTests;
        private System.Windows.Forms.TextBox txtDelimitedNumRows;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFixedLengthNumRows;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkFixedLengthTextFileTests;
        internal System.Windows.Forms.CheckBox chkFixedLengthCrLf;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.RadioButton optFixedLengthExtract;
        internal System.Windows.Forms.RadioButton optDelimitedExtract;
        private System.Windows.Forms.CheckBox chkExtractFileTest;
        private System.Windows.Forms.Button cmdEraseResults;
        internal System.Windows.Forms.CheckBox chkAllowDataTruncation;
        private System.Windows.Forms.CheckBox chkShowDataDelimiterPrompt;
        private System.Windows.Forms.CheckBox chkImportXmlDocument;
        internal System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        private System.Windows.Forms.CheckBox chkImportTextData;
        private System.Windows.Forms.CheckBox chkShowFixedLengthDataPrompt;
        private System.Windows.Forms.CheckBox chkShowTextFileViewer;
        private System.Windows.Forms.Button cmdShowHideOutputLog;
    }
}

