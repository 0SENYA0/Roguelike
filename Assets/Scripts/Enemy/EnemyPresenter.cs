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
		private EnemyFactory _enemyFactory;

		public EnemyPresenter(EnemyView enemyView)
		{
			_enemyView = enemyView;
			_enemyFactory = new EnemyFactory();
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
				Enemy enemy = _enemyFactory.CreateEnemy(_enemyView);
				enemy.Sprite = _enemyView.Sprite;

				if (_enemyView.Type == ObjectType.Boos)
					enemy.MakeBoss();

				_enemy.Add(enemy);
			}
		}

		private int GenerateRandomCountEnemy() =>
			Random.Range(1, 4);
	}
}