using System;
using Assets.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Inventory.Panel
{
    public class WeaponPanelItem : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _damage;
        [SerializeField] private TMP_Text _splash;
        [SerializeField] private TMP_Text _critical;
        [SerializeField] private TMP_Text _modifier;
        [SerializeField] private Button _remove;
        [SerializeField] private Button _use;

        private IInventoryItem _weapon;

        public Action<IInventoryItem> OnItemRemove;
        
        public void Init(IWeapon weapon)
        {
            _weapon = weapon as IInventoryItem;
            _name.text = "Random name";
            _damage.text = $"{weapon.Damage}";
            _splash.text = $"{weapon.ChanceToSplash}";
            _critical.text = $"{weapon.MinValueToCriticalDamage}";
            _modifier.text = $"{weapon.ValueModifier}";
            
            _remove.onClick.AddListener(OnClickRemove);
            _use.onClick.AddListener(OnClickUse);
        }

        public void OnDispose()
        {
            _remove.onClick.RemoveListener(OnClickRemove);
            _use.onClick.RemoveListener(OnClickUse);
            Destroy(gameObject);
        }

        private void OnClickRemove()
        {
            OnItemRemove?.Invoke(_weapon);
            Destroy(gameObject);
        }

        private void OnClickUse()
        {
            
        }
    }
}