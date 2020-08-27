using System;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("PH.Time.XUnitTest")]
namespace PH.Time
{
    public static class TimeExtensions
    {
        /// <summary>Get the next <see cref="Time"/> by the specified part.</summary>
        /// <param name="time">The time.</param>
        /// <param name="part">The part.</param>
        /// <param name="onNextDay">if set to <c>true</c> occurs on next day.</param>
        /// <returns>Next Time</returns>
        public static Time Next(this Time time, TimePart part, out bool onNextDay)
        {
            switch (part)
            {
                case TimePart.Hours:
                    return time.NextHour(out onNextDay);
                case TimePart.Minutes:
                    return time.NextMinute(out onNextDay);
                case TimePart.Seconds:
                    default:
                    return time.NextSecond(out onNextDay);
            }
        }

        /// <summary>Get the previous <see cref="Time"/> by the specified part .</summary>
        /// <param name="time">The time.</param>
        /// <param name="part">The part.</param>
        /// <param name="onPreviousDay">if set to <c>true</c> occurs on previous day.</param>
        /// <returns>Previous Time</returns>
        public static Time Previous(this Time time, TimePart part, out bool onPreviousDay)
        {
            switch (part)
            {
                case TimePart.Hours:
                    return time.PreviousHour(out onPreviousDay);
                case TimePart.Minutes:
                    return time.PreviousMinute(out onPreviousDay);
                case TimePart.Seconds:
                default:
                    return time.PreviousSecond(out onPreviousDay);
            }
        }

        internal static Time PreviousSecond(this Time time, out bool onPreviousDay)
        {
            var dt0 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var dt1 = new DateTime(1970, 1, 1, time.Hours, time.Minutes, time.Seconds, DateTimeKind.Utc).AddSeconds(-1);
            onPreviousDay = dt1.Day != dt0.Day;
            return dt1.GetTime();

        }
        internal static Time PreviousMinute(this Time time, out bool onPreviousDay)
        {
            var dt0 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var dt1 = new DateTime(1970, 1, 1, time.Hours, time.Minutes, time.Seconds, DateTimeKind.Utc).AddMinutes(-1);
            onPreviousDay = dt1.Day != dt0.Day;
            return dt1.GetTime();
        }
        internal static Time PreviousHour(this Time time, out bool onPreviousDay)
        {
            var dt0 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var dt1 = new DateTime(1970, 1, 1, time.Hours, time.Minutes, time.Seconds, DateTimeKind.Utc).AddHours(-1);
            onPreviousDay = dt1.Day != dt0.Day;
            return dt1.GetTime();
        }
        internal static Time NextSecond(this Time time, out bool onNextDay)
        {
            var dt0 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var dt1 = new DateTime(1970, 1, 1, time.Hours, time.Minutes, time.Seconds, DateTimeKind.Utc).AddSeconds(1);
            onNextDay = dt1.Day != dt0.Day;
            return dt1.GetTime();
        
        }

        internal static Time NextMinute(this Time time, out bool onNextDay)
        {
            var dt0 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var dt1 = new DateTime(1970, 1, 1, time.Hours, time.Minutes, time.Seconds, DateTimeKind.Utc).AddMinutes(1);
            onNextDay = dt1.Day != dt0.Day;
            return dt1.GetTime();
        }

        internal static Time NextHour(this Time time, out bool onNextDay)
        {
            var dt0 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var dt1 = new DateTime(1970, 1, 1, time.Hours, time.Minutes, time.Seconds, DateTimeKind.Utc).AddHours(1);
            onNextDay = dt1.Day != dt0.Day;
            return dt1.GetTime();
        }

        public static Time GetTime(this DateTime dateTime)
        {
            return new Time(dateTime.Hour, dateTime.Minute, dateTime.Second);
        }
        public static Time GetTime(this DateTimeOffset offset)
        {
            return new Time(offset.Hour, offset.Minute, offset.Second);
        }

    }
}