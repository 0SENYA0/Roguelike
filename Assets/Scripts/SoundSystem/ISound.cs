using System;

namespace Assets.Scripts.SoundSystem
{
	public interface ISound
	{
		event Action<bool> OnMusicStateChanged;

		event Action<bool> OnSfxStateChanged;

		event Action<bool> OnPauseStateChanged;

		bool IsMusicOn { get; }

		bool IsSfxOn { get; }

		void Pause();

		void UpPause();
	}
}