using System;
using UnityEngine;

namespace Unitylities
{
    public class LevelSystem
    {
        #region Fields
        private int level;
        private int maxLevel;
        private int experience;
        private int experienceMultiplier;
        #endregion

        #region Properties
        //public int Strength { get; set; }
        //public int Agility { get; set; }
        //public int Intelligence { get; set; }
        //public int SkillPoints { get; set; }
        public int Level { get => level; }
        public int MaxLevel { get => maxLevel; }
        public int Experience { get => experience; }
        public int ExperienceMultiplier { get => experienceMultiplier; }
        public float ExperienceNormalized
        {
            get
            {
                if (IsMaxLevel)
                {
                    return 1f;
                }
                else
                {
                    return (float)experience / ExperienceToNextLevel;
                }
            }
        }
        public int ExperienceToNextLevel { get => level * experienceMultiplier; }
        public bool IsMaxLevel
        {
            get
            {
                if (maxLevel != 0 && level == maxLevel)
                {
                    Debug.Log("Max Level Reached: " + maxLevel);
                    return true;
                }
                return false;
            }
        }
        #endregion

        #region Events
        public event EventHandler OnExperienceChanged;
        public event EventHandler OnLevelChanged;
        public event EventHandler OnMaxLevelChanged;
        #endregion

        #region Constructor
        public LevelSystem(int level = 1, int maxLevel = 10, int experience = 0, int experienceMultiplier = 100)
        {
            this.level = level;
            this.maxLevel = maxLevel;
            this.experience = experience;
            this.experienceMultiplier = experienceMultiplier;
        }
        #endregion

        #region Methods

        #region Private
        private void LevelUp()
        {
            while (!IsMaxLevel && experience >= ExperienceToNextLevel)
            {
                experience -= ExperienceToNextLevel;

                // experience = Mathf.Clamp(experience, 0, ExperienceToNextLevel);

                level++;

                level = Mathf.Clamp(level, 0, maxLevel);

                OnLevelChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Public
        public void AddExperience(int amount)
        {
            //if (!IsMaxLevel)
            //{
            experience += amount;

            // experience = Mathf.Clamp(experience, 0, ExperienceToNextLevel);

            OnExperienceChanged?.Invoke(this, EventArgs.Empty);

            LevelUp();
            //}
        }

        public int GetExperienceNeededToLevel(int level)
        {
            int amount = 0;

            for (int i = 1; i < level; i++)
            {
                amount += i * experienceMultiplier;
            }

            return amount;
        }

        public void SetNewMaxLevel(int byAmount)
        {
            if (IsMaxLevel)
            {
                maxLevel += byAmount;

                LevelUp();

                OnMaxLevelChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool CompareLevelToMax(int level)
        {
            if (level == maxLevel)
            {
                return true;
            }
            return false;
        }

        public void DisplayStats()
        {
            Debug.Log($"Level System Stats: Level: {Level}, Max Level: {MaxLevel}, Is Max Level: {IsMaxLevel}, Experience: {Experience}, Experience Multiplier: {ExperienceMultiplier}, Experience To Next Level: {ExperienceToNextLevel}, Experience Normalized: {ExperienceNormalized}");
        }
        #endregion
        //private void LevelUp()
        //{


        //    // Increase attributes when leveling up
        //    Strength += 2;
        //    Agility += 1;
        //    Intelligence += 3;
        //    MaxHealth += 20;
        //    Health = MaxHealth;

        //    // Gain skill points when leveling up
        //    SkillPoints += 2;
        //}
        #endregion


    }
}