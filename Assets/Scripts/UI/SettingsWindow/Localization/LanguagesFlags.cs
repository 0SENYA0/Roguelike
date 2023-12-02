using System.Collections.Generic;
using Assets.Infrastructure;
using UnityEngine;

namespace Assets.UI.SettingsWindow.Localization
{
    public class LanguagesFlags : MonoBehaviour
    {
        [SerializeField] private List<Flag> _flags;

        private void Awake()
        {
            foreach (Flag flag in _flags)
            {
                flag.FlagClicked += OnFlagClick;
                flag.SetIconActive(false);
            }
        }

        private void OnEnable()
        {
            if (Game.GameSettings == null) 
                return;
            
            foreach (var flag in _flags)
            {
                if (flag.Language == Game.GameSettings.CurrentLocalization)
                {
                    flag.SetIconActive(true);
                    break;
                }
            }
        }

        private void OnDisable()
        {
            foreach (Flag flag in _flags)
            {
                flag.FlagClicked += OnFlagClick;
                flag.SetIconActive(false);
            }
        }

        private void OnFlagClick(Flag clickedFlag, string selectedLanguage)
        {
            foreach (Flag flag in _flags)
                flag.SetIconActive(false);

            clickedFlag.SetIconActive(true);
            
            if (Game.GameSettings == null) 
                return;
            
            Game.GameSettings.ChangeLocalization(selectedLanguage);
        }
    }
}