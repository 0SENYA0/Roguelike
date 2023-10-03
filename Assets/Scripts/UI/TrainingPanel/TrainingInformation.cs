using System.Collections.Generic;
using Assets.ScriptableObjects;
using Assets.UI.SettingsWindow.Localization;
using UnityEngine;

namespace Assets.UI.TrainingPanel
{
    [CreateAssetMenu(fileName = "TrainingInformation", menuName = "ScriptableObject/TrainingInformation", order = 0)]
    public class TrainingInformation : ScriptableObject
    {
        [SerializeField] private List<Info> _information;

        public List<Info> Information => _information;

        [System.Serializable]
        public class Info
        {
            [SerializeField] private TrainingStep _step;
            [SerializeField] private LocalizedSprite _sprite;
            [SerializeField] private LocalizedText _label;
            [SerializeField] private LocalizedText _text;

            public TrainingStep Step => _step;
            public LocalizedSprite Sprite => _sprite;
            public LocalizedText Label => _label;
            public LocalizedText Text => _text;
        }
        
        [System.Serializable]
        public class LocalizedSprite
        {
            [SerializeField] private Sprite _rus;
            [SerializeField] private Sprite _eng;
            [SerializeField] private Sprite _tur;
            
            public Sprite GetLocalization(string lang)
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
                        return _eng;
                }
            }
        }
    }
}