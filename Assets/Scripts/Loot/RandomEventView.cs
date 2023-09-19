using TMPro;
using UnityEngine;


namespace Assets.Loot
{
    public class RandomEventView : InfoView
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Text _data;

        private const string LabelKey = "RandomEvent";
        private const string DataKey = "EnemyOrLoot";

        public void Show()
        {
            gameObject.SetActive(true);
            
            _label.text = GetLocalizedText(LabelKey);
            _data.text = GetLocalizedText(DataKey);
        }
    }
}