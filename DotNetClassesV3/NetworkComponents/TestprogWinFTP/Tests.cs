using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Configuration;
using PFFileTransferObjects;
using PFTimers;

namespace TestprogWinFTP
{
    public class Tests
    {
        private static StringBuilder _msg = new StringBuilder();
        private static StringBuilder _str = new StringBuilder();
        private static bool _saveErrorMessagesToAppLog = false;

        //properties
        public static bool SaveErrorMessagesToAppLog
        {
            get
            {
                return Tests._saveErrorMessagesToAppLog;
            }
            set
            {
                Tests._saveErrorMessagesToAppLog = value;
            }
        }

        //tests
        public static void GetStaticKeys(MainForm frm)
        {

            try
            {
                _msg.Length = 0;
                _msg.Append("GetStaticKeys started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("StaticKeySection values:\r\n");
                _msg.Append("MainFormCaption = ");
                _msg.Append(StaticKeysSection.Settings.MainFormCaption);
                _msg.Append("\r\n");
                _msg.Append("MinAppThreads = ");
                _msg.Append(StaticKeysSection.Settings.MinAppThreads.ToString());
                _msg.Append("\r\n");
                _msg.Append("MaxAppThreads = ");
                _msg.Append(StaticKeysSection.Settings.MaxAppThreads.ToString());
                _msg.Append("\r\n");
                _msg.Append("RequireLogon = ");
                _msg.Append(StaticKeysSection.Settings.RequireLogon.ToString());
                _msg.Append("\r\n");
                _msg.Append("ValidBooleanValues = ");
                _msg.Append(StaticKeysSection.Settings.ValidBooleanValues);
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... GetStaticKeys finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void UploadFileTest(MainForm frm)
        {
            PFWinFTP ftp = new PFWinFTP();
            Stopwatch sw = new Stopwatch();

            try
            {
                sw.Start();

                _msg.Length = 0;
                _msg.Append("UploadFileTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                InitializeFtpFromForm(frm, ftp);

                //_msg.Length = 0;
                //_msg.Append("_ftp object:\r\n");
                //_msg.Append(_ftp.ToString());
                //Program._messageLog.WriteLine(_msg.ToString());

                ftp.UploadFileToFtpHost();

                if (ftp.RemoteFileExists(frm.txtRemoteFile.Text))
                {
                    _msg.Length = 0;
                    _msg.Append(frm.txtRemoteFile.Text);
                    _msg.Append(" was written to ");
                    _msg.Append(ftp.FtpHost);
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find ");
                    _msg.Append(frm.txtRemoteFile.Text);
                    _msg.Append(" on ");
                    _msg.Append(ftp.FtpHost);
                }
                Program._messageLog.WriteLine(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("\r\nElapsed time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("\r\n... UploadFileTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        private static void InitializeFtpFromForm(MainForm frm, PFWinFTP ftp)
        {
            ftp.FtpHost = frm.txtFtpHost.Text;
            ftp.FtpUsername = frm.txtFtpUsername.Text;
            ftp.FtpPassword = frm.txtFtpPassword.Text;
            //ftp.UseAsyncFileTransfer = frm.chkUseAsync.Checked;
            //ftp.ProgressIntervalInSeconds = AppTextGlobals.ConvertStringToInt(frm.txtProgressIntervalInSecs.Text, 1);
            //ftp.RetryTimeout = AppTextGlobals.ConvertStringToInt(frm.txtRetryTimeout.Text, 300000);
            //ftp.UseFipsCompliantMode = frm._useFipsCompliantMode;
            ftp.FtpBufferSize = AppTextGlobals.ConvertStringToInt(frm.txtBufferSize.Text, 32767);
            ftp.SourceFile = frm.txtLocalSourceFile.Text;
            ftp.RemoteFile = frm.txtRemoteFile.Text;
            ftp.LocalDestinationFile = frm.txtLocalDestinationFile.Text;

        }

        public static void DownloadFileTest(MainForm frm)
        {
            PFWinFTP ftp = new PFWinFTP();
            Stopwatch sw = new Stopwatch();

            try
            {
                sw.Start();

                _msg.Length = 0;
                _msg.Append("DownloadFileTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                InitializeFtpFromForm(frm, ftp);

                //_msg.Length = 0;
                //_msg.Append("_ftp object:\r\n");
                //_msg.Append(_ftp.ToString());
                //Program._messageLog.WriteLine(_msg.ToString());

                if (ftp.RemoteFileExists(frm.txtRemoteFile.Text))
                {
                    _msg.Length = 0;
                    _msg.Append(frm.txtRemoteFile.Text);
                    _msg.Append(" exists on ");
                    _msg.Append(ftp.FtpHost);
                    Program._messageLog.WriteLine(_msg.ToString());
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find ");
                    _msg.Append(frm.txtRemoteFile.Text);
                    _msg.Append(" on ");
                    _msg.Append(ftp.FtpHost);
                    throw new FileNotFoundException(_msg.ToString());
                }

                ftp.DownloadFileFromFtpHost();

                if (File.Exists(frm.txtLocalDestinationFile.Text))
                {
                    _msg.Length = 0;
                    _msg.Append(frm.txtLocalDestinationFile.Text);
                    _msg.Append(" exists.");
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find ");
                    _msg.Append(frm.txtLocalDestinationFile.Text);
                    _msg.Append(".");
                }
                Program._messageLog.WriteLine(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessageWithStackTrace(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("\r\nElapsed time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("\r\n... DownloadFileTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void GetRemoteFileInfoTest(MainForm frm)
        {
            PFWinFTP ftp = new PFWinFTP();
            Stopwatch sw = new Stopwatch();

            try
            {
                sw.Start();

                _msg.Length = 0;
                _msg.Append("GetRemoteFileInfoTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                InitializeFtpFromForm(frm, ftp);

                if (ftp.RemoteFileExists(frm.txtRemoteFile.Text))
                {
                    _msg.Length = 0;
                    _msg.Append(frm.txtRemoteFile.Text);
                    _msg.Append(" exists on ");
                    _msg.Append(ftp.FtpHost);
                    Program._messageLog.WriteLine(_msg.ToString());
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find ");
                    _msg.Append(frm.txtRemoteFile.Text);
                    _msg.Append(" on ");
                    _msg.Append(ftp.FtpHost);
                    throw new FileNotFoundException(_msg.ToString());
                }


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("\r\nElapsed time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("\r\n... GetRemoteFileInfoTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void RenameRemoteFileTest(MainForm frm)
        {
            PFWinFTP ftp = new PFWinFTP();
            Stopwatch sw = new Stopwatch();

            try
            {
                sw.Start();

                _msg.Length = 0;
                _msg.Append("RenameRemoteFileTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                InitializeFtpFromForm(frm, ftp);

                if (ftp.RemoteFileExists(frm.txtRemoteFile.Text))
                {
                    _msg.Length = 0;
                    _msg.Append(frm.txtRemoteFile.Text);
                    _msg.Append(" exists on ");
                    _msg.Append(ftp.FtpHost);
                    Program._messageLog.WriteLine(_msg.ToString());
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find ");
                    _msg.Append(frm.txtRemoteFile.Text);
                    _msg.Append(" on ");
                    _msg.Append(ftp.FtpHost);
                    throw new FileNotFoundException(_msg.ToString());
                }

                string newFileName = frm.txtRenameTo.Text;
                bool renameSucceeded = ftp.RenameFileOnFtpHost(frm.txtRemoteFile.Text, newFileName);

                if (renameSucceeded)
                    Program._messageLog.WriteLine("Rename succeeded.");
                else
                    Program._messageLog.WriteLine("Rename failed.");

                if (ftp.RemoteFileExists(newFileName))
                {
                    _msg.Length = 0;
                    _msg.Append(newFileName);
                    _msg.Append(" exists on ");
                    _msg.Append(ftp.FtpHost);
                    Program._messageLog.WriteLine(_msg.ToString());
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find ");
                    _msg.Append(newFileName);
                    _msg.Append(" on ");
                    _msg.Append(ftp.FtpHost);
                    Program._messageLog.WriteLine(_msg.ToString());
                }


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("\r\nElapsed time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("\r\n... RenameRemoteFileTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void DeleteRemoteFileTest(MainForm frm)
        {
            PFWinFTP ftp = new PFWinFTP();
            Stopwatch sw = new Stopwatch();

            try
            {
                sw.Start();

                _msg.Length = 0;
                _msg.Append("DeleteRemoteFileTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                InitializeFtpFromForm(frm, ftp);

                if (ftp.RemoteFileExists(frm.txtRemoteFile.Text))
                {
                    _msg.Length = 0;
                    _msg.Append(frm.txtRemoteFile.Text);
                    _msg.Append(" exists on ");
                    _msg.Append(ftp.FtpHost);
                    Program._messageLog.WriteLine(_msg.ToString());
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find ");
                    _msg.Append(frm.txtRemoteFile.Text);
                    _msg.Append(" on ");
                    _msg.Append(ftp.FtpHost);
                    throw new FileNotFoundException(_msg.ToString());
                }

                bool deleteSucceeded = ftp.DeleteRemoteFile(frm.txtRemoteFile.Text);

                if (deleteSucceeded)
                    Program._messageLog.WriteLine("Delete succeeded.");
                else
                    Program._messageLog.WriteLine("Delete failed.");

                if (ftp.RemoteFileExists(frm.txtRemoteFile.Text))
                {
                    _msg.Length = 0;
                    _msg.Append(frm.txtRemoteFile.Text);
                    _msg.Append(" exists on ");
                    _msg.Append(ftp.FtpHost);
                    Program._messageLog.WriteLine(_msg.ToString());
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find ");
                    _msg.Append(frm.txtRemoteFile.Text);
                    _msg.Append(" on ");
                    _msg.Append(ftp.FtpHost);
                    Program._messageLog.WriteLine(_msg.ToString());
                }


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("\r\nElapsed time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("\r\n... DeleteRemoteFileTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void ListRemoteFolderFilesTest(MainForm frm)
        {
            PFWinFTP ftp = new PFWinFTP();
            Stopwatch sw = new Stopwatch();

            try
            {
                sw.Start();

                _msg.Length = 0;
                _msg.Append("ListRemoteFolderFilesTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                InitializeFtpFromForm(frm, ftp);

                string folderName = Path.GetDirectoryName(frm.txtRemoteFile.Text).Replace(@"\",@"/");
                List<string> files = ftp.ListRemoteDirectoryFiles(folderName);

                foreach (string fi in files)
                {
                    _msg.Length = 0;
                    _msg.Append("FileName: ");
                    _msg.Append(fi);
                    _msg.Append("\r\n");
                    Program._messageLog.WriteLine(_msg.ToString());
                }


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                sw.Stop();
                _msg.Length = 0;
                _msg.Append("\r\nElapsed time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("\r\n... ListRemoteFolderFilesTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



    }//end class
}//end namespace
