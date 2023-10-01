using Assets.Config;
using Assets.DefendItems;
using Assets.Infrastructure;
using Assets.Person;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Inventory.Panel
{
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField] private PlayerView _player;
        [Space]
        [SerializeField] private Button _openWeapon;
        [SerializeField] private Button _closeWeapon;
        [SerializeField] private GameObject _panelWeapon;
        [SerializeField] private WeaponPanelView _weapon;
        [Space]
        [SerializeField] private Button _openArmor;
        [SerializeField] private Button _closeArmor;
        [SerializeField] private GameObject _panelArmor;
        [SerializeField] private ArmorPanelView _armor;
        [Space] 
        [SerializeField] private Button _potion;
        [SerializeField] private TMP_Text _numberOfPotion;

        private void Awake()
        {
            _openWeapon.onClick.AddListener(OpenWeaponPanel);
            _closeWeapon.onClick.AddListener(CloseWeaponPanel);
            
            _openArmor.onClick.AddListener(OpenArmorPanel);
            _closeArmor.onClick.AddListener(CloseArmorPanel);
            
            _potion.onClick.AddListener(UsePotion);
            _numberOfPotion.text = Game.GameSettings.PlayerData.Potion.ToString();
        }

        private void OnDestroy()
        {
            _openWeapon.onClick.RemoveListener(OpenWeaponPanel);
            _closeWeapon.onClick.RemoveListener(CloseWeaponPanel);
            
            _openArmor.onClick.RemoveListener(OpenArmorPanel);
            _closeArmor.onClick.RemoveListener(CloseArmorPanel);
            
            _potion.onClick.RemoveListener(UsePotion);
        }

        private void UsePotion()
        {
            if (Game.GameSettings.PlayerData.Potion > 0 && _player.PlayerPresenter.Player.Health < PlayerHealth.MaxPlayerHealth)
            {
                _player.PlayerPresenter.UsePotion();
                _numberOfPotion.text = Game.GameSettings.PlayerData.Potion.ToString();
            }
        }

        private void OpenWeaponPanel()
        {
            _panelWeapon.SetActive(true);
            _weapon.Show(_player.InventoryPresenter.InventoryModel.GetWeapon());
            _weapon.RemoveItem += RemoveItem;
        }

        private void CloseWeaponPanel()
        {
            _weapon.Hide();
            _weapon.RemoveItem -= RemoveItem;
            _panelWeapon.SetActive(false);
        }

        private void OpenArmorPanel()
        {
            _panelArmor.SetActive(true);
            _armor.Show(_player.InventoryPresenter.InventoryModel.GetArmor());
            _armor.RemoveItem += RemoveItem;
            _armor.SelectItem += SelectItem;
        }

        private void CloseArmorPanel()
        {
            _armor.Hide();
            _armor.RemoveItem -= RemoveItem;
            _armor.SelectItem -= SelectItem;
            _panelArmor.SetActive(false);
        }

        private void RemoveItem(IInventoryItem obj)
        {
            _player.InventoryPresenter.InventoryModel.RemoveItem(obj);
        }

        private void SelectItem(IInventoryItem obj)
        {
            _player.InventoryPresenter.SelectActiveArmor(obj as Armor);
        }
    }
}