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
        [SerializeField] private LevelRoot _levelRoot;
        [SerializeField] private LevelsStatsScriptableObject _levelsStats;
        [SerializeField] private ElementsParticleScriptableObject _elementsParticle;

        private RandomParameterGenerator _random;
        private ListOfItemStatsRangesScriptableObject _stats;

        public static ItemGenerator Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            _random = new RandomParameterGenerator(_elementsParticle);

            var currentLevelStats = _levelsStats.ListStats.FirstOrDefault(x => x.LevelNumber == _levelRoot.LevelNumber);

            if (currentLevelStats == null)
                throw new Exception($"There are no item characteristics for this level: {_levelRoot.LevelNumber}!");

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
                (int)_random.RandomValue(dws.SplashChance.MinValue + weaponLevel, dws.SplashChance.MaxValue + weaponLevel),
                (int)_random.RandomValue(dws.CriticalChance.MinValue + weaponLevel, dws.CriticalChance.MaxValue + weaponLevel),
                (int)_random.RandomValue(dws.DamageModifier.MinValue + weaponLevel, dws.DamageModifier.MaxValue + weaponLevel),
                _random.RandomParticle(randomElement));
            
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
            int splashChance = (int)_random.RandomValue(stat.SplashChance);
            int criticalChance = (int)_random.RandomValue(stat.CriticalChance);
            int damageModifier = (int)_random.RandomValue(stat.DamageModifier);
            ParticleSystem particle = _random.RandomParticle(element);

            return new Weapon.Weapon(damage, element, splashChance, criticalChance, damageModifier, particle);
        }

        private Armor CreateRandomArmor(ListOfItemStatsRangesScriptableObject.ArmorStat stat)
        {
            var randomElement = _random.RandomElement();
            var head = new Head(_random.RandomValue(stat.HeadStatValue));
            var body = new Body(_random.RandomValue(stat.BodyStatValue), randomElement);

            return new Armor(body, head, _random.RandomParticle(randomElement));
        }
    }

    public class InventoryItem
    {
        public InventoryItem(Armor armor, Weapon.Weapon weapon)
        {
            Armor = armor;
            Weapon = weapon;
        }

        public Armor Armor { get; private set; }
        public Weapon.Weapon Weapon { get; private set; }
    }
}