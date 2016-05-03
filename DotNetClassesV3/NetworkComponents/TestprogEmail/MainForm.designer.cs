namespace TestprogEmail
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
            this.chkResendEmailsTest = new System.Windows.Forms.CheckBox();
            this.txtMaxIdleTime = new System.Windows.Forms.TextBox();
            this.txtSendTimeout = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBCCAddress = new System.Windows.Forms.TextBox();
            this.txtCCAddress = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMessageBody = new System.Windows.Forms.TextBox();
            this.txtSubjectLine = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkUseSsl = new System.Windows.Forms.CheckBox();
            this.txtToAddress = new System.Windows.Forms.TextBox();
            this.txtFromAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSmtpPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSmtpServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkSmtpTest = new System.Windows.Forms.CheckBox();
            this.chkSMTPQuickTest = new System.Windows.Forms.CheckBox();
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
            this.cmdExit.Location = new System.Drawing.Point(510, 509);
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
            this.grpTestsToRun.Controls.Add(this.chkResendEmailsTest);
            this.grpTestsToRun.Controls.Add(this.txtMaxIdleTime);
            this.grpTestsToRun.Controls.Add(this.txtSendTimeout);
            this.grpTestsToRun.Controls.Add(this.label10);
            this.grpTestsToRun.Controls.Add(this.label9);
            this.grpTestsToRun.Controls.Add(this.txtBCCAddress);
            this.grpTestsToRun.Controls.Add(this.txtCCAddress);
            this.grpTestsToRun.Controls.Add(this.label8);
            this.grpTestsToRun.Controls.Add(this.label7);
            this.grpTestsToRun.Controls.Add(this.txtMessageBody);
            this.grpTestsToRun.Controls.Add(this.txtSubjectLine);
            this.grpTestsToRun.Controls.Add(this.label6);
            this.grpTestsToRun.Controls.Add(this.label5);
            this.grpTestsToRun.Controls.Add(this.chkUseSsl);
            this.grpTestsToRun.Controls.Add(this.txtToAddress);
            this.grpTestsToRun.Controls.Add(this.txtFromAddress);
            this.grpTestsToRun.Controls.Add(this.label4);
            this.grpTestsToRun.Controls.Add(this.label3);
            this.grpTestsToRun.Controls.Add(this.txtSmtpPort);
            this.grpTestsToRun.Controls.Add(this.label2);
            this.grpTestsToRun.Controls.Add(this.txtSmtpServer);
            this.grpTestsToRun.Controls.Add(this.label1);
            this.grpTestsToRun.Controls.Add(this.chkSmtpTest);
            this.grpTestsToRun.Controls.Add(this.chkSMTPQuickTest);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 60);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 468);
            this.grpTestsToRun.TabIndex = 0;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // chkResendEmailsTest
            // 
            this.chkResendEmailsTest.AutoSize = true;
            this.chkResendEmailsTest.Location = new System.Drawing.Point(17, 445);
            this.chkResendEmailsTest.Name = "chkResendEmailsTest";
            this.chkResendEmailsTest.Size = new System.Drawing.Size(129, 17);
            this.chkResendEmailsTest.TabIndex = 23;
            this.chkResendEmailsTest.Text = "&3 Resend Emails Test";
            this.chkResendEmailsTest.UseVisualStyleBackColor = true;
            // 
            // txtMaxIdleTime
            // 
            this.txtMaxIdleTime.Location = new System.Drawing.Point(336, 161);
            this.txtMaxIdleTime.Name = "txtMaxIdleTime";
            this.txtMaxIdleTime.Size = new System.Drawing.Size(68, 20);
            this.txtMaxIdleTime.TabIndex = 22;
            // 
            // txtSendTimeout
            // 
            this.txtSendTimeout.Location = new System.Drawing.Point(135, 162);
            this.txtSendTimeout.Name = "txtSendTimeout";
            this.txtSendTimeout.Size = new System.Drawing.Size(59, 20);
            this.txtSendTimeout.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(254, 162);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Max Idle Time:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(39, 162);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Send Timeout:";
            // 
            // txtBCCAddress
            // 
            this.txtBCCAddress.Location = new System.Drawing.Point(135, 286);
            this.txtBCCAddress.Name = "txtBCCAddress";
            this.txtBCCAddress.Size = new System.Drawing.Size(269, 20);
            this.txtBCCAddress.TabIndex = 18;
            // 
            // txtCCAddress
            // 
            this.txtCCAddress.Location = new System.Drawing.Point(135, 254);
            this.txtCCAddress.Name = "txtCCAddress";
            this.txtCCAddress.Size = new System.Drawing.Size(269, 20);
            this.txtCCAddress.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(39, 286);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "BCC Address:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(42, 254);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "CC Address:";
            // 
            // txtMessageBody
            // 
            this.txtMessageBody.Location = new System.Drawing.Point(135, 352);
            this.txtMessageBody.Multiline = true;
            this.txtMessageBody.Name = "txtMessageBody";
            this.txtMessageBody.Size = new System.Drawing.Size(269, 68);
            this.txtMessageBody.TabIndex = 14;
            // 
            // txtSubjectLine
            // 
            this.txtSubjectLine.Location = new System.Drawing.Point(135, 318);
            this.txtSubjectLine.Name = "txtSubjectLine";
            this.txtSubjectLine.Size = new System.Drawing.Size(269, 20);
            this.txtSubjectLine.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 352);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Message Body:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 318);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Subject Line:";
            // 
            // chkUseSsl
            // 
            this.chkUseSsl.AutoSize = true;
            this.chkUseSsl.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUseSsl.Location = new System.Drawing.Point(336, 135);
            this.chkUseSsl.Name = "chkUseSsl";
            this.chkUseSsl.Size = new System.Drawing.Size(68, 17);
            this.chkUseSsl.TabIndex = 10;
            this.chkUseSsl.Text = "Use SSL";
            this.chkUseSsl.UseVisualStyleBackColor = true;
            // 
            // txtToAddress
            // 
            this.txtToAddress.Location = new System.Drawing.Point(135, 223);
            this.txtToAddress.Name = "txtToAddress";
            this.txtToAddress.Size = new System.Drawing.Size(269, 20);
            this.txtToAddress.TabIndex = 9;
            // 
            // txtFromAddress
            // 
            this.txtFromAddress.Location = new System.Drawing.Point(135, 194);
            this.txtFromAddress.Name = "txtFromAddress";
            this.txtFromAddress.Size = new System.Drawing.Size(269, 20);
            this.txtFromAddress.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 223);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "To Address:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "From Address:";
            // 
            // txtSmtpPort
            // 
            this.txtSmtpPort.Location = new System.Drawing.Point(135, 133);
            this.txtSmtpPort.Name = "txtSmtpPort";
            this.txtSmtpPort.Size = new System.Drawing.Size(59, 20);
            this.txtSmtpPort.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "SMTP Port:";
            // 
            // txtSmtpServer
            // 
            this.txtSmtpServer.Location = new System.Drawing.Point(135, 102);
            this.txtSmtpServer.Name = "txtSmtpServer";
            this.txtSmtpServer.Size = new System.Drawing.Size(269, 20);
            this.txtSmtpServer.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "SMTP Server:";
            // 
            // chkSmtpTest
            // 
            this.chkSmtpTest.AutoSize = true;
            this.chkSmtpTest.Location = new System.Drawing.Point(17, 68);
            this.chkSmtpTest.Name = "chkSmtpTest";
            this.chkSmtpTest.Size = new System.Drawing.Size(89, 17);
            this.chkSmtpTest.TabIndex = 1;
            this.chkSmtpTest.Text = "&2 SMTP Test";
            this.chkSmtpTest.UseVisualStyleBackColor = true;
            // 
            // chkSMTPQuickTest
            // 
            this.chkSMTPQuickTest.AutoSize = true;
            this.chkSMTPQuickTest.Location = new System.Drawing.Point(17, 33);
            this.chkSMTPQuickTest.Name = "chkSMTPQuickTest";
            this.chkSMTPQuickTest.Size = new System.Drawing.Size(120, 17);
            this.chkSMTPQuickTest.TabIndex = 0;
            this.chkSMTPQuickTest.Text = "&1 SMTP Quick Test";
            this.chkSMTPQuickTest.UseVisualStyleBackColor = true;
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(39, 547);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 8;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogEmail\\TestprogEmail\\InitWinFormsHe" +
    "lpFile.chm";
            // 
            // MainForm
            // 
            this.AcceptButton = this.cmdRunTests;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(638, 576);
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
        private System.Windows.Forms.CheckBox chkSMTPQuickTest;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        private System.Windows.Forms.CheckBox chkSmtpTest;
        internal System.Windows.Forms.TextBox txtSmtpPort;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox txtSmtpServer;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.CheckBox chkUseSsl;
        internal System.Windows.Forms.TextBox txtToAddress;
        internal System.Windows.Forms.TextBox txtFromAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtMessageBody;
        internal System.Windows.Forms.TextBox txtSubjectLine;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox txtBCCAddress;
        internal System.Windows.Forms.TextBox txtCCAddress;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.TextBox txtMaxIdleTime;
        internal System.Windows.Forms.TextBox txtSendTimeout;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkResendEmailsTest;
    }
}

