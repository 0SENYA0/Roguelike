using System;
using Assets.Infrastructure;
using Assets.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.HUD.LosePanels
{
	public class RebirthSuggestionPanel : MonoBehaviour
	{
		[SerializeField] private Button _buttonYes;
		[SerializeField] private Button _buttonNo;
		[SerializeField] private TMP_Text _text;
		[SerializeField] private LocalizedText _localizedPanel;
		[SerializeField] private UserLossAnswers _answers;

		public event Action<UserLossAnswers> UserAnswerEvent;

		public void Init()
		{
			_text.text = _localizedPanel.GetLocalization(Game.GameSettings.CurrentLocalization);
			_buttonYes.onClick.AddListener(ProcessResponseYes);
			_buttonNo.onClick.AddListener(ProcessResponseNo);
		}

		private void ProcessResponseYes() =>
			ProcessResponse(_answers);

		private void ProcessResponseNo() =>
			ProcessResponse(UserLossAnswers.Menu);

		private void ProcessResponse(UserLossAnswers userAnswer)
		{
			_buttonYes.onClick.RemoveListener(ProcessResponseYes);
			_buttonNo.onClick.RemoveListener(ProcessResponseNo);
			UserAnswerEvent?.Invoke(userAnswer);
		}
	}
}