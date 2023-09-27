using UnityEngine;

namespace Assets.ProfilerComponents
{
    public class FPSCounter : MonoBehaviour
    {
        public int FPS { get; private set; }
        
        private float _deltaTime = 0.0f;

        private void Update()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
            FPS = Mathf.RoundToInt(1.0f / _deltaTime);
        }
    }
}