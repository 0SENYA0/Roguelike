using System;
using Assets.Infrastructure.DataStorageSystem;
using Assets.Scripts.SoundSystem;
using Assets.UI.SettingsWindow.Localization;

namespace Assets.Infrastructure
{
    public class GameSettings: IDisposable
    {
        private Sound _sound;
        private PlayerData _playerData;

        public event Action<string> OnLanguageChange;

        public GameSettings()
        {
            Initialization();
        }

        public ISound Sound => _sound;
        public PlayerData PlayerData => _playerData;
        public string CurrentLocalization => _playerData.Localization;

        public void Dispose()
        {
            _sound.Dispose();
            _playerData.SaveData();
        }

        private void Initialization()
        {
            _playerData = new PlayerData();
            _sound = new Sound(_playerData);
        }

        public void ChangeSoundSettings(SoundType type)
        {
            if (type == SoundType.Music)
                _playerData.IsMusicOn = !_playerData.IsMusicOn;
            else
                _playerData.IsSfxOn = !_playerData.IsSfxOn;
            
            _playerData.SaveData();
            _sound.UpdateSoundSettings(type);
        }

        public void ChangeLocalization(string lang)
        {
            _playerData.Localization = lang;
            _playerData.SaveData();
            OnLanguageChange?.Invoke(lang);
        }
    }
}