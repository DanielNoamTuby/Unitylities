using System.Collections.Generic;
using Unitylities.DNTEnums;
using Unitylities;

namespace Unitylities
{
    public class GameEvent
    {
        #region Fields
        private string name;
        private string description;
        private string startTime;
        private string endTime;
        private List<GameEventObjective> objectives;
        private List<GameEventPrerequisites> prerequisites;
        private List<GameEventReward> rewards;
        private bool isRepeatable;
        private bool isActive;
        #endregion

        #region Properties
        public string Name => name;
        public string Description => description;
        public string StartTime => startTime;
        public string EndTime => endTime;
        public List<GameEventObjective> Objectives => objectives;
        public List<GameEventPrerequisites> Prerequisites => prerequisites;
        public List<GameEventReward> Rewards => rewards;
        public bool IsRepeatable => isRepeatable;
        public bool IsActive => isActive;
        #endregion

        #region Constructor
        public GameEvent(string name, string description, string startTime, string endTime, List<GameEventObjective> objectives, List<GameEventPrerequisites> prerequisites, List<GameEventReward> rewards, bool isRepeatable, bool isActive)
        {
            this.name = name;
            this.description = description;
            this.startTime = startTime;
            this.endTime = endTime;
            this.prerequisites = prerequisites;
            this.rewards = rewards;
            this.isRepeatable = isRepeatable;
            this.isActive = isActive;
        }
        #endregion
    }
}