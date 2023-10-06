using System.Collections.Generic;
using UnityEngine;
using Unitylities.FakeTimeUtils;

namespace Unitylities
{
    public class GameEventSystem
    {
        #region Fields
        private List<GameEvent> events;
        #endregion

        #region Properties
        public List<GameEvent> Events => events;
        #endregion

        #region Constructor
        public GameEventSystem(List<GameEvent> events)
        {
            this.events = events;
        }
        #endregion

        #region Methods
        public void Add(GameEvent e)
        {
            events.Add(e);
        }
        public void Remove(GameEvent e)
        {
            events.Remove(e);
        }
        public List<GameEvent> GetAll()
        {
            List<GameEvent> list = new List<GameEvent>();

            if (events.Count > 0)
            {
                foreach (GameEvent e in events)
                {
                    list.Add(e);
                }

                return list;
            }

            return list;
        }
        public List<GameEvent> GetActive()
        {
            List<GameEvent> list = new List<GameEvent>();

            if (events.Count > 0)
            {
                foreach (GameEvent e in events)
                {
                    if (e.IsActive)
                    {
                        list.Add(e);
                    }
                }

                return list;
            }

            return list;
        }
        public List<GameEvent> HandleCurrentEvents(float currentTime, TimeConverter tc)
        {
            List<GameEvent> activeEvents = GetActive();

            List<GameEvent> currentEvents = new List<GameEvent>();

            if (activeEvents.Count > 0)
            {
                foreach (GameEvent e in activeEvents)
                {
                    int start = tc.ConvertToSeconds(e.StartTime);
                    int end = tc.ConvertToSeconds(e.EndTime);

                    if (currentTime >= start || currentTime <= end)
                    {
                        currentEvents.Add(e);
                    }
                }

                return currentEvents;
            }

            return currentEvents;
        }
        #endregion
    }
}