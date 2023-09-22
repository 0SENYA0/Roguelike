using System.Collections.Generic;
using Assets.UI.ShopWindow;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ShopItems", menuName = "ScriptableObject/ShopItems", order = 0)]
    public class ShopItemScriptableObject : ScriptableObject
    {
        [SerializeField] private List<Item> _shopItems;

        public IReadOnlyList<Item> ShopItems => _shopItems;
        
        [System.Serializable]
        public class Item
        {
            [SerializeField] private ShopItemType _itemType;
            [FormerlySerializedAs("_information")] [SerializeField] private LocalizedText text;
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
}