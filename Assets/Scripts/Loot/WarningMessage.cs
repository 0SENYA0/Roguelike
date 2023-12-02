using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Loot
{
	public class WarningMessage : MonoBehaviour
	{
		[SerializeField] private Button _close;
		[SerializeField] private TMP_Text _text;

		private void OnEnable() =>
			_close.onClick.AddListener(CloseWindow);

		private void OnDisable() =>
			_close.onClick.RemoveListener(CloseWindow);

		public void ShowMessage(string difference)
		{
			gameObject.SetActive(true);
			string oldText = _text.text;
			string newText = $"{oldText} {difference})";
			_text.text = newText;
		}

		private void CloseWindow() =>
			gameObject.SetActive(false);
	}
}