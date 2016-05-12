using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;////
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AppGlobals;
using PFFileSystemObjects;
using PFEncryptionObjects;
using pfEncryptorObjects;

namespace pfEncryptor
{
    public partial class MainForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"pfEncryptor.log";
        private string _appLogFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\pfEncryptor\Logs\";

        PFAppProcessor _appProcessor = new PFAppProcessor();
        private string _defaultKeyIVSaveFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),@"PFApps\pfEncryptor\Settings\");
        private string _defaultEncryptionDefinitionsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"PFApps\pfEncryptor\EncryptionDefinitions\");
        private string _defaultDecryptionDefinitionsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"PFApps\pfEncryptor\DecryptionDefinitions\");

        private string _defaultEncryptSourceFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"PFApps\pfEncryptor\DataFiles\");
        private string _defaultEncryptDestinationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"PFApps\pfEncryptor\EncryptedDataFiles\");
        private string _defaultDecryptSourceFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"PFApps\pfEncryptor\EncryptedDataFiles\");
        private string _defaultDecryptDestinationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"PFApps\pfEncryptor\DecryptedDataFiles\");

        private string _keyIVFilePath =  Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"PFApps\pfEncryptor\Keys\");
        private string _encryptionDefinitionsFile = string.Empty;
        private string _decryptionDefinitionsFile = string.Empty;

        private int _numCallbacks = 0;
        private const string ENCRYPT_SUCCESSFUL = "Encryption succeeded.";
        private const string ENCRYPT_FAILED = "Encryption failed.";
        private const string DECRYPT_SUCCESSFUL = "Decryption succeeded.";
        private const string DECRYPT_FAILED = "Decryption failed.";
        private const string ENCRYPT_SAVE_SUCCESSFUL = "Encryption definition saved to file.";
        private const string ENCRYPT_SAVE_FAILED = "Encryption definition save to file failed.";
        private const string DECRYPT_SAVE_SUCCESSFUL = "Decryption definition saved to file.";
        private const string DECRYPT_SAVE_FAILED = "Decryption definition save to file failed.";
        private const string ENCRYPT_LOAD_SUCCESSFUL = "Encryption definition retrieved from file.";
        private const string ENCRYPT_LOAD_FAILED = "Encryption definition load from file failed.";
        private const string DECRYPT_LOAD_SUCCESSFUL = "Decryption definition retrieved from file.";
        private const string DECRYPT_LOAD_FAILED = "Decryption definition load from file failed.";

        //private fields for processing file and folder dialogs
        private OpenFileDialog _openFileDialog = new OpenFileDialog();
        private SaveFileDialog _saveFileDialog = new SaveFileDialog();
        private FolderBrowserDialog _folderBrowserDialog = new FolderBrowserDialog();
        private string _saveSelectionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string _saveSelectionsFile = string.Empty;
        private string[] _saveSelectedFiles = null;
        private bool _saveMultiSelect = true;
        private string _saveFilter = "All Files|*.*";
        private int _saveFilterIndex = 1;
        private bool _showCreatePrompt = false;
        private bool _showOverwritePrompt = true;
        private bool _showNewFolderButton = true;

        public MainForm()
        {
            InitializeComponent();
        }

        //button click events

        //encryption form command clicks

        private void cmdGenerateRandomKeyIV_Click(object sender, EventArgs e)
        {
            ReinitEncryptionFormDisplaySettings();
            GenerateRandomKeyIV();
        }

        private void cmdEncryptionLoadKeyIVFromFile_Click(object sender, EventArgs e)
        {
            ReinitEncryptionFormDisplaySettings();
            LoadKeyIVFromFile();
        }

        private void cmdEncryptionSaveKeyIVToFile_Click(object sender, EventArgs e)
        {
            ReinitEncryptionFormDisplaySettings();
            SaveKeyIVToFile();
        }

        private void cmdEncryptionSource_Click(object sender, EventArgs e)
        {
            ReinitEncryptionFormDisplaySettings();
            if (this.optEncryptFile.Checked)
                GetFileToEncrypt();
            else
                GetStringToEncrypt();
        }

        private void cmdEncryptionTarget_Click(object sender, EventArgs e)
        {
            ReinitEncryptionFormDisplaySettings();
            if (this.optSaveEncryptedToFile.Checked)
                GetFileNameForEncryptedOutput();
            else
                ShowEncryptedString();
        }

        private void cmdEncrypt_Click(object sender, EventArgs e)
        {
            ReinitEncryptionFormDisplaySettings();
            this.Encrypt();
        }

        private void cmdNewEncryptionDef_Click(object sender, EventArgs e)
        {
            NewEncryptionDef();
        }

        private void cmdLoadEncryptionDef_Click(object sender, EventArgs e)
        {
            LoadEncryptionDef();
        }

        private void cmdSaveEncryptionDef_Click(object sender, EventArgs e)
        {
            SaveEncryptionDef();
        }

        //decryption form command clicks

        private void cmdCopyKeyIVFromAbove_Click(object sender, EventArgs e)
        {
            ReinitDecryptionFormDisplaySettings();
            CopyKeyIVFromAbove();
        }

        private void cmdDecryptionLoadKeyIVFromFile_Click(object sender, EventArgs e)
        {
            ReinitDecryptionFormDisplaySettings();
            LoadDecryptionKeyIVFromFile();
        }

        private void cmdDecryptionSaveKeyIVToFile_Click(object sender, EventArgs e)
        {
            ReinitDecryptionFormDisplaySettings();
            SaveDecryptionKeyIVToFile();
        }

        private void cmdDecryptionSource_Click(object sender, EventArgs e)
        {
            ReinitDecryptionFormDisplaySettings();
            if (this.optDecryptFile.Checked)
                GetFileToDecrypt();
            else
                GetStringToDecrypt();
        }

        private void cmdDecryptionTarget_Click(object sender, EventArgs e)
        {
            ReinitDecryptionFormDisplaySettings();
            if (this.optSaveDecryptedToFile.Checked)
                GetFileNameForDecryptedOutput();
            else
                ShowDecryptedString();
        }

        private void cmdDecrypt_Click(object sender, EventArgs e)
        {
            ReinitDecryptionFormDisplaySettings();
            this.Decrypt();
        }

        private void cmdNewDecryptionDef_Click(object sender, EventArgs e)
        {
            NewDecryptionDef();
        }

        private void cmdLoadDecryptionDef_Click(object sender, EventArgs e)
        {
            LoadDecryptionDef();
        }

        private void cmdSaveDecryptionDef_Click(object sender, EventArgs e)
        {
            SaveDecryptionDef();
        }


        //exit and test command clicks
        
        private void cmdExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        //Menu item clicks
        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }


        //Form Routines
        private void CloseForm()
        {
            this.Close();
        }

        //event handlers

        private void MainForm_Load(object sender, EventArgs e)
        {
            string configValue = string.Empty;

            try
            {
                this.Text = AppInfo.AssemblyProduct;

                SetLoggingValues();

                InitEncryptionFormDisplaySettings();

                InitDecryptionFormDisplaySettings();

                InitFormDataSettings();

                InitAppProcessor();

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }

        }

        internal void SetLoggingValues()
        {
            string configValue = string.Empty;

            configValue = AppGlobals.AppConfig.GetConfigValue("SaveErrorMessagesToErrorLog");
            if (configValue.ToUpper() == "TRUE")
                _saveErrorMessagesToAppLog = true;
            else
                _saveErrorMessagesToAppLog = false;

            configValue = AppGlobals.AppConfig.GetStringValueFromConfigFile("AppLogFileName", string.Empty);
            if (configValue.Trim().Length > 0)
                _appLogFileName = configValue;
            if (Directory.Exists(_appLogFolder) == false)
                Directory.CreateDirectory(_appLogFolder);
            AppGlobals.AppMessages.AppLogFilename = Path.Combine(_appLogFolder, _appLogFileName);

            _appProcessor.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
        }

        internal void InitEncryptionFormDisplaySettings()
        {
            this.optEncryptFile.Checked = true;
            this.optEncryptAES.Checked = true;
            this.optSaveEncryptedToFile.Checked = true;
            optEncryptObject_CheckedChanged(null, null);
            SaveEncryptedToObject_CheckedChanged(null, null);
            
            this.chkBinaryEncryption.Checked = false;
            BinaryEncryption_CheckedChanged(null, null);
            this.txtEncryptResult.Text = string.Empty;

            this.txtEncryptionKey.Text = string.Empty;
            this.txtEncryptionIV.Text = string.Empty;
            this.txtEncryptionSource.Text = string.Empty;
            this.txtEncryptionTarget.Text = string.Empty;
        }

        internal void InitDecryptionFormDisplaySettings()
        {
            this.optDecryptFile.Checked = true;
            this.optDecryptAES.Checked = true;
            this.optSaveDecryptedToFile.Checked = true;
            optDecryptObject_CheckedChanged(null, null);
            SaveDecryptedToObject_CheckedChanged(null, null);

            this.chkBinaryDecryption.Checked = false;
            BinaryDecryption_CheckedChanged(null, null);
            this.txtDecryptionResult.Text = string.Empty;

            this.txtDecryptionKey.Text = string.Empty;
            this.txtDecryptionIV.Text = string.Empty;
            this.txtDecryptionSource.Text = string.Empty;
            this.txtDecryptionTarget.Text = string.Empty;

        }

        internal void ReinitEncryptionFormDisplaySettings()
        {
            this.txtEncryptResult.Text = string.Empty;
        }

        internal void ReinitDecryptionFormDisplaySettings()
        {
            this.txtDecryptionResult.Text = string.Empty;
        }

        internal void InitFormDataSettings()
        {
            string configValue = string.Empty;
            string currAppFolder = AppInfo.CurrentEntryAssemblyDirectory;
            string testFileName = StaticKeysSection.Settings.TestfileName;
            string testfile = Path.Combine(currAppFolder,testFileName);

            configValue = AppConfig.GetStringValueFromConfigFile("DefaultKeyIVSaveFolder", string.Empty);
            if (configValue != string.Empty)
            {
                _defaultKeyIVSaveFolder = configValue;
            }
            if (Directory.Exists(_defaultKeyIVSaveFolder)==false)
            {
                Directory.CreateDirectory(_defaultKeyIVSaveFolder);
            }

            configValue = AppConfig.GetStringValueFromConfigFile("DefaultEncryptionDefinitionsFolder", string.Empty);
            if (configValue != string.Empty)
            {
                _defaultEncryptionDefinitionsFolder = configValue;
            }
            if (Directory.Exists(_defaultEncryptionDefinitionsFolder) == false)
            {
                Directory.CreateDirectory(_defaultEncryptionDefinitionsFolder);
            }

            configValue = AppConfig.GetStringValueFromConfigFile("DefaultDecryptionDefinitionsFolder", string.Empty);
            if (configValue != string.Empty)
            {
                _defaultDecryptionDefinitionsFolder = configValue;
            }
            if (Directory.Exists(_defaultDecryptionDefinitionsFolder) == false)
            {
                Directory.CreateDirectory(_defaultDecryptionDefinitionsFolder);
            }

            configValue = AppConfig.GetStringValueFromConfigFile("DefaultKeyIVSaveFolder", string.Empty);
            if (configValue != string.Empty)
            {
                _keyIVFilePath = configValue;
            }
            if (Directory.Exists(_keyIVFilePath) == false)
            {
                Directory.CreateDirectory(_keyIVFilePath);
            }

            if (Directory.Exists(_defaultEncryptSourceFolder) == false)
            {
                Directory.CreateDirectory(_defaultEncryptSourceFolder);
            }

            if (Directory.Exists(_defaultEncryptDestinationFolder) == false)
            {
                Directory.CreateDirectory(_defaultEncryptDestinationFolder);
            }

            if (Directory.Exists(_defaultDecryptSourceFolder) == false)
            {
                Directory.CreateDirectory(_defaultDecryptSourceFolder);
            }

            if (Directory.Exists(_defaultDecryptDestinationFolder) == false)
            {
                Directory.CreateDirectory(_defaultDecryptDestinationFolder);
            }

            string mydocumentsTestfile = Path.Combine(_defaultEncryptSourceFolder, testFileName);
            if (File.Exists(mydocumentsTestfile) == false)
            {
                if (File.Exists(testfile) == true)
                {
                    File.Copy(testfile, mydocumentsTestfile, true);
                }
            }

        }

        internal void InitAppProcessor()
        {
            _appProcessor.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
        }

        //click events for encryption

        private void optEncryptString_CheckedChanged(object sender, EventArgs e)
        {
            optEncryptObject_CheckedChanged(sender, e);
        }

        private void optEncryptObject_CheckedChanged(object sender, EventArgs e)
        {
            ReinitEncryptionFormDisplaySettings();
            this.txtEncryptionSource.Text = string.Empty;
            this.txtEncryptionTarget.Text = string.Empty;
            if (this.optEncryptString.Checked)
            {
                this.lblEncryptionSource.Text = "String to encrypt:";
                this.chkBinaryEncryption.Checked = false;
                this.chkBinaryEncryption.Visible = false;
                this.optSaveEncryptedToString.Checked = true;
                this.mainFormToolTips.SetToolTip(this.cmdEncryptionSource, "Show larger text box to edit string to encrypt.");
                this.mainFormToolTips.SetToolTip(this.txtEncryptionSource, "Enter string to encrypt in this text box.");
            }
            else
            {
                this.lblEncryptionSource.Text = "File to encrypt:";
                this.chkBinaryEncryption.Visible = true;
                this.optSaveEncryptedToFile.Checked = true;
                this.mainFormToolTips.SetToolTip(this.cmdEncryptionSource, "Show OpenFile dialog to select file to encrypt.");
                this.mainFormToolTips.SetToolTip(this.txtEncryptionSource, "Full path to file to encrypt goes here.");
            }

        }

        private void optEncryptAES_CheckedChanged(object sender, EventArgs e)
        {
            EncryptAlgorithm_CheckedChanged(sender, e);
        }

        private void optEncryptTripleDES_CheckedChanged(object sender, EventArgs e)
        {
            EncryptAlgorithm_CheckedChanged(sender, e);
        }

        private void optEncryptDES_CheckedChanged(object sender, EventArgs e)
        {
            EncryptAlgorithm_CheckedChanged(sender, e);
        }

        private void EncryptAlgorithm_CheckedChanged(object sender, EventArgs e)
        {
            ReinitEncryptionFormDisplaySettings();
            this.txtEncryptionKey.Text = string.Empty;
            this.txtEncryptionIV.Text = string.Empty;
        }

        private void chkBinaryEncryption_CheckedChanged(object sender, EventArgs e)
        {
            BinaryEncryption_CheckedChanged(sender, e);
        }

        public void BinaryEncryption_CheckedChanged(object sender, EventArgs e)
        {
            ReinitEncryptionFormDisplaySettings();
            if (this.chkBinaryEncryption.Checked)
            {
                this.optSaveEncryptedToFile.Checked = true;
                this.grpSaveOutputTo.Visible = false;
                this.lblEncryptionTarget.Text = "Save encrypted output to file";
            }
            else
            {
                this.grpSaveOutputTo.Visible = true;
                this.lblEncryptionTarget.Text = "Save encrypted output to";
            }
        }

        private void chkEncryptVeryLargeFile_CheckedChanged(object sender, EventArgs e)
        {
            EncryptVeryLargeFile_CheckedChanged(sender, e);
        }

        public void EncryptVeryLargeFile_CheckedChanged(object sender, EventArgs e)
        {
            ReinitEncryptionFormDisplaySettings();
            if (this.chkBinaryEncryption.Checked)
            {
                this.optSaveEncryptedToFile.Checked = true;
                this.grpSaveOutputTo.Visible = false;
                this.lblEncryptionTarget.Text = "Save encrypted output to file";
            }
            else
            {
                this.grpSaveOutputTo.Visible = true;
                this.lblEncryptionTarget.Text = "Save encrypted output to";
            }
        }

        private void optSaveEncryptedToString_CheckedChanged(object sender, EventArgs e)
        {
            SaveEncryptedToObject_CheckedChanged(sender, e);
        }

        private void SaveEncryptedToObject_CheckedChanged(object sender, EventArgs e)
        {
            ReinitEncryptionFormDisplaySettings();
            this.txtEncryptionTarget.Text = string.Empty;
            if (this.optSaveEncryptedToString.Checked)
            {
                this.chkBinaryEncryption.Checked = false;
                this.chkBinaryEncryption.Visible = false;
                this.mainFormToolTips.SetToolTip(this.cmdEncryptionTarget, "Show larger text box to show encrypted string.");
                this.mainFormToolTips.SetToolTip(this.txtEncryptionTarget, "Encrypted string will be displayed here.");
            }
            else
            {
                this.chkBinaryEncryption.Visible = true;
                this.mainFormToolTips.SetToolTip(this.cmdEncryptionTarget, "Show SaveFile dialog to specify file name for encrypted file.");
                this.mainFormToolTips.SetToolTip(this.txtEncryptionTarget, "Full path for output file that contains encrypted data goes here.");
            }
        }

        //click events for decryption

        private void optDecryptString_CheckedChanged(object sender, EventArgs e)
        {
            optDecryptObject_CheckedChanged(sender, e);
        }

        private void optDecryptObject_CheckedChanged(object sender, EventArgs e)
        {
            ReinitDecryptionFormDisplaySettings();
            this.txtDecryptionSource.Text = string.Empty;
            this.txtDecryptionTarget.Text = string.Empty;
            if (this.optDecryptString.Checked)
            {
                this.lblDecryptionSource.Text = "String to decrypt:";
                this.chkBinaryDecryption.Checked = false;
                this.chkBinaryDecryption.Visible = false;
                this.optSaveDecryptedToString.Checked = true;
                this.mainFormToolTips.SetToolTip(this.cmdDecryptionSource, "Show larger text box to edit string to decrypt.");
                this.mainFormToolTips.SetToolTip(this.txtDecryptionSource, "Contains the encrypted text that will be decrypted.");
            }
            else
            {
                this.lblDecryptionSource.Text = "File to decrypt:";
                this.chkBinaryDecryption.Visible = true;
                this.optSaveDecryptedToFile.Checked = true;
                this.mainFormToolTips.SetToolTip(this.cmdDecryptionSource, "Show OpenFile dialog to select file to decrypt.");
                this.mainFormToolTips.SetToolTip(this.txtDecryptionSource, "Full path to file containing the encrypted data.");
            }
        }

        private void optDecryptAES_CheckedChanged(object sender, EventArgs e)
        {
            DecryptAlgorithm_CheckedChanged(sender, e);
        }

        private void optDecryptTripleDES_CheckedChanged(object sender, EventArgs e)
        {
            DecryptAlgorithm_CheckedChanged(sender, e);
        }

        private void optDecryptDES_CheckedChanged(object sender, EventArgs e)
        {
            DecryptAlgorithm_CheckedChanged(sender, e);
        }

        private void DecryptAlgorithm_CheckedChanged(object sender, EventArgs e)
        {
            ReinitDecryptionFormDisplaySettings();
            this.txtDecryptionKey.Text = string.Empty;
            this.txtDecryptionIV.Text = string.Empty;
        }

        private void chkBinaryDecryption_CheckedChanged(object sender, EventArgs e)
        {
            BinaryDecryption_CheckedChanged(sender, e);
        }

        public void BinaryDecryption_CheckedChanged(object sender, EventArgs e)
        {
            ReinitDecryptionFormDisplaySettings();
            if (this.chkBinaryDecryption.Checked)
            {
                this.optSaveDecryptedToFile.Checked = true;
                this.grpSaveDecryptionOutputTo.Visible = false;
                this.lblDecryptionTarget.Text = "Save decrypted output to file";
            }
            else
            {
                this.grpSaveDecryptionOutputTo.Visible = true;
                this.lblDecryptionTarget.Text = "Save decrypted output to";
            }
        }

        private void chkDecryptVeryLargeFile_CheckedChanged(object sender, EventArgs e)
        {
            DecryptVeryLargeFile_CheckedChanged(sender, e);
        }

        public void DecryptVeryLargeFile_CheckedChanged(object sender, EventArgs e)
        {
            ReinitDecryptionFormDisplaySettings();
            if (this.chkBinaryDecryption.Checked)
            {
                this.optSaveDecryptedToFile.Checked = true;
                this.grpSaveOutputTo.Visible = false;
                this.lblDecryptionTarget.Text = "Save decrypted output to file";
            }
            else
            {
                this.grpSaveOutputTo.Visible = true;
                this.lblDecryptionTarget.Text = "Save decrypted output to";
            }
        }

        private void optSaveDecryptedToString_CheckedChanged(object sender, EventArgs e)
        {
            SaveDecryptedToObject_CheckedChanged(sender, e);
        }

        private void SaveDecryptedToObject_CheckedChanged(object sender, EventArgs e)
        {
            ReinitDecryptionFormDisplaySettings();
            this.txtDecryptionTarget.Text = string.Empty;
            if (this.optSaveDecryptedToString.Checked)
            {
                this.mainFormToolTips.SetToolTip(this.cmdDecryptionTarget, "Show larger text box to show decrypted string.");
                this.mainFormToolTips.SetToolTip(this.txtDecryptionTarget, "String containing the decrypted data.");
            }
            else
            {
                this.mainFormToolTips.SetToolTip(this.cmdDecryptionTarget, "Show SaveFile dialog to specify file name for decrypted file.");
                this.mainFormToolTips.SetToolTip(this.txtDecryptionTarget, "Full path to file containing the decrypted data.");
            }
        }


        //form processing routines

        private void EnableFormControls()
        {
            TextBox txt = null;
            CheckBox chk = null;
            Button btn = null;
            MenuStrip mnu = null;
            GroupBox grp = null;
            Panel pnl = null;
            ListBox lst = null;
            ComboBox cbo = null;

            foreach (Control ctl in this.Controls)
            {
                if (ctl is MenuStrip)
                {
                    mnu = (MenuStrip)ctl;
                    foreach (ToolStripItem itm in mnu.Items)
                    {
                        itm.Enabled = true;
                    }
                }
                if (ctl is TextBox)
                {
                    txt = (TextBox)ctl;
                    ctl.Enabled = true;
                }
                if (ctl is CheckBox)
                {
                    chk = (CheckBox)ctl;
                    chk.Enabled = true;
                }
                if (ctl is Button)
                {
                    btn = (Button)ctl;
                    btn.Enabled = true;
                }
                if (ctl is GroupBox)
                {
                    grp = (GroupBox)ctl;
                    grp.Enabled = true;
                }
                if (ctl is Panel)
                {
                    pnl = (Panel)ctl;
                    pnl.Enabled = true;
                }
                if (ctl is ListBox)
                {
                    lst = (ListBox)ctl;
                    lst.Enabled = true;
                }
                if (ctl is ComboBox)
                {
                    cbo = (ComboBox)ctl;
                    cbo.Enabled = true;
                }

            }//end foreach
        }//end method

        private void DisableFormControls()
        {
            TextBox txt = null;
            CheckBox chk = null;
            Button btn = null;
            MenuStrip mnu = null;
            GroupBox grp = null;
            Panel pnl = null;
            ListBox lst = null;
            ComboBox cbo = null;

            foreach (Control ctl in this.Controls)
            {
                if (ctl is MenuStrip)
                {
                    mnu = (MenuStrip)ctl;
                    foreach (ToolStripItem itm in mnu.Items)
                    {
                        itm.Enabled = false;
                    }
                }
                if (ctl is TextBox)
                {
                    txt = (TextBox)ctl;
                    ctl.Enabled = false;
                }
                if (ctl is CheckBox)
                {
                    chk = (CheckBox)ctl;
                    chk.Enabled = false;
                }
                if (ctl is Button)
                {
                    btn = (Button)ctl;
                    btn.Enabled = false;
                }
                if (ctl is GroupBox)
                {
                    grp = (GroupBox)ctl;
                    grp.Enabled = false;
                }
                if (ctl is Panel)
                {
                    pnl = (Panel)ctl;
                    pnl.Enabled = false;
                }
                if (ctl is ListBox)
                {
                    lst = (ListBox)ctl;
                    lst.Enabled = false;
                }
                if (ctl is ComboBox)
                {
                    cbo = (ComboBox)ctl;
                    cbo.Enabled = false;
                }

            }//end foreach control

        }//end method

        //routines for processing file open, file save and folder browser dialogs
        private DialogResult ShowOpenFileDialog()
        {
            DialogResult res = DialogResult.None;
            _openFileDialog.InitialDirectory = _saveSelectionsFolder;
            _openFileDialog.FileName = string.Empty; // _saveSelectionsFile;
            _openFileDialog.Filter = _saveFilter;
            _openFileDialog.FilterIndex = _saveFilterIndex;
            _openFileDialog.Multiselect = _saveMultiSelect;
            _saveSelectionsFile = string.Empty;
            _saveSelectedFiles = null;
            res = _openFileDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                _saveSelectionsFolder = Path.GetDirectoryName(_openFileDialog.FileName);
                _saveSelectionsFile = _openFileDialog.FileName;
                _saveFilterIndex = _openFileDialog.FilterIndex;
                if (_openFileDialog.Multiselect)
                {
                    _saveSelectedFiles = _openFileDialog.FileNames;
                }
            }
            return res;
        }

        private DialogResult ShowSaveFileDialog()
        {
            DialogResult res = DialogResult.None;
            _saveFileDialog.InitialDirectory = _saveSelectionsFolder;
            _saveFileDialog.FileName = _saveSelectionsFile;
            _saveFileDialog.Filter = _saveFilter;
            _saveFileDialog.FilterIndex = _saveFilterIndex;
            _saveFileDialog.CreatePrompt = _showCreatePrompt;
            _saveFileDialog.OverwritePrompt = _showOverwritePrompt;
            res = _saveFileDialog.ShowDialog();
            _saveSelectionsFile = string.Empty;
            if (res == DialogResult.OK)
            {
                _saveSelectionsFolder = Path.GetDirectoryName(_saveFileDialog.FileName);
                _saveSelectionsFile = _saveFileDialog.FileName;
                _saveFilterIndex = _saveFileDialog.FilterIndex;
            }
            return res;
        }

        private DialogResult ShowFolderBrowserDialog()
        {
            DialogResult res = DialogResult.None;

            string folderPath = string.Empty;

            if (_saveSelectionsFolder.Length > 0)
                folderPath = _saveSelectionsFolder;
            else
                folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _folderBrowserDialog.ShowNewFolderButton = _showNewFolderButton;
            //_folderBrowserDialog.RootFolder = 
            _folderBrowserDialog.SelectedPath = folderPath;
            res = _folderBrowserDialog.ShowDialog();
            if (res != DialogResult.Cancel)
            {
                folderPath = _folderBrowserDialog.SelectedPath;
                _str.Length = 0;
                _str.Append(folderPath);
                if (folderPath.EndsWith(@"\") == false)
                    _str.Append(@"\");
                _saveSelectionsFolder = folderPath;
            }


            return res;
        }

        //application routines

        //encryption routines
        
        internal void Encrypt()
        {

            try
            {
                DisableFormControls();
                this.Cursor = Cursors.WaitCursor;
                pfEncryptorRequest er = CreateEncryptionRequestObject();
                bool encryptionSuccessful = false;
                _numCallbacks = 0;
                _appProcessor.currentStatusReport += ShowEncryptStatus;
                string encryptedText =  _appProcessor.Encrypt(er, ref encryptionSuccessful);

                if (_numCallbacks == 0)
                {
                    if (encryptionSuccessful == true)
                        this.txtEncryptResult.Text = ENCRYPT_SUCCESSFUL;
                    else
                        this.txtEncryptResult.Text = ENCRYPT_FAILED;
                }

                //if (this.optSaveEncryptedToString.Checked)
                //    this.txtEncryptionTarget.Text = encryptedText;
                this.txtEncryptionTarget.Text = encryptedText;
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _appProcessor.currentStatusReport -= ShowEncryptStatus;
                EnableFormControls();
                this.Cursor = Cursors.Default;
            }
                 
        
        }

        private void ShowEncryptStatus(string operationType, string operationState, long totalBytesProcessed, long totalSeconds, string formattedElapsedTime)
        {
            _numCallbacks++;
            _msg.Length = 0;
            _msg.Append(operationType);
            _msg.Append(" ");
            _msg.Append(operationState);
            _msg.Append(". Bytes  ");
            _msg.Append(totalBytesProcessed.ToString("#,##0"));
            _msg.Append("  Time: ");
            _msg.Append(formattedElapsedTime);
            this.txtEncryptResult.Text = _msg.ToString();
            this.txtEncryptResult.Refresh();
            this.Refresh();
        }

        internal void GenerateRandomKeyIV()
        {
            pfEncryptionAlgorithm alg = GetCurrentEncryptionAlgorithm();
            pfKeyIvPair ki = _appProcessor.GenerateKeyIVPair(alg);
            this.txtEncryptionKey.Text = ki.key;
            this.txtEncryptionIV.Text = ki.IV;
        }

        public void GetFileToEncrypt()
        {
            _saveSelectionsFolder = string.Empty;
            if (this.txtEncryptionSource.Text.Trim().Length > 0)
            {
                _saveSelectionsFile = this.txtEncryptionSource.Text;
                string folderPath = Path.GetDirectoryName(_saveSelectionsFile);
                if (folderPath.Length > 0)
                    _saveSelectionsFolder = folderPath;
                if (_saveSelectionsFolder == string.Empty)
                {
                    _saveSelectionsFolder = Properties.Settings.Default.DefaultEncryptionSourceFolder;
                }

            }
            else
            {
                _saveSelectionsFile = string.Empty;
                string folderPath = Properties.Settings.Default.DefaultEncryptionSourceFolder;
                if (folderPath.Length > 0)
                    _saveSelectionsFolder = folderPath;
            }
            if (_saveSelectionsFolder == string.Empty)
            {
                _saveSelectionsFolder = _defaultEncryptSourceFolder;
            }
            _saveFilter = "All Files|*.*";
            DialogResult res = ShowOpenFileDialog();
            if (res == DialogResult.OK && _saveSelectionsFile.Trim().Length > 0)
            {
                this.txtEncryptionSource.Text = _saveSelectionsFile;
                this.txtEncryptionTarget.Text = string.Empty;
                string folderPath = Path.GetDirectoryName(_saveSelectionsFile);
                if(folderPath.Length > 0)
                    Properties.Settings.Default["DefaultEncryptionSourceFolder"] = folderPath;
                else
                    Properties.Settings.Default["DefaultEncryptionSourceFolder"] = _defaultEncryptSourceFolder;
                Properties.Settings.Default.Save();
            }
        }

        public void GetStringToEncrypt()
        {
            TextDisplayForm frm = new TextDisplayForm();
            frm.TextToProcess = this.txtEncryptionSource.Text;
            frm.TextToProcessReadOnly = false;
            frm.ShowCancelCommand = true;
            frm.Caption = "Text to Encrypt";

            DialogResult res = frm.ShowDialog();
            if (res == DialogResult.OK)
            {
                this.txtEncryptionSource.Text = frm.TextToProcess;
                this.txtEncryptionTarget.Text = string.Empty;
            }
            frm.Close();
        }

        public void GetFileNameForEncryptedOutput()
        {
            _saveSelectionsFolder = string.Empty;
            if (this.txtEncryptionTarget.Text.Trim().Length == 0
                && this.txtEncryptionSource.Text.Trim().Length > 0
                && this.optEncryptFile.Checked
                && this.optSaveEncryptedToFile.Checked)
            {
                _saveSelectionsFile = Path.GetFileName(this.txtEncryptionSource.Text) + ".encrypted";
                string folderPath = Path.GetDirectoryName(_saveSelectionsFile);
                if (folderPath.Length > 0)
                    _saveSelectionsFolder = folderPath;
                if (_saveSelectionsFolder == string.Empty)
                {
                    _saveSelectionsFolder = Properties.Settings.Default.DefaultEncryptionDestinationFolder;
                }

            }
            else
            {
                _saveSelectionsFile = string.Empty;
                string folderPath = Properties.Settings.Default.DefaultEncryptionDestinationFolder;
                if (folderPath.Length > 0)
                    _saveSelectionsFolder = folderPath;
                else
                    _saveSelectionsFolder = _defaultEncryptDestinationFolder;
            }
            if (_saveSelectionsFolder == string.Empty)
            {
                _saveSelectionsFolder = _defaultEncryptDestinationFolder;
            }
            _saveFilter = "All Files|*.*";
            DialogResult res = ShowSaveFileDialog();
            if (res == DialogResult.OK && _saveSelectionsFile.Trim().Length > 0)
            {
                this.txtEncryptionTarget.Text = _saveSelectionsFile;
                string folderPath = Path.GetDirectoryName(_saveSelectionsFile);
                if (folderPath.Length > 0)
                    Properties.Settings.Default["DefaultEncryptionDestinationFolder"] = folderPath;
                else
                    Properties.Settings.Default["DefaultEncryptionDestinationFolder"] = _defaultEncryptDestinationFolder;
                Properties.Settings.Default.Save();
            }
        }

        public void ShowEncryptedString()
        {
            TextDisplayForm frm = new TextDisplayForm();
            frm.TextToProcess = this.txtEncryptionTarget.Text;
            frm.TextToProcessReadOnly = true;
            frm.ShowCancelCommand = false;
            frm.Caption = "Encrypted Text";

            DialogResult res = frm.ShowDialog();
       
            frm.Close();
        }

        public void SaveKeyIVToFile()
        {
            if (_keyIVFilePath.Trim().Length == 0)
                _saveSelectionsFolder = _defaultKeyIVSaveFolder;
            else
            {
                string folderPath = Path.GetDirectoryName(_keyIVFilePath);
                if (folderPath.Length == 0)
                    _saveSelectionsFolder = _defaultKeyIVSaveFolder;
                else
                    _saveSelectionsFolder = folderPath;
            }
            _saveFilter = "XML Files|*.XML";
            DialogResult res = ShowSaveFileDialog();
            if (res == DialogResult.OK && _saveSelectionsFile.Trim().Length > 0)
            {
                pfEncryptionAlgorithm alg = GetCurrentEncryptionAlgorithm();
                pfKeyIvPair kvp = new pfKeyIvPair();
                kvp.key = this.txtEncryptionKey.Text;
                kvp.IV = this.txtEncryptionIV.Text;
                _appProcessor.SaveKeyIVPair(alg, kvp, _saveSelectionsFile);
            }
        }

        private pfEncryptorObjectType GetCurrentEncryptionObjectType()
        {
            pfEncryptorObjectType typ = this.optEncryptFile.Checked ? pfEncryptorObjectType.File : pfEncryptorObjectType.String;
            return typ;
        }

        private pfEncryptionAlgorithm GetCurrentEncryptionAlgorithm()
        {
            pfEncryptionAlgorithm alg = this.optEncryptAES.Checked ? pfEncryptionAlgorithm.AES : this.optEncryptTripleDES.Checked ? pfEncryptionAlgorithm.TripleDES : pfEncryptionAlgorithm.DES;
            return alg;
        }

        private void SetCurrentEncryptionAlgorithm(pfEncryptionAlgorithm alg)
        {
            if (alg == pfEncryptionAlgorithm.AES)
                this.optEncryptAES.Checked = true;
            else if (alg == pfEncryptionAlgorithm.TripleDES)
                this.optEncryptTripleDES.Checked = true;
            else
                this.optEncryptDES.Checked = true;
        }

        public void LoadKeyIVFromFile()
        {
            if (_keyIVFilePath.Trim().Length == 0)
                _saveSelectionsFolder = _defaultKeyIVSaveFolder;
            else
            {
                string folderPath = Path.GetDirectoryName(_keyIVFilePath);
                if (folderPath.Length == 0)
                    _saveSelectionsFolder = _defaultKeyIVSaveFolder;
                else
                    _saveSelectionsFolder = folderPath;
            }
            _saveFilter = "XML Files|*.XML";
            DialogResult res = ShowOpenFileDialog();
            if (res == DialogResult.OK && _saveSelectionsFile.Trim().Length > 0)
            {
                PFKeyIVValues kvv = PFKeyIVValues.LoadFromXmlFile(_saveSelectionsFile);
                SetCurrentEncryptionAlgorithm(kvv.Algorithm);
                this.txtEncryptionKey.Text = kvv.Key;
                this.txtEncryptionIV.Text = kvv.IV;
            }
        }

        public void NewEncryptionDef()
        {
            InitEncryptionFormDisplaySettings();
        }

        public void SaveEncryptionDef()
        {
            string folderPath = _defaultEncryptionDefinitionsFolder;
            if (_encryptionDefinitionsFile.Length > 0)
            {
                folderPath = Path.GetDirectoryName(_encryptionDefinitionsFile);
                if (folderPath.Length == 0)
                    folderPath = _defaultEncryptionDefinitionsFolder;
            }
            _saveFilter = "XML Files|*.XML";
            _saveSelectionsFile = string.Empty;
            _saveSelectionsFolder = folderPath;
            DialogResult res = ShowSaveFileDialog();
            if (res == DialogResult.OK && _saveSelectionsFile.Trim().Length > 0)
            {
                pfEncryptorRequest er = CreateEncryptionRequestObject();
                bool saveSucceeded = _appProcessor.SaveEncryptionRequestToFile(er, _saveSelectionsFile);
                if (saveSucceeded)
                    this.txtEncryptResult.Text = ENCRYPT_SAVE_SUCCESSFUL;
                else
                    this.txtEncryptResult.Text = ENCRYPT_SAVE_FAILED;
            }

        }

        public void LoadEncryptionDef()
        {
            string folderPath = _defaultEncryptionDefinitionsFolder;
            if (_encryptionDefinitionsFile.Length > 0)
            {
                folderPath = Path.GetDirectoryName(_encryptionDefinitionsFile);
                if (folderPath.Length == 0)
                    folderPath = _defaultEncryptionDefinitionsFolder;
            }
            _saveFilter = "XML Files|*.XML";
            _saveSelectionsFile = string.Empty;
            _saveSelectionsFolder = folderPath;
            DialogResult res = ShowOpenFileDialog();
            if (res == DialogResult.OK && _saveSelectionsFile.Trim().Length > 0)
            {
                InitEncryptionFormDisplaySettings();
                pfEncryptorRequest er = new pfEncryptorRequest();
                bool loadSucceeded = _appProcessor.LoadEncryptionRequestFromFile(_saveSelectionsFile, ref er);
                if (loadSucceeded)
                {
                    LoadEncryptionRequestObject(er);
                    this.txtEncryptResult.Text = ENCRYPT_LOAD_SUCCESSFUL;
                }
                else
                    this.txtEncryptResult.Text = ENCRYPT_LOAD_FAILED;
            }

        }

        private pfEncryptorRequest CreateEncryptionRequestObject()
        {
            pfEncryptorRequest er = new pfEncryptorRequest();
            er.OperationType = pfEncryptorOperationType.Encryption;
            if (this.optEncryptFile.Checked)
                er.SourceObjectType = pfEncryptorObjectType.File;
            else
                er.SourceObjectType = pfEncryptorObjectType.String;
            if (this.optSaveEncryptedToFile.Checked)
                er.DestinationObjectType = pfEncryptorObjectType.File;
            else
                er.DestinationObjectType = pfEncryptorObjectType.String;
            er.EncryptionAlgorithm = GetCurrentEncryptionAlgorithm();
            er.EncryptionKey = this.txtEncryptionKey.Text;
            er.EncryptionIV = this.txtEncryptionIV.Text;
            er.SourceObject = this.txtEncryptionSource.Text;
            er.DestinationObject = this.txtEncryptionTarget.Text;
            er.UseBinaryEncryption = this.chkBinaryEncryption.Checked;

            return er;
        }

        private void LoadEncryptionRequestObject(pfEncryptorRequest er)
        {

            try
            {
                if (er.OperationType != pfEncryptorOperationType.Encryption)
                {
                    _msg.Length = 0;
                    _msg.Append("Error: File did not store an encryption request. ");
                    _msg.Append(er.OperationType.ToString());
                    _msg.Append(" operation type was stored in the file.");
                    throw new System.Exception(_msg.ToString());
                }

                if (er.SourceObjectType == pfEncryptorObjectType.File)
                    this.optEncryptFile.Checked = true;
                else
                    this.optEncryptString.Checked = true;
                if(er.DestinationObjectType == pfEncryptorObjectType.File)
                    this.optSaveEncryptedToFile.Checked = true;
                else
                    this.optSaveEncryptedToString.Checked = true;
                if(er.EncryptionAlgorithm == pfEncryptionAlgorithm.AES)
                    this.optEncryptAES.Checked = true;
                else if (er.EncryptionAlgorithm == pfEncryptionAlgorithm.TripleDES)
                    this.optEncryptTripleDES.Checked = true;
                else
                    this.optEncryptDES.Checked = true;
                this.txtEncryptionKey.Text = er.EncryptionKey;
                this.txtEncryptionIV.Text = er.EncryptionIV;
                this.txtEncryptionSource.Text=er.SourceObject;
                this.txtEncryptionTarget.Text=er.DestinationObject;
                this.chkBinaryEncryption.Checked=er.UseBinaryEncryption;

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                ;
            }
                 
        
        }

        //Decryption routines
        
        public void CopyKeyIVFromAbove()
        {
            this.optDecryptFile.Checked = this.optSaveEncryptedToFile.Checked;
            this.optDecryptString.Checked = this.optSaveEncryptedToString.Checked;
            this.optDecryptAES.Checked = this.optEncryptAES.Checked;
            this.optDecryptTripleDES.Checked = this.optEncryptTripleDES.Checked;
            this.optDecryptDES.Checked = this.optEncryptDES.Checked;
            this.txtDecryptionKey.Text = this.txtEncryptionKey.Text;
            this.txtDecryptionIV.Text = this.txtEncryptionIV.Text;
            this.chkBinaryDecryption.Checked = this.chkBinaryEncryption.Checked;
            if (this.txtEncryptionTarget.Text.Length > 0)
            {
                if (this.txtDecryptionSource.Text.Length == 0)
                {
                    this.txtDecryptionSource.Text = this.txtEncryptionTarget.Text;
                    this.txtDecryptionTarget.Text = string.Empty;
                    this.chkBinaryDecryption.Checked = this.chkBinaryEncryption.Checked;
                }
            }
            this.optSaveDecryptedToFile.Checked = this.optSaveEncryptedToFile.Checked;
            this.optSaveDecryptedToString.Checked = this.optSaveEncryptedToString.Checked;
        }

        internal void Decrypt()
        {

            try
            {
                DisableFormControls();
                Cursor.Current = Cursors.WaitCursor;
                pfEncryptorRequest er = CreateDecryptionRequestObject();
                bool decryptionSuccessful = false;
                _numCallbacks = 0;
                _appProcessor.currentStatusReport += ShowDecryptStatus;
                string decryptedText = _appProcessor.Decrypt(er, ref decryptionSuccessful);

                if (_numCallbacks == 0)
                {
                    if (decryptionSuccessful == true)
                        this.txtDecryptionResult.Text = DECRYPT_SUCCESSFUL;
                    else
                        this.txtDecryptionResult.Text = DECRYPT_FAILED;
                }


                //if (this.optSaveDecryptedToString.Checked)
                //    this.txtDecryptionTarget.Text = decryptedText;
                this.txtDecryptionTarget.Text = decryptedText;
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _appProcessor.currentStatusReport -= ShowDecryptStatus;
                EnableFormControls();
                Cursor.Current = Cursors.Default;
            }


        }

        private void ShowDecryptStatus(string operationType, string operationState, long totalBytesProcessed, long totalSeconds, string formattedElapsedTime)
        {
            _numCallbacks++;
            _msg.Length = 0;
            _msg.Append(operationType);
            _msg.Append(" ");
            _msg.Append(operationState);
            _msg.Append(". Bytes  ");
            _msg.Append(totalBytesProcessed.ToString("#,##0"));
            _msg.Append("  Time: ");
            _msg.Append(formattedElapsedTime);
            this.txtDecryptionResult.Text = _msg.ToString();
            this.txtDecryptionResult.Refresh();
            this.Refresh();
        }

        public void GetFileToDecrypt()
        {
            _saveSelectionsFolder = string.Empty;
            if (this.txtDecryptionSource.Text.Trim().Length > 0)
            {
                _saveSelectionsFile = this.txtDecryptionSource.Text;
                string folderPath = Path.GetDirectoryName(_saveSelectionsFile);
                if (folderPath.Length > 0)
                    _saveSelectionsFolder = folderPath;
                if (_saveSelectionsFolder == string.Empty)
                {
                    _saveSelectionsFolder = Properties.Settings.Default.DefaultDecryptionSourceFolder;
                }
            }
            else
            {
                _saveSelectionsFile = string.Empty;
                if (this.txtEncryptionTarget.Text.Length > 0)
                {
                    _saveSelectionsFile = Path.GetFileName(this.txtEncryptionTarget.Text);
                    _saveSelectionsFolder = Path.GetDirectoryName(this.txtEncryptionTarget.Text);
                }
                else
                {
                    string folderPath = Properties.Settings.Default.DefaultDecryptionSourceFolder;
                    if (folderPath.Length > 0)
                        _saveSelectionsFolder = folderPath;
                }
            }
            if (_saveSelectionsFolder == string.Empty)
            {
                _saveSelectionsFolder = _defaultDecryptSourceFolder;
            }
            _saveFilter = "All Files|*.*";
            DialogResult res = ShowOpenFileDialog();
            if (res == DialogResult.OK && _saveSelectionsFile.Trim().Length > 0)
            {
                this.txtDecryptionSource.Text = _saveSelectionsFile;
                this.txtDecryptionTarget.Text = string.Empty;
                string folderPath = Path.GetDirectoryName(_saveSelectionsFile);
                if (folderPath.Length > 0)
                    Properties.Settings.Default["DefaultDecryptionSourceFolder"] = folderPath;
                else
                    Properties.Settings.Default["DefaultDecryptionSourceFolder"] = _defaultDecryptSourceFolder;
                Properties.Settings.Default.Save();

            }
        }

        public void GetStringToDecrypt()
        {
            TextDisplayForm frm = new TextDisplayForm();
            frm.TextToProcess = this.txtDecryptionSource.Text;
            frm.TextToProcessReadOnly = false;
            frm.ShowCancelCommand = true;
            frm.Caption = "Text to Decrypt";

            DialogResult res = frm.ShowDialog();
            if (res == DialogResult.OK)
            {
                this.txtDecryptionSource.Text = frm.TextToProcess;
                this.txtDecryptionTarget.Text = string.Empty;
            }
            frm.Close();
        }

        public void GetFileNameForDecryptedOutput()
        {
            _saveSelectionsFolder = string.Empty;
            if (this.txtDecryptionTarget.Text.Trim().Length == 0
                && this.txtDecryptionSource.Text.Trim().Length > 0
                && this.optDecryptFile.Checked
                && this.optSaveDecryptedToFile.Checked)
            {
                string filename = Path.GetFileName(this.txtDecryptionSource.Text);
                if (filename.ToLower().EndsWith(".encrypted"))
                {
                    _saveSelectionsFile = filename.Replace(".encrypted", ".decrypted");
                }
                else
                {
                    _saveSelectionsFile = Path.GetFileName(this.txtDecryptionSource.Text) + ".decrypted";
                }
                string folderPath = Path.GetDirectoryName(_saveSelectionsFile);
                if (folderPath.Length > 0)
                    _saveSelectionsFolder = folderPath;
                if (_saveSelectionsFolder == string.Empty)
                {
                    _saveSelectionsFolder = Properties.Settings.Default.DefaultDecryptionDestinationFolder;
                }
            }
            else
            {
                _saveSelectionsFile = string.Empty;
                string folderPath = Properties.Settings.Default.DefaultDecryptionDestinationFolder;
                if (folderPath.Length > 0)
                    _saveSelectionsFolder = folderPath;
            }
            if (_saveSelectionsFolder == string.Empty)
            {
                _saveSelectionsFolder = _defaultDecryptDestinationFolder;
            }

            _saveFilter = "All Files|*.*";
            DialogResult res = ShowSaveFileDialog();
            if (res == DialogResult.OK && _saveSelectionsFile.Trim().Length > 0)
            {
                this.txtDecryptionTarget.Text = _saveSelectionsFile;
                string folderPath = Path.GetDirectoryName(_saveSelectionsFile);
                if (folderPath.Length > 0)
                    Properties.Settings.Default["DefaultDecryptionDestinationFolder"] = folderPath;
                else
                    Properties.Settings.Default["DefaultDecryptionDestinationFolder"] = _defaultDecryptDestinationFolder;
                Properties.Settings.Default.Save();
            }
        }

        public void ShowDecryptedString()
        {
            TextDisplayForm frm = new TextDisplayForm();
            frm.TextToProcess = this.txtDecryptionTarget.Text;
            frm.TextToProcessReadOnly = true;
            frm.ShowCancelCommand = false;
            frm.Caption = "Decrypted Text";

            DialogResult res = frm.ShowDialog();

            frm.Close();
        }

        public void SaveDecryptionKeyIVToFile()
        {
            if (_keyIVFilePath.Trim().Length == 0)
                _saveSelectionsFolder = _defaultKeyIVSaveFolder;
            else
            {
                string folderPath = Path.GetDirectoryName(_keyIVFilePath);
                if (folderPath.Length == 0)
                    _saveSelectionsFolder = _defaultKeyIVSaveFolder;
                else
                    _saveSelectionsFolder = folderPath;
            }
            _saveFilter = "XML Files|*.XML";
            DialogResult res = ShowSaveFileDialog();
            if (res == DialogResult.OK && _saveSelectionsFile.Trim().Length > 0)
            {
                pfEncryptionAlgorithm alg = GetCurrentDecryptionAlgorithm();
                pfKeyIvPair kvp = new pfKeyIvPair();
                kvp.key = this.txtDecryptionKey.Text;
                kvp.IV = this.txtDecryptionIV.Text;
                _appProcessor.SaveKeyIVPair(alg, kvp, _saveSelectionsFile);
            }
        }

        private pfEncryptorObjectType GetCurrentDecryptionObjectType()
        {
            pfEncryptorObjectType typ = this.optDecryptFile.Checked ? pfEncryptorObjectType.File : pfEncryptorObjectType.String;
            return typ;
        }

        private pfEncryptionAlgorithm GetCurrentDecryptionAlgorithm()
        {
            pfEncryptionAlgorithm alg = this.optDecryptAES.Checked ? pfEncryptionAlgorithm.AES : this.optDecryptTripleDES.Checked ? pfEncryptionAlgorithm.TripleDES : pfEncryptionAlgorithm.DES;
            return alg;
        }

        private void SetCurrentDecryptionAlgorithm(pfEncryptionAlgorithm alg)
        {
            if (alg == pfEncryptionAlgorithm.AES)
                this.optDecryptAES.Checked = true;
            else if (alg == pfEncryptionAlgorithm.TripleDES)
                this.optDecryptTripleDES.Checked = true;
            else
                this.optDecryptDES.Checked = true;
        }

        public void LoadDecryptionKeyIVFromFile()
        {
            if (_keyIVFilePath.Trim().Length == 0)
                _saveSelectionsFolder = _defaultKeyIVSaveFolder;
            else
            {
                string folderPath = Path.GetDirectoryName(_keyIVFilePath);
                if (folderPath.Length == 0)
                    _saveSelectionsFolder = _defaultKeyIVSaveFolder;
                else
                    _saveSelectionsFolder = folderPath;
            }
            _saveFilter = "XML Files|*.XML";
            DialogResult res = ShowOpenFileDialog();
            if (res == DialogResult.OK && _saveSelectionsFile.Trim().Length > 0)
            {
                PFKeyIVValues kvv = PFKeyIVValues.LoadFromXmlFile(_saveSelectionsFile);
                SetCurrentDecryptionAlgorithm(kvv.Algorithm);
                this.txtDecryptionKey.Text = kvv.Key;
                this.txtDecryptionIV.Text = kvv.IV;
            }
        }

        public void NewDecryptionDef()
        {
            InitDecryptionFormDisplaySettings();
        }

        public void SaveDecryptionDef()
        {
            string folderPath = _defaultDecryptionDefinitionsFolder;
            if (_decryptionDefinitionsFile.Length > 0)
            {
                folderPath = Path.GetDirectoryName(_decryptionDefinitionsFile);
                if (folderPath.Length == 0)
                    folderPath = _defaultDecryptionDefinitionsFolder;
            }
            _saveFilter = "XML Files|*.XML";
            _saveSelectionsFile = string.Empty;
            _saveSelectionsFolder = folderPath;
            DialogResult res = ShowSaveFileDialog();
            if (res == DialogResult.OK && _saveSelectionsFile.Trim().Length > 0)
            {
                pfEncryptorRequest er = CreateDecryptionRequestObject();
                bool saveSucceeded = _appProcessor.SaveEncryptionRequestToFile(er, _saveSelectionsFile);
                if (saveSucceeded)
                    this.txtDecryptionResult.Text = DECRYPT_SAVE_SUCCESSFUL;
                else
                    this.txtDecryptionResult.Text = DECRYPT_SAVE_FAILED;
            }

        }

        public void LoadDecryptionDef()
        {
            string folderPath = _defaultDecryptionDefinitionsFolder;
            if (_decryptionDefinitionsFile.Length > 0)
            {
                folderPath = Path.GetDirectoryName(_decryptionDefinitionsFile);
                if (folderPath.Length == 0)
                    folderPath = _defaultDecryptionDefinitionsFolder;
            }
            _saveFilter = "XML Files|*.XML";
            _saveSelectionsFile = string.Empty;
            _saveSelectionsFolder = folderPath;
            DialogResult res = ShowOpenFileDialog();
            if (res == DialogResult.OK && _saveSelectionsFile.Trim().Length > 0)
            {
                InitDecryptionFormDisplaySettings();
                pfEncryptorRequest er = new pfEncryptorRequest();
                bool loadSucceeded = _appProcessor.LoadEncryptionRequestFromFile(_saveSelectionsFile, ref er);
                if (loadSucceeded)
                {
                    LoadDecryptionRequestObject(er);
                    this.txtDecryptionResult.Text = DECRYPT_LOAD_SUCCESSFUL;
                }
                else
                    this.txtDecryptionResult.Text = DECRYPT_LOAD_FAILED;
            }

        }

        private pfEncryptorRequest CreateDecryptionRequestObject()
        {
            pfEncryptorRequest er = new pfEncryptorRequest();
            er.OperationType = pfEncryptorOperationType.Decryption;
            if (this.optDecryptFile.Checked)
                er.SourceObjectType = pfEncryptorObjectType.File;
            else
                er.SourceObjectType = pfEncryptorObjectType.String;
            if (this.optSaveDecryptedToFile.Checked)
                er.DestinationObjectType = pfEncryptorObjectType.File;
            else
                er.DestinationObjectType = pfEncryptorObjectType.String;
            er.EncryptionAlgorithm = GetCurrentDecryptionAlgorithm();
            er.EncryptionKey = this.txtDecryptionKey.Text;
            er.EncryptionIV = this.txtDecryptionIV.Text;
            er.SourceObject = this.txtDecryptionSource.Text;
            er.DestinationObject = this.txtDecryptionTarget.Text;
            er.UseBinaryEncryption = this.chkBinaryDecryption.Checked;

            return er;
        }

        private void LoadDecryptionRequestObject(pfEncryptorRequest er)
        {

            try
            {
                if (er.OperationType != pfEncryptorOperationType.Decryption)
                {
                    _msg.Length = 0;
                    _msg.Append("Error: File did not store an decryption request. ");
                    _msg.Append(er.OperationType.ToString());
                    _msg.Append(" operation type was stored in the file.");
                    throw new System.Exception(_msg.ToString());
                }

                if (er.SourceObjectType == pfEncryptorObjectType.File)
                    this.optDecryptFile.Checked = true;
                else
                    this.optDecryptString.Checked = true;
                if (er.DestinationObjectType == pfEncryptorObjectType.File)
                    this.optSaveDecryptedToFile.Checked = true;
                else
                    this.optSaveDecryptedToString.Checked = true;
                if (er.EncryptionAlgorithm == pfEncryptionAlgorithm.AES)
                    this.optDecryptAES.Checked = true;
                else if (er.EncryptionAlgorithm == pfEncryptionAlgorithm.TripleDES)
                    this.optDecryptTripleDES.Checked = true;
                else
                    this.optDecryptDES.Checked = true;
                this.txtDecryptionKey.Text = er.EncryptionKey;
                this.txtDecryptionIV.Text = er.EncryptionIV;
                this.txtDecryptionSource.Text = er.SourceObject;
                this.txtDecryptionTarget.Text = er.DestinationObject;
                this.chkBinaryDecryption.Checked = er.UseBinaryEncryption;

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                ;
            }


        }




    }//end class
}//end namespace
