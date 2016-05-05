using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Data;
using PFProcessObjects;
using PFMessageLogs;
using PFDocumentObjects;
using PFDocumentGlobals;

namespace TestprogDocumentObjects
{
    public class PFAppProcessor
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = false;

        private MessageLog _messageLog;
        private string _appConfigManagerExe = @"pfAppConfigManager.exe";

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


        //application routines

        /// <summary>
        /// Routine to add context menu item for pfFolderSize to Windows Explorer.
        /// </summary>
        public void ShowAppConfigManager()
        {
            PFProcess proc = new PFProcess();
            string currAppFolder = AppInfo.CurrentEntryAssemblyDirectory;
            string currAppExePath = AppInfo.CurrentEntryAssembly;
            string appConfigManagerApp = Path.Combine(currAppFolder, _appConfigManagerExe);
            string mydocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            bool appConfigManagerFound = false;

            try
            {
                proc.Arguments = "\"" + currAppExePath + "\" \"" + _helpFilePath + "\" " + "\"Change an Application Setting\"";

                if (File.Exists(appConfigManagerApp))
                {
                    appConfigManagerFound = true;
                    proc.WorkingDirectory = currAppFolder;
                    proc.ExecutableToRun = appConfigManagerApp;
                }
                else
                {
                    string configValue = AppConfig.GetStringValueFromConfigFile("appConfigManagerPath", string.Empty);
                    if (configValue.Length > 0)
                    {
                        if (File.Exists(configValue))
                        {
                            appConfigManagerFound = true;
                            proc.WorkingDirectory = Path.GetDirectoryName(configValue);
                            proc.ExecutableToRun = configValue;
                        }
                        else
                        {
                            appConfigManagerFound = false;
                        }
                    }
                    else
                    {
                        appConfigManagerFound = false;

                    }
                }
                if (appConfigManagerFound == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find Application Configuration Manager Application in current app folder: ");
                    _msg.Append(currAppFolder);
                    throw new System.Exception(_msg.ToString());
                }
                proc.CreateNoWindow = true;
                proc.UseShellExecute = true;
                proc.WindowStyle = PFProcessWindowStyle.Normal;
                proc.RedirectStandardOutput = false;
                proc.RedirectStandardError = false;
                proc.RedirectStandardInput = false;
                proc.CheckIfProcessWaitingForInput = false;
                proc.MaxProcessRunSeconds = (int)0;

                proc.Run();

                System.Configuration.ConfigurationManager.RefreshSection("appSettings");

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                proc = null;
            }

        }


        public void OutputToExcelDocument()
        {
            PFExcelDocument excelDoc = null;
            DataSet ds = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("OutputToExcelDocument started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                ds = new DataSet();
                ds.ReadXml(@"C:\Testfiles\C1Testing\RandomNames.xml");
                ds.Tables[0].TableName = "RandomNames";

                excelDoc = new PFExcelDocument(enExcelOutputFormat.Excel2007, @"c:\temp\ExcelRandomNames.xlsx", true);
                excelDoc.SheetName = "RandomNames";
                excelDoc.WriteDataToDocument(ds.Tables[0]);

                excelDoc = new PFExcelDocument(enExcelOutputFormat.Excel2003, @"c:\temp\ExcelRandomNames.xls", true);
                excelDoc.SheetName = "RandomNames";
                excelDoc.WriteDataToDocument(ds.Tables[0]);

                excelDoc = new PFExcelDocument(enExcelOutputFormat.CSV, @"c:\temp\ExcelRandomNames.csv", true);
                excelDoc.SheetName = "RandomNames";
                excelDoc.WriteDataToDocument(ds.Tables[0]);

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
                if (excelDoc != null)
                    excelDoc = null;
                if (ds != null)
                    ds = null;

                _msg.Length = 0;
                _msg.Append("\r\n... OutputToExcelDocument finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public void ExportDataFromExcelDocument()
        {
            PFExcelDocument excelDoc = null;
            PFExcelDocument excelDoc2 = null;
            DataSet ds = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("ExportDataFromExcelDocument started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                ds = new DataSet();

                excelDoc = new PFExcelDocument(enExcelOutputFormat.Excel2007, @"C:\Testfiles\C1Testing\ExcelRandomNames.xlsx", true);
                excelDoc.SheetName = "RandomNames";
                
                DataTable dt = excelDoc.ExportExcelDataToDataTable("RandomNamesList", true);
                ds.Tables.Add(dt);
                ds.WriteXml(@"c:\temp\RandomNamesOut.xml", XmlWriteMode.WriteSchema);

                excelDoc = null;


                excelDoc2 = new PFExcelDocument(enExcelOutputFormat.Excel2007, @"C:\Testfiles\C1Testing\ExcelRandomNamesForExport.xlsx", true);
                excelDoc2.SheetName = "RandomNames";

                DataTable dt2 = excelDoc2.ExportExcelDataToDataTable(1, 1, 101, 16, true);
                dt2.TableName = "RandomNamesListTable";
                dt2.WriteXml(@"c:\temp\RandomNamesOutTable.xml", XmlWriteMode.WriteSchema);

                excelDoc2 = null;
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
                if (excelDoc != null)
                    excelDoc = null;
                if (excelDoc2 != null)
                    excelDoc2 = null;
                if (ds != null)
                    ds = null;

                _msg.Length = 0;
                _msg.Append("\r\n... ExportDataFromExcelDocument finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public void OutputToWordDocument()
        {
            PFWordDocument wordDoc = null;
            DataSet ds = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("OutputToWordDocument started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                ds = new DataSet();
                ds.ReadXml(@"C:\Testfiles\C1Testing\BostonZipInfo.xml");
                ds.Tables[0].TableName = "RandomNames";

                wordDoc = new PFWordDocument(enWordOutputFormat.Word2007, @"c:\temp\WordRandomNamesBostonZip.docx", true);
                wordDoc.WriteDataToDocument(ds.Tables[0]);

                ds = new DataSet();
                ds.ReadXml(@"C:\Testfiles\C1Testing\RandomNames100Lines.xml");
                ds.Tables[0].TableName = "RandomNames";

                wordDoc = new PFWordDocument(enWordOutputFormat.Word2007, @"c:\temp\WordRandomNames.docx", true);
                wordDoc.WriteDataToDocument(ds.Tables[0]);

                ds = new DataSet();
                ds.ReadXml(@"C:\Testfiles\C1Testing\BostonZipInfo.xml");
                ds.Tables[0].TableName = "RandomNames";

                wordDoc = new PFWordDocument(enWordOutputFormat.Word2003, @"c:\temp\WordRandomNamesBostonZip.doc", true);
                wordDoc.WriteDataToDocument(ds.Tables[0]);

                ds = new DataSet();
                ds.ReadXml(@"C:\Testfiles\C1Testing\RandomNames100Lines.xml");
                ds.Tables[0].TableName = "RandomNames";

                wordDoc = new PFWordDocument(enWordOutputFormat.Word2003, @"c:\temp\WordRandomNames.doc", true);
                wordDoc.WriteDataToDocument(ds.Tables[0]);

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
                _msg.Append("\r\n... OutputToWordDocument finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public void OutputToRTFDocument(MainForm frm)
        {
            PFRTFDocument rtfDoc = null;
            DataSet ds = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("OutputToRTFDocument started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                ds = new DataSet();
                ds.ReadXml(@"C:\Testfiles\C1Testing\RandomNames100Lines.xml");
                ds.Tables[0].TableName = "RandomNames";

                rtfDoc = new PFRTFDocument(@"c:\temp\WordRandomNames.rtf", true);
                rtfDoc.WriteDataToDocument(ds.Tables[0]);

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
                _msg.Append("\r\n... OutputToRTFDocument finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public void OutputToPDFDocument(MainForm frm)
        {
            PFPDFDocument pdfDoc = null;
            DataSet ds = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("OutputToPDFDocument started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                ds = new DataSet();
                ds.ReadXml(@"C:\Testfiles\C1Testing\BostonZipInfo.xml");
                ds.Tables[0].TableName = "RandomNames";

                pdfDoc = new PFPDFDocument(@"c:\temp\SharpRandomNames5Cols.pdf", true);
                pdfDoc.WriteDataToDocument(ds.Tables[0]);

                ds = null;
                ds = new DataSet();
                ds.ReadXml(@"C:\Testfiles\C1Testing\RandomNames100Lines.xml");
                ds.Tables[0].TableName = "RandomNames";

                pdfDoc = new PFPDFDocument(@"c:\temp\SharpRandomNames17Cols.pdf", true);
                pdfDoc.WriteDataToDocument(ds.Tables[0]);

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
                _msg.Append("\r\n... OutputToPDFDocument finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        

    }//end class
}//end namespace
