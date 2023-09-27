using UnityEngine;

namespace Assets.ProfilerComponents
{
    public class MemoryUsage : MonoBehaviour
    {
        private readonly float _timeToResetData = 5f;
        
        private float _timer;
        
        public float Memory { get; private set; }

        private void Update()
        {
            _timer -= Time.deltaTime;
            
            if (_timer <= 0)
            {
                Memory = UnityEngine.Profiling.Profiler.GetTotalAllocatedMemoryLong() / 1048576L;
                _timer += _timeToResetData;
            }
        }
    }
}