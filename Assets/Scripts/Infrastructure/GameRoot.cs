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
        [SerializeField] private bool _loadMainMenu = false;
        
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
            if (_loadMainMenu == false)
                return;

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
            _sound = new Sound();
            callback?.Invoke();
        }

#if UNITY_EDITOR
        private IEnumerator ArtificialDelay()
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("Menu");
        }
#endif        
    }
}