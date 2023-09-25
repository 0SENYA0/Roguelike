using Assets.Infrastructure;
using Assets.Scripts.SoundSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.SettingsWindow
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private SettingsButton _musicButton;
        [SerializeField] private SettingsButton _sfxButton;
        [SerializeField] private Button _resetPlayerData;
        [SerializeField] private Sprite _soundImageOn;
        [SerializeField] private Sprite _soundImageOff;

        private void OnEnable()
        {
            if (Game.GameSettings != null)
            {
                _musicButton.Icon.sprite = Game.GameSettings.Sound.IsMusicOn ? _soundImageOn : _soundImageOff;
                _sfxButton.Icon.sprite = Game.GameSettings.Sound.IsSfxOn ? _soundImageOn : _soundImageOff;
            }            
            
            if (_resetPlayerData != null)
                _resetPlayerData.onClick.AddListener(ResetPlayerData);
            _musicButton.Button.onClick.AddListener(OnMusicClickChange);
            _sfxButton.Button.onClick.AddListener(OnSfxClickChange);
        }

        private void OnDisable()
        {
            if (_resetPlayerData != null)
                _resetPlayerData.onClick.RemoveListener(ResetPlayerData);
            _musicButton.Button.onClick.RemoveListener(OnMusicClickChange);
            _sfxButton.Button.onClick.RemoveListener(OnSfxClickChange);
        }

        private void OnMusicClickChange()
        {
            if (Game.GameSettings == null) 
                return;
            
            Game.GameSettings.ChangeSoundSettings(SoundType.Music);
            _musicButton.Icon.sprite = Game.GameSettings.Sound.IsMusicOn ? _soundImageOn : _soundImageOff;
        }

        private void OnSfxClickChange()
        {
            if (Game.GameSettings == null) 
                return;
            
            Game.GameSettings.ChangeSoundSettings(SoundType.SFX);
            _sfxButton.Icon.sprite = Game.GameSettings.Sound.IsSfxOn ? _soundImageOn : _soundImageOff;
        }

        private void ResetPlayerData()
        {
            if (Game.GameSettings == null)
                return;

            Game.GameSettings.PlayerData.ResetData();
            Game.GameSettings.PlayerData.SaveData();
        }
    }

    [System.Serializable]
    public class SettingsButton
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;

        public Button Button => _button;
        public Image Icon => _icon;
    }
}