namespace TestprogOfficeInteropObjects
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
            this.grpReadDataBy = new System.Windows.Forms.GroupBox();
            this.optNamedRange = new System.Windows.Forms.RadioButton();
            this.optRowCol = new System.Windows.Forms.RadioButton();
            this.chkExportExcelDocument = new System.Windows.Forms.CheckBox();
            this.chkAppendToExcelDocument = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optPDFFormat = new System.Windows.Forms.RadioButton();
            this.optRTFFormat = new System.Windows.Forms.RadioButton();
            this.optDOCFormat = new System.Windows.Forms.RadioButton();
            this.optDOCXFormat = new System.Windows.Forms.RadioButton();
            this.grpExcelFormat = new System.Windows.Forms.GroupBox();
            this.chkUseExtWriteMethod = new System.Windows.Forms.CheckBox();
            this.optCsvFormat = new System.Windows.Forms.RadioButton();
            this.optXLSFormat = new System.Windows.Forms.RadioButton();
            this.optXLSXFormat = new System.Windows.Forms.RadioButton();
            this.chkOutputToWordDocument = new System.Windows.Forms.CheckBox();
            this.chkOutputToExcelDocument = new System.Windows.Forms.CheckBox();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.MainMenu.SuspendLayout();
            this.grpTestsToRun.SuspendLayout();
            this.grpReadDataBy.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpExcelFormat.SuspendLayout();
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
            this.cmdExit.TabIndex = 3;
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
            this.cmdRunTests.TabIndex = 2;
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
            this.grpTestsToRun.Controls.Add(this.grpReadDataBy);
            this.grpTestsToRun.Controls.Add(this.chkExportExcelDocument);
            this.grpTestsToRun.Controls.Add(this.chkAppendToExcelDocument);
            this.grpTestsToRun.Controls.Add(this.groupBox1);
            this.grpTestsToRun.Controls.Add(this.grpExcelFormat);
            this.grpTestsToRun.Controls.Add(this.chkOutputToWordDocument);
            this.grpTestsToRun.Controls.Add(this.chkOutputToExcelDocument);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 60);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 376);
            this.grpTestsToRun.TabIndex = 1;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // grpReadDataBy
            // 
            this.grpReadDataBy.Controls.Add(this.optNamedRange);
            this.grpReadDataBy.Controls.Add(this.optRowCol);
            this.grpReadDataBy.Location = new System.Drawing.Point(33, 133);
            this.grpReadDataBy.Name = "grpReadDataBy";
            this.grpReadDataBy.Size = new System.Drawing.Size(388, 43);
            this.grpReadDataBy.TabIndex = 6;
            this.grpReadDataBy.TabStop = false;
            this.grpReadDataBy.Text = "Read Data by";
            // 
            // optNamedRange
            // 
            this.optNamedRange.AutoSize = true;
            this.optNamedRange.Location = new System.Drawing.Point(170, 20);
            this.optNamedRange.Name = "optNamedRange";
            this.optNamedRange.Size = new System.Drawing.Size(94, 17);
            this.optNamedRange.TabIndex = 1;
            this.optNamedRange.TabStop = true;
            this.optNamedRange.Text = "Named Range";
            this.optNamedRange.UseVisualStyleBackColor = true;
            // 
            // optRowCol
            // 
            this.optRowCol.AutoSize = true;
            this.optRowCol.Location = new System.Drawing.Point(26, 20);
            this.optRowCol.Name = "optRowCol";
            this.optRowCol.Size = new System.Drawing.Size(67, 17);
            this.optRowCol.TabIndex = 0;
            this.optRowCol.TabStop = true;
            this.optRowCol.Text = "Row/Col";
            this.optRowCol.UseVisualStyleBackColor = true;
            // 
            // chkExportExcelDocument
            // 
            this.chkExportExcelDocument.AutoSize = true;
            this.chkExportExcelDocument.Location = new System.Drawing.Point(15, 110);
            this.chkExportExcelDocument.Name = "chkExportExcelDocument";
            this.chkExportExcelDocument.Size = new System.Drawing.Size(137, 17);
            this.chkExportExcelDocument.TabIndex = 5;
            this.chkExportExcelDocument.Text = "Export Excel Document";
            this.chkExportExcelDocument.UseVisualStyleBackColor = true;
            // 
            // chkAppendToExcelDocument
            // 
            this.chkAppendToExcelDocument.AutoSize = true;
            this.chkAppendToExcelDocument.Location = new System.Drawing.Point(265, 32);
            this.chkAppendToExcelDocument.Name = "chkAppendToExcelDocument";
            this.chkAppendToExcelDocument.Size = new System.Drawing.Size(156, 17);
            this.chkAppendToExcelDocument.TabIndex = 4;
            this.chkAppendToExcelDocument.Text = "Append to Excel Document";
            this.chkAppendToExcelDocument.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optPDFFormat);
            this.groupBox1.Controls.Add(this.optRTFFormat);
            this.groupBox1.Controls.Add(this.optDOCFormat);
            this.groupBox1.Controls.Add(this.optDOCXFormat);
            this.groupBox1.Location = new System.Drawing.Point(23, 216);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Word Output Format";
            // 
            // optPDFFormat
            // 
            this.optPDFFormat.AutoSize = true;
            this.optPDFFormat.Location = new System.Drawing.Point(170, 63);
            this.optPDFFormat.Name = "optPDFFormat";
            this.optPDFFormat.Size = new System.Drawing.Size(150, 17);
            this.optPDFFormat.TabIndex = 3;
            this.optPDFFormat.TabStop = true;
            this.optPDFFormat.Text = "Portable Document  (*.pdf)";
            this.optPDFFormat.UseVisualStyleBackColor = true;
            // 
            // optRTFFormat
            // 
            this.optRTFFormat.AutoSize = true;
            this.optRTFFormat.Location = new System.Drawing.Point(26, 63);
            this.optRTFFormat.Name = "optRTFFormat";
            this.optRTFFormat.Size = new System.Drawing.Size(96, 17);
            this.optRTFFormat.TabIndex = 2;
            this.optRTFFormat.TabStop = true;
            this.optRTFFormat.Text = "Rich Text (*.rtf)";
            this.optRTFFormat.UseVisualStyleBackColor = true;
            // 
            // optDOCFormat
            // 
            this.optDOCFormat.AutoSize = true;
            this.optDOCFormat.Location = new System.Drawing.Point(170, 29);
            this.optDOCFormat.Name = "optDOCFormat";
            this.optDOCFormat.Size = new System.Drawing.Size(112, 17);
            this.optDOCFormat.TabIndex = 1;
            this.optDOCFormat.TabStop = true;
            this.optDOCFormat.Text = "Word 2003 (*.doc)";
            this.optDOCFormat.UseVisualStyleBackColor = true;
            // 
            // optDOCXFormat
            // 
            this.optDOCXFormat.AutoSize = true;
            this.optDOCXFormat.Location = new System.Drawing.Point(26, 29);
            this.optDOCXFormat.Name = "optDOCXFormat";
            this.optDOCXFormat.Size = new System.Drawing.Size(113, 17);
            this.optDOCXFormat.TabIndex = 0;
            this.optDOCXFormat.TabStop = true;
            this.optDOCXFormat.Text = "Word 2007 (.docx)";
            this.optDOCXFormat.UseVisualStyleBackColor = true;
            // 
            // grpExcelFormat
            // 
            this.grpExcelFormat.Controls.Add(this.chkUseExtWriteMethod);
            this.grpExcelFormat.Controls.Add(this.optCsvFormat);
            this.grpExcelFormat.Controls.Add(this.optXLSFormat);
            this.grpExcelFormat.Controls.Add(this.optXLSXFormat);
            this.grpExcelFormat.Location = new System.Drawing.Point(33, 56);
            this.grpExcelFormat.Name = "grpExcelFormat";
            this.grpExcelFormat.Size = new System.Drawing.Size(398, 47);
            this.grpExcelFormat.TabIndex = 1;
            this.grpExcelFormat.TabStop = false;
            this.grpExcelFormat.Text = "Excel Output Format";
            // 
            // chkUseExtWriteMethod
            // 
            this.chkUseExtWriteMethod.AutoSize = true;
            this.chkUseExtWriteMethod.Location = new System.Drawing.Point(231, -3);
            this.chkUseExtWriteMethod.Name = "chkUseExtWriteMethod";
            this.chkUseExtWriteMethod.Size = new System.Drawing.Size(130, 17);
            this.chkUseExtWriteMethod.TabIndex = 3;
            this.chkUseExtWriteMethod.Text = "Use Ext Write Method";
            this.chkUseExtWriteMethod.UseVisualStyleBackColor = true;
            // 
            // optCsvFormat
            // 
            this.optCsvFormat.AutoSize = true;
            this.optCsvFormat.Location = new System.Drawing.Point(307, 20);
            this.optCsvFormat.Name = "optCsvFormat";
            this.optCsvFormat.Size = new System.Drawing.Size(81, 17);
            this.optCsvFormat.TabIndex = 2;
            this.optCsvFormat.TabStop = true;
            this.optCsvFormat.Text = "CSV Format";
            this.optCsvFormat.UseVisualStyleBackColor = true;
            // 
            // optXLSFormat
            // 
            this.optXLSFormat.AutoSize = true;
            this.optXLSFormat.Location = new System.Drawing.Point(170, 20);
            this.optXLSFormat.Name = "optXLSFormat";
            this.optXLSFormat.Size = new System.Drawing.Size(102, 17);
            this.optXLSFormat.TabIndex = 1;
            this.optXLSFormat.TabStop = true;
            this.optXLSFormat.Text = "Excel 2003 (.xls)";
            this.optXLSFormat.UseVisualStyleBackColor = true;
            // 
            // optXLSXFormat
            // 
            this.optXLSXFormat.AutoSize = true;
            this.optXLSXFormat.Location = new System.Drawing.Point(26, 20);
            this.optXLSXFormat.Name = "optXLSXFormat";
            this.optXLSXFormat.Size = new System.Drawing.Size(107, 17);
            this.optXLSXFormat.TabIndex = 0;
            this.optXLSXFormat.TabStop = true;
            this.optXLSXFormat.Text = "Excel 2007 (.xlsx)";
            this.optXLSXFormat.UseVisualStyleBackColor = true;
            // 
            // chkOutputToWordDocument
            // 
            this.chkOutputToWordDocument.AutoSize = true;
            this.chkOutputToWordDocument.Location = new System.Drawing.Point(15, 193);
            this.chkOutputToWordDocument.Name = "chkOutputToWordDocument";
            this.chkOutputToWordDocument.Size = new System.Drawing.Size(155, 17);
            this.chkOutputToWordDocument.TabIndex = 2;
            this.chkOutputToWordDocument.Text = "Output To Word Document";
            this.chkOutputToWordDocument.UseVisualStyleBackColor = true;
            // 
            // chkOutputToExcelDocument
            // 
            this.chkOutputToExcelDocument.AutoSize = true;
            this.chkOutputToExcelDocument.Location = new System.Drawing.Point(15, 32);
            this.chkOutputToExcelDocument.Name = "chkOutputToExcelDocument";
            this.chkOutputToExcelDocument.Size = new System.Drawing.Size(151, 17);
            this.chkOutputToExcelDocument.TabIndex = 0;
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
            this.chkEraseOutputBeforeEachTest.TabIndex = 4;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogOfficeInteropObjects\\InitWinFormsA" +
    "ppWithUserAndAppSettings\\InitWinFormsHelpFile.chm";
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
            this.grpReadDataBy.ResumeLayout(false);
            this.grpReadDataBy.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpExcelFormat.ResumeLayout(false);
            this.grpExcelFormat.PerformLayout();
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
        private System.Windows.Forms.CheckBox chkOutputToWordDocument;
        private System.Windows.Forms.CheckBox chkOutputToExcelDocument;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpExcelFormat;
        internal System.Windows.Forms.RadioButton optPDFFormat;
        internal System.Windows.Forms.RadioButton optRTFFormat;
        internal System.Windows.Forms.RadioButton optDOCFormat;
        internal System.Windows.Forms.RadioButton optDOCXFormat;
        internal System.Windows.Forms.RadioButton optXLSFormat;
        internal System.Windows.Forms.RadioButton optXLSXFormat;
        internal System.Windows.Forms.RadioButton optCsvFormat;
        private System.Windows.Forms.CheckBox chkAppendToExcelDocument;
        internal System.Windows.Forms.CheckBox chkUseExtWriteMethod;
        private System.Windows.Forms.GroupBox grpReadDataBy;
        private System.Windows.Forms.CheckBox chkExportExcelDocument;
        internal System.Windows.Forms.RadioButton optNamedRange;
        internal System.Windows.Forms.RadioButton optRowCol;
    }
}

