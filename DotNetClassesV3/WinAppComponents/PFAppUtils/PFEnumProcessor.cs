//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFAppUtils
{
    /// <summary>
    /// Class provides extra functionality for working with enums.
    /// </summary>
    public class PFEnumProcessor
    {
        //private work variables
        private static StringBuilder _msg = new StringBuilder();

        //private variables for properties

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFEnumProcessor()
        {
            ;
        }

        //properties

        //methods
        /// <summary>
        /// Converts enumeration constant to its corresponding value.
        /// </summary>
        /// <param name="str">Constant name for the enum value.</param>
        /// <returns>An enumeration value.</returns>
        public static T StringToEnum<T>(string str)
        {
            return (T)Enum.Parse(typeof(T), str);
        }



    }//end class
}//end namespace
