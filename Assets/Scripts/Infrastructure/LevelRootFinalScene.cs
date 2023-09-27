using System;
using Assets.Scripts.SoundSystem;
using Assets.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Infrastructure
{
    public class LevelRootFinalScene : MonoBehaviour
    {
        [SerializeField] private SoundComponent _sound;
        

        private void Start()
        {
            _sound.Play();
            Curtain.Instance.HideCurtain();
        }
    }
}