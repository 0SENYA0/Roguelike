using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI
{
	[Serializable]
	public class MenuButtonItem
	{
		[SerializeField] private Button _show;
		[SerializeField] private Button _hide;
		[SerializeField] private GameObject _window;

		public void Init()
		{
			_show.onClick.AddListener(ShowWindow);
			_hide.onClick.AddListener(HideWindow);
		}

		public void Dispose()
		{
			_show.onClick.RemoveListener(ShowWindow);
			_hide.onClick.RemoveListener(HideWindow);
		}

		private void ShowWindow() =>
			_window.SetActive(true);

		private void HideWindow() =>
			_window.SetActive(false);
	}
}