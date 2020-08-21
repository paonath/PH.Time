using System;
using System.Globalization;
using JetBrains.Annotations;

namespace PH.Time
{
    /// <summary>
    /// The Time representation
    /// </summary>
    public struct Time :  IComparable<Time> , IEquatable<Time> , IFormattable
    {
        private int _hours;

        /// <summary>
        /// The Hours part of a DateTime
        /// </summary>
        public int Hours
        {
            get => _hours;
            set => SetHours(value);
        }

        private int _minutes;

        /// <summary>
        /// The Minutes part of a DateTime
        /// </summary>
        public int Minutes
        {
            get => _minutes;
            set => SetMinutes(value);
        }

        private int _seconds;

        /// <summary>
        /// The Seconds part of a DateTime
        /// </summary>
        public int Seconds
        {
            get => _seconds;
            set => SetSeconds(value);
        }

        private Time(object o)
        {
            _hours   = 0;
            _minutes = 0;
            _seconds = 0;
        }

        /// <summary>
        /// Init new Time
        /// </summary>
        /// <param name="hours">hours</param>
        /// <param name="minutes">minutes</param>
        /// <param name="seconds">seconds</param>
        /// <exception cref="ArgumentOutOfRangeException">hours - A valid Hour is between 0 and 23</exception>
        /// <exception cref="ArgumentOutOfRangeException">minutes - A valid Minute is between 0 and 59</exception>
        /// <exception cref="ArgumentOutOfRangeException">seconds - A valid Second is between 0 and 59</exception>
        public Time(int hours, int minutes, int seconds = 0)
            :this(null)
        {
            SetHours(hours);
            SetMinutes(minutes);
            SetSeconds(seconds);
        }

        /// <summary>Sets the hours.</summary>
        /// <param name="hours">The hours.</param>
        /// <exception cref="ArgumentOutOfRangeException">hours - A valid Hour is between 0 and 23</exception>
        private void SetHours(int hours)
        {
            if (hours < 0 || hours > 23)
            {
                throw new ArgumentOutOfRangeException(nameof(hours), hours, $"A valid Hour is between 0 and 23");
            }

            _hours = hours;
        }

        /// <summary>Sets the minutes.</summary>
        /// <param name="minutes">The minutes.</param>
        /// <exception cref="ArgumentOutOfRangeException">minutes - A valid Minute is between 0 and 59</exception>
        private void SetMinutes(int minutes)
        {
            if (minutes < 0 || minutes > 59)
            {
                throw new ArgumentOutOfRangeException(nameof(minutes), minutes, "A valid Minute is between 0 and 59");
            }

            _minutes = minutes;
        }

        /// <summary>Sets the seconds.</summary>
        /// <param name="seconds">The seconds.</param>
        /// <exception cref="ArgumentOutOfRangeException">seconds - A valid Second is between 0 and 59</exception>
        private void SetSeconds(int seconds = 0)
        {
            if (seconds < 0 || seconds > 59)
            {
                throw new ArgumentOutOfRangeException(nameof(seconds), seconds, "A valid Second is between 0 and 59");
            }

            _seconds = seconds;
        }

        /// <summary>The minimum value for a Time: 00:00:00</summary>
        public static readonly Time MinValue = new Time(0, 0);

        /// <summary>The maximum value for a Time: 23:59:59</summary>
        public static readonly Time MaxValue = new Time(23, 59, 59);

        /// <summary>Get if Time is smaller, greather or equal than other.</summary>
        /// <param name="other">The other Time.</param>
        /// <returns><c>-1</c> if smaller, <c>0</c> if equals or <c>1</c> if grather</returns>
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

        /// <summary>Returns the hash code for this instance. The value is numeber of milliseconds from 00:00:00 to <see cref="Time"/></summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return GetMilliseconds();

        }

        /// <summary>Gets the milliseconds from 00:00:00 to <see cref="Time"/>.</summary>
        /// <returns>milliseconds from 00:00:00 to time</returns>
        public int GetMilliseconds()
        {
            var millis =  Hours * 60 * 60 * 1000;
            millis += Minutes * 60 * 1000;
            millis += Seconds * 1000;
            return millis;
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


        [NotNull]
        public string GetLongTime() =>
            $"{Hours.ToString().PadLeft(2, '0')}:{Minutes.ToString().PadLeft(2, '0')}:{Seconds.ToString().PadLeft(2, '0')}";

        [NotNull]
        public string GetShortTime() => $"{Hours.ToString().PadLeft(2, '0')}:{Minutes.ToString().PadLeft(2, '0')}";

        /// <summary>Returns the string value representation of this instance as '16:32:32'. </summary>
        /// <returns>string value.</returns>
        [NotNull]
        public override string ToString()
        {
            return ToString("T", CultureInfo.CurrentCulture);
        }

        /// <summary>Formats the value of the current instance using the specified format.</summary>
        /// <param name="format">The format to use.   -or-   A null reference (Nothing in Visual Basic) to use the default format defined for the type of the <see cref="T:System.IFormattable"></see> implementation.</param>
        /// <param name="formatProvider">The provider to use to format the value.   -or-   A null reference (Nothing in Visual Basic) to obtain the numeric format information from the current locale setting of the operating system.</param>
        /// <returns>The value of the current instance in the specified format.</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "G";
            }

            //if (formatProvider == null)
            //{
            //    formatProvider = CultureInfo.CurrentCulture;
            //}
            
            switch (format)
            {
                    


                case "t":
                    case "shortTime":
                    return GetShortTime();
                case "T":
                case "G":
                case "longTime":
                default:
                    return GetLongTime();

            }

           
        }


        
        /// <summary>Parses the specified time as string.</summary>
        /// <param name="timeAsString">The time as string if format hh:mm:ss or hh:mm.</param>
        /// <returns>Time value</returns>
        public static Time Parse(string timeAsString)
        {
            var spl = timeAsString.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
            if (spl.Length == 3)
            {
                return new Time(int.Parse(spl[0]), int.Parse(spl[1]), int.Parse(spl[3]));
                
            }
            return new Time(int.Parse(spl[0]), int.Parse(spl[1]));
        }
        
        /// <summary>
        /// Parse a string as hh:mm:ss or hh:mm to a <see cref="Time"/>
        /// </summary>
        /// <param name="timeAsString">string representation of Time</param>
        /// <param name="time">Time</param>
        /// <returns><c>true</c> if parsing is success</returns>
        public static bool TryParse(string timeAsString, out Time time)
        {
            time = MinValue;
            try
            {
                time = Parse(timeAsString);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }


    
}
