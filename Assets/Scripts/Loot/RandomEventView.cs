using Assets.Config;
using TMPro;
using UnityEngine;

namespace Assets.Loot
{
    public class RandomEventView : InfoView
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Text _data;

        public void Show()
        {
            gameObject.SetActive(true);
            
            _label.text = GetLocalizedText(LanguageConfig.RandomEventKey);
            _data.text = GetLocalizedText(LanguageConfig.EnemyOrLootKey);
        }
    }
}