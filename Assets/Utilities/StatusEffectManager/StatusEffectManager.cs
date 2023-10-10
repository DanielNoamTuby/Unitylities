// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using Unitylities;

// namespace Unitylities
// {
//     public class StatusEffectManager
//     {
//         private List<StatusEffect> activeEffects;

//         public event EventHandler OnStatusEffectEnded;

//         public StatusEffectManager()
//         {
//             activeEffects = new List<StatusEffect>();
//         }


//         public void ActivateStatusEffect(StatusEffect effect)
//         {
//             activeEffects.Add(effect);
//             // Optionally, you can update the UI to display active status effects
//         }

//         public void UpdateStatusEffects(Player player)
//         {
//             for (int i = activeEffects.Count - 1; i >= 0; i--)
//             {
//                 StatusEffect effect = activeEffects[i];

//                 effect.ApplyEffect(player);

//                 Debug.Log($"{effect.Name} Ends In: {effect.Duration} Seconds With Amount: {effect.Amount}.");

//                 effect.Duration--;

//                 if (effect.Duration == 0)
//                 {
//                     OnStatusEffectEnded?.Invoke(this, EventArgs.Empty);

//                     activeEffects.RemoveAt(i);
//                 }
//             }
//         }

//         public int GetActiveStatusEffects()
//         {
//             return activeEffects.Count;
//         }
//         public void DisplayStats()
//         {
//             Debug.Log($"Status Effect Manager Stats: Active Effects: {activeEffects.Count}");
//         }
//     }
// }