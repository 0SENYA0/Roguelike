using Assets.Interface;
using Assets.Person;
using Assets.Person.DefendItems;
using Assets.Person.PersonStates;
using UnityEngine;

namespace Assets.Enemy
{
    public class EnemyFactory
    {
        private readonly IPersonStateMachine _personStateMachine;

        public EnemyFactory(IPersonStateMachine personStateMachine) =>
            _personStateMachine = personStateMachine;

        private const int BaseHealthFirstLevel = 100;

        public Enemy Create(IWeapon weapon, Armor armor, IPersonStateMachine personStateMachine) =>
            new Enemy(BaseHealthFirstLevel, weapon, armor, new MagicItem(), personStateMachine);
    }
}