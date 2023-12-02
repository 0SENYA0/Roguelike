using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
	[Serializable]
	public class AttackAndDefendView
	{
		[SerializeField] private Image _attactElement;
		[SerializeField] private Image _attactIcon;
		[SerializeField] private Image _defendElement;
		[SerializeField] private Image _defendIcon;

		public Image AttactElement => _attactElement;

		public Image DefendElement => _defendElement;

		public void Show()
		{
			_attactElement.gameObject.SetActive(true);
			_attactIcon.gameObject.SetActive(true);
			_defendElement.gameObject.SetActive(true);
			_defendIcon.gameObject.SetActive(true);
		}

		public void Hide()
		{
			_attactElement.gameObject.SetActive(false);
			_attactIcon.gameObject.SetActive(false);
			_defendElement.gameObject.SetActive(false);
			_defendIcon.gameObject.SetActive(false);
		}
	}
}