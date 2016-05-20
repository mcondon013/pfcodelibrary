//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;
using PFTextObjects;
using PFDataAccessObjects;
using PFDocumentGlobals;

namespace PFOfficeInteropObjects
{
    /// <summary>
    /// Routines for manipulating an Excel document using the Office Interop library.
    /// </summary>
    public class PFExcelInterop
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private PFDataImporter _importer = new PFDataImporter();

        //private variables for properties
        enExcelOutputFormat _excelOutputFormat = enExcelOutputFormat.NotSpecified;
        private string _documentFilePath = string.Empty;
        private string _sheetName = "Sheet1";
        private bool _replaceExistingFile = true;
        private bool _replaceExistingSheet = true;

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PFExcelInterop()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="excelOutputFormat">Set to enExcelOutputFormat.Excel2003 for an XLS file. Set to enExcelOutputFormat.Excel2007 for an XLSX file.</param>
        public PFExcelInterop(enExcelOutputFormat excelOutputFormat)
        {
            _excelOutputFormat = excelOutputFormat;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="excelOutputFormat">Set to enExcelOutputFormat.Excel2003 for an XLS file. Set to enExcelOutputFormat.Excel2007 for an XLSX file.</param>
        /// <param name="documentFilePath">Full path to the output file.</param>
        public PFExcelInterop(enExcelOutputFormat excelOutputFormat, string documentFilePath)
        {
            _excelOutputFormat = excelOutputFormat;
            _documentFilePath = documentFilePath;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="excelOutputFormat">Set to enExcelOutputFormat.Excel2003 for an XLS file. Set to enExcelOutputFormat.Excel2007 for an XLSX file.</param>
        /// <param name="documentFilePath">Full path to the output file.</param>
        /// <param name="replaceExistingFile">True to overwrite existing file with same name. Otherwise, set to false to throw an error if file with same name already exists.</param>
        public PFExcelInterop(enExcelOutputFormat excelOutputFormat, string documentFilePath, bool replaceExistingFile)
        {
            _excelOutputFormat = excelOutputFormat;
            _documentFilePath = documentFilePath;
            _replaceExistingFile = replaceExistingFile;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="excelOutputFormat">Set to enExcelOutputFormat.Excel2003 for an XLS file. Set to enExcelOutputFormat.Excel2007 for an XLSX file.</param>
        /// <param name="documentFilePath">Full path to the output file.</param>
        /// <param name="sheetName">Name of the Excel sheet where data is to be written to or read from.</param>
        public PFExcelInterop(enExcelOutputFormat excelOutputFormat, string documentFilePath, string sheetName)
        {
            _excelOutputFormat = excelOutputFormat;
            _documentFilePath = documentFilePath;
            _sheetName = sheetName;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="excelOutputFormat">Set to enExcelOutputFormat.Excel2003 for an XLS file. Set to enExcelOutputFormat.Excel2007 for an XLSX file.</param>
        /// <param name="documentFilePath">Full path to the output file.</param>
        /// <param name="sheetName">Name of the Excel sheet where data is to be written to or read from.</param>
        /// <param name="replaceExistingFile">True to overwrite existing file with same name. Otherwise, set to false to throw an error if file with same name already exists.</param>
        public PFExcelInterop(enExcelOutputFormat excelOutputFormat, string documentFilePath, string sheetName, bool replaceExistingFile)
        {
            _excelOutputFormat = excelOutputFormat;
            _documentFilePath = documentFilePath;
            _sheetName = sheetName;
        }

                /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="excelOutputFormat">Set to enExcelOutputFormat.Excel2003 for an XLS file. Set to enExcelOutputFormat.Excel2007 for an XLSX file.</param>
        /// <param name="documentFilePath">Full path to the output file.</param>
        /// <param name="sheetName">Name of the Excel sheet where data is to be written to or read from.</param>
        /// <param name="replaceExistingFile">True to overwrite existing file with same name. Otherwise, set to false to modify contents of existing file.</param>
        /// <param name="replaceExistingSheet">True to overwrite existing sheet with same name. Otherwise, set to false to modify contents of existing sheet.</param>
        public PFExcelInterop(enExcelOutputFormat excelOutputFormat, string documentFilePath, string sheetName, bool replaceExistingFile, bool replaceExistingSheet)
        {
            _excelOutputFormat = excelOutputFormat;
            _documentFilePath = documentFilePath;
            _sheetName = sheetName;
            _replaceExistingFile = replaceExistingFile;
            _replaceExistingSheet = replaceExistingSheet;
        }


        //properties

        /// <summary>
        /// Set to enExcelVersion.Excel2003 for an XLS file.
        /// Set to enExcelVersion.Excel2007 for an XLSX file.
        /// </summary>
        public enExcelOutputFormat ExcelOutputFormat
        {
            get
            {
                return _excelOutputFormat;
            }
            set
            {
                _excelOutputFormat = value;
            }
        }

        /// <summary>
        /// Full path to the document file.
        /// </summary>
        public string DocumentFilePath
        {
            get
            {
                return _documentFilePath;
            }
            set
            {
                _documentFilePath = value;
            }
        }

        /// <summary>
        /// Name of Excel sheet to use. Default is Sheet1.
        /// </summary>
        public string SheetName
        {
            get
            {
                return _sheetName;
            }
            set
            {
                _sheetName = value;
            }
        }

        /// <summary>
        /// If True and file already exists, the old file will be deleted and the new file created.
        /// If False, an error will be raised if a file by the same name already exists.
        /// </summary>
        /// <remarks>Default is True. An existing file with same name will be deleted.</remarks>
        public bool ReplaceExistingFile
        {
            get
            {
                return _replaceExistingFile;
            }
            set
            {
                _replaceExistingFile = value;
            }
        }

        /// <summary>
        /// If True and sheet already exists, the old sheet will be deleted and the new sheet created.
        /// If False, an error will be raised if a sheet with the same name already exists.
        /// </summary>
        /// <remarks>Default is True. An existing sheet with same name will be deleted.</remarks>
        public bool ReplaceExistingSheet
        {
            get
            {
                return _replaceExistingSheet;
            }
            set
            {
                _replaceExistingSheet = value;
            }
        }


        //methods

        /// <summary>
        /// Writes data contained in XML string to path stored in DocumentFilePath property.
        /// </summary>
        /// <param name="xmlString">String containing valid XML formatted data.</param>
        /// <returns>True if output operation is successful. False if write fails.</returns>
        public bool WriteDataToDocument(string xmlString)
        {
            bool success = true;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);
            success = WriteDataToDocument(xmlDoc);
            return success;
        }

        /// <summary>
        /// Writes data contained in XML document object to path stored in DocumentFilePath property.
        /// </summary>
        /// <param name="xmlDoc">XML formatted document object.</param>
        /// <returns>True if output operation is successful. False if write fails.</returns>
        public bool WriteDataToDocument(XmlDocument xmlDoc)
        {
            bool success = true;
            DataTable dt = _importer.ImportXmlDocumentToDataTable(xmlDoc);
            success = WriteDataToDocument(dt);
            return success;
        }

        /// <summary>
        /// Writes data contained in ADO.NET DataTable object to path stored in DocumentFilePath property.
        /// </summary>
        /// <param name="dt">DataTable object containing data to be imported.</param>
        /// <returns>True if output operation is successful. False if write fails.</returns>
        public bool WriteDataToDocument(DataTable dt)
        {
            bool success = true;

            try
            {
                if (File.Exists(this.DocumentFilePath))
                {
                    if (_replaceExistingFile)
                    {
                        File.SetAttributes(this.DocumentFilePath, FileAttributes.Normal);
                        File.Delete(this.DocumentFilePath);
                    }
                    else
                    {
                        _msg.Length = 0;
                        _msg.Append("Excel document file exists and ReplaceExistingFile set to False. Write to Excel document has failed.");
                        throw new System.Exception(_msg.ToString());
                    }
                }

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

                Microsoft.Office.Interop.Excel.Workbook wb = excelApp.Workbooks.Add(Type.Missing);

                excelApp.Columns.ColumnWidth = 30;

                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)excelApp.Worksheets["Sheet1"];
                ws.Name = dt.TableName;

                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    ws.Cells[1, c + 1] = dt.Columns[c].ColumnName;
                }

                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        DataRow row = dt.Rows[r];
                        ws.Cells[r + 2, c + 1] = row[c].ToString();
                    }
                }

                Microsoft.Office.Interop.Excel.XlFileFormat fileFormat = Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV;
                if (this.ExcelOutputFormat == enExcelOutputFormat.Excel2007)
                    fileFormat = Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook;
                else if (this.ExcelOutputFormat == enExcelOutputFormat.Excel2003)
                    fileFormat = Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8;
                else
                    fileFormat = Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV;  //default to CSV for not specified or invalid format request
                                                                                        
                excelApp.ActiveWorkbook.SaveAs(this.DocumentFilePath, fileFormat);

                wb.Close(false);
                excelApp.Quit();

                excelApp = null;
            }
            catch (System.Exception ex)
            {
                success = false;
                _msg.Length = 0;
                _msg.Append("Attempt to import DataTable into Excel document failed.");
                _msg.Append(Environment.NewLine);
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }

            return success;
        }

        /// <summary>
        /// Writes data contained in ADO.NET DataTable object to path stored in DocumentFilePath property.
        /// </summary>
        /// <param name="dt">DataTable object containing data to be imported.</param>
        /// <returns>True if output operation is successful. False if write fails.</returns>
        /// <remarks>AppendDataToExistingDocument will report an error if workbook or sheet cannot be found.s.</remarks>
        public bool AppendDataToExistingSheet(DataTable dt)
        {
            bool success = true;
            int excelRow = 0;

            try
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

                Microsoft.Office.Interop.Excel.Workbook wb = null; 

                if (File.Exists(this.DocumentFilePath))
                {
                    wb = excelApp.Workbooks.Open(this.DocumentFilePath);
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to load ");
                    _msg.Append(this.DocumentFilePath);
                    _msg.Append(". AppendDataToExistSheet has failed.");
                    throw new System.Exception(_msg.ToString());
                }


                Microsoft.Office.Interop.Excel.Worksheet ws = null;

                foreach (Microsoft.Office.Interop.Excel.Worksheet sh in wb.Sheets)
                {
                    if (sh.Name == this.SheetName)
                        ws = sh;
                }

                if (ws == null)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find sheet ");
                    _msg.Append(this.SheetName);
                    _msg.Append(". AppendDataToExistSheet has failed.");
                    throw new System.Exception(_msg.ToString());
                }
                

                //no headings output when appending to existing data

                Microsoft.Office.Interop.Excel.Range range = ws.UsedRange;

                excelRow = range.Rows.Count;
                
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    excelRow++;
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        DataRow row = dt.Rows[r];
                        excelApp.Cells[excelRow, c + 1] = row[c].ToString();
                    }
                }


                Microsoft.Office.Interop.Excel.XlFileFormat fileFormat = Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV;
                if (this.ExcelOutputFormat == enExcelOutputFormat.Excel2007)
                    fileFormat = Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook;
                else if (this.ExcelOutputFormat == enExcelOutputFormat.Excel2003)
                    fileFormat = Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8;
                else
                    fileFormat = Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV;  //default to CSV for not specified or invalid format request

                excelApp.DisplayAlerts = false;
                excelApp.ActiveWorkbook.SaveAs(this.DocumentFilePath, fileFormat);

                wb.Close(false);
                excelApp.Quit();

                excelApp = null;
            }
            catch (System.Exception ex)
            {
                success = false;
                _msg.Length = 0;
                _msg.Append("Attempt to import DataTable into Excel document failed.");
                _msg.Append(Environment.NewLine);
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }

            return success;
        }//end method


        /// <summary>
        /// Writes data contained in XML string to path stored in DocumentFilePath property.
        /// </summary>
        /// <param name="xmlString">String containing valid XML formatted data.</param>
        /// <returns>True if output operation is successful. False if write fails.</returns>
        /// <remarks>Ext version of WriteDataToDocument has extra logic for modifying existing documents.</remarks>
        public bool WriteDataToDocumentExt(string xmlString)
        {
            bool success = true;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);
            success = WriteDataToDocumentExt(xmlDoc);
            return success;
        }

        /// <summary>
        /// Writes data contained in XML document object to path stored in DocumentFilePath property.
        /// </summary>
        /// <param name="xmlDoc">XML formatted document object.</param>
        /// <returns>True if output operation is successful. False if write fails.</returns>
        /// <remarks>Ext version of WriteDataToDocument has extra logic for modifying existing documents.</remarks>
        public bool WriteDataToDocumentExt(XmlDocument xmlDoc)
        {
            bool success = true;
            DataTable dt = _importer.ImportXmlDocumentToDataTable(xmlDoc);
            success = WriteDataToDocumentExt(dt);
            return success;
        }

        /// <summary>
        /// Writes data contained in ADO.NET DataTable object to path stored in DocumentFilePath property.
        /// </summary>
        /// <param name="dt">DataTable object containing data to be imported.</param>
        /// <returns>True if output operation is successful. False if write fails.</returns>
        /// <remarks>Ext version of WriteDataToDocument has extra logic for modifying existing documents.</remarks>
        public bool WriteDataToDocumentExt(DataTable dt)
        {
            bool success = true;

            try
            {
                if (File.Exists(this.DocumentFilePath))
                {
                    if (_replaceExistingFile)
                    {
                        File.SetAttributes(this.DocumentFilePath, FileAttributes.Normal);
                        File.Delete(this.DocumentFilePath);
                    }
                }

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

                Microsoft.Office.Interop.Excel.Workbook wb = excelApp.Workbooks.Add(Type.Missing);

                excelApp.Columns.ColumnWidth = 30;

                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)excelApp.Worksheets["Sheet1"];
                excelApp.DisplayAlerts = false;
                ws.Delete();


                if (File.Exists(this.DocumentFilePath))
                {
                    wb = excelApp.Workbooks.Open(this.DocumentFilePath);
                }

                if (this.SheetName.Trim().Length == 0)
                    this.SheetName = "Sheet1";


                if (this._replaceExistingSheet)
                {
                    foreach (Microsoft.Office.Interop.Excel.Worksheet sh in wb.Sheets)
                    {
                        if (sh.Name == this.SheetName)
                        {
                            sh.Delete();
                            break;
                        }
                    }
                }

                Microsoft.Office.Interop.Excel.Worksheet sheet = null;
                foreach (Microsoft.Office.Interop.Excel.Worksheet sh in wb.Sheets)
                {
                    if (sh.Name == this.SheetName)
                    {
                        sheet = sh;
                        break;
                    }
                }

                if (sheet == null)
                {
                    sheet = wb.Sheets.Add(After: wb.Sheets[wb.Sheets.Count]);
                    sheet.Name = this.SheetName;
                }

                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    sheet.Cells[1, c + 1] = dt.Columns[c].ColumnName;
                }

                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        DataRow row = dt.Rows[r];
                        sheet.Cells[r + 2, c + 1] = row[c].ToString();
                    }
                }

                Microsoft.Office.Interop.Excel.XlFileFormat fileFormat = Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV;
                if (this.ExcelOutputFormat == enExcelOutputFormat.Excel2007)
                    fileFormat = Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook;
                else if (this.ExcelOutputFormat == enExcelOutputFormat.Excel2003)
                    fileFormat = Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8;
                else
                    fileFormat = Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV;  //default to CSV for not specified or invalid format request

                excelApp.DisplayAlerts = false;
                excelApp.ActiveWorkbook.SaveAs(this.DocumentFilePath, fileFormat);

                wb.Close(false);
                excelApp.Quit();

                excelApp = null;

            }
            catch (System.Exception ex)
            {
                success = false;
                _msg.Length = 0;
                _msg.Append("Attempt to import DataTable into Excel document failed.");
                _msg.Append(Environment.NewLine);
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }

            return success;
        }

        /// <summary>
        /// ExportDataToDataTable
        /// </summary>
        /// <param name="excelRangeName">Name given to a range of cells in the document.</param>
        /// <param name="columnNamesInFirstRow">If true, then export will assume first row contains column names.</param>
        /// <returns>DataTable object that contains the values in the given excelNameRange.</returns>
        public DataTable ExportExcelDataToDataTable(string excelRangeName, bool columnNamesInFirstRow)
        {
            DataTable dt = null;
            Microsoft.Office.Interop.Excel.Application excelApp = null;

            try
            {
                if (this.ExcelOutputFormat == enExcelOutputFormat.CSV)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to export data from a CSV file using a named range.");
                    throw new System.Exception(_msg.ToString());
                }


                if (File.Exists(this.DocumentFilePath) == false)
                {
                    _msg.Length = 0;
                    _msg.Append("File ");
                    _msg.Append(this.DocumentFilePath);
                    _msg.Append(" does not exist. Will not be able to export data.");
                    throw new System.Exception(_msg.ToString());
                }

                excelApp = new Microsoft.Office.Interop.Excel.Application();

                Microsoft.Office.Interop.Excel.Workbook wb = excelApp.Workbooks.Open(this.DocumentFilePath);

                Microsoft.Office.Interop.Excel.Worksheet ws = null;


                foreach (Microsoft.Office.Interop.Excel.Worksheet sh in wb.Sheets)
                {
                    if (sh.Name == this.SheetName)
                        ws = sh;
                }

                if (ws == null)
                {
                    _msg.Length = 0;
                    _msg.Append("Sheet ");
                    _msg.Append(this.SheetName);
                    _msg.Append(" does not exist. Will not be able to export data.");
                    throw new System.Exception(_msg.ToString());
                }

                Microsoft.Office.Interop.Excel.Range rng = null;
                try
                {
                    rng = ws.get_Range(excelRangeName, Type.Missing);
                }
                catch
                {
                    rng = null;
                }

                if (rng == null)
                {
                    _msg.Length = 0;
                    _msg.Append("Range ");
                    _msg.Append(excelRangeName);
                    _msg.Append(" does not exist. Will not be able to export data.");
                    throw new System.Exception(_msg.ToString());
                }

                dt = ExportExcelDataToDataTable(rng.Row, rng.Column, rng.Rows.Count, rng.Columns.Count, columnNamesInFirstRow);

                wb.Close(false);
                excelApp.Quit();
                excelApp = null;


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to export Excel data to DataTable failed.");
                _msg.Append(Environment.NewLine);
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                if (excelApp != null)
                {
                    excelApp.Quit();
                    excelApp = null;
                }
            }

            return dt;
        }


        /// <summary>
        /// ExportDataToDataTable
        /// </summary>
        /// <param name="startRow">First row of Excel range.</param>
        /// <param name="startCol">First column of Excel range.</param>
        /// <param name="endRow">Last row of Excel range.</param>
        /// <param name="endCol">Last column of Excel range.</param>
        /// <param name="columnNamesInFirstRow">If true, then export will assume first row contains column names.</param>
        /// <returns>DataTable object that contains the values in the given range of Excel cells.</returns>
        /// <remarks>Row and col values are one based. e.g. 1,1 to 100,100 for a range of 100 rows and 100 columns.</remarks>
        public DataTable ExportExcelDataToDataTable(int startRow, int startCol, int endRow, int endCol, bool columnNamesInFirstRow)
        {
            DataTable dt = new DataTable();
            int rowNum = 0;
            int colNum = 0;
            Microsoft.Office.Interop.Excel.Application excelApp = null;

            try
            {
                if (this.SheetName.Trim().Length == 0)
                    this.SheetName = "Sheet1";

                excelApp = new Microsoft.Office.Interop.Excel.Application();

                Microsoft.Office.Interop.Excel.Workbook wb = null;

                if (File.Exists(this.DocumentFilePath))
                {
                    wb = excelApp.Workbooks.Open(this.DocumentFilePath);
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to load ");
                    _msg.Append(this.DocumentFilePath);
                    _msg.Append(". ExportExcelDataToDataTable has failed.");
                    throw new System.Exception(_msg.ToString());
                }

                Microsoft.Office.Interop.Excel.Worksheet ws = null;
                foreach (Microsoft.Office.Interop.Excel.Worksheet sh in wb.Sheets)
                {
                    if (sh.Name == this.SheetName)
                        ws = sh;
                }

                if (ws == null)
                {
                    _msg.Length = 0;
                    _msg.Append("Sheet ");
                    _msg.Append(this.SheetName);
                    _msg.Append(" does not exist. Will not be able to export data.");
                    throw new System.Exception(_msg.ToString());
                }

                if (endCol < startCol || endRow < startRow)
                {
                    _msg.Length = 0;
                    _msg.Append("Invalid row and column coordinates specified.");
                    _msg.Append("FROM row " + startRow.ToString() + " column " + startCol.ToString() + " TO  row " + endRow.ToString() + " column " + endCol.ToString() + ". ");
                    throw new System.Exception(_msg.ToString());
                }

                ws = wb.Sheets[this.SheetName];

                if (columnNamesInFirstRow)
                {
                    //set datatable column names to values in first row of spreadsheet
                    for (int colInx = startCol; colInx <= endCol; colInx++)
                    {
                        DataColumn dc = new DataColumn();
                        dc.ColumnName = ws.Cells[startRow, colInx].Text.ToString();
                        dc.DataType = Type.GetType("System.String");
                        dc.Caption = dc.ColumnName;
                        dc.DefaultValue = string.Empty; 
                        dc.MaxLength = int.MaxValue;
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    //create arbitrary column names
                    colNum = 0;
                    for (int colInx = startCol; colInx < endCol; colInx++)
                    {
                        colNum++;
                        DataColumn dc = new DataColumn();
                        dc.ColumnName = "F" + colNum.ToString("0");
                        dc.DataType = Type.GetType("System.String");
                        dc.Caption = dc.ColumnName;
                        dc.DefaultValue = string.Empty;
                        dc.MaxLength = int.MaxValue;
                        dt.Columns.Add(dc);
                    }

                }


                if (columnNamesInFirstRow)
                    rowNum = startRow + 1;
                else
                    rowNum = startRow;

                for (int rowInx = rowNum; rowInx <= endRow; rowInx++)
                {
                    DataRow dr = dt.NewRow();
                    colNum = 0;
                    for (int colInx = startCol; colInx <= endCol; colInx++)
                    {
                        dr[colNum] = ws.Cells[rowInx, colInx].Text.ToString();
                        colNum++;
                    }
                    dt.Rows.Add(dr);
                }

                wb.Close(false);
                excelApp.Quit();

                excelApp = null;



            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to output Excel data to DataTable failed.");
                _msg.Append(Environment.NewLine);
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                if (excelApp != null)
                {
                    excelApp.Quit();
                    excelApp = null;
                }

            }



            return dt;
        }



    }//end class
}//end namespace
