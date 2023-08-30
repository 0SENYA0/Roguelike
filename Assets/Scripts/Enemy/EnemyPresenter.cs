using System.Collections.Generic;
using Assets.DefendItems;
using Assets.Fight.Element;
using Assets.Interface;
using Assets.Scripts.InteractiveObjectSystem;
using Assets.Weapon;
using UnityEngine;

namespace Assets.Enemy
{
    public class EnemyPresenter
    {
        private readonly EnemyView _enemyView;
        private List<Enemy> _enemies;
        public EnemyPresenter(EnemyView enemyView)
        {
            _enemyView = enemyView;
            _enemies = new List<Enemy>();
            CreateEnemy();
        }
        
        public IReadOnlyList<Enemy> Enemies => _enemies;
        public EnemyView EnemyView => _enemyView;

        private void CreateEnemy()
        {
            int countEnemy = GenerateRandomCountEnemy();
            
            string data = _enemyView.Data;
            data += $" {countEnemy}";
            
            _enemyView.Weapon.SetNewElement(GetRandomElement());
            _enemyView.Armor.BodyPart.SetNewElement(GetRandomElement());

            for (int i = 0; i < countEnemy; i++)
            {
                var enemy = GetEnemy();
                enemy.Sprite = _enemyView.Sprite;

                if (_enemyView.Type == ObjectType.Boos)
                    enemy.MakeBoss();

                _enemies.Add(enemy);
            }
        }

        private Element GetRandomElement() =>
            (Element)Random.Range(0, 5);

        private Enemy GetEnemy()
        {
            EnemyFactory factory = new EnemyFactory();
            WeaponFactory weaponFactory = new WeaponFactory();
            ArmorFactory armorFactory = new ArmorFactory();

            IWeapon weapon = weaponFactory.Create(_enemyView.Weapon.Damage, _enemyView.Weapon.Element, 
                _enemyView.Weapon.ChanceToSplash, _enemyView.Weapon.MinValueToCriticalDamage,
                _enemyView.Weapon.ValueModifier, _enemyView.Weapon.ParticleSystem);

            Body body = new Body(_enemyView.Armor.BodyPart.Value, _enemyView.Armor.BodyPart.Element);
            Head head = new Head(_enemyView.Armor.HeadPart.Value);

            Armor armor = armorFactory.Create(body, head, _enemyView.Armor.ParticleSystem);

            return factory.Create(weapon, armor, _enemyView.Health, _enemyView.SpriteAnimation);
        }

        private int GenerateRandomCountEnemy() =>
            Random.Range(1, 4);
    }
}