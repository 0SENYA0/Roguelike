using System;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Image _selectableImage;
        [SerializeField] private Slider _healthBarSlider;
        [SerializeField] private Image _skin;

        private void OnEnable() => 
            PlayerSingleAttackObserver.Instance.Register(ShowSelectableImage);

        private void OnDisable() => 
            PlayerSingleAttackObserver.Instance.Unregister(ShowSelectableImage);

        private void ShowSelectableImage()
        {
            _selectableImage.gameObject.SetActive(true);
        }
    }
}