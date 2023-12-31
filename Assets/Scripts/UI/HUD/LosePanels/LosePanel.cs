using System;
using Assets.Infrastructure;
using Assets.ScriptableObjects;
using Assets.Scripts.SoundSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.HUD.LosePanels
{
    public class LosePanel : MonoBehaviour
    {
        [SerializeField] private SoundService _loseSound;
        [SerializeField] private LevelRoot _levelRoot;
        [Header("Final Loss")] [SerializeField] private GameObject _finalLossPanel;
        [SerializeField] private Button _finalLossButton;
        [SerializeField] private TMP_Text _finalLossText;
        [SerializeField] private LocalizedText _localizedFinalLoss;
        [Header("Idol")] [SerializeField] private RebirthSuggestionPanel _idolPanel;
        [Header("AD")] [SerializeField] private RebirthSuggestionPanel _adPanel;
        
        public event Action<UserLossAnswers> UserAnswerEvent; 

        public void Show(bool isBoosFight)
        {
            _loseSound.Play();

            if (isBoosFight)
                BackToMenu();
            else if (Game.GameSettings.PlayerData.Idol > 0)
                OfferRebornByIdol();
            else if (_levelRoot.IsPossibleToRebornForAd)
                OfferRebornByAd();
            else
                BackToMenu();           
        }

        public void Hide()
        {
            _idolPanel.gameObject.SetActive(false);
            _adPanel.gameObject.SetActive(false);
            _finalLossPanel.SetActive(false);
        }

        private void BackToMenu()
        {
            _finalLossPanel.SetActive(true);
            _finalLossButton.onClick.AddListener(OnButtonClicked);
            _finalLossText.text = _localizedFinalLoss.GetLocalization(Game.GameSettings.CurrentLocalization);
        }

        private void OfferRebornByIdol()
        {
            _idolPanel.gameObject.SetActive(true);
            _idolPanel.Init();
            _idolPanel.UserAnswerEvent += ProcessUserAnswer;
        }

        private void OfferRebornByAd()
        {
            _adPanel.gameObject.SetActive(true);
            _adPanel.Init();
            _adPanel.UserAnswerEvent += ProcessUserAnswer;
        }

        private void ProcessUserAnswer(UserLossAnswers answer)
        {
            _idolPanel.UserAnswerEvent -= ProcessUserAnswer;
            _adPanel.UserAnswerEvent -= ProcessUserAnswer;
            UserAnswerEvent?.Invoke(answer);
        }

        private void OnButtonClicked()
        {
            _finalLossButton.onClick.RemoveListener(OnButtonClicked);
            UserAnswerEvent?.Invoke(UserLossAnswers.Menu);
        }
    }
}