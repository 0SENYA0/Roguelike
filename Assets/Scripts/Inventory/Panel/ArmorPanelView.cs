using System;
using System.Collections.Generic;
using Assets.DefendItems;
using UnityEngine;

namespace Assets.Inventory.Panel
{
	public class ArmorPanelView : MonoBehaviour
	{
		[SerializeField] private ArmorPanelItem _template;
		[SerializeField] private Transform _container;

		private List<ArmorPanelItem> _items = new List<ArmorPanelItem>();

		public Action<IInventoryItem> RemoveItem;

		public Action<IInventoryItem> SelectItem;

		public void Show(IEnumerable<Armor> armors)
		{
			foreach (Armor armor in armors)
			{
				ArmorPanelItem newItem = Instantiate(_template, _container).GetComponent<ArmorPanelItem>();
				newItem.Init(armor);
				newItem.OnItemClicked += OnItemRemove;
				newItem.OnItemUse += OnItemUse;
				_items.Add(newItem);
			}
		}

		public void Hide()
		{
			foreach (ArmorPanelItem item in _items)
			{
				if (item == null)
					continue;

				item.OnItemClicked -= OnItemRemove;
				item.OnItemUse -= OnItemUse;
				item.OnDispose();
			}

			_items = new List<ArmorPanelItem>();
		}

		private void OnItemRemove(IInventoryItem obj) =>
			RemoveItem?.Invoke(obj);

		private void OnItemUse(IInventoryItem obj)
		{
			SelectItem?.Invoke(obj);

			foreach (ArmorPanelItem item in _items)
				item.CheckSelect();
		}
	}
}