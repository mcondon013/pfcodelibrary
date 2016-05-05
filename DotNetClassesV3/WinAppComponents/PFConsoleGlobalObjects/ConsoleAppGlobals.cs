//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2013
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace PFConsoleGlobalObjects
{
    /// <summary>
    /// Class that console applications can use for displaying application messages and logging events to text and windows logs.
    /// </summary>
    public class ConsoleMessages
    {
#pragma warning disable 1591
        public enum pfConsoleMessageType
        {
            Information = 0,
            Alert = 1,
            Warning = 2,
            Error = 3
        }

        // Fields
        private static string _appLogFilename = "";
        private static bool _copyConsoleOutputToAppLog;
        private static string _prodName = AppInfo.AssemblyProduct.Length > 0 ? AppInfo.AssemblyProduct : AppInfo.AssemblyName.Length > 0 ? AppInfo.AssemblyName : Environment.MachineName;
        private static string _defaultMessageBoxCaption = _prodName;

        // Properties
        public static string AppLogFilename
        {
            get
            {
                return _appLogFilename;
            }
            set
            {
                _appLogFilename = value;
            }
        }

        public static bool CopyConsoleOutputToAppLog
        {
            get
            {
                return ConsoleMessages._copyConsoleOutputToAppLog;
            }
            set
            {
                ConsoleMessages._copyConsoleOutputToAppLog = value;
            }
        }

        public static string DefaultMessageBoxCaption
        {
            get
            {
                return _defaultMessageBoxCaption;
            }
            set
            {
                _defaultMessageBoxCaption = value;
            }
        }

        // Methods
        public static void DisplayAlertMessage(string message)
        {
            DisplayAlertMessage(message, false, false);
        }

        public static void DisplayAlertMessage(string message, bool writeToAppLog)
        {
            DisplayAlertMessage(message, writeToAppLog, false);
        }

        public static void DisplayAlertMessage(string message, bool writeToAppLog, bool writeToEventLog)
        {
            try
            {
                DisplayMessage(message, pfConsoleMessageType.Alert, writeToAppLog, writeToEventLog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error in DisplayMessage" + ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public static void DisplayErrorMessage(Exception ex)
        {
            DisplayErrorMessage(ex, false, false);
        }

        public static void DisplayErrorMessage(string message)
        {
            DisplayErrorMessage(message, false, false);
        }

        public static void DisplayErrorMessage(Exception ex, bool writeToAppLog)
        {
            DisplayErrorMessage(ex, writeToAppLog, false);
        }

        public static void DisplayErrorMessage(string message, bool writeToAppLog)
        {
            DisplayErrorMessage(message, writeToAppLog, false);
        }

        public static void DisplayErrorMessage(Exception pEx, bool writeToAppLog, bool writeToEventLog)
        {
            try
            {
                DisplayMessage(FormatErrorMessage(pEx), pfConsoleMessageType.Error, writeToAppLog, writeToEventLog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error in DisplayMessage" + ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public static void DisplayErrorMessage(string message, bool writeToAppLog, bool writeToEventLog)
        {
            try
            {
                DisplayMessage(message, pfConsoleMessageType.Error, writeToAppLog, writeToEventLog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error in DisplayMessage" + ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public static void DisplayInfoMessage(string message)
        {
            DisplayInfoMessage(message, false, false);
        }

        public static void DisplayInfoMessage(string message, bool writeToAppLog)
        {
            DisplayInfoMessage(message, writeToAppLog, false);
        }

        public static void DisplayInfoMessage(string message, bool writeToAppLog, bool writeToEventLog)
        {
            try
            {
                DisplayMessage(message, pfConsoleMessageType.Information, writeToAppLog, writeToEventLog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error in DisplayMessage" + ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public static void DisplayMessage(string message, pfConsoleMessageType messageType)
        {
            DisplayMessage(message, messageType, false, false);
        }

        public static void DisplayMessage(string message, pfConsoleMessageType messageType, bool writeToAppLog)
        {
            DisplayMessage(message, messageType, writeToAppLog, false);
        }

        public static void DisplayMessage(string message, pfConsoleMessageType messageType, bool writeToAppLog, bool writeToEventLog)
        {
            string messagePrefix = string.Empty;
            if (messageType != pfConsoleMessageType.Information)
                messagePrefix = messageType.ToString() + ": ";

            try
            {
                if (writeToAppLog)
                {
                    WriteToAppLog(message, messageType);
                }
                if (writeToEventLog)
                {
                    EventLogEntryType eventLogMessageType;
                    switch (messageType)
                    {
                        case pfConsoleMessageType.Error:
                            eventLogMessageType = EventLogEntryType.Error;
                            break;
                        case pfConsoleMessageType.Warning:
                            eventLogMessageType = EventLogEntryType.Warning;
                            break;
                        default:
                            eventLogMessageType = EventLogEntryType.Information;
                            break;

                    }
                    WriteToEventLog(message, eventLogMessageType);
                }
                Console.WriteLine(messagePrefix + message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error in DisplayMessage" + ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public static void DisplayWarningMessage(string message)
        {
            DisplayWarningMessage(message, false, false);
        }

        public static void DisplayWarningMessage(string message, bool writeToAppLog)
        {
            DisplayWarningMessage(message, writeToAppLog, false);
        }

        public static void DisplayWarningMessage(string message, bool writeToAppLog, bool writeToEventLog)
        {
            try
            {
                DisplayMessage(message, pfConsoleMessageType.Warning, writeToAppLog, writeToEventLog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error in DisplayMessage" + ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public static string FormatErrorMessage(Exception pex)
        {
            StringBuilder errMsg = new StringBuilder();
            try
            {
                errMsg.Append("Source: ");
                errMsg.Append(pex.Source);
                errMsg.Append(": \r\n");
                errMsg.Append(pex.Message);
                Exception iex = pex.InnerException;
                while (iex != null)
                {
                    errMsg.Append("\r\n\r\nInner Exception:\r\nSource: ");
                    errMsg.Append(iex.Source);
                    errMsg.Append(": \r\n");
                    errMsg.Append(iex.Message);
                    iex = iex.InnerException;
                }
            }
            catch (Exception ex)
            {
                errMsg.Append("\r\n*** UNEXPECTED ERROR: ***\r\nUnable to format error message.\n");
                errMsg.Append("Source: ");
                errMsg.Append(ex.Source);
                errMsg.Append(": \r\n");
                errMsg.Append(ex.Message);
            }
            return errMsg.ToString();
        }

        public static string FormatErrorMessageWithStackTrace(Exception pex)
        {
            StringBuilder errMsg = new StringBuilder();
            try
            {
                errMsg.Append("Source: ");
                errMsg.Append(pex.Source);
                errMsg.Append(": \r\n");
                errMsg.Append(pex.Message);
                errMsg.Append(": \r\n");
                errMsg.Append(pex.StackTrace);
                Exception iex = pex.InnerException;
                while (iex != null)
                {
                    errMsg.Append("\r\n\r\nInner Exception:\r\nSource: ");
                    errMsg.Append(iex.Source);
                    errMsg.Append(": \r\n");
                    errMsg.Append(iex.Message);
                    errMsg.Append(": \r\n");
                    errMsg.Append(iex.StackTrace);
                    iex = iex.InnerException;
                }
            }
            catch (Exception ex)
            {
                errMsg.Append("\r\n*** UNEXPECTED ERROR: ***\r\nUnable to format error message.\n");
                errMsg.Append("Source: ");
                errMsg.Append(ex.Source);
                errMsg.Append(": \r\n");
                errMsg.Append(ex.Message);
            }
            return errMsg.ToString();

        }

        public static void WriteToAppLog(string message)
        {
            WriteToAppLog(message, pfConsoleMessageType.Information);
        }

        public static void WriteToAppLog(string message, pfConsoleMessageType messageType)
        {
            string appLogFilename = "";
            string separator = new string('-', 50);
            string msgType = "";
            try
            {
                if (_appLogFilename.Length > 0)
                {
                    if (Path.GetDirectoryName(_appLogFilename).Length == 0)
                        appLogFilename = Environment.CurrentDirectory + @"\" + _appLogFilename;
                    else
                        appLogFilename = _appLogFilename;
                }
                else
                {
                    appLogFilename = Environment.CurrentDirectory + @"\" + _prodName + "_log.txt";
                }
                StreamWriter log = new StreamWriter(appLogFilename, true);

                switch (messageType)
                {
                    case pfConsoleMessageType.Error:
                        msgType = "Error:\n";
                        break;
                    case pfConsoleMessageType.Warning:
                        msgType = "Warning:\n";
                        break;
                    default:
                        msgType = string.Empty;
                        break;
                }

                if (_copyConsoleOutputToAppLog)
                {
                    //console output likely being copied to app log to simulate redirecting of output
                    //output redirection does not work when runas administrator processing being done by some apps
                    if (messageType == pfConsoleMessageType.Warning || messageType == pfConsoleMessageType.Error)
                        log.WriteLine(messageType.ToString() + ": " + message);
                    else
                        log.WriteLine(message);
                }
                else
                {
                    log.WriteLine(separator);
                    log.WriteLine(msgType + DateTime.Now.ToString());
                    log.WriteLine(separator);
                    log.WriteLine(message);
                    log.WriteLine("");
                }
                log.Flush();
                log.Close();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace;
                Console.WriteLine(errorMessage);
            }
        }

        public static void WriteToEventLog(string message, EventLogEntryType messageType)
        {
            EventLog log = new EventLog("Application", ".", _prodName);
            try
            {
                log.WriteEntry(message, messageType);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace;
                Console.WriteLine(errorMessage);
            }
            finally
            {
                log.Close();
            }
        }

    }//end class

#pragma warning restore 1591

}//end namespace
