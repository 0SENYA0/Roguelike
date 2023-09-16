using Assets.Interface;
using Assets.Scripts.InteractiveObjectSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Inventory.Panel
{
    public class WeaponPanelItem : PanelItem
    {
        [SerializeField] private ElementsSpriteView _elementsSprite;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _damage;
        [SerializeField] private TMP_Text _splash;
        [SerializeField] private TMP_Text _critical;
        [SerializeField] private TMP_Text _modifier;

        public void Init(IWeapon weapon)
        {
            _item = weapon as IInventoryItem;
            
            _image.sprite = _elementsSprite.GetElementSprite(weapon.Element);
            _name.text = "Random name";
            _damage.text = $"{weapon.Damage}";
            _splash.text = $"{weapon.ChanceToSplash}";
            _critical.text = $"{weapon.MinValueToCriticalDamage}";
            _modifier.text = $"{weapon.ValueModifier}";
            
            _remove.onClick.AddListener(OnClickRemove);
        }
    }
}