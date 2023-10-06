namespace Unitylities
{
    public class QuestObjective
    {
        #region Fields
        private string description;
        private bool isCompleted;
        #endregion

        #region Properties
        public string Description => description; // A description of the objective.
        public bool IsCompleted => isCompleted; // A flag indicating whether the objective is completed.
        #endregion

        #region Constructor
        public QuestObjective(string description, bool isCompleted)
        {
            this.description = description;
            this.isCompleted = isCompleted;
        }

        public void Complete()
        {
            isCompleted = true;
        }
        #endregion
    }
}