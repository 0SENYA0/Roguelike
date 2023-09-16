using System;
using Assets.Person;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Assets.Inventory.Panel
{
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField] private PlayerView _player;
        [SerializeField] private Button _open;
        [SerializeField] private Button _close;
        [SerializeField] private GameObject _panel;
        [SerializeField] private WeaponPanelView _weapon;

        private void Awake()
        {
            _open.onClick.AddListener(OpenPanel);
            _close.onClick.AddListener(ClosePanel);
        }

        private void OnDestroy()
        {
            _open.onClick.RemoveListener(OpenPanel);
            _close.onClick.RemoveListener(ClosePanel);
        }

        private void OpenPanel()
        {
            _panel.SetActive(true);
            _weapon.Show(_player.InventoryPresenter.InventoryModel.GetWeapon());
            _weapon.ChangeItem += OnListener;
        }

        private void OnListener(IInventoryItem obj)
        {
            Debug.Log($"Тык по предмету: {obj}");
            _player.InventoryPresenter.InventoryModel.RemoveItem(obj);
        }

        private void ClosePanel()
        {
            _weapon.Hide();
            _weapon.ChangeItem -= OnListener;
            _panel.SetActive(false);
        }
    }

}