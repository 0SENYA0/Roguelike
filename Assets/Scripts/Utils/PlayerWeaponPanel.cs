using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Fight.Element;
using Assets.Inventory;
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
        private Weapon.Weapon _defaultWeapon;
        public event Action<IInventoryItem> ChooseWeaponEvent; 
        public event Action ClosePanelEvent; 

        public void Init(IEnumerable<Weapon.Weapon> weapons)
        {
            _items = new List<WeaponPanelElement>();

            if (weapons.Count() == 0)
                _defaultWeapon = new Weapon.Weapon(1, Element.Fire, 0, 0, 0, null);
            
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
            
            if (_defaultWeapon != null)
                ChooseWeaponEvent?.Invoke(_defaultWeapon);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}