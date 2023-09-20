using Assets.Person;
using Assets.Scripts.GenerationSystem;
using Assets.Scripts.SoundSystem;
using Assets.TimerSystem;
using Assets.UI;
using Assets.UI.HUD;
using UnityEngine;

namespace Assets.Infrastructure
{
    public class LevelRoot : MonoBehaviour
    {
        [SerializeField] private ProceduralGeneration _generation;
        [SerializeField] private Timer _gameTimer;
        [SerializeField] private SoundComponent _levelSound;
        [SerializeField] private PlayerInfo _playerInfo;
        [Space] 
        [SerializeField] private PlayerView _player;

        private void Start()
        {
            _generation.GenerateLevel();
            Invoke(nameof(HideCurtain), 1f);
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

        private void HideCurtain()
        {
            Curtain.Instance.HideCurtain();
            _gameTimer.StartTimer();
            _levelSound.Play();
            _playerInfo.Init(_player);
        }
    }
}