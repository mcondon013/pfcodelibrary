namespace TestprogProcessObjects
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
            this.cmdRunTest = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpContents = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHelpTutorial = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHelpContact = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.chkRedirectStandardError = new System.Windows.Forms.CheckBox();
            this.chkRedirectStandardOutput = new System.Windows.Forms.CheckBox();
            this.chkUseShellExecute = new System.Windows.Forms.CheckBox();
            this.chkCreateNoWindow = new System.Windows.Forms.CheckBox();
            this.cboWindowStyle = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtArguments = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmdGetWorkingDirectory = new System.Windows.Forms.Button();
            this.txtWorkingDirectory = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmdGetFileToRun = new System.Windows.Forms.Button();
            this.txtFileToRun = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkCheckIfWaitingForInput = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaxRunsecs = new System.Windows.Forms.TextBox();
            this.chkRedirectStandardInput = new System.Windows.Forms.CheckBox();
            this.chkUseCmdExe = new System.Windows.Forms.CheckBox();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(633, 303);
            this.cmdExit.Name = "cmdExit";
            this.appHelpProvider.SetShowHelp(this.cmdExit, true);
            this.cmdExit.Size = new System.Drawing.Size(93, 37);
            this.cmdExit.TabIndex = 2;
            this.cmdExit.Text = "E&xit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdRunTest
            // 
            this.cmdRunTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.appHelpProvider.SetHelpKeyword(this.cmdRunTest, "Run Tests");
            this.appHelpProvider.SetHelpNavigator(this.cmdRunTest, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.appHelpProvider.SetHelpString(this.cmdRunTest, "Help for Run Tests: See Help File.");
            this.cmdRunTest.Location = new System.Drawing.Point(633, 60);
            this.cmdRunTest.Name = "cmdRunTest";
            this.appHelpProvider.SetShowHelp(this.cmdRunTest, true);
            this.cmdRunTest.Size = new System.Drawing.Size(93, 37);
            this.cmdRunTest.TabIndex = 1;
            this.cmdRunTest.Text = "&Run Test";
            this.cmdRunTest.UseVisualStyleBackColor = true;
            this.cmdRunTest.Click += new System.EventHandler(this.cmdRunTest_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuTools,
            this.mnuHelp});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(761, 24);
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
            this.mnuFileExit.Size = new System.Drawing.Size(92, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsOptions});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(48, 20);
            this.mnuTools.Text = "&Tools";
            // 
            // mnuToolsOptions
            // 
            this.mnuToolsOptions.Name = "mnuToolsOptions";
            this.mnuToolsOptions.Size = new System.Drawing.Size(116, 22);
            this.mnuToolsOptions.Text = "&Options";
            this.mnuToolsOptions.Click += new System.EventHandler(this.mnuToolsOptions_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpContents,
            this.mnuHelpIndex,
            this.mnuHelpSearch,
            this.toolStripSeparator2,
            this.mnuHelpTutorial,
            this.toolStripSeparator3,
            this.mnuHelpContact,
            this.toolStripSeparator4,
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpContents
            // 
            this.mnuHelpContents.Name = "mnuHelpContents";
            this.mnuHelpContents.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpContents.Text = "Contents";
            this.mnuHelpContents.Click += new System.EventHandler(this.mnuHelpContents_Click);
            // 
            // mnuHelpIndex
            // 
            this.mnuHelpIndex.Name = "mnuHelpIndex";
            this.mnuHelpIndex.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpIndex.Text = "Index";
            this.mnuHelpIndex.Click += new System.EventHandler(this.mnuHelpIndex_Click);
            // 
            // mnuHelpSearch
            // 
            this.mnuHelpSearch.Name = "mnuHelpSearch";
            this.mnuHelpSearch.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpSearch.Text = "Search";
            this.mnuHelpSearch.Click += new System.EventHandler(this.mnuHelpSearch_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(219, 6);
            // 
            // mnuHelpTutorial
            // 
            this.mnuHelpTutorial.Name = "mnuHelpTutorial";
            this.mnuHelpTutorial.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpTutorial.Text = "Tutorial";
            this.mnuHelpTutorial.Click += new System.EventHandler(this.mnuHelpTutorial_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(219, 6);
            // 
            // mnuHelpContact
            // 
            this.mnuHelpContact.Name = "mnuHelpContact";
            this.mnuHelpContact.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpContact.Text = "Contact ProFast Computing";
            this.mnuHelpContact.Click += new System.EventHandler(this.mnuHelpContact_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(219, 6);
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpAbout.Text = "&About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(31, 323);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 8;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogProcessObjects\\TestprogProcessObje" +
    "cts\\InitWinFormsHelpFile.chm";
            // 
            // chkRedirectStandardError
            // 
            this.chkRedirectStandardError.AutoSize = true;
            this.chkRedirectStandardError.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRedirectStandardError.Location = new System.Drawing.Point(479, 211);
            this.chkRedirectStandardError.Name = "chkRedirectStandardError";
            this.chkRedirectStandardError.Size = new System.Drawing.Size(97, 17);
            this.chkRedirectStandardError.TabIndex = 38;
            this.chkRedirectStandardError.Text = "Redirect Stderr";
            this.chkRedirectStandardError.UseVisualStyleBackColor = true;
            // 
            // chkRedirectStandardOutput
            // 
            this.chkRedirectStandardOutput.AutoSize = true;
            this.chkRedirectStandardOutput.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRedirectStandardOutput.Location = new System.Drawing.Point(477, 188);
            this.chkRedirectStandardOutput.Name = "chkRedirectStandardOutput";
            this.chkRedirectStandardOutput.Size = new System.Drawing.Size(100, 17);
            this.chkRedirectStandardOutput.TabIndex = 37;
            this.chkRedirectStandardOutput.Text = "Redirect Stdout";
            this.chkRedirectStandardOutput.UseVisualStyleBackColor = true;
            // 
            // chkUseShellExecute
            // 
            this.chkUseShellExecute.AutoSize = true;
            this.chkUseShellExecute.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUseShellExecute.Location = new System.Drawing.Point(470, 165);
            this.chkUseShellExecute.Name = "chkUseShellExecute";
            this.chkUseShellExecute.Size = new System.Drawing.Size(107, 17);
            this.chkUseShellExecute.TabIndex = 36;
            this.chkUseShellExecute.Text = "UseShellExecute";
            this.chkUseShellExecute.UseVisualStyleBackColor = true;
            // 
            // chkCreateNoWindow
            // 
            this.chkCreateNoWindow.AutoSize = true;
            this.chkCreateNoWindow.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCreateNoWindow.Location = new System.Drawing.Point(467, 142);
            this.chkCreateNoWindow.Name = "chkCreateNoWindow";
            this.chkCreateNoWindow.Size = new System.Drawing.Size(110, 17);
            this.chkCreateNoWindow.TabIndex = 35;
            this.chkCreateNoWindow.Text = "CreateNoWindow";
            this.chkCreateNoWindow.UseVisualStyleBackColor = true;
            // 
            // cboWindowStyle
            // 
            this.cboWindowStyle.FormattingEnabled = true;
            this.cboWindowStyle.Location = new System.Drawing.Point(482, 115);
            this.cboWindowStyle.Name = "cboWindowStyle";
            this.cboWindowStyle.Size = new System.Drawing.Size(95, 21);
            this.cboWindowStyle.TabIndex = 34;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(393, 115);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "Window Style:";
            // 
            // txtArguments
            // 
            this.txtArguments.Location = new System.Drawing.Point(119, 115);
            this.txtArguments.Multiline = true;
            this.txtArguments.Name = "txtArguments";
            this.txtArguments.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtArguments.Size = new System.Drawing.Size(253, 154);
            this.txtArguments.TabIndex = 32;
            this.txtArguments.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtArguments_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 115);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "Arguments:";
            // 
            // cmdGetWorkingDirectory
            // 
            this.cmdGetWorkingDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetWorkingDirectory.Location = new System.Drawing.Point(539, 92);
            this.cmdGetWorkingDirectory.Name = "cmdGetWorkingDirectory";
            this.cmdGetWorkingDirectory.Size = new System.Drawing.Size(38, 20);
            this.cmdGetWorkingDirectory.TabIndex = 30;
            this.cmdGetWorkingDirectory.Text = "•••";
            this.cmdGetWorkingDirectory.UseVisualStyleBackColor = true;
            this.cmdGetWorkingDirectory.Click += new System.EventHandler(this.cmdGetWorkingDirectory_Click);
            // 
            // txtWorkingDirectory
            // 
            this.txtWorkingDirectory.Location = new System.Drawing.Point(119, 87);
            this.txtWorkingDirectory.Name = "txtWorkingDirectory";
            this.txtWorkingDirectory.Size = new System.Drawing.Size(403, 20);
            this.txtWorkingDirectory.TabIndex = 29;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Working Dir.:";
            // 
            // cmdGetFileToRun
            // 
            this.cmdGetFileToRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetFileToRun.Location = new System.Drawing.Point(539, 65);
            this.cmdGetFileToRun.Name = "cmdGetFileToRun";
            this.cmdGetFileToRun.Size = new System.Drawing.Size(38, 20);
            this.cmdGetFileToRun.TabIndex = 27;
            this.cmdGetFileToRun.Text = "•••";
            this.cmdGetFileToRun.UseVisualStyleBackColor = true;
            this.cmdGetFileToRun.Click += new System.EventHandler(this.cmdGetFileToRun_Click);
            // 
            // txtFileToRun
            // 
            this.txtFileToRun.Location = new System.Drawing.Point(119, 60);
            this.txtFileToRun.Name = "txtFileToRun";
            this.txtFileToRun.Size = new System.Drawing.Size(403, 20);
            this.txtFileToRun.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "FileToRun:";
            // 
            // chkCheckIfWaitingForInput
            // 
            this.chkCheckIfWaitingForInput.AutoSize = true;
            this.chkCheckIfWaitingForInput.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCheckIfWaitingForInput.Location = new System.Drawing.Point(430, 255);
            this.chkCheckIfWaitingForInput.Name = "chkCheckIfWaitingForInput";
            this.chkCheckIfWaitingForInput.Size = new System.Drawing.Size(146, 17);
            this.chkCheckIfWaitingForInput.TabIndex = 39;
            this.chkCheckIfWaitingForInput.Text = "Check if Waiting for Input";
            this.chkCheckIfWaitingForInput.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 275);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 41;
            this.label1.Text = "Max Runsecs:";
            // 
            // txtMaxRunsecs
            // 
            this.txtMaxRunsecs.Location = new System.Drawing.Point(119, 275);
            this.txtMaxRunsecs.Name = "txtMaxRunsecs";
            this.txtMaxRunsecs.Size = new System.Drawing.Size(92, 20);
            this.txtMaxRunsecs.TabIndex = 42;
            // 
            // chkRedirectStandardInput
            // 
            this.chkRedirectStandardInput.AutoSize = true;
            this.chkRedirectStandardInput.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRedirectStandardInput.Location = new System.Drawing.Point(484, 234);
            this.chkRedirectStandardInput.Name = "chkRedirectStandardInput";
            this.chkRedirectStandardInput.Size = new System.Drawing.Size(93, 17);
            this.chkRedirectStandardInput.TabIndex = 43;
            this.chkRedirectStandardInput.Text = "Redirect Stdin";
            this.chkRedirectStandardInput.UseVisualStyleBackColor = true;
            // 
            // chkUseCmdExe
            // 
            this.chkUseCmdExe.AutoSize = true;
            this.chkUseCmdExe.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUseCmdExe.Location = new System.Drawing.Point(238, 278);
            this.chkUseCmdExe.Name = "chkUseCmdExe";
            this.chkUseCmdExe.Size = new System.Drawing.Size(116, 17);
            this.chkUseCmdExe.TabIndex = 44;
            this.chkUseCmdExe.Text = "Use Command.Exe";
            this.chkUseCmdExe.UseVisualStyleBackColor = true;
            this.chkUseCmdExe.CheckedChanged += new System.EventHandler(this.chkUseCmdExe_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(761, 373);
            this.Controls.Add(this.chkUseCmdExe);
            this.Controls.Add(this.chkRedirectStandardInput);
            this.Controls.Add(this.txtMaxRunsecs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkCheckIfWaitingForInput);
            this.Controls.Add(this.chkRedirectStandardError);
            this.Controls.Add(this.chkRedirectStandardOutput);
            this.Controls.Add(this.chkUseShellExecute);
            this.Controls.Add(this.chkCreateNoWindow);
            this.Controls.Add(this.cboWindowStyle);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtArguments);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmdGetWorkingDirectory);
            this.Controls.Add(this.txtWorkingDirectory);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmdGetFileToRun);
            this.Controls.Add(this.txtFileToRun);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chkEraseOutputBeforeEachTest);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.cmdRunTest);
            this.Controls.Add(this.cmdExit);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Form";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdRunTest;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpContents;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpIndex;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpTutorial;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpContact;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        internal System.Windows.Forms.CheckBox chkRedirectStandardError;
        internal System.Windows.Forms.CheckBox chkRedirectStandardOutput;
        internal System.Windows.Forms.CheckBox chkUseShellExecute;
        internal System.Windows.Forms.CheckBox chkCreateNoWindow;
        internal System.Windows.Forms.ComboBox cboWindowStyle;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.TextBox txtArguments;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button cmdGetWorkingDirectory;
        internal System.Windows.Forms.TextBox txtWorkingDirectory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button cmdGetFileToRun;
        internal System.Windows.Forms.TextBox txtFileToRun;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.CheckBox chkCheckIfWaitingForInput;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox txtMaxRunsecs;
        internal System.Windows.Forms.CheckBox chkRedirectStandardInput;
        private System.Windows.Forms.CheckBox chkUseCmdExe;
    }
}

