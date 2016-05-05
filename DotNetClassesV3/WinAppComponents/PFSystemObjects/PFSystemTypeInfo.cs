//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFSystemObjects
{
    /// <summary>
    /// Routines for getting information about System.Type of an object.
    /// </summary>
    public class PFSystemTypeInfo
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFSystemTypeInfo()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Returns an object of the type specified by the full name string in the parameter.
        /// </summary>
        /// <param name="DataTypeName">Full name of the data type.</param>
        /// <returns></returns>
        public static System.Type ConvertNameToType(string DataTypeName)
        {
            return System.Type.GetType(DataTypeName);
        }

        /// <summary>
        /// Determines if specified data type name is one of the base .NET data types.
        /// </summary>
        /// <param name="DataTypeName">Name of the data type.</param>
        /// <returns>Returns true if the data type name refers to one of the base .NET data types.</returns>
        public static bool DataTypeIsValid(string DataTypeName)
        {
            bool typeIsValid = false;

            if (DataTypeName == "System.String")
                typeIsValid = true;
            else if (DataTypeName == "System.Int32")
                typeIsValid = true;
            else if (DataTypeName == "System.UInt32")
                typeIsValid = true;
            else if (DataTypeName == "System.Int64")
                typeIsValid = true;
            else if (DataTypeName == "System.UInt64")
                typeIsValid = true;
            else if (DataTypeName == "System.Int16")
                typeIsValid = true;
            else if (DataTypeName == "System.UInt16")
                typeIsValid = true;
            else if (DataTypeName == "System.Double")
                typeIsValid = true;
            else if (DataTypeName == "System.Single")
                typeIsValid = true;
            else if (DataTypeName == "System.Decimal")
                typeIsValid = true;
            else if (DataTypeName == "System.Char")
                typeIsValid = true;
            else if (DataTypeName == "System.Byte")
                typeIsValid = true;
            else if (DataTypeName == "System.SByte")
                typeIsValid = true;
            else if (DataTypeName == "System.Boolean")
                typeIsValid = true;
            else if (DataTypeName == "System.DateTime")
                typeIsValid = true;
            else if (DataTypeName == "System.Guid")
                typeIsValid = true;
            else if (DataTypeName == "System.Object")
                typeIsValid = true;
            else if (DataTypeName == "System.Byte[]")
                typeIsValid = true;
            else if (DataTypeName == "System.Char[]")
                typeIsValid = true;
            else
                typeIsValid = false;

            return typeIsValid;
            
        }

        /// <summary>
        /// Determines the maximum length for a specified .NET data type.
        /// </summary>
        /// <param name="DataTypeName">Name of the data type.</param>
        /// <returns>Returns true if the data type name refers to one of the base .NET data types.</returns>
        public static long GetDataTypeMaxLength(string DataTypeName)
        {
            long maxLength = 1073741823;

            if (DataTypeName == "System.String")
                maxLength = 1073741823;
            else if (DataTypeName == "System.Int32")
                maxLength = 10;
            else if (DataTypeName == "System.UInt32")
                maxLength = 11;
            else if (DataTypeName == "System.Int64")
                maxLength = 19;
            else if (DataTypeName == "System.UInt64")
                maxLength = 20;
            else if (DataTypeName == "System.Int16")
                maxLength = 5;
            else if (DataTypeName == "System.UInt16")
                maxLength = 6;
            else if (DataTypeName == "System.Double")
                maxLength = 15;
            else if (DataTypeName == "System.Single")
                maxLength = 7;
            else if (DataTypeName == "System.Decimal")
                maxLength = 30;
            else if (DataTypeName == "System.Char")
                maxLength = 1;
            else if (DataTypeName == "System.Byte")
                maxLength = 4;
            else if (DataTypeName == "System.SByte")
                maxLength = 3;
            else if (DataTypeName == "System.Boolean")
                maxLength = 1;
            else if (DataTypeName == "System.DateTime")
                maxLength = 22;
            else if (DataTypeName == "System.Guid")
                maxLength = 36;
            else if (DataTypeName == "System.Object")
                maxLength = 1073741823;
            else if (DataTypeName == "System.Byte[]")
                maxLength = 1073741823;
            else if (DataTypeName == "System.Char[]")
                maxLength = 1073741823;
            else
                maxLength = 1073741823;

            return maxLength;

        }

        /// <summary>
        /// Evaluates whether or not the specified system type is numeric.
        /// </summary>
        /// <param name="sysType">Type to be evaluated.</param>
        /// <returns>Returns true if the specified type is a numeric data type; otherwise false.</returns>
        public static bool DataTypeIsNumeric(System.Type sysType)
        {
            bool typeIsNumeric = false;

            if (sysType.FullName == "System.Int32")
                typeIsNumeric = true;
            else if (sysType.FullName == "System.UInt32")
                typeIsNumeric = true;
            else if (sysType.FullName == "System.Int64")
                typeIsNumeric = true;
            else if (sysType.FullName == "System.UInt64")
                typeIsNumeric = true;
            else if (sysType.FullName == "System.Int16")
                typeIsNumeric = true;
            else if (sysType.FullName == "System.UInt16")
                typeIsNumeric = true;
            else if (sysType.FullName == "System.Double")
                typeIsNumeric = true;
            else if (sysType.FullName == "System.Single")
                typeIsNumeric = true;
            else if (sysType.FullName == "System.Decimal")
                typeIsNumeric = true;
            else
                typeIsNumeric = false;

            return typeIsNumeric;
        }

        /// <summary>
        /// Evaluates whether or not the specified system type is one of the integer data types.
        /// </summary>
        /// <param name="sysType">Type to be evaluated.</param>
        /// <returns>Returns true if the specified type is an integer data type; otherwise false.</returns>
        public static bool DataTypeIsInteger(System.Type sysType)
        {
            bool typeIsInteger = false;

            if (sysType.FullName == "System.Int32")
                typeIsInteger = true;
            else if (sysType.FullName == "System.UInt32")
                typeIsInteger = true;
            else if (sysType.FullName == "System.Int64")
                typeIsInteger = true;
            else if (sysType.FullName == "System.UInt64")
                typeIsInteger = true;
            else if (sysType.FullName == "System.Int16")
                typeIsInteger = true;
            else if (sysType.FullName == "System.UInt16")
                typeIsInteger = true;
            else
                typeIsInteger = false;

            return typeIsInteger;
        }

        /// <summary>
        /// Evaluates whether or not the specified system type is one of the 32-bit integer data types.
        /// </summary>
        /// <param name="sysType">Type to be evaluated.</param>
        /// <returns>Returns true if the specified type is a 32-bit integer data type; otherwise false.</returns>
        public static bool DataTypeIs32BitInteger(System.Type sysType)
        {
            bool typeIs32BitInteger = false;

            if (sysType.FullName == "System.Int32")
                typeIs32BitInteger = true;
            else if (sysType.FullName == "System.UInt32")
                typeIs32BitInteger = true;
            else
                typeIs32BitInteger = false;

            return typeIs32BitInteger;
        }

        /// <summary>
        /// Evaluates whether or not the specified system type is one of the 64-bit integer data types.
        /// </summary>
        /// <param name="sysType">Type to be evaluated.</param>
        /// <returns>Returns true if the specified type is a 64-bit integer data type; otherwise false.</returns>
        public static bool DataTypeIs64BitInteger(System.Type sysType)
        {
            bool typeIs64BitInteger = false;

            if (sysType.FullName == "System.Int64")
                typeIs64BitInteger = true;
            else if (sysType.FullName == "System.UInt64")
                typeIs64BitInteger = true;
            else
                typeIs64BitInteger = false;

            return typeIs64BitInteger;
        }

        /// <summary>
        /// Evaluates whether or not the specified system type is one of the 16-bit integer data types.
        /// </summary>
        /// <param name="sysType">Type to be evaluated.</param>
        /// <returns>Returns true if the specified type is a 16-bit integer data type; otherwise false.</returns>
        public static bool DataTypeIs16BitInteger(System.Type sysType)
        {
            bool typeIs16BitInteger = false;

            if (sysType.FullName == "System.Int16")
                typeIs16BitInteger = true;
            else if (sysType.FullName == "System.UInt16")
                typeIs16BitInteger = true;
            else
                typeIs16BitInteger = false;

            return typeIs16BitInteger;
        }

        /// <summary>
        /// Evaluates whether or not the specified system type is floating point.
        /// </summary>
        /// <param name="sysType">Type to be evaluated.</param>
        /// <returns>Returns true if the specified type is a floating point data type; otherwise false.</returns>
        public static bool DataTypeIsFloatingPoint(System.Type sysType)
        {
            bool typeIsFloatingPoint = false;

            if (sysType.FullName == "System.Double")
                typeIsFloatingPoint = true;
            else if (sysType.FullName == "System.Single")
                typeIsFloatingPoint = true;
            else
                typeIsFloatingPoint = false;

            return typeIsFloatingPoint;
        }


        /// <summary>
        /// Evaluates whether or not the specified system type is decimal.
        /// </summary>
        /// <param name="sysType">Type to be evaluated.</param>
        /// <returns>Returns true if the specified type is a decimal data type; otherwise false.</returns>
        public static bool DataTypeIsDecimal(System.Type sysType)
        {
            bool typeIsDecimal = false;

            if (sysType.FullName == "System.Decimal")
                typeIsDecimal = true;
            else
                typeIsDecimal = false;

            return typeIsDecimal;
        }

        /// <summary>
        /// Evaluates whether or not the specified system type is a boolean.
        /// </summary>
        /// <param name="sysType">Type to be evaluated.</param>
        /// <returns>Returns true if the specified type is a boolean data type; otherwise false.</returns>
        public static bool DataTypeIsBoolean(System.Type sysType)
        {
            bool typeIsBoolean = false;

            if (sysType.FullName == "System.Boolean")
                typeIsBoolean = true;
            else
                typeIsBoolean = false;

            return typeIsBoolean;
        }
        /// <summary>
        /// Evaluates whether or not the specified system type is a DateTime.
        /// </summary>
        /// <param name="sysType">Type to be evaluated.</param>
        /// <returns>Returns true if the specified type is a DateTime data type; otherwise false.</returns>
        public static bool DataTypeIsDateTime(System.Type sysType)
        {
            bool typeIsDateTime = false;

            if (sysType.FullName == "System.DateTime")
                typeIsDateTime = true;
            else
                typeIsDateTime = false;

            return typeIsDateTime;
        }

        /// <summary>
        /// Evaluates whether or not the specified system type is a string.
        /// </summary>
        /// <param name="sysType">Type to be evaluated.</param>
        /// <returns>Returns true if the specified type is a string data type; otherwise false.</returns>
        public static bool DataTypeIsString(System.Type sysType)
        {
            bool typeIsString = false;

            if (sysType.FullName == "System.String")
                typeIsString = true;
            else
                typeIsString = false;

            return typeIsString;
        }

        /// <summary>
        /// Evaluates whether or not the specified system type is a Guid.
        /// </summary>
        /// <param name="sysType">Type to be evaluated.</param>
        /// <returns>Returns true if the specified type is the guid data type; otherwise false.</returns>
        public static bool DataTypeIsGuid(System.Type sysType)
        {
            bool typeIsGuid = false;

            if (sysType.FullName == "System.Guid")
                typeIsGuid = true;
            else
                typeIsGuid = false;

            return typeIsGuid;
        }

        /// <summary>
        /// Evaluates whether or not the specified system type is an Object.
        /// </summary>
        /// <param name="sysType">Type to be evaluated.</param>
        /// <returns>Returns true if the specified type is the object data type; otherwise false.</returns>
        public static bool DataTypeIsObject(System.Type sysType)
        {
            bool typeIsObject = false;

            if (sysType.FullName == "System.Object")
                typeIsObject = true;
            else
                typeIsObject = false;

            return typeIsObject;
        }

        /// <summary>
        /// Evaluates whether or not the specified system type is the char data type.
        /// </summary>
        /// <param name="sysType">Type to be evaluated.</param>
        /// <returns>Returns true if the specified type is the char data type; otherwise false.</returns>
        public static bool DataTypeIsChar(System.Type sysType)
        {
            bool typeIsChar = false;

            if (sysType.FullName == "System.Char")
                typeIsChar = true;
            else
                typeIsChar = false;

            return typeIsChar;
        }

        /// <summary>
        /// Evaluates whether or not the specified system type is one of the byte data types.
        /// </summary>
        /// <param name="sysType">Type to be evaluated.</param>
        /// <returns>Returns true if the specified type is an byte data type; otherwise false.</returns>
        public static bool DataTypeIsByte(System.Type sysType)
        {
            bool typeIsByte = false;

            if (sysType.FullName == "System.Byte")
                typeIsByte = true;
            else if (sysType.FullName == "System.SByte")
                typeIsByte = true;
            else
                typeIsByte = false;

            return typeIsByte;
        }

        /// <summary>
        /// Evaluates whether or not the specified system type is a char array.
        /// </summary>
        /// <param name="sysType">Type to be evaluated.</param>
        /// <returns>Returns true if the specified type is a char array data type; otherwise false.</returns>
        public static bool DataTypeIsCharArray(System.Type sysType)
        {
            bool typeIsCharArray = false;

            if (sysType.FullName.ToLower().Contains("system.char["))
                typeIsCharArray = true;
            else
                typeIsCharArray = false;

            return typeIsCharArray;
        }

        /// <summary>
        /// Evaluates whether or not the specified system type is a byte array.
        /// </summary>
        /// <param name="sysType">Type to be evaluated.</param>
        /// <returns>Returns true if the specified type is a byte array data type; otherwise false.</returns>
        public static bool DataTypeIsByteArray(System.Type sysType)
        {
            bool typeIsByteArray = false;

            if (sysType.FullName.ToLower().Contains("system.byte["))
                typeIsByteArray = true;
            else
                typeIsByteArray = false;

            return typeIsByteArray;
        }


    }//end class
}//end namespace
