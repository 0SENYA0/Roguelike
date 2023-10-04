using Assets.Config;
using Assets.Infrastructure;
using Assets.Person;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.HUD
{
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] private Image _heart;
        [SerializeField] private TMP_Text _playerName;

        private PlayerView _player;

        private void OnDestroy()
        {
            if (_player != null)
                _player.PlayerPresenter.Player.HealthChanged -= OnHealthChang;
        }

        public void Init(PlayerView player)
        {
            _player = player;
            _player.PlayerPresenter.Player.HealthChanged += OnHealthChang;
            OnHealthChang(_player.PlayerPresenter.Player.Health);

            if (Game.GameSettings.PlayerName != string.Empty)
                _playerName.text = Game.GameSettings.PlayerName;
            else
                _playerName.text = LanguageConfig.GetAnonymous(Game.GameSettings.CurrentLocalization);
        }

        private void OnHealthChang(float value)
        {
            float currentHeartsCount = value / PlayerHealth.MaxPlayerHealth;
            _heart.fillAmount = currentHeartsCount;
        }
    }
}