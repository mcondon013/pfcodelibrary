using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using PFMessageLogs;
using PFTimers;
using PFFileSystemObjects;

namespace TestprogDotNetZip
{
    public class Tests
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = false;

        private MessageLog _messageLog;
        private string _helpFilePath = string.Empty;

        //properties
        public bool SaveErrorMessagesToAppLog
        {
            get
            {
                return _saveErrorMessagesToAppLog;
            }
            set
            {
                _saveErrorMessagesToAppLog = value;
            }
        }

        /// <summary>
        /// Message log window manager.
        /// </summary>
        public MessageLog MessageLogUI
        {
            get
            {
                return _messageLog;
            }
            set
            {
                _messageLog = value;
            }
        }

        /// <summary>
        /// Path to application help file.
        /// </summary>
        public string HelpFilePath
        {
            get
            {
                return _helpFilePath;
            }
            set
            {
                _helpFilePath = value;
            }
        }


        //tests


        public void CreateZipFile(MainForm frm)
        {
            string zipOutputFile = string.Empty;
            Stopwatch sw = new Stopwatch();
            try
            {
                _msg.Length = 0;
                _msg.Append("CreateZipFile started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                zipOutputFile = Path.Combine(Path.GetDirectoryName(frm.txtZipOutputFolder.Text), Path.GetFileName(frm.txtFileToZip.Text) + ".zip");
                ZipArchive zip = new ZipArchive(zipOutputFile);
                if (frm.chkIncludeDirectoryPathInZipArchive.Checked)
                    zip.DirectoryPathInArchive = null;
                else
                    zip.DirectoryPathInArchive = string.Empty;
                bool res = zip.AddFiles(new string[] { frm.txtFileToZip.Text });

                _msg.Length = 0;
                if (res == true)
                {
                    _msg.Append(frm.txtFileToZip.Text);
                    _msg.Append(" successfully zipped to ");
                    _msg.Append(zipOutputFile);
                    _msg.Append(".");
                }
                else
                {
                    _msg.Append("Attempt to zip ");
                    _msg.Append(frm.txtFileToZip.Text);
                    _msg.Append(" to ");
                    _msg.Append(zipOutputFile);
                    _msg.Append(" failed.");
                }
                WriteMessageToLog(_msg.ToString());

                sw.Stop();
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Elapsed Time: ");
                _msg.Append(sw.FormattedElapsedTime);
                _msg.Append(Environment.NewLine);
                _msg.Append("\r\n... CreateZipFile finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public void AddMultipleFilesToZipFile(MainForm frm)
        {
            string zipOutputFile = string.Empty;
            Stopwatch sw = new Stopwatch();
            
            try
            {
                _msg.Length = 0;
                _msg.Append("AddMultipleFilesToZipFile started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                string[] files = frm.txtMultipleFilesToZip.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                zipOutputFile = Path.Combine(Path.GetDirectoryName(frm.txtZipOutputFolder.Text), Path.GetFileName(frm.txtFileToZip.Text) + ".zip");
                ZipArchive zip = new ZipArchive(zipOutputFile);
                if (frm.chkIncludeDirectoryPathInZipArchive.Checked)
                    zip.DirectoryPathInArchive = null;
                else
                    zip.DirectoryPathInArchive = string.Empty;
                bool res = zip.AddFiles(files);

                _msg.Length = 0;
                if (res == true)
                {
                    _msg.Append(frm.txtMultipleFilesToZip.Text);
                    _msg.Append("\r\n  successfully zipped to ");
                    _msg.Append(zipOutputFile);
                    _msg.Append(".");
                }
                else
                {
                    _msg.Append("Attempt to zip ");
                    _msg.Append(frm.txtMultipleFilesToZip.Text);
                    _msg.Append("\r\n  to ");
                    _msg.Append(zipOutputFile);
                    _msg.Append(" failed.");
                }
                WriteMessageToLog(_msg.ToString());

                sw.Stop();
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Elapsed Time: ");
                _msg.Append(sw.FormattedElapsedTime);
                _msg.Append(Environment.NewLine);
                _msg.Append("\r\n... AddMultipleFilesToZipFile finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        

        public void ExtractFromZipFile(MainForm frm)
        {
            string zipInputFile = string.Empty;
            string destinationFolder = string.Empty;
            Stopwatch sw = new Stopwatch();
            
            try
            {
                _msg.Length = 0;
                _msg.Append("ExtractFromZipFile started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();
                
                zipInputFile = frm.txtFileToUnzip.Text;
                destinationFolder = frm.txtUnzipOutputFolder.Text;
                ZipArchive zip = new ZipArchive(zipInputFile);
                zip.FlattenFoldersOnExtract = frm.chkFlattenFoldersOnExtract.Checked;
                zip.OverwriteSilently = frm.chkOverwriteSilently.Checked;
                int numEntriesExtracted = zip.ExtractAll(destinationFolder);

                _msg.Length = 0;
                _msg.Append(numEntriesExtracted.ToString());
                _msg.Append(" entries extracted from ");
                _msg.Append(zipInputFile);
                _msg.Append(" to ");
                _msg.Append(destinationFolder);
                _msg.Append(".");
                WriteMessageToLog(_msg.ToString());

                sw.Stop();
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Elapsed Time: ");
                _msg.Append(sw.FormattedElapsedTime);
                _msg.Append(Environment.NewLine);
                _msg.Append("\r\n... ExtractFromZipFile finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        

        //calling module must set MessageLog property to a valid instance of the message log for messages to be displayed
        public void WriteMessageToLog(string msg)
        {
            if (_messageLog != null)
                _messageLog.WriteLine(msg);
        }



    }//end class
}//end namespace

