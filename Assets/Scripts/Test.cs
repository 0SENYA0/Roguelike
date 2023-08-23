using System;
using Assets.Infrastructure;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.SoundSystem;

namespace DefaultNamespace
{
    public class Test : MonoBehaviour
    {
        private void OnEnable()
        {
            GameRoot.Instance.Sound.Pause();
        }

        private void OnDisable()
        {
            GameRoot.Instance.Sound.UpPause();
        }
    }

}