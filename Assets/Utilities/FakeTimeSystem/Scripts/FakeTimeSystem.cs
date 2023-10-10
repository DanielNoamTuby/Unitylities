using System;
using UnityEngine;
using Unitylities.FakeTimeEnums;

namespace Unitylities
{
    /// <summary>
    /// Represents a fake time system for simulating in-game time in Unity.
    /// <br /> Version: 1.0.0
    /// </summary>

    public class FakeTimeSystem
    {
        #region Fields
        /// <summary>
        /// Length of a day in seconds (24 hours = 86400f seconds).
        /// </summary>
        private const float DAY_LENGTH = 86400f;
        /// <summary>
        /// The speed at which the in-game clock advances (30 real-time seconds = 1 hour in-game).
        /// </summary>
        private const float CLOCK_SPEED = 120.0f; // 30 real-time seconds = 1 hour in-game.
        private float currentTime; // Up to 86400 (24 hours) seconds.
        private int currentSecond; // Up to 60 seconds.
        private int currentMinute; // Up to 60 minutes.
        private int currentHour; // Up to 24 hours.
        private Day currentDay; // Sun - Sat.
        private int currentWeek; // 1 - 4.
        private Month currentMonth; // Jan - Dec.
        private int currentYear; // Unlimited.
        private Season currentSeason; // Winter, Summer, Spring, Autumn.
        private bool isFastForward;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the length of a day in seconds (24 hours = 86400 seconds).
        /// </summary>
        public float DayLength => DAY_LENGTH;
        /// <summary>
        /// Gets the speed at which the in-game clock advances (30 real-time seconds = 1 hour in-game).
        /// </summary>
        public float ClockSpeed => CLOCK_SPEED;
        /// <summary>
        /// Gets the current in-game time in seconds.
        /// </summary>
        public float CurrentTime => currentTime;
        /// <summary>
        /// Gets the current second (0 - 59).
        /// </summary>
        public int CurrentSecond => currentSecond;
        /// <summary>
        /// Gets the current minute (0 - 59).
        /// </summary>
        public int CurrentMinute => currentMinute;
        /// <summary>
        /// Gets the current hour (0 - 23).
        /// </summary>
        public int CurrentHour => currentHour;
        /// <summary>
        /// Gets the current day of the week (Sun - Sat).
        /// </summary>
        public Day CurrentDay => currentDay;
        /// <summary>
        /// Gets the current week of the month (1 - 4).
        /// </summary>
        public int CurrentWeek => currentWeek;
        /// <summary>
        /// Gets the current month (Jan - Dec).
        /// </summary>
        public Month CurrentMonth => currentMonth;
        /// <summary>
        /// Gets the current year (unlimited).
        /// </summary>
        public int CurrentYear => currentYear;
        /// <summary>
        /// Gets the current season (Winter, Summer, Spring, or Autumn).
        /// </summary>
        public Season CurrentSeason => currentSeason;
        /// <summary>
        /// Gets a value indicating whether it's currently daytime in the game.
        /// </summary>
        public bool IsDaytime => CurrentTime < DayLength / 2;
        /// <summary>
        /// Gets a value indicating whether it's currently fast forward in the game.
        /// </summary>
        public bool IsFastForward => isFastForward;

        #endregion

        #region Events
        /// <summary>
        /// Occurs when the in-game minute or hour changes.
        /// </summary>
        public event EventHandler OnMinuteHourChanged;
        /// <summary>
        /// Occurs when the in-game day changes.
        /// </summary>
        public event EventHandler OnDayChanged;
        /// <summary>
        /// Occurs when the in-game week changes.
        /// </summary>
        public event EventHandler OnWeekChanged;
        /// <summary>
        /// Occurs when the in-game month changes.
        /// </summary>
        public event EventHandler OnMonthChanged;
        /// <summary>
        /// Occurs when the in-game year changes.
        /// </summary>
        public event EventHandler OnYearChanged;
        /// <summary>
        /// Occurs when the in-game season changes.
        /// </summary>
        public event EventHandler OnSeasonChanged;

        /// <summary>
        /// Provides event data for the <see cref=" OnMinuteHourChanged"/> event.
        /// </summary>
        public class OnMinuteHourChangedEventArgs : EventArgs
        {
            /// <summary>
            /// Gets or sets the current second (0 - 59).
            /// </summary>
            public int currentSecond;

            /// <summary>
            /// Gets or sets the current minute (0 - 59).
            /// </summary>
            public int currentMinute;

            /// <summary>
            /// Gets or sets the current hour (0 - 23).
            /// </summary>
            public int currentHour;
        }
        /// <summary>
        /// Provides event data for the <see cref="OnDayChanged"/> event.
        /// </summary>
        public class OnDayChangedEventArgs : EventArgs
        {
            /// <summary>
            /// Gets or sets the current day of the week (Sun - Sat).
            /// </summary>
            public Day currentDay;
        }
        /// <summary>
        /// Provides event data for the <see cref="OnWeekChanged"/> event.
        /// </summary>
        public class OnWeekChangedEventArgs : EventArgs
        {
            /// <summary>
            /// Gets or sets the current week of the month (1 - 4).
            /// </summary>
            public int currentWeek;
        }
        /// <summary>
        /// Provides event data for the <see cref="OnMonthChanged"/> event.
        /// </summary>
        public class OnMonthChangedEventArgs : EventArgs
        {
            /// <summary>
            /// Gets or sets the current month (Jan - Dec).
            /// </summary>
            public Month currentMonth;
        }
        /// <summary>
        /// Provides event data for the <see cref="OnYearChanged"/> event.
        /// </summary>
        public class OnYearChangedEventArgs : EventArgs
        {
            /// <summary>
            /// Gets or sets the current year (unlimited).
            /// </summary>
            public int currentYear;
        }
        /// <summary>
        /// Provides event data for the <see cref="OnSeasonChanged"/> event.
        /// </summary>
        public class OnSeasonChangedEventArgs : EventArgs
        {
            /// <summary>
            /// Gets or sets the current season (Winter, Summer, Spring, or Autumn).
            /// </summary>
            public Season currentSeason;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeTimeSystem"/> class with default values.
        /// </summary>
        public FakeTimeSystem()
        {
            currentTime = 0f; // 00:00 AM
            currentDay = Day.Sunday;
            currentWeek = 0;
            currentMonth = Month.January;
            currentYear = 0;
            currentSeason = GetSeasonForMonth(currentMonth);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeTimeSystem"/> class with specified initial values.
        /// </summary>
        /// <param name="currentTime">The initial in-game time in seconds.</param>
        /// <param name="currentDay">The initial day of the week.</param>
        /// <param name="currentWeek">The initial week of the month.</param>
        /// <param name="currentMonth">The initial month.</param>
        /// <param name="currentYear">The initial year.</param>
        public FakeTimeSystem(float currentTime, Day currentDay, int currentWeek, Month currentMonth, int currentYear)
        {
            this.currentTime = currentTime;
            this.currentDay = currentDay;
            this.currentWeek = currentWeek;
            this.currentMonth = currentMonth;
            this.currentYear = currentYear;
            currentSeason = GetSeasonForMonth(this.currentMonth);
        }
        #endregion

        #region Methods
        // Private
        /// <summary>
        /// Handles the progression of in-game time and related changes.
        /// </summary>
        private void HandleTimeMovement()
        {
            if (currentTime >= DAY_LENGTH)
            {
                HandleDayChange();

                currentTime -= DAY_LENGTH;
            }

            HandleSecondMinuteHourChanged();

            // FormatFullCurrentTime();

        }
        /// <summary>
        /// Handles the change of the in-game day.
        /// </summary>
        private void HandleDayChange()
        {
            // Cycle to Sunday or go to next day.
            if (currentDay == Day.Saturday)
            {
                currentDay = Day.Sunday;
                OnDayChanged?.Invoke(this, new OnDayChangedEventArgs { currentDay = currentDay });

                HandleWeekChange();
            }
            else
            {
                currentDay++;
                OnDayChanged?.Invoke(this, new OnDayChangedEventArgs { currentDay = currentDay });
            }
        }
        /// <summary>
        /// Handles the change of the in-game week.
        /// </summary>
        private void HandleWeekChange()
        {
            if (currentWeek == 4)
            {
                currentWeek = 1;
                OnWeekChanged?.Invoke(this, new OnWeekChangedEventArgs { currentWeek = currentWeek });

                HandleMonthChange();
            }
            else
            {
                currentWeek++;
                OnWeekChanged?.Invoke(this, new OnWeekChangedEventArgs { currentWeek = currentWeek });
            }
        }
        /// <summary>
        /// Handles the change of the in-game month.
        /// </summary>
        private void HandleMonthChange()
        {
            if (currentMonth == Month.December)
            {
                currentMonth = Month.January;
                OnMonthChanged?.Invoke(this, new OnMonthChangedEventArgs { currentMonth = currentMonth });

                HandleSeasonChanged();

                HandleYearChanged();
            }
            else
            {
                currentMonth++;
                OnMonthChanged?.Invoke(this, new OnMonthChangedEventArgs { currentMonth = currentMonth });

                HandleSeasonChanged();
            }
        }
        /// <summary>
        /// Handles the change of the in-game season.
        /// </summary>
        private void HandleSeasonChanged()
        {
            currentSeason = GetSeasonForMonth(currentMonth);
            OnSeasonChanged?.Invoke(this, new OnSeasonChangedEventArgs { currentSeason = currentSeason });
        }
        /// <summary>
        /// Handles the change of the in-game year.
        /// </summary>
        private void HandleYearChanged()
        {
            currentYear++;
            OnYearChanged?.Invoke(this, new OnYearChangedEventArgs { currentYear = currentYear });
        }
        /// <summary>
        /// Handles changes in the in-game second, minute, and hour.
        /// </summary>
        private void HandleSecondMinuteHourChanged()
        {
            int prevMinute = currentMinute;
            int prevHour = currentHour;

            currentSecond = (int)currentTime % 60;
            currentMinute = (int)currentTime % 3600 / 60;
            currentHour = (int)currentTime / 3600;

            if (currentMinute != prevMinute || currentHour != prevHour) OnMinuteHourChanged?.Invoke(this, new OnMinuteHourChangedEventArgs
            {
                currentSecond = currentSecond,
                currentMinute = currentMinute,
                currentHour = currentHour
            });
        }
        /// <summary>
        /// Formats the full current time as a string for debugging.
        /// </summary>
        /// <returns>string - The formatted time string.</returns>
        private string FormatFullCurrentTime()
        {
            string formattedTime = string.Format("Year: {0:D2}, Month: {1}, Week: {2:D2}, Day: {3}, Time: {4:D2}:{5:D2}:{6:D2}, Season: {7}", CurrentYear, CurrentMonth.ToString(), CurrentWeek, CurrentDay.ToString(), CurrentHour, CurrentMinute, CurrentSecond, CurrentSeason);

            Debug.Log(formattedTime);

            return formattedTime;
        }
        /// <summary>
        /// Gets the season for a given month.
        /// </summary>
        /// <param name="month">The month to determine the season for.</param>
        /// <returns>The corresponding season (Winter, Summer, Spring, or Autumn).</returns>
        private Season GetSeasonForMonth(Month month)
        {
            switch (month)
            {
                case Month.December:
                case Month.January:
                case Month.February:
                    return Season.Winter;
                case Month.March:
                case Month.April:
                case Month.May:
                    return Season.Spring;
                case Month.June:
                case Month.July:
                case Month.August:
                    return Season.Summer;
                case Month.September:
                case Month.October:
                case Month.November:
                    return Season.Autumn;
                default:
                    return Season.Winter;
            }
        }

        // Public
        /// <summary>
        /// Updates the in-game time based on the specified deltaTime.
        /// </summary>
        /// <param name="deltaTime">The time interval in seconds to advance the in-game time (Time.deltaTime).</param>
        public void UpdateTime(float deltaTime)
        {
            // Ensure the time to advance is non-negative.
            if (deltaTime < 0f)
            {
                Debug.LogWarning("Cannot advance time by a negative duration.");
                return;
            }

            currentTime += deltaTime * CLOCK_SPEED;

            // currentTime = Mathf.Clamp(currentTime, 0f, DAY_LENGTH);

            HandleTimeMovement();

            FormatFullCurrentTime();
        }
        /// <summary>
        /// Advances the in-game time by the specified duration in seconds. 
        /// <br /> Examples:
        /// <br />1. day = 86400f;
        /// <br />2. week = 604800f;
        /// <br />3. month = 2419200f;
        /// <br />4. year = 29030400f;
        /// </summary>
        /// <param name="timeToAdvanceInSeconds"> The time duration in seconds to advance. </param>
        public void AdvanceTime(float timeToAdvanceInSeconds)
        {
            if (timeToAdvanceInSeconds < 0f)
            {
                Debug.LogWarning("Cannot advance time by a negative duration.");
                return;
            }

            currentTime += timeToAdvanceInSeconds;

            while (currentTime >= DayLength)
            {
                HandleDayChange();

                currentTime -= DAY_LENGTH;
            }

            HandleSecondMinuteHourChanged();
        }
        /// <summary>
        /// Formats the current time as a string for display purposes.
        /// </summary>
        /// <returns>The formatted time string.</returns>
        public string FormatCurrentTime()
        {
            string formattedTime = string.Format("{0}, {1:D2}:{2:D2}", CurrentDay.ToString()[..3], CurrentHour, CurrentMinute);

            // Debug.Log(formattedTime);

            return formattedTime;
        }
        #endregion
    }
}