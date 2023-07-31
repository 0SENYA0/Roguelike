using System;
using Assets.Fight.Element;
using Assets.Interface;
using Assets.Person.PersonStates;
using Assets.Weapon;
using UnityEngine;

namespace Assets.Person
{
    public abstract class Unit : IDamagable
    {
        private float _health;
        private Sprite _sprite;
        private PersonStateMachine _personStateMachine;
        private Weapon.Weapon _weapon;
        private Armor _armor;
        private MagicItem _magicItem;

        public Unit(Sprite sprite, int health = 100, Weapon.Weapon weapon = null, Armor armor = null, MagicItem magicItem = null) =>
            _sprite = sprite;

        public event Action<Unit> Died;

        public float Healh => _health;
        public Sprite Sprite => _sprite;
        public Weapon.Weapon Weapon => _weapon;
        public IPersonStateMachine PersonStateMachine => _personStateMachine;
        public void TakeDamage(DamageData damage)
        {
            CalculateDamageMultiplier(damage);
            ConditionForDead();
        }

        protected virtual void ConditionForDead()
        {
            if (_health <= 0)
            {
                Died?.Invoke(this);
                _health = 0;
            }
        }

        protected virtual void CalculateDamageMultiplier(DamageData damage)
        {
            float damageMultiplier = damage.Value / (CalculateDamageModifier(damage) * damage.Value + (_armor.Body.Value + _armor.Head.Value));
            _health -= damageMultiplier * damage.Value;
        }

        private float CalculateDamageModifier(DamageData damage) =>
            ElementManager.GetDamageModifier(damage.Element, _armor.Body.DefendElement);
    }

    public interface IDamagable
    {
        void TakeDamage(DamageData damage);
    }

    public interface IAttack
    {
        void Attack();
    }
}