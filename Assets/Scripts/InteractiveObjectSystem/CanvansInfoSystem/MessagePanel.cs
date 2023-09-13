using System;
using Assets.Enemy;
using Assets.Loot;
using UnityEngine;

namespace Assets.Scripts.InteractiveObjectSystem.CanvasInfoSystem
{
    public class MessagePanel : MonoBehaviour
    {
        [SerializeField] private EnemyInfoView _enemyInfoPanel;
        [SerializeField] private RandomEventView _randomEventPanel;
        [SerializeField] private LootInfoView _randomLootPanel;
        
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
            gameObject.SetActive(true);
            if (interactiveObject.TryGetComponent(out EnemyView enemyView))
                _enemyInfoPanel.Show(enemyView.EnemyPresenter);
            else if (interactiveObject.TryGetComponent(out InteractiveLootObject lootObject))
                _randomLootPanel.Show(lootObject);
            else if (interactiveObject.TryGetComponent(out InteractiveRandomEventObject randomEventObject))
                _randomEventPanel.Show();
        }

        private void CallResponse(bool answer)
        {
            UserResponse?.Invoke(answer);
        }
    }
}