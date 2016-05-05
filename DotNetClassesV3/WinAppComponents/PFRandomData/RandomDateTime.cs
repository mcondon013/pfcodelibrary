//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2014
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFRandomData
{
    /// <summary>
    /// Generates random date/time values.
    /// </summary>
    public class RandomDateTime
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();

        //private variables for properties
        RandomNumber _rn = new RandomNumber();


        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RandomDateTime()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Generates random date and time based on caller specified range of date/times.
        /// </summary>
        /// <param name="fromDate">Earliest random date to generate.</param>
        /// <param name="toDate">Latest random date to generate.</param>
        /// <param name="fromTime">Earliest random time to generate.</param>
        /// <param name="toTime">Latest random time to generate.</param>
        /// <returns>Random DateTime value.</returns>
        public DateTime GenerateRandomDateTime(DateTime fromDate, DateTime toDate, TimeSpan fromTime, TimeSpan toTime)
        {
            long temp = 0;

            return GenerateRandomDateTime(fromDate, toDate, fromTime, toTime, out temp);
        }

        /// <summary>
        /// Generates random date and time formatted as an integer and based on caller specified range of date/times.
        /// </summary>
        /// <param name="fromDate">Earliest random date to generate.</param>
        /// <param name="toDate">Latest random date to generate.</param>
        /// <param name="fromTime">Earliest random time to generate.</param>
        /// <param name="toTime">Latest random time to generate.</param>
        /// <returns>Random dates and times as a 64-bit integer array.</returns>
        /// <remarks>DateTimes in integer format are frequently used in data warehousing databases. Output value is in yyyyMMddHHmmss format.</remarks>
        public long GenerateRandomDateTimeAsInteger(DateTime fromDate, DateTime toDate, TimeSpan fromTime, TimeSpan toTime)
        {
            long dwRandomDateTime = 0;

            GenerateRandomDateTime(fromDate, toDate, fromTime, toTime, out dwRandomDateTime);

            return dwRandomDateTime;
        }

        private DateTime GenerateRandomDateTime(DateTime fromDate, DateTime toDate, TimeSpan fromTime, TimeSpan toTime, out long dwRandomDateTime)
        {
            DateTime dtm = DateTime.MinValue;

            TimeSpan fromDateTs = Convert.ToDateTime(fromDate).Subtract(DateTime.MinValue);
            TimeSpan toDateTs = Convert.ToDateTime(toDate).Subtract(DateTime.MinValue);
            double fromDays = fromDateTs.TotalDays;
            double toDays = toDateTs.TotalDays;
            TimeSpan fromTimeTs = Convert.ToDateTime(fromTime.ToString()).TimeOfDay;
            TimeSpan toTimeTs = Convert.ToDateTime(toTime.ToString()).TimeOfDay;
            double fromSeconds = fromTimeTs.TotalSeconds;
            double toSeconds = toTimeTs.TotalSeconds;

            Double randNum = _rn.GenerateRandomNumber(fromDays, toDays);
            TimeSpan ts = new TimeSpan((int)randNum, 0, 0, 0, 0);
            dtm = DateTime.MinValue.Add(ts);
            randNum = _rn.GenerateRandomNumber(fromSeconds, toSeconds);
            TimeSpan ts2 = new TimeSpan(0, 0, 0, (int)randNum, 0);
            dtm = dtm.AddSeconds(ts2.TotalSeconds);

            dwRandomDateTime = Convert.ToInt64(dtm.ToString("yyyyMMddHHmmss"));

            return dtm;
        }

        /// <summary>
        /// Generates random date  on caller specified range of dates.
        /// </summary>
        /// <param name="fromDate">Earliest random date to generate.</param>
        /// <param name="toDate">Latest random date to generate.</param>
        /// <returns>DateTime value containing random Date. Time of "00:00:00" is assigned to each date.</returns> 
        public DateTime GenerateRandomDate(DateTime fromDate, DateTime toDate)
        {
            int temp = 0;
            return GenerateRandomDate(fromDate, toDate, out temp);
        }

        /// <summary>
        /// Generates random date formatted as an integer and based on caller specified range of dates.
        /// </summary>
        /// <param name="fromDate">Earliest random date to generate.</param>
        /// <param name="toDate">Latest random date to generate.</param>
        /// <returns>Random date as a 32-bit integer value..</returns>
        /// <remarks>Dates in integer format are frequently used in data warehousing databases. Output value is in yyyyMMdd format.</remarks>
        public int GenerateRandomDateAsInteger(DateTime fromDate, DateTime toDate)
        {
            int dwRandomDateTime = 0;

            GenerateRandomDate(fromDate, toDate, out dwRandomDateTime);

            return dwRandomDateTime;
        }

        private DateTime GenerateRandomDate(DateTime fromDate, DateTime toDate, out int dwRandomDate)
        {
            DateTime dtm = DateTime.MinValue;

            TimeSpan fromDateTs = Convert.ToDateTime(fromDate).Subtract(DateTime.MinValue);
            TimeSpan toDateTs = Convert.ToDateTime(toDate).Subtract(DateTime.MinValue);
            double fromDays = fromDateTs.TotalDays;
            double toDays = toDateTs.TotalDays;

            Double randNum = _rn.GenerateRandomNumber(fromDays, toDays);
            TimeSpan ts = new TimeSpan((int)randNum, 0, 0, 0, 0);
            dtm = DateTime.MinValue.Add(ts);

            dwRandomDate = Convert.ToInt32(dtm.ToString("yyyyMMdd"));

            return dtm;
        }

        /// <summary>
        /// Generates random  time based on caller specified range of times.
        /// </summary>
        /// <param name="fromTime">Earliest random time to generate.</param>
        /// <param name="toTime">Latest random time to generate.</param>
        /// <returns>Random TimeSpan value. Time of day in hours/minutes/seconds format returned.</returns>
        public TimeSpan GenerateRandomTime(TimeSpan fromTime, TimeSpan toTime)
        {
            int temp = 0;
            return GenerateRandomTime(fromTime, toTime, out temp);
        }

        /// <summary>
        /// Generates random time formatted as an integer and based on caller specified range of times.
        /// </summary>
        /// <param name="fromTime">Earliest random time to generate.</param>
        /// <param name="toTime">Latest random time to generate.</param>
        /// <returns>Random time as a 32-bit integer.</returns>
        /// <remarks>Times in integer format can be useful in data warehousing databases. Output value is in HHmmss format.</remarks>
        public int GenerateRandomTimeAsInteger(TimeSpan fromTime, TimeSpan toTime)
        {
            int dwRandomDateTime = 0;

            GenerateRandomTime(fromTime, toTime, out dwRandomDateTime);

            return dwRandomDateTime;
        }



        private TimeSpan GenerateRandomTime(TimeSpan fromTime, TimeSpan toTime, out int dwRandomTime)
        {
            DateTime dt = DateTime.MinValue;
            TimeSpan ts = TimeSpan.MinValue;

            TimeSpan fromTimeTs = Convert.ToDateTime(fromTime.ToString()).TimeOfDay;
            TimeSpan toTimeTs = Convert.ToDateTime(toTime.ToString()).TimeOfDay;
            double fromSeconds = fromTimeTs.TotalSeconds;
            double toSeconds = toTimeTs.TotalSeconds;

            Double randNum = _rn.GenerateRandomNumber(fromSeconds, toSeconds);
            dt = dt.AddSeconds(randNum);
            ts = dt.TimeOfDay;

            string timeStr = Math.Abs(ts.Hours).ToString("00") + Math.Abs(ts.Minutes).ToString("00") + Math.Abs(ts.Seconds).ToString("00");
            dwRandomTime = Convert.ToInt32(timeStr);

            return ts;
        }


        /// <summary>
        /// Generates array of random date and time values based on caller specified range of date/times.
        /// </summary>
        /// <param name="fromDate">Earliest random date to generate.</param>
        /// <param name="toDate">Latest random date to generate.</param>
        /// <param name="fromTime">Earliest random time to generate.</param>
        /// <param name="toTime">Latest random time to generate.</param>
        /// <param name="arraySize">Number of DateTime values to generate.</param>
        /// <returns>Array of random DateTime values.</returns>
        public DateTime[] GenerateRandomDateTime(DateTime fromDate, DateTime toDate, TimeSpan fromTime, TimeSpan toTime, int arraySize)
        {
            long[] temp = {0};

            return GenerateRandomDateTime(fromDate, toDate, fromTime, toTime, arraySize, out temp);
        }

        /// <summary>
        /// Generates random dates and times formatted as an integer array and based on caller specified range of date/times.
        /// </summary>
        /// <param name="fromDate">Earliest random date to generate.</param>
        /// <param name="toDate">Latest random date to generate.</param>
        /// <param name="fromTime">Earliest random time to generate.</param>
        /// <param name="toTime">Latest random time to generate.</param>
        /// <param name="arraySize">Number of DateTime values to generate.</param>
        /// <returns>Random date/times as a 64-bit integer array.</returns>
        /// <remarks>Dates in integer format are frequently used in data warehousing databases. Output values are in yyyyMMddHHmmss format.</remarks>
        public long[] GenerateRandomDateTimeAsInteger(DateTime fromDate, DateTime toDate, TimeSpan fromTime, TimeSpan toTime, int arraySize)
        {
            long[] dwRandomDateTime = default(long[]);

            GenerateRandomDateTime(fromDate, toDate, fromTime, toTime, arraySize, out dwRandomDateTime);

            return dwRandomDateTime;
        }

        private DateTime[] GenerateRandomDateTime(DateTime fromDate, DateTime toDate, TimeSpan fromTime, TimeSpan toTime, int arraySize, out long[] dwRandomDateTime)
        {
            DateTime[] dtm = new DateTime[arraySize];
            dwRandomDateTime = new long[arraySize];
            TimeSpan fromDateTs = Convert.ToDateTime(fromDate).Subtract(DateTime.MinValue);
            TimeSpan toDateTs = Convert.ToDateTime(toDate).Subtract(DateTime.MinValue);
            double fromDays = fromDateTs.TotalDays;
            double toDays = toDateTs.TotalDays;
            TimeSpan fromTimeTs = Convert.ToDateTime(fromTime.ToString()).TimeOfDay;
            TimeSpan toTimeTs = Convert.ToDateTime(toTime.ToString()).TimeOfDay;
            double fromSeconds = fromTimeTs.TotalSeconds;
            double toSeconds = toTimeTs.TotalSeconds;
            Double randNum = 0;
            DateTime dt = DateTime.MinValue;
            TimeSpan ts = TimeSpan.MinValue;
            TimeSpan ts2 = TimeSpan.MinValue;


            for (int i = 0; i < arraySize; i++)
            {
                randNum = _rn.GenerateRandomNumber(fromDays, toDays);
                ts = new TimeSpan((int)randNum, 0, 0, 0, 0);
                dt = DateTime.MinValue.Add(ts);
                randNum = _rn.GenerateRandomNumber(fromSeconds, toSeconds);
                ts2 = new TimeSpan(0, 0, 0, (int)randNum, 0);
                dtm[i] = dt.AddSeconds(ts2.TotalSeconds);

                dwRandomDateTime[i] = Convert.ToInt64(dtm[i].ToString("yyyyMMddHHmmss"));
            }



            return dtm;
        }


        /// <summary>
        /// Generates random date array on caller specified range of dates.
        /// </summary>
        /// <param name="fromDate">Earliest random date to generate.</param>
        /// <param name="toDate">Latest random date to generate.</param>
        /// <param name="arraySize">Number of Date values to generate.</param>
        /// <returns>Array of DateTime values containing random Date. Time of "00:00:00" is assigned to each date.</returns> 
        public DateTime[] GenerateRandomDate(DateTime fromDate, DateTime toDate, int arraySize)
        {
            int[] temp = {0};
            return GenerateRandomDate(fromDate, toDate, arraySize, out temp);
        }

        /// <summary>
        /// Generates random dates formatted as an integer array and based on caller specified range of dates.
        /// </summary>
        /// <param name="fromDate">Earliest random date to generate.</param>
        /// <param name="toDate">Latest random date to generate.</param>
        /// <param name="arraySize">Number of Date values to generate.</param>
        /// <returns>Random dates as array of 32-bit integer values..</returns>
        /// <remarks>Dates in integer format are frequently used in data warehousing databases. Output values are in yyyyMMdd format.</remarks>
        public int[] GenerateRandomDateAsInteger(DateTime fromDate, DateTime toDate, int arraySize)
        {
            int[] dwRandomDate = {0};

            GenerateRandomDate(fromDate, toDate, arraySize, out dwRandomDate);

            return dwRandomDate;
        }

        private DateTime[] GenerateRandomDate(DateTime fromDate, DateTime toDate, int arraySize, out int[] dwRandomDate)
        {
            DateTime[] dtm= new DateTime[arraySize];
            dwRandomDate = new int[arraySize];
            TimeSpan fromDateTs = Convert.ToDateTime(fromDate).Subtract(DateTime.MinValue);
            TimeSpan toDateTs = Convert.ToDateTime(toDate).Subtract(DateTime.MinValue);
            double fromDays = fromDateTs.TotalDays;
            double toDays = toDateTs.TotalDays;
            Double randNum = 0.0;
            TimeSpan ts = TimeSpan.MinValue;


            for (int i = 0; i < arraySize; i++)
            {
                randNum = _rn.GenerateRandomNumber(fromDays, toDays);
                ts = new TimeSpan((int)randNum, 0, 0, 0, 0);
                dtm[i] = DateTime.MinValue.Add(ts);

                dwRandomDate[i] = Convert.ToInt32(dtm[i].ToString("yyyyMMdd"));
            }

            return dtm;
        }

        /// <summary>
        /// Generates array of random  times based on caller specified range of times.
        /// </summary>
        /// <param name="fromTime">Earliest random time to generate.</param>
        /// <param name="toTime">Latest random time to generate.</param>
        /// <param name="arraySize">Number of Time values to generate.</param>
        /// <returns>Array of random TimeSpan values. Time of day in hours/minutes/seconds format returned.</returns>
        public TimeSpan[] GenerateRandomTime(TimeSpan fromTime, TimeSpan toTime, int arraySize)
        {
            int[] temp = {0};
            return GenerateRandomTime(fromTime, toTime, arraySize, out temp);
        }

        /// <summary>
        /// Generates random times formatted as an integer array and based on caller specified range of times.
        /// </summary>
        /// <param name="fromTime">Earliest random time to generate.</param>
        /// <param name="toTime">Latest random time to generate.</param>
        /// <param name="arraySize">Number of Time values to generate.</param>
        /// <returns>Random times as a 32-bit integer array.</returns>
        /// <remarks>Times in integer format can be useful in data warehousing databases. Output values are in HHmmss format.</remarks>
        public int[] GenerateRandomTimeAsInteger(TimeSpan fromTime, TimeSpan toTime, int arraySize)
        {
            int[] dwRandomTime = { 0 };

            GenerateRandomTime(fromTime, toTime, arraySize, out dwRandomTime);

            return dwRandomTime;
        }

        private TimeSpan[] GenerateRandomTime(TimeSpan fromTime, TimeSpan toTime, int arraySize, out int[] dwRandomTime)
        {
            DateTime[] dt = new DateTime[arraySize];
            TimeSpan[] ts = new TimeSpan[arraySize];
            dwRandomTime = new int[arraySize];

            TimeSpan fromTimeTs = Convert.ToDateTime(fromTime.ToString()).TimeOfDay;
            TimeSpan toTimeTs = Convert.ToDateTime(toTime.ToString()).TimeOfDay;
            double fromSeconds = fromTimeTs.TotalSeconds;
            double toSeconds = toTimeTs.TotalSeconds;
            Double randNum = 0.0;

            for (int i = 0; i < arraySize; i++)
            {
                dt[i] = DateTime.MinValue;
                ts[i] = TimeSpan.MinValue;
                randNum = _rn.GenerateRandomNumber(fromSeconds, toSeconds);
                dt[i] = dt[i].AddSeconds(randNum);
                ts[i] = dt[i].TimeOfDay;

                string timeStr = Math.Abs(ts[i].Hours).ToString("00") + Math.Abs(ts[i].Minutes).ToString("00") + Math.Abs(ts[i].Seconds).ToString("00");
                dwRandomTime[i] = Convert.ToInt32(timeStr);
            }

            return ts;
        }

        /// <summary>
        /// Generates a DateTime value that is offset from the caller supplied DateTime by a random interval.
        /// </summary>
        /// <param name="dateTimeToModify">DateTime value to modify.</param>
        /// <param name="offsetInterval">Specifies offset time interval (e.g. years, months, days, hours, minutes or seconds).</param>
        /// <param name="minOffset">Smallest offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <param name="maxOffset">Maximum offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <returns>DateTime value that has been offset from the dateTimeToModify by a random duration.</returns>
        public DateTime OffsetDateTime(DateTime dateTimeToModify, enRandomOffsetType offsetInterval, int minOffset, int maxOffset)
        {
            long temp = 0;
            return OffsetDateTime(dateTimeToModify, offsetInterval, minOffset, maxOffset, out temp, enDwDateTimeFormat.enDateAndTime);
        }

        /// <summary>
        /// Generates a DateTime value as an integer that is offset from the caller supplied DateTime by a random interval.
        /// </summary>
        /// <param name="dateTimeToModify">DateTime value to modify.</param>
        /// <param name="offsetInterval">Specifies offset time interval (e.g. years, months, days, hours, minutes or seconds).</param>
        /// <param name="minOffset">Smallest offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <param name="maxOffset">Maximum offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <returns>DateTime value converted to an integer that has been offset from the dateTimeToModify by a random duration. Output value is in yyyyMMddHHmmss format.</returns>
        public long OffsetDateTimeAsInteger(DateTime dateTimeToModify, enRandomOffsetType offsetInterval, int minOffset, int maxOffset)
        {
            long dwDateTime = 0;

            OffsetDateTime(dateTimeToModify, offsetInterval, minOffset, maxOffset, out dwDateTime, enDwDateTimeFormat.enDateAndTime);

            return dwDateTime;
        }

        /// <summary>
        /// Generates a date value that is offset from the caller supplied DateTime by a random interval.
        /// </summary>
        /// <param name="dateTimeToModify">DateTime value to modify.</param>
        /// <param name="offsetInterval">Specifies offset time interval (e.g. years, months, days, hours, minutes or seconds).</param>
        /// <param name="minOffset">Smallest offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <param name="maxOffset">Maximum offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <returns>DateTime value that has been offset from the dateTimeToModify by a random duration. Time is not set in the return value. </returns>
        public DateTime OffsetDate(DateTime dateTimeToModify, enRandomOffsetType offsetInterval, int minOffset, int maxOffset)
        {
            long temp = 0;
            DateTime dateToModify = dateTimeToModify.Date + new TimeSpan(0, 0, 0);
            return OffsetDateTime(dateToModify, offsetInterval, minOffset, maxOffset, out temp, enDwDateTimeFormat.enDateOnly);
        }

        /// <summary>
        /// Generates a date value as an integer that is offset from the caller supplied DateTime by a random interval.
        /// </summary>
        /// <param name="dateTimeToModify">DateTime value to modify.</param>
        /// <param name="offsetInterval">Specifies offset time interval (e.g. years, months, days, hours, minutes or seconds).</param>
        /// <param name="minOffset">Smallest offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <param name="maxOffset">Maximum offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <returns>DateTime value converted to an integer that has been offset from the dateTimeToModify by a random duration. Time is not set in the return value. Output value is in yyyyMMdd format.</returns>
        public int OffsetDateAsInteger(DateTime dateTimeToModify, enRandomOffsetType offsetInterval, int minOffset, int maxOffset)
        {
            long dwDate = 0;

            DateTime dateToModify = dateTimeToModify.Date + new TimeSpan(0, 0, 0);

            OffsetDateTime(dateToModify, offsetInterval, minOffset, maxOffset, out dwDate, enDwDateTimeFormat.enDateOnly);

            return Convert.ToInt32(dwDate);
        }

        /// <summary>
        /// Generates a time value that is offset from the time portion of the caller supplied DateTime by a random interval.
        /// </summary>
        /// <param name="dateTimeToModify">DateTime value to modify.</param>
        /// <param name="offsetInterval">Specifies offset time interval (e.g. years, months, days, hours, minutes or seconds).</param>
        /// <param name="minOffset">Smallest offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <param name="maxOffset">Maximum offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <returns>Time value that has been offset from the time portion of dateTimeToModify by a random duration. Date is not set in the return value. </returns>
        public DateTime OffsetTime(DateTime dateTimeToModify, enRandomOffsetType offsetInterval, int minOffset, int maxOffset)
        {
            long temp = 0;
            DateTime timeToModify = DateTime.Now.Date + dateTimeToModify.TimeOfDay;
            return OffsetDateTime(timeToModify, offsetInterval, minOffset, maxOffset, out temp, enDwDateTimeFormat.enTimeOnly);
        }

        /// <summary>
        /// Generates a time value as an integer that is offset from the time portion of the caller supplied DateTime by a random interval.
        /// </summary>
        /// <param name="dateTimeToModify">DateTime value to modify.</param>
        /// <param name="offsetInterval">Specifies offset time interval (e.g. years, months, days, hours, minutes or seconds).</param>
        /// <param name="minOffset">Smallest offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <param name="maxOffset">Maximum offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <returns>Time value converted to an integer that has been offset from the time portion of dateTimeToModify by a random duration. Date is not set in the return value. Output value is in HHmmss format.</returns>
        public int OffsetTimeAsInteger(DateTime dateTimeToModify, enRandomOffsetType offsetInterval, int minOffset, int maxOffset)
        {
            long dwTime = 0;

            DateTime timeToModify = DateTime.Now.Date + dateTimeToModify.TimeOfDay;

            OffsetDateTime(timeToModify, offsetInterval, minOffset, maxOffset, out dwTime, enDwDateTimeFormat.enTimeOnly);

            return Convert.ToInt32(dwTime);
        }

        private DateTime OffsetDateTime(DateTime dateTimeToModify, enRandomOffsetType offsetInterval, int minOffset, int maxOffset, out long dwDateTime, enDwDateTimeFormat dwDtFormat)
        {
            DateTime dtm = DateTime.MinValue;
            DateTime retDtm = DateTime.MinValue;
            DateTime minDt = DateTime.MinValue;
            DateTime maxDt = DateTime.MaxValue;
            double minDays = 0.0;
            double maxDays = 0.0;
            double minSeconds = 0.9;
            double maxSeconds = 0.0;
            double randNum = 0.0;


            //randNum = _rn.GenerateRandomInt(minOffset, maxOffset);

            switch (offsetInterval)
            {
                case enRandomOffsetType.enYears:
                    minDt = dateTimeToModify.AddYears(minOffset);
                    maxDt = dateTimeToModify.AddYears(maxOffset);
                    break;
                case enRandomOffsetType.enMonths:
                    minDt = dateTimeToModify.AddMonths(minOffset);
                    maxDt = dateTimeToModify.AddMonths(maxOffset);
                    break;
                case enRandomOffsetType.enDays:
                    minDt = dateTimeToModify.AddDays(minOffset);
                    maxDt = dateTimeToModify.AddDays(maxOffset);
                    break;
                case enRandomOffsetType.enHours:
                    minDt = dateTimeToModify.AddHours(minOffset);
                    maxDt = dateTimeToModify.AddHours(maxOffset);
                    break;
                case enRandomOffsetType.enMinutes:
                    minDt = dateTimeToModify.AddMinutes(minOffset);
                    maxDt = dateTimeToModify.AddMinutes(maxOffset);
                    break;
                case enRandomOffsetType.enSeconds:
                    minDt = dateTimeToModify.AddSeconds(minOffset);
                    maxDt = dateTimeToModify.AddSeconds(maxOffset);
                    break;
                default:
                    minDt = dateTimeToModify;
                    maxDt = dateTimeToModify;
                    break;
            }

            minDays = minDt.Subtract(DateTime.MinValue).TotalDays;
            maxDays = maxDt.Subtract(DateTime.MinValue).TotalDays;
            minSeconds = minDt.Subtract(DateTime.MinValue).TotalSeconds;
            maxSeconds = maxDt.Subtract(DateTime.MinValue).TotalSeconds;



            switch (offsetInterval)
            {
                case enRandomOffsetType.enYears:
                case enRandomOffsetType.enMonths:
                case enRandomOffsetType.enDays:
                    randNum = _rn.GenerateRandomNumber(minDays, maxDays);
                    dtm = DateTime.MinValue.AddDays(randNum);
                    break;
                case enRandomOffsetType.enHours:
                case enRandomOffsetType.enMinutes:
                case enRandomOffsetType.enSeconds:
                    randNum = _rn.GenerateRandomNumber(minSeconds, maxSeconds);
                    dtm = DateTime.MinValue.AddSeconds(randNum);
                    break;
                default:
                    dtm = dateTimeToModify;
                    break;
            }

            switch (dwDtFormat)
            {
                case enDwDateTimeFormat.enDateAndTime:
                    dwDateTime = Convert.ToInt64(dtm.ToString("yyyyMMddHHmmss"));
                    retDtm = dtm;
                    break;
                case enDwDateTimeFormat.enDateOnly:
                    dwDateTime = Convert.ToInt64(dtm.ToString("yyyyMMdd"));
                    retDtm = dtm.Date + new TimeSpan(0,0,0); 
                    break;
                case enDwDateTimeFormat.enTimeOnly:
                    dwDateTime = Convert.ToInt64(dtm.ToString("HHmmss"));
                    retDtm = DateTime.Now.Date + dtm.TimeOfDay;
                    break;
                default:
                    dwDateTime = Convert.ToInt64(dtm.ToString("yyyyMMddHHmmss"));
                    retDtm = dtm;
                    break;
            }

            return retDtm;
        }

        /// <summary>
        /// Generates an array DateTime values that are offset from the caller supplied DateTime by random intervals.
        /// </summary>
        /// <param name="dateTimesToModify">Array of DateTime values to modify.</param>
        /// <param name="offsetInterval">Specifies offset time interval (e.g. years, months, days, hours, minutes or seconds).</param>
        /// <param name="minOffset">Smallest offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <param name="maxOffset">Maximum offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <returns>Array of DateTime values that have been offset from the dateTimesToModify by random durations.</returns>
        public DateTime[] OffsetDateTime(DateTime[] dateTimesToModify, enRandomOffsetType offsetInterval, int minOffset, int maxOffset)
        {
            long[] temp = default(long[]);

            return OffsetDateTime(dateTimesToModify, offsetInterval, minOffset, maxOffset, out temp, enDwDateTimeFormat.enDateAndTime);
        }

        /// <summary>
        /// Generates DateTime values as an array of integers that are offset from the caller supplied DateTime by random intervals.
        /// </summary>
        /// <param name="dateTimesToModify">Array of DateTime values to modify.</param>
        /// <param name="offsetInterval">Specifies offset time interval (e.g. years, months, days, hours, minutes or seconds).</param>
        /// <param name="minOffset">Smallest offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <param name="maxOffset">Maximum offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <returns>DateTime values converted to a 64-bit integer array that have been offset from the dateTimesToModify by random durationsNC. Output values are in yyyyMMddHHmmss format.</returns>
        public long[] OffsetDateTimeAsInteger(DateTime[] dateTimesToModify, enRandomOffsetType offsetInterval, int minOffset, int maxOffset)
        {
            long[] dwDateTime = default(long[]);

            OffsetDateTime(dateTimesToModify, offsetInterval, minOffset, maxOffset, out dwDateTime, enDwDateTimeFormat.enDateAndTime);

            return dwDateTime;
        }

        /// <summary>
        /// Generates an array of date values that are offset from the caller supplied DateTime by random intervals.
        /// </summary>
        /// <param name="dateTimesToModify">Array of date values to modify.</param>
        /// <param name="offsetInterval">Specifies offset time interval (e.g. years, months, days, hours, minutes or seconds).</param>
        /// <param name="minOffset">Smallest offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <param name="maxOffset">Maximum offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <returns>Array of DateTime values that have been offset from the dateTimesToModify by random durations. Time is not set in the return values. </returns>
        public DateTime[] OffsetDate(DateTime[] dateTimesToModify, enRandomOffsetType offsetInterval, int minOffset, int maxOffset)
        {
            long[] temp = default(long[]);

            DateTime[] datesToModify = new DateTime[dateTimesToModify.Length];

            for (int i = 0; i < dateTimesToModify.Length; i++)
            {
                datesToModify[i] = dateTimesToModify[i].Date + new TimeSpan(0,0,0);
            }

            return OffsetDateTime(datesToModify, offsetInterval, minOffset, maxOffset, out temp, enDwDateTimeFormat.enDateOnly);
        }

        /// <summary>
        /// Generates an array of date values as a integers that are offset from the caller supplied DateTime by random intervals.
        /// </summary>
        /// <param name="dateTimesToModify">Array of date values to modify.</param>
        /// <param name="offsetInterval">Specifies offset time interval (e.g. years, months, days, hours, minutes or seconds).</param>
        /// <param name="minOffset">Smallest offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <param name="maxOffset">Maximum offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <returns>Array of date values converted to a 32-bit integer array. Values have been offset from the dateTimesToModify by random durations. Time is not set in the return values. Output values are in yyyyMMdd format.</returns>
        public int[] OffsetDateAsInteger(DateTime[] dateTimesToModify, enRandomOffsetType offsetInterval, int minOffset, int maxOffset)
        {
            long[] temp = new long[dateTimesToModify.Length];
            int[] dwDate = new int[dateTimesToModify.Length];
            DateTime[] datesToModify = new DateTime[dateTimesToModify.Length];

            for (int i = 0; i < dateTimesToModify.Length; i++)
            {
                datesToModify[i] = dateTimesToModify[i].Date + new TimeSpan(0, 0, 0);
            }
            OffsetDateTime(datesToModify, offsetInterval, minOffset, maxOffset, out temp, enDwDateTimeFormat.enDateOnly);

            for (int i = 0; i < dateTimesToModify.Length; i++)
            {
                dwDate[i] = Convert.ToInt32(temp[i]);
            }


            return dwDate;
        }

        /// <summary>
        /// Generates an array of time values that are offset from the time portion of the caller supplied DateTime array by random intervals.
        /// </summary>
        /// <param name="dateTimesToModify">Array of time values to modify.</param>
        /// <param name="offsetInterval">Specifies offset time interval (e.g. years, months, days, hours, minutes or seconds).</param>
        /// <param name="minOffset">Smallest offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <param name="maxOffset">Maximum offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <returns>Array of time values that have been offset from the time portion of dateTimesToModify by random durations. Date is not set in the return values. </returns>
        public DateTime[] OffsetTime(DateTime[] dateTimesToModify, enRandomOffsetType offsetInterval, int minOffset, int maxOffset)
        {
            long[] temp = default(long[]);

            DateTime[] timesToModify = new DateTime[dateTimesToModify.Length];

            for (int i = 0; i < dateTimesToModify.Length; i++)
            {
                timesToModify[i] = DateTime.Now.Date + dateTimesToModify[i].TimeOfDay;
            }

            return OffsetDateTime(timesToModify, offsetInterval, minOffset, maxOffset, out temp, enDwDateTimeFormat.enTimeOnly);
        }

        /// <summary>
        /// Generates an array of time values as integers that are offset from the time portion of the caller supplied dateTimesToModify by random intervals.
        /// </summary>
        /// <param name="dateTimesToModify">Array of time values to modify.</param>
        /// <param name="offsetInterval">Specifies offset time interval (e.g. years, months, days, hours, minutes or seconds).</param>
        /// <param name="minOffset">Smallest offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <param name="maxOffset">Maximum offset. Specify negative numbers to specify offsets that generate earlier dates.</param>
        /// <returns>Array of time values converted to an array of 32-bit integers that have been offset from the time portion of dateTimesToModify by random durations. Date is not set in the return value. Return values are in HHmmss format.</returns>
        public int[] OffsetTimeAsInteger(DateTime[] dateTimesToModify, enRandomOffsetType offsetInterval, int minOffset, int maxOffset)
        {
            long[] temp = new long[dateTimesToModify.Length];
            int[] dwTime = new int[dateTimesToModify.Length];
            DateTime[] timesToModify = new DateTime[dateTimesToModify.Length];

            for (int i = 0; i < dateTimesToModify.Length; i++)
            {
                timesToModify[i] = DateTime.Now.Date + dateTimesToModify[i].TimeOfDay;
            }
            OffsetDateTime(timesToModify, offsetInterval, minOffset, maxOffset, out temp, enDwDateTimeFormat.enTimeOnly);

            for (int i = 0; i < dateTimesToModify.Length; i++)
            {
                dwTime[i] = Convert.ToInt32(temp[i]);
            }


            return dwTime;
        }

        private DateTime[] OffsetDateTime(DateTime[] dateTimesToModify, enRandomOffsetType offsetInterval, int minOffset, int maxOffset, out long[] dwDateTime, enDwDateTimeFormat dwDtFormat)
        {
            DateTime[] dtm = new DateTime[dateTimesToModify.Length];
            DateTime[] retDtm = new DateTime[dateTimesToModify.Length];
            dwDateTime = new long[dateTimesToModify.Length];
            DateTime minDt = DateTime.MinValue;
            DateTime maxDt = DateTime.MaxValue;
            double minDays = 0.0;
            double maxDays = 0.0;
            double minSeconds = 0.9;
            double maxSeconds = 0.0;
            double randNum = 0.0;


            for (int i = 0; i < dateTimesToModify.Length; i++)
            {
                switch (offsetInterval)
                {
                    case enRandomOffsetType.enYears:
                        minDt = dateTimesToModify[i].AddYears(minOffset);
                        maxDt = dateTimesToModify[i].AddYears(maxOffset);
                        break;
                    case enRandomOffsetType.enMonths:
                        minDt = dateTimesToModify[i].AddMonths(minOffset);
                        maxDt = dateTimesToModify[i].AddMonths(maxOffset);
                        break;
                    case enRandomOffsetType.enDays:
                        minDt = dateTimesToModify[i].AddDays(minOffset);
                        maxDt = dateTimesToModify[i].AddDays(maxOffset);
                        break;
                    case enRandomOffsetType.enHours:
                        minDt = dateTimesToModify[i].AddHours(minOffset);
                        maxDt = dateTimesToModify[i].AddHours(maxOffset);
                        break;
                    case enRandomOffsetType.enMinutes:
                        minDt = dateTimesToModify[i].AddMinutes(minOffset);
                        maxDt = dateTimesToModify[i].AddMinutes(maxOffset);
                        break;
                    case enRandomOffsetType.enSeconds:
                        minDt = dateTimesToModify[i].AddSeconds(minOffset);
                        maxDt = dateTimesToModify[i].AddSeconds(maxOffset);
                        break;
                    default:
                        minDt = dateTimesToModify[i];
                        maxDt = dateTimesToModify[i];
                        break;
                }
            
                minDays = minDt.Subtract(DateTime.MinValue).TotalDays;
                maxDays = maxDt.Subtract(DateTime.MinValue).TotalDays;
                minSeconds = minDt.Subtract(DateTime.MinValue).TotalSeconds;
                maxSeconds = maxDt.Subtract(DateTime.MinValue).TotalSeconds;

                switch (offsetInterval)
                {
                    case enRandomOffsetType.enYears:
                    case enRandomOffsetType.enMonths:
                    case enRandomOffsetType.enDays:
                        randNum = _rn.GenerateRandomNumber(minDays, maxDays);
                        dtm[i] = DateTime.MinValue.AddDays(randNum);
                        break;
                    case enRandomOffsetType.enHours:
                    case enRandomOffsetType.enMinutes:
                    case enRandomOffsetType.enSeconds:
                        randNum = _rn.GenerateRandomNumber(minSeconds, maxSeconds);
                        dtm[i] = DateTime.MinValue.AddSeconds(randNum);
                        break;
                    default:
                        dtm = dateTimesToModify;
                        break;
                }

                switch (dwDtFormat)
                {
                    case enDwDateTimeFormat.enDateAndTime:
                        dwDateTime[i] = Convert.ToInt64(dtm[i].ToString("yyyyMMddHHmmss"));
                        retDtm[i] = dtm[i];
                        break;
                    case enDwDateTimeFormat.enDateOnly:
                        dwDateTime[i] = Convert.ToInt64(dtm[i].ToString("yyyyMMdd"));
                        retDtm[i] = dtm[i].Date + new TimeSpan(0,0,0);
                        break;
                    case enDwDateTimeFormat.enTimeOnly:
                        dwDateTime[i] = Convert.ToInt64(dtm[i].ToString("HHmmss"));
                        retDtm[i] = DateTime.Now.Date + dtm[i].TimeOfDay;
                        break;
                    default:
                        dwDateTime[i] = Convert.ToInt64(dtm[i].ToString("yyyyMMddHHmmss"));
                        retDtm[i] = dtm[i];
                        break;
                }


            }

            
            return retDtm;
        }

    }//end class
}//end namespace
