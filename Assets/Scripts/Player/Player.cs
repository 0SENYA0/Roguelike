using Assets.DefendItems;
using Assets.Interface;
using Assets.Person;
using Assets.Person.PersonStates;
using UnityEngine;

namespace Assets.Player
{
    public class Player : Unit
    {
        private readonly int _health;
        private readonly IWeapon _weapon;
        private readonly Armor _armor;
        private readonly MagicItem _magicItem;

        public Player(int health, IWeapon weapon, Armor armor, MagicItem magicItem) 
            : base(health, weapon, armor, magicItem)
        {
            _health = health;
            _weapon = weapon;
            _armor = armor;
            _magicItem = magicItem;
        }
    }
}