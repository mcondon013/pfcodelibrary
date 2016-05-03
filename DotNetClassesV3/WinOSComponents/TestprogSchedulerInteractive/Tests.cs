using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Configuration;
using PFSchedulerObjects;
using PFTimers;

namespace TestprogSchedulerInteractive
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
        public static void RunTest(MainForm frm)
        {
            enScheduleFrequency skedFreq = enScheduleFrequency.Unknown;

            try
            {
                _msg.Length = 0;
                _msg.Append("RunTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                skedFreq = PFScheduler.GetScheduleFrequency(frm.cboScheduleFrequency.Text);

                switch (skedFreq)
                {
                    case enScheduleFrequency.OneTime:
                        RunOneTimeTest(frm);
                        break;
                    case enScheduleFrequency.Daily:
                        RunDailyTest(frm);
                        break;
                    case enScheduleFrequency.Weekly:
                        RunWeeklyTest(frm);
                        break;
                    case enScheduleFrequency.Monthly:
                        RunMonthlyTest(frm);
                        break;
                    default:
                        break;
                }


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
                _msg.Append("\r\n... RunTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        private static void RunOneTimeTest(MainForm frm)
        {
            PFSchedule sked = new PFSchedule(frm.txtScheduleName.Text.Trim());
            bool isScheduled = false;
            enScheduleLookupResult lookupResult = enScheduleLookupResult.Unknown;

            CreateRunOneTimeSchedule(frm, sked);

            isScheduled = sked.DateIsScheduled(sked.TestCurrDateTime);
            lookupResult = sked.ScheduleLookupResult;
            

            _msg.Length = 0;
            _msg.Append("Schedule type:        ");
            _msg.Append(sked.ScheduleFrequency.ToString());
            _msg.Append("\r\n");
            _msg.Append("Schedule Name:        ");
            _msg.Append(sked.Name);
            _msg.Append("\r\n");
            _msg.Append("RunAt date:           ");
            _msg.Append(sked.RunAt.ToString("MM/dd/yyyy HH:mm:ss"));
            _msg.Append("\r\n");
            _msg.Append("Test date:            ");
            _msg.Append(sked.TestCurrDateTime.ToString("MM/dd/yyyy HH:mm:ss"));
            _msg.Append("\r\n");
            _msg.Append("Actual result:        ");
            _msg.Append(lookupResult.ToString());
            _msg.Append("\r\n");
            _msg.Append("Expected test result: ");
            _msg.Append(sked.ExpectedTestResult.ToString());
            _msg.Append("\r\n");
            _msg.Append("Success/Failure:      ");
            if (sked.ExpectedTestResult == lookupResult)
                _msg.Append("SUCCESS");
            else
                _msg.Append("FAIL");
            _msg.Append("\r\n");
            Program._messageLog.WriteLine(_msg.ToString());

        }

        private static void RunDailyTest(MainForm frm)
        {
            PFSchedule sked = new PFSchedule(frm.txtScheduleName.Text.Trim());
            bool isScheduled = false;
            enScheduleLookupResult lookupResult = enScheduleLookupResult.Unknown;

            CreateDailySchedule(frm, sked);

            isScheduled = sked.DateIsScheduled(sked.TestCurrDateTime);
            lookupResult = sked.ScheduleLookupResult;


            _msg.Length = 0;
            _msg.Append("Schedule type:        ");
            _msg.Append(sked.ScheduleFrequency.ToString());
            _msg.Append("\r\n");
            _msg.Append("Schedule Name:        ");
            _msg.Append(sked.Name);
            _msg.Append("\r\n");
            _msg.Append("Schedule Start Date:  ");
            _msg.Append(sked.ScheduleStartDate.ToString("MM/dd/yyyy HH:mm:ss"));
            _msg.Append("\r\n");
            _msg.Append("Schedule End Date:    ");
            _msg.Append(sked.ScheduleEndDate.ToString("MM/dd/yyyy HH:mm:ss"));
            _msg.Append("\r\n");

            _msg.Append("Schedule runs every:  ");
            _msg.Append(sked.DailyOccursEveryTimeInterval.ToString());
            _msg.Append(" day(s)");
            _msg.Append("\r\n");
            _msg.Append("Daily Frequency:      ");
            _msg.Append(sked.DailyFrequency.ToString());
            _msg.Append("\r\n");

            if (sked.DailyFrequency == enDailyFrequency.OneTime)
            {
                _msg.Append("Daily Run At:         ");
                _msg.Append(sked.OccursOnceAtTime.ToString());
                _msg.Append("\r\n");
            }
            else
            {
                _msg.Append("Daily occurs every:   ");
                _msg.Append(sked.DailyOccursEveryTimeInterval.ToString());
                _msg.Append(" ");
                _msg.Append(sked.DailyOccursTimeInterval.ToString());
                _msg.Append("\r\n");
                _msg.Append("Daily starting at:    ");
                _msg.Append(PFScheduler.FormatTimeSpan(sked.DailyOccursStartTime));
                _msg.Append("\r\n");
                _msg.Append("Daily ends at:        ");
                _msg.Append(PFScheduler.FormatTimeSpan(sked.DailyOccursEndTime));
                _msg.Append("\r\n");
            }

            _msg.Append("Test date:            ");
            _msg.Append(sked.TestCurrDateTime.ToString("MM/dd/yyyy HH:mm:ss"));
            _msg.Append("\r\n");
            _msg.Append("Actual result:        ");
            _msg.Append(lookupResult.ToString());
            _msg.Append("\r\n");
            _msg.Append("Expected test result: ");
            _msg.Append(sked.ExpectedTestResult.ToString());
            _msg.Append("\r\n");
            _msg.Append("Success/Failure:      ");
            if (sked.ExpectedTestResult == lookupResult)
                _msg.Append("SUCCESS");
            else
                _msg.Append("FAIL");
            _msg.Append("\r\n");
            Program._messageLog.WriteLine(_msg.ToString());

        }

        private static void RunWeeklyTest(MainForm frm)
        {
            PFSchedule sked = new PFSchedule(frm.txtScheduleName.Text.Trim());
            bool isScheduled = false;
            enScheduleLookupResult lookupResult = enScheduleLookupResult.Unknown;

            CreateWeeklySchedule(frm, sked);

            isScheduled = sked.DateIsScheduled(sked.TestCurrDateTime);
            lookupResult = sked.ScheduleLookupResult;

            _msg.Length = 0;
            _msg.Append("Schedule type:        ");
            _msg.Append(sked.ScheduleFrequency.ToString());
            _msg.Append("\r\n");
            _msg.Append("Schedule Name:        ");
            _msg.Append(sked.Name);
            _msg.Append("\r\n");
            _msg.Append("Schedule Start Date:  ");
            _msg.Append(sked.ScheduleStartDate.ToString("MM/dd/yyyy HH:mm:ss"));
            _msg.Append("\r\n");
            _msg.Append("Schedule End Date:    ");
            _msg.Append(sked.ScheduleEndDate.ToString("MM/dd/yyyy HH:mm:ss"));
            _msg.Append("\r\n");

            _msg.Append("Schedule runs every:  ");
            _msg.Append(sked.WeeklyOccursEveryNumWeeks.ToString());
            _msg.Append(" week(s)");
            _msg.Append("\r\n");
            _msg.Append("Scheduled days:       ");
            _msg.Append(GetScheduledDaysList(sked));
            _msg.Append("\r\n");


            _msg.Append("Schedule runs every:  ");
            _msg.Append(sked.DailyOccursEveryTimeInterval.ToString());
            _msg.Append(" day(s)");
            _msg.Append("\r\n");
            _msg.Append("Daily Frequency:      ");
            _msg.Append(sked.DailyFrequency.ToString());
            _msg.Append("\r\n");

            if (sked.DailyFrequency == enDailyFrequency.OneTime)
            {
                _msg.Append("Daily Run At:         ");
                _msg.Append(sked.OccursOnceAtTime.ToString());
                _msg.Append("\r\n");
            }
            else
            {
                _msg.Append("Daily occurs every:   ");
                _msg.Append(sked.DailyOccursEveryTimeInterval.ToString());
                _msg.Append(" ");
                _msg.Append(sked.DailyOccursTimeInterval.ToString());
                _msg.Append("\r\n");
                _msg.Append("Daily starting at:    ");
                _msg.Append(PFScheduler.FormatTimeSpan(sked.DailyOccursStartTime));
                _msg.Append("\r\n");
                _msg.Append("Daily ends at:        ");
                _msg.Append(PFScheduler.FormatTimeSpan(sked.DailyOccursEndTime));
                _msg.Append("\r\n");
            }

            _msg.Append("Test date:            ");
            _msg.Append(sked.TestCurrDateTime.ToString("MM/dd/yyyy HH:mm:ss"));
            _msg.Append("\r\n");
            _msg.Append("Actual result:        ");
            _msg.Append(lookupResult.ToString());
            _msg.Append("\r\n");
            _msg.Append("Expected test result: ");
            _msg.Append(sked.ExpectedTestResult.ToString());
            _msg.Append("\r\n");
            _msg.Append("Success/Failure:      ");
            if (sked.ExpectedTestResult == lookupResult)
                _msg.Append("SUCCESS");
            else
                _msg.Append("FAIL");
            _msg.Append("\r\n");
            Program._messageLog.WriteLine(_msg.ToString());
        }


        private static string GetScheduledDaysList(PFSchedule sked)
        {
            StringBuilder scheduledDays = new StringBuilder();
            int numDaysScheduled = 0;

            for (int i = 0; i < sked.WeeklySchedule.Day.Length; i++)
            {
                if (sked.WeeklySchedule.Day[i] == true)
                {
                    numDaysScheduled++;
                    enScheduleDay day = (enScheduleDay)i;

                    if (numDaysScheduled > 1)
                        scheduledDays.Append(", ");
                    scheduledDays.Append(day.ToString());
                }
            }

            return scheduledDays.ToString();
        }
        
        private static void RunMonthlyTest(MainForm frm)
        {
            PFSchedule sked = new PFSchedule(frm.txtScheduleName.Text.Trim());
            bool isScheduled = false;
            enScheduleLookupResult lookupResult = enScheduleLookupResult.Unknown;

            CreateMonthlySchedule(frm, sked);

            isScheduled = sked.DateIsScheduled(sked.TestCurrDateTime);
            lookupResult = sked.ScheduleLookupResult;

            _msg.Length = 0;
            _msg.Append("Schedule type:        ");
            _msg.Append(sked.ScheduleFrequency.ToString());
            _msg.Append("\r\n");
            _msg.Append("Schedule Name:        ");
            _msg.Append(sked.Name);
            _msg.Append("\r\n");
            _msg.Append("Schedule Start Date:  ");
            _msg.Append(sked.ScheduleStartDate.ToString("MM/dd/yyyy HH:mm:ss"));
            _msg.Append("\r\n");
            _msg.Append("Schedule End Date:    ");
            _msg.Append(sked.ScheduleEndDate.ToString("MM/dd/yyyy HH:mm:ss"));
            _msg.Append("\r\n");


            if (sked.MonthlyScheduleMonthIdType == enMonthlyScheduleMonthIdType.EveryNumMonths)
            {
                _msg.Append("Schedule runs every:  ");
                _msg.Append(sked.MonthlyOccursEveryNumMonths.ToString());
                _msg.Append(" month(s)");
            }
            else
            {
                _msg.Append("Scheduled months:     ");
                _msg.Append(GetScheduledMonthDaysList(sked));
                _msg.Append("\r\n");
            }
            _msg.Append("\r\n");

            if (sked.MonthlyScheduleDayIdType == enMonthlyScheduleDayIdType.DayNumber)
            {
                _msg.Append("Schedule runs on day: ");
                _msg.Append(sked.MonthlyScheduleDay.ToString());
            }
            else
            {
                _msg.Append("Schedule runs on:     ");
                _msg.Append(sked.MonthlyScheduleOrdinal.ToString());
                _msg.Append(" ");
                _msg.Append(sked.MonthlyScheduleDay.ToString());
            }
            _msg.Append("\r\n");


            _msg.Append("Schedule runs every:  ");
            _msg.Append(sked.DailyOccursEveryTimeInterval.ToString());
            _msg.Append(" day(s)");
            _msg.Append("\r\n");
            _msg.Append("Daily Frequency:      ");
            _msg.Append(sked.DailyFrequency.ToString());
            _msg.Append("\r\n");

            if (sked.DailyFrequency == enDailyFrequency.OneTime)
            {
                _msg.Append("Daily Run At:         ");
                _msg.Append(sked.OccursOnceAtTime.ToString());
                _msg.Append("\r\n");
            }
            else
            {
                _msg.Append("Daily occurs every:   ");
                _msg.Append(sked.DailyOccursEveryTimeInterval.ToString());
                _msg.Append(" ");
                _msg.Append(sked.DailyOccursTimeInterval.ToString());
                _msg.Append("\r\n");
                _msg.Append("Daily starting at:    ");
                _msg.Append(PFScheduler.FormatTimeSpan(sked.DailyOccursStartTime));
                _msg.Append("\r\n");
                _msg.Append("Daily ends at:        ");
                _msg.Append(PFScheduler.FormatTimeSpan(sked.DailyOccursEndTime));
                _msg.Append("\r\n");
            }

            _msg.Append("Test date:            ");
            _msg.Append(sked.TestCurrDateTime.ToString("MM/dd/yyyy HH:mm:ss"));
            _msg.Append("\r\n");
            _msg.Append("Actual result:        ");
            _msg.Append(lookupResult.ToString());
            _msg.Append("\r\n");
            _msg.Append("Expected test result: ");
            _msg.Append(sked.ExpectedTestResult.ToString());
            _msg.Append("\r\n");
            _msg.Append("Success/Failure:      ");
            if (sked.ExpectedTestResult == lookupResult)
                _msg.Append("SUCCESS");
            else
                _msg.Append("FAIL");
            _msg.Append("\r\n");
            Program._messageLog.WriteLine(_msg.ToString());
        }

        private static string GetScheduledMonthDaysList(PFSchedule sked)
        {
            StringBuilder scheduledMonths = new StringBuilder();
            int numMonthsScheduled = 0;

            for (int i = 0; i < sked.MonthlySchedule.Month.Length; i++)
            {
                if (sked.MonthlySchedule.Month[i] == true)
                {
                    numMonthsScheduled++;
                    enScheduleMonth mo = (enScheduleMonth)i;

                    if (numMonthsScheduled > 1)
                        scheduledMonths.Append(", ");
                    scheduledMonths.Append(mo.ToString());
                }
            }

            return scheduledMonths.ToString();
        }
        

        public static PFSchedule CreateScheduleFromScreenInput(MainForm frm)
        {
            PFSchedule sked = new PFSchedule(frm.txtScheduleName.Text.Trim());            
            try
            {
                _msg.Length = 0;
                _msg.Append("CreateScheduleFromScreenInput started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                enScheduleFrequency skedFreq = PFScheduler.GetScheduleFrequency(frm.cboScheduleFrequency.Text.Trim());

                if (frm.txtCurrDateTime.Text.Trim().Length == 0)
                    frm.txtCurrDateTime.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                switch (skedFreq)
                {
                    case enScheduleFrequency.OneTime:
                        CreateRunOneTimeSchedule(frm, sked);
                        break;
                    case enScheduleFrequency.Daily:
                        CreateDailySchedule(frm, sked);
                        break;
                    case enScheduleFrequency.Weekly:
                        CreateWeeklySchedule(frm, sked);
                        break;
                    case enScheduleFrequency.Monthly:
                        CreateMonthlySchedule(frm, sked);
                        break;
                    default:
                        _msg.Length=0;
                        _msg.Append("Unexpected or invalid schedule frequency: ");
                        _msg.Append(frm.txtScheduleName.Text.Trim());
                        throw new System.Exception(_msg.ToString());
                        //break;
                }

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
                _msg.Append("\r\n... CreateScheduleFromScreenInput finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }

            return sked;
        }

        private static void CreateRunOneTimeSchedule (MainForm frm, PFSchedule sked)
        {

            sked.ScheduleFrequency = enScheduleFrequency.OneTime;
            sked.RunAt = Convert.ToDateTime(frm.txtRunOnceAt.Text.Trim());
            TimeSpan runWindow = new TimeSpan(0, Convert.ToInt32(frm.txtRunWindowInMinutes.Text.Trim()), 0);
            sked.RunWindow = runWindow;
            sked.ScheduleStartDate = Convert.ToDateTime(frm.txtRunOnceAt.Text.Trim());
            sked.ScheduleEndDate = Convert.ToDateTime(frm.txtRunOnceAt.Text.Trim()).Add(sked.RunWindow);
            frm.txtScheduleStart.Text = sked.ScheduleStartDate.ToString("MM/dd/yyyy HH:mm:ss");
            frm.txtScheduleEnd.Text = sked.ScheduleEndDate.ToString("MM/dd/yyyy HH:mm:ss");
            sked.ScheduleOccursEveryNumDays = 1;
            sked.TestCurrDateTime = Convert.ToDateTime(frm.txtCurrDateTime.Text.Trim());
            sked.ExpectedTestResult = PFScheduler.GetScheduleLookupResult(frm.cboExpectedTestResult.Text);

        }

        private static void CreateDailySchedule(MainForm frm, PFSchedule sked)
        {
            sked.ScheduleFrequency = enScheduleFrequency.Daily;
            TimeSpan runWindow = new TimeSpan(0, Convert.ToInt32(frm.txtRunWindowInMinutes.Text.Trim()), 0);
            sked.RunWindow = runWindow;
            sked.ScheduleStartDate = Convert.ToDateTime(frm.txtScheduleStart.Text.Trim());
            sked.ScheduleEndDate = Convert.ToDateTime(frm.txtScheduleEnd.Text.Trim());

            sked.TestCurrDateTime = Convert.ToDateTime(frm.txtCurrDateTime.Text.Trim());
            sked.ExpectedTestResult = PFScheduler.GetScheduleLookupResult(frm.cboExpectedTestResult.Text);


            if (frm.optDailyRunOnce.Checked)
                sked.DailyFrequency = enDailyFrequency.OneTime;
            else if (frm.optDailyRecurring.Checked)
                sked.DailyFrequency = enDailyFrequency.Recurring;
            else
                sked.DailyFrequency = enDailyFrequency.Unknown;

            if (sked.DailyFrequency == enDailyFrequency.OneTime)
            {
                sked.OccursOnceAtTime = PFScheduler.GetTimeSpan(frm.txtDailyRunOnceAt.Text);
                sked.ScheduleOccursEveryNumDays = Convert.ToInt32(frm.txtScheduleRunsEveryNumDays.Text.Trim());
            }
            else if (sked.DailyFrequency == enDailyFrequency.Recurring)
            {
                sked.ScheduleOccursEveryNumDays = Convert.ToInt32(frm.txtScheduleRunsEveryNumDays.Text.Trim());
                sked.DailyOccursEveryTimeInterval = Convert.ToInt32(frm.txtDailyOccursEveryIntervalNum.Text.Trim());
                sked.DailyOccursTimeInterval = PFScheduler.GetDailyOccursInterval(frm.cboDailyOccursInterval.Text);
                sked.DailyOccursStartTime = PFScheduler.GetTimeSpan(frm.txtOccursStartingAt.Text.Trim());
                sked.DailyOccursEndTime = PFScheduler.GetTimeSpan(frm.txtOccursEndsAt.Text.Trim());
            }
            else
            {
                sked.DailyFrequency = enDailyFrequency.Unknown;
            }

        }

        private static void CreateWeeklySchedule(MainForm frm, PFSchedule sked)
        {
            CreateDailySchedule(frm, sked);

            sked.ScheduleFrequency = enScheduleFrequency.Weekly;

            sked.ScheduleOccursEveryNumDays = 1;

            sked.WeeklyOccursEveryNumWeeks = Convert.ToInt32(frm.txtWeeklyRecursEveryNumDays.Text.Trim());
            sked.WeeklySchedule.Day[0] = frm.chkWeeklyMonday.Checked;
            sked.WeeklySchedule.Day[1] = frm.chkWeeklyTuesday.Checked;
            sked.WeeklySchedule.Day[2] = frm.chkWeeklyWednesday.Checked;
            sked.WeeklySchedule.Day[3] = frm.chkWeeklyThursday.Checked;
            sked.WeeklySchedule.Day[4] = frm.chkWeeklyFriday.Checked;
            sked.WeeklySchedule.Day[5] = frm.chkWeeklySaturday.Checked;
            sked.WeeklySchedule.Day[6] = frm.chkWeeklySunday.Checked;

        }

        private static void CreateMonthlySchedule(MainForm frm, PFSchedule sked)
        {
            CreateDailySchedule(frm, sked);

            sked.ScheduleFrequency = enScheduleFrequency.Monthly;

            sked.ScheduleOccursEveryNumDays = 1;

            if (frm.optOccursEveryMonthNum.Checked)
            {
                sked.MonthlyScheduleMonthIdType = enMonthlyScheduleMonthIdType.EveryNumMonths;
                sked.MonthlyOccursEveryNumMonths = Convert.ToInt32(frm.txtMonthlyOccursIntervalNum.Text.Trim());
            }
            else if (frm.optOccursDuringMonthName.Checked)
            {
                sked.MonthlyScheduleMonthIdType = enMonthlyScheduleMonthIdType.OccursDuringMonthName;
                sked.MonthlySchedule.Month[1] = frm.chkMonthlyJan.Checked;
                sked.MonthlySchedule.Month[2] = frm.chkMonthlyFeb.Checked;
                sked.MonthlySchedule.Month[3] = frm.chkMonthlyMar.Checked;
                sked.MonthlySchedule.Month[4] = frm.chkMonthlyApr.Checked;
                sked.MonthlySchedule.Month[5] = frm.chkMonthlyMay.Checked;
                sked.MonthlySchedule.Month[6] = frm.chkMonthlyJun.Checked;
                sked.MonthlySchedule.Month[7] = frm.chkMonthlyJul.Checked;
                sked.MonthlySchedule.Month[8] = frm.chkMonthlyAug.Checked;
                sked.MonthlySchedule.Month[9] = frm.chkMonthlySep.Checked;
                sked.MonthlySchedule.Month[10] = frm.chkMonthlyOct.Checked;
                sked.MonthlySchedule.Month[11] = frm.chkMonthlyNov.Checked;
                sked.MonthlySchedule.Month[12] = frm.chkMonthlyDec.Checked;
            }
            else
            {
                sked.MonthlyScheduleMonthIdType = enMonthlyScheduleMonthIdType.Unknown;
            }
           
            if (frm.optMonthlyDayName.Checked)
            {
                sked.MonthlyScheduleDayIdType = enMonthlyScheduleDayIdType.DayName;
                sked.MonthlyScheduleOrdinal = PFScheduler.GetMonthlyScheduleOrdinal(frm.cboMonthlyDayNameOrdinal.Text.Trim());
                sked.MonthlyScheduleDay = PFScheduler.GetScheduleDay(frm.cboMonthlyDayName.Text.Trim());
            }
            else if (frm.optMonthlyDayNumber.Checked)
            {
                sked.MonthlyScheduleDayIdType = enMonthlyScheduleDayIdType.DayNumber;
                sked.MonthlyDayNumber = Convert.ToInt32(frm.txtMonthlyDayNumber.Text.Trim());
            }
            else
                sked.MonthlyScheduleDayIdType = enMonthlyScheduleDayIdType.Unknown;

        }



        public static void CreateScreenInputFromSchedule(MainForm frm, PFSchedule sked)
        {
            try
            {
                _msg.Length = 0;
                _msg.Append("CreateScreenInputFromSchedule started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                frm.InitializeForm();

                frm.cboScheduleFrequency.Text = sked.ScheduleFrequency.ToString();
                frm.txtScheduleName.Text = sked.Name;
                frm.txtRunWindowInMinutes.Text = sked.RunWindow.TotalMinutes.ToString();
                frm.txtScheduleStart.Text = sked.ScheduleStartDate.ToString("MM/dd/yyyy HH:mm:ss");
                frm.txtScheduleEnd.Text = sked.ScheduleEndDate.ToString("MM/dd/yyyy HH:mm:ss");
                
                frm.txtCurrDateTime.Text = sked.TestCurrDateTime.ToString("MM/dd/yyyy HH:mm:ss");
                frm.cboExpectedTestResult.Text = sked.ExpectedTestResult.ToString();

                switch (sked.ScheduleFrequency)
                {
                    case enScheduleFrequency.OneTime:
                        LoadRunOneTimeSchedule(frm, sked);
                        break;
                    case enScheduleFrequency.Daily:
                        LoadDailySchedule(frm, sked);
                        break;
                    case enScheduleFrequency.Weekly:
                        LoadWeeklySchedule(frm, sked);
                        break;
                    case enScheduleFrequency.Monthly:
                        LoadMonthlySchedule(frm, sked);
                        break;
                    default:
                        _msg.Length = 0;
                        _msg.Append("Unexpected or invalid schedule frequency: ");
                        _msg.Append(frm.txtScheduleName.Text.Trim());
                        throw new System.Exception(_msg.ToString());
                    //break;
                }


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
                _msg.Append("\r\n... CreateScreenInputFromSchedule finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        private static void LoadRunOneTimeSchedule(MainForm frm, PFSchedule sked)
        {
            frm.txtRunOnceAt.Text = sked.RunAt.ToString("MM/dd/yyyy HH:mm:ss");
        }

        private static void LoadDailySchedule(MainForm frm, PFSchedule sked)
        {

            if (sked.DailyFrequency == enDailyFrequency.OneTime)
            {
                frm.optDailyRunOnce.Checked = true;
                frm.txtDailyRunOnceAt.Text = PFScheduler.FormatTimeSpan(sked.OccursOnceAtTime);
                frm.txtScheduleRunsEveryNumDays.Text = sked.ScheduleOccursEveryNumDays.ToString();
            }
            else if (sked.DailyFrequency == enDailyFrequency.Recurring)
            {
                frm.optDailyRecurring.Checked = true;
                frm.txtScheduleRunsEveryNumDays.Text = sked.ScheduleOccursEveryNumDays.ToString();
                frm.txtDailyOccursEveryIntervalNum.Text = sked.DailyOccursEveryTimeInterval.ToString();
                frm.cboDailyOccursInterval.Text = sked.DailyOccursTimeInterval.ToString();
                frm.txtOccursStartingAt.Text = PFScheduler.FormatTimeSpan(sked.DailyOccursStartTime);
                frm.txtOccursEndsAt.Text = PFScheduler.FormatTimeSpan(sked.DailyOccursEndTime);
            }
            else
            {
                frm.optDailyRunOnce.Checked = false;
                frm.optDailyRecurring.Checked = false;
            }

        }

        private static void LoadWeeklySchedule(MainForm frm, PFSchedule sked)
        {
            LoadDailySchedule(frm, sked);

            frm.txtWeeklyRecursEveryNumDays.Text = sked.WeeklyOccursEveryNumWeeks.ToString();
            frm.chkWeeklyMonday.Checked = sked.WeeklySchedule.Day[0];
            frm.chkWeeklyTuesday.Checked = sked.WeeklySchedule.Day[1];
            frm.chkWeeklyWednesday.Checked = sked.WeeklySchedule.Day[2];
            frm.chkWeeklyThursday.Checked = sked.WeeklySchedule.Day[3];
            frm.chkWeeklyFriday.Checked = sked.WeeklySchedule.Day[4];
            frm.chkWeeklySaturday.Checked = sked.WeeklySchedule.Day[5];
            frm.chkWeeklySunday.Checked = sked.WeeklySchedule.Day[6];

        }

        private static void LoadMonthlySchedule(MainForm frm, PFSchedule sked)
        {
            LoadDailySchedule(frm, sked);

            if (sked.MonthlyScheduleMonthIdType == enMonthlyScheduleMonthIdType.EveryNumMonths)
            {
                frm.optOccursEveryMonthNum.Checked = true;
                frm.txtMonthlyOccursIntervalNum.Text = sked.MonthlyOccursEveryNumMonths.ToString();
            }
            else if (sked.MonthlyScheduleMonthIdType == enMonthlyScheduleMonthIdType.OccursDuringMonthName)
            {
                frm.optOccursDuringMonthName.Checked = true;
                frm.chkMonthlyJan.Checked = sked.MonthlySchedule.Month[1];
                frm.chkMonthlyFeb.Checked = sked.MonthlySchedule.Month[2];
                frm.chkMonthlyMar.Checked = sked.MonthlySchedule.Month[3];
                frm.chkMonthlyApr.Checked = sked.MonthlySchedule.Month[4];
                frm.chkMonthlyMay.Checked = sked.MonthlySchedule.Month[5];
                frm.chkMonthlyJun.Checked = sked.MonthlySchedule.Month[6];
                frm.chkMonthlyJul.Checked = sked.MonthlySchedule.Month[7];
                frm.chkMonthlyAug.Checked = sked.MonthlySchedule.Month[8];
                frm.chkMonthlySep.Checked = sked.MonthlySchedule.Month[9];
                frm.chkMonthlyOct.Checked = sked.MonthlySchedule.Month[10];
                frm.chkMonthlyNov.Checked = sked.MonthlySchedule.Month[11];
                frm.chkMonthlyDec.Checked = sked.MonthlySchedule.Month[12];
            }
            else
            {
                frm.optOccursEveryMonthNum.Checked = true;
                frm.txtMonthlyOccursIntervalNum.Text = "1";
            }

            if(sked.MonthlyScheduleDayIdType == enMonthlyScheduleDayIdType.DayName)
            {
                frm.cboMonthlyDayNameOrdinal.Enabled = true;
                frm.cboMonthlyDayName.Enabled = true;
                frm.optMonthlyDayName.Checked = true;
                frm.cboMonthlyDayNameOrdinal.Text = sked.MonthlyScheduleOrdinal.ToString();
                frm.cboMonthlyDayName.Text = sked.MonthlyScheduleDay.ToString();
                frm.txtMonthlyDayNumber.Text = "1";
                frm.txtMonthlyDayNumber.Enabled = false;
            }
            else if (sked.MonthlyScheduleDayIdType == enMonthlyScheduleDayIdType.DayNumber)
            {
                frm.cboMonthlyDayNameOrdinal.Enabled = false;
                frm.cboMonthlyDayName.Enabled = false;
                frm.txtMonthlyDayNumber.Enabled = true;
                frm.optMonthlyDayNumber.Checked = true;
                frm.txtMonthlyDayNumber.Text = sked.MonthlyDayNumber.ToString();
            }
            else
            {
                frm.cboMonthlyDayNameOrdinal.Enabled = false;
                frm.cboMonthlyDayName.Enabled = false;
                frm.txtMonthlyDayNumber.Enabled = true;
                frm.optMonthlyDayName.Checked = false;
                frm.optMonthlyDayNumber.Checked = true;
                frm.txtMonthlyDayNumber.Text = "1";
            }


        }



        public static void GetNextScheduleDateTest(MainForm frm)
        {
            PFSchedule sked = null;
            DateTime prevDateTime = DateTime.MinValue;
            DateTime currDateTime = DateTime.MinValue;
            DateTime searchEndDateTime = DateTime.MaxValue;
            DateTime nextScheduleDateTime = DateTime.MaxValue;
            DateTime currentScheduleDateTime = DateTime.MaxValue;
            DateTime scheduleEndDate = Convert.ToDateTime(frm.txtScheduleEnd.Text.Trim());
            Stopwatch sw = new Stopwatch();

            try
            {
                _msg.Length = 0;
                _msg.Append("GetNextScheduleDateTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                sked = CreateScheduleFromScreenInput(frm);

                currDateTime = Convert.ToDateTime(frm.txtCurrDateTime.Text);
                currentScheduleDateTime = sked.GetCurrentScheduledDateTime(currDateTime, searchEndDateTime);

                prevDateTime = Convert.ToDateTime(frm.txtCurrDateTime.Text);
                searchEndDateTime = prevDateTime.Add(new TimeSpan(366,0,0,0));
                if (scheduleEndDate < searchEndDateTime)
                    searchEndDateTime = scheduleEndDate;

                nextScheduleDateTime = sked.GetNextScheduledDateTime(prevDateTime, searchEndDateTime);

                _msg.Length = 0;
                _msg.Append("Curr scheduled date/time: ");
                _msg.Append(currentScheduleDateTime.ToString("MM/dd/yyyy HH:mm:ss"));
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("Next scheduled date/time: ");
                _msg.Append(nextScheduleDateTime.ToString("MM/dd/yyyy HH:mm:ss"));
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("\r\n");
                _msg.Append("Elapsed time: ");
                _msg.Append(sw.FormattedElapsedTime);
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
                _msg.Append("\r\n... GetNextScheduleDateTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void ShowAllScheduledDates(MainForm frm)
        {
            List<DateTime> scheduledDates;
            PFSchedule sked = null;
            DateTime prevDateTime = DateTime.MinValue;
            DateTime searchEndDateTime = DateTime.MaxValue;
            DateTime scheduleEndDate = Convert.ToDateTime(frm.txtScheduleEnd.Text.Trim());
            Stopwatch sw = new Stopwatch();

            try
            {
                _msg.Length = 0;
                _msg.Append("ShowAllScheduledDates started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                sked = CreateScheduleFromScreenInput(frm);

                prevDateTime = Convert.ToDateTime(frm.txtCurrDateTime.Text);
                searchEndDateTime = Convert.ToDateTime(frm.txtScheduleEnd.Text);
                if (scheduleEndDate < searchEndDateTime)
                    searchEndDateTime = scheduleEndDate;

                scheduledDates = sked.GetListOfScheduledDates(prevDateTime, searchEndDateTime);

                _msg.Length=0;
                _msg.Append("\r\n");
                _msg.Append("Total # of scheduled dates: ");
                _msg.Append(scheduledDates.Count.ToString("#,##0"));
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                for (int inx = 0; inx < scheduledDates.Count; inx++)
                {
                    _msg.Length = 0;
                    _msg.Append(scheduledDates[inx].ToString("MM/dd/yyyy HH:mm:ss"));
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("\r\n");
                _msg.Append("Elapsed time: ");
                _msg.Append(sw.FormattedElapsedTime);
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
                _msg.Append("\r\n... ShowAllScheduledDates finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        


    }//end class
}//end namespace
