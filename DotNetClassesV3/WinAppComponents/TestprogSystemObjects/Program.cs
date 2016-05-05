using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using AppGlobals;
using PFMessageLogs;
using PFSystemObjects;
using System.Text;

namespace TestprogSystemObjects
{
    static class Program
    {
        public static MainForm _mainForm;
        public static MessageLog _messageLog;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new CMainForm());


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CAppGlobalErrorHandler.WriteToAppLog = true;
            CAppGlobalErrorHandler.WriteToEventLog = false;
            CAppGlobalErrorHandler.CancelApplicationOnGlobalError = false;

            Application.ThreadException += new ThreadExceptionEventHandler(CAppGlobalErrorHandler.GlobalErrorHandler);

            _messageLog = new MessageLog();
            _messageLog.Caption = "Test Progam (TestprogSystemObjects)";
            _messageLog.ShowDatetime = false;
            _messageLog.Font = "Lucida Console";
            _messageLog.FontSize = (float)10.0;
            _messageLog.ShowWindow();

            //MessageBox.Show(Environment.MachineName);

            RegisterEventLogSources();

            _mainForm = new MainForm();

            Application.Run(_mainForm);


        }


        //NOTE: TestprogSystemObjects must have appManifest with line to request Administrator mode to run.
        //      DO NOT USE THIS APPROACH IN A SHIPPING APPLICATION.
        //      Try to reister the event source at application setup time.
        private static void RegisterEventLogSources()
        {
            StringBuilder msg = new StringBuilder();

            //check if the event source is registered
            //if not, register it and prompt user to restart the application
            //if it does, continue with the app start

            try
            {
                string logName = AppConfig.GetStringValueFromConfigFile("WindowsEventLog", "Application");
                string machineName = AppConfig.GetStringValueFromConfigFile("WindowsEventLogMachineName", ".");
                string eventSourceName = AppConfig.GetStringValueFromConfigFile("WindowsEventLogEventSource", "PFApps");
                string eventSourceInitializerApp = AppConfig.GetStringValueFromConfigFile("EventSourceInitializer", string.Empty);
                string currentWorkingDirectory = AppConfig.GetStringValueFromConfigFile("EventSourceInitializerWorkingDirectory", string.Empty);

                if (currentWorkingDirectory.Length == 0)
                {
                    currentWorkingDirectory = Environment.CurrentDirectory;
                }


                if (WindowsEventLog.SourceExists(eventSourceName, machineName) == false)
                {
                    WindowsEventLog.EventSourceInitializer = eventSourceInitializerApp;
                    WindowsEventLog.CurrentWorkingDirectory = currentWorkingDirectory;
                    int result = WindowsEventLog.RegisterEventSource(logName, machineName, eventSourceName);
                    msg.Length = 0;
                    if (result == 0)
                    {
                        msg.Append(eventSourceName);
                        msg.Append(" Windows Event Log event source name successfully registered.");
                        if (WindowsEventLog.OutputMessages.Length > 0)
                        {
                            msg.Append("\r\n");
                            msg.Append(WindowsEventLog.OutputMessages);
                        }
                        AppMessages.DisplayInfoMessage(msg.ToString());
                    }
                    else
                    {
                        msg.Append("Unable to register ");
                        msg.Append(eventSourceName);
                        msg.Append(" Windows Event Log event source name. Application event log write requests, if any, will be ignored.");
                        if (WindowsEventLog.OutputMessages.Length > 0)
                        {
                            msg.Append("\r\n");
                            msg.Append(WindowsEventLog.OutputMessages);
                        }
                        if (WindowsEventLog.ErrorMessages.Length > 0)
                        {
                            msg.Append("\r\n");
                            msg.Append(WindowsEventLog.ErrorMessages);
                        }
                        AppMessages.DisplayWarningMessage(msg.ToString());
                    }

                }

            }
            catch (System.Exception ex)
            {
                string errMsg = "Attempt to set event source name for windows log messages failed with following error: ";
                AppMessages.DisplayErrorMessage(errMsg + ex.Message);
            }
            finally
            {
                ;
            }
                 
        
        }

    }//end class
}//end namespace
