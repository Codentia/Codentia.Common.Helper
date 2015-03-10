using System;
using System.Collections.Generic;

namespace Codentia.Common.Helper
{   
    /// <summary>
    /// This static class encapsulates utility methods for dealing with Dates/Times/Etc.
    /// </summary>
    public static class DateHelper
    {
        private const byte Sunday = 1;
        private const byte Monday = 2;
        private const byte Tuesday = 4;
        private const byte Wednesday = 8;
        private const byte Thursday = 16;
        private const byte Friday = 32;
        private const byte Saturday = 64;

        /// <summary>
        /// Get the ordinal associated with a given date (day), e.g. th, st, rd, nd.
        /// </summary>
        /// <param name="input">DateTime to be processed</param>
        /// <returns>string ordinal</returns>
        public static string GetOrdinal(DateTime input)
        {
            string ordinal = "th";

            switch (input.Day)
            {
                case 1:
                case 21:
                case 31:
                    ordinal = "st";
                    break;
                case 2:
                case 22:
                    ordinal = "nd";
                    break;
                case 3:
                case 23:
                    ordinal = "rd";
                    break;
            }

            return ordinal;
        }

        /// <summary>
        /// Get a truncated (unpadded) day of month with ordinal e.g. 20th, 3rd, etc
        /// </summary>
        /// <param name="input">DateTime to be processed</param>
        /// <returns>string day of month with ordinal</returns>
        public static string GetDayOfMonthWithOrdinal(DateTime input)
        {
            string value = input.ToString("dd");
            if (value.Substring(0, 1) == "0")
            {
                value = value.Substring(1, 1);
            }

            return string.Format("{0}{1}", value, DateHelper.GetOrdinal(input));
        }

        /// <summary>
        /// Get Array of Days of the Week (Sunday 0 index)
        /// </summary>
        /// <returns>DayOfWeek array</returns>
        public static DayOfWeek[] DayOfWeekArray()
        {
            DayOfWeek[] dowArray = new DayOfWeek[7];

            dowArray[0] = DayOfWeek.Sunday;
            dowArray[1] = DayOfWeek.Monday;
            dowArray[2] = DayOfWeek.Tuesday;
            dowArray[3] = DayOfWeek.Wednesday;
            dowArray[4] = DayOfWeek.Thursday;
            dowArray[5] = DayOfWeek.Friday;
            dowArray[6] = DayOfWeek.Saturday;

            return dowArray;
        }

        /// <summary>
        /// Get Day of Week as an array of enums (DayOfWeek) starting at startDay
        /// </summary>
        /// <param name="startDay">start Day</param>
        /// <returns>DayOfWeek array</returns>
        public static DayOfWeek[] DayOfWeekArray(DayOfWeek startDay)
        {
            DayOfWeek[] dowArray = new DayOfWeek[7];
            
            for (int i = 0; i < 7; i++)
            {
                if (Convert.ToInt32(startDay) + i > 6)
                {
                    dowArray[i] = (DayOfWeek)(startDay + i - 7);
                }
                else
                {
                    dowArray[i] = (DayOfWeek)(startDay + i);
                }
            }

            return dowArray;
        }

        /// <summary>
        /// Gets the time span as string.
        /// </summary>
        /// <param name="timespan">The timespan.</param>
        /// <returns>string of timespan in format days:hours:mins:secs</returns>
        public static string GetTimeSpanAsString(TimeSpan timespan)
        {
            return string.Format("{0:00}:{1:00}:{2:00}:{3:00}", timespan.Days, timespan.Hours, timespan.Minutes, timespan.Seconds);
        }
        
        /// <summary>
        /// Gets string as time span (object)
        /// </summary>
        /// <param name="timespan">The timespan string in format days:hours:mins:secs sent through as an object</param>
        /// <returns>Timespan of string</returns>
        public static TimeSpan GetStringAsTimeSpan(object timespan)
        {
            return GetStringAsTimeSpan(Convert.ToString(timespan));
        }

        /// <summary>
        /// Gets string as time span
        /// </summary>
        /// <param name="timespan">The timespan string in format days:hours:mins:secs</param>
        /// <returns>Timespan of string</returns>
        public static TimeSpan GetStringAsTimeSpan(string timespan)
        {
            TimeSpan ts;

            try
            {
                int days = Convert.ToInt32(timespan.Substring(0, 2));
                int hours = Convert.ToInt32(timespan.Substring(3, 2));
                int mins = Convert.ToInt32(timespan.Substring(6, 2));
                int secs = Convert.ToInt32(timespan.Substring(9, 2));

                ts = new TimeSpan(days, hours, mins, secs);
            }
            catch
            {
                throw new InvalidCastException(string.Format("Unable to cast: {0} as timespan - format is days:hours:mins:secs", timespan));
            }

            return ts;
        }

        /// <summary>
        /// Writes the nullable date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>object - if date is DateTime.MinValue returns DBNull.Value otherwise date is returned</returns>
        public static object WriteNullableDate(DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                return DBNull.Value;
            }
            else
            {
                return date;
            }
        }

        /// <summary>
        /// Gets the nullable date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>DateTime - if date is DBNull.Value then returns DateTime.MinValue otherwise date is returned</returns>
        public static DateTime GetNullableDate(object date)
        {
            if (date == DBNull.Value)
            {
                return DateTime.MinValue;
            }
            else
            {
                return Convert.ToDateTime(date);
            }
        }
        
        /// <summary>
        /// Gets the day of week array as a byte value
        /// </summary>
        /// <param name="dayofWeekArray">The day of week array.</param>
        /// <returns>byte value</returns>
        public static byte GetDayOfWeekArrayAsByte(DayOfWeek[] dayofWeekArray)
        {
            byte retVal = 0;

            for (int i = 0; i < dayofWeekArray.Length; i++)
            {
                byte val = GetDayOfWeekAsByte(dayofWeekArray[i]);
                retVal += val;
            }

            return Convert.ToByte(retVal);
        }

        /// <summary>
        /// Gets the day of week array as a byte value
        /// </summary>
        /// <param name="dayofWeekByteValue">a byte value representing a day of week array.</param>
        /// <returns>DayOfWeek array</returns>
        public static DayOfWeek[] GetDayOfWeekArrayFromByte(byte dayofWeekByteValue)
        {
            ParameterCheckHelper.CheckIsValidByte(dayofWeekByteValue, "dayofWeekByteValue");
            if (dayofWeekByteValue > 127)
            {
                throw new ArgumentException(string.Format("dayofWeekByteValue: {0} cannot be greater than 127", dayofWeekByteValue));
            }

            int intValue = Convert.ToInt32(dayofWeekByteValue);
            List<DayOfWeek> daysOfWeek = new List<DayOfWeek>();

            if ((intValue & Sunday) == Sunday)
            {
                daysOfWeek.Add(DayOfWeek.Sunday);
            }

            if ((intValue & Monday) == Monday)
            {
               daysOfWeek.Add(DayOfWeek.Monday);
            }

            if ((intValue & Tuesday) == Tuesday)
            {
                daysOfWeek.Add(DayOfWeek.Tuesday);
            }

            if ((intValue & Wednesday) == Wednesday)
            {
                daysOfWeek.Add(DayOfWeek.Wednesday);
            }

            if ((intValue & Thursday) == Thursday)
            {
                daysOfWeek.Add(DayOfWeek.Thursday);
            }

            if ((intValue & Friday) == Friday)
            {
                daysOfWeek.Add(DayOfWeek.Friday);
            }

            if ((intValue & Saturday) == Saturday)
            {
                daysOfWeek.Add(DayOfWeek.Saturday);
            }

            return daysOfWeek.ToArray();
        }

        /// <summary>
        /// Gets the day of week as a byte value
        /// </summary>
        /// <param name="dayofWeek">The day of week.</param>
        /// <returns>byte value</returns>
        public static byte GetDayOfWeekAsByte(DayOfWeek dayofWeek)
        {
            byte retVal = 0;

            switch (dayofWeek)
            {
                case DayOfWeek.Sunday:
                    retVal = Sunday;
                    break;
                case DayOfWeek.Monday:
                    retVal = Monday;
                    break;
                case DayOfWeek.Tuesday:
                    retVal = Tuesday;
                    break;
                case DayOfWeek.Wednesday:
                    retVal = Wednesday;
                    break;
                case DayOfWeek.Thursday:
                    retVal = Thursday;
                    break;
                case DayOfWeek.Friday:
                    retVal = Friday;
                    break;
                case DayOfWeek.Saturday:
                    retVal = Saturday;
                    break;
            }

            return retVal;
        }

        /// <summary>
        /// Gets the day of week from a byte value        
        /// <para>
        /// Sunday = 1, Monday = 2, Tuesday = 4, Wednesday = 8, Thursday = 16, Friday = 32, Saturday = 64
        /// </para>
        /// </summary>
        /// <param name="dayofWeekValue">The dayof week value.</param>
        /// <returns>DayOfWeek object</returns>
        public static DayOfWeek GetDayOfWeekFromByte(byte dayofWeekValue)
        {
            DayOfWeek? retVal = null;

            switch (dayofWeekValue)
            {
                case Sunday:
                    retVal = DayOfWeek.Sunday;
                    break;
                case Monday:
                    retVal = DayOfWeek.Monday;
                    break;
                case Tuesday:
                    retVal = DayOfWeek.Tuesday;
                    break;
                case Wednesday:
                    retVal = DayOfWeek.Wednesday;
                    break;
                case Thursday:
                    retVal = DayOfWeek.Thursday;
                    break;
                case Friday:
                    retVal = DayOfWeek.Friday;
                    break;
                case Saturday:
                    retVal = DayOfWeek.Saturday;
                    break;                    
            }

            if (retVal == null)
            {
                throw new ArgumentException(string.Format("Invalid byte value: {0} for dayofWeekValue", dayofWeekValue));
            }

            return (DayOfWeek)retVal;
        }
    }
}
