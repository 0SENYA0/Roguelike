using System;
using Assets.Fight.Element;
using Assets.Interface;
using Assets.Person.DefendItems;
using Assets.Weapon;

namespace Assets.Person
{
    public abstract class Unit : IDamagable
    {
        private float _health;
        private IPersonStateMachine _personStateMachine;
        private IWeapon _weapon;
        private Armor _armor;
        private MagicItem _magicItem;

        public Unit(int health, IWeapon weapon, Armor armor, MagicItem magicItem, IPersonStateMachine personStateMachine)
        {
            _health = health;
            _weapon = weapon;
            _armor = armor;
            _magicItem = magicItem;
        }

        public event Action<Unit> Died;

        public float Healh => _health;
        public IWeapon Weapon => _weapon;
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