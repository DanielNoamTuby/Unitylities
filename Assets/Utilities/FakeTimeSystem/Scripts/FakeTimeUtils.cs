using System.Collections.Generic;
using UnityEngine;

namespace Unitylities.FakeTimeUtils
{
    /// <summary>
    /// A utility class for converting time string values to int seconds.
    /// </summary>
    public class TimeConverter
    {
        #region Fields
        private Dictionary<string, int> timeToSeconds = new Dictionary<string, int>();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the dictionary that maps time values to seconds.
        /// </summary>
        public Dictionary<string, int> TimeToSeconds => timeToSeconds;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeConverter"/> class.
        /// </summary>
        public TimeConverter()
        {
            // Populate the dictionary with time values and corresponding seconds
            PopulateTimeToSeconds();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Populates the <see cref="TimeToSeconds"/> dictionary with time string values and corresponding int seconds.
        /// </summary>
        private void PopulateTimeToSeconds()
        {
            // // Adding entries for the entire day (30 minutes jump).
            // for (int hour = 0; hour < 24; hour++)
            // {
            //     for (int minute = 0; minute < 60; minute += 30)
            //     {
            //         string time = $"{hour:D2}:{minute:D2}";
            //         int seconds = hour * 3600 + minute * 60;
            //         timeToSeconds[time] = seconds;
            //     }
            // }

            // Adding entries for the entire day (30 minutes jump).
            for (int hour = 0; hour < 24; hour++)
            {
                for (int minute = 0; minute < 60; minute += 1)
                {
                    string time = $"{hour:D2}:{minute:D2}";
                    int seconds = hour * 3600 + minute * 60;
                    timeToSeconds[time] = seconds;
                }
            }
        }
        /// <summary>
        /// Converts a time string value to int seconds.
        /// </summary>
        /// <param name="time">The time value in the format "HH:mm".</param>
        /// <returns>The corresponding number of seconds.</returns>
        public int ConvertToSeconds(string time)
        {
            if (timeToSeconds.TryGetValue(time, out int seconds))
            {
                return seconds;
            }
            else
            {
                // Handle invalid time format or unknown time
                Debug.Log("Time Frame Not Exist...");
                return 0;
            }
        }
        #endregion
    }
}

// +-------+---------+
// | Time  | Seconds |
// +-------+---------+
// | 00:00 |    0    |
// | 00:30 | 1800    |
// | 01:00 | 3600    |
// | 01:30 | 5400    |
// | 02:00 | 7200    |
// | 02:30 | 9000    |
// | 03:00 | 10800   |
// | 03:30 | 12600   |
// | 04:00 | 14400   |
// | 04:30 | 16200   |
// | 05:00 | 18000   |
// | 05:30 | 19800   |
// | 06:00 | 21600   |
// | 06:30 | 23400   |
// | 07:00 | 25200   |
// | 07:30 | 27000   |
// | 08:00 | 28800   |
// | 08:30 | 30600   |
// | 09:00 | 32400   |
// | 09:30 | 34200   |
// | 10:00 | 36000   |
// | 10:30 | 37800   |
// | 11:00 | 39600   |
// | 11:30 | 41400   |
// | 12:00 | 43200   |
// | 12:30 | 45000   |
// | 13:00 | 46800   |
// | 13:30 | 48600   |
// | 14:00 | 50400   |
// | 14:30 | 52200   |
// | 15:00 | 54000   |
// | 15:30 | 55800   |
// | 16:00 | 57600   |
// | 16:30 | 59400   |
// | 17:00 | 61200   |
// | 17:30 | 63000   |
// | 18:00 | 64800   |
// | 18:30 | 66600   |
// | 19:00 | 68400   |
// | 19:30 | 70200   |
// | 20:00 | 72000   |
// | 20:30 | 73800   |
// | 21:00 | 75600   |
// | 21:30 | 77400   |
// | 22:00 | 79200   |
// | 22:30 | 81000   |
// | 23:00 | 82800   |
// | 23:30 | 84600   |
// +-------+---------+