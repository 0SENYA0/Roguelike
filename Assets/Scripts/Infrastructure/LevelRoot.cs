using System;
using Assets.Infrastructure.DataStorageSystem;
using Assets.Infrastructure.SceneLoadHandler;
using Assets.Inventory.ItemGeneratorSystem;
using Assets.Person;
using Assets.Scripts.GenerationSystem;
using Assets.Scripts.SoundSystem;
using Assets.TimerSystem;
using Assets.UI;
using Assets.UI.HUD;
using IJunior.TypedScenes;
using UnityEngine;

namespace Assets.Infrastructure
{
    public class LevelRoot : MonoBehaviour
    {
        [SerializeField] private int _levelNumber;
        [SerializeField] private ItemGenerator _itemGenerator;
        [SerializeField] private ProceduralGeneration _generation;
        [SerializeField] private Timer _gameTimer;
        [SerializeField] private SoundComponent _levelSound;
        [SerializeField] private PlayerInfo _playerInfo;
        [Space] 
        [SerializeField] private PlayerView _player;

        private int _numberOfEnemiesKilled;
        private bool _isPossibleToRebornForAd;

        public bool IsPossibleToRebornForAd => _isPossibleToRebornForAd;
        public int LevelNumber => _levelNumber;

        // private void Start()
        // {
        //     _generation.GenerateLevel();
        //     _isPossibleToRebornForAd = true;
        //     Invoke(nameof(HideCurtain), 2f);
        // }

        public void Init(PlayerLevelData playerLevelData)
        {
            _itemGenerator.Init(_levelNumber);
            
            if (playerLevelData == null)
                _player.Init();
            else
                _player.Init(playerLevelData.Health, playerLevelData.Inventory);
            
            //_generation.GenerateLevel();
            _isPossibleToRebornForAd = true;
            Invoke(nameof(HideCurtain), 2f);
        }

        public void PauseGlobalMap()
        {
            _gameTimer.Pause();
            _levelSound.Stop();
        }

        public void UnpauseGlobalMap()
        {
            _gameTimer.StartTimer();
            _levelSound.Play();
        }

        public void IncreaseKilledEnemies(int count)
        {
            _numberOfEnemiesKilled += count;
        }

        public void LoadMainMenu()
        {
            SaveEntries(false);
            Curtain.Instance.ShowAnimation(() =>
            {
                Menu.Load();
            });
        }

        public void LoadNextLevel()
        {
            SaveEntries(true);
            Curtain.Instance.ShowAnimation(() =>
            {
                LevelLoadingChooser.LoadScene(
                    _levelNumber + 1, 
                    new PlayerLevelData(_player.PlayerPresenter.Player.Health, 
                        _player.PlayerPresenter.Player.InventoryPresenter));
            });
        }

        public void RebornWithAd()
        {
            _isPossibleToRebornForAd = false;
        }

        private void SaveEntries(bool isBossKilled)
        {
            if (Game.GameSettings == null)
                return;

            var gameStats = Game.GameSettings.PlayerData.GameStatistics;
            var newGameStats = new GameStatistics(
                gameStats.NumberOfAttempts,
                gameStats.NumberOfEnemiesKilled + _numberOfEnemiesKilled,
                gameStats.NumberOfBossesKilled + Convert.ToInt32(isBossKilled)
                );

            Game.GameSettings.PlayerData.GameStatistics = newGameStats;
            Game.GameSettings.PlayerData.SaveData();
        }

        private void HideCurtain()
        {
            _generation.GenerateLevel();
            Curtain.Instance.HideCurtain();
            _gameTimer.StartTimer();
            _levelSound.Play();
            _playerInfo.Init(_player);
        }
    }
}