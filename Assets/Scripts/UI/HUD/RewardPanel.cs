using System;
using Assets.Config;
using Assets.DefendItems;
using Assets.Inventory;
using Assets.Scripts.InteractiveObjectSystem;
using Assets.Scripts.SoundSystem;
using Assets.Weapons;
using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.HUD
{
    public class RewardPanel : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private SoundService _winSound;
        [Space]
        [SerializeField] private Image _attackImage;
        [SerializeField] private Image _defendImage;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _data;
        [SerializeField] private Image _element;
        [SerializeField] private ElementsSpriteView _elementsSpriteView; 

        public event Action OnButtonClickEvent; 

        public void Show(IInventoryItem loot, int money)
        {
            ActiveLootPanel();

            if (loot is Armor armor)
                ShowArmorLootInfo(armor, money);
            else if (loot is Weapon weapon)
                ShowWeaponLootInfo(weapon, money);
        }

        public void Show(Armor armor, Weapon weapon, int money)
        {
            ActiveLootPanel();
            ShowBossLoot(armor, weapon, money);
        }

        public void Hide() =>
            gameObject.SetActive(false);

        protected string GetLocalizedText(string key) =>
            LeanLocalization.GetTranslation(key).Data.ToString();

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

        private void ShowWeaponLootInfo(Weapon weapon, int money)
        {
            _defendImage.gameObject.SetActive(false);
            _attackImage.gameObject.SetActive(true);
            _element.sprite = _elementsSpriteView.GetElementSprite(weapon.Element);
            _name.text = "Ты победил!";
            _data.text = $"Твоя награда:\n" +
                         $"{GetLocalizedText(LanguageConfig.MoneyKey)}: +{money}$\n" +
                         $"{GetLocalizedText(LanguageConfig.WeaponKey)}\n" +
                         $"{GetLocalizedText(LanguageConfig.DamageKey)} = {weapon.Damage:F1}\n" +
                         $"{GetLocalizedText(LanguageConfig.DmgModifierKey)} = {weapon.ChanceToModifier}\n" +
                         $"{GetLocalizedText(LanguageConfig.SplashChanceKey)} = {weapon.ChanceToSplash}\n" +
                         $"{GetLocalizedText(LanguageConfig.CriticalChanceKey)} = {weapon.ChanceToCritical}";
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
                         $"{GetLocalizedText(LanguageConfig.BodyKey)} = {armor.Body.Value:F1}\n" +
                         $"{GetLocalizedText(LanguageConfig.HeadKey)} = {armor.Head.Value:F1}";
        }

        private void ShowBossLoot(Armor armor, Weapon weapon, int money)
        {
            _attackImage.gameObject.SetActive(true);
            _defendImage.gameObject.SetActive(true);
            _name.text = "Ты победил!";
            _data.text = $"Твоя награда:\n" +
                         $"{GetLocalizedText(LanguageConfig.MoneyKey)}: +{money}$\n" +
                         $"{GetLocalizedText(LanguageConfig.WeaponKey)}\n" +
                         $"{GetLocalizedText(LanguageConfig.DamageKey)} = {weapon.Damage:F1}\n" +
                         $"{GetLocalizedText(LanguageConfig.DmgModifierKey)} = {weapon.ChanceToModifier}\n" +
                         $"{GetLocalizedText(LanguageConfig.SplashChanceKey)} = {weapon.ChanceToSplash}\n" +
                         $"{GetLocalizedText(LanguageConfig.CriticalChanceKey)} = {weapon.ChanceToCritical}\n\n" +
                         $"{GetLocalizedText(LanguageConfig.ArmorKey)}\n" +
                         $"{GetLocalizedText(LanguageConfig.BodyKey)} = {armor.Body.Value:F1}\n" +
                         $"{GetLocalizedText(LanguageConfig.HeadKey)} = {armor.Head.Value:F1}";
        }
    }
}