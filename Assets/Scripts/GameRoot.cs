using System;
using System.Collections;
using Assets.Infrastructure.DataStorageSystem;
using Assets.Scripts.SoundSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameRoot : MonoBehaviour
    {
        public static GameRoot Instance { get; private set; }

        public ISound Sound => _sound;
        public UserData UserData => _userData;

        private Sound _sound;
        private UserData _userData;

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
            SceneManager.LoadScene("Menu");
        }
        
        private void Initialization(Action callback)
        {
            _sound = new Sound();
            _userData = new UserData();
            callback?.Invoke();
            
#if UNITY_EDITOR
            StartCoroutine(ArtificialDelay());
#endif
        }

#if UNITY_EDITOR
        private IEnumerator ArtificialDelay()
        {
            yield return new WaitForSeconds(2f);
        }
#endif        
    }
}