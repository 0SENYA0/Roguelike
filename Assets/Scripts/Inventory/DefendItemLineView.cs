using System;
using Assets.Scripts.UI.Widgets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Inventory
{
    public class DefendItemLineView : MonoBehaviour
    {
        [SerializeField] private CustomButton _customButton;
        [SerializeField] private Image _elementImage;
        [SerializeField] private TMP_Text _bodyDamage;
        [SerializeField] private TMP_Text _headDamage;

        private void OnEnable()
        {
            
        }

        public CustomButton CustomButton => _customButton;

        public Image ElementImage => _elementImage;

        public TMP_Text BodyDamage => _bodyDamage;

        public TMP_Text HeadDamage => _headDamage;
    }
}