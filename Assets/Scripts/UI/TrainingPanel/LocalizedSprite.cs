using Assets.UI.SettingsWindow.Localization;
using System;
using UnityEngine;

namespace Assets.UI.TrainingPanel
{
	[Serializable]
	public class LocalizedSprite
	{
		[SerializeField] private Sprite _rus;
		[SerializeField] private Sprite _eng;
		[SerializeField] private Sprite _tur;

		public Sprite GetLocalization(string lang)
		{
			switch (lang)
			{
				case Language.RUS:
					return _rus;
				case Language.ENG:
					return _eng;
				case Language.TUR:
					return _tur;
				default:
					return _eng;
			}
		}
	}
}