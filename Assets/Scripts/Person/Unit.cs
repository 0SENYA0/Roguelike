using System;
using Assets.DefendItems;
using Assets.Fight.Element;
using Assets.Interface;
using Assets.Person.PersonStates;
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
        
        public Unit(int health, IWeapon weapon, Armor armor, MagicItem magicItem)
        {
            _health = health;
            _weapon = weapon;
            _armor = armor;
            _magicItem = magicItem;
            _personStateMachine = new PersonStateMachine();
        }

        public event Action<Unit> Died;

        public float Healh => _health;
        public IWeapon Weapon => _weapon;
        public Armor Armor => _armor;
        public IPersonStateMachine PersonStateMachine => _personStateMachine;
        public void TakeDamage(IWeapon weapon)
        {
            CalculateDamageMultiplier(weapon);
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

        protected virtual void CalculateDamageMultiplier(IWeapon weapon)
        {
            float damageMultiplier = weapon.Damage / (CalculateDamageModifier(weapon.Element) * weapon.Damage + (_armor.Body.Value + _armor.Head.Value));
            _health -= damageMultiplier * weapon.Damage;
        }

        private float CalculateDamageModifier(Element element) =>
            ElementManager.GetDamageModifier(element, _armor.Body.Element);
    }

    public interface IDamagable
    {
        //void TakeDamage(DamageData damage);
    }

    public interface IAttack
    {
        void Attack();
    }
}