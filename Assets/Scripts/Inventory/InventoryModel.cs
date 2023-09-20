using System;
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

        public event Action<int, int> CountItemsChangeEvent; 

        public int TotalSize => _items.Count;
        public int MaxSize => _maxSize;

        public IReadOnlyList<Armor> GetArmor() => _items.OfType<Armor>().ToList();
        public IReadOnlyList<Weapon.Weapon> GetWeapon() => _items.OfType<Weapon.Weapon>().ToList();

        public void AddItem(IInventoryItem item)
        {
            if (TotalSize < _maxSize)
            {
                _items.Add(item);
                CountItemsChangeEvent?.Invoke(MaxSize, TotalSize);
            }
            else
            {
                throw new AggregateException("Inventory is full");
            }
        }

        public void RemoveItem(IInventoryItem item)
        {
            if (_items.Contains(item))
            {
                if (item is Armor armor)
                    armor.UnSelect();
                
                _items.Remove(item);
                CountItemsChangeEvent?.Invoke(MaxSize, TotalSize);
            }
            else
            {
                throw new AggregateException("This item is not in the inventory");
            }
        }
    }
}