using Fall2020_CSC403_Project.code;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable 1591 // use this to disable comment warnings

    public class HarderBattleCharacter : Character 
    {
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public int Magic { get; private set; }
        public int MaxMagic { get; private set; }
        public int CurrentStrength { get; private set; }
        public int Strength { get; private set; }
        public int EnemyStrength { get; private set; }

        public event Action<int> AttackEvent;

        public event Action<int> HealEvent;

        public event Action<int> Strength_UpEvent;






        public HarderBattleCharacter(Vector2 initPos, Collider collider) : base(initPos, collider)
        {
            MaxHealth = 10;
            MaxMagic = 20;
            CurrentStrength = 2;
            Strength = CurrentStrength;
            EnemyStrength = 2;
            Health = MaxHealth;
            Magic = MaxMagic;
        }

        public void OnAttack(int amount)
        {
            AttackEvent((int)(amount * Strength));
        }

        public void OnEnemyAttack(int amount)
        {
            AttackEvent((int)(amount * EnemyStrength));
        }

        public void OnHeal(int amount)
        {
            HealEvent((int)(amount));
        }

        public void OnStrength_Up(int amount)
        {
            Strength_UpEvent((int)(amount));
        }

        public void AlterHealth(int amount)
        {
            Health += amount;
        }

        public void AlterMagic(int amount)
        {
            Magic += amount;
        }

        public void AlterStrength(int amount)
        {
            Strength += amount;
        }

        public void ChangeStrengthBack()
        {
            Strength = 2;
        }
    }
