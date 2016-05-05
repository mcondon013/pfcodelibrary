namespace TestprogFileSystemObjects
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
            this.txtDeleteFilesFileSpec = new System.Windows.Forms.TextBox();
            this.chkIncludeFilesInSubfolders = new System.Windows.Forms.CheckBox();
            this.chkDeleteSubfoldersTest = new System.Windows.Forms.CheckBox();
            this.txtDeletePath = new System.Windows.Forms.TextBox();
            this.chkDeleteFilesTest = new System.Windows.Forms.CheckBox();
            this.chkPreserveTimestamps = new System.Windows.Forms.CheckBox();
            this.txtSearchPattern = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkDeleteDestinationBeforeCopy = new System.Windows.Forms.CheckBox();
            this.cmdSetSourceDirectory = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSourceDirectory = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdSetDestinationDirectory = new System.Windows.Forms.Button();
            this.txtDestinationDirectory = new System.Windows.Forms.TextBox();
            this.chkCopyDirectoryTest = new System.Windows.Forms.CheckBox();
            this.chkPFTempFileCollectionTest = new System.Windows.Forms.CheckBox();
            this.chkPFTempFileTest = new System.Windows.Forms.CheckBox();
            this.chkPFDirectoryStatsTest = new System.Windows.Forms.CheckBox();
            this.chkPFFileExAttributesTest = new System.Windows.Forms.CheckBox();
            this.chkValidPathAndFileTest = new System.Windows.Forms.CheckBox();
            this.chkFileNtfsEncryptDecryptTest = new System.Windows.Forms.CheckBox();
            this.chkReadListsTest = new System.Windows.Forms.CheckBox();
            this.chkGetDirectoryTree = new System.Windows.Forms.CheckBox();
            this.cmdGetDirectoryToProcessPath = new System.Windows.Forms.Button();
            this.txtDirectoryToProcess = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkPFDirectoryExTest = new System.Windows.Forms.CheckBox();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.cmdShowHideOutputLog = new System.Windows.Forms.Button();
            this.chkUncompressFileTest = new System.Windows.Forms.CheckBox();
            this.chkCompressFileTest = new System.Windows.Forms.CheckBox();
            this.chkUncompressDirectoryTest = new System.Windows.Forms.CheckBox();
            this.chkCompressDirectoryTest = new System.Windows.Forms.CheckBox();
            this.MainMenu.SuspendLayout();
            this.grpTestsToRun.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(659, 490);
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
            this.cmdRunTests.Location = new System.Drawing.Point(659, 60);
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
            this.MainMenu.Size = new System.Drawing.Size(818, 24);
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
            // grpTestsToRun
            // 
            this.grpTestsToRun.Controls.Add(this.chkUncompressFileTest);
            this.grpTestsToRun.Controls.Add(this.chkCompressFileTest);
            this.grpTestsToRun.Controls.Add(this.chkUncompressDirectoryTest);
            this.grpTestsToRun.Controls.Add(this.chkCompressDirectoryTest);
            this.grpTestsToRun.Controls.Add(this.label5);
            this.grpTestsToRun.Controls.Add(this.txtDeleteFilesFileSpec);
            this.grpTestsToRun.Controls.Add(this.chkIncludeFilesInSubfolders);
            this.grpTestsToRun.Controls.Add(this.chkDeleteSubfoldersTest);
            this.grpTestsToRun.Controls.Add(this.txtDeletePath);
            this.grpTestsToRun.Controls.Add(this.chkDeleteFilesTest);
            this.grpTestsToRun.Controls.Add(this.chkPreserveTimestamps);
            this.grpTestsToRun.Controls.Add(this.txtSearchPattern);
            this.grpTestsToRun.Controls.Add(this.label4);
            this.grpTestsToRun.Controls.Add(this.chkDeleteDestinationBeforeCopy);
            this.grpTestsToRun.Controls.Add(this.cmdSetSourceDirectory);
            this.grpTestsToRun.Controls.Add(this.label3);
            this.grpTestsToRun.Controls.Add(this.txtSourceDirectory);
            this.grpTestsToRun.Controls.Add(this.label2);
            this.grpTestsToRun.Controls.Add(this.cmdSetDestinationDirectory);
            this.grpTestsToRun.Controls.Add(this.txtDestinationDirectory);
            this.grpTestsToRun.Controls.Add(this.chkCopyDirectoryTest);
            this.grpTestsToRun.Controls.Add(this.chkPFTempFileCollectionTest);
            this.grpTestsToRun.Controls.Add(this.chkPFTempFileTest);
            this.grpTestsToRun.Controls.Add(this.chkPFDirectoryStatsTest);
            this.grpTestsToRun.Controls.Add(this.chkPFFileExAttributesTest);
            this.grpTestsToRun.Controls.Add(this.chkValidPathAndFileTest);
            this.grpTestsToRun.Controls.Add(this.chkFileNtfsEncryptDecryptTest);
            this.grpTestsToRun.Controls.Add(this.chkReadListsTest);
            this.grpTestsToRun.Controls.Add(this.chkGetDirectoryTree);
            this.grpTestsToRun.Controls.Add(this.cmdGetDirectoryToProcessPath);
            this.grpTestsToRun.Controls.Add(this.txtDirectoryToProcess);
            this.grpTestsToRun.Controls.Add(this.label1);
            this.grpTestsToRun.Controls.Add(this.chkPFDirectoryExTest);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 60);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(580, 534);
            this.grpTestsToRun.TabIndex = 0;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(344, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 113;
            this.label5.Text = "File Spec:";
            // 
            // txtDeleteFilesFileSpec
            // 
            this.txtDeleteFilesFileSpec.Location = new System.Drawing.Point(414, 159);
            this.txtDeleteFilesFileSpec.Name = "txtDeleteFilesFileSpec";
            this.txtDeleteFilesFileSpec.Size = new System.Drawing.Size(108, 20);
            this.txtDeleteFilesFileSpec.TabIndex = 112;
            // 
            // chkIncludeFilesInSubfolders
            // 
            this.chkIncludeFilesInSubfolders.AutoSize = true;
            this.chkIncludeFilesInSubfolders.Location = new System.Drawing.Point(344, 136);
            this.chkIncludeFilesInSubfolders.Name = "chkIncludeFilesInSubfolders";
            this.chkIncludeFilesInSubfolders.Size = new System.Drawing.Size(149, 17);
            this.chkIncludeFilesInSubfolders.TabIndex = 111;
            this.chkIncludeFilesInSubfolders.Text = "Include Files in Subfolders";
            this.chkIncludeFilesInSubfolders.UseVisualStyleBackColor = true;
            // 
            // chkDeleteSubfoldersTest
            // 
            this.chkDeleteSubfoldersTest.AutoSize = true;
            this.chkDeleteSubfoldersTest.Location = new System.Drawing.Point(341, 226);
            this.chkDeleteSubfoldersTest.Name = "chkDeleteSubfoldersTest";
            this.chkDeleteSubfoldersTest.Size = new System.Drawing.Size(144, 17);
            this.chkDeleteSubfoldersTest.TabIndex = 110;
            this.chkDeleteSubfoldersTest.Text = "&B Delete Subfolders Test";
            this.chkDeleteSubfoldersTest.UseVisualStyleBackColor = true;
            // 
            // txtDeletePath
            // 
            this.txtDeletePath.BackColor = System.Drawing.SystemColors.Window;
            this.txtDeletePath.Location = new System.Drawing.Point(341, 106);
            this.txtDeletePath.Name = "txtDeletePath";
            this.txtDeletePath.ReadOnly = true;
            this.txtDeletePath.Size = new System.Drawing.Size(215, 20);
            this.txtDeletePath.TabIndex = 109;
            // 
            // chkDeleteFilesTest
            // 
            this.chkDeleteFilesTest.AutoSize = true;
            this.chkDeleteFilesTest.Location = new System.Drawing.Point(341, 194);
            this.chkDeleteFilesTest.Name = "chkDeleteFilesTest";
            this.chkDeleteFilesTest.Size = new System.Drawing.Size(115, 17);
            this.chkDeleteFilesTest.TabIndex = 108;
            this.chkDeleteFilesTest.Text = "&A Delete Files Test";
            this.chkDeleteFilesTest.UseVisualStyleBackColor = true;
            // 
            // chkPreserveTimestamps
            // 
            this.chkPreserveTimestamps.AutoSize = true;
            this.chkPreserveTimestamps.Location = new System.Drawing.Point(252, 498);
            this.chkPreserveTimestamps.Name = "chkPreserveTimestamps";
            this.chkPreserveTimestamps.Size = new System.Drawing.Size(127, 17);
            this.chkPreserveTimestamps.TabIndex = 107;
            this.chkPreserveTimestamps.Text = "Preserve Timestamps";
            this.chkPreserveTimestamps.UseVisualStyleBackColor = true;
            // 
            // txtSearchPattern
            // 
            this.txtSearchPattern.Location = new System.Drawing.Point(252, 390);
            this.txtSearchPattern.Name = "txtSearchPattern";
            this.txtSearchPattern.Size = new System.Drawing.Size(127, 20);
            this.txtSearchPattern.TabIndex = 106;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(164, 394);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 105;
            this.label4.Text = "Search Pattern:";
            // 
            // chkDeleteDestinationBeforeCopy
            // 
            this.chkDeleteDestinationBeforeCopy.AutoSize = true;
            this.chkDeleteDestinationBeforeCopy.Location = new System.Drawing.Point(112, 498);
            this.chkDeleteDestinationBeforeCopy.Name = "chkDeleteDestinationBeforeCopy";
            this.chkDeleteDestinationBeforeCopy.Size = new System.Drawing.Size(118, 17);
            this.chkDeleteDestinationBeforeCopy.TabIndex = 104;
            this.chkDeleteDestinationBeforeCopy.Text = "Delete Before Copy";
            this.chkDeleteDestinationBeforeCopy.UseVisualStyleBackColor = true;
            // 
            // cmdSetSourceDirectory
            // 
            this.cmdSetSourceDirectory.Location = new System.Drawing.Point(341, 415);
            this.cmdSetSourceDirectory.Name = "cmdSetSourceDirectory";
            this.cmdSetSourceDirectory.Size = new System.Drawing.Size(38, 20);
            this.cmdSetSourceDirectory.TabIndex = 103;
            this.cmdSetSourceDirectory.Text = "•••";
            this.cmdSetSourceDirectory.UseVisualStyleBackColor = true;
            this.cmdSetSourceDirectory.Click += new System.EventHandler(this.cmdSetSourceDirectory_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 419);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 102;
            this.label3.Text = "Source Directory";
            // 
            // txtSourceDirectory
            // 
            this.txtSourceDirectory.Location = new System.Drawing.Point(38, 432);
            this.txtSourceDirectory.Name = "txtSourceDirectory";
            this.txtSourceDirectory.Size = new System.Drawing.Size(341, 20);
            this.txtSourceDirectory.TabIndex = 101;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 464);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 100;
            this.label2.Text = "Destination Directory";
            // 
            // cmdSetDestinationDirectory
            // 
            this.cmdSetDestinationDirectory.Location = new System.Drawing.Point(341, 460);
            this.cmdSetDestinationDirectory.Name = "cmdSetDestinationDirectory";
            this.cmdSetDestinationDirectory.Size = new System.Drawing.Size(38, 20);
            this.cmdSetDestinationDirectory.TabIndex = 99;
            this.cmdSetDestinationDirectory.Text = "•••";
            this.cmdSetDestinationDirectory.UseVisualStyleBackColor = true;
            this.cmdSetDestinationDirectory.Click += new System.EventHandler(this.cmdSetDestinationDirectory_Click);
            // 
            // txtDestinationDirectory
            // 
            this.txtDestinationDirectory.Location = new System.Drawing.Point(35, 478);
            this.txtDestinationDirectory.Name = "txtDestinationDirectory";
            this.txtDestinationDirectory.Size = new System.Drawing.Size(344, 20);
            this.txtDestinationDirectory.TabIndex = 98;
            // 
            // chkCopyDirectoryTest
            // 
            this.chkCopyDirectoryTest.AutoSize = true;
            this.chkCopyDirectoryTest.Location = new System.Drawing.Point(16, 390);
            this.chkCopyDirectoryTest.Name = "chkCopyDirectoryTest";
            this.chkCopyDirectoryTest.Size = new System.Drawing.Size(128, 17);
            this.chkCopyDirectoryTest.TabIndex = 97;
            this.chkCopyDirectoryTest.Text = "&9 Copy Directory Test";
            this.chkCopyDirectoryTest.UseVisualStyleBackColor = true;
            // 
            // chkPFTempFileCollectionTest
            // 
            this.chkPFTempFileCollectionTest.AutoSize = true;
            this.chkPFTempFileCollectionTest.Location = new System.Drawing.Point(16, 355);
            this.chkPFTempFileCollectionTest.Name = "chkPFTempFileCollectionTest";
            this.chkPFTempFileCollectionTest.Size = new System.Drawing.Size(161, 17);
            this.chkPFTempFileCollectionTest.TabIndex = 96;
            this.chkPFTempFileCollectionTest.Text = "&8 PFTempFileCollection Test";
            this.chkPFTempFileCollectionTest.UseVisualStyleBackColor = true;
            // 
            // chkPFTempFileTest
            // 
            this.chkPFTempFileTest.AutoSize = true;
            this.chkPFTempFileTest.Location = new System.Drawing.Point(16, 319);
            this.chkPFTempFileTest.Name = "chkPFTempFileTest";
            this.chkPFTempFileTest.Size = new System.Drawing.Size(115, 17);
            this.chkPFTempFileTest.TabIndex = 95;
            this.chkPFTempFileTest.Text = "&7 PFTempFile Test";
            this.chkPFTempFileTest.UseVisualStyleBackColor = true;
            // 
            // chkPFDirectoryStatsTest
            // 
            this.chkPFDirectoryStatsTest.AutoSize = true;
            this.chkPFDirectoryStatsTest.Location = new System.Drawing.Point(16, 285);
            this.chkPFDirectoryStatsTest.Name = "chkPFDirectoryStatsTest";
            this.chkPFDirectoryStatsTest.Size = new System.Drawing.Size(138, 17);
            this.chkPFDirectoryStatsTest.TabIndex = 94;
            this.chkPFDirectoryStatsTest.Text = "&6 PFDirectoryStats Test";
            this.chkPFDirectoryStatsTest.UseVisualStyleBackColor = true;
            // 
            // chkPFFileExAttributesTest
            // 
            this.chkPFFileExAttributesTest.AutoSize = true;
            this.chkPFFileExAttributesTest.Location = new System.Drawing.Point(16, 248);
            this.chkPFFileExAttributesTest.Name = "chkPFFileExAttributesTest";
            this.chkPFFileExAttributesTest.Size = new System.Drawing.Size(147, 17);
            this.chkPFFileExAttributesTest.TabIndex = 93;
            this.chkPFFileExAttributesTest.Text = "&5 PFFileEx Attributes Test";
            this.chkPFFileExAttributesTest.UseVisualStyleBackColor = true;
            // 
            // chkValidPathAndFileTest
            // 
            this.chkValidPathAndFileTest.AutoSize = true;
            this.chkValidPathAndFileTest.Location = new System.Drawing.Point(19, 210);
            this.chkValidPathAndFileTest.Name = "chkValidPathAndFileTest";
            this.chkValidPathAndFileTest.Size = new System.Drawing.Size(136, 17);
            this.chkValidPathAndFileTest.TabIndex = 92;
            this.chkValidPathAndFileTest.Text = "&4 ValidPathAndFileTest";
            this.chkValidPathAndFileTest.UseVisualStyleBackColor = true;
            // 
            // chkFileNtfsEncryptDecryptTest
            // 
            this.chkFileNtfsEncryptDecryptTest.AutoSize = true;
            this.chkFileNtfsEncryptDecryptTest.Location = new System.Drawing.Point(19, 175);
            this.chkFileNtfsEncryptDecryptTest.Name = "chkFileNtfsEncryptDecryptTest";
            this.chkFileNtfsEncryptDecryptTest.Size = new System.Drawing.Size(185, 17);
            this.chkFileNtfsEncryptDecryptTest.TabIndex = 91;
            this.chkFileNtfsEncryptDecryptTest.Text = "&3 File NTFS Encrypt Decrypt Test";
            this.chkFileNtfsEncryptDecryptTest.UseVisualStyleBackColor = true;
            // 
            // chkReadListsTest
            // 
            this.chkReadListsTest.AutoSize = true;
            this.chkReadListsTest.Location = new System.Drawing.Point(16, 140);
            this.chkReadListsTest.Name = "chkReadListsTest";
            this.chkReadListsTest.Size = new System.Drawing.Size(109, 17);
            this.chkReadListsTest.TabIndex = 90;
            this.chkReadListsTest.Text = "&2 Read Lists Test";
            this.chkReadListsTest.UseVisualStyleBackColor = true;
            // 
            // chkGetDirectoryTree
            // 
            this.chkGetDirectoryTree.AutoSize = true;
            this.chkGetDirectoryTree.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkGetDirectoryTree.Location = new System.Drawing.Point(443, 72);
            this.chkGetDirectoryTree.Name = "chkGetDirectoryTree";
            this.chkGetDirectoryTree.Size = new System.Drawing.Size(113, 17);
            this.chkGetDirectoryTree.TabIndex = 89;
            this.chkGetDirectoryTree.Text = "Get Directory Tree";
            this.chkGetDirectoryTree.UseVisualStyleBackColor = true;
            // 
            // cmdGetDirectoryToProcessPath
            // 
            this.cmdGetDirectoryToProcessPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetDirectoryToProcessPath.Location = new System.Drawing.Point(518, 26);
            this.cmdGetDirectoryToProcessPath.Name = "cmdGetDirectoryToProcessPath";
            this.cmdGetDirectoryToProcessPath.Size = new System.Drawing.Size(38, 20);
            this.cmdGetDirectoryToProcessPath.TabIndex = 88;
            this.cmdGetDirectoryToProcessPath.Text = "•••";
            this.cmdGetDirectoryToProcessPath.UseVisualStyleBackColor = true;
            this.cmdGetDirectoryToProcessPath.Click += new System.EventHandler(this.cmdGetDirectoryToProcessPath_Click);
            // 
            // txtDirectoryToProcess
            // 
            this.txtDirectoryToProcess.Location = new System.Drawing.Point(16, 46);
            this.txtDirectoryToProcess.Name = "txtDirectoryToProcess";
            this.txtDirectoryToProcess.Size = new System.Drawing.Size(540, 20);
            this.txtDirectoryToProcess.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Directory to Process:";
            // 
            // chkPFDirectoryExTest
            // 
            this.chkPFDirectoryExTest.AutoSize = true;
            this.chkPFDirectoryExTest.Location = new System.Drawing.Point(16, 106);
            this.chkPFDirectoryExTest.Name = "chkPFDirectoryExTest";
            this.chkPFDirectoryExTest.Size = new System.Drawing.Size(126, 17);
            this.chkPFDirectoryExTest.TabIndex = 0;
            this.chkPFDirectoryExTest.Text = "&1 PFDirectoryEx Test";
            this.chkPFDirectoryExTest.UseVisualStyleBackColor = true;
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(659, 119);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(91, 43);
            this.chkEraseOutputBeforeEachTest.TabIndex = 8;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output \r\nBefore Each \r\nTest is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogFileSystemObjects\\TestprogFileSyst" +
    "emObjects\\InitWinFormsHelpFile.chm";
            // 
            // cmdShowHideOutputLog
            // 
            this.cmdShowHideOutputLog.Location = new System.Drawing.Point(659, 259);
            this.cmdShowHideOutputLog.Name = "cmdShowHideOutputLog";
            this.cmdShowHideOutputLog.Size = new System.Drawing.Size(93, 44);
            this.cmdShowHideOutputLog.TabIndex = 9;
            this.cmdShowHideOutputLog.Text = "Show/Hide\r\nOutput Log";
            this.cmdShowHideOutputLog.UseVisualStyleBackColor = true;
            this.cmdShowHideOutputLog.Click += new System.EventHandler(this.cmdShowHideOutputLog_Click);
            // 
            // chkUncompressFileTest
            // 
            this.chkUncompressFileTest.AutoSize = true;
            this.chkUncompressFileTest.Location = new System.Drawing.Point(341, 342);
            this.chkUncompressFileTest.Name = "chkUncompressFileTest";
            this.chkUncompressFileTest.Size = new System.Drawing.Size(128, 17);
            this.chkUncompressFileTest.TabIndex = 117;
            this.chkUncompressFileTest.Text = "Uncompress File Test";
            this.chkUncompressFileTest.UseVisualStyleBackColor = true;
            // 
            // chkCompressFileTest
            // 
            this.chkCompressFileTest.AutoSize = true;
            this.chkCompressFileTest.Location = new System.Drawing.Point(341, 319);
            this.chkCompressFileTest.Name = "chkCompressFileTest";
            this.chkCompressFileTest.Size = new System.Drawing.Size(115, 17);
            this.chkCompressFileTest.TabIndex = 116;
            this.chkCompressFileTest.Text = "Compress File Test";
            this.chkCompressFileTest.UseVisualStyleBackColor = true;
            // 
            // chkUncompressDirectoryTest
            // 
            this.chkUncompressDirectoryTest.AutoSize = true;
            this.chkUncompressDirectoryTest.Location = new System.Drawing.Point(341, 285);
            this.chkUncompressDirectoryTest.Name = "chkUncompressDirectoryTest";
            this.chkUncompressDirectoryTest.Size = new System.Drawing.Size(154, 17);
            this.chkUncompressDirectoryTest.TabIndex = 115;
            this.chkUncompressDirectoryTest.Text = "Uncompress Directory Test";
            this.chkUncompressDirectoryTest.UseVisualStyleBackColor = true;
            // 
            // chkCompressDirectoryTest
            // 
            this.chkCompressDirectoryTest.AutoSize = true;
            this.chkCompressDirectoryTest.Location = new System.Drawing.Point(341, 263);
            this.chkCompressDirectoryTest.Name = "chkCompressDirectoryTest";
            this.chkCompressDirectoryTest.Size = new System.Drawing.Size(144, 17);
            this.chkCompressDirectoryTest.TabIndex = 114;
            this.chkCompressDirectoryTest.Text = "Comprress Directory Test";
            this.chkCompressDirectoryTest.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AcceptButton = this.cmdRunTests;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(818, 617);
            this.Controls.Add(this.cmdShowHideOutputLog);
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
        private System.Windows.Forms.CheckBox chkPFDirectoryExTest;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        internal System.Windows.Forms.TextBox txtDirectoryToProcess;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdGetDirectoryToProcessPath;
        internal System.Windows.Forms.CheckBox chkGetDirectoryTree;
        private System.Windows.Forms.CheckBox chkReadListsTest;
        private System.Windows.Forms.CheckBox chkFileNtfsEncryptDecryptTest;
        private System.Windows.Forms.CheckBox chkValidPathAndFileTest;
        private System.Windows.Forms.CheckBox chkPFFileExAttributesTest;
        private System.Windows.Forms.CheckBox chkPFDirectoryStatsTest;
        private System.Windows.Forms.CheckBox chkPFTempFileCollectionTest;
        private System.Windows.Forms.CheckBox chkPFTempFileTest;
        private System.Windows.Forms.Button cmdSetDestinationDirectory;
        private System.Windows.Forms.CheckBox chkCopyDirectoryTest;
        private System.Windows.Forms.Button cmdSetSourceDirectory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox txtDestinationDirectory;
        internal System.Windows.Forms.TextBox txtSourceDirectory;
        internal System.Windows.Forms.CheckBox chkDeleteDestinationBeforeCopy;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtSearchPattern;
        internal System.Windows.Forms.CheckBox chkPreserveTimestamps;
        private System.Windows.Forms.CheckBox chkDeleteSubfoldersTest;
        private System.Windows.Forms.CheckBox chkDeleteFilesTest;
        internal System.Windows.Forms.TextBox txtDeletePath;
        internal System.Windows.Forms.CheckBox chkIncludeFilesInSubfolders;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox txtDeleteFilesFileSpec;
        private System.Windows.Forms.Button cmdShowHideOutputLog;
        private System.Windows.Forms.CheckBox chkUncompressFileTest;
        private System.Windows.Forms.CheckBox chkCompressFileTest;
        private System.Windows.Forms.CheckBox chkUncompressDirectoryTest;
        private System.Windows.Forms.CheckBox chkCompressDirectoryTest;
    }
}

