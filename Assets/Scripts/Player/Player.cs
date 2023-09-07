using System;
using Assets.DefendItems;
using Assets.Interface;
using Assets.Person;
using Assets.Person.PersonStates;
using Assets.Scripts.AnimationComponent;
using UnityEngine;

namespace Assets.Player
{
    public class Player : Unit
    {
        private int _health;
        private readonly IWeapon _weapon;
        private readonly Armor _armor;
        private readonly MagicItem _magicItem;
        private readonly PlayerInventary _playerInventary;

        public Player(int health, IWeapon weapon, Armor armor, MagicItem magicItem, SpriteAnimation spriteAnimation)
            : base(health, weapon, armor, magicItem, spriteAnimation)
        {
            _health = health;
            _weapon = weapon;
            _armor = armor;
            _magicItem = magicItem;
        }

        public Player(int health, PlayerInventary playerInventary, SpriteAnimation spriteAnimation)
            : base(health, null, null, null, spriteAnimation)
        {
            _playerInventary = playerInventary;
            _health = health;
        }

        protected override void CalculateDamageMultiplier(IWeapon weapon)
        {
            _health -= Convert.ToInt32(weapon.Damage);
        }

        public PlayerInventary PlayerInventary => _playerInventary;
    }

    public class PlayerInventary
    {
        private readonly IWeapon[] _weapon;
        private readonly Armor[] _armor;
        private readonly MagicItem[] _magicItem;

        public PlayerInventary(IWeapon[] weapon, Armor[] armor, MagicItem[] magicItem)
        {
            _weapon = weapon;
            _armor = armor;
            _magicItem = magicItem;
        }

        public IWeapon[] Weapon => _weapon;

        public Armor[] Armor => _armor;

        public MagicItem[] MagicItem => _magicItem;
    }
}