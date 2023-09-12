using System;
using Assets.DefendItems;
using Assets.Interface;

namespace Assets.Inventory
{
    public class InventoryModel
    {
        private readonly IWeapon[] _weapon;
        private readonly Armor[] _armor;
        private readonly MagicItem[] _magicItem;

        public InventoryModel(IWeapon[] weapon, Armor[] armor, MagicItem[] magicItem)
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