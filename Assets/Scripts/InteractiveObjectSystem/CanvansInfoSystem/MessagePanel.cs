using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.InteractiveObjectSystem.CanvasInfoSystem
{
    public class MessagePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _lable;
        [SerializeField] private TMP_Text _enemyInfo;
        [SerializeField] private TMP_Text _fullInfo;
        [SerializeField] private Image _elementImage;
        [SerializeField] private ElementsSpriteView _elementsSprite;
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

        public void ShowPanel(ObjectType objectType, InteractiveObjectData data)
        {
            _enemyInfo.gameObject.SetActive(false);
            _fullInfo.gameObject.SetActive(false);
            _elementImage.gameObject.SetActive(false);
            
            switch (objectType)
            {
                case ObjectType.Enemy:
                    ShowEnemyPanel(data);
                    break;
                case ObjectType.RandomEvent:
                    ShowRandomEventPanel(data);
                    break;
                case ObjectType.Loot:
                    ShowRandomEventPanel(data);
                    break;
                case ObjectType.Boos:
                    ShowBossPanel(data);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            _panel.SetActive(true);
        }

        private void ShowRandomEventPanel(InteractiveObjectData data)
        {
            _lable.text = data.Name;
            
            _fullInfo.text = data.Data;
            _fullInfo.gameObject.SetActive(true);
        }

        private void ShowEnemyPanel(InteractiveObjectData data)
        {
            _lable.text = data.Name;
            
            _enemyInfo.text = data.Data;
            _enemyInfo.gameObject.SetActive(true);

            _elementImage.sprite = _elementsSprite.GetElementSprite(data.Element);
            _elementImage.gameObject.SetActive(true);
        }

        private void ShowBossPanel(InteractiveObjectData data)
        {
            _lable.text = $"BOSS: {data.Name}";
            
            _enemyInfo.text = data.Data;
            _enemyInfo.gameObject.SetActive(true);

            _elementImage.sprite = _elementsSprite.GetElementSprite(data.Element);
            _elementImage.gameObject.SetActive(true);
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