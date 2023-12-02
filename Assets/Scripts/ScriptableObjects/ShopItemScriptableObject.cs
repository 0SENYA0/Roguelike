using System.Collections.Generic;
using UnityEngine;

namespace Assets.ScriptableObjects
{
	[CreateAssetMenu(fileName = "ShopItems", menuName = "ScriptableObject/ShopItems", order = 0)]
	public class ShopItemScriptableObject : ScriptableObject
	{
		[SerializeField] private List<Item> _shopItems;

		public IReadOnlyList<Item> ShopItems => _shopItems;
	}
}