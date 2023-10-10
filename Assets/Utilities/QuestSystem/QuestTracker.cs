using System.Collections.Generic;
using System.Diagnostics;
using Unitylities;
using UnityEngine.InputSystem;

namespace Unitylities
{
    public class QuestTracker
    {
        #region Fields
        private Dictionary<Quest, List<QuestObjective>> questProgress;
        #endregion

        #region Properties
        public Dictionary<Quest, List<QuestObjective>> QuestProgress => questProgress;
        #endregion

        #region Constructor
        public QuestTracker()
        {
            questProgress = new Dictionary<Quest, List<QuestObjective>>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add a new quest to the quest tracker.
        /// </summary>
        /// <param name="quest">The quest to add.</param>
        /// <returns>True if the quest was successfully added; otherwise, false.</returns>
        private bool Add(Quest quest)
        {
            if (!IsQuestExist(quest))
            {
                quest.Activate();

                questProgress.Add(quest, quest.Objectives);

                return IsQuestExist(quest) && quest.IsActive;
            }
            return false;
        }

        /// <summary>
        /// Remove a quest from the quest tracker.
        /// </summary>
        /// <param name="quest">The quest to remove.</param>
        /// <returns>True if the quest was successfully removed; otherwise, false.</returns>
        private bool Remove(Quest quest)
        {
            if (IsQuestExist(quest))
            {
                return questProgress.Remove(quest);
            }
            return false;
        }

        /// <summary>
        /// Check if a quest exists in the quest tracker.
        /// </summary>
        /// <param name="quest">The quest to check.</param>
        /// <returns>True if the quest exists; otherwise, false.</returns>
        private bool IsQuestExist(Quest quest)
        {
            return questProgress.ContainsKey(quest);
        }

        /// <summary>
        /// Check if an objective exists in a list of objectives.
        /// </summary>
        /// <param name="objectives">The list of objectives to check.</param>
        /// <param name="objective">The objective to check.</param>
        /// <returns>True if the objective exists in the list; otherwise, false.</returns>
        private bool IsObjectiveExist(List<QuestObjective> objectives, QuestObjective objective)
        {
            return objectives.Contains(objective);
        }

        /// <summary>
        /// Update a quest objective's status.
        /// </summary>
        /// <param name="quest">The quest containing the objective.</param>
        /// <param name="objective">The objective to update.</param>
        /// <returns>True if the objective was successfully updated; otherwise, false.</returns>
        private bool CompleteObjective(Quest quest, QuestObjective objective)
        {
            List<QuestObjective> objectives = questProgress[quest];

            if (IsQuestExist(quest) && IsObjectiveExist(objectives, objective))
            {
                if (!objective.IsCompleted && objectives.Remove(objective))
                {

                    objective.Complete();

                    objectives.Add(objective);

                    return IsObjectiveExist(objectives, objective);
                }

                return false;
            }

            return false;
        }

        /// <summary>
        /// Check if all objectives within a quest are completed.
        /// </summary>
        /// <param name="quest">The quest to check.</param>
        /// <returns>True if all objectives are completed; otherwise, false.</returns>
        private bool IsAllObjectivesCompleted(Quest quest)
        {
            List<QuestObjective> objectives = questProgress[quest];

            if (IsQuestExist(quest) && objectives.Count > 0)
            {
                foreach (QuestObjective objective in objectives)
                {
                    if (!objective.IsCompleted)
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if a quest is completed and update its status in the quest tracker.
        /// </summary>
        /// <param name="quest">The quest to check.</param>
        /// <returns>True if the quest was successfully completed; otherwise, false.</returns>
        private bool IsQuestCompleted(Quest quest)
        {
            if (IsQuestExist(quest) && IsAllObjectivesCompleted(quest))
            {
                if (Remove(quest))
                {
                    quest.Complete();

                    questProgress.Add(quest, quest.Objectives);

                    return IsQuestExist(quest) && !quest.IsActive;
                }

                return false;
            }
            return false;
        }

        /// <summary>
        /// Activate a quest by adding it to the quest tracker.
        /// </summary>
        /// <param name="quest">The quest to activate.</param>
        /// <returns>True if the quest was successfully activated; otherwise, false.</returns>
        public bool ActivateQuest(Quest quest)
        {
            return Add(quest);
        }

        /// <summary>
        /// Complete a quest and update its status in the quest tracker.
        /// </summary>
        /// <param name="quest">The quest to complete.</param>
        /// <returns>True if the quest was successfully completed; otherwise, false.</returns>
        public bool UpdateObjective(Quest quest, QuestObjective objective)
        {
            return CompleteObjective(quest, objective);
        }

        /// <summary>
        /// Complete a quest and update its status in the quest tracker.
        /// </summary>
        /// <param name="quest">The quest to complete.</param>
        /// <returns>The completed quest if successful; otherwise, null.</returns>
        public Quest CompleteQuest(Quest quest)
        {
            if (IsQuestCompleted(quest))
            {
                foreach ((Quest k, List<QuestObjective> v) in questProgress)
                {
                    return (k == quest) ? k : null;
                }
            }

            return null;
        }
    }
    #endregion
}