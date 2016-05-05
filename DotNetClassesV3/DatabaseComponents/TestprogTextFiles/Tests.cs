using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using PFTextFiles;
using PFTextObjects;
using PFRandomData;
using PFDataAccessObjects;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Xml;
using PFTimers;
using PFTextFileViewer;

namespace TestprogTextFiles
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
        public static void RunPFTextFileTests(bool appendData, bool deleteAfterWrite)
        {
            Program._messageLog.Clear();
            Program._messageLog.WriteLine("----------------------------------------------------------");
            Program._messageLog.WriteLine("RunPFTextFileTests starting ...");
            try
            {
                string fileName = @"c:\temp\TestTextFile1.txt";
                string fileData = string.Empty;
                PFTextFile textFile = null;

                if(appendData)
                    textFile = new PFTextFile(@"c:\temp\TestTextFile1.txt", PFFileOpenOperation.OpenFileForAppend);
                else
                    textFile = new PFTextFile(fileName, PFFileOpenOperation.OpenFileForWrite);
                textFile.WriteLine("This is line 1.");
                textFile.WriteData("abcdefghijk");
                textFile.WriteData("12345678901");
                textFile.WriteBlankLine(10);
                textFile.WriteLine("This is line 2.");
                textFile.CloseFile();
                textFile.OpenFile(fileName, PFFileOpenOperation.OpenFileToRead);
                fileData = textFile.ReadAllText();
                textFile.CloseFile();
                Program._messageLog.WriteLine(fileData);

                Program._messageLog.WriteLine(string.Empty);
                Program._messageLog.WriteLine("Read lines test follows:\r\n");
                textFile.OpenFile(fileName, PFFileOpenOperation.OpenFileToRead);
                while (textFile.Peek() >= 0)
                {
                    fileData = textFile.ReadLine();
                    Program._messageLog.WriteLine(fileData);
                }
                textFile.CloseFile();


                Program._messageLog.WriteLine(string.Empty);
                Program._messageLog.WriteLine("Read lines test 2 follows:\r\n");
                textFile.OpenFile(fileName, PFFileOpenOperation.OpenFileToRead);
                fileData = textFile.ReadLine();
                while (fileData != null)
                {
                    Program._messageLog.WriteLine(fileData);
                    fileData = textFile.ReadLine();
                }

                _msg.Length = 0;
                _msg.Append("\r\nFILE SIZE: \r\n");
                _msg.Append(textFile.Length.ToString("#,##0"));
                _msg.Append(" bytes.");
                Program._messageLog.WriteLine(_msg.ToString());
                textFile.CloseFile();


                if (deleteAfterWrite)
                    PFTextFile.DeleteFile(textFile);
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
                Program._messageLog.WriteLine("... RunPFTextFileTests finished.");
                Program._messageLog.WriteLine("----------------------------------------------------------");
            }
        }


        public static void DelimitedLineTextFileTests(int numRowsToOutput)
        {
            PFTextFile textFile = new PFTextFile(@"c:\temp\DelimitedText.txt",PFFileOpenOperation.OpenFileForWrite);
            PFDelimitedDataLine line = new PFDelimitedDataLine(5);
            RandomString dat = new RandomString();
            RandomNumber siz = new RandomNumber();
            int num = 0;
            string str = string.Empty;

            try
            {
                line.LineTerminator = "\r\n";
                line.ColumnSeparator = ",";
                line.SetColumnDefinition(0, "FirstColumn",10);
                line.SetColumnDefinition(1, "SecondColumn",15);
                line.SetColumnDefinition(2, "ColumnThree",5);
                line.SetColumnDefinition(3, "ColumnFour",35);
                line.SetColumnDefinition(4, "FifthColumn",25);

                textFile.WriteData(line.OutputColumnNames());

                for (int rowNum = 0; rowNum < numRowsToOutput; rowNum++)
                {
                    for (int inx = 0; inx < 5; inx++)
                    {
                        num = siz.GenerateRandomInt(1, line.ColumnDefinitions.ColumnDefinition[inx].ColumnLength);
                        str = dat.GetStringAL(num);
                        line.ColumnData.ColumnDataValue[inx].Data = str;
                    }
                    textFile.WriteData(line.OutputColumnData());
                }

                textFile.CloseFile();

                line.SaveToXmlFile(@"c:\temp\DelimitedText.xml");

                //read textfile
                textFile.OpenFile(@"c:\temp\DelimitedText.txt", PFFileOpenOperation.OpenFileToRead);
                string input = textFile.ReadLine();
                line.VerifyColumnNames(input);
                Program._messageLog.WriteLine(line.ColumnDefinitions.ToString());
                input = textFile.ReadLine();
                line.ParseData(input);
                Program._messageLog.WriteLine(line.ColumnData.ToString());
                
                textFile.CloseFile();


                //double check loading of xml schema works
                PFDelimitedDataLine line2 = PFDelimitedDataLine.LoadFromXmlFile(@"c:\temp\DelimitedText.xml");

                Program._messageLog.WriteLine(line2.ToXmlString());

                PFTextFile textFile2 = new PFTextFile(@"c:\temp\DelimitedText2.txt", PFFileOpenOperation.OpenFileForWrite);
                char[] tst = { 'a', 'b', 'c', 'd', 'e' };

                //line2.LineTerminator = "\r\n";
                line2.ColumnSeparator = ",";
                line2.SetColumnDefinition(0, "Column One", 10);
                line2.SetColumnDefinition(1, "2ndColumn", 15);
                line2.SetColumnDefinition(2, "Column Three", 5);
                line2.SetColumnDefinition(3, "Column Four", 35);
                line2.SetColumnDefinition(4, "5thColumn", 25);

                textFile2.WriteData(line2.OutputColumnNames());

                for (int rowNum = 0; rowNum < numRowsToOutput; rowNum++)
                {
                    for (int inx = 0; inx < 5; inx++)
                    {
                        num = siz.GenerateRandomInt(1, line2.ColumnDefinitions.ColumnDefinition[inx].ColumnLength);
                        str = new string(tst[inx], num); 
                        line2.ColumnData.ColumnDataValue[inx].Data = str;
                    }
                    textFile2.WriteData(line2.OutputColumnData());
                }

                textFile2.CloseFile();



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
                if(textFile!=null)
                    textFile = null; ;
            }
                 
        

        }//end DelimitedLineTextFileTests


        public static void FixedLengthLineTextFileTests(int numRowsToOutput, bool useCrLfLineTerminator, bool allowDataTruncation)
        {
            PFTextFile textFile = new PFTextFile(@"c:\temp\FixedLengthText.txt", PFFileOpenOperation.OpenFileForWrite);
            PFFixedLengthDataLine line = new PFFixedLengthDataLine(5);
            int num = 0;
            string str = string.Empty;
            char[] tst = { 'a', 'b', 'c', 'd', 'e' };

            try
            {
                line.UseLineTerminator = useCrLfLineTerminator;
                line.AllowDataTruncation = allowDataTruncation;
                line.SetColumnDefinition(0, "Column01", 10);
                line.SetColumnDefinition(1, "SecondColumnHeadingIsThis", 15);
                line.SetColumnDefinition(2, "Col3HDR", 5);
                line.SetColumnDefinition(3, "Col4", 35);
                line.SetColumnDefinition(4, "FifthColumn", 25,PFDataAlign.RightJustify);

                textFile.WriteData(line.OutputColumnNames());

                for (int rowNum = 0; rowNum < numRowsToOutput; rowNum++)
                {
                    for (int inx = 0; inx < 5; inx++)
                    {
                        num = line.ColumnDefinitions.ColumnDefinition[inx].ColumnLength;
                        if (inx == 4)
                            num = num - 7;
                        //str = new string(tst[inx],num);
                        str = PFTextProcessor.RepeatChar(tst[inx], num);
                        line.ColumnData.ColumnDataValue[inx].Data = str;
                        str = null;
                    }
                    textFile.WriteData(line.OutputColumnData());
                }

                textFile.CloseFile();

                line.SaveToXmlFile(@"c:\temp\FixedLengthText.xml");

                
                //read textfile
                textFile.OpenFile(@"c:\temp\FixedLengthText.txt", PFFileOpenOperation.OpenFileToRead);
                int lineLen = line.LineLength;
                string input = textFile.ReadData(lineLen);
                line.VerifyColumnNames(input);
                Program._messageLog.WriteLine(line.ColumnDefinitions.ToString());
                input = textFile.ReadData(lineLen);
                int lineNo = 0;
                while (input != null)
                {
                    lineNo++;
                    line.ParseData(input);
                    Program._messageLog.WriteLine("LINE " + lineNo.ToString() + ": " + line.ColumnData.ToString());
                    input = textFile.ReadData(lineLen);
                }

                textFile.CloseFile();


                PFTextFile textFile2 = new PFTextFile(@"c:\temp\FixedLengthText2.txt", PFFileOpenOperation.OpenFileForWrite);
                PFFixedLengthDataLine line2 = PFFixedLengthDataLine.LoadFromXmlFile(@"c:\temp\FixedLengthText.xml");

                Program._messageLog.WriteLine(line2.ToXmlString());

                line2.UseLineTerminator = useCrLfLineTerminator;
                line2.SetColumnDefinition(0, "Column01", 10);
                line2.SetColumnDefinition(1, "SecondColumnHeadingIsThis", 15);
                line2.SetColumnDefinition(2, "Col3HDR", 5);
                line2.SetColumnDefinition(3, "Col4", 35);
                line2.SetColumnDefinition(4, "FifthColumn", 25, PFDataAlign.RightJustify);

                textFile2.WriteData(line.OutputColumnNames());

                for (int rowNum = 0; rowNum < numRowsToOutput; rowNum++)
                {
                    for (int inx = 0; inx < 5; inx++)
                    {
                        num = line2.ColumnDefinitions.ColumnDefinition[inx].ColumnLength;
                        if (inx == 4)
                            num = num - 7;
                        str = PFTextProcessor.RepeatChar(tst[inx], num);
                        line2.ColumnData.ColumnDataValue[inx].Data = str;
                        str = null;
                    }
                    textFile2.WriteData(line.OutputColumnData());
                }

                Program._messageLog.WriteLine(line2.ToString());


                textFile2.CloseFile();


            
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
                if (textFile != null)
                {
                    if (textFile.FileIsOpen)
                    {
                        textFile.CloseFile();
                    }
                    textFile = null;
                }
            }


        }//end method

        public static void DelimitedExtractFileTest(MainForm frm)
        {
            PFSQLServer sqlserv = new PFSQLServer();
            string connectionString = string.Empty;
            string sqlQuery = "select GeographyKey, City, StateProvinceName as StateName, EnglishCountryRegionName as CountryName, PostalCode from dbo.DimGeography;";
            PFTextFile extractFile = new PFTextFile(@"c:\temp\ExtractFileTest.txt", PFFileOpenOperation.OpenFileForWrite);
            Stopwatch watch = new Stopwatch();

            try
            {
                watch.Start();

                sqlserv.ServerName = "PROFASTSV2";
                sqlserv.DatabaseName = "AdventureWorksDW2008R2";
                sqlserv.UseIntegratedSecurity = true;
                sqlserv.ApplicationName = "TextExtractTest";
                sqlserv.WorkstationId = Environment.MachineName;

                connectionString = sqlserv.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());


                sqlserv.OpenConnection();


                sqlserv.SQLQuery = sqlQuery;
                sqlserv.CommandType = CommandType.Text;

                SqlDataReader rdr = (SqlDataReader)sqlserv.RunQueryDataReader();

                DataTable schemaTable = rdr.GetSchemaTable();
                PFDelimitedDataLine line = new PFDelimitedDataLine(schemaTable.Rows.Count);

                for (int i = 0; i < schemaTable.Rows.Count; i++)
                {
                    line.SetColumnDefinition(i, schemaTable.Rows[i]["ColumnName"].ToString());
                }
                extractFile.WriteData(line.OutputColumnNames());

                //foreach (DataRow row in schemaTable.Rows)
                //{

                //    Program._messageLog.WriteLine(row["ColumnName"].ToString());
                //    //For each property of the field...
                //    //foreach (DataColumn prop in schemaTable.Columns)
                //    //{
                //    //    //Display the field name and value.
                //    //     Program._messageLog.WriteLine(prop.ColumnName + " = " + fld[prop].ToString());
                //    //}

                //}

                int numRows = 0;
                if (rdr.HasRows)
                {
                    int colInx = 0;
                    int maxColInx = -1;
                    while (rdr.Read())
                    {
                        numRows++;
                        maxColInx = rdr.FieldCount - 1;
                        for (colInx = 0; colInx <= maxColInx; colInx++)
                        {
                            line.SetColumnData(colInx, rdr[colInx].ToString());
                        }
                        extractFile.WriteData(line.OutputColumnData());
                    }
                }

                _msg.Length = 0;
                _msg.Append("Number of data rows written:   ");
                _msg.Append(numRows.ToString("#,##0"));
                _msg.Append("\r\n");
                _msg.Append("Number of header rows written: 1");
                Program._messageLog.WriteLine(_msg.ToString());

                //if (rdr.HasRows)
                //{

                //    int colInx = 0;
                //    int maxColInx = -1;
                //    while (rdr.Read())
                //    {
                //        _msg.Length = 0;
                //        maxColInx = rdr.FieldCount - 1;
                //        for (colInx = 0; colInx <= maxColInx; colInx++)
                //        {
                //            _msg.Append(rdr.GetName(colInx));
                //            _msg.Append(": ");
                //            _msg.Append(rdr[colInx].ToString());
                //            if (colInx < maxColInx)
                //                _msg.Append(", ");
                //        }
                //        Program._messageLog.WriteLine(_msg.ToString());
                //    }
                //}

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
                sqlserv.CloseConnection();
                sqlserv = null;
                if (extractFile != null)
                {
                    extractFile.CloseFile();
                    extractFile = null;
                }
                watch.Stop();
                _msg.Length = 0;
                _msg.Append("Elapsed time: ");
                _msg.Append(watch.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());
            }



        }

        public static void FixedLengthExtractFileTest(MainForm frm)
        {
            PFSQLServer sqlserv = new PFSQLServer();
            string connectionString = string.Empty;
            string sqlQuery = "select GeographyKey, City, StateProvinceName as StateName, EnglishCountryRegionName as CountryName, PostalCode, SalesTerritoryKey as TerritoryKey from dbo.DimGeography;";
            PFTextFile extractFile = new PFTextFile(@"c:\temp\FixedLengthFileTest.txt", PFFileOpenOperation.OpenFileForWrite);
            Stopwatch watch = new Stopwatch();

            try
            {
                watch.Start();

                sqlserv.ServerName = "PROFASTWS1";
                sqlserv.DatabaseName = "AdventureWorksDW2008R2";
                sqlserv.UseIntegratedSecurity = true;
                sqlserv.ApplicationName = "TextExtractTest";
                sqlserv.WorkstationId = Environment.MachineName;

                connectionString = sqlserv.ConnectionString;

                _msg.Length = 0;
                _msg.Append("Connection string is: \r\n");
                _msg.Append(connectionString);
                Program._messageLog.WriteLine(_msg.ToString());


                sqlserv.OpenConnection();


                sqlserv.SQLQuery = sqlQuery;
                sqlserv.CommandType = CommandType.Text;

                SqlDataReader rdr = (SqlDataReader)sqlserv.RunQueryDataReader();

                DataTable schemaTable = rdr.GetSchemaTable();
                PFFixedLengthDataLine line = new PFFixedLengthDataLine(schemaTable.Rows.Count);
                line.UseLineTerminator = frm.chkFixedLengthCrLf.Checked;
                line.AllowDataTruncation = frm.chkAllowDataTruncation.Checked;

                //foreach (DataRow myField in schemaTable.Rows)
                //{
                //    //For each property of the field...
                //    foreach (DataColumn myProperty in schemaTable.Columns)
                //    {
                //        //Display the field name and value.
                //        Program._messageLog.WriteLine(myProperty.ColumnName + " = " + myField[myProperty].ToString() + " - " );
                //    }
                //}
                //Program._messageLog.WriteLine("--------------------------------");

                //Program._messageLog.WriteLine(schemaTable.Rows[0]["DataType"].ToString());
                //Program._messageLog.WriteLine(schemaTable.Rows[1].ItemArray[12].ToString());
                //Program._messageLog.WriteLine(schemaTable.Rows[2].ItemArray[12].ToString());
                //Program._messageLog.WriteLine(schemaTable.Rows[3].ItemArray[12].ToString());
                //Program._messageLog.WriteLine(schemaTable.Rows[4].ItemArray[12].ToString());
                //Program._messageLog.WriteLine(schemaTable.Rows[5].ItemArray[12].ToString());

                ////DataRow row = schemaTable.Rows[0];
                ////Program._messageLog.WriteLine(row["ColumnName"] + " is " + row["DataType"]);

                //Program._messageLog.WriteLine("--------------------------------");

                //for (int rowInx = 0; rowInx < schemaTable.Rows.Count; rowInx++)
                //{
                //    DataRow row = schemaTable.Rows[rowInx];
                //    Program._messageLog.WriteLine(row["ColumnName"] + " is " + row["DataType"]);
                //}

                //Program._messageLog.WriteLine("--------------------------------");

                //line.SetColumnDefinition(0, "GeographyKey", 12,PFDataAlign.RightJustify);
                //line.SetColumnDefinition(1, "City", 30, PFDataAlign.LeftJustify);
                //line.SetColumnDefinition(2, "StateName", 50, PFDataAlign.LeftJustify);
                //line.SetColumnDefinition(3, "CountryName", 50, PFDataAlign.LeftJustify);
                //line.SetColumnDefinition(4, "PostalCode", 15, PFDataAlign.LeftJustify);
                //line.SetColumnDefinition(5, "TerritoryKey", 12, PFDataAlign.RightJustify);

                //foreach (DataRow myField in schemaTable.Rows)
                //{
                //    //For each property of the field...
                //    foreach (DataColumn myProperty in schemaTable.Columns)
                //    {
                //    //Display the field name and value.
                //        Program._messageLog.WriteLine(myProperty.ColumnName + " = " + myField[myProperty].ToString());
                //    }
                //}


                for (int rowInx = 0; rowInx < schemaTable.Rows.Count; rowInx++)
                {
                    DataRow row = schemaTable.Rows[rowInx];
                    string colName = row["ColumnName"].ToString();
                    System.Type colType = (System.Type)row["DataType"];
                    bool typeIsNumeric = PFFixedLengthDataLine.DataTypeIsNumeric(colType);
                    int colLen = PFFixedLengthDataLine.GetNumericTypeMaxExtractLength(colType);
                    if(colLen<1)
                    {
                        colLen = (int)row["ColumnSize"];
                    }
                    if (colName.Length > colLen)
                    {
                        colLen = colName.Length;
                    }
                    PFDataAlign dataAlignment = typeIsNumeric ? PFDataAlign.RightJustify : PFDataAlign.LeftJustify;

                    line.SetColumnDefinition(rowInx, colName , colLen, dataAlignment);
                    Program._messageLog.WriteLine(colName + ", " + colType.FullName + ", " + colLen.ToString() + ", " + dataAlignment.ToString());
                }
                extractFile.WriteData(line.OutputColumnNames());

                int numRows = 0;
                if (rdr.HasRows)
                {
                    int colInx = 0;
                    int maxColInx = -1;
                    while (rdr.Read())
                    {
                        numRows++;
                        maxColInx = rdr.FieldCount - 1;
                        for (colInx = 0; colInx <= maxColInx; colInx++)
                        {
                            line.SetColumnData(colInx, rdr[colInx].ToString());
                        }
                        extractFile.WriteData(line.OutputColumnData());
                    }
                }

                _msg.Length = 0;
                _msg.Append("Number of data rows written:   ");
                _msg.Append(numRows.ToString("#,##0"));
                _msg.Append("\r\n");
                _msg.Append("Number of header rows written: 1");
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
                sqlserv.CloseConnection();
                sqlserv = null;
                if (extractFile != null)
                {
                    extractFile.CloseFile();
                    extractFile = null;
                }
                watch.Stop();
                _msg.Length = 0;
                _msg.Append("Elapsed time: ");
                _msg.Append(watch.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());
            }



        }//end test


        public static void ShowDataDelimiterPrompt()
        {
            System.Windows.Forms.DialogResult res = System.Windows.Forms.DialogResult.None;

            try
            {
                _msg.Length = 0;
                _msg.Append("ShowDataDelimiterPrompt started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                PFDataDelimitersPrompt frm = new PFDataDelimitersPrompt();

                res = frm.ShowDialog();

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
                _msg.Append("... ShowDataDelimiterPrompt finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void ShowFixedLengthDataPrompt()
        {
            System.Windows.Forms.DialogResult res = System.Windows.Forms.DialogResult.None;
            
            try
            {
                _msg.Length = 0;
                _msg.Append("ShowFixedLengthDataPrompt started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                PFFixedLengthDataPrompt frm = new PFFixedLengthDataPrompt();

                res = frm.ShowDialog();

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
                _msg.Append("\r\n... ShowFixedLengthDataPrompt finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        


        public static void ImportXmlDocument(MainForm frm)
        {
            string configValue = string.Empty;
            string dirName = string.Empty;
            string fileName = string.Empty;
            string xmlNoSchemaFileName = string.Empty;
            string xmlWithSchemaFileName = string.Empty;
            string tabXmlNoSchemaFileName = string.Empty;
            string tabXmlWithSchemaFileName = string.Empty;
            PFDataImporter dataImporter = new PFDataImporter();
            string xmlString = string.Empty;
            DataSet dsNoSchema = null;
            DataSet dsWithSchema = null;
            DataTable dtNoSchema = null;
            DataTable dtWithSchema = null;
            DataTable dtWithSchema2 = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("ImportXmlDocument started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                configValue = AppConfig.GetStringValueFromConfigFile("XmlNoSchemaFileName", "TestXmlNoSchema.xml");
                dirName = Path.GetDirectoryName(configValue);
                fileName = Path.GetFileName(configValue);
                if (String.IsNullOrEmpty(dirName))
                    dirName = AppDomain.CurrentDomain.BaseDirectory;
                if (String.IsNullOrEmpty(fileName))
                    fileName = "TestXmlNoSchema.xml";
                xmlNoSchemaFileName = Path.Combine(dirName, fileName);

                configValue = AppConfig.GetStringValueFromConfigFile("XmlWithSchemaFileName", "TestXmlWithSchema.xml");
                dirName = Path.GetDirectoryName(configValue);
                fileName = Path.GetFileName(configValue);
                if (String.IsNullOrEmpty(dirName))
                    dirName = AppDomain.CurrentDomain.BaseDirectory;
                if (String.IsNullOrEmpty(fileName))
                    fileName = "TestXmlWithSchema.xml";
                xmlWithSchemaFileName = Path.Combine(dirName, fileName);

                configValue = AppConfig.GetStringValueFromConfigFile("TabXmlNoSchemaFileName", "TestTabXmlNoSchema.xml");
                dirName = Path.GetDirectoryName(configValue);
                fileName = Path.GetFileName(configValue);
                if (String.IsNullOrEmpty(dirName))
                    dirName = AppDomain.CurrentDomain.BaseDirectory;
                if (String.IsNullOrEmpty(fileName))
                    fileName = "TesTabtXmlNoSchema.xml";
                tabXmlNoSchemaFileName = Path.Combine(dirName, fileName);

                configValue = AppConfig.GetStringValueFromConfigFile("TabXmlWithSchemaFileName", "TestTabXmlWithSchema.xml");
                dirName = Path.GetDirectoryName(configValue);
                fileName = Path.GetFileName(configValue);
                if (String.IsNullOrEmpty(dirName))
                    dirName = AppDomain.CurrentDomain.BaseDirectory;
                if (String.IsNullOrEmpty(fileName))
                    fileName = "TestTabXmlWithSchema.xml";
                tabXmlWithSchemaFileName = Path.Combine(dirName, fileName);

                
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Output to DataSet and DataTable test: ");
                _msg.Append(Environment.NewLine);
                _msg.Append(xmlNoSchemaFileName);
                _msg.Append(Environment.NewLine);
                _msg.Append(xmlWithSchemaFileName);
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());

                XmlDocument xmlDocNoSchema = new XmlDocument();
                xmlDocNoSchema.Load(xmlNoSchemaFileName);
                XmlDocument xmlDocWithSchema = new XmlDocument();
                xmlDocWithSchema.Load(xmlWithSchemaFileName);
                XmlDocument tabXmlDocNoSchema = new XmlDocument();
                tabXmlDocNoSchema.Load(tabXmlNoSchemaFileName);
                XmlDocument tabXmlDocWithSchema = new XmlDocument();
                tabXmlDocWithSchema.Load(tabXmlWithSchemaFileName);

                dsNoSchema = dataImporter.ImportXmlDocumentToDataSet(xmlDocNoSchema);
                dsWithSchema = dataImporter.ImportXmlDocumentToDataSet(xmlDocWithSchema);
                try
                {
                    dtNoSchema = dataImporter.ImportXmlDocumentToDataTable(tabXmlDocNoSchema);
                }
                catch (System.Exception ex)
                {
                    _msg.Length = 0;
                    _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                    Program._messageLog.WriteLine(_msg.ToString());
                }
                dtWithSchema = dataImporter.ImportXmlDocumentToDataTable(tabXmlDocWithSchema);

                _msg.Length = 0;
                _msg.Append(Environment.NewLine + Environment.NewLine);
                _msg.Append("DS NoSchema rows: ");
                _msg.Append(dsNoSchema.Tables[0].Rows.Count.ToString());
                _msg.Append(Environment.NewLine);
                _msg.Append("DS WithSchema rows: ");
                _msg.Append(dsWithSchema.Tables[0].Rows.Count.ToString());
                _msg.Append(Environment.NewLine + Environment.NewLine);
                _msg.Append("DT NoSchema rows: ");
                if (dtNoSchema != null)
                {
                    _msg.Append(dtNoSchema.Rows.Count.ToString());
                }
                else
                {
                    _msg.Append("Schema missing. Is required for DataTable object.");
                }
                _msg.Append(Environment.NewLine);
                _msg.Append("DT WithSchema rows: ");
                _msg.Append(dtWithSchema.Rows.Count.ToString());
                _msg.Append(Environment.NewLine + Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());

                dsNoSchema.Tables[0].WriteXml(@"c:\temp\dtNoSchema.xml");
                dsWithSchema.Tables[0].WriteXml(@"c:\temp\dtWithSchema.xml",XmlWriteMode.WriteSchema);
                dsWithSchema.Tables[0].WriteXmlSchema(@"c:\temp\dtSchema.xsd");

                //dtNoSchema = dataImporter.ImportXmlDocumentToDataTable(xmlDocNoSchema);
                //dtWithSchema = dataImporter.ImportXmlDocumentToDataTable(xmlDocWithSchema);
                //dtWithSchema = dataImporter.ImportXmlFileToDataTable(xmlWithSchemaFileName);
                //dtWithSchema = dataImporter.ImportXmlFileToDataTable(@"c:\temp\dtWithSchema.xml");
                dtWithSchema = dataImporter.ImportXmlFileToDataTable(@"c:\temp\dtWithSchemaMod10Rows.xml");
                dtWithSchema2 = dataImporter.ImportXmlFileToDataTable(@"c:\temp\testdata.xml");

                _msg.Length = 0;
                _msg.Append(Environment.NewLine + Environment.NewLine);
                //_msg.Append("DT NoSchema rows: ");
                //_msg.Append(dtNoSchema.Rows.Count.ToString());
                //_msg.Append(Environment.NewLine);
                _msg.Append("DT WithSchema rows: ");
                _msg.Append(dtWithSchema.Rows.Count.ToString());
                _msg.Append(Environment.NewLine + Environment.NewLine);
                _msg.Append("DT WithSchema 2 rows: ");
                _msg.Append(dtWithSchema2.Rows.Count.ToString());
                _msg.Append(Environment.NewLine + Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;

                dtNoSchema = new DataTable();
                dtNoSchema = dataImporter.ImportXmlFileToDataTable(@"c:\temp\dtNoSchema.xml");
                dtWithSchema = new DataTable();
                dtWithSchema = dataImporter.ImportXmlFileToDataTable(@"c:\temp\dtWithSchema.xml");
                dtWithSchema2 = new DataTable();
                dtWithSchema2 = dataImporter.ImportXmlFileToDataTable(@"c:\temp\dtNoSchema.xml", @"c:\temp\dtSchema.xsd");


                _msg.Length = 0;
                _msg.Append(Environment.NewLine + Environment.NewLine);
                _msg.Append("Testing new dataImporter xml and xml schema importing ...");
                _msg.Append(Environment.NewLine + Environment.NewLine);
                _msg.Append("DT NoSchema imported rows:     ");
                _msg.Append(dtNoSchema.Rows.Count.ToString());
                _msg.Append(Environment.NewLine);
                _msg.Append("DT NoSchema state maxlen:      ");
                _msg.Append(dtNoSchema.Columns[2].MaxLength.ToString());
                _msg.Append(Environment.NewLine);
                _msg.Append("DT WithSchema imported rows:   ");
                _msg.Append(dtWithSchema.Rows.Count.ToString());
                _msg.Append(Environment.NewLine);
                _msg.Append("DT WithSchema state maxlen:    ");
                _msg.Append(dtWithSchema.Columns[2].MaxLength.ToString());
                _msg.Append(Environment.NewLine);
                _msg.Append("DT WithSchema2 imported rows:  ");
                _msg.Append(dtWithSchema2.Rows.Count.ToString());
                _msg.Append(Environment.NewLine);
                _msg.Append("DT WithSchema2 state maxlen:   ");
                _msg.Append(dtWithSchema2.Columns[2].MaxLength.ToString());
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());

                
                //DemonstrateReadWriteXMLDocumentWithString();

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
                _msg.Append("\r\n... ImportXmlDocument finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        private static void DemonstrateReadWriteXMLDocumentWithString()
        {
            DataTable table = CreateTestTable("XmlDemo");
            PrintValues(table, "Original table");

            string fileName = @"C:\Temp\TestData.xml";
            table.WriteXml(fileName, XmlWriteMode.WriteSchema);

            DataTable newTable = new DataTable();
            newTable.ReadXml(fileName);

            // Print out values in the table.
            PrintValues(newTable, "New table");
        }

        private static DataTable CreateTestTable(string tableName)
        {
            // Create a test DataTable with two columns and a few rows.
            DataTable table = new DataTable(tableName);
            DataColumn column = new DataColumn("id", typeof(System.Int32));
            column.AutoIncrement = true;
            table.Columns.Add(column);

            column = new DataColumn("item", typeof(System.String));
            table.Columns.Add(column);

            // Add ten rows.
            DataRow row;
            for (int i = 0; i <= 9; i++)
            {
                row = table.NewRow();
                row["item"] = "item " + i;
                table.Rows.Add(row);
            }

            table.AcceptChanges();
            return table;
        }

        private static void PrintValues(DataTable table, string label)
        {
            Program._messageLog.WriteLine(label);
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    Program._messageLog.WriteLine(row[column].ToString());
                }
                Program._messageLog.WriteLine("-----");
            }
        }



        public static void ImportTextData(MainForm frm)
        {
            PFDataImporter dataImporter = new PFDataImporter();
            string configValue = string.Empty;
            string dirName = string.Empty;
            string fileName = string.Empty;
            string textDelimDataFileName = "TableTestExtractDelim.txt";
            string textDelimDataLineFormatFileName = "TableTestExtractDelimLineFormat.xml";
            string textFixedLengthDataFileName = "TableTestExtractFxl.txt";
            string textFixedLengthDataLineFormatFileName = "TableTestExtractFxlLineFormat.xml";
            string textFixedLengthNoCrLfDataFileName = "TableTestExtractFxlNoCrLf.txt";
            string textFixedLengthNoCrLfDataLineFormatFileName = "TableTestExtractFxlNoCrLfLineFormat.xml";
            DataSet dsDelimited = null;
            DataSet dsFixedLength = null;
            DataSet dsFixedLengthNoCrLf = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("ImportTextData started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                //Get filenames
                configValue = AppConfig.GetStringValueFromConfigFile("textDelimDataFileName", "TableTestExtractDelim.txt");
                dirName = Path.GetDirectoryName(configValue);
                fileName = Path.GetFileName(configValue);
                if (String.IsNullOrEmpty(dirName))
                    dirName = AppDomain.CurrentDomain.BaseDirectory;
                if (String.IsNullOrEmpty(fileName))
                    fileName = "TableTestExtractDelim.txt";
                textDelimDataFileName = Path.Combine(dirName, fileName);

                configValue = AppConfig.GetStringValueFromConfigFile("textDelimDataLineFormatFileName", "TableTestExtractDelimLineFormat.xml");
                dirName = Path.GetDirectoryName(configValue);
                fileName = Path.GetFileName(configValue);
                if (String.IsNullOrEmpty(dirName))
                    dirName = AppDomain.CurrentDomain.BaseDirectory;
                if (String.IsNullOrEmpty(fileName))
                    fileName = "TableTestExtractDelimLineFormat.xml";
                textDelimDataLineFormatFileName = Path.Combine(dirName, fileName);

                configValue = AppConfig.GetStringValueFromConfigFile("textFixedLengthDataFileName", "TableTestExtractFxl.txt");
                dirName = Path.GetDirectoryName(configValue);
                fileName = Path.GetFileName(configValue);
                if (String.IsNullOrEmpty(dirName))
                    dirName = AppDomain.CurrentDomain.BaseDirectory;
                if (String.IsNullOrEmpty(fileName))
                    fileName = "TableTestExtractFxl.txt";
                textFixedLengthDataFileName = Path.Combine(dirName, fileName);

                configValue = AppConfig.GetStringValueFromConfigFile("textFixedLengthDataLineFormatFileName", "TableTestExtractFxlLineFormat.xml");
                dirName = Path.GetDirectoryName(configValue);
                fileName = Path.GetFileName(configValue);
                if (String.IsNullOrEmpty(dirName))
                    dirName = AppDomain.CurrentDomain.BaseDirectory;
                if (String.IsNullOrEmpty(fileName))
                    fileName = "TableTestExtractFxlLineFormat.xmldar";
                textFixedLengthDataLineFormatFileName = Path.Combine(dirName, fileName);

                configValue = AppConfig.GetStringValueFromConfigFile("textFixedLengthNoCrLfDataFileName", "TableTestExtractFxlNoCrLf.txt");
                dirName = Path.GetDirectoryName(configValue);
                fileName = Path.GetFileName(configValue);
                if (String.IsNullOrEmpty(dirName))
                    dirName = AppDomain.CurrentDomain.BaseDirectory;
                if (String.IsNullOrEmpty(fileName))
                    fileName = "TableTestExtractFxlNoCrLf.txt";
                textFixedLengthNoCrLfDataFileName = Path.Combine(dirName, fileName);

                configValue = AppConfig.GetStringValueFromConfigFile("textFixedLengthNoCrLfDataLineFormatFileName", "TableTestExtractFxlNoCrLfLineFormat.xml");
                dirName = Path.GetDirectoryName(configValue);
                fileName = Path.GetFileName(configValue);
                if (String.IsNullOrEmpty(dirName))
                    dirName = AppDomain.CurrentDomain.BaseDirectory;
                if (String.IsNullOrEmpty(fileName))
                    fileName = "TableTestExtractFxlNoCrLfLineFormat.xmldar";
                textFixedLengthNoCrLfDataLineFormatFileName = Path.Combine(dirName, fileName);

                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Output text to DataSet and DataTable test: ");
                _msg.Append(Environment.NewLine);
                _msg.Append(textDelimDataFileName);
                _msg.Append(Environment.NewLine);
                _msg.Append(textDelimDataLineFormatFileName);
                _msg.Append(Environment.NewLine);
                _msg.Append(textFixedLengthDataFileName);
                _msg.Append(Environment.NewLine);
                _msg.Append(textFixedLengthDataLineFormatFileName);
                _msg.Append(Environment.NewLine);
                _msg.Append(textFixedLengthNoCrLfDataFileName);
                _msg.Append(Environment.NewLine);
                _msg.Append(textFixedLengthNoCrLfDataLineFormatFileName);
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());

                //run PFDataImporter routines

                dsDelimited = dataImporter.ImportDelimitedTextFileToDataSet(textDelimDataFileName, textDelimDataLineFormatFileName);
                dsFixedLength = dataImporter.ImportFixedLengthTextFileToDataSet(textFixedLengthDataFileName, textFixedLengthDataLineFormatFileName);
                dsFixedLengthNoCrLf = dataImporter.ImportFixedLengthTextFileToDataSet(textFixedLengthNoCrLfDataFileName, textFixedLengthNoCrLfDataLineFormatFileName);


                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("dsDelimited rows: ");
                _msg.Append(dsDelimited.Tables[0].Rows.Count.ToString());
                _msg.Append(Environment.NewLine);
                _msg.Append("dsFixedLength rows: ");
                _msg.Append(dsFixedLength.Tables[0].Rows.Count.ToString());
                _msg.Append(Environment.NewLine);
                _msg.Append("dsFixedLengthNoCrLf rows: ");
                _msg.Append(dsFixedLengthNoCrLf.Tables[0].Rows.Count.ToString());
                _msg.Append(Environment.NewLine);
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
                _msg.Append("\r\n... ImportTextData finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void ShowTextFileViewer(MainForm frm)
        {
            TextFileViewer textoutForm = null;
            //string filename = @"c:\testfiles\testfile.txt";
            string filename = @"c:\testfiles\FirstNamesShort.txt";
            
            try
            {
                _msg.Length = 0;
                _msg.Append("ShowTextFileViewer started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                textoutForm = new TextFileViewer();
                textoutForm.Caption = "Test Progam for PFTextFileViewer ...";
                textoutForm.Font = "Lucida Console";
                textoutForm.FontSize = (float)10.0;

                textoutForm.LoadFile(filename);

                textoutForm.ShowDialog();
                textoutForm.CloseWindow();
                textoutForm = null;

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
                if (textoutForm != null)
                {
                    if (textoutForm.FormIsVisible)
                    {
                        textoutForm.CloseWindow();
                    }
                    textoutForm = null;
                }
                _msg.Length = 0;
                _msg.Append("\r\n... ShowTextFileViewer finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        
    
    }//end class
}//end namespace
