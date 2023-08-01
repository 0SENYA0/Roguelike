using Assets.Person;
<<<<<<< Updated upstream
=======
using Assets.Person.DefendItems;
using Assets.Person.PersonStates;
using Assets.Scripts.InteractiveObjectSystem;
>>>>>>> Stashed changes
using UnityEngine;

namespace Assets.Enemy
{
    public class Enemy : Person.Person
    {
<<<<<<< Updated upstream
        public bool Boss { get; private set; }
=======
        private string _name;
        private string _data;

        public Enemy(int health, IWeapon weapon, Armor armor, MagicItem magicItem, IPersonStateMachine personStateMachine) 
            : base(health, weapon, armor, magicItem, personStateMachine)
        {
        }

        public bool Boss { get; private set; }
        
        public ObjectType ObjectType { get; }
        
        // Временный метод, чтобы удалять противников с поля
        public void DestroyObject()
        {
            //Destroy(gameObject);
        }

        public InteractiveObjectData GetData()
        {
            return new InteractiveObjectData(_name, _data);
        }

        public void MakeBoss() =>
            Boss = true;
>>>>>>> Stashed changes

    }
}