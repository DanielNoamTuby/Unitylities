// using System;
// using Unitylities;
// using UnityEngine;

// namespace Unitylities
// {
//     public class HealthStatusEffect : StatusEffect
//     {
//         public HealthStatusEffect(string name, int duration, int amount, bool isBad) : base(name, duration, amount, isBad)
//         {
//         }
//         public override void ApplyEffect(Player player)
//         {
//             if (this.IsBad)
//             {
//                 player.TakeDamage(Amount);
//             }
//             else
//             {
//                 player.GainHealth(Amount);
//             }
//         }

//     }
// }