using System;
using Assets.Person.PersonStates;
using UnityEngine;

namespace Assets.Person
{
    public abstract class Person : MonoBehaviour, IDamagable
    {
        [SerializeField] private Sprite _sprite;

        [SerializeField] private PersonStateMachine _personStateMachine;

        private float _health;
        public float Healh => _health;
        public event Action Died;
        public Sprite Sprite => _sprite;

        private Weapon.Weapon _weapon;
        public Weapon.Weapon Weapon => _weapon;

        private Armor _armor;

        private MagicItem _magicItem;

        protected virtual void ConditionForDead()
        {
            if (_health <= 0)
            {
                Died?.Invoke();
                _health = 0;
            }
        }

        public PersonStateMachine PersonStateMachine => _personStateMachine;

        public void TakeDamage(DamageData damage)
        {
            CalculateDamageMultiplier(damage);
            ConditionForDead();
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