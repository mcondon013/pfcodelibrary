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

    public class TestClass1
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties

        private string _testString = "testing ...";

        //constructors

        public TestClass1()
        {
            _msg.Length = 0;
            _msg.Append("Hello from TestClass1. How are you today? I am fine, glad you asked.");
            _testString = "Testing 1 2 3 ...";
        }

        //properties

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



        //methods


    }//end class

}//end namespace
