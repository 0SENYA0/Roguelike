using UnityEngine;

namespace Assets.TimerSystem
{
    public class Timer : MonoBehaviour
    {
        private float _time;
        private bool _isRun;

        public float TimePerSeconds => _time;

        private void Update()
        {
            if (_isRun == false)
                return;

            _time += Time.deltaTime;
        }

        public void StartTimer() =>
            _isRun = true;

        public void Pause() =>
            _isRun = false;

        public void Reset() =>
            _time = 0f;
    }
}