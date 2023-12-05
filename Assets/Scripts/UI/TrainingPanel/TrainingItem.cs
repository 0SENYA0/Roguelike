using System;
using System.Linq;
using Assets.Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.TrainingPanel
{
	public class TrainingItem : MonoBehaviour
	{
		[SerializeField] private TrainingInformation _information;
		[SerializeField] private TrainingStep _step;
		[Space] [SerializeField] private TMP_Text _text;
		[SerializeField] private Image _image;

		private string _label;

		public string Label => _label;

		private void OnEnable()
		{
			BacgroundInfo bacgroundInfo = _information.Information.FirstOrDefault(x => x.Step == _step);

			if (bacgroundInfo == null)
				throw new Exception($"There is no information about this ({_step}) training step");

			string lang = Game.GameSettings.CurrentLocalization;

			_label = bacgroundInfo.Label.GetLocalization(lang);
			_text.text = bacgroundInfo.Text.GetLocalization(lang);
			_image.sprite = bacgroundInfo.Sprite.GetLocalization(lang);
		}
	}
}