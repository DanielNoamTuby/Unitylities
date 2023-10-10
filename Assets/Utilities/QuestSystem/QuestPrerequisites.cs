using Unitylities.DNTEnums;

namespace Unitylities
{

    public class QuestPrerequisite
    {
        #region Fields
        private string description;
        private PrerequisiteType type;
        private bool isCompleted;
        #endregion

        #region Properties
        public string Description => description; // A description of the prerequisite.
        public PrerequisiteType Type => type;
        public bool IsCompleted => isCompleted; // A flag indicating whether the prerequisite is completed.
        #endregion

        #region Constructor
        public QuestPrerequisite(string description, PrerequisiteType type, bool isCompleted)
        {
            this.description = description;
            this.type = type;
            this.isCompleted = isCompleted;
        }
        #endregion
    }
}