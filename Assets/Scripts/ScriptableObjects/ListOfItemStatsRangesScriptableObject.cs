using UnityEngine;

namespace Assets.ScriptableObjects
{
	[CreateAssetMenu(fileName = "ListOfItemStatsRanges", menuName = "ScriptableObject/ListOfItemStatsRanges", order = 0)]
	public class ListOfItemStatsRangesScriptableObject : ScriptableObject
	{
		[SerializeField] private WeaponStat _defaultWeaponStat;
		[SerializeField] private WeaponStat _levelWeapon;
		[SerializeField] private WeaponStat _bossLootWeapon;
		[Space] [SerializeField] private ArmorStat _defaultArmorStat;
		[SerializeField] private ArmorStat _levelArmor;
		[SerializeField] private ArmorStat _bossLootArmor;
		[Space] [SerializeField] private Range _enemyReward;
		[SerializeField] private Range _bossReward;

		public WeaponStat DefaultWeaponStat => _defaultWeaponStat;

		public WeaponStat LevelWeapon => _levelWeapon;

		public WeaponStat BossLootWeapon => _bossLootWeapon;

		public ArmorStat DefaultArmorStat => _defaultArmorStat;

		public ArmorStat LevelArmor => _levelArmor;

		public ArmorStat BossLootArmor => _bossLootArmor;

		public Range EnemyReward => _enemyReward;

		public Range BossReward => _bossReward;
	}
}