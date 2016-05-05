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
    /// Class contains functionality for decrypting PGP files using the GPG command line utility.
    /// See GPGEncryptor class for functionality to encrypt PGP files using the GPG command line utility.
    /// </summary>

    public class GPGDecryptor
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for decryption routines.
        private string _gpgEncryptionFileExtension = "PGP";
        private string[] _expectedEncryptionFileExtensions;
        private string _gpgAppPath = string.Empty;
        private string _gpgWorkingFolder = string.Empty;
        private string _gpgHomeDirOption = string.Empty;
        private string _windowsCmdExePath = string.Empty;

        private string _source = string.Empty;
        private string _destination = string.Empty;
        private string _decryptionPassphrase = string.Empty;
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
        public enum FileDecryptorResult
        {
            FileStatusUnknown = 0,
            FileAlreadyDecrypted = 1,
            DecryptedFileCreated = 2,
            DecryptionNotRequired = 3,
            FileNotClosed = 4,
            DecryptionFailed = 999
        }
#pragma warning restore 1591


        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public GPGDecryptor()
        {
            InitializeFileDecryptor();
        }

        private void InitializeFileDecryptor()
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
        /// File extension to use for encrypted file.
        /// </summary>
        public string GpgEncryptionFileExtension
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
        /// Comma delimited list of file extensions that can be used by encrypted input files.
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
        /// Path to the cmd.exe program.
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
        /// Path to file that will be decrypted.
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
        /// Path to decrypted file.
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
        /// Command line used to launch the decryption process.
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
        /// Messages from the process that ran the decryption.
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
        /// Password to use for decryption. Can be directly specified or contained in a text file.
        /// </summary>
        public string DecryptionPassphrase
        {
            get 
            { 
                return _decryptionPassphrase;
            }
            set 
            { 
                _decryptionPassphrase = value; 
            }
        }


        //methods

        /// <summary>
        /// Method to encrypt a file using PGP via the GPG command line.
        /// </summary>
        /// <param name="sourcePath">Path to encrypted file.</param>
        /// <param name="destinationPath">Path to the decrypted file.</param>
        /// <param name="decryptionPassphrase">Password to be used to decrypt the file. Can be the password itself or a path to a file containing the password.</param>
        /// <returns></returns>
        public FileDecryptorResult CreateDecryptedFile(string sourcePath,
                                               string destinationPath,
                                               string decryptionPassphrase)
        {
            FileDecryptorResult result = FileDecryptorResult.FileStatusUnknown;


            if (String.IsNullOrEmpty(sourcePath))
                throw new ArgumentNullException("The name of file that needs to be decrypted cannot be null or empty.");
            if (String.IsNullOrEmpty(destinationPath))
                throw new ArgumentNullException("The name of the destination file for the decrypted data cannot be null or empty.");

            result = RunGPGDecryptor(sourcePath, destinationPath, decryptionPassphrase);


            return result;

        }

        /// <summary>
        /// Determines whether or not the file extension for the specified file identifies the file as having been encrypted with PGP.
        /// </summary>
        /// <param name="sourcePath">Path to file.</param>
        /// <returns>True if file extension is one of the accepted PGP file extensions.</returns>
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

        private FileDecryptorResult RunGPGDecryptor(string sourcePath,
                                                    string destinationPath,
                                                    string decryptionPassphrase)
        {
            FileDecryptorResult result = FileDecryptorResult.FileStatusUnknown;
            Process gpgDecryptorProcess = new Process();

            try
            {
                _processExitCode = -1;
                _processMessages = string.Empty;
                this.Source = sourcePath;
                this.Destination = destinationPath;
                this.DecryptionPassphrase = decryptionPassphrase;

                gpgDecryptorProcess.StartInfo.WorkingDirectory = _gpgWorkingFolder;
                //oGPGDecryptorProcess.StartInfo.FileName = _sGPGAppPath;
                gpgDecryptorProcess.StartInfo.FileName = _windowsCmdExePath;
                gpgDecryptorProcess.StartInfo.Arguments = BuildDecryptionArguments();
                this.ProcessCommandLine = GetCommandLine(gpgDecryptorProcess.StartInfo.Arguments);
                gpgDecryptorProcess.StartInfo.CreateNoWindow = true;
                gpgDecryptorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                gpgDecryptorProcess.StartInfo.UseShellExecute = false;
                gpgDecryptorProcess.StartInfo.RedirectStandardOutput = true;
                gpgDecryptorProcess.StartInfo.RedirectStandardError = true;


                gpgDecryptorProcess.Start();
                _msg.Length = 0;
                _msg.Append(gpgDecryptorProcess.StandardOutput.ReadToEnd());
                _msg.Append(" ");
                _msg.Append(gpgDecryptorProcess.StandardError.ReadToEnd());
                _processMessages = _msg.ToString();
                gpgDecryptorProcess.WaitForExit();
                _processExitCode = gpgDecryptorProcess.ExitCode;

                if (this.ProcessExitCode != 0)
                {
                    result = FileDecryptorResult.DecryptionFailed;
                }
                else
                {
                    result = FileDecryptorResult.DecryptedFileCreated;
                }


            }
            catch (System.Exception ex)
            {
                result = FileDecryptorResult.DecryptionFailed;
                _msg.Length = 0;
                _msg.Append("Error in RunGPGDecryptor method.\r\n");
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

        private String BuildDecryptionArguments()
        {
            StringBuilder arguments = new StringBuilder();

            arguments.Length = 0;

            arguments.Append("/c ");

            if (File.Exists(this.DecryptionPassphrase))
            {
                //passphrase is contained in a file
                arguments.Append("type ");
                arguments.Append(this.DecryptionPassphrase);
                arguments.Append("| ");
            }
            else
            {
                //assume a password was directly specified in the config file
                arguments.Append("@echo ");
                arguments.Append(this.DecryptionPassphrase);
                arguments.Append("| ");
            }


            arguments.Append("\"");
            arguments.Append(_gpgAppPath);
            arguments.Append("\"");
            arguments.Append(" ");
            if (_gpgHomeDirOption != string.Empty)
            {
                arguments.Append("--homedir ");
                arguments.Append("\"");
                arguments.Append(_gpgHomeDirOption);
                arguments.Append("\"");
                arguments.Append(" ");
            }
            if (this.DecryptionPassphrase != string.Empty)
            {
                arguments.Append("--passphrase-fd 0 ");
                //sArguments.Append("\"");
                //sArguments.Append(this.DecryptionPassphrase);
                //sArguments.Append("\"");
                //sArguments.Append(" ");
            }
            arguments.Append(" --batch ");
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
                arguments.Append("--decrypt ");
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
            commandLine.Append(_windowsCmdExePath);
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
