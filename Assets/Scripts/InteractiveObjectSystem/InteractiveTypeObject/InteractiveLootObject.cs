using Assets.DefendItems;
using Assets.Inventory.ItemGeneratorSystem;
using Assets.Weapons;
using UnityEngine;

namespace Assets.Scripts.InteractiveObjectSystem
{
    public class InteractiveLootObject : InteractiveObject
    {
        private Weapon _weapon;
        private Armor _armor;

        public Weapon Weapon => _weapon;

        public Armor Armor => _armor;

        protected override void OnStart()
        {
            base.OnStart();

            if (Random.Range(0, 2) == 0)
                _weapon = ItemGenerator.Instance.GetRandomWeapon();
            else
                _armor = ItemGenerator.Instance.GetRandomArmor();
        }
    }
}