using UnityEngine;
using System.Collections;
using Unitylities;
using System;

namespace Unitylities
{
    public class Potion : IPotion
    {
        private PotionSize size;
        private int amount;

        public PotionSize Size { get => size; }
        public int Amount { get => amount; }

        public event EventHandler OnPotionConsumed;

        public Potion(PotionSize size)
        {
            this.size = size;

            amount = (int)size;
        }
        public void Use()
        {
            amount = 0;

            OnPotionConsumed?.Invoke(this, EventArgs.Empty);
        }
    }
}
