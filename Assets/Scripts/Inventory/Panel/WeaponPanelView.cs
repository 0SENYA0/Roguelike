using System;
using System.Collections.Generic;
using Assets.Interface;
using UnityEngine;

namespace Assets.Inventory.Panel
{
	public class WeaponPanelView : MonoBehaviour
	{
		[SerializeField] private WeaponPanelItem _template;
		[SerializeField] private Transform _container;

		private List<WeaponPanelItem> _items = new List<WeaponPanelItem>();

		public Action<IInventoryItem> RemoveItem;

		public void Show(IEnumerable<IWeapon> weapons)
		{
			foreach (IWeapon weapon in weapons)
			{
				WeaponPanelItem newItem = Instantiate(_template, _container).GetComponent<WeaponPanelItem>();
				newItem.Init(weapon);
				newItem.OnItemClicked += OnItemRemove;
				_items.Add(newItem);
			}
		}

		public void Hide()
		{
			foreach (WeaponPanelItem item in _items)
			{
				if (item == null)
					continue;

				item.OnItemClicked -= OnItemRemove;
				item.OnDispose();
			}

			_items = new List<WeaponPanelItem>();
		}

		private void OnItemRemove(IInventoryItem obj) =>
			RemoveItem?.Invoke(obj);
	}
}