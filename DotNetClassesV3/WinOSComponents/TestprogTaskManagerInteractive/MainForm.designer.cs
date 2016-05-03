namespace TestprogTaskManagerInteractive
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
            this.cmdRunTests = new System.Windows.Forms.Button();
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
            this.chkGetTaskHistory = new System.Windows.Forms.CheckBox();
            this.grpTestsToRun = new System.Windows.Forms.GroupBox();
            this.chkShowScheduleDetail = new System.Windows.Forms.CheckBox();
            this.chkGetScheduleList = new System.Windows.Forms.CheckBox();
            this.chkGetTaskList = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTaskName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboTaskType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTaskDescription = new System.Windows.Forms.TextBox();
            this.txtTaskSchedule = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMaxHistoryEntries = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFileToRun = new System.Windows.Forms.TextBox();
            this.cmdGetSchedule = new System.Windows.Forms.Button();
            this.cmdGetFileToRun = new System.Windows.Forms.Button();
            this.mainFormToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.txtWorkingDirectory = new System.Windows.Forms.TextBox();
            this.cmdGetWorkingDirectory = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtArguments = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboWindowStyle = new System.Windows.Forms.ComboBox();
            this.chkCreateNoWindow = new System.Windows.Forms.CheckBox();
            this.chkUseShellExecute = new System.Windows.Forms.CheckBox();
            this.chkRedirectStandardOutput = new System.Windows.Forms.CheckBox();
            this.chkRedirectStandardError = new System.Windows.Forms.CheckBox();
            this.cmdGetTask = new System.Windows.Forms.Button();
            this.cmdSaveTask = new System.Windows.Forms.Button();
            this.cmdRunTask = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optUseDatabase = new System.Windows.Forms.RadioButton();
            this.optUseXmlFiles = new System.Windows.Forms.RadioButton();
            this.cmdReinit = new System.Windows.Forms.Button();
            this.MainMenu.SuspendLayout();
            this.grpTestsToRun.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(635, 470);
            this.cmdExit.Name = "cmdExit";
            this.appHelpProvider.SetShowHelp(this.cmdExit, true);
            this.cmdExit.Size = new System.Drawing.Size(93, 41);
            this.cmdExit.TabIndex = 33;
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
            this.cmdRunTests.Location = new System.Drawing.Point(635, 353);
            this.cmdRunTests.Name = "cmdRunTests";
            this.appHelpProvider.SetShowHelp(this.cmdRunTests, true);
            this.cmdRunTests.Size = new System.Drawing.Size(93, 41);
            this.cmdRunTests.TabIndex = 32;
            this.cmdRunTests.Text = "&Run Tests";
            this.cmdRunTests.UseVisualStyleBackColor = true;
            this.cmdRunTests.Click += new System.EventHandler(this.cmdRunTest_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuTools,
            this.mnuHelp});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(763, 24);
            this.MainMenu.TabIndex = 34;
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
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(39, 489);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 26;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogTaskManagerInteractive\\InitWinForm" +
    "sAppWithExtendedOptions\\InitWinFormsHelpFile.chm";
            // 
            // chkGetTaskHistory
            // 
            this.chkGetTaskHistory.AutoSize = true;
            this.chkGetTaskHistory.Location = new System.Drawing.Point(17, 33);
            this.chkGetTaskHistory.Name = "chkGetTaskHistory";
            this.chkGetTaskHistory.Size = new System.Drawing.Size(114, 17);
            this.chkGetTaskHistory.TabIndex = 0;
            this.chkGetTaskHistory.Text = "&1 Get Task History";
            this.chkGetTaskHistory.UseVisualStyleBackColor = true;
            // 
            // grpTestsToRun
            // 
            this.grpTestsToRun.Controls.Add(this.chkShowScheduleDetail);
            this.grpTestsToRun.Controls.Add(this.chkGetScheduleList);
            this.grpTestsToRun.Controls.Add(this.chkGetTaskList);
            this.grpTestsToRun.Controls.Add(this.chkGetTaskHistory);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 353);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(546, 101);
            this.grpTestsToRun.TabIndex = 25;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // chkShowScheduleDetail
            // 
            this.chkShowScheduleDetail.AutoSize = true;
            this.chkShowScheduleDetail.Location = new System.Drawing.Point(168, 33);
            this.chkShowScheduleDetail.Name = "chkShowScheduleDetail";
            this.chkShowScheduleDetail.Size = new System.Drawing.Size(140, 17);
            this.chkShowScheduleDetail.TabIndex = 1;
            this.chkShowScheduleDetail.Text = "&3 Show Schedule Detail";
            this.chkShowScheduleDetail.UseVisualStyleBackColor = true;
            // 
            // chkGetScheduleList
            // 
            this.chkGetScheduleList.AutoSize = true;
            this.chkGetScheduleList.Location = new System.Drawing.Point(168, 66);
            this.chkGetScheduleList.Name = "chkGetScheduleList";
            this.chkGetScheduleList.Size = new System.Drawing.Size(119, 17);
            this.chkGetScheduleList.TabIndex = 3;
            this.chkGetScheduleList.Text = "&4 Get Schedule List";
            this.chkGetScheduleList.UseVisualStyleBackColor = true;
            // 
            // chkGetTaskList
            // 
            this.chkGetTaskList.AutoSize = true;
            this.chkGetTaskList.Location = new System.Drawing.Point(17, 66);
            this.chkGetTaskList.Name = "chkGetTaskList";
            this.chkGetTaskList.Size = new System.Drawing.Size(98, 17);
            this.chkGetTaskList.TabIndex = 2;
            this.chkGetTaskList.Text = "&2 Get Task List";
            this.chkGetTaskList.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Task Name:";
            // 
            // txtTaskName
            // 
            this.txtTaskName.Location = new System.Drawing.Point(127, 60);
            this.txtTaskName.Name = "txtTaskName";
            this.txtTaskName.Size = new System.Drawing.Size(458, 20);
            this.txtTaskName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Task Type:";
            // 
            // cboTaskType
            // 
            this.cboTaskType.FormattingEnabled = true;
            this.cboTaskType.Location = new System.Drawing.Point(127, 134);
            this.cboTaskType.Name = "cboTaskType";
            this.cboTaskType.Size = new System.Drawing.Size(253, 21);
            this.cboTaskType.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Task Schedule:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Task Descr.:";
            // 
            // txtTaskDescription
            // 
            this.txtTaskDescription.Location = new System.Drawing.Point(127, 86);
            this.txtTaskDescription.Name = "txtTaskDescription";
            this.txtTaskDescription.Size = new System.Drawing.Size(458, 20);
            this.txtTaskDescription.TabIndex = 3;
            // 
            // txtTaskSchedule
            // 
            this.txtTaskSchedule.Location = new System.Drawing.Point(127, 110);
            this.txtTaskSchedule.Name = "txtTaskSchedule";
            this.txtTaskSchedule.Size = new System.Drawing.Size(403, 20);
            this.txtTaskSchedule.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(386, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Max History Entries:";
            // 
            // txtMaxHistoryEntries
            // 
            this.txtMaxHistoryEntries.Location = new System.Drawing.Point(490, 134);
            this.txtMaxHistoryEntries.Name = "txtMaxHistoryEntries";
            this.txtMaxHistoryEntries.Size = new System.Drawing.Size(95, 20);
            this.txtMaxHistoryEntries.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "FileToRun:";
            // 
            // txtFileToRun
            // 
            this.txtFileToRun.Location = new System.Drawing.Point(127, 165);
            this.txtFileToRun.Name = "txtFileToRun";
            this.txtFileToRun.Size = new System.Drawing.Size(403, 20);
            this.txtFileToRun.TabIndex = 12;
            // 
            // cmdGetSchedule
            // 
            this.cmdGetSchedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetSchedule.Location = new System.Drawing.Point(547, 110);
            this.cmdGetSchedule.Name = "cmdGetSchedule";
            this.cmdGetSchedule.Size = new System.Drawing.Size(38, 20);
            this.cmdGetSchedule.TabIndex = 6;
            this.cmdGetSchedule.Text = "•••";
            this.mainFormToolTips.SetToolTip(this.cmdGetSchedule, "Get Schedule from storage");
            this.cmdGetSchedule.UseVisualStyleBackColor = true;
            this.cmdGetSchedule.Click += new System.EventHandler(this.cmdGetSchedule_Click);
            // 
            // cmdGetFileToRun
            // 
            this.cmdGetFileToRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetFileToRun.Location = new System.Drawing.Point(547, 170);
            this.cmdGetFileToRun.Name = "cmdGetFileToRun";
            this.cmdGetFileToRun.Size = new System.Drawing.Size(38, 20);
            this.cmdGetFileToRun.TabIndex = 13;
            this.cmdGetFileToRun.Text = "•••";
            this.mainFormToolTips.SetToolTip(this.cmdGetFileToRun, "Select file to run");
            this.cmdGetFileToRun.UseVisualStyleBackColor = true;
            this.cmdGetFileToRun.Click += new System.EventHandler(this.cmdGetFileToRun_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Working Dir.:";
            // 
            // txtWorkingDirectory
            // 
            this.txtWorkingDirectory.Location = new System.Drawing.Point(127, 192);
            this.txtWorkingDirectory.Name = "txtWorkingDirectory";
            this.txtWorkingDirectory.Size = new System.Drawing.Size(403, 20);
            this.txtWorkingDirectory.TabIndex = 15;
            // 
            // cmdGetWorkingDirectory
            // 
            this.cmdGetWorkingDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetWorkingDirectory.Location = new System.Drawing.Point(547, 197);
            this.cmdGetWorkingDirectory.Name = "cmdGetWorkingDirectory";
            this.cmdGetWorkingDirectory.Size = new System.Drawing.Size(38, 20);
            this.cmdGetWorkingDirectory.TabIndex = 16;
            this.cmdGetWorkingDirectory.Text = "•••";
            this.cmdGetWorkingDirectory.UseVisualStyleBackColor = true;
            this.cmdGetWorkingDirectory.Click += new System.EventHandler(this.cmdGetWorkingDirectory_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(35, 220);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Arguments:";
            // 
            // txtArguments
            // 
            this.txtArguments.Location = new System.Drawing.Point(127, 220);
            this.txtArguments.Multiline = true;
            this.txtArguments.Name = "txtArguments";
            this.txtArguments.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtArguments.Size = new System.Drawing.Size(253, 113);
            this.txtArguments.TabIndex = 18;
            this.txtArguments.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtArguments_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(401, 220);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Window Style:";
            // 
            // cboWindowStyle
            // 
            this.cboWindowStyle.FormattingEnabled = true;
            this.cboWindowStyle.Location = new System.Drawing.Point(490, 220);
            this.cboWindowStyle.Name = "cboWindowStyle";
            this.cboWindowStyle.Size = new System.Drawing.Size(95, 21);
            this.cboWindowStyle.TabIndex = 20;
            // 
            // chkCreateNoWindow
            // 
            this.chkCreateNoWindow.AutoSize = true;
            this.chkCreateNoWindow.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCreateNoWindow.Location = new System.Drawing.Point(475, 247);
            this.chkCreateNoWindow.Name = "chkCreateNoWindow";
            this.chkCreateNoWindow.Size = new System.Drawing.Size(110, 17);
            this.chkCreateNoWindow.TabIndex = 21;
            this.chkCreateNoWindow.Text = "CreateNoWindow";
            this.chkCreateNoWindow.UseVisualStyleBackColor = true;
            // 
            // chkUseShellExecute
            // 
            this.chkUseShellExecute.AutoSize = true;
            this.chkUseShellExecute.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUseShellExecute.Location = new System.Drawing.Point(478, 270);
            this.chkUseShellExecute.Name = "chkUseShellExecute";
            this.chkUseShellExecute.Size = new System.Drawing.Size(107, 17);
            this.chkUseShellExecute.TabIndex = 22;
            this.chkUseShellExecute.Text = "UseShellExecute";
            this.chkUseShellExecute.UseVisualStyleBackColor = true;
            // 
            // chkRedirectStandardOutput
            // 
            this.chkRedirectStandardOutput.AutoSize = true;
            this.chkRedirectStandardOutput.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRedirectStandardOutput.Location = new System.Drawing.Point(485, 293);
            this.chkRedirectStandardOutput.Name = "chkRedirectStandardOutput";
            this.chkRedirectStandardOutput.Size = new System.Drawing.Size(100, 17);
            this.chkRedirectStandardOutput.TabIndex = 23;
            this.chkRedirectStandardOutput.Text = "Redirect Stdout";
            this.chkRedirectStandardOutput.UseVisualStyleBackColor = true;
            // 
            // chkRedirectStandardError
            // 
            this.chkRedirectStandardError.AutoSize = true;
            this.chkRedirectStandardError.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRedirectStandardError.Location = new System.Drawing.Point(487, 316);
            this.chkRedirectStandardError.Name = "chkRedirectStandardError";
            this.chkRedirectStandardError.Size = new System.Drawing.Size(97, 17);
            this.chkRedirectStandardError.TabIndex = 24;
            this.chkRedirectStandardError.Text = "Redirect Stderr";
            this.chkRedirectStandardError.UseVisualStyleBackColor = true;
            // 
            // cmdGetTask
            // 
            this.cmdGetTask.Location = new System.Drawing.Point(635, 60);
            this.cmdGetTask.Name = "cmdGetTask";
            this.cmdGetTask.Size = new System.Drawing.Size(93, 41);
            this.cmdGetTask.TabIndex = 28;
            this.cmdGetTask.Text = "Get Task";
            this.cmdGetTask.UseVisualStyleBackColor = true;
            this.cmdGetTask.Click += new System.EventHandler(this.cmdGetTask_Click);
            // 
            // cmdSaveTask
            // 
            this.cmdSaveTask.Location = new System.Drawing.Point(635, 123);
            this.cmdSaveTask.Name = "cmdSaveTask";
            this.cmdSaveTask.Size = new System.Drawing.Size(93, 41);
            this.cmdSaveTask.TabIndex = 29;
            this.cmdSaveTask.Text = "Save Task";
            this.cmdSaveTask.UseVisualStyleBackColor = true;
            this.cmdSaveTask.Click += new System.EventHandler(this.cmdSaveTask_Click);
            // 
            // cmdRunTask
            // 
            this.cmdRunTask.Location = new System.Drawing.Point(635, 187);
            this.cmdRunTask.Name = "cmdRunTask";
            this.cmdRunTask.Size = new System.Drawing.Size(93, 41);
            this.cmdRunTask.TabIndex = 30;
            this.cmdRunTask.Text = "Run Task";
            this.cmdRunTask.UseVisualStyleBackColor = true;
            this.cmdRunTask.Click += new System.EventHandler(this.cmdRunTask_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optUseDatabase);
            this.groupBox1.Controls.Add(this.optUseXmlFiles);
            this.groupBox1.Location = new System.Drawing.Point(362, 470);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 43);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Storage Type";
            // 
            // optUseDatabase
            // 
            this.optUseDatabase.AutoSize = true;
            this.optUseDatabase.Location = new System.Drawing.Point(135, 19);
            this.optUseDatabase.Name = "optUseDatabase";
            this.optUseDatabase.Size = new System.Drawing.Size(71, 17);
            this.optUseDatabase.TabIndex = 1;
            this.optUseDatabase.TabStop = true;
            this.optUseDatabase.Text = "Database";
            this.optUseDatabase.UseVisualStyleBackColor = true;
            // 
            // optUseXmlFiles
            // 
            this.optUseXmlFiles.AutoSize = true;
            this.optUseXmlFiles.Location = new System.Drawing.Point(29, 19);
            this.optUseXmlFiles.Name = "optUseXmlFiles";
            this.optUseXmlFiles.Size = new System.Drawing.Size(71, 17);
            this.optUseXmlFiles.TabIndex = 0;
            this.optUseXmlFiles.TabStop = true;
            this.optUseXmlFiles.Text = "XML Files";
            this.optUseXmlFiles.UseVisualStyleBackColor = true;
            // 
            // cmdReinit
            // 
            this.cmdReinit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReinit.ForeColor = System.Drawing.SystemColors.Desktop;
            this.cmdReinit.Location = new System.Drawing.Point(635, 257);
            this.cmdReinit.Name = "cmdReinit";
            this.cmdReinit.Size = new System.Drawing.Size(93, 41);
            this.cmdReinit.TabIndex = 31;
            this.cmdReinit.Text = "Reinit";
            this.cmdReinit.UseVisualStyleBackColor = true;
            this.cmdReinit.Click += new System.EventHandler(this.cmdReinit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(763, 540);
            this.Controls.Add(this.cmdReinit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdRunTask);
            this.Controls.Add(this.cmdSaveTask);
            this.Controls.Add(this.cmdGetTask);
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
            this.Controls.Add(this.cmdGetSchedule);
            this.Controls.Add(this.txtFileToRun);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtMaxHistoryEntries);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTaskSchedule);
            this.Controls.Add(this.txtTaskDescription);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboTaskType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTaskName);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdRunTests;
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
        private System.Windows.Forms.CheckBox chkGetTaskHistory;
        private System.Windows.Forms.GroupBox grpTestsToRun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cmdGetSchedule;
        private System.Windows.Forms.ToolTip mainFormToolTips;
        private System.Windows.Forms.Button cmdGetFileToRun;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button cmdGetWorkingDirectory;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button cmdGetTask;
        private System.Windows.Forms.Button cmdSaveTask;
        private System.Windows.Forms.Button cmdRunTask;
        internal System.Windows.Forms.TextBox txtTaskDescription;
        internal System.Windows.Forms.TextBox txtTaskSchedule;
        internal System.Windows.Forms.TextBox txtMaxHistoryEntries;
        internal System.Windows.Forms.TextBox txtFileToRun;
        internal System.Windows.Forms.TextBox txtWorkingDirectory;
        internal System.Windows.Forms.TextBox txtArguments;
        internal System.Windows.Forms.ComboBox cboWindowStyle;
        internal System.Windows.Forms.CheckBox chkCreateNoWindow;
        internal System.Windows.Forms.CheckBox chkUseShellExecute;
        internal System.Windows.Forms.CheckBox chkRedirectStandardOutput;
        internal System.Windows.Forms.CheckBox chkRedirectStandardError;
        internal System.Windows.Forms.TextBox txtTaskName;
        internal System.Windows.Forms.ComboBox cboTaskType;
        private System.Windows.Forms.CheckBox chkShowScheduleDetail;
        private System.Windows.Forms.CheckBox chkGetScheduleList;
        private System.Windows.Forms.CheckBox chkGetTaskList;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.RadioButton optUseDatabase;
        internal System.Windows.Forms.RadioButton optUseXmlFiles;
        internal System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        private System.Windows.Forms.Button cmdReinit;
    }
}

