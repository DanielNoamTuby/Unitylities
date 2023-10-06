// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// namespace Unitylities
// {
//     public class LevelSystemAnimated
//     {

//         #region Fields & Properties
//         private LevelSystem _levelSystem;
//         private bool isAnimating;
//         private float updateTimer;
//         private float updateTimerMax;

//         private int level;
//         private int experience;
//         #endregion

//         #region Events
//         public event EventHandler OnExperienceChanged;
//         public event EventHandler OnLevelChanged;
//         public event EventHandler OnMaxLevelChanged;
//         #endregion

//         public LevelSystemAnimated(LevelSystem levelSystem)
//         {
//             SetLevelSystem(levelSystem);
//             updateTimerMax = .010f;

//             DNT.Utils.FunctionUpdater.Create(() => Update());
//         }

//         public void SetLevelSystem(LevelSystem levelSystem)
//         {
//             this._levelSystem = levelSystem;

//             level = levelSystem.Level;
//             experience = levelSystem.Experience;

//             levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
//             levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
//             levelSystem.OnMaxLevelChanged += LevelSystem_OnMaxLevelChanged;
//         }

//         private void LevelSystem_OnMaxLevelChanged(object sender, EventArgs e)
//         {
//             isAnimating = true;
//         }

//         private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
//         {
//             isAnimating = true;
//         }

//         private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
//         {
//             isAnimating = true;
//         }

//         private void Update()
//         {
//             if (isAnimating)
//             {
//                 // Check if its time to update
//                 updateTimer += Time.deltaTime;
//                 while (updateTimer > updateTimerMax)
//                 {
//                     // Time to update
//                     updateTimer -= updateTimerMax;
//                     UpdateAddExperience();
//                 }
//             }
//         }

//         private void UpdateAddExperience()
//         {
//             if (level < _levelSystem.Level)
//             {
//                 // Local level under target level
//                 AddExperience();
//             }
//             else
//             {
//                 // Local level equals the target level
//                 if (experience < _levelSystem.Experience)
//                 {
//                     AddExperience();
//                 }
//                 else
//                 {
//                     isAnimating = false;
//                 }
//             }
//         }

//         private void AddExperience()
//         {
//             experience++;
//             if (experience >= _levelSystem.ExperienceToNextLevel)
//             {
//                 level++;
//                 experience = 0;
//                 if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
//             }
//             if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
//         }

//         public int GetLevelNumber()
//         {
//             return level;
//         }

//         public float GetExperienceNormalized()
//         {
//             if (_levelSystem.CompareLevelToMax(level))
//             {
//                 return 1f;
//             }
//             else
//             {
//                 return (float)experience / _levelSystem.GetExperienceNeededToLevel(level);
//             }
//         }

//     }
// }