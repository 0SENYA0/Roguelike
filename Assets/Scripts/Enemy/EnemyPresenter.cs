using System.Collections.Generic;
using Assets.DefendItems;
using Assets.Interface;
using Assets.Inventory.ItemGeneratorSystem;
using Assets.Scripts.InteractiveObjectSystem;
using Random = UnityEngine.Random;

namespace Assets.Enemy
{
    public class EnemyPresenter : IEnemyPresenter
    {
        private readonly EnemyView _enemyView;

        private List<Enemy> _enemy;
        private int _count;

        public EnemyPresenter(EnemyView enemyView)
        {
            _enemyView = enemyView;
            Start();
        }

        public EnemyView EnemyView => _enemyView;
        public IReadOnlyList<Enemy> Enemy => _enemy;

        private void Start()
        {
            _enemy = new List<Enemy>();
            _count = GenerateRandomCountEnemy();

            for (int i = 0; i < _count; i++)
            {
                Enemy enemy = GetEnemy();
                enemy.Sprite = _enemyView.Sprite;

                if (_enemyView.Type == ObjectType.Boos)
                    enemy.MakeBoss();

                _enemy.Add(enemy);
            }
        }

        private Enemy GetEnemy()
        {
            EnemyFactory factory = new EnemyFactory();
            IWeapon weapon = ItemGenerator.Instance.GetRandomWeapon();
            Armor armor = ItemGenerator.Instance.GetRandomArmor();

            return factory.Create(weapon, armor, _enemyView.Health, _enemyView.SpriteAnimation);
        }
        
        private int GenerateRandomCountEnemy() =>
            Random.Range(1, 4);
    }

    public interface IEnemyPresenter : IUnitPresenter
    {
        public EnemyView EnemyView { get; }
        public IReadOnlyList<Enemy> Enemy { get; }
    }

    public interface IUnitPresenter
    {
    }
}