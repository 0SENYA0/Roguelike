using System;
using Assets.Fight.Element;
using UnityEngine;

namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WeaponSO", menuName = "ScriptableObject/Weapon", order = 0)]
    public class WeaponScriptableObject : ScriptableObject
    {
        [SerializeField] private int chanceToSplash;
        [SerializeField] private int minValueToCriticalDamage;
        [SerializeField] private int valueModifier;
        
        [Space(10)]
        [SerializeField] private DamageData damageData;

        [Serializable]
        public class DamageData
        {
            [SerializeField] private Element _element;
            [SerializeField] private float _damage;
        }
    }
}