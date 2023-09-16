using System;
using System.Collections.Generic;
using Assets.Interface;
using UnityEngine;

namespace Assets.Inventory.Panel
{
    public class WeaponPanelView : MonoBehaviour
    {
        [SerializeField] private WeaponPanelItem _template;
        [SerializeField] private Transform _container;
        
        public Action<IInventoryItem> ChangeItem;
        
        private List<WeaponPanelItem> _items = new();

        public void Show(IEnumerable<IWeapon> weapons)
        {
            foreach (var weapon in weapons)
            {
                var newItem = Instantiate(_template, _container).GetComponent<WeaponPanelItem>();
                newItem.Init(weapon);
                newItem.OnItemRemove += OnItemRemove;
                _items.Add(newItem);
            }
        }

        private void OnItemRemove(IInventoryItem obj)
        {
            ChangeItem?.Invoke(obj);
        }

        public void Hide()
        {
            foreach (var item in _items)
            {
                if (item == null)
                    continue;
                
                item.OnItemRemove -= OnItemRemove;
                item.OnDispose();
            }

            _items = new();
        }
    }
}