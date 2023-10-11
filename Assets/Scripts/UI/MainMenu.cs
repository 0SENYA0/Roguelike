using Agava.YandexGames;
using Assets.Infrastructure;
using Assets.Infrastructure.DataStorageSystem;
using Assets.Infrastructure.SceneLoadHandler;
using Assets.Scripts.SoundSystem;
using Assets.UI.ShopWindow;
using Assets.YandexAds;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private YandexAdView _yandexAd;
        [SerializeField] private SoundComponent _sound;
        [SerializeField] private GameObject _buttonGroups;
        [SerializeField] private Button _startGame;
        [SerializeField] private MenuButtonItem _settings;
        [SerializeField] private MenuButtonItem _training;
        [SerializeField] private MenuButtonItem _shop;
        [SerializeField] private ShopPanel _shopPanel;

        private void Start()
        {
            YandexGamesSdk.GameReady();
            
            _sound.Play();
            Curtain.Instance.HideCurtain();
            _buttonGroups.SetActive(true);
            _shopPanel.Init();
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
            AddNewTrying();
            _sound.Stop();
            _shopPanel.Dispose();
            _yandexAd.ShowInterstitialAd(LoadFirstLevel);
        }

        private void LoadFirstLevel()
        {
            Curtain.Instance.ShowAnimation(() =>
            {
                LevelLoadingChooser.LoadScene(1, null);
            });
        }

        private void AddNewTrying()
        {
            if (Game.GameSettings == null)
                return;

            var gameStats = Game.GameSettings.PlayerData.GameStatistics;
            var newGameStats = new GameStatistics(
                gameStats.NumberOfAttempts + 1,
                gameStats.NumberOfEnemiesKilled,
                gameStats.NumberOfBossesKilled
            );

            Game.GameSettings.PlayerData.GameStatistics = newGameStats;
            Game.GameSettings.PlayerData.SaveData();
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