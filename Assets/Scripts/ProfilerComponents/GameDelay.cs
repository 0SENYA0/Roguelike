using UnityEngine;

namespace Assets.ProfilerComponents
{
    public class GameDelay : MonoBehaviour
    {
        public float Delay { get; private set; }

        private void Update()
        {
            Delay = Time.deltaTime * 1000;
        }
    }
}