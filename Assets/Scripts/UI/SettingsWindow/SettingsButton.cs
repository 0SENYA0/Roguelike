using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.SettingsWindow
{
	[Serializable]
	public class SettingsButton
	{
		[SerializeField] private Button _button;
		[SerializeField] private Image _icon;

		public Button Button => _button;

		public Image Icon => _icon;
	}
}