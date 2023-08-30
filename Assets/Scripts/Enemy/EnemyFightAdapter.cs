using System.Collections.Generic;
using Assets.Person;

namespace Assets.Enemy
{
    public class EnemyFightAdapter
    {
        private readonly EnemyPresenter _presenter;
        private readonly List<UnitFightView> _enemyFightView;

        public EnemyFightAdapter(EnemyPresenter presenter, List<UnitFightView> enemyFightView)
        {
            _presenter = presenter;
            _enemyFightView = enemyFightView;
            _enemyFightPresenterses = new List<EnemyFightPresenter>();
            FillEnemyFightPresenters();
        }

        private List<EnemyFightPresenter> _enemyFightPresenterses;
        public IReadOnlyList<EnemyFightPresenter> EnemyFightPresenters => _enemyFightPresenterses;        
        
        private void FillEnemyFightPresenters()
        {
            for (int i = 0; i < _presenter.Enemies.Count; i++)
                _enemyFightPresenterses.Add(new EnemyFightPresenter(_presenter.Enemies[i], _enemyFightView[i]));
        }
    }
}