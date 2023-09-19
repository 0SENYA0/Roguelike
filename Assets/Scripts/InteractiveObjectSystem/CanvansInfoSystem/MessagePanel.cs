using System;
using Assets.Enemy;
using Assets.Loot;
using Assets.Person;
using UnityEngine;

namespace Assets.Scripts.InteractiveObjectSystem.CanvasInfoSystem
{
    public class MessagePanel : MonoBehaviour
    {
        [SerializeField] private EnemyInfoView _enemyInfoPanel;
        [SerializeField] private RandomEventView _randomEventPanel;
        [SerializeField] private LootInfoView _randomLootPanel;
        [SerializeField] private WarningMessage _warningMessage;

        [Space] [Header("Player inventory")] [SerializeField]
        private PlayerView _inventory;

        public event Action<bool> UserResponse;

        private void OnEnable()
        {
            _enemyInfoPanel.UserResponse += CallResponse;
            _randomEventPanel.UserResponse += CallResponse;
            _randomLootPanel.UserResponse += CallResponse;
        }

        private void OnDisable()
        {
            _enemyInfoPanel.UserResponse -= CallResponse;
            _randomEventPanel.UserResponse -= CallResponse;
            _randomLootPanel.UserResponse -= CallResponse;
        }

        public void ShowPanel(InteractiveObject interactiveObject)
        {
            if (CheckForInventoryOverload(interactiveObject))
                return;
            
            if (interactiveObject.TryGetComponent(out EnemyView enemyView))
                _enemyInfoPanel.Show(enemyView.EnemyPresenter);
            else if (interactiveObject.TryGetComponent(out InteractiveLootObject lootObject))
                _randomLootPanel.Show(lootObject);
            else if (interactiveObject.TryGetComponent(out InteractiveRandomEventObject randomEventObject))
                _randomEventPanel.Show();
        }

        private bool CheckForInventoryOverload(InteractiveObject interactiveObject)
        {
            int currentSize = _inventory.InventoryPresenter.InventoryModel.TotalSize;
            int maxSize = _inventory.InventoryPresenter.InventoryModel.MaxSize;

            if (interactiveObject.NumberOfAwards + currentSize > maxSize)
            {
                int difference = interactiveObject.NumberOfAwards + currentSize - maxSize;
                _warningMessage.ShowMessage(difference.ToString());
                UserResponse?.Invoke(false);

                return true;
            }
            
            return false;
        }

        private void CallResponse(bool answer)
        {
            UserResponse?.Invoke(answer);
        }
    }
}