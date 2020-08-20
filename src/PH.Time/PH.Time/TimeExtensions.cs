using System;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("PH.Time.XUnitTest")]
namespace PH.Time
{
    public static class TimeExtensions
    {
        internal static Time NextSecond(this Time time, out bool onNextDay)
        {
            onNextDay = false;
            if (time.Seconds < 59)
            {
                int s = 1 + time.Seconds;
                return new Time(time.Hours, time.Minutes, s);
            }
            
            time.Seconds = 0;
            return time.NextMinute(out onNextDay);
        
        }

        internal static Time NextMinute(this Time time, out bool onNextDay)
        {
            onNextDay = false;
            if (time.Minutes < 59)
            {
                int m = 1 + time.Minutes;
                return new Time(time.Hours, m, time.Seconds);
            }
            time.Minutes = 0;
            return time.NextHour(out onNextDay);
        }

        internal static Time NextHour(this Time time, out bool onNextDay)
        {
            onNextDay = false;
            if (time.Hours < 23)
            {
                int h = 1 + time.Hours;
                return new Time(h, time.Minutes, time.Seconds);
            }

            onNextDay = true;
            return new Time(0, time.Minutes, time.Seconds);
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