﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.WebJobs.Extensions.Timers;

namespace Microsoft.Azure.WebJobs
{
    /// <summary>
    /// Provides access to timer information for jobs triggered by <see cref="TimerTriggerAttribute"/>
    /// </summary>
    public class TimerInfo
    {
        /// <summary>
        /// Constructs a new instances
        /// </summary>
        /// <param name="schedule">The timer trigger schedule.</param>
        public TimerInfo(TimerSchedule schedule)
        {
            Schedule = schedule;
        }

        /// <summary>
        /// Gets the schedule for the timer trigger.
        /// </summary>
        public TimerSchedule Schedule { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this timer invocation
        /// is due to a missed schedule occurrence.
        /// </summary>
        public bool IsPastDue { get; set; }

        /// <summary>
        /// Formats the next 'count' occurrences of the schedule into an
        /// easily loggable string.
        /// </summary>
        /// <param name="count">The number of occurrences to format.</param>
        /// <param name="now">The optional <see cref="DateTime"/> to start from.</param>
        /// <returns>A formatted string with the next occurrences.</returns>
        public string FormatNextOccurrences(int count, DateTime? now = null)
        {
            return FormatNextOccurrences(Schedule, count, now);
        }

        internal static string FormatNextOccurrences(TimerSchedule schedule, int count, DateTime? now = null)
        {
            if (schedule == null)
            {
                throw new ArgumentNullException("schedule");
            }

            IEnumerable<DateTime> nextOccurrences = schedule.GetNextOccurrences(count, now);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Format("The next {0} occurrences of the schedule will be:", count));
            foreach (DateTime occurrence in nextOccurrences)
            {
                builder.AppendLine(occurrence.ToString());
            }

            return builder.ToString();
        }
    }
}
