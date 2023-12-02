using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.UI.SettingsWindow.Localization
{
	[RequireComponent(typeof(Button))]
	public class Flag : MonoBehaviour
	{
		[SerializeField] private GameObject _iconCheck;
		[SerializeField] private string _language;

		private Button _button;

		public event UnityAction<Flag, string> FlagClicked;

		public string Language => _language;

		private void Awake()
		{
			_button = GetComponent<Button>();
			_button.onClick.AddListener(OnClickFlag);
		}

		private void OnDestroy() =>
			_button.onClick.RemoveListener(OnClickFlag);

		public void SetIconActive(bool iconEnabled) =>
			_iconCheck.SetActive(iconEnabled);

		private void OnClickFlag() =>
			FlagClicked?.Invoke(this, _language);
	}
}