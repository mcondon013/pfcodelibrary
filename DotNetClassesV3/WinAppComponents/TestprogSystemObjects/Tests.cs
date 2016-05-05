using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using PFSystemObjects;

namespace TestprogSystemObjects
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
        public static void WinAppConsoleTest()
        {

            try
            {
                _msg.Length = 0;
                _msg.Append("WinAppConsoleTest started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                WinAppConsole.Create();

                _msg.Length = 0;
                _msg.Append("Current date/time is ");
                _msg.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                _msg.Append(Environment.NewLine);
                Console.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("This is a line of test text.\r\n");
                _msg.Append("Press any key to exit.\r\n");
                Console.WriteLine(_msg.ToString());


                Console.ReadLine();

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
                WinAppConsole.Destroy();
                _msg.Length = 0;
                _msg.Append("... WinAppConsoleTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void DynamicLoadTests()
        {
            //string pathToDLL = @"C:\ProFast\Projects\DotNetClassesV3\WinAppComponents\TestprogSystemObjectsDLL\bin\Release\TestprogSystemObjectsDLL.dll";
            string pathToDLL = System.IO.Path.GetFullPath(@"..\..\..\..\..\DotNetClassesV3\WinAppComponents\TestprogSystemObjectsDLL\bin\Release\TestprogSystemObjectsDLL.dll");
            try
            {
                _msg.Length = 0;
                _msg.Append("DynamicLoadTests started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                Type testClass = WindowsAssembly.LoadType(pathToDLL, "TestprogSystemObjectsDLL.PFInitClassExtended");

                WindowsAssembly.InvokeVoidMethodNoArguments(testClass, "DisplayTestMessage");

                //no error will be thrown on this call, even though routine is a void method and no parameters required
                object nores = WindowsAssembly.InvokeMethod(testClass, "DisplayTestMessage", null);

                int x = 5;
                int y = 8;
                int z = -1;

                object[] parms = new object[2] {x, y};
                object res = WindowsAssembly.InvokeMethod(testClass, "AddTwoNumbers", parms);
                z = (int)res;

                _msg.Length = 0;
                _msg.Append("Result from AddTwoNumbers is ");
                _msg.Append(z.ToString());
                Program._messageLog.WriteLine(_msg.ToString());

                object obj = WindowsAssembly.InstantiateType(testClass);
                object val = WindowsAssembly.GetPropertyValue(obj, "TestDouble", null);
                _msg.Length = 0;
                _msg.Append("TestDouble value is ");
                _msg.Append(val.ToString());
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
                _msg.Append("... DynamicLoadTests finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void WriteToEventLogTest()
        {
            WindowsEventLog evt = null;
            try
            {
                _msg.Length = 0;
                _msg.Append("WriteToEventLogTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                evt = new WindowsEventLog(WindowsEventLog.EventLogName.Application, ".", "PFApps");

                evt.WriteEntry("Test message from testprog ... yes indeed!", WindowsEventLog.WindowsEventLogEntryType.Information);

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
                _msg.Append("\r\n... WriteToEventLogTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void PrinterInfoTest()
        {
            string[] printerNames;

            try
            {
                _msg.Length = 0;
                _msg.Append("PrinterInfoTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("Default printer has been defined? ");
                _msg.Append(SysInfo.DefaultPrinterIsDefined().ToString());
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("Default printer name: ");
                _msg.Append(SysInfo.GetDefaultPrinterName());
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());

                printerNames = SysInfo.GetPrinterNameList();
                _msg.Length = 0;
                if (printerNames == null)
                {
                    _msg.Append("No printers defined on this system.");
                    _msg.Append(Environment.NewLine);
                }
                else
                {
                    _msg.Append("Number of printers: ");
                    _msg.Append(printerNames.Length.ToString());
                    _msg.Append(Environment.NewLine);
                    foreach (string nm in printerNames)
                    {
                        _msg.Append(nm);
                        _msg.Append(Environment.NewLine);
                    }
                }
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
                _msg.Append("\r\n... PrinterInfoTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void RunRegistryTest()
        {
            string value = string.Empty;

            try
            {
                WindowsRegistry.pfRegistryValue retValue;
                //bool regEntryExists = false;

                retValue = WindowsRegistry.GetRegistryValue("HKEY_LOCAL_MACHINE", @"SOFTWARE\ProFastComputing\InternalApps\WinAppComponents\Testprog", "Name01", "default value is this");
                _msg.Length = 0;
                _msg.Append("Registry read: ");
                _msg.Append(retValue.regValue);
                Program._messageLog.WriteLine(_msg.ToString());

                _str.Length = 0;
                _str.Append("Updated Value ");
                _str.Append(DateTime.Now.ToString("HH_mm_ss"));
                retValue = WindowsRegistry.SetRegistryValue("HKEY_LOCAL_MACHINE", @"SOFTWARE\ProFastComputing\InternalApps\WinAppComponents\Testprog", "Name01", (string)_str.ToString());
                _msg.Length = 0;
                _msg.Append("Registry write: ");
                _msg.Append(retValue.regValue);
                _msg.Append(" is the value that was set.");
                Program._messageLog.WriteLine(_msg.ToString());

                _str.Length = 0;
                _str.Append("Modified Value ");
                _str.Append(DateTime.Now.ToString("HHmmss"));
                retValue = WindowsRegistry.SetRegistryValue("HKEY_LOCAL_MACHINE", @"SOFTWARE\ProFastComputing\InternalApps\WinAppComponents\Testprog", "Name02", (string)_str.ToString());
                _msg.Length = 0;
                _msg.Append("Registry write: ");
                _msg.Append(retValue.regValue);
                _msg.Append(" is the value that was set.");
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("Name01 ");
                if (WindowsRegistry.RegistryValueNameExists("HKEY_LOCAL_MACHINE", @"SOFTWARE\ProFastComputing\InternalApps\WinAppComponents\Testprog", "Name01"))
                {
                    _msg.Append("exists.");
                }
                else
                {
                    _msg.Append("does not exist.");
                }
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("Name01 for Testprog55 ");
                if (WindowsRegistry.RegistryValueNameExists("HKEY_LOCAL_MACHINE", @"SOFTWARE\ProFastComputing\InternalApps\WinAppComponents\Testprog55", "Name01"))
                {
                    _msg.Append("exists.");
                }
                else
                {
                    _msg.Append("does not exist.");
                }
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("Name02 ");
                if (WindowsRegistry.RegistryValueNameExists("HKEY_LOCAL_MACHINE", @"SOFTWARE\ProFastComputing\InternalApps\WinAppComponents\Testprog", "Name02"))
                {
                    _msg.Append("exists.");
                }
                else
                {
                    _msg.Append("does not exist.");
                }
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("Name02 Delete result: ");
                if (WindowsRegistry.DeleteRegistryValue("HKEY_LOCAL_MACHINE", @"SOFTWARE\ProFastComputing\InternalApps\WinAppComponents\Testprog", "Name02"))
                    _msg.Append("Succeeded");
                else
                    _msg.Append("Failed");
                Program._messageLog.WriteLine(_msg.ToString());


                _msg.Length = 0;
                _msg.Append("DeleteRegistrySubKey Testprog2: ");
                if (WindowsRegistry.DeleteRegistrySubKey("HKEY_LOCAL_MACHINE", @"SOFTWARE\ProFastComputing\InternalApps\WinAppComponents\Testprog2"))
                    _msg.Append("Succeeded");
                else
                    _msg.Append("Failed");
                Program._messageLog.WriteLine(_msg.ToString());


                _msg.Length = 0;
                _msg.Append("CreateRegistrySubKey Testprog2: ");
                if (WindowsRegistry.CreateRegistrySubKey("HKEY_LOCAL_MACHINE", @"SOFTWARE\ProFastComputing\InternalApps\WinAppComponents\Testprog2"))
                    _msg.Append("Succeeded");
                else
                    _msg.Append("Failed");
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("Registry SubKey Testprog2 was ");
                if (WindowsRegistry.RegistrySubKeyExists("HKEY_LOCAL_MACHINE", @"SOFTWARE\ProFastComputing\InternalApps\WinAppComponents\Testprog2"))
                    _msg.Append("found.");
                else
                    _msg.Append("not found.");
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("CreateRegistrySubKey: ");
                if (WindowsRegistry.CreateRegistrySubKey("HKEY_LOCAL_MACHINE", @"SOFTWARE\ProFastComputing2"))
                    _msg.Append("Succeeded");
                else
                    _msg.Append("Failed");
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("Registry SubKey was ");
                if (WindowsRegistry.RegistrySubKeyExists("HKEY_LOCAL_MACHINE", @"SOFTWARE\ProFastComputing2"))
                    _msg.Append("found.");
                else
                    _msg.Append("not found.");
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("DeleteRegistrySubKey: ");
                if (WindowsRegistry.DeleteRegistrySubKey("HKEY_LOCAL_MACHINE", @"SOFTWARE\ProFastComputing2"))
                    _msg.Append("Succeeded");
                else
                    _msg.Append("Failed");
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("Registry SubKey was ");
                if (WindowsRegistry.RegistrySubKeyExists("HKEY_LOCAL_MACHINE", @"SOFTWARE\ProFastComputing2"))
                    _msg.Append("found.");
                else
                    _msg.Append("not found.");
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
                ;

            }

        }//end RunRegistryTest


                      

    }//end class
}//end namespace
