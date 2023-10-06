using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unitylities
{
    public class HealthSystem
    {
        #region Fields
        private int health;
        private int shield;
        private int maxHealth;
        private int maxShield;
        #endregion

        #region Properties
        public int Health { get => health; }
        public int Shield { get => shield; }
        public int MaxHealth { get => maxHealth; }
        public int MaxShield { get => maxShield; }

        public float HealthNormalized { get => (float)health / maxHealth; }
        public float ShieldNormalized { get => (float)shield / maxShield; }

        public bool IsMaxHealth
        {
            get
            {
                if (health >= maxHealth)
                {
                    health = maxHealth;
                    return true;
                }
                return false;
            }
        }
        public bool IsMaxShield
        {
            get
            {
                if (maxShield != 0 && shield >= maxShield)
                {
                    shield = maxShield;
                    return true;
                }
                return false;
            }
        }

        public bool IsShieldBroken { get => shield <= 0; }
        public bool IsDead { get => health <= 0; }
        #endregion

        #region Events
        public event EventHandler OnHeal;
        public event EventHandler OnShieldChanged;

        public event EventHandler OnMaxShieldChanged;
        public event EventHandler OnMaxHealthChanged;

        public event EventHandler OnDamaged;
        public event EventHandler OnFullHealth;
        public event EventHandler OnFullShield;
        public event EventHandler OnShieldBroke;
        public event EventHandler OnDead;
        #endregion

        #region Constructors
        public HealthSystem(int maxHealth)
        {
            this.maxHealth = maxHealth;
            health = maxHealth;

            maxShield = 0;
            shield = 0;
        }

        public HealthSystem(int maxHealth, int maxShield)
        {
            this.maxHealth = maxHealth;
            health = maxHealth;

            this.maxShield = maxShield;
            shield = maxShield;
        }
        #endregion

        #region Methods
        public void TakeDamage(int amount)
        {
            if (shield >= amount)
            {
                shield -= amount;

                shield = Mathf.Clamp(shield, 0, maxShield);

                OnShieldChanged?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                health -= (amount - shield);

                health = Mathf.Clamp(health, 0, maxHealth);

                OnDamaged?.Invoke(this, EventArgs.Empty);

                shield = 0;

                OnShieldChanged?.Invoke(this, EventArgs.Empty);

                OnShieldBroke?.Invoke(this, EventArgs.Empty);
            }

            if (IsDead)
            {
                health = 0;
                Die();
            }


        }

        public void Die()
        {
            OnDead?.Invoke(this, EventArgs.Empty);
        }

        public void GainHealth(int amount)
        {
            health += amount;

            health = Mathf.Clamp(health, 0, maxHealth);

            OnHeal?.Invoke(this, EventArgs.Empty);

            if (IsMaxHealth)
            {
                OnFullHealth?.Invoke(this, EventArgs.Empty);
            }

        }

        public void GainShield(int amount)
        {
            if (maxShield != 0 && !IsDead)
            {
                shield += amount;

                shield = Mathf.Clamp(shield, 0, maxShield);

                OnShieldChanged?.Invoke(this, EventArgs.Empty);

                if (IsMaxShield)
                {
                    OnFullShield?.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                Debug.Log("There is no shield !");
            }
        }

        public void IncreaseMaxHealth(int amount, bool fullHealth = false)
        {
            maxHealth += amount;

            OnMaxHealthChanged?.Invoke(this, EventArgs.Empty);

            if (fullHealth)
            {
                health = maxHealth;

                OnHeal?.Invoke(this, EventArgs.Empty);

                OnFullHealth?.Invoke(this, EventArgs.Empty);
            }


        }

        public void IncreaseMaxShield(int amount, bool fullShield = false)
        {
            maxShield += amount;

            OnMaxShieldChanged?.Invoke(this, EventArgs.Empty);

            if (fullShield)
            {
                shield = maxShield;

                OnShieldChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void DisplayStats()
        {
            Debug.Log($"Health System Stats: Health: {Health}, Max Health: {MaxHealth}, Shield: {Shield}, Max Shield: {MaxShield}, Health Normalized: {HealthNormalized}, Shield Normalized: {ShieldNormalized}, Is Max Health: {IsMaxHealth}, Is Max Shield: {IsMaxShield}, Is Shield Broken: {IsShieldBroken}, Is Dead: {IsDead}");
        }
        #endregion
    }
}