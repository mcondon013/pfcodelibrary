namespace TestprogWinFTP
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
            this.label5 = new System.Windows.Forms.Label();
            this.txtRenameTo = new System.Windows.Forms.TextBox();
            this.chkListRemoveFolderFilesTest = new System.Windows.Forms.CheckBox();
            this.chkDeleteRemoteFileTest = new System.Windows.Forms.CheckBox();
            this.chkRenameRemoteFileTest = new System.Windows.Forms.CheckBox();
            this.chkGetRemoteFileInfoTest = new System.Windows.Forms.CheckBox();
            this.chkDownloadFileTest = new System.Windows.Forms.CheckBox();
            this.chkUploadFileTest = new System.Windows.Forms.CheckBox();
            this.chkGetStaticKeysTest = new System.Windows.Forms.CheckBox();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFtpHost = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFtpPort = new System.Windows.Forms.TextBox();
            this.txtFtpUsername = new System.Windows.Forms.TextBox();
            this.txtFtpPassword = new System.Windows.Forms.TextBox();
            this.chkUseSSL = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBufferSize = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLocalSourceFile = new System.Windows.Forms.TextBox();
            this.txtRemoteFile = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLocalDestinationFile = new System.Windows.Forms.TextBox();
            this.cmdGetLocalSourceFile = new System.Windows.Forms.Button();
            this.cmdGetLocalDestinationFile = new System.Windows.Forms.Button();
            this.chkUseBinaryMode = new System.Windows.Forms.CheckBox();
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
            this.grpTestsToRun.Controls.Add(this.label5);
            this.grpTestsToRun.Controls.Add(this.txtRenameTo);
            this.grpTestsToRun.Controls.Add(this.chkListRemoveFolderFilesTest);
            this.grpTestsToRun.Controls.Add(this.chkDeleteRemoteFileTest);
            this.grpTestsToRun.Controls.Add(this.chkRenameRemoteFileTest);
            this.grpTestsToRun.Controls.Add(this.chkGetRemoteFileInfoTest);
            this.grpTestsToRun.Controls.Add(this.chkDownloadFileTest);
            this.grpTestsToRun.Controls.Add(this.chkUploadFileTest);
            this.grpTestsToRun.Controls.Add(this.chkGetStaticKeysTest);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 309);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 127);
            this.grpTestsToRun.TabIndex = 0;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(273, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "to";
            // 
            // txtRenameTo
            // 
            this.txtRenameTo.Location = new System.Drawing.Point(295, 57);
            this.txtRenameTo.Name = "txtRenameTo";
            this.txtRenameTo.Size = new System.Drawing.Size(114, 20);
            this.txtRenameTo.TabIndex = 7;
            // 
            // chkListRemoveFolderFilesTest
            // 
            this.chkListRemoveFolderFilesTest.AutoSize = true;
            this.chkListRemoveFolderFilesTest.Location = new System.Drawing.Point(251, 105);
            this.chkListRemoveFolderFilesTest.Name = "chkListRemoveFolderFilesTest";
            this.chkListRemoveFolderFilesTest.Size = new System.Drawing.Size(171, 17);
            this.chkListRemoveFolderFilesTest.TabIndex = 6;
            this.chkListRemoveFolderFilesTest.Text = "&7 List Remote Folder Files Test";
            this.chkListRemoveFolderFilesTest.UseVisualStyleBackColor = true;
            // 
            // chkDeleteRemoteFileTest
            // 
            this.chkDeleteRemoteFileTest.AutoSize = true;
            this.chkDeleteRemoteFileTest.Location = new System.Drawing.Point(251, 81);
            this.chkDeleteRemoteFileTest.Name = "chkDeleteRemoteFileTest";
            this.chkDeleteRemoteFileTest.Size = new System.Drawing.Size(149, 17);
            this.chkDeleteRemoteFileTest.TabIndex = 5;
            this.chkDeleteRemoteFileTest.Text = "&6 Delete Remote File Test";
            this.chkDeleteRemoteFileTest.UseVisualStyleBackColor = true;
            // 
            // chkRenameRemoteFileTest
            // 
            this.chkRenameRemoteFileTest.AutoSize = true;
            this.chkRenameRemoteFileTest.Location = new System.Drawing.Point(251, 33);
            this.chkRenameRemoteFileTest.Name = "chkRenameRemoteFileTest";
            this.chkRenameRemoteFileTest.Size = new System.Drawing.Size(158, 17);
            this.chkRenameRemoteFileTest.TabIndex = 4;
            this.chkRenameRemoteFileTest.Text = "&5 Rename Remote File Test";
            this.chkRenameRemoteFileTest.UseVisualStyleBackColor = true;
            // 
            // chkGetRemoteFileInfoTest
            // 
            this.chkGetRemoteFileInfoTest.AutoSize = true;
            this.chkGetRemoteFileInfoTest.Location = new System.Drawing.Point(17, 104);
            this.chkGetRemoteFileInfoTest.Name = "chkGetRemoteFileInfoTest";
            this.chkGetRemoteFileInfoTest.Size = new System.Drawing.Size(156, 17);
            this.chkGetRemoteFileInfoTest.TabIndex = 3;
            this.chkGetRemoteFileInfoTest.Text = "&4 Get Remote File Info Test";
            this.chkGetRemoteFileInfoTest.UseVisualStyleBackColor = true;
            // 
            // chkDownloadFileTest
            // 
            this.chkDownloadFileTest.AutoSize = true;
            this.chkDownloadFileTest.Location = new System.Drawing.Point(17, 81);
            this.chkDownloadFileTest.Name = "chkDownloadFileTest";
            this.chkDownloadFileTest.Size = new System.Drawing.Size(126, 17);
            this.chkDownloadFileTest.TabIndex = 2;
            this.chkDownloadFileTest.Text = "&3 Download File Test";
            this.chkDownloadFileTest.UseVisualStyleBackColor = true;
            // 
            // chkUploadFileTest
            // 
            this.chkUploadFileTest.AutoSize = true;
            this.chkUploadFileTest.Location = new System.Drawing.Point(17, 57);
            this.chkUploadFileTest.Name = "chkUploadFileTest";
            this.chkUploadFileTest.Size = new System.Drawing.Size(112, 17);
            this.chkUploadFileTest.TabIndex = 1;
            this.chkUploadFileTest.Text = "&2 Upload File Test";
            this.chkUploadFileTest.UseVisualStyleBackColor = true;
            // 
            // chkGetStaticKeysTest
            // 
            this.chkGetStaticKeysTest.AutoSize = true;
            this.chkGetStaticKeysTest.Location = new System.Drawing.Point(17, 33);
            this.chkGetStaticKeysTest.Name = "chkGetStaticKeysTest";
            this.chkGetStaticKeysTest.Size = new System.Drawing.Size(123, 17);
            this.chkGetStaticKeysTest.TabIndex = 0;
            this.chkGetStaticKeysTest.Text = "&1 GetStaticKeysTest";
            this.chkGetStaticKeysTest.UseVisualStyleBackColor = true;
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
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogWinFTP\\InitWinFormsAppWithExtended" +
    "Options\\InitWinFormsHelpFile.chm";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "FTP Server:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "FTP Username:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "FTP Password:";
            // 
            // txtFtpHost
            // 
            this.txtFtpHost.Location = new System.Drawing.Point(130, 56);
            this.txtFtpHost.Name = "txtFtpHost";
            this.txtFtpHost.Size = new System.Drawing.Size(116, 20);
            this.txtFtpHost.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Port:";
            // 
            // txtFtpPort
            // 
            this.txtFtpPort.Location = new System.Drawing.Point(130, 78);
            this.txtFtpPort.Name = "txtFtpPort";
            this.txtFtpPort.Size = new System.Drawing.Size(116, 20);
            this.txtFtpPort.TabIndex = 14;
            // 
            // txtFtpUsername
            // 
            this.txtFtpUsername.Location = new System.Drawing.Point(130, 100);
            this.txtFtpUsername.Name = "txtFtpUsername";
            this.txtFtpUsername.Size = new System.Drawing.Size(116, 20);
            this.txtFtpUsername.TabIndex = 15;
            // 
            // txtFtpPassword
            // 
            this.txtFtpPassword.Location = new System.Drawing.Point(130, 123);
            this.txtFtpPassword.Name = "txtFtpPassword";
            this.txtFtpPassword.Size = new System.Drawing.Size(116, 20);
            this.txtFtpPassword.TabIndex = 16;
            // 
            // chkUseSSL
            // 
            this.chkUseSSL.AutoSize = true;
            this.chkUseSSL.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUseSSL.Location = new System.Drawing.Point(290, 58);
            this.chkUseSSL.Name = "chkUseSSL";
            this.chkUseSSL.Size = new System.Drawing.Size(128, 17);
            this.chkUseSSL.TabIndex = 17;
            this.chkUseSSL.Text = "Use SSL                    ";
            this.chkUseSSL.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(290, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Buffer Size:";
            // 
            // txtBufferSize
            // 
            this.txtBufferSize.Location = new System.Drawing.Point(419, 127);
            this.txtBufferSize.Name = "txtBufferSize";
            this.txtBufferSize.Size = new System.Drawing.Size(57, 20);
            this.txtBufferSize.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(39, 167);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Local Source File:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(39, 207);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Remote File:";
            // 
            // txtLocalSourceFile
            // 
            this.txtLocalSourceFile.Location = new System.Drawing.Point(39, 184);
            this.txtLocalSourceFile.Name = "txtLocalSourceFile";
            this.txtLocalSourceFile.Size = new System.Drawing.Size(437, 20);
            this.txtLocalSourceFile.TabIndex = 26;
            // 
            // txtRemoteFile
            // 
            this.txtRemoteFile.Location = new System.Drawing.Point(39, 224);
            this.txtRemoteFile.Name = "txtRemoteFile";
            this.txtRemoteFile.Size = new System.Drawing.Size(437, 20);
            this.txtRemoteFile.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(42, 251);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "Local Destination File:";
            // 
            // txtLocalDestinationFile
            // 
            this.txtLocalDestinationFile.Location = new System.Drawing.Point(39, 268);
            this.txtLocalDestinationFile.Name = "txtLocalDestinationFile";
            this.txtLocalDestinationFile.Size = new System.Drawing.Size(437, 20);
            this.txtLocalDestinationFile.TabIndex = 29;
            // 
            // cmdGetLocalSourceFile
            // 
            this.cmdGetLocalSourceFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetLocalSourceFile.Location = new System.Drawing.Point(438, 163);
            this.cmdGetLocalSourceFile.Name = "cmdGetLocalSourceFile";
            this.cmdGetLocalSourceFile.Size = new System.Drawing.Size(38, 20);
            this.cmdGetLocalSourceFile.TabIndex = 89;
            this.cmdGetLocalSourceFile.Text = "•••";
            this.cmdGetLocalSourceFile.UseVisualStyleBackColor = true;
            // 
            // cmdGetLocalDestinationFile
            // 
            this.cmdGetLocalDestinationFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetLocalDestinationFile.Location = new System.Drawing.Point(438, 247);
            this.cmdGetLocalDestinationFile.Name = "cmdGetLocalDestinationFile";
            this.cmdGetLocalDestinationFile.Size = new System.Drawing.Size(38, 20);
            this.cmdGetLocalDestinationFile.TabIndex = 90;
            this.cmdGetLocalDestinationFile.Text = "•••";
            this.cmdGetLocalDestinationFile.UseVisualStyleBackColor = true;
            // 
            // chkUseBinaryMode
            // 
            this.chkUseBinaryMode.AutoSize = true;
            this.chkUseBinaryMode.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUseBinaryMode.Location = new System.Drawing.Point(290, 82);
            this.chkUseBinaryMode.Name = "chkUseBinaryMode";
            this.chkUseBinaryMode.Size = new System.Drawing.Size(128, 17);
            this.chkUseBinaryMode.TabIndex = 91;
            this.chkUseBinaryMode.Text = "Use Binary Mode       ";
            this.chkUseBinaryMode.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AcceptButton = this.cmdRunTests;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(638, 500);
            this.Controls.Add(this.chkUseBinaryMode);
            this.Controls.Add(this.cmdGetLocalDestinationFile);
            this.Controls.Add(this.cmdGetLocalSourceFile);
            this.Controls.Add(this.txtLocalDestinationFile);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtRemoteFile);
            this.Controls.Add(this.txtLocalSourceFile);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtBufferSize);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chkUseSSL);
            this.Controls.Add(this.txtFtpPassword);
            this.Controls.Add(this.txtFtpUsername);
            this.Controls.Add(this.txtFtpPort);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFtpHost);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.CheckBox chkGetStaticKeysTest;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtFtpHost;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtFtpPort;
        internal System.Windows.Forms.TextBox txtFtpUsername;
        internal System.Windows.Forms.TextBox txtFtpPassword;
        internal System.Windows.Forms.CheckBox chkUseSSL;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.TextBox txtBufferSize;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.TextBox txtLocalSourceFile;
        internal System.Windows.Forms.TextBox txtRemoteFile;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.TextBox txtLocalDestinationFile;
        private System.Windows.Forms.CheckBox chkListRemoveFolderFilesTest;
        private System.Windows.Forms.CheckBox chkDeleteRemoteFileTest;
        private System.Windows.Forms.CheckBox chkRenameRemoteFileTest;
        private System.Windows.Forms.CheckBox chkGetRemoteFileInfoTest;
        private System.Windows.Forms.CheckBox chkDownloadFileTest;
        private System.Windows.Forms.CheckBox chkUploadFileTest;
        private System.Windows.Forms.Button cmdGetLocalSourceFile;
        private System.Windows.Forms.Button cmdGetLocalDestinationFile;
        private System.Windows.Forms.CheckBox chkUseBinaryMode;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox txtRenameTo;
    }
}

