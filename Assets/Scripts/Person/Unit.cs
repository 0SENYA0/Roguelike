using System;
using Assets.DefendItems;
using Assets.Fight.Element;
using Assets.Interface;
using Assets.Person.PersonStates;
using Assets.Scripts.AnimationComponent;
using UnityEngine;

namespace Assets.Person
{
    public  class Unit
    {
        protected float _health;
        private IPersonStateMachine _personStateMachine;
        private IWeapon _weapon;
        private Armor _armor;

        public Unit(float health, IWeapon weapon, Armor armor, SpriteAnimation spriteAnimation)
        {
            _health = health;
            _weapon = weapon;
            _armor = armor;
            SpriteAnimation = spriteAnimation;
            _personStateMachine = new PersonStateMachine();
        }

        public event Action<Unit> Died;
        public event Action<float> HealthChanged;

        public SpriteAnimation SpriteAnimation { get; }
        public float Health => _health;
        public IWeapon Weapon => _weapon;
        public Armor Armor => _armor;
        public bool IsDie { get; private set; } = false;
        public IPersonStateMachine PersonStateMachine => _personStateMachine;
        public Sprite Sprite { get; set; }

        public void TakeDamage(IWeapon weapon, bool isCriticalDamage, bool isModifiedDamage)
        {
            CalculateDamageMultiplier(weapon, isCriticalDamage, isModifiedDamage);
            ConditionForDead();
            HealthChanged?.Invoke(_health);
        }
        
        public void Heal(int value)
        {
            _health += value;
            HealthChanged?.Invoke(_health);
        }

        public void Reborn()
        {
            IsDie = false;
        }

        protected virtual void ConditionForDead()
        {
            if (_health <= 0)
            {
                IsDie = true;
                Died?.Invoke(this);
                _health = 0;
            }
            else
                IsDie = false;
        }

        protected virtual void CalculateDamageMultiplier(IWeapon weapon, bool isCriticalDamage, bool isModifiedDamage)
        {
            if (IsDie)
                return;

            float elementMultiplier = CalculateDamageModifier(weapon.Element, _armor.Body.Element);

            float damage = elementMultiplier * weapon.Damage;

            if (isCriticalDamage)
                damage *= 2;

            if (isModifiedDamage)
                damage *= 2;

            float armorValue = (_armor.Body.Value + _armor.Head.Value);
            _health -= Math.Max(damage - armorValue, 1);
        }

        protected float CalculateDamageModifier(Element weaponElement, Element element) =>
            ElementManager.GetDamageModifier(weaponElement, element);
    }
}