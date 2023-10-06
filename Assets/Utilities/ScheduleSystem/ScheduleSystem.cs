using System;
using System.Collections.Generic;
using UnityEngine;
using Unitylities.FakeTimeUtils;

namespace Unitylities
{
    public class ScheduleSystem
    {
        #region Fields
        private List<Schedule> schedules;
        #endregion

        #region Properties
        public List<Schedule> Schedules => schedules;
        #endregion

        #region Events
        public event EventHandler OnSchedule;
        #endregion

        #region Constructor
        public ScheduleSystem(List<Schedule> schedules)
        {
            this.schedules = schedules;
        }
        #endregion

        #region Methods
        public void Add(Schedule schedule)
        {
            this.schedules.Add(schedule);
        }
        public void Remove(Schedule schedule)
        {
            // Use a lambda expression to remove the event by name
            schedules.Remove(schedule);
        }
        public List<Schedule> GetAll()
        {
            List<Schedule> list = new List<Schedule>();

            if (schedules.Count > 0)
            {
                foreach (Schedule s in schedules)
                {
                    list.Add(s);
                }

                return list;
            }

            return list;
        }
        public List<Schedule> GetActive()
        {
            List<Schedule> list = new List<Schedule>();

            if (schedules.Count > 0)
            {
                foreach (Schedule s in schedules)
                {
                    if (s.IsActive)
                    {
                        list.Add(s);
                    }
                }

                return list;
            }

            return list;
        }
        public List<Schedule> HandleCurrentSchedule(float currentTime, TimeConverter tc)
        {
            List<Schedule> activeSchedule = GetActive();

            List<Schedule> currentSchedule = new List<Schedule>();

            if (activeSchedule.Count > 0)
            {
                foreach (Schedule s in activeSchedule)
                {
                    int start = tc.ConvertToSeconds(s.StartTime);
                    int end = tc.ConvertToSeconds(s.EndTime);

                    if (currentTime >= start || currentTime <= end)
                    {
                        currentSchedule.Add(s);
                    }
                }

                return currentSchedule;
            }

            return currentSchedule;
        }
        #endregion
    }
}