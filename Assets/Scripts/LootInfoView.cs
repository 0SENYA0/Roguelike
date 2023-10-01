using Assets.Config;
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
            
            _name.text = GetLocalizedText(LanguageConfig.WeaponKey);
            _data.text = $"{GetLocalizedText(LanguageConfig.DamageKey)} = {lootObject.Weapon.Damage}\n" +
                         $"{GetLocalizedText(LanguageConfig.DmgModifierKey)} = {lootObject.Weapon.ValueModifier}\n" +
                         $"{GetLocalizedText(LanguageConfig.SplashChanceKey)} = {lootObject.Weapon.ChanceToSplash}\n" +
                         $"{GetLocalizedText(LanguageConfig.CriticalChanceKey)} = {lootObject.Weapon.MinValueToCriticalDamage}";
        }

        private void ShowArmorLootInfo(InteractiveLootObject lootObject)
        {
            _attackImage.gameObject.SetActive(false);
            _defendImage.gameObject.SetActive(true);
            _element.sprite = _elementsSpriteView.GetElementSprite(lootObject.Armor.Body.Element);
            
            _name.text = GetLocalizedText(LanguageConfig.ArmorKey);
            _data.text = $"{GetLocalizedText(LanguageConfig.BodyKey)} = {lootObject.Armor.Body.Value}\n" +
                         $"{GetLocalizedText(LanguageConfig.HeadKey)} = {lootObject.Armor.Head.Value}";
        }
    }
}