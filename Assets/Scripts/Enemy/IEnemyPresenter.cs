using System.Collections.Generic;

namespace Assets.Enemy
{
	public interface IEnemyPresenter : IUnitPresenter
	{
		public EnemyView EnemyView { get; }
		
		public IReadOnlyList<Enemy> Enemy { get; }
	}
}