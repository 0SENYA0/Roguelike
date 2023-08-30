using Assets.Infrastructure;
using Lean.Localization;
using UnityEngine;

namespace Assets.UI.SettingsWindow.Localization
{
    [RequireComponent(typeof(LeanLocalization))]
    public class Localizer : MonoBehaviour
    {
        private LeanLocalization _localization;

        private void Awake()
        {
            _localization = GetComponent<LeanLocalization>();
            _localization.SetCurrentLanguage(Game.GameSettings.CurrentLocalization);
            Game.GameSettings.OnLanguageChange += ChangeLanguage;
        }

        private void OnDestroy()
        {
            Game.GameSettings.OnLanguageChange -= ChangeLanguage;
        }

        private void ChangeLanguage(string language)
        {
            _localization.SetCurrentLanguage(language);
        }
    }
}