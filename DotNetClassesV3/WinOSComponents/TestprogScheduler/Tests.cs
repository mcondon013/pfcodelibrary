using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Configuration;
using PFSchedulerObjects;

namespace TestprogScheduler
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
        public static void OneTimeScheduleTest(MainForm frm)
        {

            try
            {
                _msg.Length = 0;
                _msg.Append("OneTimeScheduleTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                PFSchedule sked = new PFSchedule();
                sked.ScheduleFrequency = enScheduleFrequency.OneTime;
                sked.RunAt = Convert.ToDateTime("07/23/2013 16:05:00");
                sked.RunWindow = new TimeSpan(0, 10, 0);

                DateTime currDate = DateTime.Now;

                _msg.Length =0;
                _msg.Append("CurrDate: ");
                _msg.Append(currDate.ToString("MMMM d, yyyy HH:mm:ss"));
                _msg.Append(" ");

                if(sked.DateIsScheduled(currDate))
                {
                    _msg.Append("is match");
                }
                else
                {
                    _msg.Append("NOT a match");
                }
                Program._messageLog.WriteLine(_msg.ToString());

                DateTime nextDate = DateTime.Now.Add(new TimeSpan(0,0,15));

                _msg.Length = 0;
                _msg.Append("NextDate: ");
                _msg.Append(nextDate.ToString("MMMM d, yyyy HH:mm:ss"));
                _msg.Append(" ");

                if (sked.DateIsScheduled(nextDate))
                {
                    _msg.Append("is match");
                }
                else
                {
                    _msg.Append("NOT a match");
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
                _msg.Append("\r\n... OneTimeScheduleTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void DailyScheduleTest(MainForm frm)
        {
            try
            {
                _msg.Length = 0;
                _msg.Append("DailyScheduleTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                PFSchedule sked = new PFSchedule();
                sked.Name = "DailyOneTimeSchedule001";
                sked.ScheduleFrequency = enScheduleFrequency.Daily;
                sked.ScheduleOccursEveryNumDays = 1;   //occurs every day
                sked.DailyFrequency = enDailyFrequency.OneTime;
                sked.RunWindow = new TimeSpan(0, 10, 0);

                sked.OccursOnceAtTime = new TimeSpan(17, 15, 0);
                sked.ScheduleStartDate = Convert.ToDateTime("7/11/2013");
                sked.ScheduleEndDate = Convert.ToDateTime("12/31/2013");

                _msg.Length = 0;
                _msg.Append("Schedule Name::  ");
                _msg.Append(sked.Name.ToString());
                _msg.Append("\r\n");
                _msg.Append("DailyFrequency:  ");
                _msg.Append(sked.DailyFrequency.ToString());
                _msg.Append("\r\n");
                _msg.Append("RunWindow:  ");
                _msg.Append(sked.RunWindow.ToString());
                _msg.Append("\r\n");
                _msg.Append("OccursEvery:     ");
                _msg.Append(sked.ScheduleOccursEveryNumDays.ToString());
                _msg.Append(" day(s)");
                _msg.Append("\r\n");
                _msg.Append("OccursOnceAtTime:  ");
                _msg.Append(sked.OccursOnceAtTime.ToString());
                _msg.Append("\r\n");
                _msg.Append("DurationStart:   ");
                _msg.Append(sked.ScheduleStartDate.ToString());
                _msg.Append("\r\n");
                _msg.Append("DurationEnd:     ");
                _msg.Append(sked.ScheduleEndDate.ToString());
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                sked.Name = "DailyRecurringSchedule001";
                sked.ScheduleOccursEveryNumDays = 1;   //occurs every day
                sked.DailyFrequency = enDailyFrequency.Recurring;
                sked.RunWindow = new TimeSpan(0, 15, 0);
                sked.DailyOccursEveryTimeInterval = 5;
                sked.DailyOccursTimeInterval = enDailyOccursInterval.Minutes;  //recurs every 5 minutes
                sked.DailyOccursStartTime = new TimeSpan(0,3,15,0,0);
                sked.DailyOccursEndTime = new TimeSpan(0, 22, 45, 0, 0);
                sked.ScheduleStartDate = Convert.ToDateTime("7/11/2013");
                sked.ScheduleEndDate = Convert.ToDateTime("12/31/2013");

                _msg.Length = 0;
                _msg.Append("Schedule Name::  ");
                _msg.Append(sked.Name.ToString());
                _msg.Append("\r\n");
                _msg.Append("DailyFrequency:  ");
                _msg.Append(sked.DailyFrequency.ToString());
                _msg.Append("\r\n");
                _msg.Append("RunWindow:       ");
                _msg.Append(sked.RunWindow.ToString());
                _msg.Append("\r\n");
                _msg.Append("OccursEvery:     ");
                _msg.Append(sked.DailyOccursEveryTimeInterval.ToString());
                _msg.Append(" ");
                _msg.Append(sked.DailyOccursTimeInterval.ToString());
                _msg.Append("\r\n");
                _msg.Append("DailyOccursStartTime: ");
                _msg.Append(sked.DailyOccursStartTime.ToString());
                _msg.Append("\r\n");
                _msg.Append("DailyOccursEndTime:   ");
                _msg.Append(sked.DailyOccursEndTime.ToString());
                _msg.Append("\r\n");
                _msg.Append("DurationStart:   ");
                _msg.Append(sked.ScheduleStartDate.ToString());
                _msg.Append("\r\n");
                _msg.Append("DurationEnd:     ");
                _msg.Append(sked.ScheduleEndDate.ToString());
                _msg.Append("\r\n");
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
                _msg.Append("\r\n... DailyScheduleTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void VerifyDailyScheduleTimesTest(MainForm frm)
        {
            bool expectedResult = false;
            bool result = false;
            DateTime currTime = DateTime.Now;

            try
            {
                _msg.Length = 0;
                _msg.Append("VerifyDailyScheduleTimesTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                PFSchedule sked = new PFSchedule("TestSchedule");
                sked.ScheduleFrequency = enScheduleFrequency.Daily;
                TimeSpan skedTime = new TimeSpan(12, 25, 00);
                sked.SetDailyOneTimeSchedule(2, 10, skedTime, Convert.ToDateTime("7/13/2013 11:00:00"), Convert.ToDateTime("7/31/2013 23:22:59"));

                expectedResult = true;
                currTime = Convert.ToDateTime("7/13/2013 12:27:00");
                result = sked.DateIsScheduled(currTime);
                PrintResult(sked, currTime, result, expectedResult);

                expectedResult = false;
                currTime = Convert.ToDateTime("7/13/2013 12:47:00");
                result = sked.DateIsScheduled(currTime);
                PrintResult(sked, currTime, result, expectedResult);

                expectedResult = true;
                currTime = Convert.ToDateTime("7/15/2013 12:27:00");
                result = sked.DateIsScheduled(currTime);
                PrintResult(sked, currTime, result, expectedResult);

                expectedResult = false;
                currTime = Convert.ToDateTime("7/14/2013 12:27:00");
                result = sked.DateIsScheduled(currTime);
                PrintResult(sked, currTime, result, expectedResult);

                sked = null;
                sked = new PFSchedule("TestRecurringSchedule");

                sked.ScheduleFrequency = enScheduleFrequency.Daily;
                //skedTime = new TimeSpan(15, 35, 00);
                sked.SetDailyRecurringSchedule(3, 10, 3, enDailyOccursInterval.Hours, new TimeSpan(1, 0, 0), new TimeSpan(21, 59, 59), Convert.ToDateTime("7/13/2013 00:00:00"), Convert.ToDateTime("7/31/2013 23:59:59"));

                expectedResult = true;
                currTime = Convert.ToDateTime("7/13/2013 10:05:00");
                result = sked.DateIsScheduled(currTime);
                PrintResult(sked, currTime, result, expectedResult);

                expectedResult = false;
                currTime = Convert.ToDateTime("7/13/2013 11:05:00");
                result = sked.DateIsScheduled(currTime);
                PrintResult(sked, currTime, result, expectedResult);

                expectedResult = true;
                currTime = Convert.ToDateTime("7/13/2013 13:05:00");
                result = sked.DateIsScheduled(currTime);
                PrintResult(sked, currTime, result, expectedResult);

                expectedResult = false;
                currTime = Convert.ToDateTime("7/13/2014 11:05:00");
                result = sked.DateIsScheduled(currTime);
                PrintResult(sked, currTime, result, expectedResult);

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
                _msg.Append("\r\n... VerifyDailyScheduleTimesTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        private static void PrintResult(PFSchedule sked, DateTime currTime,  bool result, bool expectedResult)
        {
            _msg.Length = 0;
            _msg.Append(sked.DailyFrequency.ToString());
            _msg.Append(": ");
            _msg.Append(currTime.ToString());
            if (result == expectedResult)
                _msg.Append(" matches expected result. ");
            else
                _msg.Append(" DOES NOT MATCH expected result. ");
            _msg.Append(sked.ScheduleLookupResult.ToString());
            Program._messageLog.WriteLine(_msg.ToString());
        }


    }//end class
}//end namespace
