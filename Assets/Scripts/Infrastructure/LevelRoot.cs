using Assets.Scripts.GenerationSystem;
using Assets.UI;
using UnityEngine;

namespace Assets.Infrastructure
{
    public class LevelRoot : MonoBehaviour
    {
        [SerializeField] private ProceduralGeneration _generation;

        private void Start()
        {
            _generation.GenerateLevel();
            Curtain.Instance.HideCurtain();
        }
    }
}