namespace TestprogDocumentObjects
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
            this.chkOutputToPDFDocument = new System.Windows.Forms.CheckBox();
            this.chkOutputToRTFDocument = new System.Windows.Forms.CheckBox();
            this.chkOutputToWordDocument = new System.Windows.Forms.CheckBox();
            this.chkExportDataFromExcelDocument = new System.Windows.Forms.CheckBox();
            this.chkOutputToExcelDocument = new System.Windows.Forms.CheckBox();
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
            this.grpTestsToRun.Controls.Add(this.chkOutputToPDFDocument);
            this.grpTestsToRun.Controls.Add(this.chkOutputToRTFDocument);
            this.grpTestsToRun.Controls.Add(this.chkOutputToWordDocument);
            this.grpTestsToRun.Controls.Add(this.chkExportDataFromExcelDocument);
            this.grpTestsToRun.Controls.Add(this.chkOutputToExcelDocument);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 60);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 376);
            this.grpTestsToRun.TabIndex = 1;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // chkOutputToPDFDocument
            // 
            this.chkOutputToPDFDocument.AutoSize = true;
            this.chkOutputToPDFDocument.Location = new System.Drawing.Point(17, 167);
            this.chkOutputToPDFDocument.Name = "chkOutputToPDFDocument";
            this.chkOutputToPDFDocument.Size = new System.Drawing.Size(146, 17);
            this.chkOutputToPDFDocument.TabIndex = 6;
            this.chkOutputToPDFDocument.Text = "Output to PDF Document";
            this.chkOutputToPDFDocument.UseVisualStyleBackColor = true;
            // 
            // chkOutputToRTFDocument
            // 
            this.chkOutputToRTFDocument.AutoSize = true;
            this.chkOutputToRTFDocument.Location = new System.Drawing.Point(17, 133);
            this.chkOutputToRTFDocument.Name = "chkOutputToRTFDocument";
            this.chkOutputToRTFDocument.Size = new System.Drawing.Size(146, 17);
            this.chkOutputToRTFDocument.TabIndex = 5;
            this.chkOutputToRTFDocument.Text = "Output to RTF Document";
            this.chkOutputToRTFDocument.UseVisualStyleBackColor = true;
            // 
            // chkOutputToWordDocument
            // 
            this.chkOutputToWordDocument.AutoSize = true;
            this.chkOutputToWordDocument.Location = new System.Drawing.Point(17, 100);
            this.chkOutputToWordDocument.Name = "chkOutputToWordDocument";
            this.chkOutputToWordDocument.Size = new System.Drawing.Size(155, 17);
            this.chkOutputToWordDocument.TabIndex = 4;
            this.chkOutputToWordDocument.Text = "Output To Word Document";
            this.chkOutputToWordDocument.UseVisualStyleBackColor = true;
            // 
            // chkExportDataFromExcelDocument
            // 
            this.chkExportDataFromExcelDocument.AutoSize = true;
            this.chkExportDataFromExcelDocument.Location = new System.Drawing.Point(17, 65);
            this.chkExportDataFromExcelDocument.Name = "chkExportDataFromExcelDocument";
            this.chkExportDataFromExcelDocument.Size = new System.Drawing.Size(186, 17);
            this.chkExportDataFromExcelDocument.TabIndex = 3;
            this.chkExportDataFromExcelDocument.Text = "Export Data from Excel Document";
            this.chkExportDataFromExcelDocument.UseVisualStyleBackColor = true;
            // 
            // chkOutputToExcelDocument
            // 
            this.chkOutputToExcelDocument.AutoSize = true;
            this.chkOutputToExcelDocument.Location = new System.Drawing.Point(17, 33);
            this.chkOutputToExcelDocument.Name = "chkOutputToExcelDocument";
            this.chkOutputToExcelDocument.Size = new System.Drawing.Size(151, 17);
            this.chkOutputToExcelDocument.TabIndex = 2;
            this.chkOutputToExcelDocument.Text = "Output to Excel Document";
            this.chkOutputToExcelDocument.UseVisualStyleBackColor = true;
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(39, 454);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 2;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogDocumentObjects\\InitWinFormsAppWit" +
    "hUserAndAppSettings\\InitWinFormsHelpFile.chm";
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
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
        private System.Windows.Forms.CheckBox chkOutputToExcelDocument;
        private System.Windows.Forms.CheckBox chkExportDataFromExcelDocument;
        private System.Windows.Forms.CheckBox chkOutputToWordDocument;
        private System.Windows.Forms.CheckBox chkOutputToRTFDocument;
        private System.Windows.Forms.CheckBox chkOutputToPDFDocument;
    }
}

