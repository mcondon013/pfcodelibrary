//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2013
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using AppGlobals;
using System.IO;
using System.Diagnostics;

namespace PFEncryptionObjects
{
    /// <summary>
    /// Class contains functionality for encrypting PGP files using the GPG command line utility.
    /// See GPGDecryptor class for functionality to decrypt PGP files using the GPG command line utility.
    /// </summary>
    public class GPGEncryptor
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private varialbles for encryption properties
        private string _gpgEncryptionFileExtension = "PGP";
        private string[] _expectedEncryptionFileExtensions;
        private string _gpgAppPath = string.Empty;
        private string _gpgWorkingFolder = string.Empty;
        private string _gpgHomeDirOption = string.Empty;
        private string _windowsCmdExePath = string.Empty;

        private string _source = string.Empty;
        private string _destination = string.Empty;
        private string _encryptionKey = string.Empty;
        private bool _asciiArmor = true;
        private string _processCommandLine = string.Empty;
        private string _processMessages = string.Empty;
        private int _processExitCode = -1;

        private string _defaultGPGEncryptionFileExtension = "PGP";
        private string _defaultExpectedEncryptionFileExtensions = "PGP,GPG,DEZ,ENC,RSA";
        private string _defaultGPGAppPath = Environment.Is64BitOperatingSystem ? Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\GNU\GnuPG\" + "gpg2.exe"
                                                                             : Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\GNU\GnuPG\" + "gpg2.exe";
        private string _defaultGPGWorkingFolder = Environment.Is64BitOperatingSystem ? Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\GNU\GnuPG"
                                                                                     : Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\GNU\GnuPG";
        private string _defaultGPGHomeDirOption = Environment.Is64BitOperatingSystem ? Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\GNU\GnuPG"
                                                                                     : Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\GNU\GnuPG";
        private string _defaultWindowsCmdExePath = Environment.Is64BitOperatingSystem ? Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\cmd.exe"
                                                                                      : Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\cmd.exe";


#pragma warning disable 1591
        public enum FileEncryptorResult
        {
            FileStatusUnknown = 0,
            FileAlreadyEncrypted = 1,
            EncryptedFileCreated = 2,
            EncryptionNotRequired = 3,
            FileNotClosed = 4,
            EncryptionFailed = 999
        }
#pragma warning restore 1591


        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public GPGEncryptor()
        {
            InitializeFileEncryptor();
        }

        private void InitializeFileEncryptor()
        {
            string configValue = string.Empty;

            //get file extension to use for any files encrypted by this class
            configValue = AppConfig.GetStringValueFromConfigFile("GPGEncryptionFileExtension", _defaultGPGEncryptionFileExtension);
            _gpgEncryptionFileExtension = configValue;

            //get list of expected encrypted file extensions
            configValue = AppConfig.GetStringValueFromConfigFile("ExpectedEncryptionFileExtensions", _defaultExpectedEncryptionFileExtensions);
            if (configValue.Length == 0)
                configValue = "PGP";
            _expectedEncryptionFileExtensions = configValue.Split(',');

            //get app path for the GPG utility
            configValue = AppConfig.GetStringValueFromConfigFile("GPGAppPath", _defaultGPGAppPath);
            _gpgAppPath = configValue;

            //get working folder for the GPG utility
            configValue = AppConfig.GetStringValueFromConfigFile("GPGWorkingFolder", _defaultGPGWorkingFolder);
            _gpgWorkingFolder = configValue;

            //get homedir path that GPG utility will use
            configValue = AppConfig.GetStringValueFromConfigFile("GPGHomeDirOption", _defaultGPGHomeDirOption);
            _gpgHomeDirOption = configValue;

            //get homedir path that GPG utility will use
            configValue = AppConfig.GetStringValueFromConfigFile("WindowsCmdExePath", _defaultWindowsCmdExePath);
            _windowsCmdExePath = configValue;



        }



        //properties
        /// <summary>
        /// File to be encrypted.
        /// </summary>
        public string Source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
            }
        }

        /// <summary>
        /// Location of encrypted output.
        /// </summary>
        public string Destination
        {
            get
            {
                return _destination;
            }
            set
            {
                _destination = value;
            }
        }

        /// <summary>
        /// File extension to use for encrypted file.
        /// </summary>
        public string GPGEncryptionFileExtension
        {
            get 
            { 
                return _gpgEncryptionFileExtension; 
            }
            set 
            { 
                _gpgEncryptionFileExtension = value; 
            }
        }

        /// <summary>
        /// Comma delimited list of file extensions that can be used for encrypted output files.
        /// </summary>
        public string[] ExpectedEncryptionFileExtensions
        {
            get
            {
                return _expectedEncryptionFileExtensions;
            }
            set
            {
                _expectedEncryptionFileExtensions = value;
            }
        }

        /// <summary>
        /// Path to the GPG executable.
        /// </summary>
        public string GpgAppPath
        {
            get
            {
                return _gpgAppPath;
            }
            set
            {
                _gpgAppPath = value;
            }
        }

        /// <summary>
        /// Working folder to use.
        /// </summary>
        public string GpgWorkingFolder
        {
            get
            {
                return _gpgWorkingFolder;
            }
            set
            {
                _gpgWorkingFolder = value;
            }
        }

        /// <summary>
        /// Gpg Home Director Option Property.
        /// </summary>
        public string GpgHomeDirOption
        {
            get
            {
                return _gpgHomeDirOption;
            }
            set
            {
                _gpgHomeDirOption = value;
            }
        }

        /// <summary>
        /// Path to the Windows cmd.exe batch processing program.
        /// </summary>
        public string WindowsCmdExePath
        {
            get
            {
                return _windowsCmdExePath;
            }
            set
            {
                _windowsCmdExePath = value;
            }
        }

        /// <summary>
        /// Command line used to launch the encryption process.
        /// </summary>
        /// <remarks>Useful for debugging.</remarks>
        public string ProcessCommandLine
        {
            get 
            { 
                return _processCommandLine; 
            }
            private set 
            { 
                _processCommandLine = value; 
            }
        }

        /// <summary>
        /// Messages from the process that ran the encryption.
        /// </summary>
        /// <remarks>Useful for debugging.</remarks>
        public string ProcessMessages
        {
            get 
            { 
                return _processMessages; 
            }
            private set 
            { 
                _processMessages = value; 
            }
        }

        /// <summary>
        /// Exit code from the process that ran the encryption. Exit code of 0 indicates success.
        /// </summary>
        /// <remarks>Useful for debugging in the event of an error.</remarks>
        public int ProcessExitCode
        {
            get 
            { 
                return _processExitCode; 
            }
            private set 
            { 
                _processExitCode = value; 
            }
        }

        /// <summary>
        /// Key to use for encrypting the data. Use a recipient name in the GPG key store.
        /// </summary>
        public string EncryptionKey
        {
            get 
            { 
                return _encryptionKey; 
            }
            set 
            { 
                _encryptionKey = value; 
            }
        }

        /// <summary>
        /// Set to true if you want the output ascii armored. If false, then output will be in binary format.
        /// </summary>
        public bool AsciiArmor
        {
            get
            {
                return _asciiArmor;
            }
            set
            {
                _asciiArmor = value;
            }
        }

        //methods
        /// <summary>
        /// Method to encrypt a file using PGP via the GPG command line.
        /// </summary>
        /// <param name="sourcePath">File to be encrypted.</param>
        /// <param name="destinationPath">Path to encrypted file.</param>
        /// <param name="encryptionKey">Key to use for encryption. Use a recipient name in the GPG key store.</param>
        /// <returns></returns>
        public FileEncryptorResult CreateEncryptedFile(string sourcePath,
                                                       string destinationPath,
                                                       string encryptionKey)
        {
            FileEncryptorResult result = FileEncryptorResult.FileStatusUnknown;


            if (String.IsNullOrEmpty(sourcePath))
                throw new ArgumentNullException("The name of file that needs to be encrypted cannot be null or empty.");
            if (String.IsNullOrEmpty(destinationPath))
                throw new ArgumentNullException("The name of the destination file for the encrypted data cannot be null or empty.");

            result = RunGPGEncryptor(sourcePath, destinationPath, encryptionKey);


            return result;

        }

        /// <summary>
        /// Determines whether or not the input file is already encrypted using PGP.
        /// </summary>
        /// <param name="sourcePath">File to be encrypted.</param>
        /// <returns>True if file already has the expected PGP file extension.</returns>
        /// <remarks>Use this function to bypass encryption of files that are already encrypted.</remarks>
        public bool FileHasEncryptionExtension(string sourcePath)
        {
            bool fileHasEncryptionExtension = false;
            string filePathExtension = Path.GetExtension(sourcePath).Replace(".", string.Empty);
            int inx = 0;
            int maxInx = _expectedEncryptionFileExtensions.GetUpperBound(0);
            for (inx = 0; inx <= maxInx; inx++)
            {
                if (filePathExtension.ToUpper() == _expectedEncryptionFileExtensions[inx].ToUpper())
                {
                    fileHasEncryptionExtension = true;
                    break;
                }
            }
            return fileHasEncryptionExtension;
        }

        private FileEncryptorResult RunGPGEncryptor(string sourcePath,
                                                    string destinationPath,
                                                    string encryptionKey)
        {
            FileEncryptorResult result = FileEncryptorResult.FileStatusUnknown;
            Process gpgEncryptorProcess = new Process();

            try
            {
                _processExitCode = -1;
                _processMessages = string.Empty;
                this.Source = sourcePath;
                this.Destination = destinationPath;
                this.EncryptionKey = encryptionKey;

                gpgEncryptorProcess.StartInfo.WorkingDirectory = _gpgWorkingFolder;
                gpgEncryptorProcess.StartInfo.FileName = _gpgAppPath;
                gpgEncryptorProcess.StartInfo.Arguments = BuildEncryptionArguments();
                this.ProcessCommandLine = GetCommandLine(gpgEncryptorProcess.StartInfo.Arguments);
                gpgEncryptorProcess.StartInfo.CreateNoWindow = true;
                gpgEncryptorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                gpgEncryptorProcess.StartInfo.UseShellExecute = false;
                gpgEncryptorProcess.StartInfo.RedirectStandardOutput = true;
                gpgEncryptorProcess.StartInfo.RedirectStandardError = true;


                gpgEncryptorProcess.Start();
                _msg.Length = 0;
                _msg.Append(gpgEncryptorProcess.StandardOutput.ReadToEnd());
                _msg.Append(" ");
                _msg.Append(gpgEncryptorProcess.StandardError.ReadToEnd());
                _processMessages = _msg.ToString();
                gpgEncryptorProcess.WaitForExit();
                _processExitCode = gpgEncryptorProcess.ExitCode;

                if (this.ProcessExitCode != 0)
                {
                    result = FileEncryptorResult.EncryptionFailed;
                }
                else
                {
                    result = FileEncryptorResult.EncryptedFileCreated;
                }


            }
            catch (System.Exception ex)
            {
                result = FileEncryptorResult.EncryptionFailed;
                _msg.Length = 0;
                _msg.Append("Error in RunGPGEncryptor method.\r\n");
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                if (this.ProcessCommandLine.Length > 0)
                {
                    _msg.Append("\r\nCommand line: ");
                    _msg.Append(this.ProcessCommandLine);
                }
                if (this.ProcessMessages.Length > 0)
                {
                    _msg.Append("\r\nProcess messages: ");
                    _msg.Append(this.ProcessMessages);
                }
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }

            return result;
        }

        private String BuildEncryptionArguments()
        {
            StringBuilder arguments = new StringBuilder();

            arguments.Length = 0;
            //arguments.Append("\"");
            //arguments.Append(_sGPGAppPath);
            //arguments.Append("\"");
            //arguments.Append(" ");
            if (_gpgHomeDirOption != string.Empty)
            {
                arguments.Append("--homedir ");
                arguments.Append("\"");
                arguments.Append(_gpgHomeDirOption);
                arguments.Append("\"");
                arguments.Append(" ");
            }
            if (this.EncryptionKey != string.Empty)
            {
                arguments.Append("-r ");
                arguments.Append("\"");
                arguments.Append(this.EncryptionKey);
                arguments.Append("\"");
                arguments.Append(" ");
            }
            if (this._asciiArmor)
            {
                arguments.Append(" -a --batch ");
            }
            else
            {
                arguments.Append(" --batch ");
            }
            if (this.Destination != string.Empty)
            {
                arguments.Append("--output ");
                arguments.Append("\"");
                arguments.Append(this.Destination);
                arguments.Append("\"");
                arguments.Append(" ");
            }
            if (this.Source != string.Empty)
            {
                arguments.Append("--encrypt ");
                arguments.Append("\"");
                arguments.Append(this.Source);
                arguments.Append("\"");
                arguments.Append(" ");
            }



            return arguments.ToString();

        }

        private string GetCommandLine(string arguments)
        {
            StringBuilder commandLine = new StringBuilder();

            commandLine.Length = 0;
            commandLine.Append(_gpgAppPath);
            commandLine.Append(" ");
            commandLine.Append(arguments);

            return commandLine.ToString();
        }


        //class helpers

        /// <summary>
        /// Outputs the values of all the public properties.
        /// </summary>
        /// <returns>String containing the values of the public properties.</returns>
        public override string ToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties();

            data.Append("Class type:");
            data.Append(t.FullName);
            data.Append("  ");
            int inx = 0;
            int maxInx = props.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                PropertyInfo prop = props[inx];
                object val = prop.GetValue(this, null);
                data.Append(prop.Name);
                data.Append(": ");
                if (val != null)
                    data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (prop.Name == "ExpectedEncryptionFileExtensions")
                {
                    for (int i = 0; i < this.ExpectedEncryptionFileExtensions.Length; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(this.ExpectedEncryptionFileExtensions[i].ToString());
                        data.Append("  ");
                    }
                }

            }

            return data.ToString();
        }


    }//end class
}//end namespace
