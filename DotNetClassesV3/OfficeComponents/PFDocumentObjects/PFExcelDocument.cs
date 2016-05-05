//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
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
using PFOfficeInteropObjects;
using PFDocumentGlobals;

namespace PFDocumentObjects
{
    /// <summary>
    /// Routines for manipulating an Excel document using the ComponentOne Excel for .NET library.
    /// </summary>
    public class PFExcelDocument
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
        public PFExcelDocument()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="excelOutputFormat">Set to enExcelOutputFormat.Excel2003 for an XLS file. Set to enExcelOutputFormat.Excel2007 for an XLSX file.</param>
        public PFExcelDocument(enExcelOutputFormat excelOutputFormat)
        {
            _excelOutputFormat = excelOutputFormat;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="excelOutputFormat">Set to enExcelOutputFormat.Excel2003 for an XLS file. Set to enExcelOutputFormat.Excel2007 for an XLSX file.</param>
        /// <param name="documentFilePath">Full path to the output file.</param>
        public PFExcelDocument(enExcelOutputFormat excelOutputFormat, string documentFilePath)
        {
            _excelOutputFormat = excelOutputFormat;
            _documentFilePath = documentFilePath;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="excelOutputFormat">Set to enExcelOutputFormat.Excel2003 for an XLS file. Set to enExcelOutputFormat.Excel2007 for an XLSX file.</param>
        /// <param name="documentFilePath">Full path to the output file.</param>
        /// <param name="replaceExistingFile">True to overwrite existing file with same name. Otherwise, set to false to modify contents of existing file.</param>
        public PFExcelDocument(enExcelOutputFormat excelOutputFormat, string documentFilePath, bool replaceExistingFile)
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
        public PFExcelDocument(enExcelOutputFormat excelOutputFormat, string documentFilePath, string sheetName)
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
        public PFExcelDocument(enExcelOutputFormat excelOutputFormat, string documentFilePath, string sheetName, bool replaceExistingFile)
        {
            _excelOutputFormat = excelOutputFormat;
            _documentFilePath = documentFilePath;
            _sheetName = sheetName;
            _replaceExistingFile = replaceExistingFile;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="excelOutputFormat">Set to enExcelOutputFormat.Excel2003 for an XLS file. Set to enExcelOutputFormat.Excel2007 for an XLSX file.</param>
        /// <param name="documentFilePath">Full path to the output file.</param>
        /// <param name="sheetName">Name of the Excel sheet where data is to be written to or read from.</param>
        /// <param name="replaceExistingFile">True to overwrite existing file with same name. Otherwise, set to false to modify contents of existing file.</param>
        /// <param name="replaceExistingSheet">True to overwrite existing sheet with same name. Otherwise, set to false to modify contents of existing sheet.</param>
        public PFExcelDocument(enExcelOutputFormat excelOutputFormat, string documentFilePath, string sheetName, bool replaceExistingFile, bool replaceExistingSheet)
        {
            _excelOutputFormat = excelOutputFormat;
            _documentFilePath = documentFilePath;
            _sheetName = sheetName;
            _replaceExistingFile = replaceExistingFile;
            _replaceExistingSheet = replaceExistingSheet;
        }


        //properties

        /// <summary>
        /// Set to enExcelOutputFormat.Excel2003 for an XLS file.
        /// Set to enExcelOutputFormat.Excel2007 for an XLSX file.
        /// Set to enExcelOutputFormat.CSV for a CSV file.
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
        /// <remarks>Non Ext version of WriteDataToDocument will report an error if ReplaceExistingFile is false and file exists.</remarks>
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
        /// <remarks>Non Ext version of WriteDataToDocument will report an error if ReplaceExistingFile is false and file exists.</remarks>
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
        /// <remarks>Non Ext version of WriteDataToDocument will report an error if ReplaceExistingFile is false and file exists.</remarks>
        public bool WriteDataToDocument(DataTable dt)
        {
            bool success = true;
            PFExcelInterop excelDoc = null;

            try
            {
                if (this.ExcelOutputFormat == enExcelOutputFormat.Excel2007)
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.Excel2007, this.DocumentFilePath, this.SheetName, this.ReplaceExistingFile, this.ReplaceExistingSheet);
                else if (this.ExcelOutputFormat == enExcelOutputFormat.Excel2003)
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.Excel2003, this.DocumentFilePath, this.SheetName, this.ReplaceExistingFile, this.ReplaceExistingSheet);
                else
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.CSV, this.DocumentFilePath, this.SheetName, this.ReplaceExistingFile, this.ReplaceExistingSheet);

                success = excelDoc.WriteDataToDocument(dt);

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
                if(excelDoc != null)
                    excelDoc = null;
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
            PFExcelInterop excelDoc = null;

            try
            {
                if (this.ExcelOutputFormat == enExcelOutputFormat.Excel2007)
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.Excel2007, this.DocumentFilePath, this.SheetName, this.ReplaceExistingFile, this.ReplaceExistingSheet);
                else if (this.ExcelOutputFormat == enExcelOutputFormat.Excel2003)
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.Excel2003, this.DocumentFilePath, this.SheetName, this.ReplaceExistingFile, this.ReplaceExistingSheet);
                else
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.CSV, this.DocumentFilePath, this.SheetName, this.ReplaceExistingFile, this.ReplaceExistingSheet);

                success = excelDoc.AppendDataToExistingSheet(dt);



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
                if (excelDoc != null)
                    excelDoc = null;
            }

            return success;
        }

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
            PFExcelInterop excelDoc = null;

            try
            {
                if (this.ExcelOutputFormat == enExcelOutputFormat.Excel2007)
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.Excel2007, this.DocumentFilePath, this.SheetName, this.ReplaceExistingFile, this.ReplaceExistingSheet);
                else if (this.ExcelOutputFormat == enExcelOutputFormat.Excel2003)
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.Excel2003, this.DocumentFilePath, this.SheetName, this.ReplaceExistingFile, this.ReplaceExistingSheet);
                else
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.CSV, this.DocumentFilePath, this.SheetName, this.ReplaceExistingFile, this.ReplaceExistingSheet);

                success = excelDoc.WriteDataToDocumentExt(dt);


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
                if (excelDoc != null)
                    excelDoc = null;
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
            PFExcelInterop excelDoc = null;

            try
            {


                if (this.ExcelOutputFormat == enExcelOutputFormat.Excel2007)
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.Excel2007, this.DocumentFilePath, this.SheetName, this.ReplaceExistingFile, this.ReplaceExistingSheet);
                else if (this.ExcelOutputFormat == enExcelOutputFormat.Excel2003)
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.Excel2003, this.DocumentFilePath, this.SheetName, this.ReplaceExistingFile, this.ReplaceExistingSheet);
                else
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.CSV, this.DocumentFilePath, this.SheetName, this.ReplaceExistingFile, this.ReplaceExistingSheet);

                dt = excelDoc.ExportExcelDataToDataTable(excelRangeName, columnNamesInFirstRow);


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
                if (excelDoc != null)
                    excelDoc = null;
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
        /// <remarks>Row and col values are zero based. e.g. 0,0 to 99,99 for a range of 100 rows and 100 columns.</remarks>
        public DataTable ExportExcelDataToDataTable(int startRow, int startCol, int endRow, int endCol, bool columnNamesInFirstRow)
        {
            DataTable dt = null;
            PFExcelInterop excelDoc = null;

            try
            {
                if (this.ExcelOutputFormat == enExcelOutputFormat.Excel2007)
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.Excel2007, this.DocumentFilePath, this.SheetName, this.ReplaceExistingFile, this.ReplaceExistingSheet);
                else if (this.ExcelOutputFormat == enExcelOutputFormat.Excel2003)
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.Excel2003, this.DocumentFilePath, this.SheetName, this.ReplaceExistingFile, this.ReplaceExistingSheet);
                else
                    excelDoc = new PFExcelInterop(enExcelOutputFormat.CSV, this.DocumentFilePath, this.SheetName, this.ReplaceExistingFile, this.ReplaceExistingSheet);

                dt = excelDoc.ExportExcelDataToDataTable(startRow, startCol, endRow, endCol, columnNamesInFirstRow);




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
                if (excelDoc != null)
                    excelDoc = null;
            }
                 
        

            return dt;
        }



    }//end class
}//end namespace
