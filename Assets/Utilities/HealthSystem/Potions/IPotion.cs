using System;
using Unitylities;
using UnityEditor;

namespace Unitylities
{
    public interface IPotion
    {
        public PotionSize Size { get; }

        public int Amount { get; }

        public event EventHandler OnPotionConsumed;

        public void Use();
    }
}