//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PFSystemObjects
{
    /// <summary>
    /// Class contains various methods for creating and instantiating objects dynamically.
    /// </summary>
    public class WindowsAssembly
    {
        //private work variables
        private static StringBuilder _msg = new StringBuilder();

        //private varialbles for properties

        //constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        public WindowsAssembly()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Loads assembly from specified file location 
        /// </summary>
        /// <param name="fileName">Path to the file containing the assembly.</param>
        /// <returns>Assembly object.</returns>
        public static Assembly Load(string fileName)
        {
            return Assembly.LoadFrom(fileName);
        }

        /// <summary>
        /// Loads assembly and then retrieves requested type from the assembly.
        /// </summary>
        /// <param name="fileName">Path to the file containing the assembly.</param>
        /// <param name="pType">The full name of the type.</param>
        /// <returns>Type object.</returns>
        public static Type LoadType(string fileName, string pType)
        {
            Assembly assembly = Assembly.LoadFrom(fileName);

            Type type = assembly.GetType(pType);

            return type;

        }

        /// <summary>
        /// Loads and instantiates requested type from specified assembly file.
        /// </summary>
        /// <param name="fileName">Path to the file containing the assembly.</param>
        /// <param name="pType">The full name of the type.</param>
        /// <returns>Instantiated object of the requested type.</returns>
        public static object LoadAndInstantiateType(string fileName, string pType)
        {
            Assembly assembly = Assembly.LoadFrom(fileName);

            Type type = assembly.GetType(pType);

            object instanceOfType = Activator.CreateInstance(type);

            return instanceOfType;
        }

        /// <summary>
        /// Retrieves requested type from the specified assembly.
        /// </summary>
        /// <param name="asm">Assembly containing the type.</param>
        /// <param name="pType">Type to get.</param>
        /// <returns>Type object.</returns>
        public static Type GetType(Assembly asm, string pType)
        {
            Type type = asm.GetType(pType);

            return type;
        }

        /// <summary>
        /// Retrieves requested type from the currently executing assembly.
        /// </summary>
        /// <param name="pType">The full name of the type.</param>
        /// <returns>Type object.</returns>
        public static Type GetType(string pType)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            Type type = assembly.GetType(pType);

            return type;

        }

        /// <summary>
        /// Retrieves a value for the specified property contained in the object instance.
        /// </summary>
        /// <param name="pInstance">Instance of object containing property value to be returned.</param>
        /// <param name="pProperty">Name of the property.</param>
        /// <param name="pIndex">Null if property is not indexed. Otherwise specify the required index or indexes.</param>
        /// <returns>Object containing the value of the property.</returns>
        public static object GetPropertyValue(object pInstance, string pProperty, object[] pIndex)
        {
            Type type = pInstance.GetType();

            PropertyInfo prop = type.GetProperty(pProperty);

            object retval = prop.GetValue(pInstance, pIndex);

            return retval;
        }

        /// <summary>
        /// Activates an instance of the specified type from the currently executing assembly.
        /// </summary>
        /// <param name="pType">Type to be instantiated.</param>
        /// <returns>Instantiated object of the requested type.</returns>
        public static object InstantiateType(Type pType)
        {
            object instanceOfType = Activator.CreateInstance(pType);

            return instanceOfType;
        }

        /// <summary>
        /// Invokes a method on the specified class. Method does not have any parameters and does not have a return value.
        /// </summary>
        /// <param name="pType">Type containing method to run.</param>
        /// <param name="methodName">Name of the method.</param>
        public static void InvokeVoidMethodNoArguments(Type pType, string methodName)
        {
            //// Get a type from the string 
            //Type type = Type.GetType(className);
            // Create an instance of that type
            Object typeInstance = Activator.CreateInstance(pType);
            // Retrieve the method you are looking for
            MethodInfo methodInfo = pType.GetMethod(methodName);
            // Invoke the method on the instance we created above
            methodInfo.Invoke(typeInstance, null);
        }

        /// <summary>
        /// Invokes a method on the specified class. Method has parameters but does not have a return value.
        /// </summary>
        /// <param name="pType">Type containing method to run.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="args">Array of argument values to pass to the method.</param>
        public static void InvokeVoidMethodWithArguments(Type pType, string methodName, object[] args)
        {
            Object typeInstance = Activator.CreateInstance(pType);
            MethodInfo methodInfo = pType.GetMethod(methodName);
            methodInfo.Invoke(typeInstance, args);
        }

        /// <summary>
        /// Invokes a method on the specified class. Method takes arguments. 
        /// </summary>
        /// <param name="pType">Type containing method to run.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="args">Array of argument values to pass to the method.</param>
        /// <returns>Object containing the return value.</returns>
        public static object InvokeMethod(Type pType, string methodName, object[] args)
        {
            Object typeInstance = Activator.CreateInstance(pType);
            MethodInfo methodInfo = pType.GetMethod(methodName);
            Object retval = methodInfo.Invoke(typeInstance, args);
            return retval;
        }


    }//end class
}//end namespace
