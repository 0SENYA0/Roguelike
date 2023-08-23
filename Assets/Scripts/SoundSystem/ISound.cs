using System;

namespace Assets.Scripts.SoundSystem
{
    public interface ISound
    {
        bool IsMusicOn { get; }
        bool IsSfxOn { get; }

        event Action<bool> OnMusicStateChanged; 
        event Action<bool> OnSfxStateChanged;
        event Action<bool> OnPauseStateChanged;

        void Pause();
        void UpPause();
    }
}