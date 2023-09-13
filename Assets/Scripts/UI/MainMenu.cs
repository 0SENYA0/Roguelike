using Assets.Scripts.SoundSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private SoundComponent _sound;
        [SerializeField] private GameObject _buttonGroups;
        [SerializeField] private Button _startGame;
        [SerializeField] private MenuButtonItem _settings;
        [SerializeField] private MenuButtonItem _training;
        [SerializeField] private MenuButtonItem _shop;
        
        
        private void Start()
        {
            _sound.Play();
            Curtain.Instance.HideCurtain();
            _buttonGroups.SetActive(true);
        }

        private void OnEnable()
        {
            _startGame.onClick.AddListener(StartGame);
            _settings.Init();
            _training.Init();
            _shop.Init();
        }

        private void OnDisable()
        {
            _startGame.onClick.RemoveListener(StartGame);
            _settings.Dispose();
            _training.Dispose();
            _shop.Dispose();
        }

        private void StartGame()
        {
            _sound.Stop();
            Curtain.Instance.ShowAnimation(() => { SceneManager.LoadScene("LevelGeneration");});
        }
    }

    [System.Serializable]
    public class MenuButtonItem
    {
        [SerializeField] private Button _show;
        [SerializeField] private Button _hide;
        [SerializeField] private GameObject _window;

        public void Init()
        {
            _show.onClick.AddListener(ShowWindow);
            _hide.onClick.AddListener(HideWindow);
        }

        public void Dispose()
        {
            _show.onClick.RemoveListener(ShowWindow);
            _hide.onClick.RemoveListener(HideWindow);
        }

        private void ShowWindow()
        {
            _window.SetActive(true);
        }

        private void HideWindow()
        {
            _window.SetActive(false);
        }
    }
}