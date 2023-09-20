using System;
using Assets.Interface;
using Assets.Inventory;
using Assets.Scripts.InteractiveObjectSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Utils
{
    public class WeaponPanelElement : MonoBehaviour
    {
        [SerializeField] private ElementsSpriteView _elementsSprite;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _damage;
        [SerializeField] private TMP_Text _splash;
        [SerializeField] private TMP_Text _critical;
        [SerializeField] private TMP_Text _modifier;
        [SerializeField] private Button _useButton;
        
        protected IInventoryItem _item;
        public Action<IInventoryItem> OnItemClicked;

        public void Init(IWeapon weapon)
        {
            _item = weapon as IInventoryItem;
            
            _image.sprite = _elementsSprite.GetElementSprite(weapon.Element);
            _name.text = "Random name";
            _damage.text = $"{weapon.Damage}";
            _splash.text = $"{weapon.ChanceToSplash}";
            _critical.text = $"{weapon.MinValueToCriticalDamage}";
            _modifier.text = $"{weapon.ValueModifier}";
            
            _useButton.onClick.AddListener(OnClickItem);
        }
        
        public void OnDispose()
        {
            _useButton.onClick.RemoveListener(OnClickItem);
        }

        private void OnClickItem()
        {
            OnItemClicked?.Invoke(_item);
        }
    }
}