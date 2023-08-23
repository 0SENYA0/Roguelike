using System;
using Agava.WebUtility;
using Assets.Infrastructure.DataStorageSystem;

namespace Assets.Scripts.SoundSystem
{
    public class Sound: ISound, IDisposable
    {
        private readonly IPlayerData _playerData;
        
        public bool IsMusicOn { get => _isMusicOn; }
        public bool IsSfxOn { get => _isSfxOn; }

        public event Action<bool> OnMusicStateChanged;
        public event Action<bool> OnSfxStateChanged;

        private bool _isMusicOn;
        private bool _isSfxOn;


        public Sound(IPlayerData playerData)
        {
            _playerData = playerData;
            
            _isMusicOn = playerData.IsMusicOn;
            _isSfxOn = playerData.IsSfxOn;

            WebApplication.InBackgroundChangeEvent += ChangeBackgroundSounds;
        }

        public void UpdateSoundSettings(SoundType type)
        {
            if (type == SoundType.Music)
            {
                _isMusicOn = _playerData.IsMusicOn;
                OnMusicStateChanged?.Invoke(_isMusicOn);
            }
            else
            {
                _isSfxOn = _playerData.IsSfxOn;
                OnSfxStateChanged?.Invoke(_isSfxOn);
            }
        }

        public void Pause()
        {
            OnMusicStateChanged?.Invoke(false);
            OnSfxStateChanged?.Invoke(false);
        }

        public void UpPause()
        {
            OnMusicStateChanged?.Invoke(true);
            OnSfxStateChanged?.Invoke(true);
        }

        public void Dispose()
        {
            WebApplication.InBackgroundChangeEvent -= ChangeBackgroundSounds;
        }
        
        private void ChangeBackgroundSounds(bool hidden)
        {
            if (hidden)
                Pause();
            else
                UpPause();
        }
    }
}
