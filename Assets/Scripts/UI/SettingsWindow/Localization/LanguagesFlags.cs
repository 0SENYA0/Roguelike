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
            foreach (var flag in _flags)
            {
                flag.FlagClicked += OnFlagClick;
                flag.SetIconActive(false);
            }
        }

        private void OnEnable()
        {
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
            foreach (var flag in _flags)
            {
                flag.FlagClicked += OnFlagClick;
                flag.SetIconActive(false);
            }
        }

        private void OnFlagClick(Flag clickedFlag, string selectedLanguage)
        {
            foreach (var flag in _flags)
            {
                flag.SetIconActive(false);
            }
            
            clickedFlag.SetIconActive(true);
            Game.GameSettings.ChangeLocalization(selectedLanguage);
        }
    }
}