using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GenerationSystem.LevelMovement
{
    public class CanvasInfoPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _lable;
        [SerializeField] private TMP_Text _stats;
        [SerializeField] private Button _buttonGo;
        [SerializeField] private Button _buttonNot;
        [SerializeField] private GameObject _panel;

        public event Action<bool> UserResponse;

        private void Awake()
        {
            _buttonGo.onClick.AddListener(CallPositiveResponse);
            _buttonNot.onClick.AddListener(CallNegativeResponse);
            _panel.SetActive(false);
        }

        private void OnDestroy()
        {
            _buttonGo.onClick.RemoveListener(CallPositiveResponse);
            _buttonNot.onClick.RemoveListener(CallNegativeResponse);
        }

        public void ShowPanel(InteractiveObject interactiveObject)
        {
            _lable.text = interactiveObject.Name;
            _stats.text = interactiveObject.Stats;
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