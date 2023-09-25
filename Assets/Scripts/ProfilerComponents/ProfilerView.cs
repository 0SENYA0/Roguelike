using TMPro;
using UnityEngine;

namespace Assets.ProfilerComponents
{
    [RequireComponent(typeof(FPSCounter))]
    [RequireComponent(typeof(MemoryUsage))]
    [RequireComponent(typeof(GameDelay))]
    public class ProfilerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _fps;
        [SerializeField] private TMP_Text _memory;
        [SerializeField] private TMP_Text _delay;

        private FPSCounter _fpsCounter;
        private MemoryUsage _memoryUsage;
        private GameDelay _gameDelay;

        private void Awake()
        {
            _fpsCounter = GetComponent<FPSCounter>();
            _memoryUsage = GetComponent<MemoryUsage>();
            _gameDelay = GetComponent<GameDelay>();
        }

        private void Update()
        {
            _fps.text = $"FPS: {_fpsCounter.FPS}";
            _memory.text = $"Memory: {_memoryUsage.Memory} Mb";
            _delay.text = $"Delay: {_gameDelay.Delay} ms";
        }
    }
}