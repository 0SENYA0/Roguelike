using System;
using Assets.UI.ShopWindow;
using UnityEngine;

namespace Assets.ScriptableObjects
{
	[Serializable]
	public class Item
	{
		[SerializeField] private ShopItemType _itemType;
		[SerializeField] private LocalizedText text;
		[SerializeField] private LocalizedText _level;
		[SerializeField] private LocalizedText _cost;
		[SerializeField] private Sprite _image;
		[SerializeField] private ItemPriceScriptableObject _price;

		public ShopItemType ItemType => _itemType;

		public LocalizedText Text => text;

		public LocalizedText Level => _level;

		public LocalizedText Cost => _cost;

		public Sprite Image => _image;

		public ItemPriceScriptableObject Price => _price;
	}
}