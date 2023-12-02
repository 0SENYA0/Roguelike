using System;
using Agava.WebUtility;
using Assets.Infrastructure.DataStorageSystem;

namespace Assets.Scripts.SoundSystem
{
	public class Sound : ISound, IDisposable
	{
		private readonly IPlayerData _playerData;

		private bool _isMusicOn;
		private bool _isSfxOn;

		public Sound(IPlayerData playerData)
		{
			_playerData = playerData;

			_isMusicOn = playerData.IsMusicOn;
			_isSfxOn = playerData.IsSfxOn;

			WebApplication.InBackgroundChangeEvent += ChangeBackgroundSounds;
		}

		public event Action<bool> OnMusicStateChanged;

		public event Action<bool> OnSfxStateChanged;

		public event Action<bool> OnPauseStateChanged;

		public bool IsMusicOn => _isMusicOn;

		public bool IsSfxOn => _isSfxOn;

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

		public void Pause() =>
			OnPauseStateChanged?.Invoke(true);

		public void UpPause() =>
			OnPauseStateChanged?.Invoke(false);

		public void Dispose() =>
			WebApplication.InBackgroundChangeEvent -= ChangeBackgroundSounds;

		private void ChangeBackgroundSounds(bool hidden)
		{
			if (hidden)
				Pause();
			else
				UpPause();
		}
	}
}