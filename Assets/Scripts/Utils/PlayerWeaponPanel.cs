using System;
using System.Collections.Generic;
using Assets.Inventory;
using Assets.Inventory.Panel;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Utils
{
    public class PlayerWeaponPanel : MonoBehaviour
    {
        [SerializeField] private WeaponPanelElement _template;
        [SerializeField] private Transform _container;
        [SerializeField] private Button _close;
         
        private List<WeaponPanelElement> _items;

        public event Action<IInventoryItem> ChooseWeaponEvent; 
        public event Action ClosePanelEvent; 

        public void Init(IEnumerable<Weapon.Weapon> weapons)
        {
            _items = new List<WeaponPanelElement>();
            
            foreach (var weapon in weapons)
            {
                var newItem = Instantiate(_template, _container).GetComponent<WeaponPanelElement>();
                newItem.Init(weapon);
                newItem.OnItemClicked += OnItemSelected;
                _items.Add(newItem);
            }
            
            _close.onClick.AddListener(ClosePanel);
        }

        public void Dispose()
        {
            foreach (var weapon in _items)
            {
                weapon.OnItemClicked -= OnItemSelected;
                weapon.Destroy();
            }

            _items.Clear();
            _items = null;
            
            _close.onClick.RemoveListener(ClosePanel);
        }

        private void OnItemSelected(IInventoryItem obj) => ChooseWeaponEvent?.Invoke(obj);

        private void ClosePanel() => ClosePanelEvent?.Invoke();

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}