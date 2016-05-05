//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PFMessageLogs;
using AppGlobals;

namespace TestMessageLogDLL
{
    /// <summary>
    /// Class library for use in testing pfMessageLogs DllMessageLog functionality.
    /// </summary>
    public class TestMessageLogClass
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        DllMessageLog _dllMsgLog = new DllMessageLog();

        //private variables for properties

        
        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public TestMessageLogClass()
        {
            _dllMsgLog.HideWindow();
            _dllMsgLog.Caption = "TestMessageLogDLL testing ...";
            _dllMsgLog.ShowDatetime = true;
        }

        //properties

        //methods

        public long GetSum(long minNum, long maxNum, long outputEveryInterval, bool showDateTime)
        {
            long sum = 0;
            try
            {
                _dllMsgLog.Clear();
                _dllMsgLog.ShowDatetime = showDateTime;
                _dllMsgLog.WriteLine("Running GetSum routine ...");
                _dllMsgLog.ShowWindow();
                for (long i = minNum; i <= maxNum; i++)
                {
                    sum+=i;
                    if ((i % outputEveryInterval) == 0 || i == maxNum)
                    {
                        _msg.Length = 0;
                        _msg.Append("Sum calculated to " + i.ToString("#,##0"));
                        _msg.Append(" = ");
                        _msg.Append(sum.ToString("#,##0"));
                        _dllMsgLog.WriteLine(_msg.ToString());
                    }
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                _dllMsgLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                ;
            }

            return sum;
        }

        public void ShowMessageLog()
        {
            _dllMsgLog.ShowWindow();
        }

        public void HideMessageLog()
        {
            _dllMsgLog.HideWindow();
        }


    }//end class
}//end namespace
