namespace TestprogMessageLogs
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
            this.components = new System.ComponentModel.Container();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdRunTest = new System.Windows.Forms.Button();
            this.cmdResetForm = new System.Windows.Forms.Button();
            this.mainFormToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.optRunGetSumFromDLL = new System.Windows.Forms.RadioButton();
            this.lblMaxNumGetSum = new System.Windows.Forms.Label();
            this.lblMinNumGetSum = new System.Windows.Forms.Label();
            this.txtMinNumGetSum = new System.Windows.Forms.TextBox();
            this.txtMaxNumGetSum = new System.Windows.Forms.TextBox();
            this.lblOutputInterval = new System.Windows.Forms.Label();
            this.chkShowDateTimeInOutput = new System.Windows.Forms.CheckBox();
            this.txtOutputInterval = new System.Windows.Forms.TextBox();
            this.cmdHideDLLMessageLog = new System.Windows.Forms.Button();
            this.cmdShowDLLMessageLog = new System.Windows.Forms.Button();
            this.optRunTestprogApplicationMessageLogTest = new System.Windows.Forms.RadioButton();
            this.grpMessageDefinition = new System.Windows.Forms.GroupBox();
            this.cmdHideAppLog = new System.Windows.Forms.Button();
            this.cmdShowAppLog = new System.Windows.Forms.Button();
            this.optOutputMessagesToTextLogFile = new System.Windows.Forms.RadioButton();
            this.txtTextLogFilePath = new System.Windows.Forms.TextBox();
            this.chkAppendMessagesIfFileExists = new System.Windows.Forms.CheckBox();
            this.chkShowApplicationNameWithEachMessage = new System.Windows.Forms.CheckBox();
            this.chkShowMessageTypeWithEachMessage = new System.Windows.Forms.CheckBox();
            this.chkShowMachineNameWithEachMessage = new System.Windows.Forms.CheckBox();
            this.grpTextLogFileParameters = new System.Windows.Forms.GroupBox();
            this.optOutputMessagesToWindowsApplicationEventLog = new System.Windows.Forms.RadioButton();
            this.grpEventLogParameters = new System.Windows.Forms.GroupBox();
            this.cmdDeleteEventSource = new System.Windows.Forms.Button();
            this.txtNumErrorMessagesToWrite = new System.Windows.Forms.TextBox();
            this.lblNumErrorMessagesToWrite = new System.Windows.Forms.Label();
            this.txtNumWarningMessagesToWrite = new System.Windows.Forms.TextBox();
            this.lblNumWarningMessagesToWrite = new System.Windows.Forms.Label();
            this.txtNumInformationMessagesToWrite = new System.Windows.Forms.TextBox();
            this.lblNumInformationMessagesToWrite = new System.Windows.Forms.Label();
            this.lblNumberOfMessagesToWrite = new System.Windows.Forms.Label();
            this.txtEventSource = new System.Windows.Forms.TextBox();
            this.lblEventSource = new System.Windows.Forms.Label();
            this.panelCommandButtons = new System.Windows.Forms.Panel();
            this.grpMessageDefinition.SuspendLayout();
            this.grpTextLogFileParameters.SuspendLayout();
            this.grpEventLogParameters.SuspendLayout();
            this.panelCommandButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Location = new System.Drawing.Point(21, 164);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(93, 37);
            this.cmdExit.TabIndex = 0;
            this.cmdExit.Text = "E&xit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdRunTest
            // 
            this.cmdRunTest.Location = new System.Drawing.Point(21, 15);
            this.cmdRunTest.Name = "cmdRunTest";
            this.cmdRunTest.Size = new System.Drawing.Size(93, 37);
            this.cmdRunTest.TabIndex = 1;
            this.cmdRunTest.Text = "&Run Test";
            this.cmdRunTest.UseVisualStyleBackColor = true;
            this.cmdRunTest.Click += new System.EventHandler(this.cmdRunTest_Click);
            // 
            // cmdResetForm
            // 
            this.cmdResetForm.Location = new System.Drawing.Point(21, 125);
            this.cmdResetForm.Name = "cmdResetForm";
            this.cmdResetForm.Size = new System.Drawing.Size(93, 23);
            this.cmdResetForm.TabIndex = 2;
            this.cmdResetForm.Text = "Reset Form";
            this.mainFormToolTips.SetToolTip(this.cmdResetForm, "Restores form to its default screen position and size");
            this.cmdResetForm.UseVisualStyleBackColor = true;
            this.cmdResetForm.Click += new System.EventHandler(this.cmdResetForm_Click);
            // 
            // optRunGetSumFromDLL
            // 
            this.optRunGetSumFromDLL.AutoSize = true;
            this.optRunGetSumFromDLL.Location = new System.Drawing.Point(28, 229);
            this.optRunGetSumFromDLL.Name = "optRunGetSumFromDLL";
            this.optRunGetSumFromDLL.Size = new System.Drawing.Size(156, 17);
            this.optRunGetSumFromDLL.TabIndex = 3;
            this.optRunGetSumFromDLL.TabStop = true;
            this.optRunGetSumFromDLL.Text = "Run GetSum from DLL Test";
            this.optRunGetSumFromDLL.UseVisualStyleBackColor = true;
            // 
            // lblMaxNumGetSum
            // 
            this.lblMaxNumGetSum.AutoSize = true;
            this.lblMaxNumGetSum.Location = new System.Drawing.Point(23, 52);
            this.lblMaxNumGetSum.Name = "lblMaxNumGetSum";
            this.lblMaxNumGetSum.Size = new System.Drawing.Size(181, 13);
            this.lblMaxNumGetSum.TabIndex = 4;
            this.lblMaxNumGetSum.Text = "Max Number for Get Sum Calculation";
            // 
            // lblMinNumGetSum
            // 
            this.lblMinNumGetSum.AutoSize = true;
            this.lblMinNumGetSum.Location = new System.Drawing.Point(23, 27);
            this.lblMinNumGetSum.Name = "lblMinNumGetSum";
            this.lblMinNumGetSum.Size = new System.Drawing.Size(178, 13);
            this.lblMinNumGetSum.TabIndex = 5;
            this.lblMinNumGetSum.Text = "Min Number for Get Sum Calculation";
            // 
            // txtMinNumGetSum
            // 
            this.txtMinNumGetSum.Location = new System.Drawing.Point(226, 27);
            this.txtMinNumGetSum.Name = "txtMinNumGetSum";
            this.txtMinNumGetSum.Size = new System.Drawing.Size(57, 20);
            this.txtMinNumGetSum.TabIndex = 6;
            this.txtMinNumGetSum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMaxNumGetSum
            // 
            this.txtMaxNumGetSum.Location = new System.Drawing.Point(226, 54);
            this.txtMaxNumGetSum.Name = "txtMaxNumGetSum";
            this.txtMaxNumGetSum.Size = new System.Drawing.Size(57, 20);
            this.txtMaxNumGetSum.TabIndex = 7;
            this.txtMaxNumGetSum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblOutputInterval
            // 
            this.lblOutputInterval.AutoSize = true;
            this.lblOutputInterval.Location = new System.Drawing.Point(23, 75);
            this.lblOutputInterval.Name = "lblOutputInterval";
            this.lblOutputInterval.Size = new System.Drawing.Size(77, 13);
            this.lblOutputInterval.TabIndex = 8;
            this.lblOutputInterval.Text = "Output Interval";
            // 
            // chkShowDateTimeInOutput
            // 
            this.chkShowDateTimeInOutput.AutoSize = true;
            this.chkShowDateTimeInOutput.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkShowDateTimeInOutput.Location = new System.Drawing.Point(23, 100);
            this.chkShowDateTimeInOutput.Name = "chkShowDateTimeInOutput";
            this.chkShowDateTimeInOutput.Size = new System.Drawing.Size(153, 17);
            this.chkShowDateTimeInOutput.TabIndex = 9;
            this.chkShowDateTimeInOutput.Text = "Show Date/Time in Output";
            this.chkShowDateTimeInOutput.UseVisualStyleBackColor = true;
            // 
            // txtOutputInterval
            // 
            this.txtOutputInterval.Location = new System.Drawing.Point(161, 75);
            this.txtOutputInterval.Name = "txtOutputInterval";
            this.txtOutputInterval.Size = new System.Drawing.Size(65, 20);
            this.txtOutputInterval.TabIndex = 10;
            // 
            // cmdHideDLLMessageLog
            // 
            this.cmdHideDLLMessageLog.Location = new System.Drawing.Point(45, 252);
            this.cmdHideDLLMessageLog.Name = "cmdHideDLLMessageLog";
            this.cmdHideDLLMessageLog.Size = new System.Drawing.Size(93, 21);
            this.cmdHideDLLMessageLog.TabIndex = 11;
            this.cmdHideDLLMessageLog.Text = "Hide DLL Log";
            this.cmdHideDLLMessageLog.UseVisualStyleBackColor = true;
            this.cmdHideDLLMessageLog.Click += new System.EventHandler(this.cmdHideDLLMessageLog_Click);
            // 
            // cmdShowDLLMessageLog
            // 
            this.cmdShowDLLMessageLog.Location = new System.Drawing.Point(152, 252);
            this.cmdShowDLLMessageLog.Name = "cmdShowDLLMessageLog";
            this.cmdShowDLLMessageLog.Size = new System.Drawing.Size(93, 21);
            this.cmdShowDLLMessageLog.TabIndex = 12;
            this.cmdShowDLLMessageLog.Text = "Show DLL Log";
            this.cmdShowDLLMessageLog.UseVisualStyleBackColor = true;
            this.cmdShowDLLMessageLog.Click += new System.EventHandler(this.cmdShowDLLMessageLog_Click);
            // 
            // optRunTestprogApplicationMessageLogTest
            // 
            this.optRunTestprogApplicationMessageLogTest.AutoSize = true;
            this.optRunTestprogApplicationMessageLogTest.Location = new System.Drawing.Point(28, 175);
            this.optRunTestprogApplicationMessageLogTest.Name = "optRunTestprogApplicationMessageLogTest";
            this.optRunTestprogApplicationMessageLogTest.Size = new System.Drawing.Size(302, 17);
            this.optRunTestprogApplicationMessageLogTest.TabIndex = 13;
            this.optRunTestprogApplicationMessageLogTest.TabStop = true;
            this.optRunTestprogApplicationMessageLogTest.Text = "Run TestprogMessageLogs Application Message Log Test";
            this.optRunTestprogApplicationMessageLogTest.UseVisualStyleBackColor = true;
            // 
            // grpMessageDefinition
            // 
            this.grpMessageDefinition.Controls.Add(this.lblMaxNumGetSum);
            this.grpMessageDefinition.Controls.Add(this.lblMinNumGetSum);
            this.grpMessageDefinition.Controls.Add(this.txtMinNumGetSum);
            this.grpMessageDefinition.Controls.Add(this.txtMaxNumGetSum);
            this.grpMessageDefinition.Controls.Add(this.txtOutputInterval);
            this.grpMessageDefinition.Controls.Add(this.lblOutputInterval);
            this.grpMessageDefinition.Controls.Add(this.chkShowDateTimeInOutput);
            this.grpMessageDefinition.Location = new System.Drawing.Point(28, 34);
            this.grpMessageDefinition.Name = "grpMessageDefinition";
            this.grpMessageDefinition.Size = new System.Drawing.Size(337, 135);
            this.grpMessageDefinition.TabIndex = 14;
            this.grpMessageDefinition.TabStop = false;
            this.grpMessageDefinition.Text = "Message Definition";
            // 
            // cmdHideAppLog
            // 
            this.cmdHideAppLog.Location = new System.Drawing.Point(45, 198);
            this.cmdHideAppLog.Name = "cmdHideAppLog";
            this.cmdHideAppLog.Size = new System.Drawing.Size(93, 21);
            this.cmdHideAppLog.TabIndex = 15;
            this.cmdHideAppLog.Text = "Hide App Log";
            this.cmdHideAppLog.UseVisualStyleBackColor = true;
            this.cmdHideAppLog.Click += new System.EventHandler(this.cmdHideAppLog_Click);
            // 
            // cmdShowAppLog
            // 
            this.cmdShowAppLog.Location = new System.Drawing.Point(152, 198);
            this.cmdShowAppLog.Name = "cmdShowAppLog";
            this.cmdShowAppLog.Size = new System.Drawing.Size(93, 21);
            this.cmdShowAppLog.TabIndex = 16;
            this.cmdShowAppLog.Text = "Show App Log";
            this.cmdShowAppLog.UseVisualStyleBackColor = true;
            this.cmdShowAppLog.Click += new System.EventHandler(this.cmdShowAppLog_Click);
            // 
            // optOutputMessagesToTextLogFile
            // 
            this.optOutputMessagesToTextLogFile.AutoSize = true;
            this.optOutputMessagesToTextLogFile.Location = new System.Drawing.Point(28, 286);
            this.optOutputMessagesToTextLogFile.Name = "optOutputMessagesToTextLogFile";
            this.optOutputMessagesToTextLogFile.Size = new System.Drawing.Size(184, 17);
            this.optOutputMessagesToTextLogFile.TabIndex = 17;
            this.optOutputMessagesToTextLogFile.TabStop = true;
            this.optOutputMessagesToTextLogFile.Text = "Output Messages to Text Log File";
            this.optOutputMessagesToTextLogFile.UseVisualStyleBackColor = true;
            // 
            // txtTextLogFilePath
            // 
            this.txtTextLogFilePath.Location = new System.Drawing.Point(45, 310);
            this.txtTextLogFilePath.Name = "txtTextLogFilePath";
            this.txtTextLogFilePath.Size = new System.Drawing.Size(209, 20);
            this.txtTextLogFilePath.TabIndex = 18;
            // 
            // chkAppendMessagesIfFileExists
            // 
            this.chkAppendMessagesIfFileExists.AutoSize = true;
            this.chkAppendMessagesIfFileExists.Location = new System.Drawing.Point(10, 23);
            this.chkAppendMessagesIfFileExists.Name = "chkAppendMessagesIfFileExists";
            this.chkAppendMessagesIfFileExists.Size = new System.Drawing.Size(171, 17);
            this.chkAppendMessagesIfFileExists.TabIndex = 19;
            this.chkAppendMessagesIfFileExists.Text = "Append Messages if File Exists";
            this.chkAppendMessagesIfFileExists.UseVisualStyleBackColor = true;
            // 
            // chkShowApplicationNameWithEachMessage
            // 
            this.chkShowApplicationNameWithEachMessage.AutoSize = true;
            this.chkShowApplicationNameWithEachMessage.Location = new System.Drawing.Point(10, 69);
            this.chkShowApplicationNameWithEachMessage.Name = "chkShowApplicationNameWithEachMessage";
            this.chkShowApplicationNameWithEachMessage.Size = new System.Drawing.Size(235, 17);
            this.chkShowApplicationNameWithEachMessage.TabIndex = 20;
            this.chkShowApplicationNameWithEachMessage.Text = "Show Application Name with Each Message";
            this.chkShowApplicationNameWithEachMessage.UseVisualStyleBackColor = true;
            // 
            // chkShowMessageTypeWithEachMessage
            // 
            this.chkShowMessageTypeWithEachMessage.AutoSize = true;
            this.chkShowMessageTypeWithEachMessage.Location = new System.Drawing.Point(10, 46);
            this.chkShowMessageTypeWithEachMessage.Name = "chkShowMessageTypeWithEachMessage";
            this.chkShowMessageTypeWithEachMessage.Size = new System.Drawing.Size(222, 17);
            this.chkShowMessageTypeWithEachMessage.TabIndex = 21;
            this.chkShowMessageTypeWithEachMessage.Text = "Show Message Type with Each Message";
            this.chkShowMessageTypeWithEachMessage.UseVisualStyleBackColor = true;
            // 
            // chkShowMachineNameWithEachMessage
            // 
            this.chkShowMachineNameWithEachMessage.AutoSize = true;
            this.chkShowMachineNameWithEachMessage.Location = new System.Drawing.Point(10, 93);
            this.chkShowMachineNameWithEachMessage.Name = "chkShowMachineNameWithEachMessage";
            this.chkShowMachineNameWithEachMessage.Size = new System.Drawing.Size(224, 17);
            this.chkShowMachineNameWithEachMessage.TabIndex = 22;
            this.chkShowMachineNameWithEachMessage.Text = "Show Machine Name with Each Message";
            this.chkShowMachineNameWithEachMessage.UseVisualStyleBackColor = true;
            // 
            // grpTextLogFileParameters
            // 
            this.grpTextLogFileParameters.Controls.Add(this.chkShowMachineNameWithEachMessage);
            this.grpTextLogFileParameters.Controls.Add(this.chkAppendMessagesIfFileExists);
            this.grpTextLogFileParameters.Controls.Add(this.chkShowMessageTypeWithEachMessage);
            this.grpTextLogFileParameters.Controls.Add(this.chkShowApplicationNameWithEachMessage);
            this.grpTextLogFileParameters.Location = new System.Drawing.Point(45, 336);
            this.grpTextLogFileParameters.Name = "grpTextLogFileParameters";
            this.grpTextLogFileParameters.Size = new System.Drawing.Size(247, 126);
            this.grpTextLogFileParameters.TabIndex = 23;
            this.grpTextLogFileParameters.TabStop = false;
            this.grpTextLogFileParameters.Text = "Text Log File Parameters";
            // 
            // optOutputMessagesToWindowsApplicationEventLog
            // 
            this.optOutputMessagesToWindowsApplicationEventLog.AutoSize = true;
            this.optOutputMessagesToWindowsApplicationEventLog.Location = new System.Drawing.Point(301, 286);
            this.optOutputMessagesToWindowsApplicationEventLog.Name = "optOutputMessagesToWindowsApplicationEventLog";
            this.optOutputMessagesToWindowsApplicationEventLog.Size = new System.Drawing.Size(274, 17);
            this.optOutputMessagesToWindowsApplicationEventLog.TabIndex = 24;
            this.optOutputMessagesToWindowsApplicationEventLog.TabStop = true;
            this.optOutputMessagesToWindowsApplicationEventLog.Text = "Output Messages to Windows Application Event Log";
            this.optOutputMessagesToWindowsApplicationEventLog.UseVisualStyleBackColor = true;
            // 
            // grpEventLogParameters
            // 
            this.grpEventLogParameters.Controls.Add(this.cmdDeleteEventSource);
            this.grpEventLogParameters.Controls.Add(this.txtNumErrorMessagesToWrite);
            this.grpEventLogParameters.Controls.Add(this.lblNumErrorMessagesToWrite);
            this.grpEventLogParameters.Controls.Add(this.txtNumWarningMessagesToWrite);
            this.grpEventLogParameters.Controls.Add(this.lblNumWarningMessagesToWrite);
            this.grpEventLogParameters.Controls.Add(this.txtNumInformationMessagesToWrite);
            this.grpEventLogParameters.Controls.Add(this.lblNumInformationMessagesToWrite);
            this.grpEventLogParameters.Controls.Add(this.lblNumberOfMessagesToWrite);
            this.grpEventLogParameters.Controls.Add(this.txtEventSource);
            this.grpEventLogParameters.Controls.Add(this.lblEventSource);
            this.grpEventLogParameters.Location = new System.Drawing.Point(321, 310);
            this.grpEventLogParameters.Name = "grpEventLogParameters";
            this.grpEventLogParameters.Size = new System.Drawing.Size(254, 151);
            this.grpEventLogParameters.TabIndex = 25;
            this.grpEventLogParameters.TabStop = false;
            this.grpEventLogParameters.Text = "Event Log Parameters";
            // 
            // cmdDeleteEventSource
            // 
            this.cmdDeleteEventSource.Location = new System.Drawing.Point(100, 20);
            this.cmdDeleteEventSource.Name = "cmdDeleteEventSource";
            this.cmdDeleteEventSource.Size = new System.Drawing.Size(128, 23);
            this.cmdDeleteEventSource.TabIndex = 27;
            this.cmdDeleteEventSource.Text = "Delete Event Source";
            this.cmdDeleteEventSource.UseVisualStyleBackColor = true;
            this.cmdDeleteEventSource.Click += new System.EventHandler(this.cmdDeleteEventSource_Click);
            // 
            // txtNumErrorMessagesToWrite
            // 
            this.txtNumErrorMessagesToWrite.Location = new System.Drawing.Point(180, 118);
            this.txtNumErrorMessagesToWrite.Name = "txtNumErrorMessagesToWrite";
            this.txtNumErrorMessagesToWrite.Size = new System.Drawing.Size(57, 20);
            this.txtNumErrorMessagesToWrite.TabIndex = 8;
            // 
            // lblNumErrorMessagesToWrite
            // 
            this.lblNumErrorMessagesToWrite.AutoSize = true;
            this.lblNumErrorMessagesToWrite.Location = new System.Drawing.Point(195, 97);
            this.lblNumErrorMessagesToWrite.Name = "lblNumErrorMessagesToWrite";
            this.lblNumErrorMessagesToWrite.Size = new System.Drawing.Size(29, 13);
            this.lblNumErrorMessagesToWrite.TabIndex = 7;
            this.lblNumErrorMessagesToWrite.Text = "Error";
            // 
            // txtNumWarningMessagesToWrite
            // 
            this.txtNumWarningMessagesToWrite.Location = new System.Drawing.Point(100, 118);
            this.txtNumWarningMessagesToWrite.Name = "txtNumWarningMessagesToWrite";
            this.txtNumWarningMessagesToWrite.Size = new System.Drawing.Size(56, 20);
            this.txtNumWarningMessagesToWrite.TabIndex = 6;
            // 
            // lblNumWarningMessagesToWrite
            // 
            this.lblNumWarningMessagesToWrite.AutoSize = true;
            this.lblNumWarningMessagesToWrite.Location = new System.Drawing.Point(103, 97);
            this.lblNumWarningMessagesToWrite.Name = "lblNumWarningMessagesToWrite";
            this.lblNumWarningMessagesToWrite.Size = new System.Drawing.Size(47, 13);
            this.lblNumWarningMessagesToWrite.TabIndex = 5;
            this.lblNumWarningMessagesToWrite.Text = "Warning";
            // 
            // txtNumInformationMessagesToWrite
            // 
            this.txtNumInformationMessagesToWrite.Location = new System.Drawing.Point(21, 118);
            this.txtNumInformationMessagesToWrite.Name = "txtNumInformationMessagesToWrite";
            this.txtNumInformationMessagesToWrite.Size = new System.Drawing.Size(56, 20);
            this.txtNumInformationMessagesToWrite.TabIndex = 4;
            // 
            // lblNumInformationMessagesToWrite
            // 
            this.lblNumInformationMessagesToWrite.AutoSize = true;
            this.lblNumInformationMessagesToWrite.Location = new System.Drawing.Point(18, 97);
            this.lblNumInformationMessagesToWrite.Name = "lblNumInformationMessagesToWrite";
            this.lblNumInformationMessagesToWrite.Size = new System.Drawing.Size(59, 13);
            this.lblNumInformationMessagesToWrite.TabIndex = 3;
            this.lblNumInformationMessagesToWrite.Text = "Information";
            // 
            // lblNumberOfMessagesToWrite
            // 
            this.lblNumberOfMessagesToWrite.AutoSize = true;
            this.lblNumberOfMessagesToWrite.Location = new System.Drawing.Point(18, 75);
            this.lblNumberOfMessagesToWrite.Name = "lblNumberOfMessagesToWrite";
            this.lblNumberOfMessagesToWrite.Size = new System.Drawing.Size(150, 13);
            this.lblNumberOfMessagesToWrite.TabIndex = 2;
            this.lblNumberOfMessagesToWrite.Text = "Number of Messages to Write:";
            // 
            // txtEventSource
            // 
            this.txtEventSource.Location = new System.Drawing.Point(21, 45);
            this.txtEventSource.Name = "txtEventSource";
            this.txtEventSource.Size = new System.Drawing.Size(210, 20);
            this.txtEventSource.TabIndex = 1;
            // 
            // lblEventSource
            // 
            this.lblEventSource.AutoSize = true;
            this.lblEventSource.Location = new System.Drawing.Point(18, 25);
            this.lblEventSource.Name = "lblEventSource";
            this.lblEventSource.Size = new System.Drawing.Size(72, 13);
            this.lblEventSource.TabIndex = 0;
            this.lblEventSource.Text = "Event Source";
            // 
            // panelCommandButtons
            // 
            this.panelCommandButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCommandButtons.Controls.Add(this.cmdRunTest);
            this.panelCommandButtons.Controls.Add(this.cmdResetForm);
            this.panelCommandButtons.Controls.Add(this.cmdExit);
            this.panelCommandButtons.Location = new System.Drawing.Point(436, 34);
            this.panelCommandButtons.Name = "panelCommandButtons";
            this.panelCommandButtons.Size = new System.Drawing.Size(135, 212);
            this.panelCommandButtons.TabIndex = 26;
            // 
            // MainForm
            // 
            this.AcceptButton = this.cmdRunTest;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(599, 473);
            this.Controls.Add(this.panelCommandButtons);
            this.Controls.Add(this.grpEventLogParameters);
            this.Controls.Add(this.optOutputMessagesToWindowsApplicationEventLog);
            this.Controls.Add(this.grpTextLogFileParameters);
            this.Controls.Add(this.txtTextLogFilePath);
            this.Controls.Add(this.optOutputMessagesToTextLogFile);
            this.Controls.Add(this.cmdShowAppLog);
            this.Controls.Add(this.cmdHideAppLog);
            this.Controls.Add(this.grpMessageDefinition);
            this.Controls.Add(this.optRunTestprogApplicationMessageLogTest);
            this.Controls.Add(this.cmdShowDLLMessageLog);
            this.Controls.Add(this.cmdHideDLLMessageLog);
            this.Controls.Add(this.optRunGetSumFromDLL);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.grpMessageDefinition.ResumeLayout(false);
            this.grpMessageDefinition.PerformLayout();
            this.grpTextLogFileParameters.ResumeLayout(false);
            this.grpTextLogFileParameters.PerformLayout();
            this.grpEventLogParameters.ResumeLayout(false);
            this.grpEventLogParameters.PerformLayout();
            this.panelCommandButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdRunTest;
        private System.Windows.Forms.Button cmdResetForm;
        private System.Windows.Forms.ToolTip mainFormToolTips;
        private System.Windows.Forms.RadioButton optRunGetSumFromDLL;
        private System.Windows.Forms.Label lblMaxNumGetSum;
        private System.Windows.Forms.Label lblMinNumGetSum;
        private System.Windows.Forms.TextBox txtMinNumGetSum;
        private System.Windows.Forms.TextBox txtMaxNumGetSum;
        private System.Windows.Forms.Label lblOutputInterval;
        private System.Windows.Forms.CheckBox chkShowDateTimeInOutput;
        private System.Windows.Forms.TextBox txtOutputInterval;
        private System.Windows.Forms.Button cmdHideDLLMessageLog;
        private System.Windows.Forms.Button cmdShowDLLMessageLog;
        private System.Windows.Forms.RadioButton optRunTestprogApplicationMessageLogTest;
        private System.Windows.Forms.GroupBox grpMessageDefinition;
        private System.Windows.Forms.Button cmdHideAppLog;
        private System.Windows.Forms.Button cmdShowAppLog;
        private System.Windows.Forms.RadioButton optOutputMessagesToTextLogFile;
        private System.Windows.Forms.TextBox txtTextLogFilePath;
        private System.Windows.Forms.CheckBox chkAppendMessagesIfFileExists;
        private System.Windows.Forms.CheckBox chkShowApplicationNameWithEachMessage;
        private System.Windows.Forms.CheckBox chkShowMessageTypeWithEachMessage;
        private System.Windows.Forms.CheckBox chkShowMachineNameWithEachMessage;
        private System.Windows.Forms.GroupBox grpTextLogFileParameters;
        private System.Windows.Forms.RadioButton optOutputMessagesToWindowsApplicationEventLog;
        private System.Windows.Forms.GroupBox grpEventLogParameters;
        private System.Windows.Forms.Label lblNumberOfMessagesToWrite;
        private System.Windows.Forms.TextBox txtEventSource;
        private System.Windows.Forms.Label lblEventSource;
        private System.Windows.Forms.Label lblNumInformationMessagesToWrite;
        private System.Windows.Forms.TextBox txtNumInformationMessagesToWrite;
        private System.Windows.Forms.Label lblNumWarningMessagesToWrite;
        private System.Windows.Forms.TextBox txtNumErrorMessagesToWrite;
        private System.Windows.Forms.Label lblNumErrorMessagesToWrite;
        private System.Windows.Forms.TextBox txtNumWarningMessagesToWrite;
        private System.Windows.Forms.Panel panelCommandButtons;
        private System.Windows.Forms.Button cmdDeleteEventSource;
    }
}

