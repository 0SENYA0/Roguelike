using System;
using System.Collections;
using Assets.Infrastructure.DataStorageSystem;
using Assets.Scripts.SoundSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Infrastructure
{
    public class GameRoot : MonoBehaviour
    {
        public static GameRoot Instance { get; private set; }

        public ISound Sound => _sound;
        public PlayerData PlayerData => _playerData;

        private Sound _sound;
        private PlayerData _playerData;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                Initialization(LoadMainMenu);
                return;
            }
            
            Destroy(this.gameObject);
        }

        private void OnDestroy()
        {
            _sound.Dispose();
        }

        private void LoadMainMenu()
        {
            if (Application.isEditor)
            {
                StartCoroutine(ArtificialDelay());
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }
        }
        
        private void Initialization(Action callback)
        {
            _playerData = new PlayerData();
            _sound = new Sound(_playerData);
            callback?.Invoke();
        }

#if UNITY_EDITOR
        private IEnumerator ArtificialDelay()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Menu");
        }
#endif

        public void ChangeSoundSettings(SoundType type)
        {
            if (type == SoundType.Music)
                _playerData.IsMusicOn = !_playerData.IsMusicOn;
            else
                _playerData.IsSfxOn = !_playerData.IsSfxOn;
            
            _playerData.SaveData();
            _sound.UpdateSoundSettings(type);
        }

        [ContextMenu("[!] TestChangeBackgroundSounds")]
        private void Test()
        {
            _sound.TestChangeBackgroundSounds();
        }
    }
}