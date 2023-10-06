using System.Collections.Generic;
using Unitylities.QuestEnums;

namespace Unitylities
{
    public class Quest
    {
        #region Fields
        private string name;
        private string description;
        private string startTime;
        private string endTime;
        private QuestType type;
        private List<QuestObjective> objectives;
        private List<QuestPrerequisite> prerequisites;
        private List<QuestReward> rewards;
        private bool isRepeatable;
        private bool isActive;
        private bool isCompleted;

        #endregion

        #region Properties
        public string Name => name;
        public string Description => description;
        public string StartTime => startTime;
        public string EndTime => endTime;
        public QuestType Type => type;
        public List<QuestObjective> Objectives => objectives;
        public List<QuestPrerequisite> Prerequisites => prerequisites;
        public List<QuestReward> Rewards => rewards;
        public bool IsRepeatable => isRepeatable;
        public bool IsActive => isActive;
        public bool IsCompleted => isCompleted;

        #endregion

        #region Constructor
        public Quest(string name, string description, string startTime, string endTime, QuestType type, List<QuestObjective> objectives, List<QuestPrerequisite> prerequisites, List<QuestReward> rewards, bool isRepeatable)
        {
            this.name = name;
            this.description = description;
            this.startTime = startTime;
            this.endTime = endTime;
            this.type = type;
            this.objectives = objectives;
            this.prerequisites = prerequisites;
            this.rewards = rewards;
            this.isRepeatable = isRepeatable;
            this.isActive = false;
            this.isCompleted = false;

        }
        #endregion

        public void Activate()
        {
            isActive = true;
        }

        public void Complete()
        {
            isActive = false;
            isCompleted = true;
        }
    }
}