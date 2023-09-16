using System.Collections.Generic;
using System.Linq;
using Assets.DefendItems;

namespace Assets.Inventory
{
    public class InventoryModel
    {
        private readonly List<IInventoryItem> _items;
        private readonly int _maxSize;

        public InventoryModel(int maxSize)
        {
            _maxSize = maxSize;
            _items = new List<IInventoryItem>();
        }

        public int TotalSize => _items.Count;

        public IReadOnlyList<Armor> GetArmor() => _items.OfType<Armor>().ToList();
        public IReadOnlyList<Weapon.Weapon> GetWeapon() => _items.OfType<Weapon.Weapon>().ToList();
        public IReadOnlyList<MagicItem> GetMagicItem() => _items.OfType<MagicItem>().ToList();

        public void AddItem(IInventoryItem item)
        {
            if (TotalSize < _maxSize)
                _items.Add(item);
            else
                throw new System.AggregateException("Inventory is full");
        }

        public void RemoveItem(IInventoryItem item)
        {
            if (_items.Contains(item))
                _items.Remove(item);
            else
                throw new System.AggregateException("This item is not in the inventory");
        }
    }
}