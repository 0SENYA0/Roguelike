using Assets.Scripts.GenerationSystem;
using Assets.TimerSystem;
using Assets.UI;
using Assets.UI.SettingsWindow.Localization;
using UnityEngine;

namespace Assets.Infrastructure
{
    public class LevelRoot : MonoBehaviour
    {
        [SerializeField] private ProceduralGeneration _generation;
        [SerializeField] private Timer _gameTimer;

        private void Start()
        {
            _generation.GenerateLevel();
            Invoke(nameof(HideCurtain), 1f);
        }

        private void HideCurtain()
        {
            Curtain.Instance.HideCurtain();
            _gameTimer.StartTimer();
        }
    }
}