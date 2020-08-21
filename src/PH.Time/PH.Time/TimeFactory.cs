using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace PH.Time
{
    /// <summary>
    ///  <see cref="Time"/>  Factory class
    /// </summary>
    public class TimeFactory
    {

        /// <summary>Builds the time array by steps.</summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="step">The step.</param>
        /// <param name="stepPart">The step part.</param>
        /// <param name="includeExtremes">if set to <c>true</c> include <see cref="start"/> and <see cref="end"/>.</param>
        /// <returns>Array of Time</returns>
        /// <example>
        /// Build a time array as ["9:30:00","10:0:00","10:30:00","11:00:00","11:30:00","12:00:00"]
        ///<code>
        /// class TestClass
        /// {
        ///     static int Main()
        ///     {
        ///         var toDisplay = TimeFactory.BuildTimeArrayBySteps(new Time(9, 0), new Time(12, 30), 30, TimePart.Minutes, includeExtremes:false);
        ///     }
        /// }
        /// </code>
        /// </example>
        [NotNull]
        public static Time[] BuildTimeArrayBySteps(Time start, Time end, int step, TimePart stepPart = TimePart.Minutes,
                                                   bool includeExtremes = true)
        {
            switch (stepPart)
            {
                case TimePart.Hours:
                    return TimeFactory.BuildTimeArrayByHours(start, end, includeExtremes);
                
                case TimePart.Seconds:
                    return TimeFactory.BuildTimeArrayBySeconds(start, end, includeExtremes);
                case TimePart.Minutes:
                default:
                    return TimeFactory.BuildTimeArrayByMinutes(start, end, includeExtremes,step);    
                
            }
        }

        /// <summary>Builds the time array.</summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="buildByPart">The build by part.</param>
        /// <param name="includeExtremes">if set to <c>true</c> include <see cref="start"/> and <see cref="end"/>.</param>
        /// <returns>Array of Time</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Start value must be smaller than end value.</exception>
        /// <example>
        /// Build a time array as ["9:00:00","9:01:00","9:02:00"]
        /// <code>
        /// class TestClass
        /// {
        ///     static int Main()
        ///     {
        ///         var myArray = TimeFactory.BuildTimeArray(new Time(9, 0, 0), new Time(9, 2, 0), TimePart.Minutes,includeExtremes: true);
        ///     }
        /// }
        /// </code>
        /// </example>
        [NotNull]
        public static Time[] BuildTimeArray(Time start, Time end, TimePart buildByPart = TimePart.Hours,
                                            bool includeExtremes = true)
        {
            switch (buildByPart)
            {
                case TimePart.Minutes:
                    return TimeFactory.BuildTimeArrayByMinutes(start, end, includeExtremes);
                case TimePart.Seconds:
                    return TimeFactory.BuildTimeArrayBySeconds(start, end, includeExtremes);
                default:
                    return TimeFactory.BuildTimeArrayByHours(start, end, includeExtremes);
                
            }
        }

        /// <summary>Builds the time array by hours.</summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="includeExtremes">if set to <c>true</c> include <see cref="start"/> and <see cref="end"/>.</param>
        /// <returns>time array by hours</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Start value must be smaller than end value.</exception>
        [NotNull]
        internal static Time[] BuildTimeArrayByHours(Time start, Time end, bool includeExtremes = true, int steps = 1)
        {
            ThrowIfBeginIsGratherThanEnd(start, end);
            var l = new List<Time>();
            if (includeExtremes)
            {
                l.Add(start);
            }

            var x            = start.NextHour(out var onNextDay);
            int perfomedNext = 1;
            while (x < end && !onNextDay)
            {
                if (perfomedNext == steps)
                {
                    perfomedNext = 0;
                    l.Add(x);
                }

                x = x.NextHour(out onNextDay);
                perfomedNext++;
            }
            if (includeExtremes)
            {
                if (l.All(a => a != end))
                {
                    var last = l.LastOrDefault();
                    if (null != last && last.Hours == end.Hours)
                    {
                        l = l.Except(new Time[]{last}).ToList();
                    }
                    l.Add(end);
                }
            }
            

            return l.ToArray();
        }

        /// <summary>Builds the time array by minutes.</summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="includeExtremes">if set to <c>true</c> include <see cref="start"/> and <see cref="end"/>.</param>
        /// <param name="steps">Add item to array every N minutes step</param>
        /// <returns>time array by minutes</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Start value must be smaller than end value.</exception>
        [NotNull]
        internal static Time[] BuildTimeArrayByMinutes(Time start, Time end, bool includeExtremes = true, int steps = 1)
        {
            ThrowIfBeginIsGratherThanEnd(start, end);
            var l = new List<Time>();
            if (includeExtremes)
            {
                l.Add(start);
            }

            var x            = start.NextMinute(out var onNextDay);
            int perfomedNext = 1;
            while (x < end && !onNextDay)
            {
                if (perfomedNext == steps)
                {
                    perfomedNext = 0;
                    l.Add(x);
                }

                x = x.NextMinute(out onNextDay);
                perfomedNext++;
            }
            if (includeExtremes)
            {
                if (l.All(a => a != end))
                {
                    l.Add(end);
                }
            }

            return l.ToArray();
        }

        /// <summary>Builds the time array by seconds.</summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="includeExtremes">if set to <c>true</c> include <see cref="start"/> and <see cref="end"/>.</param>
        /// <param name="steps">Add item to array every N seconds step</param>
        /// <returns>time array by seconds</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Start value must be smaller than end value.</exception>
        [NotNull]
        internal static Time[] BuildTimeArrayBySeconds(Time start, Time end, bool includeExtremes = true, int steps = 1)
        {
            ThrowIfBeginIsGratherThanEnd(start, end);
            var l = new List<Time>();
            if (includeExtremes)
            {
                l.Add(start);
            }

            var x            = start.NextSecond(out var onNextDay);
            int perfomedNext = 1;
            while (x < end && !onNextDay)
            {
                if (perfomedNext == steps)
                {
                    perfomedNext = 0;
                    l.Add(x);
                }

                x = x.NextSecond(out onNextDay);
                perfomedNext++;
            }
            if (includeExtremes)
            {
                if (l.All(a => a != end))
                {
                    l.Add(end);
                }
            }

            return l.ToArray();
        }

        /// <summary>Throws if begin is grather than end.</summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Start value must be smaller than end value.</exception>
        internal static void ThrowIfBeginIsGratherThanEnd(Time start, Time end)
        {
            if (start > end)
            {
                throw new ArgumentOutOfRangeException(nameof(start), $"Start value must be smaller than end value. Given value: start '{start}', end '{end}'");
            }
        }
    }
}