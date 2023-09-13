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

        private const string WeaponKey = "Weapon";
        private const string DamageKey = "Damage";
        private const string ArmorKey = "Armor";
        private const string BodyKey = "Body";
        private const string HeadKey = "Head";
        private const string DmgModifierKey = "DmgModifier";
        private const string SplashChanceKey = "SplashChance";
        private const string CriticalChanceKey = "CriticalChance";
      
        public void Show(InteractiveLootObject lootObject)
        {
            gameObject.SetActive(true);
            
            if (lootObject.Weapon != null)
                ShowWeaponLootInfo(lootObject);
            else
                ShowArmorLootInfo(lootObject);
        }

        private void ShowWeaponLootInfo(InteractiveLootObject lootObject)
        {
            _defendImage.gameObject.SetActive(false);
            _attackImage.gameObject.SetActive(true);
            _element.sprite = _elementsSpriteView.GetElementSprite(lootObject.Weapon.Element);
            
            _name.text = GetLocalizedText(WeaponKey);
            _data.text = $"{GetLocalizedText(DamageKey)} = {lootObject.Weapon.Damage}\n" +
                         $"{GetLocalizedText(DmgModifierKey)} = {lootObject.Weapon.ValueModifier}\n" +
                         $"{GetLocalizedText(SplashChanceKey)} = {lootObject.Weapon.ChanceToSplash}\n" +
                         $"{GetLocalizedText(CriticalChanceKey)} = {lootObject.Weapon.MinValueToCriticalDamage}";
        }

        private void ShowArmorLootInfo(InteractiveLootObject lootObject)
        {
            _attackImage.gameObject.SetActive(false);
            _defendImage.gameObject.SetActive(true);
            _element.sprite = _elementsSpriteView.GetElementSprite(lootObject.Armor.Body.Element);
            
            _name.text = GetLocalizedText(ArmorKey);
            _data.text = $"{GetLocalizedText(BodyKey)} = {lootObject.Armor.Body.Value}\n" +
                         $"{GetLocalizedText(HeadKey)} = {lootObject.Armor.Head.Value}";
        }
    }
}