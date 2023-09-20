using System;
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

        private const string WeaponKey = "Weapon";
        private const string DamageKey = "Damage";
        private const string ArmorKey = "Armor";
        private const string BodyKey = "Body";
        private const string HeadKey = "Head";
        private const string DmgModifierKey = "DmgModifier";
        private const string SplashChanceKey = "SplashChance";
        private const string CriticalChanceKey = "CriticalChance";

        public event Action OnButtonClickEvent; 

        public void Show(IInventoryItem loot)
        {
            gameObject.SetActive(true);
            _winSound.Play();
            _button.onClick.AddListener(OnButtonClicked);
            
            if (loot is Armor armor)
                ShowArmorLootInfo(armor);
            else if (loot is Weapon.Weapon weapon)
                ShowWeaponLootInfo(weapon);
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

        private void ShowWeaponLootInfo(Weapon.Weapon weapon)
        {
            _defendImage.gameObject.SetActive(false);
            _attackImage.gameObject.SetActive(true);
            _element.sprite = _elementsSpriteView.GetElementSprite(weapon.Element);
            
            _name.text = "Ты победил!";
            _data.text = $"Твоя награда:\n" +
                         $"{GetLocalizedText(WeaponKey)}\n" +
                         $"{GetLocalizedText(DamageKey)} = {weapon.Damage}\n" +
                         $"{GetLocalizedText(DmgModifierKey)} = {weapon.ValueModifier}\n" +
                         $"{GetLocalizedText(SplashChanceKey)} = {weapon.ChanceToSplash}\n" +
                         $"{GetLocalizedText(CriticalChanceKey)} = {weapon.MinValueToCriticalDamage}";
        }

        private void ShowArmorLootInfo(Armor armor)
        {
            _attackImage.gameObject.SetActive(false);
            _defendImage.gameObject.SetActive(true);
            _element.sprite = _elementsSpriteView.GetElementSprite(armor.Body.Element);
            
            _name.text = "Ты победил!";
            _data.text = $"Твоя награда:\n" +
                         $"{GetLocalizedText(ArmorKey)}\n" +
                         $"{GetLocalizedText(BodyKey)} = {armor.Body.Value}\n" +
                         $"{GetLocalizedText(HeadKey)} = {armor.Head.Value}";
        }
        
        protected string GetLocalizedText(string key)
        {
            return LeanLocalization.GetTranslation(key).Data.ToString();
        }
    }
}