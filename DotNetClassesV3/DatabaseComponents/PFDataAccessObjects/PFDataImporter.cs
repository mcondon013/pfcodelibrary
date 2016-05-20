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
using System.Data.Common;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using PFTextFiles;

namespace PFDataAccessObjects
{
    /// <summary>
    /// Class to manage the importing of XML and Text objects into ADO.NET objects.
    /// </summary>
    public class PFDataImporter
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFDataImporter()
        {
            ;
        }

        //properties

        //methods

        #region Import From XML and Text

        /// <summary>
        /// Converts an XML formatted document into an ADO.NET DataSet.
        /// </summary>
        /// <param name="xmlDoc">XML document to be converted to a DataSet.</param>
        /// <returns>ADO.NET DataSet.</returns>
        public DataSet ImportXmlDocumentToDataSet(XmlDocument xmlDoc)
        {
            DataSet ds = new DataSet();

            ds.ReadXml(new XmlNodeReader(xmlDoc));

            return ds;
        }

        /// <summary>
        /// Converts an XML formatted document into an ADO.NET DataSet.
        /// </summary>
        /// <param name="xmlFile">Path to file containing XML document to be converted to a DataSet.</param>
        /// <returns>ADO.NET DataSet.</returns>
        public DataSet ImportXmlFileToDataSet(string xmlFile)
        {
            DataSet ds = new DataSet();

            ds.ReadXml(xmlFile);

            return ds;
        }

        /// <summary>
        /// Converts an XML formatted document into an ADO.NET DataSet.
        /// </summary>
        /// <param name="xmlFile">Path to file containing XML document to be converted to a DataSet.</param>
        /// <param name="xsdFile">Path to XML Schema Definition file to be used.</param>
        /// <returns>ADO.NET DataSet.</returns>
        public DataSet ImportXmlFileToDataSet(string xmlFile, string xsdFile)
        {
            DataSet ds = new DataSet();

            ds.ReadXmlSchema(xsdFile);
            ds.ReadXml(xmlFile);

            return ds;
        }

        /// <summary>
        /// Converts an XML formatted document into an ADO.NET DataTable.
        /// </summary>
        /// <param name="xmlDoc">XML document to be converted to a DataTable.</param>
        /// <returns>ADO.NET DataTable.</returns>
        /// <remarks>Input document must have a schema associated with it. Use Import to DataSet if input has no schema.</remarks>
        public DataTable ImportXmlDocumentToDataTable(XmlDocument xmlDoc)
        {
            DataSet ds = new DataSet();

            ds.ReadXml(new XmlNodeReader(xmlDoc));

            return ds.Tables[0];
        }

        /// <summary>
        /// Converts an XML formatted document into an ADO.NET DataTable.
        /// </summary>
        /// <param name="xmlFile">Path to XML file containing XML document to convert.</param>
        /// <returns>ADO.NET DataTable.</returns>
        /// <remarks>Schema must be included in the XML file. Use Import to DataSet if input has no schema.</remarks>
        public DataTable ImportXmlFileToDataTable(string xmlFile)
        {
            DataSet ds = new DataSet();

            ds.ReadXml(xmlFile);

            return ds.Tables[0];
        }

        /// <summary>
        /// Converts an XML formatted document into an ADO.NET DataTable.
        /// </summary>
        /// <param name="xmlFile">Path to XML file containing XML document to convert.</param>
        /// <param name="xsdFile">Path to XML Schema Definition file to be used.</param>
        /// <returns>ADO.NET DataTable.</returns>
        /// <remarks>Schema must be included in the XML file. Use Import to DataSet if input has no schema.</remarks>
        public DataTable ImportXmlFileToDataTable(string xmlFile, string xsdFile)
        {
            DataSet ds = new DataSet();

            ds.ReadXmlSchema(xsdFile);
            ds.ReadXml(xmlFile);

            return ds.Tables[0];
        }

        /// <summary>
        /// Converts an XML formatted document schema into an ADO.NET DataTable.
        /// </summary>
        /// <param name="file">Path to file containing XML document schema.</param>
        /// <returns>ADO.NET DataTable.</returns>
        public DataTable ImportXmlSchemaToDataTable(string file)
        {
            DataSet ds = new DataSet();

            ds.ReadXmlSchema(file);

            return ds.Tables[0];
        }


        /// <summary>
        /// Routine to copy data from a delimited data text file to a table in a dataset.
        /// </summary>
        /// <param name="dataFileName">Path to file containing the data.</param>
        /// <param name="lineDef">PFDelimitedDataLine object containing the line formatting information for the specified data file.</param>
        /// <returns>ADO.NET DataSet.</returns>
        public DataSet ImportDelimitedTextFileToDataSet(string dataFileName, PFDelimitedDataLine lineDef)
        {
            DataSet ds = new DataSet();

            DataTable dt = ImportDelimitedTextFileToDataTable(dataFileName, lineDef);

            ds.Tables.Add(dt);

            return ds;
        }

        /// <summary>
        /// Routine to copy data from a delimited data text file to a table in a dataset.
        /// </summary>
        /// <param name="lineDefFileName">Path to file containing the PFDelimitedDataLine object that encapsulates the line formatting information for the specified data file.</param>
        /// <param name="dataFileName">Path to file containing the data.</param>
        /// <returns>ADO.NET DataSet.</returns>
        public DataSet ImportDelimitedTextFileToDataSet(string dataFileName, string lineDefFileName)
        {
            PFDelimitedDataLine lineDef = PFDelimitedDataLine.LoadFromXmlFile(lineDefFileName);

            return ImportDelimitedTextFileToDataSet(dataFileName, lineDef);
        }

        /// <summary>
        /// Routine to copy data from a delimited data text file to an ADO.NET DataTable object.
        /// </summary>
        /// <param name="dataFileName">Path to file containing the data.</param>
        /// <param name="lineDef">PFDelimitedDataLine object containing the line formatting information for the specified data file.</param>
        /// <returns>ADO.NET DataTable.</returns>
        public DataTable ImportDelimitedTextFileToDataTable(string dataFileName, PFDelimitedDataLine lineDef)
        {
            PFTextFile textFile = new PFTextFile(dataFileName, PFFileOpenOperation.OpenFileToRead);
            DataTable dt = new DataTable();
            int numColumns = -1;
            string line = string.Empty;

            numColumns = lineDef.ColumnDefinitions.NumberOfColumns;
            for (int i = 0; i < numColumns; i++)
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = lineDef.ColumnDefinitions.ColumnDefinition[i].ColumnName;
                dc.DataType = System.Type.GetType("System.String");
                dc.MaxLength = lineDef.ColumnDefinitions.ColumnDefinition[i].ColumnLength;
                dt.Columns.Add(dc);
            }//end for

            //ignore first line if it contains the column names
            if (lineDef.ColumnNamesOnFirstLine)
            {
                if (textFile.Peek() >= 0)
                {
                    line = textFile.ReadLine();
                }
            }
            //read the data lines
            while (textFile.Peek() >= 0)
            {
                line = textFile.ReadLine();
                lineDef.ParseData(line);
                DataRow dr = dt.NewRow();
                for (int i = 0; i < numColumns; i++)
                {
                    dr[i] = lineDef.ColumnData.ColumnDataValue[i].Data;
                }//end for
                dt.Rows.Add(dr);
            }//end while

            return dt;
        }

        /// <summary>
        /// Routine to copy data from a delimited data text file to an ADO.NET DataTable object.
        /// </summary>
        /// <param name="dataFileName">Path to file containing the data.</param>
        /// <param name="lineDefFileName">Path to file containing the PFDelimitedDataLine object that encapsulates the line formatting information for the specified data file.</param>
        /// <returns>ADO.NET DataTable.</returns>
        public DataTable ImportDelimitedTextFileToDataTable(string dataFileName, string lineDefFileName)
        {
            PFDelimitedDataLine lineDef = PFDelimitedDataLine.LoadFromXmlFile(lineDefFileName);

            return ImportDelimitedTextFileToDataTable(dataFileName, lineDef);
        }

        /// <summary>
        /// Routine to parse the first line of a delimited text file and infer the file's column definitions.
        /// </summary>
        /// <param name="textFilePath">Path to file containing delimited data.</param>
        /// <param name="columnDelimiter">Delimiter character used to separate column data.</param>
        /// <param name="lineTerminator">Delimiter that marks the end of a line of data.</param>
        /// <param name="columnNamesOnFirstLine">If true, first line of file contains column names. Otherwise, file does not contain column names.</param>
        /// <returns>Object representing class used to define and parse a delimited line of text.</returns>
        public PFDelimitedDataLine CreateDelimitedLineDefinitionFromTextFile(string textFilePath, string columnDelimiter, string lineTerminator, bool columnNamesOnFirstLine)
        {
            return CreateDelimitedLineDefinitionFromTextFile(textFilePath, columnDelimiter, lineTerminator, columnNamesOnFirstLine, false);
        }

        /// <summary>
        /// Routine to parse the first line of a delimited text file and infer the file's column definitions.
        /// </summary>
        /// <param name="textFilePath">Path to file containing delimited data.</param>
        /// <param name="columnDelimiter">Delimiter character used to separate column data.</param>
        /// <param name="lineTerminator">Delimiter that marks the end of a line of data.</param>
        /// <param name="columnNamesOnFirstLine">If true, first line of file contains column names. Otherwise, file does not contain column names.</param>
        /// <param name="stringValuesSurroundedWithQuotationMarks">If true, values in file are surrounded by double quotes.</param>
        /// <returns>Object representing class used to define and parse a delimited line of text.</returns>
        public PFDelimitedDataLine CreateDelimitedLineDefinitionFromTextFile(string textFilePath, string columnDelimiter, string lineTerminator, bool columnNamesOnFirstLine, bool stringValuesSurroundedWithQuotationMarks)
        {
            PFDelimitedDataLine delimitedLine = null;
            PFTextFile textFile = new PFTextFile(textFilePath, PFFileOpenOperation.OpenFileToRead);
            string line = string.Empty;
            string[] cols = null;
            string[] seps = null;
            string colName = string.Empty;

            try
            {
                //read first line of file
                if (textFile.Peek() >= 0)
                {
                    line = textFile.ReadLine();
                }

                if (line == string.Empty)
                {
                    _msg.Length = 0;
                    _msg.Append("File is empty: ");
                    _msg.Append(textFilePath);
                    _msg.Append(". Unable to create delimited file column definitions.");
                    throw new System.Exception(_msg.ToString());
                }

                seps = new string[1] {columnDelimiter};
                cols = line.Split(seps, StringSplitOptions.None);

                delimitedLine = new PFDelimitedDataLine(cols.Length);

                delimitedLine.NumberOfColumns = cols.Length;
                delimitedLine.ColumnDefinitions.NumberOfColumns = cols.Length;
                delimitedLine.LineTerminator = lineTerminator;
                delimitedLine.ColumnSeparator = columnDelimiter;
                delimitedLine.ColumnNamesOnFirstLine = columnNamesOnFirstLine;
                delimitedLine.StringValuesSurroundedWithQuotationMarks = stringValuesSurroundedWithQuotationMarks;

                for (int i = 0; i < cols.Length; i++)
                {
                    if (columnNamesOnFirstLine)
                        delimitedLine.SetColumnDefinition(i, cols[i], int.MaxValue);
                    else
                        delimitedLine.SetColumnDefinition(i, "Col" + (i+1).ToString(), int.MaxValue);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(ex.Message);
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                if(textFile.FileIsOpen)
                    textFile.CloseFile();
            }
                 
        
            return delimitedLine;
        }

        /// <summary>
        /// Routine to get DataTable containing schema information only for a delimited text file.
        /// </summary>
        /// <param name="textFilePath">Path to delimited text file.</param>
        /// <param name="columnDelimiter">Value used to separate columns.</param>
        /// <param name="lineTerminator">Value used to indicate end of line.</param>
        /// <param name="columnNamesOnFirstLine">If true, first line of file contains column names. Otherwise, file does not contain column names.</param>
        /// <returns>DataTable containing schema.</returns>
        public DataTable GetDelimitedTextFileSchemaTable(string textFilePath, string columnDelimiter, string lineTerminator, bool columnNamesOnFirstLine)
        {
            DataTable dt = new DataTable();

            PFDelimitedDataLine delimitedLine = CreateDelimitedLineDefinitionFromTextFile(textFilePath, columnDelimiter, lineTerminator, columnNamesOnFirstLine);

            if (delimitedLine.ColumnDefinitions.NumberOfColumns < 1)
            {
                _msg.Length = 0;
                _msg.Append("No column data found in ");
                _msg.Append(textFilePath);
                _msg.Append(". Unable to generate delimited text file schema table.");
                throw new System.Exception(_msg.ToString());
            }

            for (int i = 0; i < delimitedLine.ColumnDefinitions.ColumnDefinition.Length; i++)
            {
                DataColumn dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = delimitedLine.ColumnDefinitions.ColumnDefinition[i].ColumnName;
                dc.MaxLength = int.MaxValue;
                dt.Columns.Add(dc);
            }

            return dt;
        }


        /// <summary>
        /// Routine to copy data from a delimited data text file to a database table.
        /// </summary>
        /// <param name="dataFileName">Path to file containing the data.</param>
        /// <param name="lineDefFileName">Path to file containing the PFDelimitedDataLine object that encapsulates the line formatting information for the specified data file.</param>
        /// <param name="db">Database object for the target database.</param>
        /// <param name="tableName">Name of table in database to which data is to be copied.</param>
        /// <param name="updateBatchSize">Number of individual SQL modification statements to include in a table modification operation.</param>
        /// <returns>Number of rows uploaded to the database.</returns>
        public int ImportDelimitedTextFileToDatabase(string dataFileName, string lineDefFileName, PFDatabase db, string tableName, int updateBatchSize)
        {
            DataTable dt = ImportDelimitedTextFileToDataTable(dataFileName, lineDefFileName);

            dt.TableName = tableName;

            db.ImportDataFromDataTable(dt, updateBatchSize);

            return dt.Rows.Count;
        }


        /// <summary>
        /// Routine to copy data from a fixed length data text file to a table in a dataset.
        /// </summary>
        /// <param name="dataFileName">Path to file containing the data.</param>
        /// <param name="lineDef">PFFixedLengthDataLine object containing the line formatting information for the specified data file.</param>
        /// <returns>ADO.NET DataSet.</returns>
        public DataSet ImportFixedLengthTextFileToDataSet(string dataFileName, PFFixedLengthDataLine lineDef)
        {
            DataSet ds = new DataSet();

            DataTable dt = ImportFixedLengthTextFileToDataTable(dataFileName, lineDef);

            ds.Tables.Add(dt);

            return ds;
        }

        /// <summary>
        /// Routine to copy data from a fixed length data text file to a table in a dataset.
        /// </summary>
        /// <param name="lineDefFileName">Path to file containing the PFFixedLengthDataLine object that encapsulates the line formatting information for the specified data file.</param>
        /// <param name="dataFileName">Path to file containing the data.</param>
        /// <returns>ADO.NET DataSet.</returns>
        public DataSet ImportFixedLengthTextFileToDataSet(string dataFileName, string lineDefFileName)
        {
            PFFixedLengthDataLine lineDef = PFFixedLengthDataLine.LoadFromXmlFile(lineDefFileName);

            return ImportFixedLengthTextFileToDataSet(dataFileName, lineDef);
        }

        
        /// <summary>
        /// Routine to copy data from a fixed length data text file to an ADO.NET DataTable object.
        /// </summary>
        /// <param name="dataFileName">Path to file containing the data.</param>
        /// <param name="lineDef">PFFixedLengthDataLine object containing the line formatting information for the specified data file.</param>
        /// <returns>ADO.NET DataTable.</returns>
        public DataTable ImportFixedLengthTextFileToDataTable(string dataFileName, PFFixedLengthDataLine lineDef)
        {
            PFTextFile textFile = new PFTextFile(dataFileName, PFFileOpenOperation.OpenFileToRead);
            DataTable dt = new DataTable();
            int numColumns = -1;
            string line = string.Empty;


            numColumns = lineDef.ColumnDefinitions.NumberOfColumns;
            for (int i = 0; i < numColumns; i++)
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = lineDef.ColumnDefinitions.ColumnDefinition[i].ColumnName;
                dc.DataType = System.Type.GetType("System.String");
                dc.MaxLength = lineDef.ColumnDefinitions.ColumnDefinition[i].ColumnLength;
                dt.Columns.Add(dc);
            }//end for

            //ignore first line if it contains the column names
            if (lineDef.ColumnNamesOnFirstLine)
            {
                if (textFile.Peek() >= 0)
                {
                    line = textFile.ReadData(lineDef.LineLength);
                }
            }
            //read the data lines
            while (textFile.Peek() >= 0)
            {
                line = textFile.ReadData(lineDef.LineLength);
                lineDef.ParseData(line);
                DataRow dr = dt.NewRow();
                for (int i = 0; i < numColumns; i++)
                {
                    dr[i] = lineDef.ColumnData.ColumnDataValue[i].Data;
                }//end for
                dt.Rows.Add(dr);
            }//end while

            return dt;
        }

        /// <summary>
        /// Routine to copy data from a fixed length data text file to an ADO.NET DataTable object.
        /// </summary>
        /// <param name="dataFileName">Path to file containing the data.</param>
        /// <param name="lineDefFileName">Path to file containing the PFFixedLengthDataLine object that encapsulates the line formatting information for the specified data file.</param>
        /// <returns>ADO.NET DataTable.</returns>
        public DataTable ImportFixedLengthTextFileToDataTable(string dataFileName, string lineDefFileName)
        {
            PFFixedLengthDataLine lineDef = PFFixedLengthDataLine.LoadFromXmlFile(lineDefFileName);

            return ImportFixedLengthTextFileToDataTable(dataFileName, lineDef);
        }


        /// <summary>
        /// Routine to copy data from a fixed length data text file to a database table.
        /// </summary>
        /// <param name="dataFileName">Path to file containing the data.</param>
        /// <param name="lineDefFileName">Path to file containing the PFFixedLengthDataLine object that encapsulates the line formatting information for the specified data file.</param>
        /// <param name="db">Database object for the target database.</param>
        /// <param name="tableName">Name of table in database to which data is to be copied.</param>
        /// <param name="updateBatchSize">Number of individual SQL modification statements to include in a table modification operation.</param>
        /// <returns>Number of rows uploaded to the database.</returns>
        public int ImportFixedLengthTextFileToDatabase(string dataFileName, string lineDefFileName, PFDatabase db, string tableName, int updateBatchSize)
        {
            DataTable dt = ImportFixedLengthTextFileToDataTable(dataFileName, lineDefFileName);

            dt.TableName = tableName;

            db.ImportDataFromDataTable(dt, updateBatchSize);

            return dt.Rows.Count;
        }

        /// <summary>
        /// Routine to get DataTable containing schema information only for a delimited text file.
        /// </summary>
        /// <param name="fxlDataLine">Object containing definition of data line for a fixed length text file.</param>
        /// <param name="textFilePath">Path to delimited text file.</param>
        /// <returns>DataTable containing schema.</returns>
        public DataTable GetFixedLengthTextFileSchemaTable(PFFixedLengthDataLine fxlDataLine, string textFilePath)
        {
            DataTable dt = new DataTable();

            if (fxlDataLine.ColumnDefinitions.NumberOfColumns < 1)
            {
                _msg.Length = 0;
                _msg.Append("No column data found in ");
                _msg.Append(textFilePath);
                _msg.Append(". Unable to generate fixed length text file schema table.");
                throw new System.Exception(_msg.ToString());
            }

            for (int i = 0; i < fxlDataLine.ColumnDefinitions.ColumnDefinition.Length; i++)
            {
                DataColumn dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = fxlDataLine.ColumnDefinitions.ColumnDefinition[i].ColumnName;
                dc.MaxLength = int.MaxValue;
                dt.Columns.Add(dc);
            }

            return dt;
        }


        #endregion


    }//end class
}//end namespace
