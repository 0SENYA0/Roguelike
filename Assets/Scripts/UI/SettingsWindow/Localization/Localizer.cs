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
            _localization.SetCurrentLanguage(GameRoot.Instance.CurrentLocalization);
            GameRoot.Instance.OnLanguageChange += ChangeLanguage;
        }

        private void OnDestroy()
        {
            GameRoot.Instance.OnLanguageChange -= ChangeLanguage;
        }

        private void ChangeLanguage(string language)
        {
            _localization.SetCurrentLanguage(language);
        }
    }
}