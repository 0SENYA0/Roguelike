using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.ShopWindow
{
	public class ShopItem : MonoBehaviour, IDisposable
	{
		[SerializeField] private TMP_Text _itemInformation;
		[SerializeField] private TMP_Text _currentLevel;
		[SerializeField] private TMP_Text _cost;
		[SerializeField] private Image _image;
		[SerializeField] private Button _bueButton;

		private ShopItemType _itemType;
		private int _price;

		public event Action<ShopItemType, int> OnBueItemEvent;

		public ShopItemType ItemType => _itemType;

		public void Init(string information, string level, string cost, int price, Sprite image, ShopItemType itemType)
		{
			_itemInformation.text = information;
			_currentLevel.text = level;
			_price = price;
			_cost.text = cost;
			_image.sprite = image;
			_itemType = itemType;
			_bueButton.onClick.AddListener(OnItemBueClicked);
		}

		public void UpdateInformation(string information, string level, string cost, int price)
		{
			_itemInformation.text = information;
			_currentLevel.text = level;
			_price = price;
			_cost.text = cost;
		}

		public void Dispose() =>
			_bueButton.onClick.AddListener(OnItemBueClicked);

		private void OnItemBueClicked() =>
			OnBueItemEvent?.Invoke(_itemType, _price);
	}
}