using Assets.Infrastructure;
using Assets.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Assets.YandexLeaderboard
{
	public class AuthorizationMessage : InfoView
	{
		[SerializeField] private LocalizedText _localizedText;
		[SerializeField] private TMP_Text _text;

		public void Show() =>
			_text.text = _localizedText.GetLocalization(Game.GameSettings.CurrentLocalization);
	}
}