// using System;
// using Unitylities;

// namespace Unitylities
// {
//     public abstract class StatusEffect : IStatusEffect
//     {
//         private string name;
//         private int duration;
//         private int amount;
//         private bool isBad;


//         public string Name { get => name; }
//         public int Duration { get => duration; set => duration = value; }
//         public int Amount { get => amount; }
//         public bool IsBad { get => isBad; }

//         public StatusEffect(string name, int duration, int amount, bool isBad)
//         {
//             this.name = name;
//             this.duration = duration;
//             this.amount = amount;
//             this.isBad = isBad;
//         }

//         public virtual void ApplyEffect(Player player)
//         {

//         }
//     }
// }