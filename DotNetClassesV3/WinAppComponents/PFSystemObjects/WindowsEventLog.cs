//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using AppGlobals;
using System.IO;

namespace PFSystemObjects
{
    /// <summary>
    /// Class encapsulates functionality to write to the Windows event log.
    /// </summary>
    public class WindowsEventLog
    {
#pragma warning disable 1591
        public enum EventLogName
        {
            Unknown = 0,
            Application = 1,
            System = 2,
            Security = 3,
            Setup = 4
        }

        public enum WindowsEventLogEntryType
        {
            Unknown = 0,
            Error = EventLogEntryType.Error,
            Warning = EventLogEntryType.Warning,
            Information = EventLogEntryType.Information,
            SuccessAudit = EventLogEntryType.SuccessAudit,
            FailureAudit = EventLogEntryType.FailureAudit
        }
#pragma warning restore 1591

        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties

        private EventLog _eventLog = null;
        private string _logName = "Application";
        private string _machineName = ".";
        private string _eventSource = "PFApps";             //event source must already exist
        private static string _eventSourceInitializer = AppConfig.GetStringValueFromConfigFile("EventSourceInitializer", string.Empty);
        private static string _currentWorkingDirectory = Environment.CurrentDirectory;
        private static string _outputMessages = string.Empty;
        private static string _errorMessages = string.Empty;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public WindowsEventLog()
        {
            InitEventLog(string.Empty, string.Empty, string.Empty);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logName">Name of the log to write to.</param>
        public WindowsEventLog(EventLogName logName)
        {
            InitEventLog(logName.ToString(), string.Empty, string.Empty);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logName">Name of the log to write to.</param>
        /// <param name="machineName">Name of machine where log is located.</param>
        public WindowsEventLog(EventLogName logName, string machineName)
        {
            InitEventLog(logName.ToString(), machineName, string.Empty);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logName">Name of the log to write to.</param>
        /// <param name="machineName">Name of machine where log is located.</param>
        /// <param name="eventSource">Source of the event log entries.</param>
        public WindowsEventLog(EventLogName logName, string machineName, string eventSource)
        {
            InitEventLog(logName.ToString(), machineName, eventSource);
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logName">Name of the log to write to.</param>
        public WindowsEventLog(string logName)
        {
            InitEventLog(logName, string.Empty, string.Empty);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logName">Name of the log to write to.</param>
        /// <param name="machineName">Name of machine where log is located.</param>
        public WindowsEventLog(string logName, string machineName)
        {
            InitEventLog(logName, machineName, string.Empty);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logName">Name of the log to write to.</param>
        /// <param name="machineName">Name of machine where log is located.</param>
        /// <param name="eventSource">Source of the event log entries.</param>
        public WindowsEventLog(string logName, string machineName, string eventSource)
        {
            InitEventLog(logName, machineName, eventSource);
        }

        // this routine assumes that the eventSource already exists.
        private void InitEventLog(string logName, string machineName, string eventSource)
        {
            if (logName == "Unknown" || String.IsNullOrEmpty(logName))
                _logName = AppConfig.GetStringValueFromConfigFile("WindowsEventLog", "Application");
            else
                _logName = logName;

            if (String.IsNullOrEmpty(machineName))
                _machineName = AppConfig.GetStringValueFromConfigFile("WindowsEventLogMachineName", ".");
            else
                _machineName = machineName;

            if (String.IsNullOrEmpty(eventSource))
                _eventSource = AppConfig.GetStringValueFromConfigFile("WindowsEventLogEventSource", "PFApps");
            else
                _eventSource = eventSource;


            _eventLog = new EventLog(_logName, _machineName, _eventSource);

        }

        //properties

        /// <summary>
        /// EventLog Property.
        /// </summary>
        public System.Diagnostics.EventLog EventLog
        {
            get
            {
                return _eventLog;
            }
        }

        /// <summary>
        /// LogName Property.
        /// </summary>
        public string LogName
        {
            get
            {
                return _logName;
            }
            set
            {
                _logName = value;
            }
        }

        /// <summary>
        /// MachineName Property.
        /// </summary>
        public string MachineName
        {
            get
            {
                return _machineName;
            }
            set
            {
                _machineName = value;
            }
        }

        /// <summary>
        /// EventSource Property.
        /// </summary>
        public string EventSource
        {
            get
            {
                return _eventSource;
            }
            set
            {
                _eventSource = value;
            }
        }

        /// <summary>
        /// Full path to the executable that can initialize an event source. 
        /// </summary>
        /// <remarks>Value can be specified in an application's config file by setting the value for the EventSourceInitializer key.</remarks>
        public static string EventSourceInitializer
        {
            get
            {
                return _eventSourceInitializer;
            }
            set
            {
                _eventSourceInitializer = value;
            }
        }

        /// <summary>
        /// CurrentWorkingDirectory Property.
        /// </summary>
        public static string CurrentWorkingDirectory
        {
            get
            {
                return _currentWorkingDirectory;
            }
            set
            {
                _currentWorkingDirectory = value;
            }
        }

        /// <summary>
        /// Returns output messages, if any, generated by the process that registers the event source name.
        /// </summary>
        public static string OutputMessages
        {
            get
            {
                return _outputMessages;
            }
        }

        /// <summary>
        /// Returns error messages, if any, generated by the process that registers the event source name.
        /// </summary>
        public static string ErrorMessages
        {
            get
            {
                return _errorMessages;
            }
        }




        //methods

        /// <summary>
        /// Routine to write messages to an event log.
        /// </summary>
        /// <param name="message">Text of the message to be written.</param>
        /// <param name="entryType">Type of message: Error, Warning, Information, SuccessAudit or FailureAudit.</param>
        public void WriteEntry (string message, WindowsEventLogEntryType entryType)
        {
            if(EventLog.SourceExists(_eventSource))
                _eventLog.WriteEntry(message, (EventLogEntryType)entryType);
        }

        /// <summary>
        /// Determines whether or not an event source is registered on the local computer.
        /// </summary>
        /// <param name="eventSourceName">Name of the event source.</param>
        /// <returns>True if event source name is registered. Otherwise, returns false.</returns>
        public static bool SourceExists(string eventSourceName)
        {
            return EventLog.SourceExists(eventSourceName);
        }

        /// <summary>
        /// Determines whether or not an event source is registered on a specified computer.
        /// </summary>
        /// <param name="eventSourceName">Name of the event source.</param>
        /// <param name="machineName">Name of the computer on which to look, or "." for the local computer.</param>
        /// <returns>True if event source name is registered. Otherwise, returns false.</returns>
        public static bool SourceExists(string eventSourceName, string machineName)
        {
            return EventLog.SourceExists(eventSourceName, machineName);
        }

        /// <summary>
        /// Method for registering a Windows Event Log event source.
        /// </summary>
        /// <param name="logName">Name of log to which messages will be written. Usually is the Application log.</param>
        /// <param name="machineName">Name of the machine on which the log resides. Specify . or localhost for local machine.</param>
        /// <param name="eventSourceName">Name of the event source. Usually will be PFApps for ProFast applications.</param>
        /// <returns>Exit code for process that creates Event Log event source.</returns>
        public static int RegisterEventSource(string logName, string machineName, string eventSourceName)
        {
            StringBuilder msg = new StringBuilder();

            if (WindowsEventLog.EventSourceInitializer == string.Empty)
            {
                msg.Length = 0;
                msg.Append("You must specify the EventSourceInitializer key in the configuration file or specify the EventSourceInitializer property of this object in order to register the event source.");
                throw new System.Exception(msg.ToString());
            }

            string currentFolder = Environment.CurrentDirectory;
            Process initializeEventSourceNameProcess = new Process();
            string processMessages = string.Empty;
            int processExitCode = -1;


            initializeEventSourceNameProcess.StartInfo.WorkingDirectory = WindowsEventLog.CurrentWorkingDirectory;
            initializeEventSourceNameProcess.StartInfo.FileName = WindowsEventLog.EventSourceInitializer;
            initializeEventSourceNameProcess.StartInfo.Arguments = BuildRegisterArguments(logName, machineName, eventSourceName);
            initializeEventSourceNameProcess.StartInfo.CreateNoWindow = true;
            initializeEventSourceNameProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            initializeEventSourceNameProcess.StartInfo.UseShellExecute = false;
            initializeEventSourceNameProcess.StartInfo.RedirectStandardOutput = true;
            initializeEventSourceNameProcess.StartInfo.RedirectStandardError = true;


            initializeEventSourceNameProcess.Start();
            initializeEventSourceNameProcess.WaitForExit();
            processExitCode = initializeEventSourceNameProcess.ExitCode;

            _outputMessages = initializeEventSourceNameProcess.StandardOutput.ReadToEnd();
            _errorMessages = initializeEventSourceNameProcess.StandardError.ReadToEnd();

            return processExitCode;
        
        }

        private static string BuildRegisterArguments(string logName, string machineName, string eventSourceName)
        {
            StringBuilder arguments = new StringBuilder();

            arguments.Length = 0;

            arguments.Append(logName);
            arguments.Append(" ");
            arguments.Append(machineName);
            arguments.Append(" ");
            arguments.Append(eventSourceName);

            return arguments.ToString();

        }



    }//end class
}//end namespace
