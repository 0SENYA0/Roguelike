using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.InteractiveObjectSystem
{
    public class CanvasInfoPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _lable;
        [SerializeField] private TMP_Text _stats;
        [SerializeField] private Button _buttonYes;
        [SerializeField] private Button _buttonNo;
        [SerializeField] private GameObject _panel;

        public event Action<bool> UserResponse;

        private void Awake()
        {
            _buttonYes.onClick.AddListener(CallPositiveResponse);
            _buttonNo.onClick.AddListener(CallNegativeResponse);
            _panel.SetActive(false);
        }

        private void OnDestroy()
        {
            _buttonYes.onClick.RemoveListener(CallPositiveResponse);
            _buttonNo.onClick.RemoveListener(CallNegativeResponse);
        }

        public void ShowPanel(IInteractiveObject interactiveObject)
        {
            var objectInformation = interactiveObject.GetData();
            _lable.text = objectInformation.Name;
            _stats.text = objectInformation.Data;
            _panel.SetActive(true);
        }

        private void CallPositiveResponse()
        {
            UserResponse?.Invoke(true);
            _panel.SetActive(false);
        }

        private void CallNegativeResponse()
        {
            UserResponse?.Invoke(false);
            _panel.SetActive(false);
        }
    }
}