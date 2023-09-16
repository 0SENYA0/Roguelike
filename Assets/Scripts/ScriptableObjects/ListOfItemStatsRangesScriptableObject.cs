using UnityEngine;

namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ListOfItemStatsRanges", menuName = "ScriptableObject/ListOfItemStatsRanges",
        order = 0)]
    public class ListOfItemStatsRangesScriptableObject : ScriptableObject
    {
        [SerializeField] private WeaponStat _defaultWeaponStat;
        [SerializeField] private WeaponStat _levelWeapon;
        [SerializeField] private WeaponStat _bossLootWeapon;
        [Space] 
        [SerializeField] private ArmorStat _defaultArmorStat;
        [SerializeField] private ArmorStat _levelArmor;
        [SerializeField] private ArmorStat _bossLootArmor;

        public WeaponStat DefaultWeaponStat => _defaultWeaponStat;
        public WeaponStat LevelWeapon => _levelWeapon;
        public WeaponStat BossLootWeapon => _bossLootWeapon;

        public ArmorStat DefaultArmorStat => _defaultArmorStat;
        public ArmorStat LevelArmor => _levelArmor;
        public ArmorStat BossLootArmor => _bossLootArmor;

        [System.Serializable]
        public class WeaponStat
        {
            [SerializeField] private Range _damage;
            [SerializeField] private Range _SplashChance;
            [SerializeField] private Range _CriticalChance;
            [SerializeField] private Range _DamageModifier;

            public Range Damage => _damage;
            public Range SplashChance => _SplashChance;
            public Range CriticalChance => _CriticalChance;
            public Range DamageModifier => _DamageModifier;
        }

        [System.Serializable]
        public class ArmorStat
        {
            [SerializeField] private Range _headStatValue;
            [SerializeField] private Range _bodyStatValue;

            public Range HeadStatValue => _headStatValue;
            public Range BodyStatValue => _bodyStatValue;
        }

        [System.Serializable]
        public class Range
        {
            [SerializeField] private float _minValue;
            [SerializeField] private float _maxValue;

            public float MinValue => _minValue;
            public float MaxValue => _maxValue;
        }
    }
}