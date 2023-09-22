using Assets.UI.SettingsWindow.Localization;
using UnityEngine;

namespace Assets.ScriptableObjects
{
    [System.Serializable]
    public class LocalizedText
    {
        [TextArea][SerializeField] private string _rus;
        [TextArea][SerializeField] private string _eng;
        [TextArea][SerializeField] private string _tur;

        public string GetLocalization(string lang)
        {
            switch (lang)
            {
                case Language.RUS:
                    return _rus;
                case Language.ENG:
                    return _eng;
                case Language.TUR:
                    return _tur;
                default:
                    return "%ERROR IN LOCALIZATION%";
            }
        }
    }
}