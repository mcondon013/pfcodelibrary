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
    /// Routines for manipulating a Word document using the GemBox Word library.
    /// Creating Word 2003 (DOC format) documents requires Sharp2Word Library, which can be obtained from Google at
    /// http://code.google.com/p/sharp2word
    /// </summary>
    public class PFWordDocument
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
        public PFWordDocument()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="wordOutputFormat">Set to enWordOutputFormat.Word2003 for an XLS file. Set to enWordOutputFormat.Word2007 for an XLSX file.</param>
        public PFWordDocument(enWordOutputFormat wordOutputFormat)
        {
            _wordOutputFormat = wordOutputFormat;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="wordOutputFormat">Set to enWordOutputFormat.Word2003 for an XLS file. Set to enWordOutputFormat.Word2007 for an XLSX file.</param>
        /// <param name="documentFilePath">Full path to the output file.</param>
        public PFWordDocument(enWordOutputFormat wordOutputFormat, string documentFilePath)
        {
            _wordOutputFormat = wordOutputFormat;
            _documentFilePath = documentFilePath;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="wordOutputFormat">Set to enWordOutputFormat.Word2003 for an XLS file. Set to enWordOutputFormat.Word2007 for an XLSX file.</param>
        /// <param name="documentFilePath">Full path to the output file.</param>
        /// <param name="replaceExistingFile">True to overwrite existing file with same name. Otherwise, set to false to throw an error if file with same name already exists.</param>
        public PFWordDocument(enWordOutputFormat wordOutputFormat, string documentFilePath, bool replaceExistingFile)
        {
            _wordOutputFormat = wordOutputFormat;
            _documentFilePath = documentFilePath;
            _replaceExistingFile = replaceExistingFile;
        }

        //properties

        /// <summary>
        /// Set to enWordOutputFormat.Word2003 for DOC format.
        /// Set to enWordOutputFormat.Word2007 for DOCX format.
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
        /// <returns></returns>
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
        /// <returns></returns>
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
            PFWordInterop wordDoc = null;

            try
            {
                if (this.WordOutputFormat == enWordOutputFormat.Word2007)
                    wordDoc = new PFWordInterop(enWordOutputFormat.Word2007, this.DocumentFilePath, this.ReplaceExistingFile);
                else
                    wordDoc = new PFWordInterop(enWordOutputFormat.Word2003, this.DocumentFilePath, this.ReplaceExistingFile);

                success = wordDoc.WriteDataToDocument(dt);


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
                ;
            }

            return success;
        }

    }//end class
}//end namespace
