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
using System.Runtime.Serialization;
using System.IO;

namespace TestprogAppUtils
{
    /// <summary>
    /// Initial class prototype for ProFast application or library code that includes ToString override, XML Serialization and output to XML document or string.
    /// </summary>
    public class PFClassExtended1
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        private TestClass1 tst = new TestClass1();

        private string _teststring1 = "this is a test string";
        private long _testlong1 = -55555;
        internal double _testfloat = -45678.4397;
        private string _teststring2 = "Yet another string by bing.";
        public int _publicInt = 666;

        //private variables for properties
        internal bool _testBool = false;
        private string _testString = "Testing ...";
        private int _testInt = -55;
        private long _testLOng = 12555777999;
        private double _testDouble = -34578.654321;
        private decimal _testDecimal = (decimal)12345.7654321;
        private string[] _testStringArray = { "First value", "Second value", "Third value", "Fourth value", "Fifth value" };

        //constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PFClassExtended1()
        {
            _teststring1 = "What a day for a daydream. What a load of ...";
            _testlong1 = -666666664;
            _teststring2 = "a modified version of the value of this object known as a string";
        }

        //properties

        private long Testlong1
        {
            get
            {
                return _testlong1;
            }
            set
            {
                _testlong1 = value;
            }
        }

        internal string Teststring1
        {
            get
            {
                return _teststring1;
            }
            set
            {
                _teststring1 = value;
            }
        }



        /// <summary>
        /// Protype property.
        /// </summary>
        public bool TestBool
        {
            get
            {
                return _testBool;
            }
        }

        /// <summary>
        /// Protype property.
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
        /// Protype property.
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
        /// Protype property.
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
        /// Protype property.
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
        /// Protype property.
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


        public TestClass1 Tst
        {
            get
            {
                return tst;
            }
            set
            {
                tst = value;
            }
        }

        //methods


    }//end class
}//end namespace
