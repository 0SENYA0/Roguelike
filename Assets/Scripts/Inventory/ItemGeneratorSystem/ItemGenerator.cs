using System;
using System.Linq;
using Assets.DefendItems;
using Assets.Fight.Element;
using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.Inventory.ItemGeneratorSystem
{
    public class ItemGenerator : MonoBehaviour
    {
        [SerializeField] private int _levelNumber;
        [SerializeField] private LevelsStatsScriptableObject _levelsStats;
        [SerializeField] private ElementsParticleScriptableObject _elementsParticle;

        private RandomParameterGenerator _random;
        private ListOfItemStatsRangesScriptableObject _stats;

        public static ItemGenerator Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            _random = new RandomParameterGenerator(_elementsParticle);

            var currentLevelStats = _levelsStats.ListStats.FirstOrDefault(x => x.LevelNumber == _levelNumber);

            if (currentLevelStats == null)
                throw new Exception($"There are no item characteristics for this level: {_levelNumber}!");

            _stats = currentLevelStats.List;
        }

        public InventoryItem GetDefaultInventory()
        {
            var newArmor = CreateRandomArmor(_stats.DefaultArmorStat);
            var newWeapon = CreateRandomWeapon(_stats.DefaultWeaponStat);

            return new InventoryItem(newArmor, newWeapon);
        }

        public Armor GetRandomArmor()
        {
            return CreateRandomArmor(_stats.LevelArmor);
        }

        public Weapon.Weapon GetRandomWeapon()
        {
            return CreateRandomWeapon(_stats.LevelWeapon);
        }

        public InventoryItem GetBossLoot()
        {
            var newArmor = CreateRandomArmor(_stats.BossLootArmor);
            var newWeapon = CreateRandomWeapon(_stats.BossLootWeapon);

            return new InventoryItem(newArmor, newWeapon);
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