using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ItemPrice", menuName = "ScriptableObject/ItemPrice", order = 0)]
    public class ItemPriceScriptableObject : ScriptableObject
    {
        [SerializeField] private List<Price> _priceList;
        [SerializeField] private int _maxLevel;

        public bool HasNextPrice(int level)
        {
            return level < _maxLevel;
        }
        
        public int GetLevelCost(int level)
        {
            if (_priceList.Count == 1)
                return _priceList[0].NextPrice;

            var price = _priceList.FirstOrDefault(x => x.Level == level);

            return price?.NextPrice ?? _priceList[^1].NextPrice;
        }
        
        [System.Serializable]
        public class Price
        {
            [SerializeField] private int _level;
            [SerializeField] private int _nextPrice;

            public int Level => _level;
            public int NextPrice => _nextPrice;
        }
    }
}