namespace TestprogRandomWords
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
            this.txtDocumentSubject = new System.Windows.Forms.TextBox();
            this.lblDocumentSubject = new System.Windows.Forms.Label();
            this.cboWordType = new System.Windows.Forms.ComboBox();
            this.txtBookTitle = new System.Windows.Forms.TextBox();
            this.lblBookTitle = new System.Windows.Forms.Label();
            this.lblEachHeading = new System.Windows.Forms.Label();
            this.txtChapterHeadings = new System.Windows.Forms.TextBox();
            this.lblChapterHeadings = new System.Windows.Forms.Label();
            this.txtNumChapters = new System.Windows.Forms.TextBox();
            this.lblNumChapters = new System.Windows.Forms.Label();
            this.txtMaxNumParagraphs = new System.Windows.Forms.TextBox();
            this.lblMaxNumParagraphs = new System.Windows.Forms.Label();
            this.txtMinNumParagraphs = new System.Windows.Forms.TextBox();
            this.lblMinNumParagraphs = new System.Windows.Forms.Label();
            this.txtNumWords = new System.Windows.Forms.TextBox();
            this.lblNumWords = new System.Windows.Forms.Label();
            this.grpDocumentType = new System.Windows.Forms.GroupBox();
            this.optBook = new System.Windows.Forms.RadioButton();
            this.optChapters = new System.Windows.Forms.RadioButton();
            this.optDocument = new System.Windows.Forms.RadioButton();
            this.optParagraphs = new System.Windows.Forms.RadioButton();
            this.chkRandomDocumentTest = new System.Windows.Forms.CheckBox();
            this.txtMaxNumSentences = new System.Windows.Forms.TextBox();
            this.lblMaxNumSentences = new System.Windows.Forms.Label();
            this.txtMinNumSentences = new System.Windows.Forms.TextBox();
            this.lblMinNumSentences = new System.Windows.Forms.Label();
            this.chkRandomSentencesTest = new System.Windows.Forms.CheckBox();
            this.chkRandomWordsTest = new System.Windows.Forms.CheckBox();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.cmdShowHideOutputLog = new System.Windows.Forms.Button();
            this.MainMenu.SuspendLayout();
            this.grpTestsToRun.SuspendLayout();
            this.grpDocumentType.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(510, 466);
            this.cmdExit.Name = "cmdExit";
            this.appHelpProvider.SetShowHelp(this.cmdExit, true);
            this.cmdExit.Size = new System.Drawing.Size(93, 37);
            this.cmdExit.TabIndex = 4;
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
            this.cmdRunTests.TabIndex = 3;
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
            this.MainMenu.TabIndex = 0;
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
            this.grpTestsToRun.Controls.Add(this.txtDocumentSubject);
            this.grpTestsToRun.Controls.Add(this.lblDocumentSubject);
            this.grpTestsToRun.Controls.Add(this.cboWordType);
            this.grpTestsToRun.Controls.Add(this.txtBookTitle);
            this.grpTestsToRun.Controls.Add(this.lblBookTitle);
            this.grpTestsToRun.Controls.Add(this.lblEachHeading);
            this.grpTestsToRun.Controls.Add(this.txtChapterHeadings);
            this.grpTestsToRun.Controls.Add(this.lblChapterHeadings);
            this.grpTestsToRun.Controls.Add(this.txtNumChapters);
            this.grpTestsToRun.Controls.Add(this.lblNumChapters);
            this.grpTestsToRun.Controls.Add(this.txtMaxNumParagraphs);
            this.grpTestsToRun.Controls.Add(this.lblMaxNumParagraphs);
            this.grpTestsToRun.Controls.Add(this.txtMinNumParagraphs);
            this.grpTestsToRun.Controls.Add(this.lblMinNumParagraphs);
            this.grpTestsToRun.Controls.Add(this.txtNumWords);
            this.grpTestsToRun.Controls.Add(this.lblNumWords);
            this.grpTestsToRun.Controls.Add(this.grpDocumentType);
            this.grpTestsToRun.Controls.Add(this.chkRandomDocumentTest);
            this.grpTestsToRun.Controls.Add(this.txtMaxNumSentences);
            this.grpTestsToRun.Controls.Add(this.lblMaxNumSentences);
            this.grpTestsToRun.Controls.Add(this.txtMinNumSentences);
            this.grpTestsToRun.Controls.Add(this.lblMinNumSentences);
            this.grpTestsToRun.Controls.Add(this.chkRandomSentencesTest);
            this.grpTestsToRun.Controls.Add(this.chkRandomWordsTest);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 60);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 443);
            this.grpTestsToRun.TabIndex = 1;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // txtDocumentSubject
            // 
            this.txtDocumentSubject.Location = new System.Drawing.Point(220, 217);
            this.txtDocumentSubject.Name = "txtDocumentSubject";
            this.txtDocumentSubject.Size = new System.Drawing.Size(180, 20);
            this.txtDocumentSubject.TabIndex = 23;
            // 
            // lblDocumentSubject
            // 
            this.lblDocumentSubject.AutoSize = true;
            this.lblDocumentSubject.Location = new System.Drawing.Point(214, 199);
            this.lblDocumentSubject.Name = "lblDocumentSubject";
            this.lblDocumentSubject.Size = new System.Drawing.Size(95, 13);
            this.lblDocumentSubject.TabIndex = 22;
            this.lblDocumentSubject.Text = "Document Subject";
            // 
            // cboWordType
            // 
            this.cboWordType.FormattingEnabled = true;
            this.cboWordType.Location = new System.Drawing.Point(39, 57);
            this.cboWordType.Name = "cboWordType";
            this.cboWordType.Size = new System.Drawing.Size(121, 21);
            this.cboWordType.TabIndex = 21;
            // 
            // txtBookTitle
            // 
            this.txtBookTitle.Location = new System.Drawing.Point(220, 258);
            this.txtBookTitle.Name = "txtBookTitle";
            this.txtBookTitle.Size = new System.Drawing.Size(180, 20);
            this.txtBookTitle.TabIndex = 20;
            // 
            // lblBookTitle
            // 
            this.lblBookTitle.AutoSize = true;
            this.lblBookTitle.Location = new System.Drawing.Point(214, 240);
            this.lblBookTitle.Name = "lblBookTitle";
            this.lblBookTitle.Size = new System.Drawing.Size(55, 13);
            this.lblBookTitle.TabIndex = 19;
            this.lblBookTitle.Text = "Book Title";
            // 
            // lblEachHeading
            // 
            this.lblEachHeading.AutoSize = true;
            this.lblEachHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEachHeading.Location = new System.Drawing.Point(227, 304);
            this.lblEachHeading.Name = "lblEachHeading";
            this.lblEachHeading.Size = new System.Drawing.Size(159, 13);
            this.lblEachHeading.TabIndex = 18;
            this.lblEachHeading.Text = "Each Heading on Separate Line";
            // 
            // txtChapterHeadings
            // 
            this.txtChapterHeadings.Location = new System.Drawing.Point(220, 320);
            this.txtChapterHeadings.Multiline = true;
            this.txtChapterHeadings.Name = "txtChapterHeadings";
            this.txtChapterHeadings.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtChapterHeadings.Size = new System.Drawing.Size(180, 84);
            this.txtChapterHeadings.TabIndex = 17;
            // 
            // lblChapterHeadings
            // 
            this.lblChapterHeadings.AutoSize = true;
            this.lblChapterHeadings.Location = new System.Drawing.Point(214, 287);
            this.lblChapterHeadings.Name = "lblChapterHeadings";
            this.lblChapterHeadings.Size = new System.Drawing.Size(92, 13);
            this.lblChapterHeadings.TabIndex = 16;
            this.lblChapterHeadings.Text = "Chapter Headings";
            // 
            // txtNumChapters
            // 
            this.txtNumChapters.Location = new System.Drawing.Point(339, 169);
            this.txtNumChapters.Name = "txtNumChapters";
            this.txtNumChapters.Size = new System.Drawing.Size(61, 20);
            this.txtNumChapters.TabIndex = 15;
            // 
            // lblNumChapters
            // 
            this.lblNumChapters.AutoSize = true;
            this.lblNumChapters.Location = new System.Drawing.Point(214, 169);
            this.lblNumChapters.Name = "lblNumChapters";
            this.lblNumChapters.Size = new System.Drawing.Size(74, 13);
            this.lblNumChapters.TabIndex = 14;
            this.lblNumChapters.Text = "Num Chapters";
            // 
            // txtMaxNumParagraphs
            // 
            this.txtMaxNumParagraphs.Location = new System.Drawing.Point(339, 143);
            this.txtMaxNumParagraphs.Name = "txtMaxNumParagraphs";
            this.txtMaxNumParagraphs.Size = new System.Drawing.Size(61, 20);
            this.txtMaxNumParagraphs.TabIndex = 13;
            // 
            // lblMaxNumParagraphs
            // 
            this.lblMaxNumParagraphs.AutoSize = true;
            this.lblMaxNumParagraphs.Location = new System.Drawing.Point(214, 143);
            this.lblMaxNumParagraphs.Name = "lblMaxNumParagraphs";
            this.lblMaxNumParagraphs.Size = new System.Drawing.Size(109, 13);
            this.lblMaxNumParagraphs.TabIndex = 12;
            this.lblMaxNumParagraphs.Text = "Max Num Paragraphs";
            // 
            // txtMinNumParagraphs
            // 
            this.txtMinNumParagraphs.Location = new System.Drawing.Point(339, 119);
            this.txtMinNumParagraphs.Name = "txtMinNumParagraphs";
            this.txtMinNumParagraphs.Size = new System.Drawing.Size(61, 20);
            this.txtMinNumParagraphs.TabIndex = 11;
            // 
            // lblMinNumParagraphs
            // 
            this.lblMinNumParagraphs.AutoSize = true;
            this.lblMinNumParagraphs.Location = new System.Drawing.Point(214, 119);
            this.lblMinNumParagraphs.Name = "lblMinNumParagraphs";
            this.lblMinNumParagraphs.Size = new System.Drawing.Size(106, 13);
            this.lblMinNumParagraphs.TabIndex = 10;
            this.lblMinNumParagraphs.Text = "Min Num Paragraphs";
            // 
            // txtNumWords
            // 
            this.txtNumWords.Location = new System.Drawing.Point(339, 33);
            this.txtNumWords.Name = "txtNumWords";
            this.txtNumWords.Size = new System.Drawing.Size(61, 20);
            this.txtNumWords.TabIndex = 9;
            // 
            // lblNumWords
            // 
            this.lblNumWords.AutoSize = true;
            this.lblNumWords.Location = new System.Drawing.Point(214, 33);
            this.lblNumWords.Name = "lblNumWords";
            this.lblNumWords.Size = new System.Drawing.Size(63, 13);
            this.lblNumWords.TabIndex = 8;
            this.lblNumWords.Text = "Num Words";
            // 
            // grpDocumentType
            // 
            this.grpDocumentType.Controls.Add(this.optBook);
            this.grpDocumentType.Controls.Add(this.optChapters);
            this.grpDocumentType.Controls.Add(this.optDocument);
            this.grpDocumentType.Controls.Add(this.optParagraphs);
            this.grpDocumentType.Location = new System.Drawing.Point(39, 170);
            this.grpDocumentType.Name = "grpDocumentType";
            this.grpDocumentType.Size = new System.Drawing.Size(122, 125);
            this.grpDocumentType.TabIndex = 7;
            this.grpDocumentType.TabStop = false;
            this.grpDocumentType.Text = "DocumentType";
            // 
            // optBook
            // 
            this.optBook.AutoSize = true;
            this.optBook.Location = new System.Drawing.Point(24, 92);
            this.optBook.Name = "optBook";
            this.optBook.Size = new System.Drawing.Size(50, 17);
            this.optBook.TabIndex = 3;
            this.optBook.TabStop = true;
            this.optBook.Text = "Book";
            this.optBook.UseVisualStyleBackColor = true;
            // 
            // optChapters
            // 
            this.optChapters.AutoSize = true;
            this.optChapters.Location = new System.Drawing.Point(24, 68);
            this.optChapters.Name = "optChapters";
            this.optChapters.Size = new System.Drawing.Size(67, 17);
            this.optChapters.TabIndex = 2;
            this.optChapters.TabStop = true;
            this.optChapters.Text = "Chapters";
            this.optChapters.UseVisualStyleBackColor = true;
            // 
            // optDocument
            // 
            this.optDocument.AutoSize = true;
            this.optDocument.Location = new System.Drawing.Point(24, 44);
            this.optDocument.Name = "optDocument";
            this.optDocument.Size = new System.Drawing.Size(74, 17);
            this.optDocument.TabIndex = 1;
            this.optDocument.TabStop = true;
            this.optDocument.Text = "Document";
            this.optDocument.UseVisualStyleBackColor = true;
            // 
            // optParagraphs
            // 
            this.optParagraphs.AutoSize = true;
            this.optParagraphs.Location = new System.Drawing.Point(24, 20);
            this.optParagraphs.Name = "optParagraphs";
            this.optParagraphs.Size = new System.Drawing.Size(79, 17);
            this.optParagraphs.TabIndex = 0;
            this.optParagraphs.TabStop = true;
            this.optParagraphs.Text = "Paragraphs";
            this.optParagraphs.UseVisualStyleBackColor = true;
            // 
            // chkRandomDocumentTest
            // 
            this.chkRandomDocumentTest.AutoSize = true;
            this.chkRandomDocumentTest.Location = new System.Drawing.Point(17, 146);
            this.chkRandomDocumentTest.Name = "chkRandomDocumentTest";
            this.chkRandomDocumentTest.Size = new System.Drawing.Size(142, 17);
            this.chkRandomDocumentTest.TabIndex = 6;
            this.chkRandomDocumentTest.Text = "Random Document Test";
            this.chkRandomDocumentTest.UseVisualStyleBackColor = true;
            // 
            // txtMaxNumSentences
            // 
            this.txtMaxNumSentences.Location = new System.Drawing.Point(339, 89);
            this.txtMaxNumSentences.Name = "txtMaxNumSentences";
            this.txtMaxNumSentences.Size = new System.Drawing.Size(61, 20);
            this.txtMaxNumSentences.TabIndex = 5;
            // 
            // lblMaxNumSentences
            // 
            this.lblMaxNumSentences.AutoSize = true;
            this.lblMaxNumSentences.Location = new System.Drawing.Point(214, 89);
            this.lblMaxNumSentences.Name = "lblMaxNumSentences";
            this.lblMaxNumSentences.Size = new System.Drawing.Size(106, 13);
            this.lblMaxNumSentences.TabIndex = 4;
            this.lblMaxNumSentences.Text = "Max Num Sentences";
            // 
            // txtMinNumSentences
            // 
            this.txtMinNumSentences.Location = new System.Drawing.Point(339, 66);
            this.txtMinNumSentences.Name = "txtMinNumSentences";
            this.txtMinNumSentences.Size = new System.Drawing.Size(61, 20);
            this.txtMinNumSentences.TabIndex = 3;
            // 
            // lblMinNumSentences
            // 
            this.lblMinNumSentences.AutoSize = true;
            this.lblMinNumSentences.Location = new System.Drawing.Point(214, 66);
            this.lblMinNumSentences.Name = "lblMinNumSentences";
            this.lblMinNumSentences.Size = new System.Drawing.Size(103, 13);
            this.lblMinNumSentences.TabIndex = 2;
            this.lblMinNumSentences.Text = "Min Num Sentences";
            // 
            // chkRandomSentencesTest
            // 
            this.chkRandomSentencesTest.AutoSize = true;
            this.chkRandomSentencesTest.Location = new System.Drawing.Point(17, 103);
            this.chkRandomSentencesTest.Name = "chkRandomSentencesTest";
            this.chkRandomSentencesTest.Size = new System.Drawing.Size(144, 17);
            this.chkRandomSentencesTest.TabIndex = 1;
            this.chkRandomSentencesTest.Text = "Random Sentences Test";
            this.chkRandomSentencesTest.UseVisualStyleBackColor = true;
            // 
            // chkRandomWordsTest
            // 
            this.chkRandomWordsTest.AutoSize = true;
            this.chkRandomWordsTest.Location = new System.Drawing.Point(17, 33);
            this.chkRandomWordsTest.Name = "chkRandomWordsTest";
            this.chkRandomWordsTest.Size = new System.Drawing.Size(124, 17);
            this.chkRandomWordsTest.TabIndex = 0;
            this.chkRandomWordsTest.Text = "Random Words Test";
            this.chkRandomWordsTest.UseVisualStyleBackColor = true;
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(39, 521);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 2;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogRandomWords\\InitWinFormsAppWithUse" +
    "rAndAppSettings\\InitWinFormsHelpFile.chm";
            // 
            // cmdShowHideOutputLog
            // 
            this.cmdShowHideOutputLog.Location = new System.Drawing.Point(510, 243);
            this.cmdShowHideOutputLog.Name = "cmdShowHideOutputLog";
            this.cmdShowHideOutputLog.Size = new System.Drawing.Size(93, 44);
            this.cmdShowHideOutputLog.TabIndex = 11;
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
            this.ClientSize = new System.Drawing.Size(638, 567);
            this.Controls.Add(this.cmdShowHideOutputLog);
            this.Controls.Add(this.chkEraseOutputBeforeEachTest);
            this.Controls.Add(this.grpTestsToRun);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.cmdRunTests);
            this.Controls.Add(this.cmdExit);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.grpTestsToRun.ResumeLayout(false);
            this.grpTestsToRun.PerformLayout();
            this.grpDocumentType.ResumeLayout(false);
            this.grpDocumentType.PerformLayout();
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
        private System.Windows.Forms.CheckBox chkRandomWordsTest;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        private System.Windows.Forms.TextBox txtMaxNumSentences;
        private System.Windows.Forms.Label lblMaxNumSentences;
        private System.Windows.Forms.TextBox txtMinNumSentences;
        private System.Windows.Forms.Label lblMinNumSentences;
        private System.Windows.Forms.CheckBox chkRandomSentencesTest;
        private System.Windows.Forms.CheckBox chkRandomDocumentTest;
        private System.Windows.Forms.TextBox txtBookTitle;
        private System.Windows.Forms.Label lblBookTitle;
        private System.Windows.Forms.Label lblEachHeading;
        private System.Windows.Forms.TextBox txtChapterHeadings;
        private System.Windows.Forms.Label lblChapterHeadings;
        private System.Windows.Forms.TextBox txtNumChapters;
        private System.Windows.Forms.Label lblNumChapters;
        private System.Windows.Forms.TextBox txtMaxNumParagraphs;
        private System.Windows.Forms.Label lblMaxNumParagraphs;
        private System.Windows.Forms.TextBox txtMinNumParagraphs;
        private System.Windows.Forms.Label lblMinNumParagraphs;
        private System.Windows.Forms.TextBox txtNumWords;
        private System.Windows.Forms.Label lblNumWords;
        private System.Windows.Forms.GroupBox grpDocumentType;
        private System.Windows.Forms.RadioButton optBook;
        private System.Windows.Forms.RadioButton optChapters;
        private System.Windows.Forms.RadioButton optDocument;
        private System.Windows.Forms.RadioButton optParagraphs;
        private System.Windows.Forms.ComboBox cboWordType;
        private System.Windows.Forms.TextBox txtDocumentSubject;
        private System.Windows.Forms.Label lblDocumentSubject;
        private System.Windows.Forms.Button cmdShowHideOutputLog;
    }
}

