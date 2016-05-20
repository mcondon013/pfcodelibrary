//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace TestprogTextFiles
{
    /// <summary>
    /// Initial class prototype for ProFast application or library code that includes ToString override, XML Serialization and output to XML document or string.
    /// </summary>
    public class PFClassExtended
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private bool _testBool = false;
        private string _testString = "Testing ...";
        private int _testInt = -55;
        private long _testLOng = 12555777999;
        private double _testDouble = -34578.654321;
        private decimal _testDecimal = (decimal)12345.7654321;
        private string[] _testStringArray = { "First", "Second", "Third", "Fourth", "Fifth" };


        //private variables for properties

        //constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PFClassExtended()
        {
            ;
        }

        //properties
        /// <summary>
        /// TestBool Property.
        /// </summary>
        public bool TestBool
        {
            get
            {
                return _testBool;
            }
            set
            {
                _testBool = value;
            }
        }

        /// <summary>
        /// TestString Property.
        /// </summary>
        public string TestString
        {
            get
            {
                return _testString;
            }
            set
            {
                _testString = value;
            }
        }

        /// <summary>
        /// TestInt Property.
        /// </summary>
        public int TestInt
        {
            get
            {
                return _testInt;
            }
            set
            {
                _testInt = value;
            }
        }

        /// <summary>
        /// TestLOng Property.
        /// </summary>
        public long TestLOng
        {
            get
            {
                return _testLOng;
            }
            set
            {
                _testLOng = value;
            }
        }

        /// <summary>
        /// TestDouble Property.
        /// </summary>
        public double TestDouble
        {
            get
            {
                return _testDouble;
            }
            set
            {
                _testDouble = value;
            }
        }

        /// <summary>
        /// TestDecimal Property.
        /// </summary>
        public decimal TestDecimal
        {
            get
            {
                return _testDecimal;
            }
            set
            {
                _testDecimal = value;
            }
        }



        /// <summary>
        /// Protype property.
        /// </summary>
        public string[] TestStringArray
        {
            get
            {
                return _testStringArray;
            }
            set
            {
                _testStringArray = value;
            }
        }


        //methods

        /// <summary>
        /// Saves the column definitions contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFClassExtended));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFClassExtended.</returns>
        public static PFClassExtended LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFClassExtended));
            TextReader textReader = new StreamReader(filePath);
            PFClassExtended columnDefinitions;
            columnDefinitions = (PFClassExtended)deserializer.Deserialize(textReader);
            textReader.Close();
            return columnDefinitions;
        }


        //class helpers

        /// <summary>
        /// Routine overrides default ToString method and outputs name and value for all public properties.
        /// </summary>
        /// <returns>String value.</returns>
        public override string ToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties();
            int inx = 0;
            int maxInx = props.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                PropertyInfo prop = props[inx];
                object val = prop.GetValue(this, null);
                data.Append(prop.Name);
                data.Append(": ");
                if (val != null)
                    data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (prop.Name == "TestStringArray")
                {
                    for (int i = 0; i < this.TestStringArray.Length; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(this.TestStringArray[i].ToString());
                        data.Append("  ");
                    }
                }

            }

            return data.ToString();
        }

        /// <summary>
        /// Returns a string containing the contents of the object in XML format.
        /// </summary>
        /// <returns>String value in xml format.</returns>
        /// ///<remarks>XML Serialization is used for the transformation.</remarks>
        public string ToXmlString()
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFClassExtended));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Converts instance of this class into an XML document.
        /// </summary>
        /// <returns>XmlDocument</returns>
        /// ///<remarks>XML Serialization and XmlDocument class are used for the transformation.</remarks>
        public XmlDocument ToXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(this.ToXmlString());
            return doc;
        }


    }//end class
}//end namespace
