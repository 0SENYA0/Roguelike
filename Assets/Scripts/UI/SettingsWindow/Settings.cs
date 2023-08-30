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
        [SerializeField] private Sprite _soundImageOn;
        [SerializeField] private Sprite _soundImageOff;

        private void OnEnable()
        {
            _musicButton.Icon.sprite = Game.GameSettings.Sound.IsMusicOn ? _soundImageOn : _soundImageOff;
            _sfxButton.Icon.sprite = Game.GameSettings.Sound.IsSfxOn ? _soundImageOn : _soundImageOff;
            
            _musicButton.Button.onClick.AddListener(OnMusicClickChange);
            _sfxButton.Button.onClick.AddListener(OnSfxClickChange);
        }

        private void OnDisable()
        {
            _musicButton.Button.onClick.RemoveListener(OnMusicClickChange);
            _sfxButton.Button.onClick.RemoveListener(OnSfxClickChange);
        }

        private void OnMusicClickChange()
        {
            Game.GameSettings.ChangeSoundSettings(SoundType.Music);
            _musicButton.Icon.sprite = Game.GameSettings.Sound.IsMusicOn ? _soundImageOn : _soundImageOff;
        }

        private void OnSfxClickChange()
        {
            Game.GameSettings.ChangeSoundSettings(SoundType.SFX);
            _sfxButton.Icon.sprite = Game.GameSettings.Sound.IsSfxOn ? _soundImageOn : _soundImageOff;
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