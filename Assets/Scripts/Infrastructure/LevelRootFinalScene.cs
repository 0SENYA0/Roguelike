using Assets.Scripts.SoundSystem;
using Assets.UI;
using UnityEngine;

namespace Assets.Infrastructure
{
    public class LevelRootFinalScene : MonoBehaviour
    {
        [SerializeField] private SoundService _sound;

        private void Start()
        {
            _sound.Play();
            Curtain.Instance.HideCurtain();
        }
    }
}