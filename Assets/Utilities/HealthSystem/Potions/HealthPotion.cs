﻿using System;
using Unitylities;

namespace Unitylities
{
    public class HealthPotion : IPotion
    {
        private PotionSize size;
        private int amount;

        public PotionSize Size { get => size; }
        public int Amount { get => amount; }

        public event EventHandler OnPotionConsumed;

        public HealthPotion(PotionSize size)
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