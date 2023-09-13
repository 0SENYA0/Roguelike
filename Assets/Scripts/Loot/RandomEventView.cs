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

        // protected override void CallPositiveResponse()
        // {
        //     base.CallPositiveResponse();
        //     
        //     if (Random.Range(0, 2) == 0)
        //     {
        //         _test212?.Invoke(_randomEvent.InteractiveLootObject);
        //         //randomEvent.EnemyPresenter ShowLootPanel(randomEventObject.InteractiveLootObject);
        //         Debug.Log("show loot panel");
        //     }
        //     else
        //     {
        //         _test2?.Invoke(_randomEvent.GetRandomEnemyPresenter());
        //         //ShowEnemyPanel(randomEventObject.EnemyView);
        //         Debug.Log("show enemy panel");
        //     }
        // }
    }
}