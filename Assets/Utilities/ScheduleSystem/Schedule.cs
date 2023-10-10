namespace Unitylities
{
    public class Schedule
    {
        #region Fields
        private string name;
        private string description;
        private string startTime;
        private string endTime;
        private bool isRepeatable;
        private bool isActive;
        #endregion

        #region Properties
        public string Name => name;
        public string Description => description;
        public string StartTime => startTime;
        public string EndTime => endTime;
        public bool IsRepeatable => isRepeatable;
        public bool IsActive => isActive;
        #endregion

        #region Constructor
        public Schedule(string name, string description, string startTime, string endTime, bool isRepeatable, bool isActive)
        {
            this.name = name;
            this.description = description;
            this.startTime = startTime;
            this.endTime = endTime;
            this.isRepeatable = isRepeatable;
            this.isActive = isActive;
        }
        #endregion
    }
}