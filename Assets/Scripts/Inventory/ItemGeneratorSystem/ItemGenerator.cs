using System;
using System.Linq;
using Assets.DefendItems;
using Assets.Fight.Element;
using Assets.Infrastructure;
using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.Inventory.ItemGeneratorSystem
{
    public class ItemGenerator : MonoBehaviour
    {
        [SerializeField] private LevelsStatsScriptableObject _levelsStats;
        [SerializeField] private ElementsParticleScriptableObject _elementsParticle;

        private RandomParameterGenerator _random;
        private ListOfItemStatsRangesScriptableObject _stats;

        public static ItemGenerator Instance { get; private set; }

        public void Init(int levelNumber)
        {
            Instance = this;
            _random = new RandomParameterGenerator(_elementsParticle);

            var currentLevelStats = _levelsStats.ListStats.FirstOrDefault(x => x.LevelNumber == levelNumber);

            if (currentLevelStats == null)
                throw new ArgumentException($"There are no item characteristics for this level: {levelNumber}!");

            _stats = currentLevelStats.List;
        }

        public InventoryItem GetDefaultInventory()
        {
            int armorLevel = 1;
            
            if (Game.GameSettings != null)
                armorLevel = Game.GameSettings.PlayerData.ArmorLevel;
            
            Element randomElement = _random.RandomElement();
            var head = new Head(_random.RandomValue(_stats.DefaultArmorStat.HeadStatValue.MinValue + armorLevel,
                _stats.DefaultArmorStat.HeadStatValue.MaxValue + armorLevel));
            var body = new Body(_random.RandomValue(_stats.DefaultArmorStat.BodyStatValue.MinValue + armorLevel,
                _stats.DefaultArmorStat.HeadStatValue.MaxValue + armorLevel), randomElement);
            var newArmor = new Armor(body, head, _random.RandomParticle(randomElement));

            int weaponLevel = 1;
            
            if (Game.GameSettings != null)
                weaponLevel = Game.GameSettings.PlayerData.WeaponLevel;
            
            randomElement = _random.RandomElement();
            var dws = _stats.DefaultWeaponStat;
            var newWeapon = new Weapon.Weapon(
                _random.RandomValue(dws.Damage.MinValue + weaponLevel, dws.Damage.MaxValue + weaponLevel),
                randomElement,
                _random.RandomDice(),
                _random.RandomDice(),
                _random.RandomDice());
            
            return new InventoryItem(newArmor, newWeapon);
        }

        public Armor GetRandomArmor(bool isBossLoot = false)
        {
            return CreateRandomArmor(isBossLoot ? _stats.BossLootArmor : _stats.LevelArmor);
        }

        public Weapon.Weapon GetRandomWeapon(bool isBossLoot = false)
        {
            return CreateRandomWeapon(isBossLoot ? _stats.BossLootWeapon : _stats.LevelWeapon);
        }

        public int GetEnemyReward()
        {
            return (int)_random.RandomValue(_stats.EnemyReward);
        }

        public int GetBossReward()
        {
            return (int)_random.RandomValue(_stats.BossReward);
        }

        private Weapon.Weapon CreateRandomWeapon(ListOfItemStatsRangesScriptableObject.WeaponStat stat)
        {
            float damage = _random.RandomValue(stat.Damage);
            Element element = _random.RandomElement();
            int splashChance = _random.RandomDice();
            int criticalChance = _random.RandomDice();
            int damageModifier = _random.RandomDice();

            return new Weapon.Weapon(damage, element, splashChance, criticalChance, damageModifier);
        }

        private Armor CreateRandomArmor(ListOfItemStatsRangesScriptableObject.ArmorStat stat)
        {
            var randomElement = _random.RandomElement();
            var head = new Head(_random.RandomValue(stat.HeadStatValue));
            var body = new Body(_random.RandomValue(stat.BodyStatValue), randomElement);

            return new Armor(body, head, _random.RandomParticle(randomElement));
        }
    }

}