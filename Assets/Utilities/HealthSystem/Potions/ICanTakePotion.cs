using System;
using Unitylities;
using UnityEditor;

namespace Unitylities
{
    public interface ICanTakePotion
    {
        public void DrinkPotion(IPotion potion);
    }
}