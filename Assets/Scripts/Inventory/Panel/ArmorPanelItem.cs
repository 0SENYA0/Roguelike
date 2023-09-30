using System;
using Assets.DefendItems;
using Assets.Scripts.InteractiveObjectSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Inventory.Panel
{
    public class ArmorPanelItem : PanelItem
    {
        [SerializeField] private ElementsSpriteView _elementsSprite;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _body;
        [SerializeField] private TMP_Text _head;
        [SerializeField] private Button _use;
        [Space] 
        [SerializeField] private Image _background;
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private Sprite _selectSprite;
        
        public Action<IInventoryItem> OnItemUse;

        public void Init(Armor armor)
        {
            _item = armor;

            _image.sprite = _elementsSprite.GetElementSprite(armor.Body.Element);
            _name.text = _nameOfElements.GetElementName(armor.Body.Element);
            _body.text = $"{armor.Body.Value}";
            _head.text = $"{armor.Head.Value}";

            if (armor.IsSelect)
                _background.sprite = _selectSprite;

            _remove.onClick.AddListener(OnClickRemove);
            _use.onClick.AddListener(OnClickUse);
        }

        public void CheckSelect()
        {
            Armor armor = _item as Armor;
            
            if (armor.IsSelect)
                _background.sprite = _selectSprite;
            else
                _background.sprite = _defaultSprite;
        }

        private void OnClickUse()
        {
            OnItemUse?.Invoke(_item);
        }
    }
}