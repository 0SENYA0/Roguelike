using System;
using Assets.Infrastructure.SceneLoadHandler;
using Assets.Scripts.SoundSystem;
using Assets.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Infrastructure
{
    public class LevelRootFinalScene : LevelRootBase
    {
        [SerializeField] private SoundComponent _sound;
        
        private void Start()
        {
            _sound.Play();
            Curtain.Instance.HideCurtain();
        }

        public override void Init(PlayerLevelData playerLevelData)
        {
            
        }
    }
}