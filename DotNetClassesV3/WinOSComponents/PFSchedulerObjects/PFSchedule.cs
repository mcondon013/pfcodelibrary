//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Data.Common;
using AppGlobals;
using PFDataAccessObjects;
using PFCollectionsObjects;

namespace PFSchedulerObjects
{
    /// <summary>
    /// Class for storing information about schedules.
    /// </summary>
    public class PFSchedule
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        private DateTime _currentScheduledStartTime = DateTime.MaxValue;
        private TimeSpan _dailyTimeIntervalWindowStartTime = TimeSpan.MaxValue;

        //private variables for properties
        private Guid _id = Guid.NewGuid();
        private string _name = string.Empty;
        private enScheduleFrequency _scheduleFrequency = enScheduleFrequency.Unknown;
        private int _scheduleOccursEveryNumDays = 1;
        private DateTime _runAt = DateTime.MaxValue;
        private TimeSpan _occursOnceAtTime = new TimeSpan(0, 0, 0, 0, 0);
        private TimeSpan _runWindow = new TimeSpan(0, 5, 0); //default is 5 minute window for schedule active period.

        private enDailyFrequency _dailyFrequency = enDailyFrequency.Unknown;
        private int _dailyOccursEveryTimeInterval = 1;
        private enDailyOccursInterval _dailyOccursTimeInterval = enDailyOccursInterval.Unknown;
        private TimeSpan _dailyOccursStartTime = new TimeSpan(0, 0, 0, 0, 0);
        private TimeSpan _dailyOccursEndTime = new TimeSpan(0, 23, 59, 59, 990);
        private DateTime _scheduleStartDate = DateTime.MinValue;
        private DateTime _scheduleEndDate = DateTime.MaxValue;

        private int _weeklyOccursEveryNumWeeks = 1;
        private CWeeklySchedule _weeklySchedule = new CWeeklySchedule();

        private int _monthlyOccursEveryNumMonths = 1;
        private CMonthlySchedule _monthlySchedule = new CMonthlySchedule();
        private enMonthlyScheduleMonthIdType _monthlyScheduleMonthIdType = enMonthlyScheduleMonthIdType.Unknown;
        private enMonthlyScheduleDayIdType _monthlyScheduleDayIdType = enMonthlyScheduleDayIdType.DayNumber;
        private int _monthlyDayNumber = 1;
        private enMontlyScheduleOrdinal _monthlyScheduleOrdinal = enMontlyScheduleOrdinal.First;
        private enScheduleDay _monthlyScheduleDay = enScheduleDay.Monday;


        private enScheduleLookupResult _scheduleLookupResult = enScheduleLookupResult.Unknown;

        //following variables used by routines that read and write data from databases (instead of xml files)
        private string _scheduleDefinitionsSelectAllSQL = "select ScheduleObject from Schedules order by ScheduleName";
        private string _scheduleDefinitionsSelectScheduleSQL = "select ScheduleObject from Schedules where ScheduleName = '<schedulename>'";
        private string _scheduleDefinitionsUpdateSQL = "update Schedules set ScheduleObject = '<scheduleobject>' where ScheduleName = '<schedulename>'";
        private string _scheduleDefinitionsInsertSQL = "insert Schedules (ScheduleName, ScheduleObject) values ('<schedulename>', '<scheduleobject>')";
        private string _scheduleDefinitionsDeleteSQL = "delete Schedules where ScheduleName = '<schedulename>'";
        private string _scheduleDefinitionsIfScheduleExistsSQL = "select count(*) as numRecsFound from Schedules  where ScheduleName = '<schedulename>'";


        //following variables used by test procedures
        private DateTime _testCurrDateTime = DateTime.MinValue;
        private enScheduleLookupResult _expectedTestResult = enScheduleLookupResult.Unknown;
        
        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFSchedule()
        {
            
            InitInstance();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Name used to identify the schedule encapsulated by instance of this class.</param>
        public PFSchedule(string name)
        {
            _name = name;
            InitInstance();
        }


        private void InitInstance()
        {
            string configValue = string.Empty;

            configValue = AppConfig.GetStringValueFromConfigFile("ScheduleDefinitionsSelectAllSQL", string.Empty);
            if (configValue.Length > 0)
                _scheduleDefinitionsSelectAllSQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("ScheduleDefinitionsSelectScheduleSQL", string.Empty);
            if (configValue.Length > 0)
                _scheduleDefinitionsSelectScheduleSQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("ScheduleDefinitionsInsertSQL", string.Empty);
            if (configValue.Length > 0)
                _scheduleDefinitionsInsertSQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("ScheduleDefinitionsUpdateSQL", string.Empty);
            if (configValue.Length > 0)
                _scheduleDefinitionsUpdateSQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("ScheduleDefinitionsDeleteSQL", string.Empty);
            if (configValue.Length > 0)
                _scheduleDefinitionsDeleteSQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("ScheduleDefinitionsIfScheduleExistsSQL", string.Empty);
            if (configValue.Length > 0)
                _scheduleDefinitionsIfScheduleExistsSQL = configValue;

        }

        //properties

        /// <summary>
        /// ID (Guid) that uniquely identifies this instance of the class.
        /// </summary>
        public Guid ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// Name used to identify the schedule encapsulated by instance of this class.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// Specifies type of schedule that is enacapsulated by the class.
        /// </summary>
        public enScheduleFrequency ScheduleFrequency
        {
            get
            {
                return _scheduleFrequency;
            }
            set
            {
                _scheduleFrequency = value;
            }
        }

        /// <summary>
        /// Day interval for the daily schedule to run.
        /// Examples: daily schedule is run every 1 day
        /// or daily schedule is run every 2 days etc.
        /// </summary>
        public int ScheduleOccursEveryNumDays
        {
            get
            {
                return _scheduleOccursEveryNumDays;
            }
            set
            {
                _scheduleOccursEveryNumDays = value;
            }
        }

        /// <summary>
        /// Date and time assigned to a run one time schedule.
        /// </summary>
        public DateTime RunAt
        {
            get
            {
                return _runAt;
            }
            set
            {
                _runAt = value;
            }
        }

        /// <summary>
        /// Time at which a OneTime occurrance for a given day will occur.
        /// </summary>
        [XmlIgnore]
        public TimeSpan OccursOnceAtTime
        {
            get
            {
                return _occursOnceAtTime;
            }
            set
            {
                _occursOnceAtTime = value;
            }
        }

        /// <summary>
        /// Time at which a OneTime occurrance for a given day will occur. Interval is defined in terms of timer ticks (1 tick = 100 nanoseconds).
        /// </summary>
        /// <remarks>OccursOnceAtTimeTicks is usually only used for purposes of serializing an instance to an XML file. Use the OccursOnceAtTime property to set and get the OccursOnceAtTime value as a TimeSpan.</remarks>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public long OccursOnceAtTimeTicks
        {
            get
            {
                return _occursOnceAtTime.Ticks;
            }
            set
            {
                _occursOnceAtTime = new TimeSpan(value); 
            }
        }

        /// <summary>
        /// Time interval from defined runat time in which a delayed scheduled task can be started.
        /// </summary>
        /// <remarks>Example: If schedule defines start time as 1 AM and the RunWindow is 15 minutes, then a task using this schedule can be started at anytime between 1 AM and 1:15 AM.</remarks>
        [XmlIgnore]
        public TimeSpan RunWindow
        {
            get
            {
                return _runWindow;
            }
            set
            {
                _runWindow = value;
            }
        }

        /// <summary>
        /// Time interval from defined runat time in which a delayed scheduled task can be started. Interval is defined in terms of timer ticks (1 tick = 100 nanoseconds).
        /// </summary>
        /// <remarks>Example: If schedule defines start time as 1 AM and the RunWindow is 15 minutes, then a task using this schedule can be started at anytime between 1 AM and 1:15 AM.
        ///  RunWindowTicks is usually only used for purposes of serializing an instance to an XML file. Use the RunWindow property to set and get the window value as a TimeSpan.</remarks>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public long RunWindowTicks
        {
            get
            {
                return _runWindow.Ticks;
            }
            set
            {
                _runWindow = new TimeSpan(value);
            }
        }

        /// <summary>
        /// Will be OneTime or Recurring.
        /// </summary>
        public enDailyFrequency DailyFrequency
        {
            get
            {
                return _dailyFrequency;
            }
            set
            {
                _dailyFrequency = value;
            }
        }

        /// <summary>
        /// As in Occurs every 1 hour or Occurs every 2 minutes.
        /// </summary>
        public int DailyOccursEveryTimeInterval
        {
            get
            {
                return _dailyOccursEveryTimeInterval;
            }
            set
            {
                _dailyOccursEveryTimeInterval = value;
            }
        }

        /// <summary>
        /// Specify the interval for determining daily occurances: hourly, minutes, seconds.
        /// DailyOccursEveryTimeInterval property determines how many time interval is active in the daily schedule.
        /// </summary>
        public enDailyOccursInterval DailyOccursTimeInterval
        {
            get
            {
                return _dailyOccursTimeInterval;
            }
            set
            {
                _dailyOccursTimeInterval = value;
            }
        }

        /// <summary>
        /// Specifies beginning of range of times during which occurances will happen.
        /// For example, occurrances range might be to start at 01:00 hours and end at 22:00 hours
        /// </summary>
        [XmlIgnore]
        public TimeSpan DailyOccursStartTime
        {
            get
            {
                return _dailyOccursStartTime;
            }
            set
            {
                _dailyOccursStartTime = value;
            }
        }

        /// <summary>
        /// Specifies beginning of range of times during which occurances will happen.
        /// For example, occurrances range might be to start at 01:00 hours and end at 22:00 hours
        /// </summary>
        /// <remarks>DailyOccursStartTimeTicks is usually only used for purposes of serializing an instance to an XML file. Use the DailyOccursStartTime property to set and get the DailyOccursStartTime value as a TimeSpan.</remarks>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public long DailyOccursStartTimeTicks
        {
            get
            {
                return _dailyOccursStartTime.Ticks;
            }
            set
            {
                _dailyOccursStartTime = new TimeSpan(value);
            }
        }

        /// <summary>
        /// Specifies end of range of times during which occurances will happen.
        /// For example, occurrances range might be to start at 01:00 hours and end at 22:00 hours
        /// </summary>
        [XmlIgnore]
        public TimeSpan DailyOccursEndTime
        {
            get
            {
                return _dailyOccursEndTime;
            }
            set
            {
                _dailyOccursEndTime = value;
            }
        }

        /// <summary>
        /// Specifies end of range of times during which occurances will happen.
        /// For example, occurrances range might be to start at 01:00 hours and end at 22:00 hours
        /// </summary>
        /// <remarks>DailyOccursEndTimeTicks is usually only used for purposes of serializing an instance to an XML file. Use the DailyOccursEndTime property to set and get the DailyOccursEndTime value as a TimeSpan.</remarks>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public long DailyOccursEndTimeTicks
        {
            get
            {
                return _dailyOccursEndTime.Ticks;
            }
            set
            {
                _dailyOccursEndTime = new TimeSpan(value);
            }
        }

        /// <summary>
        /// ScheduleStartDate Property.
        /// </summary>
        public DateTime ScheduleStartDate
        {
            get
            {
                return _scheduleStartDate;
            }
            set
            {
                _scheduleStartDate = value;
            }
        }

        /// <summary>
        /// ScheduleEndDate Property.
        /// </summary>
        public DateTime ScheduleEndDate
        {
            get
            {
                return _scheduleEndDate;
            }
            set
            {
                _scheduleEndDate = value;
            }
        }

        /// <summary>
        /// Value of 1 for this property means every week. Value of 2 means every second week. Value of 3 means every third week. Etc.
        /// </summary>
        public int WeeklyOccursEveryNumWeeks
        {
            get
            {
                return _weeklyOccursEveryNumWeeks;
            }
            set
            {
                _weeklyOccursEveryNumWeeks = value;
            }
        }

        /// <summary>
        /// Object containing one true/false entry for each day of the week. If entry is true, then day corresponding to that entry number is scheduled.
        ///  For example, (WeeklySchedule)sked.Day[0] refers to Monday.
        /// </summary>
        public CWeeklySchedule WeeklySchedule
        {
            get
            {
                return _weeklySchedule;
            }
            set
            {
                _weeklySchedule = value;
            }
        }

        /// <summary>
        /// MonthlyOccursEveryNumMonths Property.
        /// </summary>
        public int MonthlyOccursEveryNumMonths
        {
            get
            {
                return _monthlyOccursEveryNumMonths;
            }
            set
            {
                _monthlyOccursEveryNumMonths = value;
            }
        }

        /// <summary>
        /// Object containing one true/false entry for each month of year. If entry is true, then month corresponding to that entry number is scheduled.
        ///  For example, (MonthlySchedule)sked.Month[0] refers to January. (MonthlySchedule)sked.Month[11] refers to December.
        /// </summary>
        public CMonthlySchedule MonthlySchedule
        {
            get
            {
                return _monthlySchedule;
            }
            set
            {
                _monthlySchedule = value;
            }
        }


        /// <summary>
        /// Determines whether month for schedule is identified by month interval (e.g. every 1 months or every 2 months) or by a specific month name (January through December).
        /// </summary>
        public enMonthlyScheduleMonthIdType MonthlyScheduleMonthIdType
        {
            get
            {
                return _monthlyScheduleMonthIdType;
            }
            set
            {
                _monthlyScheduleMonthIdType = value;
            }
        }

        /// <summary>
        /// Determines whether day of month for schedule is identified by DayNumber (1 to 29/30/31) or DayName (Monday through Sunday).
        /// </summary>
        public enMonthlyScheduleDayIdType MonthlyScheduleDayIdType
        {
            get
            {
                return _monthlyScheduleDayIdType;
            }
            set
            {
                _monthlyScheduleDayIdType = value;
            }
        }


        /// <summary>
        /// Number of day in month in which schedule is active.
        /// </summary>
        public int MonthlyDayNumber
        {
            get
            {
                return _monthlyDayNumber;
            }
            set
            {
                _monthlyDayNumber = value;
            }
        }

        /// <summary>
        /// Specifies whether MonthlyScheduleDay is the First, Second, Third, Fourth or Last day by that name of the month
        ///  For example: First Monday of every 1 month.
        /// </summary>
        public enMontlyScheduleOrdinal MonthlyScheduleOrdinal
        {
            get
            {
                return _monthlyScheduleOrdinal;
            }
            set
            {
                _monthlyScheduleOrdinal = value;
            }
        }

        /// <summary>
        /// Day name on which monthly schedule is active.
        /// </summary>
        public enScheduleDay MonthlyScheduleDay
        {
            get
            {
                return _monthlyScheduleDay;
            }
            set
            {
                _monthlyScheduleDay = value;
            }
        }

        /// <summary>
        /// Returns whether or not supplied date is on schedule. If not on schedule, reason for exclusion is given.
        /// </summary>
        public enScheduleLookupResult ScheduleLookupResult
        {
            get
            {
                return _scheduleLookupResult;
            }
        }

        //following properties used for testing

        /// <summary>
        /// Date/time value used for testing purposes.
        /// </summary>
        public DateTime TestCurrDateTime
        {
            get
            {
                return _testCurrDateTime;
            }
            set
            {
                _testCurrDateTime = value;
            }
        }

        /// <summary>
        /// Expected lookup result when value of TestCurrDateTime is checked by DateIsScheduled function.
        /// </summary>
        public enScheduleLookupResult ExpectedTestResult
        {
            get
            {
                return _expectedTestResult;
            }
            set
            {
                _expectedTestResult = value;
            }
        }



        //methods

        /// <summary>
        /// Sets date/time for a run one time schedule.
        /// </summary>
        /// <param name="runDate">Date and time assigned to a run one time schedule.</param>
        public void SetRunOneTimeDate(DateTime runDate)
        {
            this.RunAt = runDate;
        }

        /// <summary>
        /// Routine to define a schedule that runs once daily.
        /// </summary>
        /// <param name="scheduleOccursEveryNumDays">Specify 1 if schedule is for every day, specify 2 if for every second day, specify 3 for every third day, ... etc.</param>
        /// <param name="runWindowInMinutes">The numbers of minutes from the specified occursAt time during which schedule can be activated. For example, you can schedule 
        /// a job to run 1:00 AM with a 15 minute window. If schedule is not checked at 1 AM, job can still be run anytime during the next 15 minutes. Note: schedule will be activated only once during the run window.</param>
        /// <param name="occursAt">The time schedule is active.</param>
        /// <param name="scheduleStartDate">Specifies date/time from which schedule is enabled.</param>
        /// <param name="scheduleEndDate">Specifies date/time at which schedule is no longer enabled. (Specify at very large date such as 12/31/9999 for no end date.)</param>
        public void SetDailyOneTimeSchedule(int scheduleOccursEveryNumDays, int runWindowInMinutes, TimeSpan occursAt, DateTime scheduleStartDate, DateTime scheduleEndDate)
        {
            _dailyFrequency = enDailyFrequency.OneTime;
            _scheduleOccursEveryNumDays = scheduleOccursEveryNumDays;
            _runWindow = new TimeSpan(0, runWindowInMinutes, 0);
            _occursOnceAtTime = occursAt;
            _scheduleStartDate = scheduleStartDate;
            _scheduleEndDate = scheduleEndDate;
        }

        /// <summary>
        /// Routine to define a schedule that runs daily with a recurring schedule during the day.
        /// </summary>
        /// <param name="scheduleOccursEveryNumDays">Specify 1 if schedule is for every day, specify 2 if for every second day, specify 3 for every third day, ... etc.</param>
        /// <param name="runWindowInMinutes">The numbers of minutes from the specified occursAt time during which schedule can be activated. For example, you can schedule 
        /// a job to run 1:00 AM with a 15 minute window. If schedule is not checked at 1 AM, job can still be run anytime during the next 15 minutes. Note: schedule will be activated only once during the run window.</param>
        /// <param name="dailyOccursEveryTimeInterval">Number that qualifies time interval: every 2 hours, every 5 minutes, every 10 seconds.</param>
        /// <param name="dailyOccursTimeInterval">Time interval used for the recurring schedule: hour(s), minute(s) or second(s)</param>
        /// <param name="dailyOccursStartTime">Time at which the daily recurrances of the schedule begin.</param>
        /// <param name="dailyOccursEndTime">Time at which the daily recurrances of the schedule end. </param>
        /// <param name="scheduleStartDate">Specifies date/time from which schedule is enabled.</param>
        /// <param name="scheduleEndDate">Specifies date/time at which schedule is no longer enabled. (Specify at very large date such as 12/31/9999 for no end date.)</param>
        /// <remarks>SetDailyRecurringSchedule(1, 15, 1, enDailyOccursInterval.Hour, new TimeSpan(2, 30, 0), new TimeSpan(22, 30, 0), Convert.ToDateTime("7/1/2013"), Convert.ToDateTime("12/31/2014 23:59:59")) means the following:
        ///  Run every 1 day with a 15 minute windows for the start times. Schedule is recurring every hour starting at 2:30 AM and ending at 10:30 PM. Schedule is enabled from July 1, 2013 through the end of day 12/31/2014.</remarks>
        public void SetDailyRecurringSchedule(int scheduleOccursEveryNumDays, int runWindowInMinutes,
                                              int dailyOccursEveryTimeInterval, enDailyOccursInterval dailyOccursTimeInterval,
                                              TimeSpan dailyOccursStartTime, TimeSpan dailyOccursEndTime,
                                              DateTime scheduleStartDate, DateTime scheduleEndDate)
        {
            _dailyFrequency = enDailyFrequency.Recurring;
            _scheduleOccursEveryNumDays = scheduleOccursEveryNumDays;
            _runWindow = new TimeSpan(0, runWindowInMinutes, 0);
            _dailyOccursEveryTimeInterval = dailyOccursEveryTimeInterval;
            _dailyOccursTimeInterval = dailyOccursTimeInterval;
            _dailyOccursStartTime = dailyOccursStartTime;
            _dailyOccursEndTime = dailyOccursEndTime;
            _scheduleStartDate = scheduleStartDate;
            _scheduleEndDate = scheduleEndDate;

        }

        /// <summary>
        /// Routine to determine if date/time value has been scheduled.
        /// </summary>
        /// <param name="dtm">Date/time value to check.</param>
        /// <returns>Returns true if date and time supplied in parameter has been scheduled by this instance's schedule.</returns>
        public bool DateIsScheduled(DateTime dtm)
        {
            bool result = false;

            _currentScheduledStartTime = DateTime.MaxValue;
            _dailyTimeIntervalWindowStartTime = TimeSpan.MaxValue;

            if (dtm >= this.ScheduleStartDate && dtm <= this.ScheduleEndDate)
            {
                if (this.ScheduleFrequency == enScheduleFrequency.OneTime)
                {
                    result = CheckRunOneTimeSchedule(dtm);
                }
                else if (this.ScheduleFrequency == enScheduleFrequency.Daily)
                {
                    result = CheckDailySchedule(dtm);
                }
                else if (this.ScheduleFrequency == enScheduleFrequency.Weekly)
                {
                    result = CheckWeeklySchedule(dtm);
                }
                else if (this.ScheduleFrequency == enScheduleFrequency.Monthly)
                {
                    result = CheckMonthlySchedule(dtm);
                }
                else
                {
                    //error
                    _msg.Length = 0;
                    _msg.Append(this.ScheduleFrequency.ToString());
                    _msg.Append(" is invalid schedule frequency value.");
                    throw new System.Exception(_msg.ToString());
                }
            }
            else
            {
                _scheduleLookupResult = enScheduleLookupResult.DateOutsideScheduleStartAndEnd;
            }

            return result;
        }

        /// <summary>
        /// Routine to determine if date/time value has been scheduled.
        /// </summary>
        /// <param name="dtm">Date/time value to check.</param>
        /// <returns>Returns true if date and time supplied in parameter has been scheduled by this instance's schedule.</returns>
        public bool CheckRunOneTimeSchedule(DateTime dtm)
        {
            bool result = false;
            long dateToCompare = dtm.Ticks;
            long runAtDate = this.RunAt.Ticks;
            long maxRunAtDate = this.RunAt.Add(this.RunWindow).Ticks;

            if (dateToCompare >= runAtDate && dateToCompare <= maxRunAtDate)
            {
                result = true;
                _scheduleLookupResult = enScheduleLookupResult.DateIsScheduled;
                _currentScheduledStartTime = this.RunAt;
                _dailyTimeIntervalWindowStartTime = new TimeSpan(this.RunAt.Hour, this.RunAt.Minute, this.RunAt.Second);
            }
            else
            {
                _scheduleLookupResult = enScheduleLookupResult.DateOutsideRunAtWindow;
            }


            return result;
        }


        private bool CheckDailySchedule(DateTime dtm)
        {
            bool result = false;

            if (this.DailyFrequency == enDailyFrequency.OneTime)
                result = CheckDailyOneTimeSchedule(dtm);
            else if (this.DailyFrequency == enDailyFrequency.Recurring)
                result = CheckDailyRecurringSchedule(dtm);
            else
            {
                //error
                _msg.Length = 0;
                _msg.Append(this.DailyFrequency.ToString());
                _msg.Append(" is invalid daily frequency value.");
                throw new System.Exception(_msg.ToString());
            }


            return result;
        }


        private bool CheckDailyOneTimeSchedule(DateTime dtm)
        {
            bool result = false;
            TimeSpan currTime = new TimeSpan(dtm.Hour, dtm.Minute, dtm.Second);

            if (DailyDayIsScheduled(dtm))
            {
                if (currTime >= this.OccursOnceAtTime && currTime <= (this.OccursOnceAtTime + this.RunWindow))
                {
                    result = true;
                    _scheduleLookupResult = enScheduleLookupResult.DateIsScheduled;
                    _currentScheduledStartTime = new DateTime(dtm.Year, dtm.Month, dtm.Day,
                                                              this.OccursOnceAtTime.Hours, this.OccursOnceAtTime.Minutes, this.OccursOnceAtTime.Seconds);
                    _dailyTimeIntervalWindowStartTime = new TimeSpan(this.OccursOnceAtTime.Hours, this.OccursOnceAtTime.Minutes, this.OccursOnceAtTime.Seconds);
                }
                else
                {
                    _scheduleLookupResult = enScheduleLookupResult.TimeOutsideDailyRunAtWindow;
                }
            }
            else
            {
                _scheduleLookupResult = enScheduleLookupResult.DayNotScheduled;
            }

            return result;
        }

        private bool DailyDayIsScheduled(DateTime dtm)
        {
            bool result = false;
            int numDaysSinceStart = (dtm - this.ScheduleStartDate).Days;

            if ((numDaysSinceStart % this.ScheduleOccursEveryNumDays) == 0)
                result = true;

            return result;
        }

        private bool CheckDailyRecurringSchedule(DateTime dtm)
        {
            bool result = false;
            TimeSpan currTime = new TimeSpan(dtm.Hour, dtm.Minute, dtm.Second);

            if (DailyDayIsScheduled(dtm))
            {
                if (currTime >= this.DailyOccursStartTime && currTime <= this.DailyOccursEndTime)
                {
                    if (DailyTimeIntervalIsScheduled(dtm))
                    {
                        if (DailyTimeIsScheduled(dtm))
                        {
                            result = true;
                            _scheduleLookupResult = enScheduleLookupResult.DateIsScheduled;
                            _currentScheduledStartTime = new DateTime(dtm.Year, dtm.Month, dtm.Day,
                                                                      _dailyTimeIntervalWindowStartTime.Hours,    //_dailyTimeIntervalWindowStartTime set in DailyTimeIsScheduled
                                                                      _dailyTimeIntervalWindowStartTime.Minutes,
                                                                      _dailyTimeIntervalWindowStartTime.Seconds);
                        }
                        else
                        {
                            _scheduleLookupResult = enScheduleLookupResult.TimeNotScheduled;
                        }
                    }
                    else
                    {
                        _scheduleLookupResult = enScheduleLookupResult.TimeIntervalNotScheduled;
                    }
                }
                else
                {
                    _scheduleLookupResult = enScheduleLookupResult.TimeOutsideDailyRunAtWindow;
                }
            }
            else
            {
                _scheduleLookupResult = enScheduleLookupResult.DayNotScheduled;
            }


            return result;
        }

        private bool DailyTimeIntervalIsScheduled(DateTime dtm)
        {
            bool result = false;
            TimeSpan currTime = new TimeSpan(dtm.Hour, dtm.Minute, dtm.Second);

            switch (this.DailyOccursTimeInterval)
            {
                case enDailyOccursInterval.Hours:
                    if (((currTime.Hours - this.DailyOccursStartTime.Hours) % this.DailyOccursEveryTimeInterval) == 0)
                        result = true;
                    break;
                case enDailyOccursInterval.Minutes:
                    if (((currTime.Minutes - this.DailyOccursStartTime.Minutes) % this.DailyOccursEveryTimeInterval) == 0)
                        result = true;
                    break;
                case enDailyOccursInterval.Seconds:
                    if (((currTime.Seconds - this.DailyOccursStartTime.Seconds) % this.DailyOccursEveryTimeInterval) == 0)
                        result = true;
                    break;
                default:
                    break;
            }

            return result;
        }


        private bool DailyTimeIsScheduled(DateTime dtm)
        {
            bool result = false;
            bool intervalIsValid = true;
            TimeSpan currTime = new TimeSpan(dtm.Hour, dtm.Minute, dtm.Second);
            TimeSpan intervalTimeWindowStart;
            TimeSpan intervalTimeWindowEnd;


            switch (this.DailyOccursTimeInterval)
            {
                case enDailyOccursInterval.Hours:
                    intervalTimeWindowStart = new TimeSpan(dtm.Hour, 0, 0);
                    intervalTimeWindowEnd = intervalTimeWindowStart.Add(this.RunWindow);
                    if (currTime >= intervalTimeWindowStart && currTime <= intervalTimeWindowEnd)
                        result = true;
                    break;
                case enDailyOccursInterval.Minutes:
                    intervalTimeWindowStart = new TimeSpan(dtm.Hour, dtm.Minute, 0);
                    break;
                case enDailyOccursInterval.Seconds:
                    intervalTimeWindowStart = new TimeSpan(dtm.Hour, dtm.Minute, dtm.Second);
                    break;
                default:
                    intervalTimeWindowStart = new TimeSpan(dtm.Hour, dtm.Minute, dtm.Second);
                    intervalIsValid = false;
                    break;
            }

            if (intervalIsValid)
            {
                _dailyTimeIntervalWindowStartTime = intervalTimeWindowStart;
                intervalTimeWindowEnd = intervalTimeWindowStart.Add(this.RunWindow);
                if (currTime >= intervalTimeWindowStart && currTime <= intervalTimeWindowEnd)
                    result = true;
            }

            return result;
        }

        private bool CheckWeeklySchedule(DateTime dtm)
        {
            bool result = false;

            if (WeekIsOnWeeklySchedule(dtm))
            {
                if (DayIsOnWeeklySchedule(dtm))
                {
                    result = true;
                    _scheduleLookupResult = enScheduleLookupResult.DateIsScheduled;
                }
                else
                {
                    this._scheduleLookupResult = enScheduleLookupResult.DayNotScheduled;
                }
            }
            else
            {
                this._scheduleLookupResult = enScheduleLookupResult.WeekNotScheduled;
            }

            if (result == true)
            {
                result = CheckDailySchedule(dtm);
            }

            return result;
        }


        private bool WeekIsOnWeeklySchedule(DateTime dtm)
        {
            bool result = false;
            int numDaysSinceSkedStart = Convert.ToInt32(dtm.Subtract(this.ScheduleStartDate).TotalDays);
            int weekNumSinceSkedStart = (numDaysSinceSkedStart / 7) + 1;

            if ((weekNumSinceSkedStart % this.WeeklyOccursEveryNumWeeks) == 0)
                result = true;

            return result;
        }

        private bool DayIsOnWeeklySchedule(DateTime dtm)
        {
            bool result = false;
            string dayname = dtm.DayOfWeek.ToString();

            for (int i = 0; i < this.WeeklySchedule.Day.Length; i++)
            {
                if (this.WeeklySchedule.Day[i] == true)
                {
                    enScheduleDay day = (enScheduleDay)i;
                    if (dayname == day.ToString())
                        result = true;
                }
            }

            return result;
        }


        private bool CheckMonthlySchedule(DateTime dtm)
        {
            bool result = false;

            if (MonthIsOnMonthlySchedule(dtm))
            {
                if (MonthDayIsOnMonthlySchedule(dtm))
                {
                    result = true;
                    _scheduleLookupResult = enScheduleLookupResult.DateIsScheduled;
                }
                else
                {
                    _scheduleLookupResult = enScheduleLookupResult.MonthDayNotScheduled;
                }
            }
            else
            {
                _scheduleLookupResult = enScheduleLookupResult.MonthNotScheduled;
            }

            if (result == true)
            {
                result = CheckDailySchedule(dtm);
            }

            return result;
        }

        private bool MonthIsOnMonthlySchedule(DateTime dtm)
        {
            bool result = false;

            if (this.MonthlyScheduleMonthIdType == enMonthlyScheduleMonthIdType.EveryNumMonths)
            {
                result = CheckIfMonthNumOnSchedule(dtm);
            }
            else if (this.MonthlyScheduleMonthIdType == enMonthlyScheduleMonthIdType.OccursDuringMonthName)
            {
                result = CheckIfMonthNameOnSchedule(dtm);
            }
            else
            {
                result = false;
            }

            return result;
        }

        private bool MonthDayIsOnMonthlySchedule(DateTime dtm)
        {
            bool result = false;

            if (this.MonthlyScheduleDayIdType == enMonthlyScheduleDayIdType.DayNumber)
            {
                result = CheckIfMonthDayNumberOnSchedule(dtm);
            }
            else if (this.MonthlyScheduleDayIdType == enMonthlyScheduleDayIdType.DayName)
            {
                result = CheckIfMonthDayNameOnSchedule(dtm);
            }
            else
            {
                result = false;
            }

            return result;
        }

        private bool CheckIfMonthNumOnSchedule(DateTime dtm)
        {
            bool result = false;
            int monthsSinceScheduleStart = ((dtm.Year - this.ScheduleStartDate.Year) * 12) + (dtm.Month - this.ScheduleStartDate.Month);

            if ((monthsSinceScheduleStart % this.MonthlyOccursEveryNumMonths) == 0)
                result = true;

            return result;
        }

        private bool CheckIfMonthNameOnSchedule(DateTime dtm)
        {
            bool result = false;

            for (int i = 0; i < this.MonthlySchedule.Month.Length; i++)
            {
                if (this.MonthlySchedule.Month[i] == true)
                {
                    if(dtm.Month == i)
                        result = true;
                }
            }


            return result;
        }

        private bool CheckIfMonthDayNumberOnSchedule(DateTime dtm)
        {
            bool result = false;

            if (dtm.Day == this.MonthlyDayNumber)
                result = true;

            return result;
        }

        private bool CheckIfMonthDayNameOnSchedule(DateTime dtm)
        {
            bool result = false;
            int[] dayOfWeekCount = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
            int[] lastOccuranceOfDayOfWeek = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
            GetCountOfEachDayOfWeekDuringMonth(dtm, dayOfWeekCount, lastOccuranceOfDayOfWeek);
            int dtmDayOfWeek = (int)PFScheduler.GetScheduleDay(dtm.DayOfWeek.ToString());
            enMontlyScheduleOrdinal skedOrdinal = this.MonthlyScheduleOrdinal;

            if ((dayOfWeekCount[dtmDayOfWeek] == (int)skedOrdinal && dtm.DayOfWeek.ToString() == this.MonthlyScheduleDay.ToString())
                || (lastOccuranceOfDayOfWeek[dtmDayOfWeek] == dtm.Day && skedOrdinal == enMontlyScheduleOrdinal.Last))
            {
                result = true;
            }

            return result;
        }

        private int[] GetCountOfEachDayOfWeekDuringMonth(DateTime dtm, int[]dayOfWeekCount, int[]lastOccuranceOfDayOfWeek)
        {
            int skedMonth = dtm.Month;
            DateTime dayOfMonth = new DateTime(dtm.Year, dtm.Month, 1);

            while (dayOfMonth.Month == skedMonth)
            {
                enScheduleDay skedDay = PFScheduler.GetScheduleDay(dayOfMonth.DayOfWeek.ToString());
                if(dayOfMonth.Day <= dtm.Day)
                    dayOfWeekCount[(int)skedDay]++;
                lastOccuranceOfDayOfWeek[(int)skedDay] = dayOfMonth.Day;
                dayOfMonth = dayOfMonth.AddDays(1);
            }


            return dayOfWeekCount;
        }

        /// <summary>
        /// Returns next scheduled date/time.
        /// </summary>
        /// <param name="prevDateTime">Previously scheduled date/time.</param>
        /// <param name="endSearchDateTime">Latest date to include in the search for scheduled dates.</param>
        /// <returns>Date/Time value.</returns>
        public DateTime GetNextScheduledDateTime(DateTime prevDateTime, DateTime endSearchDateTime)
        {
            DateTime nextDateTime = DateTime.MaxValue;
            List<DateTime> scheduledDates = new List<DateTime>();

            scheduledDates = GetListOfScheduledDates(prevDateTime, endSearchDateTime, true);

            for (int inx = 0; inx < scheduledDates.Count; inx++)
            {
                if (scheduledDates[inx] > prevDateTime)
                {
                    nextDateTime = scheduledDates[inx];
                    if (this.DateIsScheduled(nextDateTime))          //do a dateisscheduled check to get the actual schedule start time for the nextdatetime
                        nextDateTime = _currentScheduledStartTime;   //  this fixes cases where the prev date time does not fall exactly on a start but does fall inside the run window
                    break;
                }
            }


            return nextDateTime;
        }


        /// <summary>
        /// Returns current scheduled date/time.
        /// </summary>
        /// <param name="currDateTime">Current date/time.</param>
        /// <param name="endSearchDateTime">Latest date to include in the search for scheduled dates.</param>
        /// <returns>Date/Time value.</returns>
        public DateTime GetCurrentScheduledDateTime(DateTime currDateTime, DateTime endSearchDateTime)
        {
            DateTime scheduledDateTime = DateTime.MaxValue;
            //List<DateTime> scheduledDates = new List<DateTime>();

            //scheduledDates = GetListOfScheduledDates(currDateTime, endSearchDateTime, true);

            //if (scheduledDates.Count > 0)
            //{
            //    scheduledDateTime = scheduledDates[0];
            //}

            if (this.DateIsScheduled(currDateTime))
            {
                scheduledDateTime = _currentScheduledStartTime;
            }

            return scheduledDateTime;
        }


        /// <summary>
        /// Returns a list of all the scheduled date/times between the given start and end date/times.
        /// </summary>
        /// <param name="startDate">Earliest date/time that can be on the list.</param>
        /// <param name="endDate">Latest date/time that can be on the list.</param>
        /// <returns>List of date/times that are scheduled by this schedule.</returns>
        public List<DateTime> GetListOfScheduledDates(DateTime startDate, DateTime endDate)
        {
            return GetListOfScheduledDates(startDate, endDate, false);
        }

        /// <summary>
        /// Returns a list of all the scheduled date/times between the given start and end date/times.
        /// </summary>
        /// <param name="startDate">Earliest date/time that can be on the list.</param>
        /// <param name="endDate">Latest date/time that can be on the list.</param>
        /// <param name="getNextScheduledDateOnly">If true then search stops after first scheduled date later than start date is found.</param>
        /// <returns>List of date/times that are scheduled by this schedule.</returns>
        public List<DateTime> GetListOfScheduledDates(DateTime startDate, DateTime endDate, bool getNextScheduledDateOnly)
        {
            List<DateTime> scheduledDates = new List<DateTime>();

            if (this.ScheduleFrequency == enScheduleFrequency.OneTime)
            {
                scheduledDates.Add(this.RunAt); 
            }
            else if (this.ScheduleFrequency == enScheduleFrequency.Daily)
            {
                GetListOfDailyScheduledDates(startDate, endDate, scheduledDates, getNextScheduledDateOnly);
            }
            else if (this.ScheduleFrequency == enScheduleFrequency.Weekly)
            {
                GetListOfWeeklyScheduledDates(startDate, endDate, scheduledDates, getNextScheduledDateOnly);
            }
            else if (this.ScheduleFrequency == enScheduleFrequency.Monthly)
            {
                GetListOfMonthlyScheduledDates(startDate, endDate, scheduledDates, getNextScheduledDateOnly);
            }
            else
            {
                //do nothing
                ;
            }

            return scheduledDates;
        }

        private void GetListOfDailyScheduledDates(DateTime startDate, DateTime endDate, List<DateTime> scheduledDates, bool getNextScheduledDateOnly)
        {
            if (this.DailyFrequency == enDailyFrequency.OneTime)
            {
                GetListOfDailyOneTimeScheduledDates(startDate, endDate, scheduledDates, getNextScheduledDateOnly);
            }
            else if (this.DailyFrequency == enDailyFrequency.Recurring)
            {
                GetListOfDailyRecurringScheduledDates(startDate, endDate, scheduledDates, getNextScheduledDateOnly);
            }
            else
            {
                //unknown daily frequency
                ;
            }

        }

        private void GetListOfDailyOneTimeScheduledDates(DateTime startDate, DateTime endDate, List<DateTime> scheduledDates, bool getNextScheduledDateOnly)
        {
            int numDaysOccurs = this.ScheduleOccursEveryNumDays;
            TimeSpan numDaysBetweenScheduledDateTimes = new TimeSpan(numDaysOccurs, 0, 0, 0, 0);
            TimeSpan oneMinute = new TimeSpan(0, 1, 0);
            DateTime nextDateTimeToCheck = DateTime.MaxValue;
            
            DateTime curDateTime = startDate;
            while (curDateTime <= endDate)
            {
                if (this.DateIsScheduled(curDateTime))
                {
                    scheduledDates.Add(curDateTime);
                    if (getNextScheduledDateOnly == true && curDateTime > startDate)
                    {
                        //break the loop: first scheduled date/time after startDate was all that was asked for
                        curDateTime = DateTime.MaxValue;
                    }
                    else
                    {
                        curDateTime = curDateTime.Add(numDaysBetweenScheduledDateTimes);
                        curDateTime = new DateTime(curDateTime.Year, curDateTime.Month, curDateTime.Day, 0, 0, 0);
                    }
                }
                else
                {
                    //date is not scheduled
                    if (this.ScheduleLookupResult == enScheduleLookupResult.MonthNotScheduled)
                    {
                        //goto next month
                        curDateTime = curDateTime.AddMonths(1);
                        curDateTime = new DateTime(curDateTime.Year, curDateTime.Month, 1, 0, 0, 0);
                    }
                    else if (this.ScheduleLookupResult == enScheduleLookupResult.DayNotScheduled
                             || this.ScheduleLookupResult == enScheduleLookupResult.WeekNotScheduled
                             || this.ScheduleLookupResult == enScheduleLookupResult.MonthDayNotScheduled)
                    {
                        //goto next day
                        curDateTime = curDateTime.AddDays(1);
                        curDateTime = new DateTime(curDateTime.Year, curDateTime.Month, curDateTime.Day, 0, 0, 0);
                    }
                    else if (this.ScheduleLookupResult == enScheduleLookupResult.DateOutsideRunAtWindow
                             && new TimeSpan(curDateTime.Hour, curDateTime.Minute, curDateTime.Second) > this.DailyOccursEndTime)
                    {
                        //goto next day
                        curDateTime = curDateTime.AddDays(1);
                        curDateTime = new DateTime(curDateTime.Year, curDateTime.Month, curDateTime.Day, 0, 0, 0);
                    }
                    else if (this.ScheduleLookupResult == enScheduleLookupResult.DateOutsideRunAtWindow
                             && new TimeSpan(curDateTime.Hour, curDateTime.Minute, curDateTime.Second) < this.DailyOccursStartTime)
                    {
                        //goto beginning of the daily run window
                        curDateTime = new DateTime(curDateTime.Year, curDateTime.Month, curDateTime.Day, this.DailyOccursStartTime.Hours, this.DailyOccursStartTime.Minutes, this.DailyOccursStartTime.Seconds);
                    }
                    else
                    {
                        //goto next minute
                        curDateTime = curDateTime.Add(oneMinute);
                    }
                }
            }

        }

        private void GetListOfDailyRecurringScheduledDates(DateTime startDate, DateTime endDate, List<DateTime> scheduledDates, bool getNextScheduledDateOnly)
        {
            int numDaysOccurs = this.ScheduleOccursEveryNumDays;
            TimeSpan numDaysBetweenScheduledDateTimes = new TimeSpan(numDaysOccurs, 0, 0, 0, 0);
            TimeSpan oneHour = new TimeSpan(1, 0, 0);
            TimeSpan oneMinute = new TimeSpan(0, 1, 0);
            TimeSpan oneSecond = new TimeSpan(0, 0, 1);
            TimeSpan occursInterval = new TimeSpan(0, 0, 0);
            TimeSpan defaultInterval = oneMinute;



            occursInterval = this.DailyOccursTimeInterval == enDailyOccursInterval.Hours ? oneHour : this.DailyOccursTimeInterval == enDailyOccursInterval.Minutes ? oneMinute : oneSecond;
            defaultInterval = this.DailyOccursTimeInterval == enDailyOccursInterval.Seconds ? oneSecond : oneMinute;
            DateTime curDateTime = startDate;
            while (curDateTime <= endDate)
            {
                if (this.DateIsScheduled(curDateTime))
                {
                    //scheduledDates.Add(curDateTime);
                    scheduledDates.Add(_currentScheduledStartTime);
                    if (getNextScheduledDateOnly == true && curDateTime > startDate)
                    {
                        //break the loop: first scheduled date/time after startDate was all that was asked for
                        curDateTime = DateTime.MaxValue;
                    }
                    else
                    {
                        curDateTime = curDateTime.Add(occursInterval);
                    }
                }
                else
                {
                    //curDateTime = curDateTime.Add(defaultInterval);
                    //date is not scheduled
                    if (this.ScheduleLookupResult == enScheduleLookupResult.MonthNotScheduled)
                    {
                        //goto next month
                        curDateTime = curDateTime.AddMonths(1);
                        curDateTime = new DateTime(curDateTime.Year, curDateTime.Month, 1, 0, 0, 0);
                    }
                    else if (this.ScheduleLookupResult == enScheduleLookupResult.DayNotScheduled
                             || this.ScheduleLookupResult == enScheduleLookupResult.WeekNotScheduled
                             || this.ScheduleLookupResult == enScheduleLookupResult.MonthDayNotScheduled)
                    {
                        //goto next day
                        curDateTime = curDateTime.AddDays(1);
                        curDateTime = new DateTime(curDateTime.Year, curDateTime.Month, curDateTime.Day, 0, 0, 0);
                    }
                    else if (this.ScheduleLookupResult == enScheduleLookupResult.DateOutsideRunAtWindow
                             && new TimeSpan(curDateTime.Hour, curDateTime.Minute, curDateTime.Second) > this.DailyOccursEndTime)
                    {
                        //goto next day
                        curDateTime = curDateTime.AddDays(1);
                        curDateTime = new DateTime(curDateTime.Year, curDateTime.Month, curDateTime.Day, 0, 0, 0);
                    }
                    else if (this.ScheduleLookupResult == enScheduleLookupResult.DateOutsideRunAtWindow
                             && new TimeSpan(curDateTime.Hour, curDateTime.Minute, curDateTime.Second) < this.DailyOccursStartTime)
                    {
                        //goto beginning of the daily run window
                        curDateTime = new DateTime(curDateTime.Year, curDateTime.Month, curDateTime.Day, this.DailyOccursStartTime.Hours, this.DailyOccursStartTime.Minutes, this.DailyOccursStartTime.Seconds);
                    }
                    else
                    {
                        //goto next default time interval
                        curDateTime = curDateTime.Add(defaultInterval);
                    }
                }
            }

        }

        private void GetListOfWeeklyScheduledDates(DateTime startDate, DateTime endDate, List<DateTime> scheduledDates, bool getNextScheduledDateOnly)
        {
            GetListOfDailyScheduledDates(startDate, endDate, scheduledDates, getNextScheduledDateOnly);
        }

        private void GetListOfMonthlyScheduledDates(DateTime startDate, DateTime endDate, List<DateTime> scheduledDates, bool getNextScheduledDateOnly)
        {
            GetListOfDailyScheduledDates(startDate, endDate, scheduledDates, getNextScheduledDateOnly);
        }


        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFSchedule));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Saves the public property values contained in the current instance to the database specified by the connection string.
        /// </summary>
        /// <param name="connectionString">Contains information needed to open the database.</param>
        /// <remarks>Schedule name must be unique in the database. SQL Server CE 3.5 local file used for database storage.</remarks>
        public void SaveToDatabase(string connectionString)
        {
            string sqlStmt = string.Empty;
            PFDatabase db = new PFDatabase(DatabasePlatform.SQLServerCE35);
            DbDataReader rdr = null;
            int numRecsFound = 0;
            int numRecsAffected = 0;

            db.ConnectionString = connectionString;
            db.OpenConnection();


            //check if already exists
            sqlStmt = _scheduleDefinitionsIfScheduleExistsSQL.Replace("<schedulename>", this.Name);
            rdr = db.RunQueryDataReader(sqlStmt, CommandType.Text);
            numRecsFound = 0;
            while (rdr.Read())
            {
                numRecsFound = rdr.GetInt32(0);
                break;  //should be only one record
            }

            // if exists update it
            if (numRecsFound > 0)
            {
                //update the record
                sqlStmt = _scheduleDefinitionsUpdateSQL.Replace("<schedulename>", this.Name);
                sqlStmt = sqlStmt.Replace("<scheduleobject>", this.ToXmlString());
                numRecsAffected = db.RunNonQuery(sqlStmt, CommandType.Text);
            }
            else
            {
                //insert the new record
                sqlStmt = _scheduleDefinitionsInsertSQL.Replace("<schedulename>", this.Name);
                sqlStmt = sqlStmt.Replace("<scheduleobject>", this.ToXmlString());
                numRecsAffected = db.RunNonQuery(sqlStmt, CommandType.Text);
            }


            db.CloseConnection();


        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFSchedule.</returns>
        public static PFSchedule LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFSchedule));
            TextReader textReader = new StreamReader(filePath);
            PFSchedule objectInstance;
            objectInstance = (PFSchedule)deserializer.Deserialize(textReader);
            textReader.Close();
            return objectInstance;
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a database record.
        /// </summary>
        /// <param name="connectionString">Connection parameters for the database.</param>
        /// <param name="scheduleName">Name of the schedule to retrieve.</param>
        /// <returns>Schedule object.</returns>
        public static PFSchedule LoadFromDatabase(string connectionString, string scheduleName)
        {
            string sqlStmt = string.Empty;
            PFSchedule objectInstance = null;
            PFSchedule tempObjectInstance = new PFSchedule();
            PFDatabase db = new PFDatabase(DatabasePlatform.SQLServerCE35);
            DbDataReader rdr = null;
            string skedDefXml = string.Empty;

            db.ConnectionString = connectionString;
            db.OpenConnection();

            sqlStmt = tempObjectInstance._scheduleDefinitionsSelectScheduleSQL.Replace("<schedulename>", scheduleName);
            rdr = db.RunQueryDataReader(sqlStmt, CommandType.Text);
            while (rdr.Read())
            {
                skedDefXml = rdr.GetString(0);
                objectInstance = PFSchedule.LoadFromXmlString(skedDefXml);
                break;  //should be only one record
            }


            return objectInstance;
        }


        //class helpers

        /// <summary>
        /// Routine overrides default ToString method and outputs name, type, scope and value for all class properties and fields.
        /// </summary>
        /// <returns>String value.</returns>
        public override string ToString()
        {
            StringBuilder data = new StringBuilder();
            data.Append(PropertiesToString());
            data.Append("\r\n");
            data.Append(FieldsToString());
            data.Append("\r\n");


            return data.ToString();
        }


        /// <summary>
        /// Routine outputs name and value for all properties.
        /// </summary>
        /// <returns>String value.</returns>
        public string PropertiesToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            data.Append("Class type:");
            data.Append(t.FullName);
            data.Append("\r\nClass properties for");
            data.Append(t.FullName);
            data.Append("\r\n");


            int inx = 0;
            int maxInx = props.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                PropertyInfo prop = props[inx];
                object val = prop.GetValue(this, null);

                /*
                //****************************************************************************************
                //use the following code if you class has an indexer or is derived from an indexed class
                //****************************************************************************************
                object val = null;
                StringBuilder temp = new StringBuilder();
                if (prop.GetIndexParameters().Length == 0)
                {
                    val = prop.GetValue(this, null);
                }
                else if (prop.GetIndexParameters().Length == 1)
                {
                    temp.Length = 0;
                    for (int i = 0; i < this.Count; i++)
                    {
                        temp.Append("Index ");
                        temp.Append(i.ToString());
                        temp.Append(" = ");
                        temp.Append(val = prop.GetValue(this, new object[] { i }));
                        temp.Append("  ");
                    }
                    val = temp.ToString();
                }
                else
                {
                    //this is an indexed property
                    temp.Length = 0;
                    temp.Append("Num indexes for property: ");
                    temp.Append(prop.GetIndexParameters().Length.ToString());
                    temp.Append("  ");
                    val = temp.ToString();
                }
                //****************************************************************************************
                // end code for indexed property
                //****************************************************************************************
                */

                if (prop.GetGetMethod(true) != null)
                {
                    data.Append(" <");
                    if (prop.GetGetMethod(true).IsPublic)
                        data.Append(" public ");
                    if (prop.GetGetMethod(true).IsPrivate)
                        data.Append(" private ");
                    if (!prop.GetGetMethod(true).IsPublic && !prop.GetGetMethod(true).IsPrivate)
                        data.Append(" internal ");
                    if (prop.GetGetMethod(true).IsStatic)
                        data.Append(" static ");
                    data.Append(" get ");
                    data.Append("> ");
                }
                if (prop.GetSetMethod(true) != null)
                {
                    data.Append(" <");
                    if (prop.GetSetMethod(true).IsPublic)
                        data.Append(" public ");
                    if (prop.GetSetMethod(true).IsPrivate)
                        data.Append(" private ");
                    if (!prop.GetSetMethod(true).IsPublic && !prop.GetSetMethod(true).IsPrivate)
                        data.Append(" internal ");
                    if (prop.GetSetMethod(true).IsStatic)
                        data.Append(" static ");
                    data.Append(" set ");
                    data.Append("> ");
                }
                data.Append(" ");
                data.Append(prop.PropertyType.FullName);
                data.Append(" ");

                data.Append(prop.Name);
                data.Append(": ");
                if (val != null)
                    data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (prop.PropertyType.IsArray)
                {
                    System.Collections.IList valueList = (System.Collections.IList)prop.GetValue(this, null);
                    for (int i = 0; i < valueList.Count; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(valueList[i].ToString());
                        data.Append("  ");
                    }
                }

                data.Append("\r\n");

            }

            return data.ToString();
        }

        /// <summary>
        /// Routine outputs name and value for all fields.
        /// </summary>
        /// <returns>String value.</returns>
        public string FieldsToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            FieldInfo[] finfos = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.SetProperty | BindingFlags.GetProperty | BindingFlags.FlattenHierarchy);
            bool typeHasFieldsToStringMethod = false;

            data.Append("\r\nClass fields for ");
            data.Append(t.FullName);
            data.Append("\r\n");

            int inx = 0;
            int maxInx = finfos.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                FieldInfo fld = finfos[inx];
                object val = fld.GetValue(this);
                if (fld.IsPublic)
                    data.Append(" public ");
                if (fld.IsPrivate)
                    data.Append(" private ");
                if (!fld.IsPublic && !fld.IsPrivate)
                    data.Append(" internal ");
                if (fld.IsStatic)
                    data.Append(" static ");
                data.Append(" ");
                data.Append(fld.FieldType.FullName);
                data.Append(" ");
                data.Append(fld.Name);
                data.Append(": ");
                typeHasFieldsToStringMethod = UseFieldsToString(fld.FieldType);
                if (val != null)
                    if (typeHasFieldsToStringMethod)
                        data.Append(GetFieldValues(val));
                    else
                        data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (fld.FieldType.IsArray)
                //if (fld.Name == "TestStringArray" || "_testStringArray")
                {
                    System.Collections.IList valueList = (System.Collections.IList)fld.GetValue(this);
                    for (int i = 0; i < valueList.Count; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(valueList[i].ToString());
                        data.Append("  ");
                    }
                }

                data.Append("\r\n");

            }

            return data.ToString();
        }

        private bool UseFieldsToString(Type pType)
        {
            bool retval = false;

            //avoid have this type calling its own FieldsToString and going into an infinite loop
            if (pType.FullName != this.GetType().FullName)
            {
                MethodInfo[] methods = pType.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    if (method.Name == "FieldsToString")
                    {
                        retval = true;
                        break;
                    }
                }
            }

            return retval;
        }

        private string GetFieldValues(object typeInstance)
        {
            Type typ = typeInstance.GetType();
            MethodInfo methodInfo = typ.GetMethod("FieldsToString");
            Object retval = methodInfo.Invoke(typeInstance, null);


            return (string)retval;
        }


        /// <summary>
        /// Returns a string containing the contents of the object in XML format.
        /// </summary>
        /// <returns>String value in xml format.</returns>
        /// ///<remarks>XML Serialization is used for the transformation.</remarks>
        public string ToXmlString()
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFSchedule));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of PFSchedule.</returns>
        public static PFSchedule LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFSchedule));
            StringReader strReader = new StringReader(xmlString);
            PFSchedule objectInstance;
            objectInstance = (PFSchedule)deserializer.Deserialize(strReader);
            strReader.Close();
            return objectInstance;
        }


        /// <summary>
        /// Converts instance of this class into an XML document.
        /// </summary>
        /// <returns>XmlDocument</returns>
        /// ///<remarks>XML Serialization and XmlDocument class are used for the transformation.</remarks>
        public XmlDocument ToXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(this.ToXmlString());
            return doc;
        }


    }//end class
}//end namespace
