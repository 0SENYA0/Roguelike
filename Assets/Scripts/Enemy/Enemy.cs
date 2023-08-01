using Assets.Interface;
using Assets.Person;
using Assets.Person.DefendItems;
using Assets.Person.PersonStates;
using UnityEngine;

namespace Assets.Enemy
{
    public class Enemy : Unit
    {
        public Enemy(int health, IWeapon weapon, Armor armor, MagicItem magicItem, IPersonStateMachine personStateMachine) 
            : base(health, weapon, armor, magicItem, personStateMachine)
        {
        }

        public bool Boss { get; private set; }

        public void MakeBoss() =>
            Boss = true;
    }
}