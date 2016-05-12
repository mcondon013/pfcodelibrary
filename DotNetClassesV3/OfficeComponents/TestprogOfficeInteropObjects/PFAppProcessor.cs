using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Data;
using PFProcessObjects;
using PFMessageLogs;
using PFOfficeInteropObjects;
using PFDocumentGlobals;

namespace TestprogOfficeInteropObjects
{
    public class PFAppProcessor
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = false;

        private MessageLog _messageLog;

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

        //application routines


        public void OutputToExcelDocument(MainForm frm)
        {
            PFExcelInterop excelDoc = null;
            DataSet ds = null;
           // string filename = @"";

            try
            {
                _msg.Length = 0;
                _msg.Append("OutputToExcelDocument started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                ds = new DataSet();
                ds.ReadXml(@"C:\Testfiles\C1Testing\RandomNames100Lines.xml");
                ds.Tables[0].TableName = "RandomNames";

                if (frm.optXLSXFormat.Checked)
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.Excel2007, @"c:\temp\ExcelInteropRandomNames.xlsx", true);
                else if (frm.optXLSFormat.Checked)
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.Excel2003, @"c:\temp\ExcelInteropRandomNames.xls", true);
                else
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.CSV, @"c:\temp\ExcelInteropRandomNames.csv", true);


                excelDoc.SheetName = "RandomNames";
                if (frm.chkUseExtWriteMethod.Checked)
                {
                    excelDoc.ReplaceExistingFile = false;
                    excelDoc.WriteDataToDocumentExt(ds.Tables[0]);
                }
                else
                {
                    excelDoc.ReplaceExistingFile = true;
                    excelDoc.WriteDataToDocument(ds.Tables[0]);
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
                if (excelDoc != null)
                    excelDoc = null;
                if (ds != null)
                    ds = null;
                _msg.Length = 0;
                _msg.Append("\r\n... OutputToExcelDocument finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public void AppendToExcelDocument(MainForm frm)
        {
            PFExcelInterop excelDoc = null;
            DataSet ds = null;
            //string filename = @"";
            string sheetName = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("AppendToExcelDocument started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                ds = new DataSet();
                ds.ReadXml(@"C:\Testfiles\C1Testing\RandomNames100Lines.xml");
                ds.Tables[0].TableName = "RandomNames";

                if (frm.optXLSXFormat.Checked)
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.Excel2007, @"c:\temp\ExcelInteropRandomNamesInput.xlsx", "RandomNames", false, false);
                else if (frm.optXLSFormat.Checked)
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.Excel2003, @"c:\temp\ExcelInteropRandomNamesInput.xls", "RandomNames", false, false);
                else
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.CSV, @"c:\temp\ExcelInteropRandomNamesInput.csv", "ExcelInteropRandomNamesInput", false, false);


                excelDoc.SheetName = "RandomNames";
                excelDoc.AppendDataToExistingSheet(ds.Tables[0]);

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
                _msg.Append("\r\n... AppendToExcelDocument finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public void ExportExcelDocument(MainForm frm)
        {
            PFExcelInterop excelDoc = null;
            DataTable dt = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("ExportExcelDocument started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                if (frm.optXLSXFormat.Checked)
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.Excel2007, @"C:\Testfiles\C1Testing\ExcelRandomNamesForExport.xlsx", "RandomNames", false, false);
                else if (frm.optXLSFormat.Checked)
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.Excel2003, @"C:\Testfiles\C1Testing\ExcelRandomNamesForExport.xls", "RandomNames", false, false);
                else
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.CSV, @"C:\Testfiles\C1Testing\ExcelRandomNamesForExport.csv", "ExcelRandomNamesForExport", false, false);

                _msg.Length = 0;
                if (frm.optRowCol.Checked)
                {
                    _msg.Append("Export by row and column method.");
                    Program._messageLog.WriteLine(_msg.ToString());
                    dt = excelDoc.ExportExcelDataToDataTable(1, 1, 101, 16, true);
                }
                else
                {
                    //frm.optNamedRange.Checked 
                    _msg.Append("Export by named range method.");
                    Program._messageLog.WriteLine(_msg.ToString());
                    dt = excelDoc.ExportExcelDataToDataTable("PersonData", true);
                }

                if (dt != null)
                {
                    excelDoc.DocumentFilePath = @"c:\temp\ExportedDataTableValues.xlsx";
                    excelDoc.ExcelOutputFormat = enExcelOutputFormat.Excel2007;
                    excelDoc.ReplaceExistingFile = true;
                    excelDoc.WriteDataToDocumentExt(dt);
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
                _msg.Length = 0;
                _msg.Append("\r\n... ExportExcelDocument finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        

        public void OutputToWordDocument(MainForm frm)
        {
            PFWordInterop wordDoc = null;
            DataSet ds = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("OutputToWordDocument started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                ds = new DataSet();
                ds.ReadXml(@"C:\Testfiles\C1Testing\RandomNames100Lines.xml");
                ds.Tables[0].TableName = "RandomNames";

                if(frm.optDOCXFormat.Checked)
                    wordDoc = new PFWordInterop(enWordOutputFormat.Word2007, @"c:\temp\WordInteropRandomNames.docx", true);
                else if (frm.optDOCFormat.Checked)
                    wordDoc = new PFWordInterop(enWordOutputFormat.Word2003, @"c:\temp\WordInteropRandomNames.doc", true);
                else if (frm.optRTFFormat.Checked)
                    wordDoc = new PFWordInterop(enWordOutputFormat.RTF, @"c:\temp\WordInteropRandomNames.rtf", true);
                else if (frm.optPDFFormat.Checked)
                    wordDoc = new PFWordInterop(enWordOutputFormat.PDF, @"c:\temp\WordInteropRandomNames.pdf", true);
                else
                    wordDoc = new PFWordInterop(enWordOutputFormat.Word2007, @"c:\temp\WordInteropRandomNames.docx", true);


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


    }//end class
}//end namespace
