//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;
using System.Net;
using System.Threading;
using System.Configuration;
using AppGlobals;

namespace PFFileTransferObjects
{
    /// <summary>
    /// Class encapsulates FTP processing using the .NET FTP classes.
    /// </summary>
    public class PFWinFTP
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _errMsg = new StringBuilder();

        private string _uri = string.Empty;


        //private variables for properties
        private string _sourceFile = string.Empty;
        private string _remoteFile = string.Empty;
        private string _ftpHost = string.Empty;
        private string _ftpUsername = string.Empty;
        private string _ftpPassword = string.Empty;
        private bool _ftpUseSSL = false;
        private int _ftpBufferSize = 2048;
        private bool _ftpUseBinaryMode = true;
        private string _localDestinationFile = string.Empty;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFWinFTP()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ftpHost">FTP server name or address.</param>
        public PFWinFTP(string ftpHost)
        {
            this.FtpHost = ftpHost;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ftpHost">FTP server name or address.</param>
        /// <param name="ftpUsername">Logon username for the FTP server.</param>
        /// <param name="ftpPassword">Logon password for the FTP server.</param>
        public PFWinFTP(string ftpHost, string ftpUsername, string ftpPassword)
        {
            this.FtpHost = ftpHost;
            this.FtpUsername = ftpUsername;
            this.FtpPassword = ftpPassword;
        }



        //properties

        /// <summary>
        /// File to be copied.
        /// </summary>
        public string SourceFile
        {
            get
            {
                return _sourceFile;
            }
            set
            {
                _sourceFile = value;
            }
        }

        /// <summary>
        /// Name of file at remote FTP site.
        /// </summary>
        public string RemoteFile
        {
            get
            {
                return _remoteFile;
            }
            set
            {
                _remoteFile = value;
            }
        }

        /// <summary>
        /// Server name or IP address.
        /// </summary>
        public string FtpHost
        {
            get
            {
                return _ftpHost;
            }
            set
            {
                _ftpHost = value;
            }
        }

        /// <summary>
        /// Logon username for FTP server.
        /// </summary>
        public string FtpUsername
        {
            get
            {
                return _ftpUsername;
            }
            set
            {
                _ftpUsername = value;
            }
        }

        /// <summary>
        /// Logon password for FTP server.
        /// </summary>
        public string FtpPassword
        {
            get
            {
                return _ftpPassword;
            }
            set
            {
                _ftpPassword = value;
            }
        }

        /// <summary>
        /// Specifies whether or not to use a Secure Sockets Layer (SSL) connection.
        /// </summary>
        public bool FTP_UseSSL
        {
            get
            {
                return _ftpUseSSL;
            }
            set
            {
                _ftpUseSSL = value;
            }
        }

        /// <summary>
        /// Size of data buffer process will use to transfer data.
        /// </summary>
        public int FtpBufferSize
        {
            get
            {
                return _ftpBufferSize;
            }
            set
            {
                _ftpBufferSize = value;
            }
        }

        /// <summary>
        /// Specifies whether or not to use binary mode when transferring a file.
        /// </summary>
        public bool FTP_UseBinaryMode
        {
            get
            {
                return _ftpUseBinaryMode;
            }
            set
            {
                _ftpUseBinaryMode = value;
            }
        }

        /// <summary>
        /// Name of local file when downloading from FTP site.
        /// </summary>
        public string LocalDestinationFile
        {
            get
            {
                return _localDestinationFile;
            }
            set
            {
                _localDestinationFile = value;
            }
        }



        //methods

        /// <summary>
        /// Uploads file to FTP server.
        /// </summary>
        /// <returns>True is file transfer is successful.</returns>
        /// <remarks> All information needed for the transfer is specified via property values for this instance of the class.</remarks>
        public bool UploadFileToFtpHost()
        {

            bool ftpSucceeded = false;

            ftpSucceeded = UploadFileToFtpHost(_sourceFile,
                                              _ftpHost,
                                              _remoteFile,
                                              _ftpUsername,
                                              _ftpPassword,
                                              _ftpBufferSize,
                                              _ftpUseBinaryMode);

            return ftpSucceeded;

        }

        /// <summary>
        /// Uploads file to FTP server.
        /// </summary>
        /// <param name="sourceFile">File to copy.</param>
        /// <param name="ftpHost">FTP server.</param>
        /// <param name="ftpRemoteFile">Name of destination file on FTP server.</param>
        /// <param name="ftpUsername">FTP server logon username.</param>
        /// <param name="ftpPassword">FTP server logon password.</param>
        /// <returns>True is file transfer is successful.</returns>
        public bool UploadFileToFtpHost(string sourceFile,
                                        string ftpHost,
                                        string ftpRemoteFile,
                                        string ftpUsername,
                                        string ftpPassword)
        {

            bool ftpSucceeded = false;

            ftpSucceeded = UploadFileToFtpHost(sourceFile,
                                                ftpHost,
                                                ftpRemoteFile,
                                                ftpUsername,
                                                ftpPassword,
                                                _ftpBufferSize,
                                                _ftpUseBinaryMode);

            return ftpSucceeded;

        }

        /// <summary>
        /// Uploads file to FTP server.
        /// </summary>
        /// <param name="sourceFile">File to copy.</param>
        /// <param name="ftpHost">FTP server.</param>
        /// <param name="ftpRemoteFile">Name of destination file on FTP server.</param>
        /// <param name="ftpUsername">FTP server logon username.</param>
        /// <param name="ftpPassword">FTP server logon password.</param>
        /// <param name="ftpBufferSize">Specifies size of data buffer for the file transfer process.</param>
        /// <param name="useBinaryMode">Specifies whether or not to use binary mode when transferring the file.</param>
        /// <returns>True is file transfer is successful.</returns>
        public bool UploadFileToFtpHost(string sourceFile,
                                        string ftpHost,
                                        string ftpRemoteFile,
                                        string ftpUsername,
                                        string ftpPassword,
                                        int ftpBufferSize,
                                        bool useBinaryMode)
        {

            bool ftpSucceeded = false;
            string uri = ftpHost + ftpRemoteFile;

            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(uri);
            FileInfo fileInfo = null;
            long fileSize = -1;
            int bufferLength = ftpBufferSize;
            byte[] buffer = new byte[bufferLength];
            int contentLength;

            FileStream fs = null;

            try
            {
                fileInfo = new FileInfo(sourceFile);
                fileSize = fileInfo.Length;

                ftp.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                ftp.KeepAlive = false;
                ftp.UseBinary = this._ftpUseBinaryMode;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;
                ftp.Proxy = null;
                ftp.EnableSsl = this.FTP_UseSSL;
                ftp.ContentLength = fileSize;

                //code to turn off ssl certificate checking
                if (ftp.EnableSsl == true)
                    ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertificates;
                else
                    ServicePointManager.ServerCertificateValidationCallback = null;
                //end code to turn off ssl certificate checking

                fs = fileInfo.OpenRead();
                Stream strm = ftp.GetRequestStream();
                try
                {
                    contentLength = fs.Read(buffer, 0, bufferLength);
                    while (contentLength != 0)
                    {
                        //write contrent from the file stream to the FTP upload stream
                        strm.Write(buffer, 0, contentLength);
                        contentLength = fs.Read(buffer, 0, bufferLength);
                    }
                    ftpSucceeded = true;
                }
                catch (IOException ioex)
                {
                    throw ioex;
                }
                catch (Exception ex)
                {
                    _errMsg.Length = 0;
                    _errMsg.Append("Error in UploadFileToFtpHost method.\r\n");
                    _errMsg.Append(AppMessages.FormatErrorMessage(ex));
                    throw new System.Exception(_errMsg.ToString());
                }
                finally
                {
                    strm.Close();
                }
            }
            catch (IOException ioex)
            {
                throw ioex;
            }
            catch (Exception ex)
            {
                _errMsg.Length = 0;
                _errMsg.Append("Error in UploadFileToFtpHost method.\r\n");
                _errMsg.Append(AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_errMsg.ToString());
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }



            return ftpSucceeded;

        }

        /// <summary>
        /// Routine to turn off ssl certificate checking.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certification"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns>Always returns true.</returns>
        /// <remarks>This is a callback routine.
        /// </remarks>
        /// <example>
        /// <code language="C#">
        ///  if(ftp.EnableSsl == true)
        ///     ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertificates;
        ///  else
        ///     ServicePointManager.ServerCertificateValidationCallback = null;
        /// </code>
        /// </example>
        public bool AcceptAllCertificates(object sender,
                                          System.Security.Cryptography.X509Certificates.X509Certificate certification,
                                          System.Security.Cryptography.X509Certificates.X509Chain chain,
                                          System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        //end code to turn off ssl certificate checking

        /// <summary>
        /// Renames file on FTP server.
        /// </summary>
        /// <param name="ftpRemoteFile">Name of file on FTP server.</param>
        /// <param name="ftpRenameTo">New name for the file.</param>
        /// <returns>True if rename succeeds.</returns>
        /// <remarks>FTP server name and logon information must be specified via the corresponding properties.</remarks>
        public bool RenameFileOnFtpHost(string ftpRemoteFile,
                                        string ftpRenameTo)
        {
            bool ftpSucceeded = false;

            ftpSucceeded = RenameFileOnFtpHost(this.FtpHost, 
                                               ftpRemoteFile,
                                               ftpRenameTo,
                                               this.FtpUsername,
                                               this.FtpPassword);

            return ftpSucceeded;

        }


        /// <summary>
        /// Routine to rename a file on the FTP server.
        /// </summary>
        /// <param name="ftpHost">FTP Server name or IP address.</param>
        /// <param name="ftpRemoteFile">Original file name.</param>
        /// <param name="ftpRenameTo">New file name.</param>
        /// <param name="ftpUsername">FTP server logon username.</param>
        /// <param name="ftpPassword">FTP server logon password.</param>
        /// <returns>True if success.</returns>
        public bool RenameFileOnFtpHost(string ftpHost,
                                        string ftpRemoteFile,
                                        string ftpRenameTo,
                                        string ftpUsername,
                                        string ftpPassword)
        {
            bool ftpSucceeded = false;
            string remoteFile = ftpRemoteFile;
            string renameTo = ftpRenameTo;
            string host = ftpHost;
            string username = ftpUsername;
            string password = ftpPassword;
            string uri = host + remoteFile;
            //string renamedUri = host + ftpRenameTo;
            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(uri);
            FtpWebResponse ftpResponse = null;

            try
            {
                ftp.Credentials = new NetworkCredential(username, password);
                ftp.KeepAlive = false;
                ftp.Proxy = null;
                ftp.Method = WebRequestMethods.Ftp.Rename;
                ftp.RenameTo = ftpRenameTo;
                //ftp.RenameTo = renamedUri;
                if (this.RemoteFileExists(ftpRenameTo))
                {
                    _msg.Length = 0;
                    _msg.Append("File ");
                    _msg.Append(ftpRenameTo);
                    _msg.Append(" already exists on ");
                    _msg.Append(ftpHost);
                    _msg.Append(". Rename operation fails.");
                    throw new System.Exception(_msg.ToString());
                }
                ftpResponse = (FtpWebResponse)ftp.GetResponse();
                ftpResponse.Close();

                ftpSucceeded = true;
            }
            catch (Exception ex)
            {
                _errMsg.Length = 0;
                _errMsg.Append("Error in RenameFileOnFtpHost method. Make sure Rename To file name does not already exist.");
                _errMsg.Append(" File name: ");
                _errMsg.Append(uri);
                _errMsg.Append(" Rename To: ");
                _errMsg.Append(ftpRenameTo);
                _errMsg.Append("\r\n");
                _errMsg.Append(AppMessages.FormatErrorMessageWithStackTrace(ex));
                throw new System.Exception(_errMsg.ToString());
            }

            return ftpSucceeded;

        }

        /// <summary>
        /// Determines whether or not file exists on FTP server.
        /// </summary>
        /// <param name="ftpRemoteFile">Name of the file on the FTP server.</param>
        /// <returns>True if file exists.</returns>
        /// <remarks>FTP server name and logon information must be specified via the corresponding properties.</remarks>
        public bool RemoteFileExists(string ftpRemoteFile)
        {
            bool bFileExists = RemoteFileExists(this.FtpHost,
                                                ftpRemoteFile,
                                                this.FtpUsername,
                                                this.FtpPassword);
            return bFileExists;
        }


        /// <summary>
        /// Routine to check if a file already exists on the FTP server.
        /// </summary>
        /// <param name="ftpHost">FTP server.</param>
        /// <param name="ftpRemoteFile">File name on FTP server.</param>
        /// <param name="ftpUsername">FTP server logon username.</param>
        /// <param name="ftpPassword">FTP server logon password.</param>
        /// <returns>True if file exists.</returns>
        public bool RemoteFileExists(string ftpHost,
                                     string ftpRemoteFile,
                                     string ftpUsername,
                                     string ftpPassword)
        {
            bool fileExists = false;
            string remoteFile = ftpRemoteFile;
            string host = ftpHost;
            string username = ftpUsername;
            string password = ftpPassword;
            string uri = host + remoteFile;
            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(uri);
            FtpWebResponse ftpResponse = null;
            StreamReader sr = null;
            string response = string.Empty;


            try
            {
                ftp.Credentials = new NetworkCredential(username, password);
                ftp.KeepAlive = false;
                ftp.Proxy = null;
                ftp.Method = WebRequestMethods.Ftp.ListDirectory;

                ftpResponse = (FtpWebResponse)ftp.GetResponse();
                sr = new StreamReader(ftpResponse.GetResponseStream());
                response = sr.ReadToEnd();
                if (response.Length > 0)
                    fileExists = true;
                else
                    fileExists = false;
                ftpResponse.Close();

            }
            catch (WebException wex)
            {
                _errMsg.Length = 0;
                _errMsg.Append(AppMessages.FormatErrorMessage(wex));
                fileExists = false;
            }
            catch (Exception ex)
            {
                fileExists = false;
                _errMsg.Length = 0;
                _errMsg.Append("Error in RemoteFileExists method.");
                _errMsg.Append(" File name: ");
                _errMsg.Append(uri);
                _errMsg.Append("\r\n");
                _errMsg.Append(AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_errMsg.ToString());
            }

            return fileExists;

        }

        /// <summary>
        /// Deletes file on FTP server.
        /// </summary>
        /// <param name="ftpRemoteFile">Name of file to be deleted.</param>
        /// <returns>True if delete succeeds.</returns>
        /// <remarks>FTP server name and logon information must be specified via the corresponding properties.</remarks>
        public bool DeleteRemoteFile(string ftpRemoteFile)
        {
            bool bFileDeleted = DeleteRemoteFile(this.FtpHost, 
                                                 ftpRemoteFile,
                                                 this.FtpUsername,
                                                 this.FtpPassword);
            return bFileDeleted;
        }

        /// <summary>
        /// Deletes file on FTP server.
        /// </summary>
        /// <param name="ftpHost">FTP server.</param>
        /// <param name="ftpRemoteFile">File name on FTP server.</param>
        /// <param name="ftpUsername">FTP server logon username.</param>
        /// <param name="ftpPassword">FTP server logon password.</param>
        /// <returns>True if file deleted.</returns>
        public bool DeleteRemoteFile(string ftpHost,
                                     string ftpRemoteFile,
                                     string ftpUsername,
                                     string ftpPassword)
        {
            bool fileDeleted = false;
            string remoteFile = ftpRemoteFile;
            string host = ftpHost;
            string username = ftpUsername;
            string password = ftpPassword;
            string uri = host + remoteFile;
            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(uri);
            FtpWebResponse ftpResponse = null;


            try
            {
                ftp.Credentials = new NetworkCredential(username, password);
                ftp.KeepAlive = false;
                ftp.Proxy = null;
                ftp.Method = WebRequestMethods.Ftp.DeleteFile;

                ftpResponse = (FtpWebResponse)ftp.GetResponse();
                ftpResponse.Close();
                fileDeleted = true;

            }
            catch (Exception ex)
            {
                fileDeleted = false;
                _errMsg.Length = 0;
                _errMsg.Append("Error in DeleteRemoteFile method. Make sure file exists. ");
                _errMsg.Append(" File name: ");
                _errMsg.Append(uri);
                _errMsg.Append("\r\n");
                _errMsg.Append(AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_errMsg.ToString());
            }

            return fileDeleted;

        }

        /// <summary>
        /// Gets list of files currently stored in specified folder on FTP server.
        /// </summary>
        /// <param name="ftpRemoteFolder">Name of folder on FTP server.</param>
        /// <returns>List containing file namess.</returns>
        /// <remarks>FTP server name and logon information must be specified via the corresponding properties.</remarks>
        public List<string> ListRemoteDirectoryFiles(string ftpRemoteFolder)
        {
            List<string> remoteFiles = ListRemoteDirectoryFiles(this.FtpHost, 
                                                                ftpRemoteFolder,
                                                                this.FtpUsername,
                                                                this.FtpPassword);
            return remoteFiles;

        }

        /// <summary>
        /// Routine to get a list of all files on FTP server.
        /// </summary>
        /// <param name="ftpHost">FTP server.</param>
        /// <param name="ftpRemoteFolder">Folder name on FTP server.</param>
        /// <param name="ftpUsername">FTP server logon username.</param>
        /// <param name="ftpPassword">FTP server logon password.</param>
        /// <returns>List containing the file names.</returns>
        public List<string> ListRemoteDirectoryFiles(string ftpHost,
                                               string ftpRemoteFolder,
                                               string ftpUsername,
                                               string ftpPassword)
        {
            string remoteFolder = ftpRemoteFolder;
            string host = ftpHost;
            string username = ftpUsername;
            string password = ftpPassword;
            string uri = host + remoteFolder;
            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(uri);
            FtpWebResponse ftpResponse = null;
            StreamReader sr = null;
            string response = string.Empty;
            List<string> remoteFiles = new List<string>();
            string remoteFile = string.Empty;


            try
            {
                ftp.Credentials = new NetworkCredential(username, password);
                ftp.KeepAlive = false;
                ftp.Proxy = null;
                ftp.Method = WebRequestMethods.Ftp.ListDirectory;

                ftpResponse = (FtpWebResponse)ftp.GetResponse();
                sr = new StreamReader(ftpResponse.GetResponseStream());
                remoteFile = sr.ReadLine();
                while (remoteFile != null)
                {
                    remoteFiles.Add(remoteFile);
                    remoteFile = sr.ReadLine();
                }
                ftpResponse.Close();

            }
            catch (WebException wex)
            {
                //ignore the web exception: most likely caused by remote folder not existing

                _errMsg.Length = 0;
                _errMsg.Append(AppMessages.FormatErrorMessage(wex));
            }
            catch (Exception ex)
            {
                _errMsg.Length = 0;
                _errMsg.Append("Error in RemoteFileExists method.");
                _errMsg.Append(" File name: ");
                _errMsg.Append(uri);
                _errMsg.Append("\r\n");
                _errMsg.Append(AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_errMsg.ToString());
            }

            return remoteFiles;

        }

        /// <summary>
        /// Downloads file from FTP server.
        /// </summary>
        /// <returns>True if download successful.</returns>
        /// <remarks> All information needed for the transfer is specified via property values for this instance of the class.</remarks>
        public bool DownloadFileFromFtpHost()
        {

            bool ftpSucceeded = false;

            ftpSucceeded = DownloadFileFromFtpHost(_localDestinationFile,
                                                    _ftpHost,
                                                    _remoteFile,
                                                    _ftpUsername,
                                                    _ftpPassword,
                                                    _ftpBufferSize,
                                                    _ftpUseBinaryMode);

            return ftpSucceeded;

        }

        /// <summary>
        /// Downloads file from FTP server.
        /// </summary>
        /// <param name="destinationFile">Name of the file after copy to local destination.</param>
        /// <param name="ftpHost">FTP server.</param>
        /// <param name="ftpRemoteFile">File name on FTP server.</param>
        /// <param name="ftpUsername">FTP server logon username.</param>
        /// <param name="ftpPassword">FTP server logon password.</param>
        /// <returns>True if download successful.</returns>
        public bool DownloadFileFromFtpHost(string destinationFile,
                                                   string ftpHost,
                                                   string ftpRemoteFile,
                                                   string ftpUsername,
                                                   string ftpPassword)
        {

            bool ftpSucceeded = false;

            ftpSucceeded = UploadFileToFtpHost(destinationFile,
                                                ftpHost,
                                                ftpRemoteFile,
                                                ftpUsername,
                                                ftpPassword,
                                                _ftpBufferSize,
                                                _ftpUseBinaryMode);

            return ftpSucceeded;

        }

        /// <summary>
        /// Downloads file from FTP server
        /// </summary>
        /// <param name="ftpDestinationFile">Name of destination file.</param>
        /// <param name="ftpHost">FTP server.</param>
        /// <param name="ftpRemoteFile">Name of destination file on FTP server.</param>
        /// <param name="ftpUsername">FTP server logon username.</param>
        /// <param name="ftpPassword">FTP server logon password.</param>
        /// <param name="ftpBufferSize">Specifies size of data buffer for the file transfer process.</param>
        /// <param name="ftpUseBinaryMode">Specifies whether or not to use binary mode when transferring the file.</param>
        /// <returns>True is file transfer is successful.</returns>
        public bool DownloadFileFromFtpHost(string ftpDestinationFile,
                                            string ftpHost,
                                            string ftpRemoteFile,
                                            string ftpUsername,
                                            string ftpPassword,
                                            int ftpBufferSize,
                                            bool ftpUseBinaryMode)
        {

            bool ftpSucceeded = false;
            string uri = ftpHost + ftpRemoteFile;

            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(uri);
            FtpWebResponse ftpResponse = null;
            BinaryReader sr = null;
            int bufferLength = ftpBufferSize;
            byte[] buffer = new byte[bufferLength];
            int contentLength;

            FileStream fs = null;

            try
            {

                ftp.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                ftp.KeepAlive = false;
                ftp.UseBinary = this._ftpUseBinaryMode;
                ftp.Method = WebRequestMethods.Ftp.DownloadFile;
                ftp.Proxy = null;
                ftp.EnableSsl = this.FTP_UseSSL;

                fs = new FileStream(ftpDestinationFile, FileMode.Create);
                ftpResponse = (FtpWebResponse)ftp.GetResponse();
                sr = new BinaryReader(ftpResponse.GetResponseStream());

                try
                {
                    contentLength = sr.Read(buffer, (int)0, bufferLength);
                    while (contentLength != 0)
                    {
                        //write contrent from the download stream to the file output stream
                        fs.Write(buffer, 0, contentLength);
                        contentLength = sr.Read(buffer, (int)0, bufferLength);
                    }
                    ftpSucceeded = true;
                }
                catch (IOException ioex)
                {
                    throw ioex;
                }
                catch (Exception ex)
                {
                    _errMsg.Length = 0;
                    _errMsg.Append("Error in DownloadFileFromFtpHost method.\r\n");
                    _errMsg.Append(AppMessages.FormatErrorMessage(ex));
                    throw new System.Exception(_errMsg.ToString());
                }
                finally
                {
                    sr.Close();
                }
            }
            catch (IOException ioex)
            {
                throw ioex;
            }
            catch (Exception ex)
            {
                _errMsg.Length = 0;
                _errMsg.Append("Error in DownloadFileFromFtpHost method.\r\n");
                _errMsg.Append(AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_errMsg.ToString());
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }



            return ftpSucceeded;

        }

        /// <summary>
        /// Gets the size of a file on the FTP server.
        /// </summary>
        /// <param name="ftpHost">FTP server.</param>
        /// <param name="ftpRemoteFile">Name of destination file on FTP server.</param>
        /// <param name="ftpUsername">FTP server logon username.</param>
        /// <param name="ftpPassword">FTP server logon password.</param>
        /// <returns>The file size as a long.</returns>
        public long RemoteFileSize(string ftpHost,
                                   string ftpRemoteFile,
                                   string ftpUsername,
                                   string ftpPassword)
        {
            long fileSize = -1;
            string remoteFile = ftpRemoteFile;
            string host = ftpHost;
            string username = ftpUsername;
            string password = ftpPassword;
            string uri = host + remoteFile;
            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(uri);
            FtpWebResponse ftpResponse = null;


            try
            {
                if (this.RemoteFileExists(ftpHost, ftpRemoteFile, ftpUsername, ftpPassword) == true)
                {
                    ftp.Credentials = new NetworkCredential(username, password);
                    ftp.KeepAlive = false;
                    ftp.Proxy = null;
                    ftp.Method = WebRequestMethods.Ftp.GetFileSize;

                    ftpResponse = (FtpWebResponse)ftp.GetResponse();
                    fileSize = ftpResponse.ContentLength;
                    ftpResponse.Close();
                }
                else
                {
                    fileSize = -1;
                }

            }
            catch (Exception ex)
            {
                fileSize = -1;
                _errMsg.Length = 0;
                _errMsg.Append("Error in RemoteFileSize method.");
                _errMsg.Append(" File name: ");
                _errMsg.Append(uri);
                _errMsg.Append("\r\n");
                _errMsg.Append(AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_errMsg.ToString());
            }

            return fileSize;

        }

        /// <summary>
        /// Get date/time stamp for a file on the FTP server.
        /// </summary>
        /// <param name="ftpHost">FTP server.</param>
        /// <param name="ftpRemoteFile">Name of destination file on FTP server.</param>
        /// <param name="ftpUsername">FTP server logon username.</param>
        /// <param name="ftpPassword">FTP server logon password.</param>
        /// <returns>DateTime value for the file.</returns>
        public DateTime RemoteFileDateTimestamp(string ftpHost,
                                                string ftpRemoteFile,
                                                string ftpUsername,
                                                string ftpPassword)
        {
            DateTime dateTimestamp = DateTime.MinValue;
            string remoteFile = ftpRemoteFile;
            string host = ftpHost;
            string username = ftpUsername;
            string password = ftpPassword;
            string uri = host + remoteFile;
            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(uri);
            FtpWebResponse ftpResponse = null;


            try
            {
                if (this.RemoteFileExists(ftpHost, ftpRemoteFile, ftpUsername, ftpPassword) == true)
                {
                    ftp.Credentials = new NetworkCredential(username, password);
                    ftp.KeepAlive = false;
                    ftp.Proxy = null;
                    ftp.Method = WebRequestMethods.Ftp.GetDateTimestamp;

                    ftpResponse = (FtpWebResponse)ftp.GetResponse();
                    dateTimestamp = ftpResponse.LastModified;
                    ftpResponse.Close();
                }
                else
                {
                    dateTimestamp = DateTime.MinValue;
                }

            }
            catch (Exception ex)
            {
                dateTimestamp = DateTime.MinValue;
                _errMsg.Length = 0;
                _errMsg.Append("Error in RemoteFileSize method.");
                _errMsg.Append(" File name: ");
                _errMsg.Append(uri);
                _errMsg.Append("\r\n");
                _errMsg.Append(AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_errMsg.ToString());
            }

            return dateTimestamp;

        }


        


        //class helpers

        /// <summary>
        /// Saves the public property values contained in the current instance. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFWinFTP));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFWinFTP.</returns>
        public static PFWinFTP LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFWinFTP));
            TextReader textReader = new StreamReader(filePath);
            PFWinFTP columnDefinitions;
            columnDefinitions = (PFWinFTP)deserializer.Deserialize(textReader);
            textReader.Close();
            return columnDefinitions;
        }


        /// <summary>
        /// Routine overrides default ToString method and outputs name, type, scope and value for all class properties and fields.
        /// </summary>
        /// <returns>String containing values.</returns>
        public override string ToString()
        {
            StringBuilder data = new StringBuilder();
            data.Append(PropertiesToString());
            data.Append("\r\n");
            data.Append(FieldsToString());
            data.Append("\r\n");


            return data.ToString();
        }


        /// <summary>
        /// Routine outputs name and value for all properties.
        /// </summary>
        /// <returns>String containing names and values.</returns>
        public string PropertiesToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            data.Append("Class type:");
            data.Append(t.FullName);
            data.Append("\r\nClass properties for");
            data.Append(t.FullName);
            data.Append("\r\n");


            int inx = 0;
            int maxInx = props.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                PropertyInfo prop = props[inx];
                object val = prop.GetValue(this, null);

                /*
                //****************************************************************************************
                //use the following code if you class has an indexer or is derived from an indexed class
                //****************************************************************************************
                object val = null;
                StringBuilder temp = new StringBuilder();
                if (prop.GetIndexParameters().Length == 0)
                {
                    val = prop.GetValue(this, null);
                }
                else if (prop.GetIndexParameters().Length == 1)
                {
                    temp.Length = 0;
                    for (int i = 0; i < this.Count; i++)
                    {
                        temp.Append("Index ");
                        temp.Append(i.ToString());
                        temp.Append(" = ");
                        temp.Append(val = prop.GetValue(this, new object[] { i }));
                        temp.Append("  ");
                    }
                    val = temp.ToString();
                }
                else
                {
                    //this is an indexed property
                    temp.Length = 0;
                    temp.Append("Num indexes for property: ");
                    temp.Append(prop.GetIndexParameters().Length.ToString());
                    temp.Append("  ");
                    val = temp.ToString();
                }
                //****************************************************************************************
                // end code for indexed property
                //****************************************************************************************
                */

                if (prop.GetGetMethod(true) != null)
                {
                    data.Append(" <");
                    if (prop.GetGetMethod(true).IsPublic)
                        data.Append(" public ");
                    if (prop.GetGetMethod(true).IsPrivate)
                        data.Append(" private ");
                    if (!prop.GetGetMethod(true).IsPublic && !prop.GetGetMethod(true).IsPrivate)
                        data.Append(" internal ");
                    if (prop.GetGetMethod(true).IsStatic)
                        data.Append(" static ");
                    data.Append(" get ");
                    data.Append("> ");
                }
                if (prop.GetSetMethod(true) != null)
                {
                    data.Append(" <");
                    if (prop.GetSetMethod(true).IsPublic)
                        data.Append(" public ");
                    if (prop.GetSetMethod(true).IsPrivate)
                        data.Append(" private ");
                    if (!prop.GetSetMethod(true).IsPublic && !prop.GetSetMethod(true).IsPrivate)
                        data.Append(" internal ");
                    if (prop.GetSetMethod(true).IsStatic)
                        data.Append(" static ");
                    data.Append(" set ");
                    data.Append("> ");
                }
                data.Append(" ");
                data.Append(prop.PropertyType.FullName);
                data.Append(" ");

                data.Append(prop.Name);
                data.Append(": ");
                if (val != null)
                    data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (prop.PropertyType.IsArray)
                {
                    System.Collections.IList valueList = (System.Collections.IList)prop.GetValue(this, null);
                    for (int i = 0; i < valueList.Count; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(valueList[i].ToString());
                        data.Append("  ");
                    }
                }

                data.Append("\r\n");

            }

            return data.ToString();
        }

        /// <summary>
        /// Routine outputs name and value for all fields.
        /// </summary>
        /// <returns>String containing names and values.</returns>
        public string FieldsToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            FieldInfo[] finfos = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.SetProperty | BindingFlags.GetProperty | BindingFlags.FlattenHierarchy);
            bool typeHasFieldsToStringMethod = false;

            data.Append("\r\nClass fields for ");
            data.Append(t.FullName);
            data.Append("\r\n");

            int inx = 0;
            int maxInx = finfos.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                FieldInfo fld = finfos[inx];
                object val = fld.GetValue(this);
                if (fld.IsPublic)
                    data.Append(" public ");
                if (fld.IsPrivate)
                    data.Append(" private ");
                if (!fld.IsPublic && !fld.IsPrivate)
                    data.Append(" internal ");
                if (fld.IsStatic)
                    data.Append(" static ");
                data.Append(" ");
                data.Append(fld.FieldType.FullName);
                data.Append(" ");
                data.Append(fld.Name);
                data.Append(": ");
                typeHasFieldsToStringMethod = UseFieldsToString(fld.FieldType);
                if (val != null)
                    if (typeHasFieldsToStringMethod)
                        data.Append(GetFieldValues(val));
                    else
                        data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (fld.FieldType.IsArray)
                //if (fld.Name == "TestStringArray" || "_testStringArray")
                {
                    System.Collections.IList valueList = (System.Collections.IList)fld.GetValue(this);
                    for (int i = 0; i < valueList.Count; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(valueList[i].ToString());
                        data.Append("  ");
                    }
                }

                data.Append("\r\n");

            }

            return data.ToString();
        }

        private bool UseFieldsToString(Type pType)
        {
            bool retval = false;

            //avoid have this type calling its own FieldsToString and going into an infinite loop
            if (pType.FullName != this.GetType().FullName)
            {
                MethodInfo[] methods = pType.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    if (method.Name == "FieldsToString")
                    {
                        retval = true;
                        break;
                    }
                }
            }

            return retval;
        }

        private string GetFieldValues(object typeInstance)
        {
            Type typ = typeInstance.GetType();
            MethodInfo methodInfo = typ.GetMethod("FieldsToString");
            Object retval = methodInfo.Invoke(typeInstance, null);


            return (string)retval;
        }


        /// <summary>
        /// Returns a string containing the contents of the object in XML format.
        /// </summary>
        /// <returns>String value in xml format.</returns>
        /// ///<remarks>XML Serialization is used for the transformation.</remarks>
        public string ToXmlString()
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFWinFTP));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Converts instance of this class into an XML document.
        /// </summary>
        /// <returns>XmlDocument</returns>
        /// ///<remarks>XML Serialization and XmlDocument class are used for the transformation.</remarks>
        public XmlDocument ToXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(this.ToXmlString());
            return doc;
        }


    }//end class
}//end namespace
