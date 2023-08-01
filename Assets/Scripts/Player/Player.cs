using Assets.Interface;
using Assets.Person;
using Assets.Person.DefendItems;
using Assets.Person.PersonStates;
using UnityEngine;

namespace Assets.Player
{
    public class Player : Unit
    {
        public Player(int health, IWeapon weapon, Armor armor, MagicItem magicItem, PersonStateMachine personStateMachine) : base(health, weapon, armor, magicItem, personStateMachine)
        {
        }
    }
}