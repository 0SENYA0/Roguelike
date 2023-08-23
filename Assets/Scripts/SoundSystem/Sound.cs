using System;
using Agava.WebUtility;
using Assets.Infrastructure;

namespace Assets.Scripts.SoundSystem
{
    public class Sound: ISound, IDisposable
    {
        public bool IsMusicOn { get => _isMusicOn; }
        public bool IsSfxOn { get => _isSfxOn; }
        public bool IsHidden { get => _isHiddenOn; }
        
        public event Action<bool> OnMusicStateChanged; 
        public event Action<bool> OnSfxStateChanged; 
        public event Action<bool> OnHiddenStateChanged;

        private bool _isMusicOn;
        private bool _isSfxOn;
        private bool _isHiddenOn;

        public Sound()
        {
            _isMusicOn = GameRoot.Instance.UserData.IsSoundOn;
            _isSfxOn = GameRoot.Instance.UserData.IsSfxOn;
            _isHiddenOn = true;
            
            WebApplication.InBackgroundChangeEvent += ChangeBackgroundSounds;
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
            _isHiddenOn = hidden;
            OnHiddenStateChanged?.Invoke(_isHiddenOn);
        }
    }
}
