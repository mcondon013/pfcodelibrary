namespace TestprogSchedulerInteractive
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtScheduleStart = new System.Windows.Forms.TextBox();
            this.txtScheduleEnd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtScheduleName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboScheduleFrequency = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRunOnceAt = new System.Windows.Forms.TextBox();
            this.grpRunOneTimeParameters = new System.Windows.Forms.GroupBox();
            this.grpDailyRunOnceParameters = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDailyRunOnceAt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRunWindowInMinutes = new System.Windows.Forms.TextBox();
            this.grpDailyScheduleParameters = new System.Windows.Forms.GroupBox();
            this.grpDailyRecurringParameters = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtOccursEndsAt = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtOccursStartingAt = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtDailyOccursEveryIntervalNum = new System.Windows.Forms.TextBox();
            this.cboDailyOccursInterval = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtScheduleRunsEveryNumDays = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.optDailyRecurring = new System.Windows.Forms.RadioButton();
            this.optDailyRunOnce = new System.Windows.Forms.RadioButton();
            this.grpWeeklyScheduleParameters = new System.Windows.Forms.GroupBox();
            this.chkWeeklySunday = new System.Windows.Forms.CheckBox();
            this.chkWeeklySaturday = new System.Windows.Forms.CheckBox();
            this.chkWeeklyFriday = new System.Windows.Forms.CheckBox();
            this.chkWeeklyThursday = new System.Windows.Forms.CheckBox();
            this.chkWeeklyWednesday = new System.Windows.Forms.CheckBox();
            this.chkWeeklyTuesday = new System.Windows.Forms.CheckBox();
            this.chkWeeklyMonday = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtWeeklyRecursEveryNumDays = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.grpMonthlyScheduleParameters = new System.Windows.Forms.GroupBox();
            this.grpMonthOccursEvery = new System.Windows.Forms.GroupBox();
            this.chkMonthlyDec = new System.Windows.Forms.CheckBox();
            this.chkMonthlyNov = new System.Windows.Forms.CheckBox();
            this.chkMonthlyOct = new System.Windows.Forms.CheckBox();
            this.chkMonthlySep = new System.Windows.Forms.CheckBox();
            this.chkMonthlyAug = new System.Windows.Forms.CheckBox();
            this.chkMonthlyJul = new System.Windows.Forms.CheckBox();
            this.chkMonthlyJun = new System.Windows.Forms.CheckBox();
            this.chkMonthlyMay = new System.Windows.Forms.CheckBox();
            this.chkMonthlyApr = new System.Windows.Forms.CheckBox();
            this.chkMonthlyMar = new System.Windows.Forms.CheckBox();
            this.chkMonthlyFeb = new System.Windows.Forms.CheckBox();
            this.chkMonthlyJan = new System.Windows.Forms.CheckBox();
            this.optOccursDuringMonthName = new System.Windows.Forms.RadioButton();
            this.optOccursEveryMonthNum = new System.Windows.Forms.RadioButton();
            this.label20 = new System.Windows.Forms.Label();
            this.txtMonthlyOccursIntervalNum = new System.Windows.Forms.TextBox();
            this.cboMonthlyDayName = new System.Windows.Forms.ComboBox();
            this.cboMonthlyDayNameOrdinal = new System.Windows.Forms.ComboBox();
            this.optMonthlyDayName = new System.Windows.Forms.RadioButton();
            this.txtMonthlyDayNumber = new System.Windows.Forms.TextBox();
            this.optMonthlyDayNumber = new System.Windows.Forms.RadioButton();
            this.cmdSaveSchedule = new System.Windows.Forms.Button();
            this.cmdLoadSchedule = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtCurrDateTime = new System.Windows.Forms.TextBox();
            this.cboExpectedTestResult = new System.Windows.Forms.ComboBox();
            this.cmdReinit = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.cmdNextSkedDate = new System.Windows.Forms.Button();
            this.cmdShowAllScheduledDates = new System.Windows.Forms.Button();
            this.grpStorageType = new System.Windows.Forms.GroupBox();
            this.optDatabase = new System.Windows.Forms.RadioButton();
            this.optXmlFiles = new System.Windows.Forms.RadioButton();
            this.MainMenu.SuspendLayout();
            this.grpRunOneTimeParameters.SuspendLayout();
            this.grpDailyRunOnceParameters.SuspendLayout();
            this.grpDailyScheduleParameters.SuspendLayout();
            this.grpDailyRecurringParameters.SuspendLayout();
            this.grpWeeklyScheduleParameters.SuspendLayout();
            this.grpMonthlyScheduleParameters.SuspendLayout();
            this.grpMonthOccursEvery.SuspendLayout();
            this.grpStorageType.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(687, 161);
            this.cmdExit.Name = "cmdExit";
            this.appHelpProvider.SetShowHelp(this.cmdExit, true);
            this.cmdExit.Size = new System.Drawing.Size(93, 37);
            this.cmdExit.TabIndex = 24;
            this.cmdExit.Text = "E&xit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdRunTest
            // 
            this.appHelpProvider.SetHelpKeyword(this.cmdRunTest, "Run Tests");
            this.appHelpProvider.SetHelpNavigator(this.cmdRunTest, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.appHelpProvider.SetHelpString(this.cmdRunTest, "Help for Run Tests: See Help File.");
            this.cmdRunTest.Location = new System.Drawing.Point(544, 41);
            this.cmdRunTest.Name = "cmdRunTest";
            this.appHelpProvider.SetShowHelp(this.cmdRunTest, true);
            this.cmdRunTest.Size = new System.Drawing.Size(99, 37);
            this.cmdRunTest.TabIndex = 15;
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
            this.MainMenu.Size = new System.Drawing.Size(823, 24);
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
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(24, 521);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 13;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogSchedulerInteractive\\InitWinFormsA" +
    "ppWithExtendedOptions\\InitWinFormsHelpFile.chm";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Schedule Start:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(249, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Schedule End:";
            // 
            // txtScheduleStart
            // 
            this.txtScheduleStart.Location = new System.Drawing.Point(122, 110);
            this.txtScheduleStart.Name = "txtScheduleStart";
            this.txtScheduleStart.Size = new System.Drawing.Size(121, 20);
            this.txtScheduleStart.TabIndex = 6;
            // 
            // txtScheduleEnd
            // 
            this.txtScheduleEnd.Location = new System.Drawing.Point(335, 110);
            this.txtScheduleEnd.Name = "txtScheduleEnd";
            this.txtScheduleEnd.Size = new System.Drawing.Size(123, 20);
            this.txtScheduleEnd.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Schedule Name:";
            // 
            // txtScheduleName
            // 
            this.txtScheduleName.Location = new System.Drawing.Point(122, 84);
            this.txtScheduleName.Name = "txtScheduleName";
            this.txtScheduleName.Size = new System.Drawing.Size(336, 20);
            this.txtScheduleName.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Schedule Frequency:";
            // 
            // cboScheduleFrequency
            // 
            this.cboScheduleFrequency.FormattingEnabled = true;
            this.cboScheduleFrequency.Items.AddRange(new object[] {
            "OneTime",
            "Daily",
            "Weekly",
            "Monthly"});
            this.cboScheduleFrequency.Location = new System.Drawing.Point(39, 57);
            this.cboScheduleFrequency.Name = "cboScheduleFrequency";
            this.cboScheduleFrequency.Size = new System.Drawing.Size(168, 21);
            this.cboScheduleFrequency.TabIndex = 1;
            this.cboScheduleFrequency.SelectedIndexChanged += new System.EventHandler(this.cboScheduleFrequency_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Run At:";
            // 
            // txtRunOnceAt
            // 
            this.txtRunOnceAt.Location = new System.Drawing.Point(106, 19);
            this.txtRunOnceAt.Name = "txtRunOnceAt";
            this.txtRunOnceAt.Size = new System.Drawing.Size(147, 20);
            this.txtRunOnceAt.TabIndex = 1;
            // 
            // grpRunOneTimeParameters
            // 
            this.grpRunOneTimeParameters.Controls.Add(this.txtRunOnceAt);
            this.grpRunOneTimeParameters.Controls.Add(this.label6);
            this.grpRunOneTimeParameters.Location = new System.Drawing.Point(39, 216);
            this.grpRunOneTimeParameters.Name = "grpRunOneTimeParameters";
            this.grpRunOneTimeParameters.Size = new System.Drawing.Size(419, 53);
            this.grpRunOneTimeParameters.TabIndex = 11;
            this.grpRunOneTimeParameters.TabStop = false;
            this.grpRunOneTimeParameters.Text = "Run One Time Parameters";
            // 
            // grpDailyRunOnceParameters
            // 
            this.grpDailyRunOnceParameters.Controls.Add(this.label4);
            this.grpDailyRunOnceParameters.Controls.Add(this.txtDailyRunOnceAt);
            this.grpDailyRunOnceParameters.Controls.Add(this.label7);
            this.grpDailyRunOnceParameters.Location = new System.Drawing.Point(22, 43);
            this.grpDailyRunOnceParameters.Name = "grpDailyRunOnceParameters";
            this.grpDailyRunOnceParameters.Size = new System.Drawing.Size(391, 50);
            this.grpDailyRunOnceParameters.TabIndex = 5;
            this.grpDailyRunOnceParameters.TabStop = false;
            this.grpDailyRunOnceParameters.Text = "Daily Run Once Parameters";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(237, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "(Format HH:mm:ss) *";
            // 
            // txtDailyRunOnceAt
            // 
            this.txtDailyRunOnceAt.Location = new System.Drawing.Point(109, 20);
            this.txtDailyRunOnceAt.Name = "txtDailyRunOnceAt";
            this.txtDailyRunOnceAt.Size = new System.Drawing.Size(122, 20);
            this.txtDailyRunOnceAt.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Run Once At: ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(36, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Run Window (minutes):";
            // 
            // txtRunWindowInMinutes
            // 
            this.txtRunWindowInMinutes.Location = new System.Drawing.Point(159, 137);
            this.txtRunWindowInMinutes.Name = "txtRunWindowInMinutes";
            this.txtRunWindowInMinutes.Size = new System.Drawing.Size(84, 20);
            this.txtRunWindowInMinutes.TabIndex = 10;
            this.txtRunWindowInMinutes.Text = "10";
            // 
            // grpDailyScheduleParameters
            // 
            this.grpDailyScheduleParameters.Controls.Add(this.grpDailyRecurringParameters);
            this.grpDailyScheduleParameters.Controls.Add(this.label10);
            this.grpDailyScheduleParameters.Controls.Add(this.txtScheduleRunsEveryNumDays);
            this.grpDailyScheduleParameters.Controls.Add(this.label9);
            this.grpDailyScheduleParameters.Controls.Add(this.optDailyRecurring);
            this.grpDailyScheduleParameters.Controls.Add(this.optDailyRunOnce);
            this.grpDailyScheduleParameters.Controls.Add(this.grpDailyRunOnceParameters);
            this.grpDailyScheduleParameters.Location = new System.Drawing.Point(39, 275);
            this.grpDailyScheduleParameters.Name = "grpDailyScheduleParameters";
            this.grpDailyScheduleParameters.Size = new System.Drawing.Size(419, 226);
            this.grpDailyScheduleParameters.TabIndex = 12;
            this.grpDailyScheduleParameters.TabStop = false;
            this.grpDailyScheduleParameters.Text = "Daily Schedule Parameters:";
            // 
            // grpDailyRecurringParameters
            // 
            this.grpDailyRecurringParameters.Controls.Add(this.label15);
            this.grpDailyRecurringParameters.Controls.Add(this.label14);
            this.grpDailyRecurringParameters.Controls.Add(this.txtOccursEndsAt);
            this.grpDailyRecurringParameters.Controls.Add(this.label13);
            this.grpDailyRecurringParameters.Controls.Add(this.txtOccursStartingAt);
            this.grpDailyRecurringParameters.Controls.Add(this.label12);
            this.grpDailyRecurringParameters.Controls.Add(this.txtDailyOccursEveryIntervalNum);
            this.grpDailyRecurringParameters.Controls.Add(this.cboDailyOccursInterval);
            this.grpDailyRecurringParameters.Controls.Add(this.label11);
            this.grpDailyRecurringParameters.Location = new System.Drawing.Point(24, 99);
            this.grpDailyRecurringParameters.Name = "grpDailyRecurringParameters";
            this.grpDailyRecurringParameters.Size = new System.Drawing.Size(389, 112);
            this.grpDailyRecurringParameters.TabIndex = 6;
            this.grpDailyRecurringParameters.TabStop = false;
            this.grpDailyRecurringParameters.Text = "Daily Recurring Parameters";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(232, 81);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(106, 13);
            this.label15.TabIndex = 8;
            this.label15.Text = "(Format: HH:mm:ss) *";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(233, 55);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(106, 13);
            this.label14.TabIndex = 5;
            this.label14.Text = "(Format: HH:mm:ss) *";
            // 
            // txtOccursEndsAt
            // 
            this.txtOccursEndsAt.Location = new System.Drawing.Point(127, 78);
            this.txtOccursEndsAt.Name = "txtOccursEndsAt";
            this.txtOccursEndsAt.Size = new System.Drawing.Size(100, 20);
            this.txtOccursEndsAt.TabIndex = 7;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(74, 75);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 13);
            this.label13.TabIndex = 6;
            this.label13.Text = "Ends At";
            // 
            // txtOccursStartingAt
            // 
            this.txtOccursStartingAt.Location = new System.Drawing.Point(127, 52);
            this.txtOccursStartingAt.Name = "txtOccursStartingAt";
            this.txtOccursStartingAt.Size = new System.Drawing.Size(100, 20);
            this.txtOccursStartingAt.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(25, 52);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Occurs Starting At";
            // 
            // txtDailyOccursEveryIntervalNum
            // 
            this.txtDailyOccursEveryIntervalNum.Location = new System.Drawing.Point(106, 22);
            this.txtDailyOccursEveryIntervalNum.Name = "txtDailyOccursEveryIntervalNum";
            this.txtDailyOccursEveryIntervalNum.Size = new System.Drawing.Size(38, 20);
            this.txtDailyOccursEveryIntervalNum.TabIndex = 1;
            this.txtDailyOccursEveryIntervalNum.Text = "1";
            this.txtDailyOccursEveryIntervalNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cboDailyOccursInterval
            // 
            this.cboDailyOccursInterval.FormattingEnabled = true;
            this.cboDailyOccursInterval.Items.AddRange(new object[] {
            "Hours",
            "Minutes",
            "Seconds"});
            this.cboDailyOccursInterval.Location = new System.Drawing.Point(156, 22);
            this.cboDailyOccursInterval.Name = "cboDailyOccursInterval";
            this.cboDailyOccursInterval.Size = new System.Drawing.Size(73, 21);
            this.cboDailyOccursInterval.TabIndex = 2;
            this.cboDailyOccursInterval.Text = "Hours";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Occurs Every ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(359, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "days";
            // 
            // txtScheduleRunsEveryNumDays
            // 
            this.txtScheduleRunsEveryNumDays.Location = new System.Drawing.Point(316, 17);
            this.txtScheduleRunsEveryNumDays.Name = "txtScheduleRunsEveryNumDays";
            this.txtScheduleRunsEveryNumDays.Size = new System.Drawing.Size(30, 20);
            this.txtScheduleRunsEveryNumDays.TabIndex = 3;
            this.txtScheduleRunsEveryNumDays.Text = "1";
            this.txtScheduleRunsEveryNumDays.Leave += new System.EventHandler(this.txtScheduleRunsEveryNumDays_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(196, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Schedule runs every #";
            // 
            // optDailyRecurring
            // 
            this.optDailyRecurring.AutoSize = true;
            this.optDailyRecurring.Location = new System.Drawing.Point(122, 19);
            this.optDailyRecurring.Name = "optDailyRecurring";
            this.optDailyRecurring.Size = new System.Drawing.Size(71, 17);
            this.optDailyRecurring.TabIndex = 1;
            this.optDailyRecurring.TabStop = true;
            this.optDailyRecurring.Text = "Recurring";
            this.optDailyRecurring.UseVisualStyleBackColor = true;
            this.optDailyRecurring.CheckedChanged += new System.EventHandler(this.optDailyRecurring_CheckedChanged);
            // 
            // optDailyRunOnce
            // 
            this.optDailyRunOnce.AutoSize = true;
            this.optDailyRunOnce.Location = new System.Drawing.Point(22, 19);
            this.optDailyRunOnce.Name = "optDailyRunOnce";
            this.optDailyRunOnce.Size = new System.Drawing.Size(97, 17);
            this.optDailyRunOnce.TabIndex = 0;
            this.optDailyRunOnce.TabStop = true;
            this.optDailyRunOnce.Text = "Daily RunOnce";
            this.optDailyRunOnce.UseVisualStyleBackColor = true;
            this.optDailyRunOnce.CheckedChanged += new System.EventHandler(this.optDailyRunOnce_CheckedChanged);
            // 
            // grpWeeklyScheduleParameters
            // 
            this.grpWeeklyScheduleParameters.Controls.Add(this.chkWeeklySunday);
            this.grpWeeklyScheduleParameters.Controls.Add(this.chkWeeklySaturday);
            this.grpWeeklyScheduleParameters.Controls.Add(this.chkWeeklyFriday);
            this.grpWeeklyScheduleParameters.Controls.Add(this.chkWeeklyThursday);
            this.grpWeeklyScheduleParameters.Controls.Add(this.chkWeeklyWednesday);
            this.grpWeeklyScheduleParameters.Controls.Add(this.chkWeeklyTuesday);
            this.grpWeeklyScheduleParameters.Controls.Add(this.chkWeeklyMonday);
            this.grpWeeklyScheduleParameters.Controls.Add(this.label18);
            this.grpWeeklyScheduleParameters.Controls.Add(this.label17);
            this.grpWeeklyScheduleParameters.Controls.Add(this.txtWeeklyRecursEveryNumDays);
            this.grpWeeklyScheduleParameters.Controls.Add(this.label16);
            this.grpWeeklyScheduleParameters.Location = new System.Drawing.Point(490, 207);
            this.grpWeeklyScheduleParameters.Name = "grpWeeklyScheduleParameters";
            this.grpWeeklyScheduleParameters.Size = new System.Drawing.Size(290, 120);
            this.grpWeeklyScheduleParameters.TabIndex = 20;
            this.grpWeeklyScheduleParameters.TabStop = false;
            this.grpWeeklyScheduleParameters.Text = "Weekly Schedule Parameters";
            // 
            // chkWeeklySunday
            // 
            this.chkWeeklySunday.AutoSize = true;
            this.chkWeeklySunday.Location = new System.Drawing.Point(177, 91);
            this.chkWeeklySunday.Name = "chkWeeklySunday";
            this.chkWeeklySunday.Size = new System.Drawing.Size(62, 17);
            this.chkWeeklySunday.TabIndex = 10;
            this.chkWeeklySunday.Text = "Sunday";
            this.chkWeeklySunday.UseVisualStyleBackColor = true;
            // 
            // chkWeeklySaturday
            // 
            this.chkWeeklySaturday.AutoSize = true;
            this.chkWeeklySaturday.Location = new System.Drawing.Point(177, 68);
            this.chkWeeklySaturday.Name = "chkWeeklySaturday";
            this.chkWeeklySaturday.Size = new System.Drawing.Size(68, 17);
            this.chkWeeklySaturday.TabIndex = 9;
            this.chkWeeklySaturday.Text = "Saturday";
            this.chkWeeklySaturday.UseVisualStyleBackColor = true;
            // 
            // chkWeeklyFriday
            // 
            this.chkWeeklyFriday.AutoSize = true;
            this.chkWeeklyFriday.Location = new System.Drawing.Point(96, 68);
            this.chkWeeklyFriday.Name = "chkWeeklyFriday";
            this.chkWeeklyFriday.Size = new System.Drawing.Size(54, 17);
            this.chkWeeklyFriday.TabIndex = 8;
            this.chkWeeklyFriday.Text = "Friday";
            this.chkWeeklyFriday.UseVisualStyleBackColor = true;
            // 
            // chkWeeklyThursday
            // 
            this.chkWeeklyThursday.AutoSize = true;
            this.chkWeeklyThursday.Location = new System.Drawing.Point(24, 68);
            this.chkWeeklyThursday.Name = "chkWeeklyThursday";
            this.chkWeeklyThursday.Size = new System.Drawing.Size(70, 17);
            this.chkWeeklyThursday.TabIndex = 7;
            this.chkWeeklyThursday.Text = "Thursday";
            this.chkWeeklyThursday.UseVisualStyleBackColor = true;
            // 
            // chkWeeklyWednesday
            // 
            this.chkWeeklyWednesday.AutoSize = true;
            this.chkWeeklyWednesday.Location = new System.Drawing.Point(177, 45);
            this.chkWeeklyWednesday.Name = "chkWeeklyWednesday";
            this.chkWeeklyWednesday.Size = new System.Drawing.Size(83, 17);
            this.chkWeeklyWednesday.TabIndex = 6;
            this.chkWeeklyWednesday.Text = "Wednesday";
            this.chkWeeklyWednesday.UseVisualStyleBackColor = true;
            // 
            // chkWeeklyTuesday
            // 
            this.chkWeeklyTuesday.AutoSize = true;
            this.chkWeeklyTuesday.Location = new System.Drawing.Point(97, 45);
            this.chkWeeklyTuesday.Name = "chkWeeklyTuesday";
            this.chkWeeklyTuesday.Size = new System.Drawing.Size(67, 17);
            this.chkWeeklyTuesday.TabIndex = 5;
            this.chkWeeklyTuesday.Text = "Tuesday";
            this.chkWeeklyTuesday.UseVisualStyleBackColor = true;
            // 
            // chkWeeklyMonday
            // 
            this.chkWeeklyMonday.AutoSize = true;
            this.chkWeeklyMonday.Location = new System.Drawing.Point(24, 45);
            this.chkWeeklyMonday.Name = "chkWeeklyMonday";
            this.chkWeeklyMonday.Size = new System.Drawing.Size(64, 17);
            this.chkWeeklyMonday.TabIndex = 4;
            this.chkWeeklyMonday.Text = "Monday";
            this.chkWeeklyMonday.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(174, 27);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(19, 13);
            this.label18.TabIndex = 3;
            this.label18.Text = "on";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(130, 27);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(38, 13);
            this.label17.TabIndex = 2;
            this.label17.Text = "weeks";
            // 
            // txtWeeklyRecursEveryNumDays
            // 
            this.txtWeeklyRecursEveryNumDays.Location = new System.Drawing.Point(97, 23);
            this.txtWeeklyRecursEveryNumDays.Name = "txtWeeklyRecursEveryNumDays";
            this.txtWeeklyRecursEveryNumDays.Size = new System.Drawing.Size(27, 20);
            this.txtWeeklyRecursEveryNumDays.TabIndex = 1;
            this.txtWeeklyRecursEveryNumDays.Text = "1";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(21, 27);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Recurs every";
            // 
            // grpMonthlyScheduleParameters
            // 
            this.grpMonthlyScheduleParameters.Controls.Add(this.grpMonthOccursEvery);
            this.grpMonthlyScheduleParameters.Controls.Add(this.cboMonthlyDayName);
            this.grpMonthlyScheduleParameters.Controls.Add(this.cboMonthlyDayNameOrdinal);
            this.grpMonthlyScheduleParameters.Controls.Add(this.optMonthlyDayName);
            this.grpMonthlyScheduleParameters.Controls.Add(this.txtMonthlyDayNumber);
            this.grpMonthlyScheduleParameters.Controls.Add(this.optMonthlyDayNumber);
            this.grpMonthlyScheduleParameters.Location = new System.Drawing.Point(490, 333);
            this.grpMonthlyScheduleParameters.Name = "grpMonthlyScheduleParameters";
            this.grpMonthlyScheduleParameters.Size = new System.Drawing.Size(290, 232);
            this.grpMonthlyScheduleParameters.TabIndex = 21;
            this.grpMonthlyScheduleParameters.TabStop = false;
            this.grpMonthlyScheduleParameters.Text = "Monthly Schedule Paramters";
            // 
            // grpMonthOccursEvery
            // 
            this.grpMonthOccursEvery.Controls.Add(this.chkMonthlyDec);
            this.grpMonthOccursEvery.Controls.Add(this.chkMonthlyNov);
            this.grpMonthOccursEvery.Controls.Add(this.chkMonthlyOct);
            this.grpMonthOccursEvery.Controls.Add(this.chkMonthlySep);
            this.grpMonthOccursEvery.Controls.Add(this.chkMonthlyAug);
            this.grpMonthOccursEvery.Controls.Add(this.chkMonthlyJul);
            this.grpMonthOccursEvery.Controls.Add(this.chkMonthlyJun);
            this.grpMonthOccursEvery.Controls.Add(this.chkMonthlyMay);
            this.grpMonthOccursEvery.Controls.Add(this.chkMonthlyApr);
            this.grpMonthOccursEvery.Controls.Add(this.chkMonthlyMar);
            this.grpMonthOccursEvery.Controls.Add(this.chkMonthlyFeb);
            this.grpMonthOccursEvery.Controls.Add(this.chkMonthlyJan);
            this.grpMonthOccursEvery.Controls.Add(this.optOccursDuringMonthName);
            this.grpMonthOccursEvery.Controls.Add(this.optOccursEveryMonthNum);
            this.grpMonthOccursEvery.Controls.Add(this.label20);
            this.grpMonthOccursEvery.Controls.Add(this.txtMonthlyOccursIntervalNum);
            this.grpMonthOccursEvery.Location = new System.Drawing.Point(24, 19);
            this.grpMonthOccursEvery.Name = "grpMonthOccursEvery";
            this.grpMonthOccursEvery.Size = new System.Drawing.Size(236, 138);
            this.grpMonthOccursEvery.TabIndex = 8;
            this.grpMonthOccursEvery.TabStop = false;
            // 
            // chkMonthlyDec
            // 
            this.chkMonthlyDec.AutoSize = true;
            this.chkMonthlyDec.Location = new System.Drawing.Point(179, 108);
            this.chkMonthlyDec.Name = "chkMonthlyDec";
            this.chkMonthlyDec.Size = new System.Drawing.Size(46, 17);
            this.chkMonthlyDec.TabIndex = 16;
            this.chkMonthlyDec.Text = "Dec";
            this.chkMonthlyDec.UseVisualStyleBackColor = true;
            // 
            // chkMonthlyNov
            // 
            this.chkMonthlyNov.AutoSize = true;
            this.chkMonthlyNov.Location = new System.Drawing.Point(131, 108);
            this.chkMonthlyNov.Name = "chkMonthlyNov";
            this.chkMonthlyNov.Size = new System.Drawing.Size(46, 17);
            this.chkMonthlyNov.TabIndex = 15;
            this.chkMonthlyNov.Text = "Nov";
            this.chkMonthlyNov.UseVisualStyleBackColor = true;
            // 
            // chkMonthlyOct
            // 
            this.chkMonthlyOct.AutoSize = true;
            this.chkMonthlyOct.Location = new System.Drawing.Point(81, 108);
            this.chkMonthlyOct.Name = "chkMonthlyOct";
            this.chkMonthlyOct.Size = new System.Drawing.Size(43, 17);
            this.chkMonthlyOct.TabIndex = 14;
            this.chkMonthlyOct.Text = "Oct";
            this.chkMonthlyOct.UseVisualStyleBackColor = true;
            // 
            // chkMonthlySep
            // 
            this.chkMonthlySep.AutoSize = true;
            this.chkMonthlySep.Location = new System.Drawing.Point(32, 108);
            this.chkMonthlySep.Name = "chkMonthlySep";
            this.chkMonthlySep.Size = new System.Drawing.Size(45, 17);
            this.chkMonthlySep.TabIndex = 13;
            this.chkMonthlySep.Text = "Sep";
            this.chkMonthlySep.UseVisualStyleBackColor = true;
            // 
            // chkMonthlyAug
            // 
            this.chkMonthlyAug.AutoSize = true;
            this.chkMonthlyAug.Location = new System.Drawing.Point(179, 85);
            this.chkMonthlyAug.Name = "chkMonthlyAug";
            this.chkMonthlyAug.Size = new System.Drawing.Size(45, 17);
            this.chkMonthlyAug.TabIndex = 12;
            this.chkMonthlyAug.Text = "Aug";
            this.chkMonthlyAug.UseVisualStyleBackColor = true;
            // 
            // chkMonthlyJul
            // 
            this.chkMonthlyJul.AutoSize = true;
            this.chkMonthlyJul.Location = new System.Drawing.Point(131, 85);
            this.chkMonthlyJul.Name = "chkMonthlyJul";
            this.chkMonthlyJul.Size = new System.Drawing.Size(39, 17);
            this.chkMonthlyJul.TabIndex = 11;
            this.chkMonthlyJul.Text = "Jul";
            this.chkMonthlyJul.UseVisualStyleBackColor = true;
            // 
            // chkMonthlyJun
            // 
            this.chkMonthlyJun.AutoSize = true;
            this.chkMonthlyJun.Location = new System.Drawing.Point(81, 85);
            this.chkMonthlyJun.Name = "chkMonthlyJun";
            this.chkMonthlyJun.Size = new System.Drawing.Size(43, 17);
            this.chkMonthlyJun.TabIndex = 10;
            this.chkMonthlyJun.Text = "Jun";
            this.chkMonthlyJun.UseVisualStyleBackColor = true;
            // 
            // chkMonthlyMay
            // 
            this.chkMonthlyMay.AutoSize = true;
            this.chkMonthlyMay.Location = new System.Drawing.Point(32, 85);
            this.chkMonthlyMay.Name = "chkMonthlyMay";
            this.chkMonthlyMay.Size = new System.Drawing.Size(46, 17);
            this.chkMonthlyMay.TabIndex = 9;
            this.chkMonthlyMay.Text = "May";
            this.chkMonthlyMay.UseVisualStyleBackColor = true;
            // 
            // chkMonthlyApr
            // 
            this.chkMonthlyApr.AutoSize = true;
            this.chkMonthlyApr.Location = new System.Drawing.Point(179, 62);
            this.chkMonthlyApr.Name = "chkMonthlyApr";
            this.chkMonthlyApr.Size = new System.Drawing.Size(42, 17);
            this.chkMonthlyApr.TabIndex = 8;
            this.chkMonthlyApr.Text = "Apr";
            this.chkMonthlyApr.UseVisualStyleBackColor = true;
            // 
            // chkMonthlyMar
            // 
            this.chkMonthlyMar.AutoSize = true;
            this.chkMonthlyMar.Location = new System.Drawing.Point(131, 62);
            this.chkMonthlyMar.Name = "chkMonthlyMar";
            this.chkMonthlyMar.Size = new System.Drawing.Size(44, 17);
            this.chkMonthlyMar.TabIndex = 7;
            this.chkMonthlyMar.Text = "Mar";
            this.chkMonthlyMar.UseVisualStyleBackColor = true;
            // 
            // chkMonthlyFeb
            // 
            this.chkMonthlyFeb.AutoSize = true;
            this.chkMonthlyFeb.Location = new System.Drawing.Point(81, 62);
            this.chkMonthlyFeb.Name = "chkMonthlyFeb";
            this.chkMonthlyFeb.Size = new System.Drawing.Size(44, 17);
            this.chkMonthlyFeb.TabIndex = 6;
            this.chkMonthlyFeb.Text = "Feb";
            this.chkMonthlyFeb.UseVisualStyleBackColor = true;
            // 
            // chkMonthlyJan
            // 
            this.chkMonthlyJan.AutoSize = true;
            this.chkMonthlyJan.Location = new System.Drawing.Point(32, 62);
            this.chkMonthlyJan.Name = "chkMonthlyJan";
            this.chkMonthlyJan.Size = new System.Drawing.Size(43, 17);
            this.chkMonthlyJan.TabIndex = 5;
            this.chkMonthlyJan.Text = "Jan";
            this.chkMonthlyJan.UseVisualStyleBackColor = true;
            // 
            // optOccursDuringMonthName
            // 
            this.optOccursDuringMonthName.AutoSize = true;
            this.optOccursDuringMonthName.Location = new System.Drawing.Point(12, 43);
            this.optOccursDuringMonthName.Name = "optOccursDuringMonthName";
            this.optOccursDuringMonthName.Size = new System.Drawing.Size(93, 17);
            this.optOccursDuringMonthName.TabIndex = 4;
            this.optOccursDuringMonthName.TabStop = true;
            this.optOccursDuringMonthName.Text = "Occurs During";
            this.optOccursDuringMonthName.UseVisualStyleBackColor = true;
            // 
            // optOccursEveryMonthNum
            // 
            this.optOccursEveryMonthNum.AutoSize = true;
            this.optOccursEveryMonthNum.Location = new System.Drawing.Point(12, 20);
            this.optOccursEveryMonthNum.Name = "optOccursEveryMonthNum";
            this.optOccursEveryMonthNum.Size = new System.Drawing.Size(88, 17);
            this.optOccursEveryMonthNum.TabIndex = 3;
            this.optOccursEveryMonthNum.TabStop = true;
            this.optOccursEveryMonthNum.Text = "Occurs every";
            this.optOccursEveryMonthNum.UseVisualStyleBackColor = true;
            this.optOccursEveryMonthNum.CheckedChanged += new System.EventHandler(this.optOccursEveryMonthNum_CheckedChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(155, 22);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(47, 13);
            this.label20.TabIndex = 2;
            this.label20.Text = "month(s)";
            // 
            // txtMonthlyOccursIntervalNum
            // 
            this.txtMonthlyOccursIntervalNum.Location = new System.Drawing.Point(115, 19);
            this.txtMonthlyOccursIntervalNum.Name = "txtMonthlyOccursIntervalNum";
            this.txtMonthlyOccursIntervalNum.Size = new System.Drawing.Size(31, 20);
            this.txtMonthlyOccursIntervalNum.TabIndex = 1;
            this.txtMonthlyOccursIntervalNum.Text = "1";
            // 
            // cboMonthlyDayName
            // 
            this.cboMonthlyDayName.FormattingEnabled = true;
            this.cboMonthlyDayName.Items.AddRange(new object[] {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Sunday"});
            this.cboMonthlyDayName.Location = new System.Drawing.Point(185, 188);
            this.cboMonthlyDayName.Name = "cboMonthlyDayName";
            this.cboMonthlyDayName.Size = new System.Drawing.Size(84, 21);
            this.cboMonthlyDayName.TabIndex = 7;
            // 
            // cboMonthlyDayNameOrdinal
            // 
            this.cboMonthlyDayNameOrdinal.FormattingEnabled = true;
            this.cboMonthlyDayNameOrdinal.Items.AddRange(new object[] {
            "First",
            "Second",
            "Third",
            "Fourth",
            "Last"});
            this.cboMonthlyDayNameOrdinal.Location = new System.Drawing.Point(114, 188);
            this.cboMonthlyDayNameOrdinal.Name = "cboMonthlyDayNameOrdinal";
            this.cboMonthlyDayNameOrdinal.Size = new System.Drawing.Size(59, 21);
            this.cboMonthlyDayNameOrdinal.TabIndex = 6;
            // 
            // optMonthlyDayName
            // 
            this.optMonthlyDayName.AutoSize = true;
            this.optMonthlyDayName.Location = new System.Drawing.Point(36, 188);
            this.optMonthlyDayName.Name = "optMonthlyDayName";
            this.optMonthlyDayName.Size = new System.Drawing.Size(72, 17);
            this.optMonthlyDayName.TabIndex = 5;
            this.optMonthlyDayName.TabStop = true;
            this.optMonthlyDayName.Text = "DayName";
            this.optMonthlyDayName.UseVisualStyleBackColor = true;
            // 
            // txtMonthlyDayNumber
            // 
            this.txtMonthlyDayNumber.Location = new System.Drawing.Point(127, 165);
            this.txtMonthlyDayNumber.Name = "txtMonthlyDayNumber";
            this.txtMonthlyDayNumber.Size = new System.Drawing.Size(32, 20);
            this.txtMonthlyDayNumber.TabIndex = 4;
            this.txtMonthlyDayNumber.Text = "1";
            // 
            // optMonthlyDayNumber
            // 
            this.optMonthlyDayNumber.AutoSize = true;
            this.optMonthlyDayNumber.Location = new System.Drawing.Point(36, 165);
            this.optMonthlyDayNumber.Name = "optMonthlyDayNumber";
            this.optMonthlyDayNumber.Size = new System.Drawing.Size(84, 17);
            this.optMonthlyDayNumber.TabIndex = 3;
            this.optMonthlyDayNumber.TabStop = true;
            this.optMonthlyDayNumber.Text = "Day Number";
            this.optMonthlyDayNumber.UseVisualStyleBackColor = true;
            this.optMonthlyDayNumber.CheckedChanged += new System.EventHandler(this.optMonthlyDayNumber_CheckedChanged);
            // 
            // cmdSaveSchedule
            // 
            this.cmdSaveSchedule.Location = new System.Drawing.Point(687, 101);
            this.cmdSaveSchedule.Name = "cmdSaveSchedule";
            this.cmdSaveSchedule.Size = new System.Drawing.Size(93, 37);
            this.cmdSaveSchedule.TabIndex = 22;
            this.cmdSaveSchedule.Text = "Save Schedule";
            this.cmdSaveSchedule.UseVisualStyleBackColor = true;
            this.cmdSaveSchedule.Click += new System.EventHandler(this.cmdSaveSchedule_Click);
            // 
            // cmdLoadSchedule
            // 
            this.cmdLoadSchedule.Location = new System.Drawing.Point(687, 41);
            this.cmdLoadSchedule.Name = "cmdLoadSchedule";
            this.cmdLoadSchedule.Size = new System.Drawing.Size(93, 37);
            this.cmdLoadSchedule.TabIndex = 23;
            this.cmdLoadSchedule.Text = "Load Schedule";
            this.cmdLoadSchedule.UseVisualStyleBackColor = true;
            this.cmdLoadSchedule.Click += new System.EventHandler(this.cmdLoadSchedule_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(36, 160);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(122, 13);
            this.label21.TabIndex = 16;
            this.label21.Text = "Test Current Date/Time:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(249, 160);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(112, 13);
            this.label22.TabIndex = 18;
            this.label22.Text = "Expected Test Result:";
            // 
            // txtCurrDateTime
            // 
            this.txtCurrDateTime.Location = new System.Drawing.Point(36, 177);
            this.txtCurrDateTime.Name = "txtCurrDateTime";
            this.txtCurrDateTime.Size = new System.Drawing.Size(166, 20);
            this.txtCurrDateTime.TabIndex = 17;
            // 
            // cboExpectedTestResult
            // 
            this.cboExpectedTestResult.FormattingEnabled = true;
            this.cboExpectedTestResult.Location = new System.Drawing.Point(252, 177);
            this.cboExpectedTestResult.Name = "cboExpectedTestResult";
            this.cboExpectedTestResult.Size = new System.Drawing.Size(166, 21);
            this.cboExpectedTestResult.TabIndex = 19;
            // 
            // cmdReinit
            // 
            this.cmdReinit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReinit.ForeColor = System.Drawing.SystemColors.Desktop;
            this.cmdReinit.Location = new System.Drawing.Point(544, 160);
            this.cmdReinit.Name = "cmdReinit";
            this.cmdReinit.Size = new System.Drawing.Size(99, 37);
            this.cmdReinit.TabIndex = 2;
            this.cmdReinit.Text = "Reinit";
            this.cmdReinit.UseVisualStyleBackColor = true;
            this.cmdReinit.Click += new System.EventHandler(this.cmdReinit_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(258, 523);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(169, 13);
            this.label23.TabIndex = 14;
            this.label23.Text = "* Format times using 24-hour clock";
            // 
            // cmdNextSkedDate
            // 
            this.cmdNextSkedDate.Location = new System.Drawing.Point(544, 93);
            this.cmdNextSkedDate.Name = "cmdNextSkedDate";
            this.cmdNextSkedDate.Size = new System.Drawing.Size(99, 22);
            this.cmdNextSkedDate.TabIndex = 25;
            this.cmdNextSkedDate.Text = "Next Sked Date";
            this.cmdNextSkedDate.UseVisualStyleBackColor = true;
            this.cmdNextSkedDate.Click += new System.EventHandler(this.cmdNextSkedDate_Click);
            // 
            // cmdShowAllScheduledDates
            // 
            this.cmdShowAllScheduledDates.Location = new System.Drawing.Point(544, 125);
            this.cmdShowAllScheduledDates.Name = "cmdShowAllScheduledDates";
            this.cmdShowAllScheduledDates.Size = new System.Drawing.Size(99, 22);
            this.cmdShowAllScheduledDates.TabIndex = 26;
            this.cmdShowAllScheduledDates.Text = "Show All Dates";
            this.cmdShowAllScheduledDates.UseVisualStyleBackColor = true;
            this.cmdShowAllScheduledDates.Click += new System.EventHandler(this.cmdShowAllScheduledDates_Click);
            // 
            // grpStorageType
            // 
            this.grpStorageType.Controls.Add(this.optDatabase);
            this.grpStorageType.Controls.Add(this.optXmlFiles);
            this.grpStorageType.Location = new System.Drawing.Point(261, 41);
            this.grpStorageType.Name = "grpStorageType";
            this.grpStorageType.Size = new System.Drawing.Size(197, 37);
            this.grpStorageType.TabIndex = 27;
            this.grpStorageType.TabStop = false;
            this.grpStorageType.Text = "Storage Type";
            // 
            // optDatabase
            // 
            this.optDatabase.AutoSize = true;
            this.optDatabase.Location = new System.Drawing.Point(120, 17);
            this.optDatabase.Name = "optDatabase";
            this.optDatabase.Size = new System.Drawing.Size(71, 17);
            this.optDatabase.TabIndex = 1;
            this.optDatabase.TabStop = true;
            this.optDatabase.Text = "Database";
            this.optDatabase.UseVisualStyleBackColor = true;
            // 
            // optXmlFiles
            // 
            this.optXmlFiles.AutoSize = true;
            this.optXmlFiles.Location = new System.Drawing.Point(25, 17);
            this.optXmlFiles.Name = "optXmlFiles";
            this.optXmlFiles.Size = new System.Drawing.Size(63, 17);
            this.optXmlFiles.TabIndex = 0;
            this.optXmlFiles.TabStop = true;
            this.optXmlFiles.Text = "XmlFiles";
            this.optXmlFiles.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AcceptButton = this.cmdRunTest;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(823, 577);
            this.Controls.Add(this.grpStorageType);
            this.Controls.Add(this.cmdShowAllScheduledDates);
            this.Controls.Add(this.cmdNextSkedDate);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.cmdReinit);
            this.Controls.Add(this.cboExpectedTestResult);
            this.Controls.Add(this.txtCurrDateTime);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.cmdLoadSchedule);
            this.Controls.Add(this.cmdSaveSchedule);
            this.Controls.Add(this.grpMonthlyScheduleParameters);
            this.Controls.Add(this.grpWeeklyScheduleParameters);
            this.Controls.Add(this.grpDailyScheduleParameters);
            this.Controls.Add(this.txtRunWindowInMinutes);
            this.Controls.Add(this.grpRunOneTimeParameters);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboScheduleFrequency);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtScheduleName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtScheduleEnd);
            this.Controls.Add(this.txtScheduleStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
            this.grpRunOneTimeParameters.ResumeLayout(false);
            this.grpRunOneTimeParameters.PerformLayout();
            this.grpDailyRunOnceParameters.ResumeLayout(false);
            this.grpDailyRunOnceParameters.PerformLayout();
            this.grpDailyScheduleParameters.ResumeLayout(false);
            this.grpDailyScheduleParameters.PerformLayout();
            this.grpDailyRecurringParameters.ResumeLayout(false);
            this.grpDailyRecurringParameters.PerformLayout();
            this.grpWeeklyScheduleParameters.ResumeLayout(false);
            this.grpWeeklyScheduleParameters.PerformLayout();
            this.grpMonthlyScheduleParameters.ResumeLayout(false);
            this.grpMonthlyScheduleParameters.PerformLayout();
            this.grpMonthOccursEvery.ResumeLayout(false);
            this.grpMonthOccursEvery.PerformLayout();
            this.grpStorageType.ResumeLayout(false);
            this.grpStorageType.PerformLayout();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox grpRunOneTimeParameters;
        private System.Windows.Forms.GroupBox grpDailyRunOnceParameters;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox grpDailyScheduleParameters;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox grpDailyRecurringParameters;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox grpWeeklyScheduleParameters;
        private System.Windows.Forms.GroupBox grpMonthlyScheduleParameters;
        internal System.Windows.Forms.RadioButton optDailyRecurring;
        internal System.Windows.Forms.RadioButton optDailyRunOnce;
        internal System.Windows.Forms.TextBox txtScheduleRunsEveryNumDays;
        internal System.Windows.Forms.ComboBox cboDailyOccursInterval;
        internal System.Windows.Forms.TextBox txtDailyOccursEveryIntervalNum;
        internal System.Windows.Forms.TextBox txtOccursEndsAt;
        internal System.Windows.Forms.TextBox txtOccursStartingAt;
        internal System.Windows.Forms.TextBox txtScheduleStart;
        internal System.Windows.Forms.TextBox txtScheduleEnd;
        internal System.Windows.Forms.TextBox txtScheduleName;
        internal System.Windows.Forms.ComboBox cboScheduleFrequency;
        internal System.Windows.Forms.TextBox txtRunOnceAt;
        internal System.Windows.Forms.TextBox txtDailyRunOnceAt;
        internal System.Windows.Forms.TextBox txtRunWindowInMinutes;
        private System.Windows.Forms.Button cmdSaveSchedule;
        private System.Windows.Forms.Button cmdLoadSchedule;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        internal System.Windows.Forms.TextBox txtCurrDateTime;
        internal System.Windows.Forms.ComboBox cboExpectedTestResult;
        internal System.Windows.Forms.TextBox txtWeeklyRecursEveryNumDays;
        internal System.Windows.Forms.ComboBox cboMonthlyDayNameOrdinal;
        internal System.Windows.Forms.TextBox txtMonthlyDayNumber;
        internal System.Windows.Forms.TextBox txtMonthlyOccursIntervalNum;
        internal System.Windows.Forms.ComboBox cboMonthlyDayName;
        internal System.Windows.Forms.CheckBox chkWeeklyTuesday;
        internal System.Windows.Forms.CheckBox chkWeeklyMonday;
        internal System.Windows.Forms.CheckBox chkWeeklySunday;
        internal System.Windows.Forms.CheckBox chkWeeklySaturday;
        internal System.Windows.Forms.CheckBox chkWeeklyFriday;
        internal System.Windows.Forms.CheckBox chkWeeklyThursday;
        internal System.Windows.Forms.CheckBox chkWeeklyWednesday;
        internal System.Windows.Forms.RadioButton optMonthlyDayName;
        internal System.Windows.Forms.RadioButton optMonthlyDayNumber;
        private System.Windows.Forms.Button cmdReinit;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.GroupBox grpMonthOccursEvery;
        internal System.Windows.Forms.RadioButton optOccursDuringMonthName;
        internal System.Windows.Forms.RadioButton optOccursEveryMonthNum;
        internal System.Windows.Forms.CheckBox chkMonthlyDec;
        internal System.Windows.Forms.CheckBox chkMonthlyNov;
        internal System.Windows.Forms.CheckBox chkMonthlyOct;
        internal System.Windows.Forms.CheckBox chkMonthlySep;
        internal System.Windows.Forms.CheckBox chkMonthlyAug;
        internal System.Windows.Forms.CheckBox chkMonthlyJul;
        internal System.Windows.Forms.CheckBox chkMonthlyJun;
        internal System.Windows.Forms.CheckBox chkMonthlyMay;
        internal System.Windows.Forms.CheckBox chkMonthlyApr;
        internal System.Windows.Forms.CheckBox chkMonthlyMar;
        internal System.Windows.Forms.CheckBox chkMonthlyFeb;
        internal System.Windows.Forms.CheckBox chkMonthlyJan;
        private System.Windows.Forms.Button cmdNextSkedDate;
        private System.Windows.Forms.Button cmdShowAllScheduledDates;
        private System.Windows.Forms.GroupBox grpStorageType;
        private System.Windows.Forms.RadioButton optDatabase;
        private System.Windows.Forms.RadioButton optXmlFiles;
    }
}

