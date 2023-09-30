using System.Collections.Generic;
using Assets.Person;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.HUD
{
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] private List<Image> _heart;
        
        private PlayerView _player;

        public void Init(PlayerView player)
        {
            _player = player;
            _player.PlayerPresenter.Player.HealthChanged += OnHealthChang;
            OnHealthChang(_player.PlayerPresenter.Player.Health);
        }

        private void OnDestroy()
        {
            if (_player != null)
                _player.PlayerPresenter.Player.HealthChanged -= OnHealthChang;
        }

        private void OnHealthChang(float value)
        {
            int currentHeartsCount = ((int)value) / 100 + 1;

            foreach (var heart in _heart)
            {
                heart.gameObject.SetActive(currentHeartsCount > 0);
                currentHeartsCount--;
            }
        }
    }
}