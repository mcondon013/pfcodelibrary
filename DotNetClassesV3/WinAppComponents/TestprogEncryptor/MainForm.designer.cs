namespace pfEncryptor
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
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.cmdExit = new System.Windows.Forms.Button();
            this.lblEncryptionOperation = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optEncryptString = new System.Windows.Forms.RadioButton();
            this.optEncryptFile = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.grpEncryptionAlgorithm = new System.Windows.Forms.GroupBox();
            this.optEncryptAES = new System.Windows.Forms.RadioButton();
            this.optEncryptTripleDES = new System.Windows.Forms.RadioButton();
            this.optEncryptDES = new System.Windows.Forms.RadioButton();
            this.mainFormToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.optDecryptAES = new System.Windows.Forms.RadioButton();
            this.optDecryptTripleDES = new System.Windows.Forms.RadioButton();
            this.optDecryptDES = new System.Windows.Forms.RadioButton();
            this.chkBinaryEncryption = new System.Windows.Forms.CheckBox();
            this.chkBinaryDecryption = new System.Windows.Forms.CheckBox();
            this.cmdEncrypt = new System.Windows.Forms.Button();
            this.cmdDecrypt = new System.Windows.Forms.Button();
            this.txtEncryptionIV = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEncryptionKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdSaveEncryptionDef = new System.Windows.Forms.Button();
            this.cmdLoadEncryptionDef = new System.Windows.Forms.Button();
            this.cmdNewEncryptionDef = new System.Windows.Forms.Button();
            this.cmdGenerateRandomKeyIV = new System.Windows.Forms.Button();
            this.cmdEncryptionLoadKeyIVFromFile = new System.Windows.Forms.Button();
            this.cmdEncryptionSaveKeyIVToFile = new System.Windows.Forms.Button();
            this.cmdDecryptionSaveKeyIVToFile = new System.Windows.Forms.Button();
            this.cmdCopyKeyIVFromAbove = new System.Windows.Forms.Button();
            this.cmdDecryptionLoadKeyIVFromFile = new System.Windows.Forms.Button();
            this.txtDecryptionKey = new System.Windows.Forms.TextBox();
            this.txtDecryptionIV = new System.Windows.Forms.TextBox();
            this.cmdSaveDecryptionDef = new System.Windows.Forms.Button();
            this.cmdLoadDecryptionDef = new System.Windows.Forms.Button();
            this.cmdNewDecryptionDef = new System.Windows.Forms.Button();
            this.lblEncryptionSource = new System.Windows.Forms.Label();
            this.txtEncryptionSource = new System.Windows.Forms.TextBox();
            this.cmdEncryptionSource = new System.Windows.Forms.Button();
            this.lblEncryptionTarget = new System.Windows.Forms.Label();
            this.txtEncryptionTarget = new System.Windows.Forms.TextBox();
            this.cmdEncryptionTarget = new System.Windows.Forms.Button();
            this.panelEncryption = new System.Windows.Forms.Panel();
            this.txtEncryptResult = new System.Windows.Forms.TextBox();
            this.grpSaveOutputTo = new System.Windows.Forms.GroupBox();
            this.optSaveEncryptedToString = new System.Windows.Forms.RadioButton();
            this.optSaveEncryptedToFile = new System.Windows.Forms.RadioButton();
            this.panelDecryption = new System.Windows.Forms.Panel();
            this.txtDecryptionResult = new System.Windows.Forms.TextBox();
            this.grpSaveDecryptionOutputTo = new System.Windows.Forms.GroupBox();
            this.optSaveDecryptedToString = new System.Windows.Forms.RadioButton();
            this.optSaveDecryptedToFile = new System.Windows.Forms.RadioButton();
            this.txtDecryptionTarget = new System.Windows.Forms.TextBox();
            this.lblDecryptionTarget = new System.Windows.Forms.Label();
            this.lblDecryptionOperation = new System.Windows.Forms.Label();
            this.cmdDecryptionTarget = new System.Windows.Forms.Button();
            this.cmdDecryptionSource = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.optDecryptString = new System.Windows.Forms.RadioButton();
            this.optDecryptFile = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDecryptionSource = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblDecryptionSource = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.MainMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpEncryptionAlgorithm.SuspendLayout();
            this.panelEncryption.SuspendLayout();
            this.grpSaveOutputTo.SuspendLayout();
            this.panelDecryption.SuspendLayout();
            this.grpSaveDecryptionOutputTo.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(666, 24);
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
            this.mnuFileExit.Size = new System.Drawing.Size(92, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\pfEncryptor\\InitWinFormsAppWithUserAndAppS" +
    "ettings\\InitWinFormsHelpFile.chm";
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(547, 606);
            this.cmdExit.Name = "cmdExit";
            this.appHelpProvider.SetShowHelp(this.cmdExit, true);
            this.cmdExit.Size = new System.Drawing.Size(68, 37);
            this.cmdExit.TabIndex = 41;
            this.cmdExit.Text = "E&xit";
            this.mainFormToolTips.SetToolTip(this.cmdExit, "Close form and exit application.");
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // lblEncryptionOperation
            // 
            this.lblEncryptionOperation.AutoSize = true;
            this.lblEncryptionOperation.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncryptionOperation.Location = new System.Drawing.Point(21, 11);
            this.lblEncryptionOperation.Name = "lblEncryptionOperation";
            this.lblEncryptionOperation.Size = new System.Drawing.Size(65, 18);
            this.lblEncryptionOperation.TabIndex = 5;
            this.lblEncryptionOperation.Text = "Encrypt";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optEncryptString);
            this.groupBox1.Controls.Add(this.optEncryptFile);
            this.groupBox1.Location = new System.Drawing.Point(97, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(130, 25);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // optEncryptString
            // 
            this.optEncryptString.AutoSize = true;
            this.optEncryptString.Location = new System.Drawing.Point(67, 10);
            this.optEncryptString.Name = "optEncryptString";
            this.optEncryptString.Size = new System.Drawing.Size(52, 17);
            this.optEncryptString.TabIndex = 1;
            this.optEncryptString.TabStop = true;
            this.optEncryptString.Text = "String";
            this.optEncryptString.UseVisualStyleBackColor = true;
            this.optEncryptString.CheckedChanged += new System.EventHandler(this.optEncryptString_CheckedChanged);
            // 
            // optEncryptFile
            // 
            this.optEncryptFile.AutoSize = true;
            this.optEncryptFile.Checked = true;
            this.optEncryptFile.Location = new System.Drawing.Point(6, 10);
            this.optEncryptFile.Name = "optEncryptFile";
            this.optEncryptFile.Size = new System.Drawing.Size(41, 17);
            this.optEncryptFile.TabIndex = 0;
            this.optEncryptFile.TabStop = true;
            this.optEncryptFile.Text = "File";
            this.optEncryptFile.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(233, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "using";
            // 
            // grpEncryptionAlgorithm
            // 
            this.grpEncryptionAlgorithm.Controls.Add(this.optEncryptAES);
            this.grpEncryptionAlgorithm.Controls.Add(this.optEncryptTripleDES);
            this.grpEncryptionAlgorithm.Controls.Add(this.optEncryptDES);
            this.grpEncryptionAlgorithm.Location = new System.Drawing.Point(271, 5);
            this.grpEncryptionAlgorithm.Name = "grpEncryptionAlgorithm";
            this.grpEncryptionAlgorithm.Size = new System.Drawing.Size(184, 33);
            this.grpEncryptionAlgorithm.TabIndex = 8;
            this.grpEncryptionAlgorithm.TabStop = false;
            // 
            // optEncryptAES
            // 
            this.optEncryptAES.AutoSize = true;
            this.optEncryptAES.Checked = true;
            this.optEncryptAES.Location = new System.Drawing.Point(15, 8);
            this.optEncryptAES.Name = "optEncryptAES";
            this.optEncryptAES.Size = new System.Drawing.Size(46, 17);
            this.optEncryptAES.TabIndex = 2;
            this.optEncryptAES.TabStop = true;
            this.optEncryptAES.Text = "AES";
            this.mainFormToolTips.SetToolTip(this.optEncryptAES, "Advanced Encryption Standard");
            this.optEncryptAES.UseVisualStyleBackColor = true;
            this.optEncryptAES.CheckedChanged += new System.EventHandler(this.optEncryptAES_CheckedChanged);
            // 
            // optEncryptTripleDES
            // 
            this.optEncryptTripleDES.AutoSize = true;
            this.optEncryptTripleDES.Location = new System.Drawing.Point(67, 8);
            this.optEncryptTripleDES.Name = "optEncryptTripleDES";
            this.optEncryptTripleDES.Size = new System.Drawing.Size(54, 17);
            this.optEncryptTripleDES.TabIndex = 1;
            this.optEncryptTripleDES.Text = "TDES";
            this.mainFormToolTips.SetToolTip(this.optEncryptTripleDES, "Triple Data Encryption Standard");
            this.optEncryptTripleDES.UseVisualStyleBackColor = true;
            this.optEncryptTripleDES.CheckedChanged += new System.EventHandler(this.optEncryptTripleDES_CheckedChanged);
            // 
            // optEncryptDES
            // 
            this.optEncryptDES.AutoSize = true;
            this.optEncryptDES.Location = new System.Drawing.Point(137, 8);
            this.optEncryptDES.Name = "optEncryptDES";
            this.optEncryptDES.Size = new System.Drawing.Size(47, 17);
            this.optEncryptDES.TabIndex = 0;
            this.optEncryptDES.Text = "DES";
            this.mainFormToolTips.SetToolTip(this.optEncryptDES, "Data Encryption Standard");
            this.optEncryptDES.UseVisualStyleBackColor = true;
            this.optEncryptDES.CheckedChanged += new System.EventHandler(this.optEncryptDES_CheckedChanged);
            // 
            // optDecryptAES
            // 
            this.optDecryptAES.AutoSize = true;
            this.optDecryptAES.Checked = true;
            this.optDecryptAES.Location = new System.Drawing.Point(15, 8);
            this.optDecryptAES.Name = "optDecryptAES";
            this.optDecryptAES.Size = new System.Drawing.Size(46, 17);
            this.optDecryptAES.TabIndex = 2;
            this.optDecryptAES.TabStop = true;
            this.optDecryptAES.Text = "AES";
            this.mainFormToolTips.SetToolTip(this.optDecryptAES, "Advanced Encryption Standard");
            this.optDecryptAES.UseVisualStyleBackColor = true;
            this.optDecryptAES.CheckedChanged += new System.EventHandler(this.optDecryptAES_CheckedChanged);
            // 
            // optDecryptTripleDES
            // 
            this.optDecryptTripleDES.AutoSize = true;
            this.optDecryptTripleDES.Location = new System.Drawing.Point(67, 8);
            this.optDecryptTripleDES.Name = "optDecryptTripleDES";
            this.optDecryptTripleDES.Size = new System.Drawing.Size(54, 17);
            this.optDecryptTripleDES.TabIndex = 1;
            this.optDecryptTripleDES.Text = "TDES";
            this.mainFormToolTips.SetToolTip(this.optDecryptTripleDES, "Triple Data Encryption Standard");
            this.optDecryptTripleDES.UseVisualStyleBackColor = true;
            this.optDecryptTripleDES.CheckedChanged += new System.EventHandler(this.optDecryptTripleDES_CheckedChanged);
            // 
            // optDecryptDES
            // 
            this.optDecryptDES.AutoSize = true;
            this.optDecryptDES.Location = new System.Drawing.Point(137, 8);
            this.optDecryptDES.Name = "optDecryptDES";
            this.optDecryptDES.Size = new System.Drawing.Size(47, 17);
            this.optDecryptDES.TabIndex = 0;
            this.optDecryptDES.Text = "DES";
            this.mainFormToolTips.SetToolTip(this.optDecryptDES, "Data Encryption Standard");
            this.optDecryptDES.UseVisualStyleBackColor = true;
            this.optDecryptDES.CheckedChanged += new System.EventHandler(this.optDecryptDES_CheckedChanged);
            // 
            // chkBinaryEncryption
            // 
            this.chkBinaryEncryption.AutoSize = true;
            this.chkBinaryEncryption.Location = new System.Drawing.Point(290, 181);
            this.chkBinaryEncryption.Name = "chkBinaryEncryption";
            this.chkBinaryEncryption.Size = new System.Drawing.Size(108, 17);
            this.chkBinaryEncryption.TabIndex = 24;
            this.chkBinaryEncryption.Text = "Binary Encryption";
            this.mainFormToolTips.SetToolTip(this.chkBinaryEncryption, "Encrypt the file using binary encryption. \r\nLeave unchecked for Base64 encoding o" +
        "f encryption.");
            this.chkBinaryEncryption.UseVisualStyleBackColor = true;
            this.chkBinaryEncryption.CheckedChanged += new System.EventHandler(this.chkBinaryEncryption_CheckedChanged);
            // 
            // chkBinaryDecryption
            // 
            this.chkBinaryDecryption.AutoSize = true;
            this.chkBinaryDecryption.Location = new System.Drawing.Point(290, 141);
            this.chkBinaryDecryption.Name = "chkBinaryDecryption";
            this.chkBinaryDecryption.Size = new System.Drawing.Size(108, 17);
            this.chkBinaryDecryption.TabIndex = 24;
            this.chkBinaryDecryption.Text = "Binary Encryption";
            this.mainFormToolTips.SetToolTip(this.chkBinaryDecryption, "If checked, the input file was encrypted using binary encryption only\r\nIf uncheck" +
        "ed, the encrypted values in the input file were encoded using Base64.");
            this.chkBinaryDecryption.UseVisualStyleBackColor = true;
            this.chkBinaryDecryption.CheckedChanged += new System.EventHandler(this.chkBinaryDecryption_CheckedChanged);
            // 
            // cmdEncrypt
            // 
            this.cmdEncrypt.Location = new System.Drawing.Point(518, 38);
            this.cmdEncrypt.Name = "cmdEncrypt";
            this.cmdEncrypt.Size = new System.Drawing.Size(126, 37);
            this.cmdEncrypt.TabIndex = 12;
            this.cmdEncrypt.Text = "&Encrypt";
            this.mainFormToolTips.SetToolTip(this.cmdEncrypt, "Run Encryption using currently displayed encryption definition.");
            this.cmdEncrypt.UseVisualStyleBackColor = true;
            this.cmdEncrypt.Click += new System.EventHandler(this.cmdEncrypt_Click);
            // 
            // cmdDecrypt
            // 
            this.cmdDecrypt.Location = new System.Drawing.Point(517, 342);
            this.cmdDecrypt.Name = "cmdDecrypt";
            this.cmdDecrypt.Size = new System.Drawing.Size(126, 37);
            this.cmdDecrypt.TabIndex = 27;
            this.cmdDecrypt.Text = "Decrypt";
            this.mainFormToolTips.SetToolTip(this.cmdDecrypt, "Run Decryption using currently displayed decryption definition.");
            this.cmdDecrypt.UseVisualStyleBackColor = true;
            this.cmdDecrypt.Click += new System.EventHandler(this.cmdDecrypt_Click);
            // 
            // txtEncryptionIV
            // 
            this.txtEncryptionIV.Location = new System.Drawing.Point(285, 33);
            this.txtEncryptionIV.Name = "txtEncryptionIV";
            this.txtEncryptionIV.Size = new System.Drawing.Size(170, 20);
            this.txtEncryptionIV.TabIndex = 20;
            this.mainFormToolTips.SetToolTip(this.txtEncryptionIV, "Initialization Vector value.");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(258, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "IV:";
            this.mainFormToolTips.SetToolTip(this.label3, "Initialization Vector value.");
            // 
            // txtEncryptionKey
            // 
            this.txtEncryptionKey.Location = new System.Drawing.Point(66, 33);
            this.txtEncryptionKey.Name = "txtEncryptionKey";
            this.txtEncryptionKey.Size = new System.Drawing.Size(170, 20);
            this.txtEncryptionKey.TabIndex = 18;
            this.mainFormToolTips.SetToolTip(this.txtEncryptionKey, "Encryption key value.");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Key:";
            this.mainFormToolTips.SetToolTip(this.label4, "Encryption key value.");
            // 
            // cmdSaveEncryptionDef
            // 
            this.cmdSaveEncryptionDef.Location = new System.Drawing.Point(460, 302);
            this.cmdSaveEncryptionDef.Name = "cmdSaveEncryptionDef";
            this.cmdSaveEncryptionDef.Size = new System.Drawing.Size(58, 23);
            this.cmdSaveEncryptionDef.TabIndex = 37;
            this.cmdSaveEncryptionDef.Text = "Save";
            this.mainFormToolTips.SetToolTip(this.cmdSaveEncryptionDef, "Save displayed encryption definition to a file.");
            this.cmdSaveEncryptionDef.UseVisualStyleBackColor = true;
            this.cmdSaveEncryptionDef.Click += new System.EventHandler(this.cmdSaveEncryptionDef_Click);
            // 
            // cmdLoadEncryptionDef
            // 
            this.cmdLoadEncryptionDef.Location = new System.Drawing.Point(385, 302);
            this.cmdLoadEncryptionDef.Name = "cmdLoadEncryptionDef";
            this.cmdLoadEncryptionDef.Size = new System.Drawing.Size(58, 23);
            this.cmdLoadEncryptionDef.TabIndex = 35;
            this.cmdLoadEncryptionDef.Text = "Load";
            this.mainFormToolTips.SetToolTip(this.cmdLoadEncryptionDef, "Load previously saved encryption definition from a file.");
            this.cmdLoadEncryptionDef.UseVisualStyleBackColor = true;
            this.cmdLoadEncryptionDef.Click += new System.EventHandler(this.cmdLoadEncryptionDef_Click);
            // 
            // cmdNewEncryptionDef
            // 
            this.cmdNewEncryptionDef.Location = new System.Drawing.Point(310, 302);
            this.cmdNewEncryptionDef.Name = "cmdNewEncryptionDef";
            this.cmdNewEncryptionDef.Size = new System.Drawing.Size(58, 23);
            this.cmdNewEncryptionDef.TabIndex = 34;
            this.cmdNewEncryptionDef.Text = "New";
            this.mainFormToolTips.SetToolTip(this.cmdNewEncryptionDef, "Erase current encryption definition and start over.");
            this.cmdNewEncryptionDef.UseVisualStyleBackColor = true;
            this.cmdNewEncryptionDef.Click += new System.EventHandler(this.cmdNewEncryptionDef_Click);
            // 
            // cmdGenerateRandomKeyIV
            // 
            this.cmdGenerateRandomKeyIV.Location = new System.Drawing.Point(184, 88);
            this.cmdGenerateRandomKeyIV.Name = "cmdGenerateRandomKeyIV";
            this.cmdGenerateRandomKeyIV.Size = new System.Drawing.Size(170, 24);
            this.cmdGenerateRandomKeyIV.TabIndex = 21;
            this.cmdGenerateRandomKeyIV.Text = "Generate Random Key/IV";
            this.mainFormToolTips.SetToolTip(this.cmdGenerateRandomKeyIV, "Generate random values for the Key and IV.");
            this.cmdGenerateRandomKeyIV.UseVisualStyleBackColor = true;
            this.cmdGenerateRandomKeyIV.Click += new System.EventHandler(this.cmdGenerateRandomKeyIV_Click);
            // 
            // cmdEncryptionLoadKeyIVFromFile
            // 
            this.cmdEncryptionLoadKeyIVFromFile.Location = new System.Drawing.Point(65, 59);
            this.cmdEncryptionLoadKeyIVFromFile.Name = "cmdEncryptionLoadKeyIVFromFile";
            this.cmdEncryptionLoadKeyIVFromFile.Size = new System.Drawing.Size(170, 23);
            this.cmdEncryptionLoadKeyIVFromFile.TabIndex = 22;
            this.cmdEncryptionLoadKeyIVFromFile.Text = "Load Key/IV From File";
            this.mainFormToolTips.SetToolTip(this.cmdEncryptionLoadKeyIVFromFile, "Load encryption key, IV and algorithm information from a file.");
            this.cmdEncryptionLoadKeyIVFromFile.UseVisualStyleBackColor = true;
            this.cmdEncryptionLoadKeyIVFromFile.Click += new System.EventHandler(this.cmdEncryptionLoadKeyIVFromFile_Click);
            // 
            // cmdEncryptionSaveKeyIVToFile
            // 
            this.cmdEncryptionSaveKeyIVToFile.Location = new System.Drawing.Point(285, 59);
            this.cmdEncryptionSaveKeyIVToFile.Name = "cmdEncryptionSaveKeyIVToFile";
            this.cmdEncryptionSaveKeyIVToFile.Size = new System.Drawing.Size(170, 23);
            this.cmdEncryptionSaveKeyIVToFile.TabIndex = 23;
            this.cmdEncryptionSaveKeyIVToFile.Text = "Save Key/IV To File";
            this.mainFormToolTips.SetToolTip(this.cmdEncryptionSaveKeyIVToFile, "Save encryption key, IV and algorithm information to a file.");
            this.cmdEncryptionSaveKeyIVToFile.UseVisualStyleBackColor = true;
            this.cmdEncryptionSaveKeyIVToFile.Click += new System.EventHandler(this.cmdEncryptionSaveKeyIVToFile_Click);
            // 
            // cmdDecryptionSaveKeyIVToFile
            // 
            this.cmdDecryptionSaveKeyIVToFile.Location = new System.Drawing.Point(285, 59);
            this.cmdDecryptionSaveKeyIVToFile.Name = "cmdDecryptionSaveKeyIVToFile";
            this.cmdDecryptionSaveKeyIVToFile.Size = new System.Drawing.Size(170, 23);
            this.cmdDecryptionSaveKeyIVToFile.TabIndex = 29;
            this.cmdDecryptionSaveKeyIVToFile.Text = "Save Key/IV To File";
            this.mainFormToolTips.SetToolTip(this.cmdDecryptionSaveKeyIVToFile, "Save decryption key, IV and algorithm information to a file.");
            this.cmdDecryptionSaveKeyIVToFile.UseVisualStyleBackColor = true;
            this.cmdDecryptionSaveKeyIVToFile.Click += new System.EventHandler(this.cmdDecryptionSaveKeyIVToFile_Click);
            // 
            // cmdCopyKeyIVFromAbove
            // 
            this.cmdCopyKeyIVFromAbove.Location = new System.Drawing.Point(184, 88);
            this.cmdCopyKeyIVFromAbove.Name = "cmdCopyKeyIVFromAbove";
            this.cmdCopyKeyIVFromAbove.Size = new System.Drawing.Size(170, 23);
            this.cmdCopyKeyIVFromAbove.TabIndex = 23;
            this.cmdCopyKeyIVFromAbove.Text = "Copy Key/IV From Above";
            this.mainFormToolTips.SetToolTip(this.cmdCopyKeyIVFromAbove, "Copies Key, IV and Algorithm values from Encrypt form at top of screen.");
            this.cmdCopyKeyIVFromAbove.UseVisualStyleBackColor = true;
            this.cmdCopyKeyIVFromAbove.Click += new System.EventHandler(this.cmdCopyKeyIVFromAbove_Click);
            // 
            // cmdDecryptionLoadKeyIVFromFile
            // 
            this.cmdDecryptionLoadKeyIVFromFile.Location = new System.Drawing.Point(65, 59);
            this.cmdDecryptionLoadKeyIVFromFile.Name = "cmdDecryptionLoadKeyIVFromFile";
            this.cmdDecryptionLoadKeyIVFromFile.Size = new System.Drawing.Size(170, 23);
            this.cmdDecryptionLoadKeyIVFromFile.TabIndex = 22;
            this.cmdDecryptionLoadKeyIVFromFile.Text = "Load Key/IV From File";
            this.mainFormToolTips.SetToolTip(this.cmdDecryptionLoadKeyIVFromFile, "Load decryption key, IV and algorithm information from a file.");
            this.cmdDecryptionLoadKeyIVFromFile.UseVisualStyleBackColor = true;
            this.cmdDecryptionLoadKeyIVFromFile.Click += new System.EventHandler(this.cmdDecryptionLoadKeyIVFromFile_Click);
            // 
            // txtDecryptionKey
            // 
            this.txtDecryptionKey.Location = new System.Drawing.Point(66, 33);
            this.txtDecryptionKey.Name = "txtDecryptionKey";
            this.txtDecryptionKey.Size = new System.Drawing.Size(170, 20);
            this.txtDecryptionKey.TabIndex = 18;
            this.mainFormToolTips.SetToolTip(this.txtDecryptionKey, "Key that was used for the encryption.");
            // 
            // txtDecryptionIV
            // 
            this.txtDecryptionIV.Location = new System.Drawing.Point(285, 33);
            this.txtDecryptionIV.Name = "txtDecryptionIV";
            this.txtDecryptionIV.Size = new System.Drawing.Size(170, 20);
            this.txtDecryptionIV.TabIndex = 20;
            this.mainFormToolTips.SetToolTip(this.txtDecryptionIV, "Initialization Vector that was used for the encryption.");
            // 
            // cmdSaveDecryptionDef
            // 
            this.cmdSaveDecryptionDef.Location = new System.Drawing.Point(460, 607);
            this.cmdSaveDecryptionDef.Name = "cmdSaveDecryptionDef";
            this.cmdSaveDecryptionDef.Size = new System.Drawing.Size(58, 23);
            this.cmdSaveDecryptionDef.TabIndex = 40;
            this.cmdSaveDecryptionDef.Text = "Save";
            this.mainFormToolTips.SetToolTip(this.cmdSaveDecryptionDef, "Save displayed decryption definition to a file.");
            this.cmdSaveDecryptionDef.UseVisualStyleBackColor = true;
            this.cmdSaveDecryptionDef.Click += new System.EventHandler(this.cmdSaveDecryptionDef_Click);
            // 
            // cmdLoadDecryptionDef
            // 
            this.cmdLoadDecryptionDef.Location = new System.Drawing.Point(385, 607);
            this.cmdLoadDecryptionDef.Name = "cmdLoadDecryptionDef";
            this.cmdLoadDecryptionDef.Size = new System.Drawing.Size(58, 23);
            this.cmdLoadDecryptionDef.TabIndex = 39;
            this.cmdLoadDecryptionDef.Text = "Load";
            this.mainFormToolTips.SetToolTip(this.cmdLoadDecryptionDef, "Load previously saved decryption definition from a file.");
            this.cmdLoadDecryptionDef.UseVisualStyleBackColor = true;
            this.cmdLoadDecryptionDef.Click += new System.EventHandler(this.cmdLoadDecryptionDef_Click);
            // 
            // cmdNewDecryptionDef
            // 
            this.cmdNewDecryptionDef.Location = new System.Drawing.Point(310, 607);
            this.cmdNewDecryptionDef.Name = "cmdNewDecryptionDef";
            this.cmdNewDecryptionDef.Size = new System.Drawing.Size(58, 23);
            this.cmdNewDecryptionDef.TabIndex = 38;
            this.cmdNewDecryptionDef.Text = "New";
            this.mainFormToolTips.SetToolTip(this.cmdNewDecryptionDef, "Erase current decryption definition and start over.");
            this.cmdNewDecryptionDef.UseVisualStyleBackColor = true;
            this.cmdNewDecryptionDef.Click += new System.EventHandler(this.cmdNewDecryptionDef_Click);
            // 
            // lblEncryptionSource
            // 
            this.lblEncryptionSource.AutoSize = true;
            this.lblEncryptionSource.Location = new System.Drawing.Point(21, 137);
            this.lblEncryptionSource.Name = "lblEncryptionSource";
            this.lblEncryptionSource.Size = new System.Drawing.Size(123, 13);
            this.lblEncryptionSource.TabIndex = 9;
            this.lblEncryptionSource.Text = "String or File To Encrypt:";
            // 
            // txtEncryptionSource
            // 
            this.txtEncryptionSource.Location = new System.Drawing.Point(21, 153);
            this.txtEncryptionSource.Name = "txtEncryptionSource";
            this.txtEncryptionSource.Size = new System.Drawing.Size(434, 20);
            this.txtEncryptionSource.TabIndex = 10;
            // 
            // cmdEncryptionSource
            // 
            this.cmdEncryptionSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdEncryptionSource.Location = new System.Drawing.Point(417, 135);
            this.cmdEncryptionSource.Name = "cmdEncryptionSource";
            this.cmdEncryptionSource.Size = new System.Drawing.Size(38, 20);
            this.cmdEncryptionSource.TabIndex = 11;
            this.cmdEncryptionSource.Text = "•••";
            this.cmdEncryptionSource.UseVisualStyleBackColor = true;
            this.cmdEncryptionSource.Click += new System.EventHandler(this.cmdEncryptionSource_Click);
            // 
            // lblEncryptionTarget
            // 
            this.lblEncryptionTarget.AutoSize = true;
            this.lblEncryptionTarget.Location = new System.Drawing.Point(21, 180);
            this.lblEncryptionTarget.Name = "lblEncryptionTarget";
            this.lblEncryptionTarget.Size = new System.Drawing.Size(127, 13);
            this.lblEncryptionTarget.TabIndex = 14;
            this.lblEncryptionTarget.Text = "Save encrypted output to";
            // 
            // txtEncryptionTarget
            // 
            this.txtEncryptionTarget.Location = new System.Drawing.Point(24, 200);
            this.txtEncryptionTarget.Name = "txtEncryptionTarget";
            this.txtEncryptionTarget.Size = new System.Drawing.Size(431, 20);
            this.txtEncryptionTarget.TabIndex = 15;
            // 
            // cmdEncryptionTarget
            // 
            this.cmdEncryptionTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdEncryptionTarget.Location = new System.Drawing.Point(417, 179);
            this.cmdEncryptionTarget.Name = "cmdEncryptionTarget";
            this.cmdEncryptionTarget.Size = new System.Drawing.Size(38, 20);
            this.cmdEncryptionTarget.TabIndex = 16;
            this.cmdEncryptionTarget.Text = "•••";
            this.cmdEncryptionTarget.UseVisualStyleBackColor = true;
            this.cmdEncryptionTarget.Click += new System.EventHandler(this.cmdEncryptionTarget_Click);
            // 
            // panelEncryption
            // 
            this.panelEncryption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEncryption.Controls.Add(this.txtEncryptResult);
            this.panelEncryption.Controls.Add(this.grpSaveOutputTo);
            this.panelEncryption.Controls.Add(this.chkBinaryEncryption);
            this.panelEncryption.Controls.Add(this.txtEncryptionTarget);
            this.panelEncryption.Controls.Add(this.cmdEncryptionSaveKeyIVToFile);
            this.panelEncryption.Controls.Add(this.lblEncryptionTarget);
            this.panelEncryption.Controls.Add(this.lblEncryptionOperation);
            this.panelEncryption.Controls.Add(this.cmdEncryptionTarget);
            this.panelEncryption.Controls.Add(this.cmdEncryptionLoadKeyIVFromFile);
            this.panelEncryption.Controls.Add(this.cmdEncryptionSource);
            this.panelEncryption.Controls.Add(this.groupBox1);
            this.panelEncryption.Controls.Add(this.label4);
            this.panelEncryption.Controls.Add(this.cmdGenerateRandomKeyIV);
            this.panelEncryption.Controls.Add(this.txtEncryptionSource);
            this.panelEncryption.Controls.Add(this.label2);
            this.panelEncryption.Controls.Add(this.txtEncryptionKey);
            this.panelEncryption.Controls.Add(this.txtEncryptionIV);
            this.panelEncryption.Controls.Add(this.lblEncryptionSource);
            this.panelEncryption.Controls.Add(this.grpEncryptionAlgorithm);
            this.panelEncryption.Controls.Add(this.label3);
            this.panelEncryption.Location = new System.Drawing.Point(25, 40);
            this.panelEncryption.Name = "panelEncryption";
            this.panelEncryption.Size = new System.Drawing.Size(493, 262);
            this.panelEncryption.TabIndex = 25;
            // 
            // txtEncryptResult
            // 
            this.txtEncryptResult.BackColor = System.Drawing.SystemColors.Control;
            this.txtEncryptResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEncryptResult.Location = new System.Drawing.Point(21, 225);
            this.txtEncryptResult.Multiline = true;
            this.txtEncryptResult.Name = "txtEncryptResult";
            this.txtEncryptResult.Size = new System.Drawing.Size(434, 30);
            this.txtEncryptResult.TabIndex = 26;
            // 
            // grpSaveOutputTo
            // 
            this.grpSaveOutputTo.Controls.Add(this.optSaveEncryptedToString);
            this.grpSaveOutputTo.Controls.Add(this.optSaveEncryptedToFile);
            this.grpSaveOutputTo.Location = new System.Drawing.Point(164, 176);
            this.grpSaveOutputTo.Name = "grpSaveOutputTo";
            this.grpSaveOutputTo.Size = new System.Drawing.Size(121, 20);
            this.grpSaveOutputTo.TabIndex = 25;
            this.grpSaveOutputTo.TabStop = false;
            // 
            // optSaveEncryptedToString
            // 
            this.optSaveEncryptedToString.AutoSize = true;
            this.optSaveEncryptedToString.Location = new System.Drawing.Point(62, 3);
            this.optSaveEncryptedToString.Name = "optSaveEncryptedToString";
            this.optSaveEncryptedToString.Size = new System.Drawing.Size(52, 17);
            this.optSaveEncryptedToString.TabIndex = 1;
            this.optSaveEncryptedToString.TabStop = true;
            this.optSaveEncryptedToString.Text = "String";
            this.optSaveEncryptedToString.UseVisualStyleBackColor = true;
            this.optSaveEncryptedToString.CheckedChanged += new System.EventHandler(this.optSaveEncryptedToString_CheckedChanged);
            // 
            // optSaveEncryptedToFile
            // 
            this.optSaveEncryptedToFile.AutoSize = true;
            this.optSaveEncryptedToFile.Checked = true;
            this.optSaveEncryptedToFile.Location = new System.Drawing.Point(6, 3);
            this.optSaveEncryptedToFile.Name = "optSaveEncryptedToFile";
            this.optSaveEncryptedToFile.Size = new System.Drawing.Size(41, 17);
            this.optSaveEncryptedToFile.TabIndex = 0;
            this.optSaveEncryptedToFile.TabStop = true;
            this.optSaveEncryptedToFile.Text = "File";
            this.optSaveEncryptedToFile.UseVisualStyleBackColor = true;
            // 
            // panelDecryption
            // 
            this.panelDecryption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDecryption.Controls.Add(this.txtDecryptionResult);
            this.panelDecryption.Controls.Add(this.cmdDecryptionSaveKeyIVToFile);
            this.panelDecryption.Controls.Add(this.grpSaveDecryptionOutputTo);
            this.panelDecryption.Controls.Add(this.chkBinaryDecryption);
            this.panelDecryption.Controls.Add(this.txtDecryptionTarget);
            this.panelDecryption.Controls.Add(this.cmdCopyKeyIVFromAbove);
            this.panelDecryption.Controls.Add(this.lblDecryptionTarget);
            this.panelDecryption.Controls.Add(this.lblDecryptionOperation);
            this.panelDecryption.Controls.Add(this.cmdDecryptionTarget);
            this.panelDecryption.Controls.Add(this.cmdDecryptionLoadKeyIVFromFile);
            this.panelDecryption.Controls.Add(this.cmdDecryptionSource);
            this.panelDecryption.Controls.Add(this.groupBox2);
            this.panelDecryption.Controls.Add(this.label7);
            this.panelDecryption.Controls.Add(this.txtDecryptionSource);
            this.panelDecryption.Controls.Add(this.label8);
            this.panelDecryption.Controls.Add(this.txtDecryptionKey);
            this.panelDecryption.Controls.Add(this.txtDecryptionIV);
            this.panelDecryption.Controls.Add(this.lblDecryptionSource);
            this.panelDecryption.Controls.Add(this.groupBox3);
            this.panelDecryption.Controls.Add(this.label10);
            this.panelDecryption.Location = new System.Drawing.Point(25, 342);
            this.panelDecryption.Name = "panelDecryption";
            this.panelDecryption.Size = new System.Drawing.Size(493, 265);
            this.panelDecryption.TabIndex = 26;
            // 
            // txtDecryptionResult
            // 
            this.txtDecryptionResult.BackColor = System.Drawing.SystemColors.Control;
            this.txtDecryptionResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDecryptionResult.Location = new System.Drawing.Point(24, 225);
            this.txtDecryptionResult.Multiline = true;
            this.txtDecryptionResult.Name = "txtDecryptionResult";
            this.txtDecryptionResult.Size = new System.Drawing.Size(431, 33);
            this.txtDecryptionResult.TabIndex = 30;
            // 
            // grpSaveDecryptionOutputTo
            // 
            this.grpSaveDecryptionOutputTo.Controls.Add(this.optSaveDecryptedToString);
            this.grpSaveDecryptionOutputTo.Controls.Add(this.optSaveDecryptedToFile);
            this.grpSaveDecryptionOutputTo.Location = new System.Drawing.Point(170, 181);
            this.grpSaveDecryptionOutputTo.Name = "grpSaveDecryptionOutputTo";
            this.grpSaveDecryptionOutputTo.Size = new System.Drawing.Size(130, 20);
            this.grpSaveDecryptionOutputTo.TabIndex = 26;
            this.grpSaveDecryptionOutputTo.TabStop = false;
            // 
            // optSaveDecryptedToString
            // 
            this.optSaveDecryptedToString.AutoSize = true;
            this.optSaveDecryptedToString.Location = new System.Drawing.Point(62, 3);
            this.optSaveDecryptedToString.Name = "optSaveDecryptedToString";
            this.optSaveDecryptedToString.Size = new System.Drawing.Size(52, 17);
            this.optSaveDecryptedToString.TabIndex = 1;
            this.optSaveDecryptedToString.TabStop = true;
            this.optSaveDecryptedToString.Text = "String";
            this.optSaveDecryptedToString.UseVisualStyleBackColor = true;
            this.optSaveDecryptedToString.CheckedChanged += new System.EventHandler(this.optSaveDecryptedToString_CheckedChanged);
            // 
            // optSaveDecryptedToFile
            // 
            this.optSaveDecryptedToFile.AutoSize = true;
            this.optSaveDecryptedToFile.Checked = true;
            this.optSaveDecryptedToFile.Location = new System.Drawing.Point(6, 3);
            this.optSaveDecryptedToFile.Name = "optSaveDecryptedToFile";
            this.optSaveDecryptedToFile.Size = new System.Drawing.Size(41, 17);
            this.optSaveDecryptedToFile.TabIndex = 0;
            this.optSaveDecryptedToFile.TabStop = true;
            this.optSaveDecryptedToFile.Text = "File";
            this.optSaveDecryptedToFile.UseVisualStyleBackColor = true;
            // 
            // txtDecryptionTarget
            // 
            this.txtDecryptionTarget.Location = new System.Drawing.Point(24, 202);
            this.txtDecryptionTarget.Name = "txtDecryptionTarget";
            this.txtDecryptionTarget.Size = new System.Drawing.Size(431, 20);
            this.txtDecryptionTarget.TabIndex = 15;
            // 
            // lblDecryptionTarget
            // 
            this.lblDecryptionTarget.AutoSize = true;
            this.lblDecryptionTarget.Location = new System.Drawing.Point(21, 185);
            this.lblDecryptionTarget.Name = "lblDecryptionTarget";
            this.lblDecryptionTarget.Size = new System.Drawing.Size(127, 13);
            this.lblDecryptionTarget.TabIndex = 14;
            this.lblDecryptionTarget.Text = "Save decrypted output to";
            // 
            // lblDecryptionOperation
            // 
            this.lblDecryptionOperation.AutoSize = true;
            this.lblDecryptionOperation.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDecryptionOperation.Location = new System.Drawing.Point(21, 12);
            this.lblDecryptionOperation.Name = "lblDecryptionOperation";
            this.lblDecryptionOperation.Size = new System.Drawing.Size(66, 18);
            this.lblDecryptionOperation.TabIndex = 5;
            this.lblDecryptionOperation.Text = "Decrypt";
            // 
            // cmdDecryptionTarget
            // 
            this.cmdDecryptionTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDecryptionTarget.Location = new System.Drawing.Point(417, 184);
            this.cmdDecryptionTarget.Name = "cmdDecryptionTarget";
            this.cmdDecryptionTarget.Size = new System.Drawing.Size(38, 20);
            this.cmdDecryptionTarget.TabIndex = 16;
            this.cmdDecryptionTarget.Text = "•••";
            this.cmdDecryptionTarget.UseVisualStyleBackColor = true;
            this.cmdDecryptionTarget.Click += new System.EventHandler(this.cmdDecryptionTarget_Click);
            // 
            // cmdDecryptionSource
            // 
            this.cmdDecryptionSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDecryptionSource.Location = new System.Drawing.Point(417, 140);
            this.cmdDecryptionSource.Name = "cmdDecryptionSource";
            this.cmdDecryptionSource.Size = new System.Drawing.Size(38, 20);
            this.cmdDecryptionSource.TabIndex = 11;
            this.cmdDecryptionSource.Text = "•••";
            this.cmdDecryptionSource.UseVisualStyleBackColor = true;
            this.cmdDecryptionSource.Click += new System.EventHandler(this.cmdDecryptionSource_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.optDecryptString);
            this.groupBox2.Controls.Add(this.optDecryptFile);
            this.groupBox2.Location = new System.Drawing.Point(97, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(130, 30);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // optDecryptString
            // 
            this.optDecryptString.AutoSize = true;
            this.optDecryptString.Location = new System.Drawing.Point(67, 8);
            this.optDecryptString.Name = "optDecryptString";
            this.optDecryptString.Size = new System.Drawing.Size(52, 17);
            this.optDecryptString.TabIndex = 1;
            this.optDecryptString.TabStop = true;
            this.optDecryptString.Text = "String";
            this.optDecryptString.UseVisualStyleBackColor = true;
            this.optDecryptString.CheckedChanged += new System.EventHandler(this.optDecryptString_CheckedChanged);
            // 
            // optDecryptFile
            // 
            this.optDecryptFile.AutoSize = true;
            this.optDecryptFile.Checked = true;
            this.optDecryptFile.Location = new System.Drawing.Point(6, 8);
            this.optDecryptFile.Name = "optDecryptFile";
            this.optDecryptFile.Size = new System.Drawing.Size(41, 17);
            this.optDecryptFile.TabIndex = 0;
            this.optDecryptFile.TabStop = true;
            this.optDecryptFile.Text = "File";
            this.optDecryptFile.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Key:";
            // 
            // txtDecryptionSource
            // 
            this.txtDecryptionSource.Location = new System.Drawing.Point(21, 158);
            this.txtDecryptionSource.Name = "txtDecryptionSource";
            this.txtDecryptionSource.Size = new System.Drawing.Size(434, 20);
            this.txtDecryptionSource.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(233, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "using";
            // 
            // lblDecryptionSource
            // 
            this.lblDecryptionSource.AutoSize = true;
            this.lblDecryptionSource.Location = new System.Drawing.Point(21, 142);
            this.lblDecryptionSource.Name = "lblDecryptionSource";
            this.lblDecryptionSource.Size = new System.Drawing.Size(124, 13);
            this.lblDecryptionSource.TabIndex = 9;
            this.lblDecryptionSource.Text = "String or File To Decrypt:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.optDecryptAES);
            this.groupBox3.Controls.Add(this.optDecryptTripleDES);
            this.groupBox3.Controls.Add(this.optDecryptDES);
            this.groupBox3.Location = new System.Drawing.Point(271, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(184, 33);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(258, 37);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "IV:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(666, 664);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdSaveDecryptionDef);
            this.Controls.Add(this.cmdLoadDecryptionDef);
            this.Controls.Add(this.cmdNewDecryptionDef);
            this.Controls.Add(this.cmdSaveEncryptionDef);
            this.Controls.Add(this.cmdLoadEncryptionDef);
            this.Controls.Add(this.cmdNewEncryptionDef);
            this.Controls.Add(this.cmdDecrypt);
            this.Controls.Add(this.panelDecryption);
            this.Controls.Add(this.panelEncryption);
            this.Controls.Add(this.cmdEncrypt);
            this.Controls.Add(this.MainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " fre";
            this.mainFormToolTips.SetToolTip(this, "Close form and exit application");
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpEncryptionAlgorithm.ResumeLayout(false);
            this.grpEncryptionAlgorithm.PerformLayout();
            this.panelEncryption.ResumeLayout(false);
            this.panelEncryption.PerformLayout();
            this.grpSaveOutputTo.ResumeLayout(false);
            this.grpSaveOutputTo.PerformLayout();
            this.panelDecryption.ResumeLayout(false);
            this.panelDecryption.PerformLayout();
            this.grpSaveDecryptionOutputTo.ResumeLayout(false);
            this.grpSaveDecryptionOutputTo.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.Label lblEncryptionOperation;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton optEncryptString;
        private System.Windows.Forms.RadioButton optEncryptFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpEncryptionAlgorithm;
        private System.Windows.Forms.RadioButton optEncryptAES;
        private System.Windows.Forms.ToolTip mainFormToolTips;
        private System.Windows.Forms.RadioButton optEncryptTripleDES;
        private System.Windows.Forms.RadioButton optEncryptDES;
        private System.Windows.Forms.Label lblEncryptionSource;
        private System.Windows.Forms.TextBox txtEncryptionSource;
        private System.Windows.Forms.Button cmdEncryptionSource;
        private System.Windows.Forms.Button cmdEncrypt;
        private System.Windows.Forms.Label lblEncryptionTarget;
        private System.Windows.Forms.TextBox txtEncryptionTarget;
        private System.Windows.Forms.Button cmdEncryptionTarget;
        private System.Windows.Forms.Button cmdGenerateRandomKeyIV;
        private System.Windows.Forms.TextBox txtEncryptionIV;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEncryptionKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdEncryptionLoadKeyIVFromFile;
        private System.Windows.Forms.Button cmdEncryptionSaveKeyIVToFile;
        private System.Windows.Forms.Panel panelEncryption;
        private System.Windows.Forms.Panel panelDecryption;
        private System.Windows.Forms.TextBox txtDecryptionTarget;
        private System.Windows.Forms.Button cmdCopyKeyIVFromAbove;
        private System.Windows.Forms.Label lblDecryptionTarget;
        private System.Windows.Forms.Label lblDecryptionOperation;
        private System.Windows.Forms.Button cmdDecryptionTarget;
        private System.Windows.Forms.Button cmdDecryptionLoadKeyIVFromFile;
        private System.Windows.Forms.Button cmdDecryptionSource;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton optDecryptString;
        private System.Windows.Forms.RadioButton optDecryptFile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDecryptionSource;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDecryptionKey;
        private System.Windows.Forms.TextBox txtDecryptionIV;
        private System.Windows.Forms.Label lblDecryptionSource;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton optDecryptAES;
        private System.Windows.Forms.RadioButton optDecryptTripleDES;
        private System.Windows.Forms.RadioButton optDecryptDES;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button cmdDecrypt;
        private System.Windows.Forms.CheckBox chkBinaryEncryption;
        private System.Windows.Forms.CheckBox chkBinaryDecryption;
        private System.Windows.Forms.GroupBox grpSaveOutputTo;
        private System.Windows.Forms.RadioButton optSaveEncryptedToString;
        private System.Windows.Forms.RadioButton optSaveEncryptedToFile;
        private System.Windows.Forms.GroupBox grpSaveDecryptionOutputTo;
        private System.Windows.Forms.RadioButton optSaveDecryptedToString;
        private System.Windows.Forms.RadioButton optSaveDecryptedToFile;
        private System.Windows.Forms.Button cmdSaveEncryptionDef;
        private System.Windows.Forms.Button cmdLoadEncryptionDef;
        private System.Windows.Forms.Button cmdNewEncryptionDef;
        private System.Windows.Forms.Button cmdSaveDecryptionDef;
        private System.Windows.Forms.Button cmdLoadDecryptionDef;
        private System.Windows.Forms.Button cmdNewDecryptionDef;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdDecryptionSaveKeyIVToFile;
        private System.Windows.Forms.TextBox txtEncryptResult;
        private System.Windows.Forms.TextBox txtDecryptionResult;
    }
}

