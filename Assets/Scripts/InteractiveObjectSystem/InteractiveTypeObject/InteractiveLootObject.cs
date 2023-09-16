using Assets.DefendItems;
using Assets.Interface;
using Assets.Inventory.ItemGeneratorSystem;
using Assets.ScriptableObjects;
using Assets.Weapon;
using UnityEngine;

namespace Assets.Scripts.InteractiveObjectSystem
{
    public class InteractiveLootObject : InteractiveObject
    {
        private IWeapon _weapon;
        private Armor _armor;

        protected override void OnStart()
        {
            base.OnStart();

            if (Random.Range(0, 2) == 0)
                _weapon = ItemGenerator.Instance.GetRandomWeapon();
            else
                _armor = ItemGenerator.Instance.GetRandomArmor();
        }

        public IWeapon Weapon => _weapon;
        public Armor Armor => _armor;
    }
}