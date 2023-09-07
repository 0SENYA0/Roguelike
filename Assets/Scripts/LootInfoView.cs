using System;
using Assets.Scripts.InteractiveObjectSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class LootInfoView : InfoView
    {
        [Header("блок атакующего элемента")] [SerializeField]
        private Image _attackImage;
        
        [Header("блок защитного элемента")] [SerializeField]
        private Image _defendImage;

        [Header("блок названия")] [SerializeField]
        private TMP_Text _name;

        [Header("блок описания")] [SerializeField]
        private TMP_Text _data;

        [SerializeField] private Image _element;
        [SerializeField] private ElementsSpriteView _elementsSpriteView;        
      
        public void Show(InteractiveLootObject lootObject)
        {
            gameObject.SetActive(true);

            if (lootObject.Weapon is not null)
            {
                _defendImage.gameObject.SetActive(false);
                _attackImage.gameObject.SetActive(true);
                _element.sprite = _elementsSpriteView.GetElementSprite(lootObject.Weapon.Element);
                _name.text = "Weapon";
                _data.text = $"damage = {lootObject.Weapon.Damage}" +
                             $"\nValueModifier = {lootObject.Weapon.ValueModifier}" +
                             $"\nChanceToSplash = {lootObject.Weapon.ChanceToSplash}" +
                             $"\nChanceToCriticalDamage {lootObject.Weapon.MinValueToCriticalDamage}";
            }
            else
            {
                _attackImage.gameObject.SetActive(false);
                _defendImage.gameObject.SetActive(true);
                _element.sprite = _elementsSpriteView.GetElementSprite(lootObject.Armor.Body.Element);
                _name.text = "Armor";
                _data.text = $"body = {lootObject.Armor.Body.Value}," +
                             $"\nhead = {lootObject.Armor.Head.Value}";
            }
        }
    }
}