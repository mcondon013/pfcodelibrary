namespace TestprogDotNetZip
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
            this.chkIncludeDirectoryPathInZipArchive = new System.Windows.Forms.CheckBox();
            this.cmdGetMultipleFilesToZip = new System.Windows.Forms.Button();
            this.txtMultipleFilesToZip = new System.Windows.Forms.TextBox();
            this.chkOverwriteSilently = new System.Windows.Forms.CheckBox();
            this.chkFlattenFoldersOnExtract = new System.Windows.Forms.CheckBox();
            this.chkAddMultipleFilesToZipFile = new System.Windows.Forms.CheckBox();
            this.cmdGetUnzipOutputFolder = new System.Windows.Forms.Button();
            this.lblUnzipOutputFolder = new System.Windows.Forms.Label();
            this.txtUnzipOutputFolder = new System.Windows.Forms.TextBox();
            this.cmdGetZipOutputFolder = new System.Windows.Forms.Button();
            this.lblZipOutputFolder = new System.Windows.Forms.Label();
            this.txtZipOutputFolder = new System.Windows.Forms.TextBox();
            this.cmdGetFileToUnzipPath = new System.Windows.Forms.Button();
            this.lblFileToUnzip = new System.Windows.Forms.Label();
            this.txtFileToUnzip = new System.Windows.Forms.TextBox();
            this.cmdGetFileToZipPath = new System.Windows.Forms.Button();
            this.lbFileToZip = new System.Windows.Forms.Label();
            this.txtFileToZip = new System.Windows.Forms.TextBox();
            this.chkExtractFromZipFile = new System.Windows.Forms.CheckBox();
            this.chkCreateZipFile = new System.Windows.Forms.CheckBox();
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
            this.cmdExit.Location = new System.Drawing.Point(510, 459);
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
            this.grpTestsToRun.Controls.Add(this.chkIncludeDirectoryPathInZipArchive);
            this.grpTestsToRun.Controls.Add(this.cmdGetMultipleFilesToZip);
            this.grpTestsToRun.Controls.Add(this.txtMultipleFilesToZip);
            this.grpTestsToRun.Controls.Add(this.chkOverwriteSilently);
            this.grpTestsToRun.Controls.Add(this.chkFlattenFoldersOnExtract);
            this.grpTestsToRun.Controls.Add(this.chkAddMultipleFilesToZipFile);
            this.grpTestsToRun.Controls.Add(this.cmdGetUnzipOutputFolder);
            this.grpTestsToRun.Controls.Add(this.lblUnzipOutputFolder);
            this.grpTestsToRun.Controls.Add(this.txtUnzipOutputFolder);
            this.grpTestsToRun.Controls.Add(this.cmdGetZipOutputFolder);
            this.grpTestsToRun.Controls.Add(this.lblZipOutputFolder);
            this.grpTestsToRun.Controls.Add(this.txtZipOutputFolder);
            this.grpTestsToRun.Controls.Add(this.cmdGetFileToUnzipPath);
            this.grpTestsToRun.Controls.Add(this.lblFileToUnzip);
            this.grpTestsToRun.Controls.Add(this.txtFileToUnzip);
            this.grpTestsToRun.Controls.Add(this.cmdGetFileToZipPath);
            this.grpTestsToRun.Controls.Add(this.lbFileToZip);
            this.grpTestsToRun.Controls.Add(this.txtFileToZip);
            this.grpTestsToRun.Controls.Add(this.chkExtractFromZipFile);
            this.grpTestsToRun.Controls.Add(this.chkCreateZipFile);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 60);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 436);
            this.grpTestsToRun.TabIndex = 0;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // chkIncludeDirectoryPathInZipArchive
            // 
            this.chkIncludeDirectoryPathInZipArchive.AutoSize = true;
            this.chkIncludeDirectoryPathInZipArchive.Location = new System.Drawing.Point(47, 147);
            this.chkIncludeDirectoryPathInZipArchive.Name = "chkIncludeDirectoryPathInZipArchive";
            this.chkIncludeDirectoryPathInZipArchive.Size = new System.Drawing.Size(199, 17);
            this.chkIncludeDirectoryPathInZipArchive.TabIndex = 103;
            this.chkIncludeDirectoryPathInZipArchive.Text = "Include Directory Path in Zip Archive";
            this.chkIncludeDirectoryPathInZipArchive.UseVisualStyleBackColor = true;
            // 
            // cmdGetMultipleFilesToZip
            // 
            this.cmdGetMultipleFilesToZip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetMultipleFilesToZip.Location = new System.Drawing.Point(378, 183);
            this.cmdGetMultipleFilesToZip.Name = "cmdGetMultipleFilesToZip";
            this.cmdGetMultipleFilesToZip.Size = new System.Drawing.Size(38, 20);
            this.cmdGetMultipleFilesToZip.TabIndex = 102;
            this.cmdGetMultipleFilesToZip.Text = "•••";
            this.cmdGetMultipleFilesToZip.UseVisualStyleBackColor = true;
            this.cmdGetMultipleFilesToZip.Click += new System.EventHandler(this.cmdGetMultipleFilesToZip_Click);
            // 
            // txtMultipleFilesToZip
            // 
            this.txtMultipleFilesToZip.Location = new System.Drawing.Point(44, 203);
            this.txtMultipleFilesToZip.Multiline = true;
            this.txtMultipleFilesToZip.Name = "txtMultipleFilesToZip";
            this.txtMultipleFilesToZip.Size = new System.Drawing.Size(372, 68);
            this.txtMultipleFilesToZip.TabIndex = 101;
            // 
            // chkOverwriteSilently
            // 
            this.chkOverwriteSilently.AutoSize = true;
            this.chkOverwriteSilently.Location = new System.Drawing.Point(241, 403);
            this.chkOverwriteSilently.Name = "chkOverwriteSilently";
            this.chkOverwriteSilently.Size = new System.Drawing.Size(107, 17);
            this.chkOverwriteSilently.TabIndex = 100;
            this.chkOverwriteSilently.Text = "Overwrite Silently";
            this.chkOverwriteSilently.UseVisualStyleBackColor = true;
            // 
            // chkFlattenFoldersOnExtract
            // 
            this.chkFlattenFoldersOnExtract.AutoSize = true;
            this.chkFlattenFoldersOnExtract.Location = new System.Drawing.Point(47, 403);
            this.chkFlattenFoldersOnExtract.Name = "chkFlattenFoldersOnExtract";
            this.chkFlattenFoldersOnExtract.Size = new System.Drawing.Size(146, 17);
            this.chkFlattenFoldersOnExtract.TabIndex = 99;
            this.chkFlattenFoldersOnExtract.Text = "Flatten Folders on Extract";
            this.chkFlattenFoldersOnExtract.UseVisualStyleBackColor = true;
            // 
            // chkAddMultipleFilesToZipFile
            // 
            this.chkAddMultipleFilesToZipFile.AutoSize = true;
            this.chkAddMultipleFilesToZipFile.Location = new System.Drawing.Point(22, 179);
            this.chkAddMultipleFilesToZipFile.Name = "chkAddMultipleFilesToZipFile";
            this.chkAddMultipleFilesToZipFile.Size = new System.Drawing.Size(157, 17);
            this.chkAddMultipleFilesToZipFile.TabIndex = 98;
            this.chkAddMultipleFilesToZipFile.Text = "Add Multiple Files to Zip File";
            this.chkAddMultipleFilesToZipFile.UseVisualStyleBackColor = true;
            // 
            // cmdGetUnzipOutputFolder
            // 
            this.cmdGetUnzipOutputFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetUnzipOutputFolder.Location = new System.Drawing.Point(378, 356);
            this.cmdGetUnzipOutputFolder.Name = "cmdGetUnzipOutputFolder";
            this.cmdGetUnzipOutputFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdGetUnzipOutputFolder.TabIndex = 96;
            this.cmdGetUnzipOutputFolder.Text = "•••";
            this.cmdGetUnzipOutputFolder.UseVisualStyleBackColor = true;
            this.cmdGetUnzipOutputFolder.Click += new System.EventHandler(this.cmdGetUnzipOutputFolder_Click);
            // 
            // lblUnzipOutputFolder
            // 
            this.lblUnzipOutputFolder.AutoSize = true;
            this.lblUnzipOutputFolder.Location = new System.Drawing.Point(41, 360);
            this.lblUnzipOutputFolder.Name = "lblUnzipOutputFolder";
            this.lblUnzipOutputFolder.Size = new System.Drawing.Size(101, 13);
            this.lblUnzipOutputFolder.TabIndex = 97;
            this.lblUnzipOutputFolder.Text = "Unzip Output Folder";
            // 
            // txtUnzipOutputFolder
            // 
            this.txtUnzipOutputFolder.Location = new System.Drawing.Point(44, 376);
            this.txtUnzipOutputFolder.Name = "txtUnzipOutputFolder";
            this.txtUnzipOutputFolder.Size = new System.Drawing.Size(372, 20);
            this.txtUnzipOutputFolder.TabIndex = 96;
            // 
            // cmdGetZipOutputFolder
            // 
            this.cmdGetZipOutputFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetZipOutputFolder.Location = new System.Drawing.Point(378, 100);
            this.cmdGetZipOutputFolder.Name = "cmdGetZipOutputFolder";
            this.cmdGetZipOutputFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdGetZipOutputFolder.TabIndex = 95;
            this.cmdGetZipOutputFolder.Text = "•••";
            this.cmdGetZipOutputFolder.UseVisualStyleBackColor = true;
            this.cmdGetZipOutputFolder.Click += new System.EventHandler(this.cmdGetZipOutputFolder_Click);
            // 
            // lblZipOutputFolder
            // 
            this.lblZipOutputFolder.AutoSize = true;
            this.lblZipOutputFolder.Location = new System.Drawing.Point(44, 104);
            this.lblZipOutputFolder.Name = "lblZipOutputFolder";
            this.lblZipOutputFolder.Size = new System.Drawing.Size(89, 13);
            this.lblZipOutputFolder.TabIndex = 94;
            this.lblZipOutputFolder.Text = "Zip Output Folder";
            // 
            // txtZipOutputFolder
            // 
            this.txtZipOutputFolder.Location = new System.Drawing.Point(44, 120);
            this.txtZipOutputFolder.Name = "txtZipOutputFolder";
            this.txtZipOutputFolder.Size = new System.Drawing.Size(372, 20);
            this.txtZipOutputFolder.TabIndex = 93;
            // 
            // cmdGetFileToUnzipPath
            // 
            this.cmdGetFileToUnzipPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetFileToUnzipPath.Location = new System.Drawing.Point(378, 310);
            this.cmdGetFileToUnzipPath.Name = "cmdGetFileToUnzipPath";
            this.cmdGetFileToUnzipPath.Size = new System.Drawing.Size(38, 20);
            this.cmdGetFileToUnzipPath.TabIndex = 92;
            this.cmdGetFileToUnzipPath.Text = "•••";
            this.cmdGetFileToUnzipPath.UseVisualStyleBackColor = true;
            this.cmdGetFileToUnzipPath.Click += new System.EventHandler(this.cmdGetFileToUnzipPath_Click);
            // 
            // lblFileToUnzip
            // 
            this.lblFileToUnzip.AutoSize = true;
            this.lblFileToUnzip.Location = new System.Drawing.Point(41, 312);
            this.lblFileToUnzip.Name = "lblFileToUnzip";
            this.lblFileToUnzip.Size = new System.Drawing.Size(69, 13);
            this.lblFileToUnzip.TabIndex = 91;
            this.lblFileToUnzip.Text = "File To Unzip";
            // 
            // txtFileToUnzip
            // 
            this.txtFileToUnzip.Location = new System.Drawing.Point(44, 328);
            this.txtFileToUnzip.Name = "txtFileToUnzip";
            this.txtFileToUnzip.Size = new System.Drawing.Size(372, 20);
            this.txtFileToUnzip.TabIndex = 90;
            // 
            // cmdGetFileToZipPath
            // 
            this.cmdGetFileToZipPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetFileToZipPath.Location = new System.Drawing.Point(378, 53);
            this.cmdGetFileToZipPath.Name = "cmdGetFileToZipPath";
            this.cmdGetFileToZipPath.Size = new System.Drawing.Size(38, 20);
            this.cmdGetFileToZipPath.TabIndex = 89;
            this.cmdGetFileToZipPath.Text = "•••";
            this.cmdGetFileToZipPath.UseVisualStyleBackColor = true;
            this.cmdGetFileToZipPath.Click += new System.EventHandler(this.cmdGetFileToZipPath_Click);
            // 
            // lbFileToZip
            // 
            this.lbFileToZip.AutoSize = true;
            this.lbFileToZip.Location = new System.Drawing.Point(44, 57);
            this.lbFileToZip.Name = "lbFileToZip";
            this.lbFileToZip.Size = new System.Drawing.Size(60, 13);
            this.lbFileToZip.TabIndex = 3;
            this.lbFileToZip.Text = "File To Zip:";
            // 
            // txtFileToZip
            // 
            this.txtFileToZip.Location = new System.Drawing.Point(44, 73);
            this.txtFileToZip.Name = "txtFileToZip";
            this.txtFileToZip.Size = new System.Drawing.Size(372, 20);
            this.txtFileToZip.TabIndex = 2;
            // 
            // chkExtractFromZipFile
            // 
            this.chkExtractFromZipFile.AutoSize = true;
            this.chkExtractFromZipFile.Location = new System.Drawing.Point(22, 289);
            this.chkExtractFromZipFile.Name = "chkExtractFromZipFile";
            this.chkExtractFromZipFile.Size = new System.Drawing.Size(122, 17);
            this.chkExtractFromZipFile.TabIndex = 1;
            this.chkExtractFromZipFile.Text = "Extract From Zip File";
            this.chkExtractFromZipFile.UseVisualStyleBackColor = true;
            // 
            // chkCreateZipFile
            // 
            this.chkCreateZipFile.AutoSize = true;
            this.chkCreateZipFile.Location = new System.Drawing.Point(22, 33);
            this.chkCreateZipFile.Name = "chkCreateZipFile";
            this.chkCreateZipFile.Size = new System.Drawing.Size(94, 17);
            this.chkCreateZipFile.TabIndex = 0;
            this.chkCreateZipFile.Text = "Create Zip File";
            this.chkCreateZipFile.UseVisualStyleBackColor = true;
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(39, 514);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 8;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogDotNetZip\\TestprogDotNetZip\\InitWi" +
    "nFormsHelpFile.chm";
            // 
            // MainForm
            // 
            this.AcceptButton = this.cmdRunTests;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(638, 560);
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
        private System.Windows.Forms.CheckBox chkCreateZipFile;
        private System.Windows.Forms.CheckBox chkExtractFromZipFile;
        private System.Windows.Forms.Label lblUnzipOutputFolder;
        private System.Windows.Forms.Button cmdGetZipOutputFolder;
        private System.Windows.Forms.Label lblZipOutputFolder;
        private System.Windows.Forms.Button cmdGetFileToUnzipPath;
        private System.Windows.Forms.Label lblFileToUnzip;
        private System.Windows.Forms.Button cmdGetFileToZipPath;
        private System.Windows.Forms.Label lbFileToZip;
        internal System.Windows.Forms.TextBox txtUnzipOutputFolder;
        internal System.Windows.Forms.TextBox txtZipOutputFolder;
        internal System.Windows.Forms.TextBox txtFileToUnzip;
        internal System.Windows.Forms.TextBox txtFileToZip;
        private System.Windows.Forms.Button cmdGetUnzipOutputFolder;
        private System.Windows.Forms.CheckBox chkAddMultipleFilesToZipFile;
        internal System.Windows.Forms.CheckBox chkFlattenFoldersOnExtract;
        internal System.Windows.Forms.CheckBox chkOverwriteSilently;
        private System.Windows.Forms.Button cmdGetMultipleFilesToZip;
        internal System.Windows.Forms.TextBox txtMultipleFilesToZip;
        internal System.Windows.Forms.CheckBox chkIncludeDirectoryPathInZipArchive;
    }
}

