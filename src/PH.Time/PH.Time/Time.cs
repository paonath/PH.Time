using System;
using System.Collections.Generic;

namespace PH.Time
{
    public class TimeUtility
    {

        public static Time[] BuildTimeArray(Time start, Time end, TimePart buildByPart = TimePart.Hours,
                                            bool includeExtremes = true)
        {
            var l = new List<Time>();
            if (includeExtremes)
            {
                l.Add(start);
                l.Add(end);
            }
            throw new NotImplementedException("to complete!");
        }
    }

    /// <summary>
    /// The Time Parts : <see cref="Hours"/> , <see cref="Minutes"/> and <see cref="Seconds"/>
    /// </summary>
    public enum TimePart
    {
        /// <summary>The hours part of <see cref="Time"/></summary>
        /// <seealso cref="Time.Hours"/>
        Hours = 0,

        /// <summary>The minutes part of <see cref="Time"/></summary>
        /// <seealso cref="Time.Minutes"/>
        Minutes = 1,

        // <summary>The seconds part of <see cref="Time"/></summary>
        /// <seealso cref="Time.Seconds"/>
        Seconds = 2
    }

    /// <summary>
    /// The Time representation
    /// </summary>
    public struct Time :  IComparable<Time> , IEquatable<Time>
    {

        /// <summary>
        /// The Hours part of a DateTime
        /// </summary>
        public int Hours { get; set; }

        /// <summary>
        /// The Minutes part of a DateTime
        /// </summary>
        public int Minutes { get; set; }

        /// <summary>
        /// The Seconds part of a DateTime
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// Init new Time
        /// </summary>
        /// <param name="hours">hours</param>
        /// <param name="minutes">minutes</param>
        /// <param name="seconds">seconds</param>
        public Time(int hours, int minutes, int seconds = 0)
        {
            if (hours < 0 || hours > 23)
            {
                throw new ArgumentOutOfRangeException(nameof(hours), hours, $"A valid Hour is between 0 and 23");
            }

            if (minutes < 0 || minutes > 59)
            {
                throw new ArgumentOutOfRangeException(nameof(minutes), minutes, $"A valid Minute is between 0 and 59");
            }

            if (seconds < 0 || seconds > 59)
            {
                throw new ArgumentOutOfRangeException(nameof(seconds), seconds, $"A valid Second is between 0 and 59");
            }

            Hours   = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        

        public static Time MinValue => new Time(0, 0, 0);
        public static Time MaxValue => new Time(23, 59, 59);

        public int CompareTo(Time other)
        {
            var hoursComparison = Hours.CompareTo(other.Hours);
            if (hoursComparison != 0)
            {
                return hoursComparison;
            }

            var minutesComparison = Minutes.CompareTo(other.Minutes);
            if (minutesComparison != 0)
            {
                return minutesComparison;
            }

            return Seconds.CompareTo(other.Seconds);
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode =  Hours * 60 * 60 * 1000;
                hashCode += Minutes * 60 * 1000;
                hashCode += Seconds * 1000;
                return hashCode;
            }
        }

        /// <summary>Indicates whether this instance and a specified object are equal.</summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>true if <paramref name="obj">obj</paramref> and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is Time other && Equals(other);
        }

        //internal Lazy<int>  OrderValue => n (Seconds * 1000) + (Minutes * 60 * 1000) + (Hours * 60 * 60 * 1000);
       

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(Time other)
        {
            if (Hours != other.Hours)
            {
                return false;
            }
            
            if (Minutes != other.Minutes)
            {
                return false;
            }
            return Seconds == other.Seconds;
            
        }

        public static bool operator ==(Time left, Time right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Time left, Time right)
        {
            return !left.Equals(right);
        }

        public static bool operator <(Time left, Time right)
        {
            return left.CompareTo(right) == -1;
        }
        public static bool operator <=(Time left, Time right)
        {
            
            return left.CompareTo(right) < 1;
        }
        public static bool operator >(Time left, Time right)
        {
            return left.CompareTo(right) == 1;
        }

        public static bool operator >=(Time left, Time right)
        {
            
            return left.CompareTo(right) >= 0;
        }

        


        /// <summary>Returns the string value representation of this instance as '16:32:32'. </summary>
        /// <returns>string value.</returns>
        public override string ToString()
        {
            return ToString("t");
        }

       


        public string ToString(string format)
        {
            var h = $"{Hours}".PadLeft(2, '0');
            var m = $"{Minutes}".PadLeft(2, '0');
            var s = $"{Seconds}".PadLeft(2, '0');
            
            //    * t :16:32
            //    * T :16:32:32
            switch (format)
            {
                case "t":
                    return $"{h}:{m}";
                case "T":
                    default:
                    return $"{h}:{m}:{s}";
            }
        }
        
        
        /// <summary>
        /// Parse a string as '13:10:27' to a <see cref="Time"/>
        /// </summary>
        /// <param name="timeAsString">string representation of Time</param>
        /// <param name="time">Time</param>
        /// <returns><c>true</c> if parsing is success</returns>
        public bool TryParse(string timeAsString, out Time time)
        {
            time = MinValue;
            try
            {
                var spl = timeAsString.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                if (spl.Length == 3)
                {
                    time = new Time(int.Parse(spl[0]), int.Parse(spl[1]), int.Parse(spl[3]));
                    return true;
                }
                time = new Time(int.Parse(spl[0]), int.Parse(spl[1]), 0);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
