namespace Unitylities.DNTEnums
{

    public enum RewardType
    {
        ExperiencePoints = 1,
        InGameCurrency,
        Item,
        AccessToArea,
    }

    public enum PrerequisiteType
    {
        None = 1, // No prerequisite
        QuestCompleted, // Requires another quest to be completed.
        ItemOwned, // Requires the player to possess a specific item.
        LevelRequirement, // Requires the player to reach a certain level.
                          // Add more prerequisite types as needed.
    }


}