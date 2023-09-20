using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.HUD
{
    public class LosePanel : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;

        public event Action OnButtonClickEvent;
        
        public void Show(string message)
        {
            gameObject.SetActive(true);
            _button.onClick.AddListener(OnButtonClicked);
            _text.text = message;
        }

        private void OnButtonClicked()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
            OnButtonClickEvent?.Invoke();
        }
    }
}