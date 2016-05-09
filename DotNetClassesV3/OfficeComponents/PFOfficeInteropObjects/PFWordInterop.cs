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
using PFDocumentGlobals;

namespace PFOfficeInteropObjects
{
    /// <summary>
    /// Routines for manipulating a Word document using the Office Interop library.
    /// </summary>
    public class PFWordInterop
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private PFDataImporter _importer = new PFDataImporter();

        //private variables for properties
        private enWordOutputFormat _wordOutputFormat = enWordOutputFormat.NotSpecified;
        private string _documentFilePath = string.Empty;
        private bool _replaceExistingFile = true;

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PFWordInterop()
        {
            ;
        }

                /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="wordOutputFormat">Set to PFWordInterop.Word2003 for an XLS file. Set to PFWordInterop.Word2007 for an XLSX file.</param>
        public PFWordInterop(enWordOutputFormat wordOutputFormat)
        {
            _wordOutputFormat = wordOutputFormat;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="wordOutputFormat">Set to PFWordInterop.Word2003 for an XLS file. Set to PFWordInterop.Word2007 for an XLSX file.</param>
        /// <param name="documentFilePath">Full path to the output file.</param>
        public PFWordInterop(enWordOutputFormat wordOutputFormat, string documentFilePath)
        {
            _wordOutputFormat = wordOutputFormat;
            _documentFilePath = documentFilePath;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="wordOutputFormat">Set to PFWordInterop.Word2003 for an XLS file. Set to PFWordInterop.Word2007 for an XLSX file.</param>
        /// <param name="documentFilePath">Full path to the output file.</param>
        /// <param name="replaceExistingFile">True to overwrite existing file with same name. Otherwise, set to false to throw an error if file with same name already exists.</param>
        public PFWordInterop(enWordOutputFormat wordOutputFormat, string documentFilePath, bool replaceExistingFile)
        {
            _wordOutputFormat = wordOutputFormat;
            _documentFilePath = documentFilePath;
            _replaceExistingFile = replaceExistingFile;
        }

        //properties

        /// <summary>
        /// Set to enWordVersion.Word2003 for DOC format.
        /// Set to enWordVersion.Word2007 for DOCX format.
        /// </summary>
        public enWordOutputFormat WordOutputFormat
        {
            get
            {
                return _wordOutputFormat;
            }
            set
            {
                _wordOutputFormat = value;
            }
        }

        /// <summary>
        /// Specifies path to the word document on disk.
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
            object missing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Application wordApp = null;
            Microsoft.Office.Interop.Word.Document wordDoc = null;
            int numCols = 0;
            int numRows = 0;
            int tblRows = 0;
            int curTblRow = 0;

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
                        _msg.Append("Word document file exists and ReplaceExistingFile set to False. Write to Word document has failed.");
                        throw new System.Exception(_msg.ToString());
                    }
                }

                numCols = dt.Columns.Count;
                numRows = dt.Rows.Count;
                tblRows = numRows + 1;

                wordApp = new Microsoft.Office.Interop.Word.Application();
                wordApp.Visible = false;

                Microsoft.Office.Interop.Word.WdSaveFormat fileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument;
                switch (this.WordOutputFormat)
                {
                    case enWordOutputFormat.Word2007:
                        fileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatXMLDocument;
                        break;
                    case enWordOutputFormat.Word2003:
                        fileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument;
                        break;
                    case enWordOutputFormat.RTF:
                        fileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatRTF;
                        break;
                    case enWordOutputFormat.PDF:
                        fileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;
                        break;
                    default:
                        fileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument;
                        break;
                }

                wordDoc = wordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                wordDoc.Content.SetRange(0, 0);
                Microsoft.Office.Interop.Word.Paragraph para1 = wordDoc.Content.Paragraphs.Add(ref missing);                
                Microsoft.Office.Interop.Word.Table tbl = wordDoc.Tables.Add(para1.Range, tblRows, numCols, ref missing, ref missing);

                curTblRow = 1;
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    tbl.Rows[curTblRow].Cells[c+1].Range.Text = dt.Columns[c].ColumnName;
                }

                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    curTblRow++;
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        tbl.Rows[curTblRow].Cells[c + 1].Range.Text = dt.Rows[r][c].ToString();
                    }
                }


                tbl.AllowAutoFit = true;
                tbl.AutoFitBehavior(Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow);
                wordDoc.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientLandscape;
                wordDoc.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaper11x17;

                wordDoc.SaveAs2(this.DocumentFilePath, fileFormat, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                //wordDoc.Save();
                //wordDoc.Close(false);
                //wordApp.Quit(false);
                //((Microsoft.Office.Interop.Word._Document)wordDoc).Close(ref nullobject, ref nullobject, ref nullobject);
                //((Microsoft.Office.Interop.Word._Application)wordApp).Quit(ref nullobject, ref nullobject, ref nullobject);
                ((Microsoft.Office.Interop.Word._Document)wordDoc).Close(null, null, null);
                ((Microsoft.Office.Interop.Word._Application)wordApp).Quit(null, null, null);

            }
            catch (System.Exception ex)
            {
                success = false;
                _msg.Length = 0;
                _msg.Append("Attempt to import DataTable into Word document failed.");
                _msg.Append(Environment.NewLine);
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                if (wordApp != null)
                    wordApp = null;
            }

            return success;
        }



    }//end class
}//end namespace
