using System;
using Assets.Config;
using Assets.DefendItems;
using Assets.Inventory;
using Assets.Scripts.InteractiveObjectSystem;
using Assets.Scripts.SoundSystem;
using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.HUD
{
    public class RewardPanel : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private SoundComponent _winSound;
        [Space]
        [Header("Блоки элементов")] 
        [SerializeField] private Image _attackImage;
        [SerializeField] private Image _defendImage;
        [SerializeField] private TMP_Text _name;
        [SerializeField]private TMP_Text _data;
        [SerializeField] private Image _element;
        [SerializeField] private ElementsSpriteView _elementsSpriteView;
        
        public event Action OnButtonClickEvent; 

        public void Show(IInventoryItem loot, int money)
        {
            ActiveLootPanel();
            
            if (loot is Armor armor)
                ShowArmorLootInfo(armor, money);
            else if (loot is Weapon.Weapon weapon)
                ShowWeaponLootInfo(weapon, money);
        }

        public void Show(Armor armor, Weapon.Weapon weapon, int money)
        {
            ActiveLootPanel();
            ShowBossLoot(armor, weapon, money);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnButtonClicked()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
            OnButtonClickEvent?.Invoke();
        }

        private void ActiveLootPanel()
        {
            gameObject.SetActive(true);
            _winSound.Play();
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void ShowWeaponLootInfo(Weapon.Weapon weapon, int money)
        {
            _defendImage.gameObject.SetActive(false);
            _attackImage.gameObject.SetActive(true);
            _element.sprite = _elementsSpriteView.GetElementSprite(weapon.Element);
            
            _name.text = "Ты победил!";
            _data.text = $"Твоя награда:\n" +
                         $"{GetLocalizedText(LanguageConfig.MoneyKey)}: +{money}$\n" +
                         $"{GetLocalizedText(LanguageConfig.WeaponKey)}\n" +
                         $"{GetLocalizedText(LanguageConfig.DamageKey)} = {weapon.Damage}\n" +
                         $"{GetLocalizedText(LanguageConfig.DmgModifierKey)} = {weapon.ValueModifier}\n" +
                         $"{GetLocalizedText(LanguageConfig.SplashChanceKey)} = {weapon.ChanceToSplash}\n" +
                         $"{GetLocalizedText(LanguageConfig.CriticalChanceKey)} = {weapon.MinValueToCriticalDamage}";
        }

        private void ShowArmorLootInfo(Armor armor, int money)
        {
            _attackImage.gameObject.SetActive(false);
            _defendImage.gameObject.SetActive(true);
            _element.sprite = _elementsSpriteView.GetElementSprite(armor.Body.Element);
            
            _name.text = "Ты победил!";
            _data.text = $"Твоя награда:\n" +
                         $"{GetLocalizedText(LanguageConfig.MoneyKey)}: +{money}$\n" +
                         $"{GetLocalizedText(LanguageConfig.ArmorKey)}\n" +
                         $"{GetLocalizedText(LanguageConfig.BodyKey)} = {armor.Body.Value}\n" +
                         $"{GetLocalizedText(LanguageConfig.HeadKey)} = {armor.Head.Value}";
        }

        private void ShowBossLoot(Armor armor, Weapon.Weapon weapon, int money)
        {
            _attackImage.gameObject.SetActive(true);
            _defendImage.gameObject.SetActive(true);
            
            _name.text = "Ты победил!";
            _data.text = $"Твоя награда:\n" +
                         $"{GetLocalizedText(LanguageConfig.MoneyKey)}: +{money}$\n" +
                         $"{GetLocalizedText(LanguageConfig.WeaponKey)}\n" +
                         $"{GetLocalizedText(LanguageConfig.DamageKey)} = {weapon.Damage}\n" +
                         $"{GetLocalizedText(LanguageConfig.DmgModifierKey)} = {weapon.ValueModifier}\n" +
                         $"{GetLocalizedText(LanguageConfig.SplashChanceKey)} = {weapon.ChanceToSplash}\n" +
                         $"{GetLocalizedText(LanguageConfig.CriticalChanceKey)} = {weapon.MinValueToCriticalDamage}\n\n" +
                         $"{GetLocalizedText(LanguageConfig.ArmorKey)}\n" +
                         $"{GetLocalizedText(LanguageConfig.BodyKey)} = {armor.Body.Value}\n" +
                         $"{GetLocalizedText(LanguageConfig.HeadKey)} = {armor.Head.Value}";
        }
        
        protected string GetLocalizedText(string key)
        {
            return LeanLocalization.GetTranslation(key).Data.ToString();
        }
    }
}