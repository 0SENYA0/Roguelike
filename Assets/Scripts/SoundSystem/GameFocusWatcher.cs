using System;
using UnityEngine;
//using Agava.WebUtility;

namespace Assets.Scripts.SoundSystem
{
    public class GameFocusWatcher : IDisposable
    {
        // TODO: Загрузить фикс для Agava
        private GameFocusWatcher()
        {
            //WebApplication.InBackgroundChangeEvent += ChangeBackgroundSounds;
        }
        
        public bool IsHidden { get; private set; }

        public void Dispose()
        {
            //WebApplication.InBackgroundChangeEvent -= ChangeBackgroundSounds;
        }

        private void ChangeBackgroundSounds(bool hidden)
        {
            IsHidden = hidden;
        }
    }
}