using Unitylities.DNTEnums;

namespace Unitylities
{
    public class GameEventReward
    {
        #region Fields
        private RewardType type;
        private int amount;
        #endregion

        #region Properties
        public RewardType Type => type; // A description of the objective.
        public int Amount => amount; // A flag indicating whether the objective is completed.
        #endregion

        #region Constructor
        public GameEventReward(RewardType type, int amount)
        {
            this.type = type;
            this.amount = amount;
        }
        #endregion
    }
}