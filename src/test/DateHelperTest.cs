using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Codentia.Common.Helper.Test
{
    /// <summary>
    /// This class contains the unit tests for the static class DateHelper
    /// </summary>
    [TestFixture]
    public class DateHelperTest
    {
        /// <summary>
        /// Scenario: Method called with valid parameters
        /// Expected: th
        /// </summary>
        [Test]
        public void _001_GetOrdinal_TH()
        {
            int[] daysWhichAreTH = new int[] { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 24, 25, 26, 27, 28, 29, 30 };

            for (int i = 0; i < daysWhichAreTH.Length; i++)
            {
                Assert.That(DateHelper.GetOrdinal(new DateTime(1900, 1, daysWhichAreTH[i])), Is.EqualTo("th"), "Failed on " + daysWhichAreTH[i]);
            }
        }

        /// <summary>
        /// Scenario: Method called with valid parameters
        /// Expected: nd
        /// </summary>
        [Test]
        public void _002_GetOrdinal_ND()
        {
            int[] daysWhichAreND = new int[] { 2, 22 };

            for (int i = 0; i < daysWhichAreND.Length; i++)
            {
                Assert.That(DateHelper.GetOrdinal(new DateTime(1900, 1, daysWhichAreND[i])), Is.EqualTo("nd"), "Failed on " + daysWhichAreND[i]);
            }
        }

        /// <summary>
        /// Scenario: Method called with valid parameters
        /// Expected: rd
        /// </summary>
        [Test]
        public void _003_GetOrdinal_RD()
        {
            int[] daysWhichAreRD = new int[] { 3, 23 };

            for (int i = 0; i < daysWhichAreRD.Length; i++)
            {
                Assert.That(DateHelper.GetOrdinal(new DateTime(1900, 1, daysWhichAreRD[i])), Is.EqualTo("rd"), "Failed on " + daysWhichAreRD[i]);
            }
        }

        /// <summary>
        /// Scenario: Method called with valid parameters
        /// Expected: st
        /// </summary>
        [Test]
        public void _004_GetOrdinal_ST()
        {
            int[] daysWhichAreST = new int[] { 1, 21, 31 };

            for (int i = 0; i < daysWhichAreST.Length; i++)
            {
                Assert.That(DateHelper.GetOrdinal(new DateTime(1900, 1, daysWhichAreST[i])), Is.EqualTo("st"), "Failed on " + daysWhichAreST[i]);
            }
        }

        /// <summary>
        /// Scenario: Method called with valid parameters, day is a single digit
        /// Expected: Unpadded day with ordinal e.g 2nd
        /// </summary>
        [Test]
        public void _005_GetDayOfMonthWithOrdinal_SingleDigit()
        {
            Assert.That(DateHelper.GetDayOfMonthWithOrdinal(new DateTime(1900, 1, 1)), Is.EqualTo("1st"));
        }

        /// <summary>
        /// Scenario: Method called with valid parameters, day is double digit
        /// Expected: Unpadded day with ordinal e.g 2nd
        /// </summary>
        [Test]
        public void _006_GetDayOfMonthWithOrdinal_DoubleDigit()
        {
            Assert.That(DateHelper.GetDayOfMonthWithOrdinal(new DateTime(1900, 1, 12)), Is.EqualTo("12th"));
        }

        /// <summary>
        /// Scenario: Call DayOfWeekArray
        /// Expected: Runs successfully
        /// </summary>
        [Test]
        public void _007_DayOfWeekArray()
        {
            DayOfWeek[] dowArray = DateHelper.DayOfWeekArray();

            Assert.That(dowArray[0], Is.EqualTo(DayOfWeek.Sunday));
            Assert.That(dowArray[1], Is.EqualTo(DayOfWeek.Monday));
            Assert.That(dowArray[2], Is.EqualTo(DayOfWeek.Tuesday));
            Assert.That(dowArray[3], Is.EqualTo(DayOfWeek.Wednesday));
            Assert.That(dowArray[4], Is.EqualTo(DayOfWeek.Thursday));
            Assert.That(dowArray[5], Is.EqualTo(DayOfWeek.Friday));
            Assert.That(dowArray[6], Is.EqualTo(DayOfWeek.Saturday));          
        }

        /// <summary>
        /// Scenario: Call DayOfWeekArray with optional argument
        /// Expected: Produces the correct list of days
        /// </summary>
        [Test]
        public void _007_DayOfWeekArray_WithStartDay()
        {
            // week starts monday
            DayOfWeek[] dowArray = DateHelper.DayOfWeekArray(DayOfWeek.Monday);
            Assert.That(dowArray.Length, Is.EqualTo(7));            
            
            Assert.That(dowArray[0], Is.EqualTo(DayOfWeek.Monday));
            Assert.That(dowArray[1], Is.EqualTo(DayOfWeek.Tuesday));
            Assert.That(dowArray[2], Is.EqualTo(DayOfWeek.Wednesday));
            Assert.That(dowArray[3], Is.EqualTo(DayOfWeek.Thursday));
            Assert.That(dowArray[4], Is.EqualTo(DayOfWeek.Friday));
            Assert.That(dowArray[5], Is.EqualTo(DayOfWeek.Saturday));
            Assert.That(dowArray[6], Is.EqualTo(DayOfWeek.Sunday));

            // week starts thursday
            dowArray = DateHelper.DayOfWeekArray(DayOfWeek.Thursday);
            Assert.That(dowArray.Length, Is.EqualTo(7));  
           
            Assert.That(dowArray[0], Is.EqualTo(DayOfWeek.Thursday));
            Assert.That(dowArray[1], Is.EqualTo(DayOfWeek.Friday));
            Assert.That(dowArray[2], Is.EqualTo(DayOfWeek.Saturday));
            Assert.That(dowArray[3], Is.EqualTo(DayOfWeek.Sunday));
            Assert.That(dowArray[4], Is.EqualTo(DayOfWeek.Monday));
            Assert.That(dowArray[5], Is.EqualTo(DayOfWeek.Tuesday));
            Assert.That(dowArray[6], Is.EqualTo(DayOfWeek.Wednesday));
        }

        /// <summary>
        /// Scenario: Call GetTimeSpanAsString 
        /// Expected: string with correct format
        /// </summary>
        [Test]
        public void _008_GetTimeSpanAsString()
        {
            // single digits are padded
            TimeSpan ts = new TimeSpan(0, 0, 5);
            Assert.That(DateHelper.GetTimeSpanAsString(ts), Is.EqualTo("00:00:00:05"));

            ts = new TimeSpan(0, 2, 0);
            Assert.That(DateHelper.GetTimeSpanAsString(ts), Is.EqualTo("00:00:02:00"));

            ts = new TimeSpan(4, 0, 0);
            Assert.That(DateHelper.GetTimeSpanAsString(ts), Is.EqualTo("00:04:00:00"));

            ts = new TimeSpan(9, 0, 0, 0);
            Assert.That(DateHelper.GetTimeSpanAsString(ts), Is.EqualTo("09:00:00:00"));

            // double digits
            ts = new TimeSpan(10, 23, 59, 12);
            Assert.That(DateHelper.GetTimeSpanAsString(ts), Is.EqualTo("10:23:59:12"));
        }

         /// <summary>
        /// Scenario: Call GetStringAsTimeSpan with badly formatted timespan string
        /// Expected: Exception
        /// </summary>
        [Test]
        public void _009_GetStringAsTimeSpan_InvalidString()
        {
            Assert.That(delegate { DateHelper.GetStringAsTimeSpan("absdghg"); }, Throws.InstanceOf<InvalidCastException>().With.Message.EqualTo("Unable to cast: absdghg as timespan - format is days:hours:mins:secs"));
        }

        /// <summary>
        /// Scenario: Call GetStringAsTimeSpan with correctly formatted timespan string
        /// Expected: Timespan returned
        /// </summary>
        [Test]
        public void _010_GetStringAsTimeSpan_ValidString()
        {
            TimeSpan ts = new TimeSpan(10, 23, 59, 12);
            string timeString = DateHelper.GetTimeSpanAsString(ts);
            TimeSpan ts2 = DateHelper.GetStringAsTimeSpan(timeString);
            Assert.That(ts2, Is.EqualTo(ts));

            // test object overload
            object timeStringObject = DateHelper.GetTimeSpanAsString(ts);
            TimeSpan ts3 = DateHelper.GetStringAsTimeSpan(timeStringObject);
            Assert.That(ts3, Is.EqualTo(ts));
        }

        /// <summary>
        /// Scenario: Call WriteNullableDate with DateTime.MinValue and DateTime.Now
        /// Expected: Returns DBNull.Value for DateTime.MinValue and DateTime.Now
        /// </summary>
        [Test]
        public void _011_WriteNullableDate()
        {
            Assert.That(DateHelper.WriteNullableDate(DateTime.MinValue), Is.EqualTo(DBNull.Value));
            DateTime dte = DateTime.Now;
            Assert.That(DateHelper.WriteNullableDate(dte), Is.EqualTo(dte));
        }

        /// <summary>
        /// Scenario: Call GetNullableDate with DBNull.Value and DateTime.Now
        /// Expected: Returns DateTime.MinValue for DBNull.Value and DateTime.Now
        /// </summary>
        [Test]
        public void _012_GetNullableDate()
        {
            Assert.That(DateHelper.GetNullableDate(DBNull.Value), Is.EqualTo(DateTime.MinValue));
            DateTime dte = DateTime.Now;
            Assert.That(DateHelper.GetNullableDate(dte), Is.EqualTo(dte));
            string dteString = "08 April 2012";
            DateTime dtecheck = Convert.ToDateTime(dteString);
            Assert.That(DateHelper.GetNullableDate(dteString), Is.EqualTo(dtecheck));
        }

        /// <summary>
        /// Scenario: Call GetDayOfWeekArrayAsByte - all Days of Week
        /// Expected: Returns 127
        /// </summary>
        [Test]
        public void _013_GetDayOfWeekArrayAsByte_AllDays()
        {
            List<DayOfWeek> days = new List<DayOfWeek>();
            days.Add(DayOfWeek.Sunday);
            days.Add(DayOfWeek.Monday);
            days.Add(DayOfWeek.Tuesday);
            days.Add(DayOfWeek.Wednesday);
            days.Add(DayOfWeek.Thursday);
            days.Add(DayOfWeek.Friday);
            days.Add(DayOfWeek.Saturday);

            Assert.That(DateHelper.GetDayOfWeekArrayAsByte(days.ToArray()), Is.EqualTo(127));
        }

        /// <summary>
        /// Scenario: Call GetDayOfWeekArrayAsByte - various Days of Week
        /// Expected: Returns correct byte values
        /// </summary>
        [Test]
        public void _014_GetDayOfWeekArrayAsByte_VariousDays()
        {
            List<DayOfWeek> days = new List<DayOfWeek>();
            days.Add(DayOfWeek.Sunday);           

            Assert.That(DateHelper.GetDayOfWeekArrayAsByte(days.ToArray()), Is.EqualTo(1));

            days.Clear();

            days.Add(DayOfWeek.Monday);
            days.Add(DayOfWeek.Tuesday);

            Assert.That(DateHelper.GetDayOfWeekArrayAsByte(days.ToArray()), Is.EqualTo(6));

            days.Clear();

            days.Add(DayOfWeek.Monday);
            days.Add(DayOfWeek.Wednesday);            
            days.Add(DayOfWeek.Friday);

            Assert.That(DateHelper.GetDayOfWeekArrayAsByte(days.ToArray()), Is.EqualTo(42));
        }

        /// <summary>
        /// Scenario: Call GetDayOfWeekArrayAsByte - negative tests
        /// Expected: Exceptions
        /// </summary>
        [Test]
        public void _015_GetDayOfWeekArrayFromByte_NegativeTests()
        {
            Assert.That(delegate { DateHelper.GetDayOfWeekArrayFromByte(0); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("dayofWeekByteValue: 0 is not valid"));
            Assert.That(delegate { DateHelper.GetDayOfWeekArrayFromByte(128); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("dayofWeekByteValue: 128 cannot be greater than 127"));
        }      
  
         /// <summary>
        /// Scenario: Call GetDayOfWeekArrayAsByte - negative tests
        /// Expected: Exceptions
        /// </summary>
        [Test]
        public void _016_GetDayOfWeekArrayFromByte()
        {
            DayOfWeek[] days = DateHelper.GetDayOfWeekArrayFromByte(1);
            Assert.That(days.Length, Is.EqualTo(1));
            Assert.That(days[0], Is.EqualTo(DayOfWeek.Sunday));

            days = DateHelper.GetDayOfWeekArrayFromByte(34);
            Assert.That(days.Length, Is.EqualTo(2));
            Assert.That(days[0], Is.EqualTo(DayOfWeek.Monday));
            Assert.That(days[1], Is.EqualTo(DayOfWeek.Friday));

            days = DateHelper.GetDayOfWeekArrayFromByte(100);
            Assert.That(days.Length, Is.EqualTo(3));
            Assert.That(days[0], Is.EqualTo(DayOfWeek.Tuesday));
            Assert.That(days[1], Is.EqualTo(DayOfWeek.Friday));
            Assert.That(days[2], Is.EqualTo(DayOfWeek.Saturday));

            days = DateHelper.GetDayOfWeekArrayFromByte(127);
            Assert.That(days.Length, Is.EqualTo(7));
            Assert.That(days[0], Is.EqualTo(DayOfWeek.Sunday));
            Assert.That(days[1], Is.EqualTo(DayOfWeek.Monday));
            Assert.That(days[2], Is.EqualTo(DayOfWeek.Tuesday));
            Assert.That(days[3], Is.EqualTo(DayOfWeek.Wednesday));
            Assert.That(days[4], Is.EqualTo(DayOfWeek.Thursday));
            Assert.That(days[5], Is.EqualTo(DayOfWeek.Friday));
            Assert.That(days[6], Is.EqualTo(DayOfWeek.Saturday));
        }

        /// <summary>
        /// Scenario: Call GetDayOfWeekFromByte - invalid byte value
        /// Expected: Exception
        /// </summary>
        [Test]
        public void _017_GetDayOfWeekFromByte_InvalidByteValue()
        {
            Assert.That(delegate { DateHelper.GetDayOfWeekFromByte(200); }, Throws.ArgumentException.With.Message.EqualTo("Invalid byte value: 200 for dayofWeekValue"));
        }

        /// <summary>
        /// Scenario: Call GetDayOfWeekFromByte
        /// Expected: Success
        /// </summary>
        [Test]
        public void _018_GetDayOfWeekFromByte_AllValues()
        {
            Assert.That(DateHelper.GetDayOfWeekFromByte(1), Is.EqualTo(DayOfWeek.Sunday));
            Assert.That(DateHelper.GetDayOfWeekFromByte(2), Is.EqualTo(DayOfWeek.Monday));
            Assert.That(DateHelper.GetDayOfWeekFromByte(4), Is.EqualTo(DayOfWeek.Tuesday));
            Assert.That(DateHelper.GetDayOfWeekFromByte(8), Is.EqualTo(DayOfWeek.Wednesday));
            Assert.That(DateHelper.GetDayOfWeekFromByte(16), Is.EqualTo(DayOfWeek.Thursday));
            Assert.That(DateHelper.GetDayOfWeekFromByte(32), Is.EqualTo(DayOfWeek.Friday));
            Assert.That(DateHelper.GetDayOfWeekFromByte(64), Is.EqualTo(DayOfWeek.Saturday));
        }
    }
}
