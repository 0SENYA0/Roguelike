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

            if (Game.GameSettings != null)
            {
                _localization.SetCurrentLanguage(Game.GameSettings.CurrentLocalization);
                Game.GameSettings.OnLanguageChange += ChangeLanguage;
            }
            else
            {
                _localization.SetCurrentLanguage(Language.RUS);
            }
        }

        private void OnDestroy()
        {
            if (Game.GameSettings != null)
                Game.GameSettings.OnLanguageChange -= ChangeLanguage;
        }

        private void ChangeLanguage(string language) =>
            _localization.SetCurrentLanguage(language);
    }
}