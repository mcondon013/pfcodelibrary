using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using AppGlobals;


//Code initially retrieved from CodeProject.
//Code has since been modified for use by ProFast.
namespace PFSystemObjects
{
    /// <summary>
    /// Class for reading and writing the Windows registry.
    /// </summary>
    /// <remarks>if running on a 64-bit machine, the HKEY_LOCAL_MACHINE code show up under the Wow6432Node.</remarks>
    public class WindowsRegistry
    {

        private static StringBuilder _str = new StringBuilder();
        private static StringBuilder _msg = new StringBuilder();

#pragma warning disable 1591
        public struct pfRegistryValue
        {
            public string regValueName;
            public object regValue;
            public RegistryValueKind regValueKind;
        }
#pragma warning restore 1591


        //***************************************************************************************************
        //NOTE:        
        //if running on a 64-bit machine, the HKEY_LOCAL_MACHINE code show up under the Wow6432Node
        //***************************************************************************************************

        //methods for registry name/value pairs

        /// <summary>
        /// Verifies whether or not a particular value name exists.
        /// </summary>
        /// <param name="baseKey">HKEY_CLASSES_ROOT, HKEY_CURRENT_USER, HKEY_LOCAL_MACHINE, HKEY_USERS, HKEY_CONFIG</param>
        /// <param name="subKey">Full path of subkey. For example: SOFTWARE\CompanyName\InternalApps\WinAppComponents\Program01</param>
        /// <param name="valueName">Name of the value. For example: ConnectionString</param>
        /// <returns>True/False.</returns>
        /// <remarks>Throws an exception if an error is encountered during processing.</remarks>
        public static bool RegistryValueNameExists(string baseKey, string subKey, string valueName)
        {
            bool ret = false;


            try
            {
                RegistryHive? rh = GetRegistryHive(baseKey);
                RegistryKey rk = RegistryKey.OpenBaseKey((RegistryHive)rh, RegistryView.Registry32);
                RegistryKey rkSubKey = rk.OpenSubKey(subKey, false);
                Object value = null;
                if (rkSubKey != null)
                    value = rkSubKey.GetValue(valueName, null);
                if (rkSubKey != null && value != null)
                    ret = true;

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("RegistryValueNameExists method failed with error: ");
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }

            
            return ret;
        }

        /// <summary>
        /// Deletes a value from the registry.
        /// </summary>
        /// <param name="baseKey">HKEY_CLASSES_ROOT, HKEY_CURRENT_USER, HKEY_LOCAL_MACHINE, HKEY_USERS, HKEY_CONFIG</param>
        /// <param name="subKey">Full path of subkey. For example: SOFTWARE\CompanyName\InternalApps\WinAppComponents\Program01</param>
        /// <param name="valueName">Name of the value. For example: ConnectionString</param>
        /// <returns>True if success. Otherwise false.</returns>
        /// <remarks>Throws an exception if an error is encountered during processing.</remarks>
        public static bool DeleteRegistryValue(string baseKey, string subKey, string valueName)
        {
            bool ret = false;

            try
            {
                RegistryHive? rh = GetRegistryHive(baseKey);
                RegistryKey rk = RegistryKey.OpenBaseKey((RegistryHive)rh, RegistryView.Registry32);
                RegistryKey rkSubKey = rk.OpenSubKey(subKey, true);
                rkSubKey.DeleteValue(valueName, false);
                ret = true;
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("DeleteRegistryValue failed with error: ");
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }
                

            return ret;
        }


        /// <summary>
        /// Retrieve a registry value as string.
        /// </summary>
        /// <param name="baseKey">HKEY_CLASSES_ROOT, HKEY_CURRENT_USER, HKEY_LOCAL_MACHINE, HKEY_USERS, HKEY_CONFIG</param>
        /// <param name="subKey">Full path of subkey. For example: SOFTWARE\CompanyName\InternalApps\WinAppComponents\Program01</param>
        /// <param name="valueName">Name of the value. For example: ConnectionString</param>
        /// <param name="defaultValue">Value if registry path not found.</param>
        /// <returns>String representing value.</returns>
        /// <remarks>Throws an exception if an error is encountered during processing.</remarks>
        public static string GetRegistryValueString(string baseKey, string subKey, string valueName, object defaultValue)
        {
            pfRegistryValue rv = GetRegistryValue(baseKey, subKey, valueName, defaultValue);
            return (string)rv.regValue;
        }

        /// <summary>
        /// Retrieve a registry value as an integer (int).
        /// </summary>
        /// <param name="baseKey">HKEY_CLASSES_ROOT, HKEY_CURRENT_USER, HKEY_LOCAL_MACHINE, HKEY_USERS, HKEY_CONFIG</param>
        /// <param name="subKey">Full path of subkey. For example: SOFTWARE\CompanyName\InternalApps\WinAppComponents\Program01</param>
        /// <param name="valueName">Name of the value. For example: ConnectionString</param>
        /// <param name="defaultValue">Value if registry path not found.</param>
        /// <returns>Int representing value.</returns>
        /// <remarks>Throws an exception if an error is encountered during processing.</remarks>
        public static int GetRegistryValueInt(string baseKey, string subKey, string valueName, object defaultValue)
        {
            pfRegistryValue rv = GetRegistryValue(baseKey, subKey, valueName, defaultValue);
            if (rv.regValueKind != RegistryValueKind.DWord)
            {
                _msg.Length = 0;
                _msg.Append("Value for ");
                _msg.Append(valueName);
                _msg.Append(" is not an int: ");
                _msg.Append(rv.regValue.ToString());
                throw new System.Exception(_msg.ToString());
            }
            return (int)rv.regValue;
        }

        /// <summary>
        /// Retrieve a registry value as a long.
        /// </summary>
        /// <param name="baseKey">HKEY_CLASSES_ROOT, HKEY_CURRENT_USER, HKEY_LOCAL_MACHINE, HKEY_USERS, HKEY_CONFIG</param>
        /// <param name="subKey">Full path of subkey. For example: SOFTWARE\CompanyName\InternalApps\WinAppComponents\Program01</param>
        /// <param name="valueName">Name of the value. For example: ConnectionString</param>
        /// <param name="defaultValue">Value if registry path not found.</param>
        /// <returns>Long representing value.</returns>
        /// <remarks>Throws an exception if an error is encountered during processing.</remarks>
        public static long GetRegistryValueLong(string baseKey, string subKey, string valueName, object defaultValue)
        {
            pfRegistryValue rv = GetRegistryValue(baseKey, subKey, valueName, defaultValue);
            if (rv.regValueKind != RegistryValueKind.DWord && rv.regValueKind != RegistryValueKind.QWord)
            {
                _msg.Length = 0;
                _msg.Append("Value for ");
                _msg.Append(valueName);
                _msg.Append(" is not a long: ");
                _msg.Append(rv.regValue.ToString());
                throw new System.Exception(_msg.ToString());
            }
            return (long)rv.regValue;
        }

        /// <summary>
        /// Retrieve a registry value as a byte array.
        /// </summary>
        /// <param name="baseKey">HKEY_CLASSES_ROOT, HKEY_CURRENT_USER, HKEY_LOCAL_MACHINE, HKEY_USERS, HKEY_CONFIG</param>
        /// <param name="subKey">Full path of subkey. For example: SOFTWARE\CompanyName\InternalApps\WinAppComponents\Program01</param>
        /// <param name="valueName">Name of the value. For example: ConnectionString</param>
        /// <param name="defaultValue">Value if registry path not found.</param>
        /// <returns>Byte array representing value.</returns>
        /// <remarks>Throws an exception if an error is encountered during processing.</remarks>
        public static byte[] GetRegistryValueBinary(string baseKey, string subKey, string valueName, object defaultValue)
        {
            pfRegistryValue rv = GetRegistryValue(baseKey, subKey, valueName, defaultValue);
            return (byte[])rv.regValue;
        }

        /// <summary>
        /// Retrieve a registry value as an object.
        /// </summary>
        /// <param name="baseKey">HKEY_CLASSES_ROOT, HKEY_CURRENT_USER, HKEY_LOCAL_MACHINE, HKEY_USERS, HKEY_CONFIG</param>
        /// <param name="subKey">Full path of subkey. For example: SOFTWARE\CompanyName\InternalApps\WinAppComponents\Program01</param>
        /// <param name="valueName">Name of the value. For example: ConnectionString</param>
        /// <param name="defaultValue">Value if registry path not found.</param>
        /// <returns>Object representing value.</returns>
        /// <remarks>Throws an exception if an error is encountered during processing.</remarks>
        public static object GetRegistryValueObject(string baseKey, string subKey, string valueName, object defaultValue)
        {
            pfRegistryValue rv = GetRegistryValue(baseKey, subKey, valueName, defaultValue);
            return (object)rv.regValue;
        }

        /// <summary>
        /// Retrieve a registry value as a pfRegistryValue struct.
        /// </summary>
        /// <param name="baseKey">HKEY_CLASSES_ROOT, HKEY_CURRENT_USER, HKEY_LOCAL_MACHINE, HKEY_USERS, HKEY_CONFIG</param>
        /// <param name="subKey">Full path of subkey. For example: SOFTWARE\CompanyName\InternalApps\WinAppComponents\Program01</param>
        /// <param name="valueName">Name of the value. For example: ConnectionString</param>
        /// <param name="defaultValue">Value if registry path not found.</param>
        /// <returns>pfRegistryValue struct representing value.</returns>
        /// <remarks>Throws an exception if an error is encountered during processing.</remarks>
        public static pfRegistryValue GetRegistryValue(string baseKey, string subKey, string valueName, object defaultValue)
        {
            pfRegistryValue regValue=new pfRegistryValue();

            try
            {
                //verify routine will throw exceptions if any problems found with the parameter values
                VerifyGetValueParameters(baseKey, subKey, valueName, defaultValue);

                //return Convert.ToString(Registry.GetValue(@"HKEY_CURRENT_USER\Software\Yahoo\Common", "Sandy", "defaultOLD"));
                RegistryHive? rh = GetRegistryHive(baseKey);
                RegistryKey rk = RegistryKey.OpenBaseKey((RegistryHive)rh, RegistryView.Registry32);
                RegistryKey rkSubKey = rk.OpenSubKey(subKey, false);
                Object value = rkSubKey.GetValue(valueName, defaultValue);
                RegistryValueKind rvk = rkSubKey.GetValueKind(valueName);

                regValue.regValueName = valueName;
                regValue.regValue = value;
                regValue.regValueKind = rvk;
            }
            catch (System.IO.IOException ioex)
            {
                _msg.Length = 0;
                _msg.Append("Unable to find registry value: ");
                _msg.Append(BuildKeyName(baseKey,subKey));
                _msg.Append(": ");
                _msg.Append(valueName);
                _msg.Append(". Error message: ");
                _msg.Append(AppMessages.FormatErrorMessage(ioex));
                throw new System.Exception(_msg.ToString());
            }
            catch (System.Exception ex)
            {
                 _msg.Length = 0;
                _msg.Append("Error while trying to retrieve registry value: ");
                _msg.Append(BuildKeyName(baseKey,subKey));
                _msg.Append(": ");
                _msg.Append(valueName);
                _msg.Append(". Error message: ");
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
           }
            finally
            {
                ;
            }


            return regValue;

            
        }

        /// <summary>
        /// Writes a value to the registry.
        /// </summary>
        /// <param name="baseKey">HKEY_CLASSES_ROOT, HKEY_CURRENT_USER, HKEY_LOCAL_MACHINE, HKEY_USERS, HKEY_CONFIG</param>
        /// <param name="subKey">Full path of subkey. For example: SOFTWARE\CompanyName\InternalApps\WinAppComponents\Program01</param>
        /// <param name="valueName">Name of the value. For example: ConnectionString</param>
        /// <param name="value">Value to set for the value name.</param>
        /// <returns>pfRegistryValue struct.</returns>
        /// <remarks>Throws an exception if an error is encountered during processing.</remarks>
        public static pfRegistryValue SetRegistryValue(string baseKey, string subKey, string valueName, object value)
        {

            return SetRegistryValue(baseKey, subKey, valueName, value, RegistryValueKind.String);

        }

        /// <summary>
        /// Writes a value to the registry.
        /// </summary>
        /// <param name="baseKey">HKEY_CLASSES_ROOT, HKEY_CURRENT_USER, HKEY_LOCAL_MACHINE, HKEY_USERS, HKEY_CONFIG</param>
        /// <param name="subKey">Full path of subkey. For example: SOFTWARE\CompanyName\InternalApps\WinAppComponents\Program01</param>
        /// <param name="valueName">Name of the value. For example: ConnectionString</param>
        /// <param name="value">Value to set for the value name.</param>
        /// <param name="valueKind">Specifies type of value to write. (e.g. binary, string, expandstring, multistring, dword, Qword) See <see cref="RegistryValueKind"/> for more information. </param>
        /// <returns>pfRegistryValue struct.</returns>
        /// <remarks>Throws an exception if an error is encountered during processing.</remarks>
        public static pfRegistryValue SetRegistryValue(string baseKey, string subKey, string valueName, object value, RegistryValueKind valueKind)
        {

            pfRegistryValue regValue = new pfRegistryValue();

            try
            {
                VerifySetValueParameters(baseKey, subKey, valueName, value);

                //Registry.SetValue(@"HKEY_CURRENT_USER\Software\Yahoo\Common", "Sandy", "NEW");
                RegistryHive? rh = GetRegistryHive(baseKey);
                RegistryKey rk = RegistryKey.OpenBaseKey((RegistryHive)rh, RegistryView.Registry32);
                RegistryKey rkSubKey = rk.OpenSubKey(subKey,true);
                rkSubKey.SetValue(valueName, value, valueKind);
                Object updatedValue = rkSubKey.GetValue(valueName,"Unable to find value");
                RegistryValueKind rvk = rkSubKey.GetValueKind(valueName);
                regValue.regValueName = valueName;
                regValue.regValue = updatedValue;
                regValue.regValueKind = rvk;
            }

            catch (System.IO.IOException ioex)
            {
                _msg.Length = 0;
                _msg.Append("Unable to set registry value: ");
                _msg.Append(BuildKeyName(baseKey,subKey));
                _msg.Append(": ");
                _msg.Append(valueName);
                _msg.Append(". Error message: ");
                _msg.Append(AppMessages.FormatErrorMessage(ioex));
                throw new System.Exception(_msg.ToString());
            }
            catch (System.Exception ex)
            {
                 _msg.Length = 0;
                _msg.Append("Error while trying to set registry value: ");
                _msg.Append(BuildKeyName(baseKey,subKey));
                _msg.Append(": ");
                _msg.Append(valueName);
                _msg.Append(". Error message: ");
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
           }
            finally
            {
                ;
            }


            return regValue;


        }


        private static string BuildKeyName(string baseKey, string subKey)
        {
            _str.Length = 0;
            _str.Append(baseKey);
            if (_str.ToString().EndsWith(@"\") == false)
                _str.Append(@"\");
            _str.Append(subKey);
            //if (_str.ToString().EndsWith(@"\") == false)
            //    _str.Append(@"\");
            return _str.ToString();
        }

        private static void VerifyGetValueParameters(string baseKey, string subKey, string valueName, object defaultValue)
        {
            if (subKey.Length == 0 || valueName.Length == 0 || defaultValue == null)
                throw new System.Exception("Key, value name and default value must be specified.");
            RegistryHive? rh = GetRegistryHive(baseKey);
            if (rh == null)
            {
                _msg.Length = 0;
                _msg.Append("Invalid baseKey: ");
                _msg.Append(baseKey);
                throw new System.Exception(_msg.ToString());
            }

            
        }

        private static void VerifySetValueParameters(string baseKey, string subKey, string valueName, object value)
        {
            if (subKey.Length == 0 || valueName.Length == 0 || value == null)
                throw new System.Exception("Key, value name and value must be specified.");
            RegistryHive? rh = GetRegistryHive(baseKey);
            if (rh == null)
            {
                _msg.Length = 0;
                _msg.Append("Invalid baseKey: ");
                _msg.Append(baseKey);
                throw new System.Exception(_msg.ToString());
            }

        }

        private static RegistryHive? GetRegistryHive(string baseKey)
        {
           RegistryHive? rh = null;

            rh = baseKey == @"HKEY_LOCAL_MACHINE" ? RegistryHive.LocalMachine:
                  baseKey == @"HKEY_CLASSES_ROOT" ? RegistryHive.ClassesRoot :
                  baseKey == @"HKEY_CURRENT_USER" ? RegistryHive.CurrentUser :
                  baseKey == @"HKEY_CURRENT_CONFIG" ? RegistryHive.CurrentConfig :
                  baseKey == @"HKEY_DYN_DATA" ? RegistryHive.DynData :
                  baseKey == @"HKEY_PERFORMANCE_DATA" ? RegistryHive.PerformanceData :
                  baseKey == @"HKEY_USERS" ? RegistryHive.Users :
                                             (RegistryHive?) null;

            return rh;
        }


        //methods for registry key processing

        /// <summary>
        /// Verifies whether or not a key/subkey path exists.
        /// </summary>
        /// <param name="baseKey">HKEY_CLASSES_ROOT, HKEY_CURRENT_USER, HKEY_LOCAL_MACHINE, HKEY_USERS, HKEY_CONFIG</param>
        /// <param name="subKey">Full path of subkey. For example: SOFTWARE\CompanyName\InternalApps\WinAppComponents\Program01</param>
        /// <returns>True or False.</returns>
        /// <remarks>Throws an exception if an error is encountered during processing.</remarks>
        public static bool RegistrySubKeyExists(string baseKey, string subKey)
        {
            bool  ret = false;


            try
            {
                RegistryHive? rh = GetRegistryHive(baseKey);
                RegistryKey rk = RegistryKey.OpenBaseKey((RegistryHive)rh, RegistryView.Registry32);
                RegistryKey rkSubKey = rk.OpenSubKey(subKey, false);
                if (rkSubKey != null)
                    ret = true;
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("RegistryKeyExists method failed with error: ");
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }


            return ret;
        }

        /// <summary>
        /// Deletes subkey from registry.
        /// </summary>
        /// <param name="baseKey">HKEY_CLASSES_ROOT, HKEY_CURRENT_USER, HKEY_LOCAL_MACHINE, HKEY_USERS, HKEY_CONFIG</param>
        /// <param name="subKey">Full path of subkey. For example: SOFTWARE\CompanyName\InternalApps\WinAppComponents\Program01</param>
        /// <returns>True if success; otherwise false.</returns>
        /// <remarks>Throws an exception if an error is encountered during processing.</remarks>
        public static bool DeleteRegistrySubKey(string baseKey, string subKey)
        {
            bool ret = false;

            try
            {
                RegistryHive? rh = GetRegistryHive(baseKey);
                RegistryKey rk = RegistryKey.OpenBaseKey((RegistryHive)rh, RegistryView.Registry32);
                rk.DeleteSubKey(subKey,false);
                RegistryKey rkSubKey = rk.OpenSubKey(subKey, false);
                if (rkSubKey == null)
                    ret = true;
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("DeleteRegistrySubKey failed with error: ");
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }


            return ret;
        }


        /// <summary>
        /// Creates the registry subKey specified by the parameters.
        /// </summary>
        /// <param name="baseKey">HKEY_CLASSES_ROOT, HKEY_CURRENT_USER, HKEY_LOCAL_MACHINE, HKEY_USERS, HKEY_CONFIG</param>
        /// <param name="subKey">Full path of subkey. For example: SOFTWARE\CompanyName\InternalApps\WinAppComponents\Program01</param>
        /// <returns>True if success; otherwise false.</returns>
        /// <remarks>Throws an exception if an error is encountered during processing.</remarks>
        public static bool CreateRegistrySubKey(string baseKey, string subKey)
        {
            bool ret = false;

            try
            {
                RegistryHive? rh = GetRegistryHive(baseKey);
                RegistryKey rk = RegistryKey.OpenBaseKey((RegistryHive)rh, RegistryView.Registry32);
                //RegistryKey rkSubKey = Registry.LocalMachine.CreateSubKey(subKey);
                rk.CreateSubKey(subKey,RegistryKeyPermissionCheck.ReadWriteSubTree,RegistryOptions.None);
                RegistryKey rkSubKey = rk.OpenSubKey(subKey, false);
                if (rkSubKey != null)
                    ret = true;
            }

           catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error while trying to create registry subkey: ");
                _msg.Append(BuildKeyName(baseKey, subKey));
                _msg.Append(". Error message: ");
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }


            return ret;


        }


    }//end class
}//end namespace
