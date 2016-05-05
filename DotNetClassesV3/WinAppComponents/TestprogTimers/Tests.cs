using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using PFMessageLogs;
using PFTimers;

namespace TestprogTimers
{
    public class Tests
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = false;
        private MessageLog _messageLog;

        //properties
        public bool SaveErrorMessagesToAppLog
        {
            get
            {
                return _saveErrorMessagesToAppLog;
            }
            set
            {
                _saveErrorMessagesToAppLog = value;
            }
        }

        /// <summary>
        /// Message log window manager.
        /// </summary>
        public MessageLog MessageLogUI
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

        public void StopwatchTest(MainForm frm)
        {
            Stopwatch timer = new Stopwatch();
            WaitTimer wait = new WaitTimer();
            long loopMax = 1000000000;
            long loopModulus = 100000000;
            long modNum = -1;

            try
            {
                _msg.Length = 0;
                _msg.Append("StopwatchTest started ...\r\n");
                WriteMessageToLog(_msg.ToString());

                timer.Start();

                _msg.Length = 0;
                _msg.Append("\r\nTimed wait ... \r\n");
                WriteMessageToLog(_msg.ToString());
                wait.Wait((long)2);

                _msg.Length = 0;
                _msg.Append("Elapsed Time:         ");
                _msg.Append(timer.FormattedElapsedTime);
                _msg.Append(Environment.NewLine);
                _msg.Append("Elapsed milliseconds: ");
                _msg.Append(timer.ElapsedMilliseconds.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                WriteMessageToLog(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("\r\nLooping ");
                _msg.Append(loopMax.ToString("#,##0"));
                _msg.Append(" times... \r\n");
                WriteMessageToLog(_msg.ToString());
                
                for(long num = 1; num <= loopMax; num++)
                {
                    modNum = (num % loopModulus);
                    if(modNum ==0)
                    {
                        _msg.Length = 0;
                        _msg.Append("Loop count = ");
                        _msg.Append(num.ToString("#,##0"));
                        _msg.Append(" Elapsed time to this point: ");
                        _msg.Append(timer.FormattedElapsedTime);
                        WriteMessageToLog(_msg.ToString());
                    }
                }

                _msg.Length = 0;
                _msg.Append("Elapsed Time:         ");
                _msg.Append(timer.FormattedElapsedTime);
                _msg.Append(Environment.NewLine);
                _msg.Append("Elapsed milliseconds: ");
                _msg.Append(timer.ElapsedMilliseconds.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                WriteMessageToLog(_msg.ToString());

                timer.Stop();
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
                _msg.Length = 0;
                _msg.Append("\r\n... StopwatchTest finished.");
                WriteMessageToLog(_msg.ToString());

            }
        }



        public void IntervalTimerTest(MainForm frm)
        {
            IntervalTimer timer = new IntervalTimer();
            long loopMax = 100000000;
            long sumNum = 0;

            try
            {
                _msg.Length = 0;
                _msg.Append("IntervalTimerTest started ...\r\n");
                WriteMessageToLog(_msg.ToString());

                timer.StartTimer();

                for (long num = 1; num <= loopMax; num++)
                {
                    sumNum += num;
                }

                timer.StopTimer();

                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("loopMax = ");
                _msg.Append(loopMax.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                _msg.Append("sumNum = ");
                _msg.Append(sumNum.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                _msg.Append("Elapsed time = ");
                _msg.Append(timer.ElapsedTime.ToString("#,##0"));
                _msg.Append(" milliseconds");
                _msg.Append(Environment.NewLine);
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
                _msg.Length = 0;
                _msg.Append("\r\n... IntervalTimerTest finished.");
                WriteMessageToLog(_msg.ToString());

            }
        }

        //WARNING: Status timer can slow down processing significantly if processing is in a tight loop.
        public void StatusTimerTest(MainForm frm)
        {
            StatusTimer timer = new StatusTimer();
            long loopMax = 100000000;
            int statusInterval = 5;

            try
            {
                _msg.Length = 0;
                _msg.Append("StatusTimerTest started ...\r\n");
                WriteMessageToLog(_msg.ToString());

                timer.NumSecondsInterval = statusInterval;
                timer.Start();  //report status every two seconds

                _msg.Length = 0;
                _msg.Append("\r\nLooping ");
                _msg.Append(loopMax.ToString("#,##0"));
                _msg.Append(" times... \r\n");
                WriteMessageToLog(_msg.ToString());

                for (long num = 1; num <= loopMax; num++)
                {
                    if (timer.StatusReportDue())
                    {
                        _msg.Length = 0;
                        _msg.Append("Loop count = ");
                        _msg.Append(num.ToString("#,##0"));
                        _msg.Append(" Elapsed time to this point: ");
                        _msg.Append(timer.GetFormattedElapsedTime());
                        WriteMessageToLog(_msg.ToString());
                    }
                }
                _msg.Length = 0;
                _msg.Append("Loop count = ");
                _msg.Append(loopMax.ToString("#,##0"));
                _msg.Append(" Elapsed time to this point: ");
                _msg.Append(timer.GetFormattedElapsedTime());
                WriteMessageToLog(_msg.ToString());


                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                _msg.Append("Total Elapsed Time:   ");
                _msg.Append(timer.GetFormattedElapsedTime());
                _msg.Append(Environment.NewLine);
                _msg.Append("Elapsed milliseconds: ");
                _msg.Append(timer.GetElapsedTime().TotalMilliseconds.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                WriteMessageToLog(_msg.ToString());

                timer.Stop();
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
                _msg.Length = 0;
                _msg.Append("\r\n... StatusTimerTest finished.");
                WriteMessageToLog(_msg.ToString());

            }
        }


        private long _num = 0;
        private long _sumNum = 0;
        private bool _statusMessagePending = false;
        private StringBuilder _statusMessage = new StringBuilder();

        public void SystemTimerTest(MainForm frm)
        {
            PFSystemTimer timer = new PFSystemTimer();
            long loopMax = 2000000000;
            int statusInterval = 2;
            Stopwatch sw = new Stopwatch();

            try
            {
                _msg.Length = 0;
                _msg.Append("SystemTimerTest started ...\r\n");
                WriteMessageToLog(_msg.ToString());

                sw.Start();

                timer.IntervalInSecs = statusInterval;
                timer.elapsedTimeStatusReport += TestStatusReport;
                timer.SetEventHandler();
                timer.Start();  //report status every two seconds

                _msg.Length = 0;
                _msg.Append("\r\nLooping ");
                _msg.Append(loopMax.ToString("#,##0"));
                _msg.Append(" times... \r\n");
                WriteMessageToLog(_msg.ToString());

                for (_num = 1; _num <= loopMax; _num++)
                {
                    _sumNum += _num;
                    if (_statusMessagePending)
                    {
                        WriteMessageToLog(_statusMessage.ToString());
                        _statusMessagePending = false;
                    }
                }

                timer.Stop();

                sw.Stop();

                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("sumNum = ");
                _msg.Append(_sumNum.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                _msg.Append("Total Elapsed Time:         ");
                _msg.Append(sw.FormattedElapsedTime);
                _msg.Append(Environment.NewLine);
                _msg.Append("Elapsed milliseconds: ");
                _msg.Append(sw.ElapsedTime.TotalMilliseconds.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
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
                _msg.Length = 0;
                _msg.Append("\r\n... SystemTimerTest finished.");
                WriteMessageToLog(_msg.ToString());

            }
        }

        private void TestStatusReport(TimeSpan elapsedTime, DateTime currentTime, DateTime startTime)
        {
            _statusMessage.Length = 0;
            _statusMessage.Append("Loop count = ");
            _statusMessage.Append(_num.ToString("#,##0"));
            _statusMessage.Append(" Elapsed time to this point: ");
            _statusMessage.Append(elapsedTime.ToString());
            _statusMessagePending = true;
        }
        
        private void WriteMessageToLog(string msg)
        {
            if (_messageLog != null)
            {
                _messageLog.WriteLine(msg);
            }
        }


    }//end class
}//end namespace
