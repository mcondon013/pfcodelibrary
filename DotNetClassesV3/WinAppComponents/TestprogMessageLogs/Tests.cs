using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using PFMessageLogs;
using TestMessageLogDLL;

namespace TestprogMessageLogs
{
    public class Tests
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = false;
        MessageLog _messageLog = null;
        TestMessageLogClass _testClass = new TestMessageLogClass();

        //properties
        public bool SaveErrorMessagesToAppLog
        {
            get
            {
                return this._saveErrorMessagesToAppLog;
            }
            set
            {
                this._saveErrorMessagesToAppLog = value;
            }
        }

        public MessageLog MessageLog
        {
            get
            {
                return _messageLog;
            }
            set
            {
                _messageLog = value;
            }
        }

        //tests
        public void RunApplicationMessageLogTest(long minNum, long maxNum, long outputEveryInterval, bool showDateTime)
        {
            long sum = 0;

            try
            {
                if (_messageLog == null)
                {
                    throw new System.Exception("MessageLog object must be defined in order to run GetSum in Tests module.");
                }

                sum = this.GetSum(minNum, maxNum, outputEveryInterval, showDateTime);
                WriteMessageToLog(Environment.NewLine);
                _msg.Length = 0;
                _msg.Append("Sum = ");
                _msg.Append(sum.ToString("#,##0"));
                WriteMessageToLog(_msg.ToString());
                WriteMessageToLog(Environment.NewLine);
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                ;
            }
                 
        
        }

        private long GetSum(long minNum, long maxNum, long outputEveryInterval, bool showDateTime)
        {
            long sum = 0;

            try
            {
                _messageLog.Clear();
                _messageLog.ShowDatetime = showDateTime;
                _messageLog.WriteLine("Running GetSum routine ...");
                _messageLog.ShowWindow();
                for (long i = minNum; i <= maxNum; i++)
                {
                    sum += i;
                    if ((i % outputEveryInterval) == 0 || i == maxNum)
                    {
                        _msg.Length = 0;
                        _msg.Append("Sum calculated to " + i.ToString("#,##0"));
                        _msg.Append(" = ");
                        _msg.Append(sum.ToString("#,##0"));
                        WriteMessageToLog(_msg.ToString());
                    }
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                ;
            }

            return sum;
        }

        public void RunDllGetSumTest(long minNum, long maxNum, long outputEveryInterval, bool showDateTime)
        {
            long sum = 0;

            try
            {
                sum = _testClass.GetSum(minNum, maxNum, outputEveryInterval, showDateTime);
                _msg.Length = 0;
                _msg.Append("Sum = ");
                _msg.Append(sum.ToString("#,##0"));
                WriteMessageToLog(_msg.ToString());
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                ;
            }
        
        }

        public void ShowDllWindow()
        {
            _testClass.ShowMessageLog();
        }

        public void HideDllWindow()
        {
            _testClass.HideMessageLog();
        }


        public void OutputMessagesToTextLogFile(string outputFilename, bool appendMessagesIfFileExists,
                                                bool showMessageType, bool showApplicationName, bool showMachineName,
                                                long minNum, long maxNum, long outputEveryInterval, bool showDateTime)
        {
            long sum = 0;

            try
            {
                _msg.Length = 0;
                _msg.Append("Outputting messages to ");
                _msg.Append(outputFilename);
                WriteMessageToLog(_msg.ToString());

                sum = GetSumToOutputFile(outputFilename, appendMessagesIfFileExists, 
                                         showMessageType, showApplicationName, showMachineName,
                                         minNum, maxNum, outputEveryInterval, showDateTime);

                _msg.Length = 0;
                _msg.Append("Sum = ");
                _msg.Append(sum.ToString("#,##0"));
                WriteMessageToLog(_msg.ToString());

                System.Diagnostics.Process.Start("notepad.exe", outputFilename);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("Output messages to file test finished.");
                WriteMessageToLog(_msg.ToString());
            }
                 
        
        }

        private long GetSumToOutputFile(string outputFilename, bool appendMessagesIfFileExists, 
                                        bool showMessageType, bool showApplicationName, bool showMachineName,
                                        long minNum, long maxNum, long outputEveryInterval, bool showDateTime)
        {
            long sum = 0;
            TextLogFile logfile = null;

            try
            {
                logfile = new TextLogFile(outputFilename);
                logfile.ShowMessageType = showMessageType;
                if(showApplicationName)
                {
                    logfile.ApplicationName = "TestprogMessageLogs";
                }
                if(showMachineName)
                {
                    logfile.MachineName = Environment.MachineName;
                }
                if(File.Exists(outputFilename))
                {
                    if(appendMessagesIfFileExists == false)
                    {
                        logfile.TruncateFile();
                    }
                }

                logfile.ShowDatetime = showDateTime;
                for (long i = minNum; i <= maxNum; i++)
                {
                    sum += i;
                    if ((i % outputEveryInterval) == 0 || i == maxNum)
                    {
                        _msg.Length = 0;
                        _msg.Append("Sum calculated to " + i.ToString("#,##0"));
                        _msg.Append(" = ");
                        _msg.Append(sum.ToString("#,##0"));
                        logfile.WriteLine(_msg.ToString(), TextLogFile.LogMessageType.Information);
                    }
                }
                if (showMessageType)
                {
                    logfile.WriteLine("This is a test warning message.", TextLogFile.LogMessageType.Warning);
                    logfile.WriteLine("This is a test error message.", TextLogFile.LogMessageType.Error);
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                logfile.WriteLine(_msg.ToString(), TextLogFile.LogMessageType.Error);
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                ;
            }

            return sum;
        }


        public void DeleteEventSource(string eventSourceName)
        {
            try
            {
                if (WindowsLog.SourceExists(eventSourceName))
                {
                    WindowsLog.DeleteEventSource(eventSourceName);
                    if (WindowsLog.SourceExists(eventSourceName) == false)
                    {
                        _msg.Length = 0;
                        _msg.Append("Event Source ");
                        _msg.Append(eventSourceName);
                        _msg.Append(" delete succeeded.");
                        WriteMessageToLog(_msg.ToString());
                    }
                    else
                    {
                        _msg.Length = 0;
                        _msg.Append("Event Source ");
                        _msg.Append(eventSourceName);
                        _msg.Append(" delete failed.");
                        WriteMessageToLog(_msg.ToString());
                    }
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Event Source ");
                    _msg.Append(eventSourceName);
                    _msg.Append(" not found. No delete needed.");
                    WriteMessageToLog(_msg.ToString());
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                _msg.Append(Environment.NewLine);
                _msg.Append("Caller must have elevated security permissions (e.g. use Run As Administrator) to create and delete event sources and event logs.");
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), true);
            }
            finally
            {
                ;
            }
                 
        
        }

        public void OutputMessagesToWindowsApplicationEventLog(string eventSourceName,
                                                                int numInformationMessagesToWrite,
                                                                int numWarningMessagesToWrite,
                                                                int numErrorMessagesToWrite)
        {
            WindowsLog eventLog = null;


            try
            {
                if (WindowsLog.SourceExists(eventSourceName) == false)
                {
                    WindowsLog.CreateEventSource(eventSourceName);
                    if (WindowsLog.SourceExists(eventSourceName))
                    {
                        _msg.Length = 0;
                        _msg.Append("Event Source ");
                        _msg.Append(eventSourceName);
                        _msg.Append(" create succeeded.");
                        WriteMessageToLog(_msg.ToString());
                    }
                    else
                    {
                        _msg.Length = 0;
                        _msg.Append("Event Source ");
                        _msg.Append(eventSourceName);
                        _msg.Append(" create failed.");
                        WriteMessageToLog(_msg.ToString());
                    }
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Event Source ");
                    _msg.Append(eventSourceName);
                    _msg.Append(" exists.");
                    WriteMessageToLog(_msg.ToString());
                }
                
                eventLog = new WindowsLog(WindowsLog.EventLogName.Application, ".", eventSourceName);

                for (int i = 1; i <= numInformationMessagesToWrite; i++)
                {
                    _msg.Length = 0;
                    _msg.Append("Message ");
                    _msg.Append(i.ToString());
                    _msg.Append(" from test program.");
                    eventLog.WriteEntry(_msg.ToString(), WindowsLog.WindowsEventLogEntryType.Information);
                }

                for (int i = 1; i <= numWarningMessagesToWrite; i++)
                {
                    _msg.Length = 0;
                    _msg.Append("Warning message ");
                    _msg.Append(i.ToString());
                    _msg.Append(" from test program.");
                    eventLog.WriteEntry(_msg.ToString(), WindowsLog.WindowsEventLogEntryType.Warning);
                }

                for (int i = 1; i <= numErrorMessagesToWrite; i++)
                {
                    _msg.Length = 0;
                    _msg.Append("Error message ");
                    _msg.Append(i.ToString());
                    _msg.Append(" from test program.");
                    eventLog.WriteEntry(_msg.ToString(), WindowsLog.WindowsEventLogEntryType.Error);
                }

                _msg.Length = 0;
                _msg.Append("Number of event log messages written = ");
                _msg.Append((numInformationMessagesToWrite + numWarningMessagesToWrite + numErrorMessagesToWrite).ToString("#,##0"));
                WriteMessageToLog(_msg.ToString());
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                _msg.Append(Environment.NewLine);
                _msg.Append("Caller must have elevated security permissions (e.g. use Run As Administrator) to create and delete event sources and event logs.");
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), true);
            }
            finally
            {
                ;
            }
                 
        
        }

        //calling module must set MessageLog property to a valid instance of the message log for messages to be displayed

        
        public void WriteMessageToLog(string msg)
        {
            if (_messageLog != null)
                _messageLog.WriteLine(msg);
        }




    }//end class
}//end namespace
