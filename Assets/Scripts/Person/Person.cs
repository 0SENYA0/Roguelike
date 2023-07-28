using System;
using Assets.Person.PersonStates;
using UnityEngine;

namespace Assets.Person
{
    public abstract class Person : MonoBehaviour, IDamagable
    {
        [SerializeField] private Sprite _sprite;

        [SerializeField] private PersonStateMachine _personStateMachine;

        private int _health;
        public int Healh => _health;
        public event Action Died;
        public Sprite Sprite => _sprite;

        private Weapon _weapon;
        public Weapon Weapon => _weapon;

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

        private void Dead() =>
            Debug.Log("He is dead");

        public PersonStateMachine PersonStateMachine => _personStateMachine;


        public virtual void TakeDamage(DamageData damage)
        {
            CalculateDamage(damage);
        }

        private void CalculateDamage(DamageData damage)
        {
           float value = ElementManager.GetDamageModifier(damage.Element, _armor.Body.DefendElement);
           
        }
    }

    public static class ElementManager
    {
        private static readonly float _normalModifier = 1f;
        private static readonly float _increasedModifier = 2f;
        private static readonly float _loweredModifier = 0.5f;

        public static float GetDamageModifier(Element attack, Element defender)
        {
            if (attack == defender)
                return _normalModifier;

            int firstStep = 1;
            int secondStep = 2;
            int positiveFirstStep = 4;
            int positiveSecondStep = 3;
            
            bool firstCondition = (defender - firstStep >= 0 ? defender - firstStep : defender + positiveFirstStep) == attack; 
            bool secondCondition = (defender - secondStep >= 0 ? defender - secondStep : defender + positiveSecondStep) == attack;

            if (firstCondition || secondCondition)
                return _increasedModifier;
            
            return _loweredModifier;
        }

    }

    public interface IDamagable
    {
        void TakeDamage(DamageData damage);
    }

    public interface IAttack
    {
        void Attack();
    }

    public struct DamageData
    {
        public float Value { get; set; }
        public Element Element { get; set; }
    }

    public enum Element
    {
        Fire = 0,
        Tree = 1,
        Water = 2,
        Metal = 3,
        Stone = 4,
    }

}