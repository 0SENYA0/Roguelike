using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Infrastructure;
using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.UI.ShopWindow
{
	public class ShopItemPanel : MonoBehaviour, IDisposable
	{
		[SerializeField] private ShopItem _template;
		[SerializeField] private Transform _contentContainer;
		[SerializeField] private ShopItemScriptableObject _itemScriptable;

		private readonly List<ShopItem> _shopItems = new List<ShopItem>();
		private string _lang;

		public event Action<ShopItemType, int> BueItemEvent;

		public void Init()
		{
			_lang = Game.GameSettings.CurrentLocalization;

			foreach (Item item in _itemScriptable.ShopItems)
			{
				ShopItem newShopItem = Instantiate(_template, _contentContainer).GetComponent<ShopItem>();

				string information = item.Text.GetLocalization(_lang);
				string level = $"{item.Level.GetLocalization(_lang)}: {GetPlayerData(item.ItemType)}";

				int price = item.Price.GetLevelCost(GetPlayerData(item.ItemType));
				string cost;

				if (item.Price.HasNextPrice(GetPlayerData(item.ItemType)))
				{
					cost = $"{item.Cost.GetLocalization(_lang)}: {price}$";
					newShopItem.OnBueItemEvent += OnBueItem;
				}
				else
				{
					cost = $"{item.Cost.GetLocalization(_lang)}: ---";
				}

				newShopItem.Init(information, level, cost, price, item.Image, item.ItemType);

				_shopItems.Add(newShopItem);
			}
		}

		public void UpdateItemInformation(ShopItemType type)
		{
			ShopItem item = _shopItems.FirstOrDefault(x => x.ItemType == type);
			Item shopItem = _itemScriptable.ShopItems.FirstOrDefault(x => x.ItemType == type);

			if (item == null || shopItem == null)
				throw new Exception("This product or show item is not available on the store panel");

			int price = shopItem.Price.GetLevelCost(GetPlayerData(shopItem.ItemType));
			string cost = shopItem.Price.HasNextPrice(GetPlayerData(shopItem.ItemType))
				? $"{shopItem.Cost.GetLocalization(_lang)}: {price}$"
				: $"{shopItem.Cost.GetLocalization(_lang)}: ---";

			if (shopItem.Price.HasNextPrice(GetPlayerData(shopItem.ItemType)) == false)
				item.OnBueItemEvent -= OnBueItem;

			item.UpdateInformation(shopItem.Text.GetLocalization(_lang),
				$"{shopItem.Level.GetLocalization(_lang)}: {GetPlayerData(shopItem.ItemType)}",
				cost,
				price);
		}

		public void Dispose()
		{
			foreach (ShopItem item in _shopItems)
			{
				item.OnBueItemEvent -= OnBueItem;
				item.Dispose();
			}
		}

		public void UpdateAllItemInformation()
		{
			_lang = Game.GameSettings.CurrentLocalization;

			foreach (Item shopItem in _itemScriptable.ShopItems)
			{
				string information = shopItem.Text.GetLocalization(_lang);
				string level = $"{shopItem.Level.GetLocalization(_lang)}: {GetPlayerData(shopItem.ItemType)}";
				int price = shopItem.Price.GetLevelCost(GetPlayerData(shopItem.ItemType));
				string cost = shopItem.Price.HasNextPrice(GetPlayerData(shopItem.ItemType))
					? $"{shopItem.Cost.GetLocalization(_lang)}: {price}$"
					: $"{shopItem.Cost.GetLocalization(_lang)}: ---";

				ShopItem item = _shopItems.FirstOrDefault(x => x.ItemType == shopItem.ItemType);

				if (item == null)
					throw new Exception("This product is not available on the store panel");

				if (shopItem.Price.HasNextPrice(GetPlayerData(shopItem.ItemType)) == false)
					item.OnBueItemEvent -= OnBueItem;

				item.UpdateInformation(information, level, cost, price);
			}
		}

		private void OnBueItem(ShopItemType item, int price) =>
			BueItemEvent?.Invoke(item, price);

		private int GetPlayerData(ShopItemType type)
		{
			switch (type)
			{
				case ShopItemType.Armor:
					return Game.GameSettings.PlayerData.ArmorLevel;
				case ShopItemType.Weapon:
					return Game.GameSettings.PlayerData.WeaponLevel;
				case ShopItemType.Potion:
					return Game.GameSettings.PlayerData.Potion;
				case ShopItemType.Idol:
					return Game.GameSettings.PlayerData.Idol;
			}

			return 0;
		}
	}
}