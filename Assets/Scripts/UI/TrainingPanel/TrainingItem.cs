using System;
using System.Linq;
using Assets.Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.TrainingPanel
{
    public class TrainingItem : MonoBehaviour
    {
        [SerializeField] private TrainingInformation _information;
        [SerializeField] private TrainingStep _step;
        [Space]
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _image;

        private string _label;

        public string Label => _label;

        private void OnEnable()
        {
            var info = _information.Information.FirstOrDefault(x => x.Step == _step);

            if (info == null)
                throw new Exception($"There is no information about this ({_step}) training step");

            var lang = Game.GameSettings.CurrentLocalization;
            
            _label = info.Label.GetLocalization(lang);
            _text.text = info.Text.GetLocalization(lang);
            _image.sprite = info.Sprite.GetLocalization(lang);
        }
    }
}