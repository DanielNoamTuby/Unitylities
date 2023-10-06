using System;
using System.Collections.Generic;
using Unitylities.QuestEnums;
using Unitylities.FakeTimeUtils;

namespace Unitylities
{
    public class QuestSystem
    {
        #region Fields
        private List<Quest> quests;
        private QuestTracker tracker;
        #endregion

        #region Properties
        public List<Quest> Quests => quests;
        public QuestTracker Tracker => tracker;
        #endregion

        #region Constructor
        public QuestSystem()
        {
            quests = new List<Quest>();
            tracker = new QuestTracker();
        }
        #endregion

        #region Methods
        public void Add(Quest quest)
        {
            quests.Add(quest);
        }
        public void Remove(Quest quest)
        {
            quests.Remove(quest);
        }
        public List<Quest> GetAll()
        {
            List<Quest> list = new List<Quest>();

            if (quests.Count > 0)
            {
                foreach (Quest q in quests)
                {
                    list.Add(q);
                }

                return list;
            }

            return list;
        }
        public List<Quest> GetActive()
        {
            List<Quest> list = new List<Quest>();

            if (quests.Count > 0)
            {
                foreach (Quest q in quests)
                {
                    if (q.IsActive && !q.IsCompleted)
                    {
                        list.Add(q);
                    }
                }

                return list;
            }

            return list;
        }
        public List<Quest> HandleCurrentQuests(float currentTime, TimeConverter tc)
        {
            List<Quest> activeQuests = GetActive();

            List<Quest> currentQuests = new List<Quest>();

            if (activeQuests.Count > 0)
            {
                foreach (Quest q in activeQuests)
                {
                    int start = tc.ConvertToSeconds(q.StartTime);
                    int end = tc.ConvertToSeconds(q.EndTime);

                    if (currentTime >= start || currentTime <= end)
                    {
                        currentQuests.Add(q);
                    }
                }

                return currentQuests;
            }

            return currentQuests;
        }
        public List<Quest> GetQuestsByType(QuestType questType)
        {
            return quests.FindAll(q => q.Type == questType);
        }
        public bool IsMainQuest(Quest quest)
        {
            return quest.Type == QuestType.Main;
        }
        public void UpdateQuestObjective(Quest quest, QuestObjective objective)
        {
            tracker.UpdateObjective(quest, objective);
        }
        public bool AcceptQuest(Quest quest)
        {
            return tracker.ActivateQuest(quest);
        }
        public bool CompleteQuest(Quest quest)
        {
            Quest completedQuest = tracker.CompleteQuest(quest);

            if (completedQuest != null)
            {
                quests.Find(quest => quests.Remove(quest));
                quests.Add(completedQuest);
                return quests.Contains(completedQuest);
            }
            return false;
        }
        #endregion
    }
}