using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using PFPrinterObjects;

namespace TestprogPrinterObjects
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
        public static void PFTextPrinterTest()
        {
            PFTextPrinter printer = new PFTextPrinter();

            printer.ShowPageNumbers = true;
            printer.Title = "TEST OUTPUT FROM TESTPROG";
            printer.TextToPrint = "Test output\r\nPlus a second line.";

            //printer.PrintPreview(true);
            printer.ShowPrintPreview();

            //printer.ShowPrintDialog();
            //printer.Print();

            //printer.ShowPageSettings();
            //printer.Print(true);
            //printer.ShowPrintPreview();

        }

        public static void PFReportPrinterTest()
        {
            PFReportPrinter printer = new PFReportPrinter();
            printer.Font = new System.Drawing.Font("Lucida Console", (float)10.0);

            _str.Length = 0;
            _str.Append("Test of column headers and other title info");
            _str.Append(Environment.NewLine);
            _str.Append(@"Folder Path: c:\test1\test2\test3 " + " Attributes: " + "RSHA               ");
            _str.Append(Environment.NewLine);
            _str.Append("Total Bytes:   " + "50,445,333     " + "As of Date:  " + "10/22/2013 17:15:25");
            _str.Append(Environment.NewLine);
            _str.Append("# Folders:     " + "54             " + "# Files:     " + "225                ");
            _str.Append(Environment.NewLine);
            _str.Append(Environment.NewLine);
            _str.Append("Item Type  " + "Item Name           " + "        Size");
            _str.Append(Environment.NewLine);
            _str.Append(Environment.NewLine);

            printer.ShowPageNumbers = true;
            printer.Title = _str.ToString();

            Program._messageLog.WriteLine(_str.ToString());

            //_str.Length = 0;
            //_str.Append(@"Folder Path: c:\test1\test2\test3 " + " Attributes: " + "RSHA               ");
            //_str.Append(Environment.NewLine);
            //_str.Append("Total Bytes:   " + "50,445,333     " + "As of Date:  " + "10/22/2013 17:15:25");
            //_str.Append(Environment.NewLine);
            //_str.Append("# Folders:     " + "54             " + "# Files:     " + "225                ");
            //_str.Append(Environment.NewLine);
            //printer.ReportHeader = _str.ToString();

            //_str.Length = 0;
            //_str.Append("Item Type  " + "Item Name           " + "        Size");
            //_str.Append(Environment.NewLine);
            //_str.Append(Environment.NewLine);
            //printer.ColumnHeadings = _str.ToString();

            _str.Length = 0;
            for (int i = 1; i <= 50; i++)
            {
                //_str.Append("FOLDER     " + "Folder001              " + "      5,500,555");
                _str.Append("FOLDER     " + "Folder" + i.ToString("000") + "           " + "   5,500,555");
                _str.Append(Environment.NewLine);
            }
            for (int i = 1; i <= 150; i++)
            {
                // _str.Append("FILE       " + "File101.txt            " + "          2,225");
                _str.Append("FILE       " + "File" + i.ToString("000") + ".txt         " + "       2,225");
                _str.Append(Environment.NewLine);
            }
            printer.TextToPrint = _str.ToString();

            Program._messageLog.WriteLine(_str.ToString());

            printer.ShowPageSettings();
            //printer.ShowPrintDialog();
            printer.PrintPreview(true);

            //printer.ShowPageSettings();
            //printer.Print(true);
            //printer.ShowPrintPreview();

        }



    }//end class
}//end namespace
