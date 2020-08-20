using System;
using System.Collections.Generic;
using System.Linq;

namespace PH.Time
{
    public class TimeFactory
    {
        public static Time[] BuildTimeArrayBySteps(Time start, Time end, int step, TimePart stepPart = TimePart.Minutes,
                                                   bool includeExtremes = true)
        {
            throw new NotImplementedException("to implement!");
        }

        /// <summary>Builds the time array.</summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="buildByPart">The build by part.</param>
        /// <param name="includeExtremes">if set to <c>true</c> include <see cref="start"/> and <see cref="end"/>.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Start value must be smaller than end value.</exception>
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
        internal static Time[] BuildTimeArrayByHours(Time start, Time end, bool includeExtremes = true)
        {
            ThrowIfBeginIsGratherThanEnd(start, end);
            var l = new List<Time>();
            if (includeExtremes)
            {
                l.Add(start);
            }

            var x = start.NextHour(out var onNextDay);
            while (x < end && !onNextDay)
            {
                l.Add(x);
                x = x.NextHour(out onNextDay);
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
        /// <returns>time array by minutes</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Start value must be smaller than end value.</exception>
        internal static Time[] BuildTimeArrayByMinutes(Time start, Time end, bool includeExtremes = true)
        {
            ThrowIfBeginIsGratherThanEnd(start, end);
            var l = new List<Time>();
            if (includeExtremes)
            {
                l.Add(start);
            }

            var x = start.NextMinute(out var onNextDay);
            while (x < end && !onNextDay)
            {
                l.Add(x);
                x = x.NextMinute(out onNextDay);
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
        /// <returns>time array by seconds</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Start value must be smaller than end value.</exception>
        internal static Time[] BuildTimeArrayBySeconds(Time start, Time end, bool includeExtremes = true)
        {
            ThrowIfBeginIsGratherThanEnd(start, end);
            var l = new List<Time>();
            if (includeExtremes)
            {
                l.Add(start);
            }

            var x = start.NextSecond(out var onNextDay);
            while (x < end && !onNextDay)
            {
                l.Add(x);
                x = x.NextSecond(out onNextDay);
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