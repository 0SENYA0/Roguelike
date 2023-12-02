using System;
using Assets.Infrastructure;
using UnityEngine;

namespace Assets.UI.ShopWindow
{
	public class ShopPanel : MonoBehaviour, IDisposable
	{
		[SerializeField] private ShopItemPanel _shopItemPanel;
		[SerializeField] private PlayerShopInfo _playerShopInfo;

		private void OnEnable()
		{
			_shopItemPanel.UpdateAllItemInformation();
			_playerShopInfo.UpdateInfo();
		}

		public void Init()
		{
			_shopItemPanel.Init();
			_shopItemPanel.BueItemEvent += OnBueItemEvent;

			_playerShopInfo.UpdateInfo();
		}

		public void Dispose() =>
			_shopItemPanel.Dispose();

		private void OnBueItemEvent(ShopItemType itemForBue, int price)
		{
			if (Game.GameSettings.TryBueItem(itemForBue, price))
			{
				_shopItemPanel.UpdateItemInformation(itemForBue);
				_playerShopInfo.UpdateInfo();
			}
		}
	}
}