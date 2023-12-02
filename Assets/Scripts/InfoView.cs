using System;
using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
	public abstract class InfoView : MonoBehaviour
	{
		[SerializeField] private Button _buttonYes;
		[SerializeField] private Button _buttonNo;

		public event Action<bool> UserResponse;

		private void Awake()
		{
			_buttonYes.onClick.AddListener(CallPositiveResponse);
			_buttonNo.onClick.AddListener(CallNegativeResponse);
		}

		private void OnDestroy()
		{
			_buttonYes.onClick.RemoveListener(CallPositiveResponse);
			_buttonNo.onClick.RemoveListener(CallNegativeResponse);
		}

		protected virtual void CallPositiveResponse()
		{
			UserResponse?.Invoke(true);
			gameObject.SetActive(false);
		}

		protected virtual void CallNegativeResponse()
		{
			UserResponse?.Invoke(false);
			gameObject.SetActive(false);
		}

		protected string GetLocalizedText(string key) =>
			LeanLocalization.GetTranslation(key).Data.ToString();
	}
}