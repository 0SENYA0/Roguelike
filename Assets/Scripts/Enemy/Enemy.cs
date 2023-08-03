using Assets.Interface;
using Assets.Person;
using Assets.Person.DefendItems;
using Assets.Scripts.InteractiveObjectSystem;

namespace Assets.Enemy
{
    public class Enemy : Unit
    {
        private string _name;
        private string _data;

        public Enemy(int health, IWeapon weapon, Armor armor, MagicItem magicItem, IPersonStateMachine personStateMachine) 
            : base(health, weapon, armor, magicItem, personStateMachine)
        {
        }

        public bool Boss { get; private set; }

        public void MakeBoss() =>
            Boss = true;

    }
}