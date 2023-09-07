using System;
using Assets.DefendItems;
using Assets.Interface;

namespace Assets.Inventory
{
    public class InventoryModel
    {
        private Armor[] _armors;
        private IWeapon[] _weapons;
        private MagicItem[] _magicItems;
        public InventoryModel()
        {
            _armors = new Armor[5];
            _weapons = new IWeapon[5];
            _magicItems = new MagicItem[5];
        }
    }
}