//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using PFTextFiles;
using System.Xml;

namespace TestprogTextFiles
{
    public class QuickTests
    {
        //private work variables
        private static StringBuilder _msg = new StringBuilder();

        //private variables for properties

        //constructors

        public QuickTests()
        {
            ;
        }

        //properties

        //methods
        public static void QuickTest1()
        {
            PFColumnDefinitions cols = new PFColumnDefinitions(5);
            PFColDef[] col = cols.ColumnDefinition;

            for (int inx = 0; inx < 5; inx++)
            {
                col[inx].ColumnName = "Column" + inx.ToString("000");
                col[inx].ColumnLength = 5;
                col[inx].ColumnDataAlignment = PFDataAlign.LeftJustify;
            }

            cols.SaveToXmlFile(@"c:\temp\coldefstest.xml");

            cols = null;

            cols = PFColumnDefinitions.LoadFromXmlFile(@"c:\temp\coldefstest.xml");

            _msg.Length = 0;
            _msg.Append("ToString:\r\n");
            _msg.Append(cols.ToString());
            Program._messageLog.WriteLine(_msg.ToString());

            _msg.Length = 0;
            _msg.Append("\r\n\r\nSerialize to XML string:\r\n");
            _msg.Append(cols.ToXmlString());
            Program._messageLog.WriteLine(_msg.ToString());

            XmlDocument doc = cols.ToXmlDocument();
            _msg.Length = 0;
            _msg.Append("\r\n\r\nSerialize to XML document:\r\n");
            _msg.Append(doc.InnerXml);
            Program._messageLog.WriteLine(_msg.ToString());
          

        }

        public static void QuickTest2()
        {
            PFClassExtended cls = new PFClassExtended();

            _msg.Length = 0;
            _msg.Append(cls.ToString());
            Program._messageLog.WriteLine(_msg.ToString());

            _msg.Length = 0;
            _msg.Append("\r\n\r\nToXmlString Test:\r\n\r\n");
            _msg.Append(cls.ToXmlString());
            Program._messageLog.WriteLine(_msg.ToString());

            XmlDocument doc = cls.ToXmlDocument();
            _msg.Length = 0;
            _msg.Append("\r\n\r\nToXmlDocument Test:\r\n\r\n");
            _msg.Append(doc.InnerXml);
            Program._messageLog.WriteLine(_msg.ToString());

            cls.SaveToXmlFile(@"c:\temp\PFClassExtended.xml");

            cls = null;

            cls = PFClassExtended.LoadFromXmlFile(@"c:\temp\PFClassExtended.xml");
            _msg.Length = 0;
            _msg.Append("\r\n\r\nLoadFromXml followed by ToString Test:\r\n\r\n");
            _msg.Append(cls.ToString());
            Program._messageLog.WriteLine(_msg.ToString());

        }

        public static void QuickTest3()
        {
            PFTextFile textFile = new PFTextFile(@"FixedLengthText.txt",PFFileOpenOperation.OpenFileToRead);
            string data = string.Empty;
            int numBytesPerLine = 92;

            data = textFile.ReadData(numBytesPerLine, true);
            while (data != null)
            {
                Program._messageLog.WriteLine(data);
                data = textFile.ReadData(numBytesPerLine, true);
            }

        }


    }//end class
}//end namespace
