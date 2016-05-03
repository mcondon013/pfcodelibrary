//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************

namespace PFSchedulerObjects
{
#pragma warning disable 1591
    public enum enScheduleLookupResult
    {
        Unknown = 0,
        DateIsScheduled = 1,
        DateOutsideRunAtWindow = 2,

        DateOutsideScheduleStartAndEnd = 3,
        DayNotScheduled = 4,
        TimeOutsideDailyRunAtWindow = 5,
        TimeIntervalNotScheduled = 6,
        TimeNotScheduled = 7,
        WeekNotScheduled = 8,
        MonthNotScheduled = 9,
        MonthDayNotScheduled = 10,


    }
    public enum enScheduleFrequency
    {
        Unknown = 0,
        OneTime = 1,
        Daily = 2,
        Weekly = 3,
        Monthly = 4
    }

    public enum enDailyFrequency
    {
        Unknown = 0,
        OneTime = 1,
        Recurring = 2
    }

    public enum enDailyOccursInterval
    {
        Unknown = 0,
        Hours = 1,
        Minutes = 2,
        Seconds = 3
    }

    public enum enMonthlyScheduleMonthIdType
    {
        Unknown = 0,
        EveryNumMonths = 1,
        OccursDuringMonthName = 2
    }
    
    public enum enMonthlyScheduleDayIdType
    {
        Unknown = 0,
        DayNumber = 1,
        DayName = 2
    }

    public enum enMontlyScheduleOrdinal
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
        Last = 99,
        Unknown = 100
   }

    public enum enScheduleDay
    {
        Monday = 0,
        Tuesday = 1,
        Wednesday = 2,
        Thursday = 3,
        Friday = 4,
        Saturday = 5,
        Sunday = 6,
        Unknown = 99
    }

    public enum enScheduleMonth
    {
        Unknown = 0,
        January = 1,
        February = 2,
        March = 3,
        April =4, 
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12,
    }

    public enum enScheduleStorageType
    {
        Unknown = 0,
        XMLFiles = 1,
        Database = 2,
    }


    /// <summary>
    /// Contains weekly schedule data.
    /// </summary>
    public class CWeeklySchedule
    {
        public bool[] Day = new bool[7] {false, false, false, false, false, false, false};
    }

    /// <summary>
    /// Contains monthly schedule data.
    /// </summary>
    public class CMonthlySchedule
    {
        public bool[] Month = new bool[13] { false, false, false, false, false, false, false, false, false, false, false, false, false };
    }

#pragma warning restore 1591

}//end namespace