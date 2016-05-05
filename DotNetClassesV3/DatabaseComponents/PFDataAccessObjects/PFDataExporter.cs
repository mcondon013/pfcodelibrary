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
using System.Data.Common;
using System.IO;
using PFTextFiles;

namespace PFDataAccessObjects
{
    /// <summary>
    /// Class to manage the export of the contents of ADO.NET DataSet and DataTable objects to external files.
    /// </summary>
    public class PFDataExporter
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();

        //private varialbles for properties
        private int _maxColumnLengthOverride = 1024;
        private int _defaultStringColumnLength = 255;

        //events
#pragma warning disable 1591
        public delegate void ResultAsStringDelegate(string outputLine, int tableNumber);
        public event ResultAsStringDelegate returnResultAsString;
#pragma warning restore 1591

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PFDataExporter()
        {
            ;
        }

        //properties
        /// <summary>
        /// Use this property to specify a maximum length for any fixed length column. This will override the maximum length contained in the data object if that length is greater than this override length.
        /// </summary>
        public int MaxColumnLengthOverride
        {
            get
            {
                return _maxColumnLengthOverride;
            }
            set
            {
                _maxColumnLengthOverride = value;
            }
        }

        /// <summary>
        /// Use this property to specify a length for any string column that has its length defined as less than 1.
        /// </summary>
        /// <remarks>This property only applies in cases where the length for a string in a data table is -1 and the calling routine has not overriden the -1 when creating the column definitions.</remarks>
        public int DefaultStringColumnLength
        {
            get
            {
                return _defaultStringColumnLength;
            }
            set
            {
                _defaultStringColumnLength = value;
            }
        }


        //methods

        #region delimited data extract routines

        /// <summary>
        /// Extracts data from DataSet into delimited text format. 
        /// </summary>
        /// <param name="ds">DataSet containing the data.</param>
        /// <param name="columnSeparator">String containing one or more characters that will separate columns in the line of data that is extracted.</param>
                                                  /// <param name="lineTerminator">String containing one or more characters that will denote the end of a line of data.</param>
        /// <param name="columnNamesOnFirstLine">If true, a line containing the column names in delimited format will be the first line returned.</param>
        /// <remarks>If the DataSet contains more than one table, the data from all the tables will be concatenated into one data extract.</remarks>
        public void ExtractDelimitedDataFromDataSet(DataSet ds,
                                                    string columnSeparator,
                                                    string lineTerminator,
                                                    bool columnNamesOnFirstLine)
        {
            int tabInx = 0;
            int maxTabInx = ds.Tables.Count - 1;

            for (tabInx = 0; tabInx <= maxTabInx; tabInx++)
            {
                ExtractDelimitedDataFromTable(ds.Tables[tabInx], columnSeparator, lineTerminator, columnNamesOnFirstLine);
            }
        }

        /// <summary>
        /// Extracts data from DataTable into delimited text format. 
        /// </summary>
        /// <param name="tab">DataTable containing the data.</param>
        /// <param name="columnSeparator">String containing one or more characters that will separate columns in the line of data that is extracted.</param>
        /// <param name="lineTerminator">String containing one or more characters that will denote the end of a line of data.</param>
        /// <param name="columnNamesOnFirstLine">If true, a line containing the column names in delimited format will be the first line returned.</param>
        public void ExtractDelimitedDataFromTable(DataTable tab,
                                                  string columnSeparator,
                                                  string lineTerminator,
                                                  bool columnNamesOnFirstLine)
        {
            ExtractDelimitedDataFromTable(tab, columnSeparator, lineTerminator, columnNamesOnFirstLine, (int)1, false);
        }

        /// <summary>
        /// Extracts data from DataTable into delimited text format. 
        /// </summary>
        /// <param name="tab">DataTable containing the data.</param>
        /// <param name="columnSeparator">String containing one or more characters that will separate columns in the line of data that is extracted.</param>
        /// <param name="lineTerminator">String containing one or more characters that will denote the end of a line of data.</param>
        /// <param name="columnNamesOnFirstLine">If true, a line containing the column names in delimited format will be the first line returned.</param>
        /// <param name="tableNumber">Arbitrary number used to identify the table.</param>
        public void ExtractDelimitedDataFromTable(DataTable tab,
                                                   string columnSeparator,
                                                   string lineTerminator,
                                                   bool columnNamesOnFirstLine,
                                                   int tableNumber)
        {
            ExtractDelimitedDataFromTable(tab, columnSeparator, lineTerminator, columnNamesOnFirstLine, tableNumber, false);
        }
        
        /// <summary>
        /// Extracts data from DataTable into delimited text format. 
        /// </summary>
        /// <param name="tab">DataTable containing the data.</param>
        /// <param name="columnSeparator">String containing one or more characters that will separate columns in the line of data that is extracted.</param>
        /// <param name="lineTerminator">String containing one or more characters that will denote the end of a line of data.</param>
        /// <param name="columnNamesOnFirstLine">If true, a line containing the column names in delimited format will be the first line returned.</param>
        /// <param name="tableNumber">Arbitrary number used to identify the table.</param>
        /// <param name="stringValuesSurroundedWithQuotationMarks">If true, string values will be surrounded by double quotes.</param>
        public void ExtractDelimitedDataFromTable(DataTable tab,
                                                   string columnSeparator,
                                                   string lineTerminator,
                                                   bool columnNamesOnFirstLine,
                                                   int tableNumber,
                                                   bool stringValuesSurroundedWithQuotationMarks)
        {
            int rowInx = 0;
            int maxRowInx = -1;
            int colInx = 0;
            int maxColInx = -1;
            DataColumnCollection cols = tab.Columns;
            PFDelimitedDataLine delimitedLine = new PFDelimitedDataLine(cols.Count);
            bool[] surroundWithQuotationMarks = new bool[tab.Columns.Count];

            maxRowInx = tab.Rows.Count - 1;
            maxColInx = tab.Columns.Count - 1;

            delimitedLine.ColumnSeparator = columnSeparator;
            delimitedLine.LineTerminator = lineTerminator;
            delimitedLine.StringValuesSurroundedWithQuotationMarks = stringValuesSurroundedWithQuotationMarks;

            for (colInx = 0; colInx <= maxColInx; colInx++)
            {
                delimitedLine.SetColumnDefinition(colInx, cols[colInx].ColumnName);
                if (cols[colInx].DataType == typeof(System.String) && stringValuesSurroundedWithQuotationMarks)
                    surroundWithQuotationMarks[colInx] = true;
                else
                    surroundWithQuotationMarks[colInx] = false;
            }

            if (columnNamesOnFirstLine == true)
            {
                if (returnResultAsString != null)
                {
                    returnResultAsString(delimitedLine.OutputColumnNames(), tableNumber);
                }
            }

            for (rowInx = 0; rowInx <= maxRowInx; rowInx++)
            {
                DataRow row = tab.Rows[rowInx];
                if (returnResultAsString != null)
                {
                    for (colInx = 0; colInx <= maxColInx; colInx++)
                    {
                        if(stringValuesSurroundedWithQuotationMarks)
                            delimitedLine.StringValuesSurroundedWithQuotationMarks = surroundWithQuotationMarks[colInx];
                        delimitedLine.SetColumnData(colInx, row[colInx].ToString());
                    }
                    returnResultAsString(delimitedLine.OutputColumnData(), tableNumber);
                }
            }

        }//end ExtractDelimitedtDataFromTable method

        /// <summary>
        /// Retrieves object containing line delimiters and column names for a table of delimited text data.
        /// </summary>
        /// <param name="tab">DataTable containing the data.</param>
        /// <param name="columnSeparator">String containing one or more characters that will separate columns in the line of data that is extracted.</param>
        /// <param name="lineTerminator">String containing one or more characters that will denote the end of a line of data.</param>
        /// <param name="columnNamesOnFirstLine">If true, a line containing the column names in delimited format will be the first line returned.</param>
        /// <returns>PFDelimitedDataLine object that contains definition of delimited text format for specified DataTable.</returns>
        public PFDelimitedDataLine GetDelimitedLineDefinitionFromTable(DataTable tab,
                                                                       string columnSeparator,
                                                                       string lineTerminator,
                                                                       bool columnNamesOnFirstLine)
        {
            return GetDelimitedLineDefinitionFromTable(tab, columnSeparator, lineTerminator, columnNamesOnFirstLine, (int)1);
        }

        /// <summary>
        /// Retrieves object containing line delimiters and column names for a table of delimited text data.
        /// </summary>
        /// <param name="tab">DataTable containing the data.</param>
        /// <param name="columnSeparator">String containing one or more characters that will separate columns in the line of data that is extracted.</param>
        /// <param name="lineTerminator">String containing one or more characters that will denote the end of a line of data.</param>
        /// <param name="columnNamesOnFirstLine">If true, a line containing the column names in delimited format will be the first line returned.</param>
        /// <param name="tableNumber">Arbitrary number used to identify the table.</param>
        /// <returns>PFDelimitedDataLine object that contains definition of delimited text format for specified DataTable.</returns>
        public PFDelimitedDataLine GetDelimitedLineDefinitionFromTable(DataTable tab,
                                                                       string columnSeparator,
                                                                       string lineTerminator,
                                                                       bool columnNamesOnFirstLine,
                                                                       int tableNumber)
        {
            int colInx = 0;
            int maxColInx = -1;
            DataColumnCollection cols = tab.Columns;
            PFDelimitedDataLine delimitedLineDefinition = new PFDelimitedDataLine(cols.Count);

            maxColInx = tab.Columns.Count - 1;

            delimitedLineDefinition.ColumnSeparator = columnSeparator;
            delimitedLineDefinition.LineTerminator = lineTerminator;
            delimitedLineDefinition.ColumnNamesOnFirstLine = columnNamesOnFirstLine;

            for (colInx = 0; colInx <= maxColInx; colInx++)
            {
                delimitedLineDefinition.SetColumnDefinition(colInx, cols[colInx].ColumnName);
            }

            if (columnNamesOnFirstLine == true)
            {
                if (returnResultAsString != null)
                {
                    returnResultAsString(delimitedLineDefinition.OutputColumnNames(), tableNumber);
                }
            }

            return delimitedLineDefinition;

        }

        #endregion

        #region fixed length data extract routines
        /// <summary>
        /// Extracts data from DataSet into fixed length text format. 
        /// </summary>
        /// <param name="ds">DataSet containing the data.</param>
        /// <param name="useLineTerminator">String containing one or more characters that will denote the end of a line of data.</param>
        /// <param name="columnNamesOnFirstLine">If true, a line containing the column names in fixed length format will be the first line returned.</param>
        /// <param name="allowDataTruncation">If true, data longer than defined column length will be truncated; otherwise an exception will be thrown.</param>
        /// <remarks>If the DataSet contains more than one table, the data from all the tables will be concatenated into one data extract.</remarks>
        public void ExtractFixedLengthDataFromDataSet(DataSet ds,
                                                    bool useLineTerminator,
                                                    bool columnNamesOnFirstLine,
                                                    bool allowDataTruncation)
        {
            int tabInx = 0;
            int maxTabInx = ds.Tables.Count - 1;

            for (tabInx = 0; tabInx <= maxTabInx; tabInx++)
            {
                ExtractFixedLengthDataFromTable(ds.Tables[tabInx], useLineTerminator, columnNamesOnFirstLine, allowDataTruncation, tabInx, Environment.NewLine);
            }
        }

        /// <summary>
        /// Extracts data from DataTable into fixed text format. 
        /// </summary>
        /// <param name="tab">DataTable containing the data.</param>
        /// <param name="useLineTerminator">String containing one or more characters that will denote the end of a line of data.</param>
        /// <param name="columnNamesOnFirstLine">If true, a line containing the column names in fixed length format will be the first line returned.</param>
        /// <param name="allowDataTruncation">If true, data longer than defined column length will be truncated; otherwise an exception will be thrown.</param>
        public void ExtractFixedLengthDataFromTable(DataTable tab,
                                                  bool useLineTerminator,
                                                  bool columnNamesOnFirstLine,
                                                  bool allowDataTruncation)
        {
            ExtractFixedLengthDataFromTable(tab, useLineTerminator, columnNamesOnFirstLine, allowDataTruncation, (int)1, Environment.NewLine);
        }

        /// <summary>
        /// Extracts data from DataTable into fixed length text format. 
        /// </summary>
        /// <param name="tab">DataTable containing the data.</param>
        /// <param name="useLineTerminator">String containing one or more characters that will denote the end of a line of data.</param>
        /// <param name="columnNamesOnFirstLine">If true, a line containing the column names in delimited format will be the first line returned.</param>
        /// <param name="allowDataTruncation">If true, data longer than defined column length will be truncated; otherwise an exception will be thrown.</param>
        /// <param name="tableNumber">Arbitrary number used to identify the table.</param>
        /// <param name="lineTerminatorChars">Default is CR/LF for characters to mark end of line. You can override the default by setting this parameter.</param>
        private void ExtractFixedLengthDataFromTable(DataTable tab,
                                                     bool useLineTerminator,
                                                     bool columnNamesOnFirstLine,
                                                     bool allowDataTruncation,
                                                     int tableNumber,
                                                     string lineTerminatorChars)
        {
            int rowInx = 0;
            int maxRowInx = -1;
            int colInx = 0;
            int maxColInx = -1;
            int colLen = -1;
            DataColumnCollection cols = tab.Columns;
            PFFixedLengthDataLine  fixedLengthLine = new PFFixedLengthDataLine(cols.Count);

            maxRowInx = tab.Rows.Count - 1;
            maxColInx = tab.Columns.Count - 1;

            fixedLengthLine.UseLineTerminator = useLineTerminator;
            fixedLengthLine.LineTerminator = lineTerminatorChars;
            fixedLengthLine.ColumnNamesOnFirstLine = columnNamesOnFirstLine;
            fixedLengthLine.AllowDataTruncation = allowDataTruncation;
            fixedLengthLine.MaxColumnLengthOverride = this.MaxColumnLengthOverride;
            fixedLengthLine.DefaultStringColumnLength = this.DefaultStringColumnLength;

            for (colInx = 0; colInx <= maxColInx; colInx++)
            {
                if (PFFixedLengthDataLine.DataTypeIsNumeric(cols[colInx].DataType))
                {
                    colLen = PFFixedLengthDataLine.GetNumericTypeMaxExtractLength(cols[colInx].DataType);
                }
                else if (PFFixedLengthDataLine.DataTypeIsDateTime(cols[colInx].DataType))
                {
                    colLen = PFFixedLengthDataLine.GetDateTimeTypeMaxExtractLength(cols[colInx].DataType);
                }
                else
                {
                    colLen = cols[colInx].MaxLength;
                }
                fixedLengthLine.SetColumnDefinition(colInx, cols[colInx].ColumnName,colLen);
            }

            if (columnNamesOnFirstLine == true)
            {
                if (returnResultAsString != null)
                {
                    returnResultAsString(fixedLengthLine.OutputColumnNames(), tableNumber);
                }
            }

            for (rowInx = 0; rowInx <= maxRowInx; rowInx++)
            {
                DataRow row = tab.Rows[rowInx];
                if (returnResultAsString != null)
                {
                    for (colInx = 0; colInx <= maxColInx; colInx++)
                    {
                        fixedLengthLine.SetColumnData(colInx, row[colInx].ToString());
                    }
                    returnResultAsString(fixedLengthLine.OutputColumnData(), tableNumber);
                }
            }

        }//end ExtractFixedLengthDataFromTable method

        /// <summary>
        /// Extracts data from DataTable into fixed length text format. 
        /// </summary>
        /// <param name="fixedLengthLineDef">Object containing column definitions to use for the output fixed length text data.</param>
        /// <param name="tab">DataTable containing the data.</param>
        /// <param name="useLineTerminator">String containing one or more characters that will denote the end of a line of data.</param>
        /// <param name="columnNamesOnFirstLine">If true, a line containing the column names in delimited format will be the first line returned.</param>
        /// <param name="allowDataTruncation">If true, data longer than defined column length will be truncated; otherwise an exception will be thrown.</param>
        /// <param name="tableNumber">Arbitrary number used to identify the table.</param>
        /// <param name="lineTerminatorChars">Default is CR/LF for characters to mark end of line. You can override the default by setting this parameter.</param>
        /// <remarks>This version of ExtractFixedLengthDataFromTable adjusts column definitions to allow for changes to the data lengths in the data table.</remarks>
        public void ExtractFixedLengthDataFromTable(PFFixedLengthDataLine fixedLengthLineDef,
                                                     DataTable tab,
                                                     bool useLineTerminator,
                                                     bool columnNamesOnFirstLine,
                                                     bool allowDataTruncation,
                                                     int tableNumber,
                                                     string lineTerminatorChars)
        {
            int rowInx = 0;
            int maxRowInx = -1;
            int dtColInx = 0;
            int defColInx = 0;
            int maxColInx = -1;
            int colLen = -1;
            DataColumnCollection cols = tab.Columns;
            PFFixedLengthDataLine dtFixedLengthLine = new PFFixedLengthDataLine(cols.Count);
            bool colDefOverride = false;

            maxRowInx = tab.Rows.Count - 1;
            maxColInx = tab.Columns.Count - 1;

            dtFixedLengthLine.UseLineTerminator = fixedLengthLineDef.UseLineTerminator;
            dtFixedLengthLine.LineTerminator = fixedLengthLineDef.LineTerminator;
            dtFixedLengthLine.ColumnNamesOnFirstLine = fixedLengthLineDef.ColumnNamesOnFirstLine;
            dtFixedLengthLine.AllowDataTruncation = fixedLengthLineDef.AllowDataTruncation;
            dtFixedLengthLine.MaxColumnLengthOverride = fixedLengthLineDef.MaxColumnLengthOverride;
            dtFixedLengthLine.DefaultStringColumnLength = fixedLengthLineDef.DefaultStringColumnLength;

            for (dtColInx = 0; dtColInx <= maxColInx; dtColInx++)
            {
                for (defColInx = 0; defColInx < fixedLengthLineDef.ColumnDefinitions.ColumnDefinition.Length; defColInx++)
                {
                    colDefOverride = false;
                    if (tab.Columns[dtColInx].ColumnName == fixedLengthLineDef.ColumnDefinitions.ColumnDefinition[defColInx].ColumnName)
                    {
                        dtFixedLengthLine.SetColumnDefinition(dtColInx,
                                                              fixedLengthLineDef.ColumnDefinitions.ColumnDefinition[defColInx].ColumnName,
                                                              fixedLengthLineDef.ColumnDefinitions.ColumnDefinition[defColInx].ColumnLength,
                                                              fixedLengthLineDef.ColumnDefinitions.ColumnDefinition[defColInx].ColumnDataAlignment);
                        colDefOverride = true;
                        break;
                    }
                }
                if (colDefOverride == false)
                {
                    //dynamically build the column definition
                    if (PFFixedLengthDataLine.DataTypeIsNumeric(tab.Columns[dtColInx].DataType))
                    {
                        colLen = PFFixedLengthDataLine.GetNumericTypeMaxExtractLength(tab.Columns[dtColInx].DataType);
                    }
                    else if (PFFixedLengthDataLine.DataTypeIsDateTime(tab.Columns[dtColInx].DataType))
                    {
                        colLen = PFFixedLengthDataLine.GetDateTimeTypeMaxExtractLength(tab.Columns[dtColInx].DataType);
                    }
                    else
                    {
                        colLen = tab.Columns[dtColInx].MaxLength;
                    }
                    dtFixedLengthLine.SetColumnDefinition(dtColInx, tab.Columns[dtColInx].ColumnName, colLen, PFDataAlign.LeftJustify);
                }

            }

            if (columnNamesOnFirstLine == true)
            {
                if (returnResultAsString != null)
                {
                    returnResultAsString(dtFixedLengthLine.OutputColumnNames(), tableNumber);
                }
            }

            for (rowInx = 0; rowInx <= maxRowInx; rowInx++)
            {
                DataRow row = tab.Rows[rowInx];
                if (returnResultAsString != null)
                {
                    for (dtColInx = 0; dtColInx <= maxColInx; dtColInx++)
                    {
                        dtFixedLengthLine.SetColumnData(dtColInx, row[dtColInx].ToString());
                    }
                    returnResultAsString(dtFixedLengthLine.OutputColumnData(), tableNumber);
                }
            }

        }//end method


        /// <summary>
        /// Retrieves formatting information for a line in a fixed length text file.
        /// </summary>
        /// <param name="tab">DataTable containing the data.</param>
        /// <param name="useLineTerminator">String containing one or more characters that will denote the end of a line of data.</param>
        /// <param name="columnNamesOnFirstLine">If true, a line containing the column names in delimited format will be the first line returned.</param>
        /// <param name="allowDataTruncation">If true, data longer than defined column length will be truncated; otherwise an exception will be thrown.</param>
        /// <returns>PFFixedLengthDataLine object.</returns>
        public PFFixedLengthDataLine GetFixedLengthLineDefinitionFromTable(DataTable tab,
                                                                           bool useLineTerminator,
                                                                           bool columnNamesOnFirstLine,
                                                                           bool allowDataTruncation)
        {
            return GetFixedLengthLineDefinitionFromTable(tab, useLineTerminator, columnNamesOnFirstLine, allowDataTruncation, (int)1, Environment.NewLine);
        }
        
        /// <summary>
        /// Retrieves formatting information for a line in a fixed length text file.
        /// </summary>
        /// <param name="tab">DataTable containing the data.</param>
        /// <param name="useLineTerminator">String containing one or more characters that will denote the end of a line of data.</param>
        /// <param name="columnNamesOnFirstLine">If true, a line containing the column names in delimited format will be the first line returned.</param>
        /// <param name="allowDataTruncation">If true, data longer than defined column length will be truncated; otherwise an exception will be thrown.</param>
        /// <param name="tableNumber">Arbitrary number used to identify the table.</param>
        /// <param name="lineTerminatorChars">Default is CR/LF for characters to mark end of line. You can override the default by setting this parameter.</param>
        /// <returns>PFFixedLengthDataLine object.</returns>
        public PFFixedLengthDataLine GetFixedLengthLineDefinitionFromTable(DataTable tab,
                                                                           bool useLineTerminator,
                                                                           bool columnNamesOnFirstLine,
                                                                           bool allowDataTruncation,
                                                                           int tableNumber,
                                                                           string lineTerminatorChars)
        {
            int colInx = 0;
            int maxColInx = -1;
            int colLen = -1;
            DataColumnCollection cols = tab.Columns;
            PFFixedLengthDataLine fixedLengthLine = new PFFixedLengthDataLine(cols.Count);

            maxColInx = tab.Columns.Count - 1;

            fixedLengthLine.UseLineTerminator = useLineTerminator;
            fixedLengthLine.LineTerminator = lineTerminatorChars;
            fixedLengthLine.ColumnNamesOnFirstLine = columnNamesOnFirstLine;
            fixedLengthLine.AllowDataTruncation = allowDataTruncation;
            fixedLengthLine.MaxColumnLengthOverride = this.MaxColumnLengthOverride;

            for (colInx = 0; colInx <= maxColInx; colInx++)
            {
                if (PFFixedLengthDataLine.DataTypeIsNumeric(cols[colInx].DataType))
                {
                    colLen = PFFixedLengthDataLine.GetNumericTypeMaxExtractLength(cols[colInx].DataType);
                }
                else if (PFFixedLengthDataLine.DataTypeIsDateTime(cols[colInx].DataType))
                {
                    colLen = PFFixedLengthDataLine.GetDateTimeTypeMaxExtractLength(cols[colInx].DataType);
                }
                else
                {
                    colLen = cols[colInx].MaxLength;
                }
                fixedLengthLine.SetColumnDefinition(colInx, cols[colInx].ColumnName, colLen);
            }

            if (columnNamesOnFirstLine == true)
            {
                if (returnResultAsString != null)
                {
                    returnResultAsString(fixedLengthLine.OutputColumnNames(), tableNumber);
                }
            }

            return fixedLengthLine;

        }

        #endregion


        #region Export to XML routines
        /// <summary>
        /// Saves dataset into an xml file.
        /// </summary>
        /// <param name="ds">DataSet object to save.</param>
        /// <param name="outputPath">Full path for xml file to create.</param>
        /// <returns>Full path to the xml file; otherwise null if file was not created.</returns>
        public string ExportDataSetToXmlFile(DataSet ds, string outputPath)
        {
            string ret = null;
            ds.WriteXml(outputPath);
            if (File.Exists(outputPath))
                ret = outputPath;
            return ret;
        }

        /// <summary>
        /// Saves dataset into an xml file.
        /// </summary>
        /// <param name="ds">DataSet object to save.</param>
        /// <param name="outputPath">Full path for xml file to create.</param>
        /// <returns>Full path to the xml file; otherwise null if file was not created.</returns>

        public string ExportDataSetWithSchemaToXmlFile(DataSet ds, string outputPath)
        {
            string ret = null;
            ds.WriteXml(outputPath, XmlWriteMode.WriteSchema);
            if (File.Exists(outputPath))
                ret = outputPath;
            return ret;
        }

        /// <summary>
        /// Saves DataTable into an xml file.
        /// </summary>
        /// <param name="dt">DataTable object to save.</param>
        /// <param name="outputPath">Full path for xml file to create.</param>
        /// <returns>Full path to the xml file; otherwise null if file was not created.</returns>
        public string ExportDataTableToXmlFile(DataTable dt, string outputPath)
        {
            string ret = null;
            DataSet newDs = new DataSet();
            if(dt.DataSet==null)
                newDs.Tables.Add(dt); //force xml root name to be NewDataSet: this will match what is produced by writexmlschema method for datatable.

            if (String.IsNullOrEmpty(dt.TableName))
                dt.TableName = "Table";
            dt.WriteXml(outputPath);
            if (File.Exists(outputPath))
                ret = outputPath;
            if (newDs.Tables.Count > 0)
                newDs.Tables.Remove(dt);
            return ret;
        }

        /// <summary>
        /// Saves DataTable into an xml file.
        /// </summary>
        /// <param name="dt">DataTable object to save.</param>
        /// <param name="outputPath">Full path for xml file to create.</param>
        /// <returns>Full path to the xml file; otherwise null if file was not created.</returns>
        public string ExportDataTableWithSchemaToXmlFile(DataTable dt, string outputPath)
        {
            string ret = null;
            if (String.IsNullOrEmpty(dt.TableName))
                dt.TableName = "Table";
            dt.WriteXml(outputPath, XmlWriteMode.WriteSchema);
            if (File.Exists(outputPath))
                ret = outputPath;
            return ret;
        }

        /// <summary>
        /// Saves dataset into an xml schema file.
        /// </summary>
        /// <param name="ds">DataSet object to save.</param>
        /// <param name="outputPath">Full path for xml schema file to create.</param>
        /// <returns>Full path to the xml schema file; otherwise null if file was not created.</returns>
        public string ExportDataSetToXmlSchmaFile(DataSet ds, string outputPath)
        {
            string ret = null;
            ds.WriteXmlSchema(outputPath);
            if (File.Exists(outputPath))
                ret = outputPath;
            return ret;
        }

        /// <summary>
        /// Saves DataTable into an xml schema file.
        /// </summary>
        /// <param name="dt">DataTable object to save.</param>
        /// <param name="outputPath">Full path for xml schema file to create.</param>
        /// <returns>Full path to the xml schema file; otherwise null if file was not created.</returns>
        public string ExportDataTableToXmlSchemaFile(DataTable dt, string outputPath)
        {
            string ret = null;
            if (String.IsNullOrEmpty(dt.TableName))
                dt.TableName = "Table";
            dt.WriteXmlSchema(outputPath);
            if (File.Exists(outputPath))
                ret = outputPath;
            return ret;
        }
        
        #endregion


    }//end class
}//end namespace
