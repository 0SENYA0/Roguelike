using Assets.Fight;
using Assets.Person;

namespace Assets.Enemy
{
    public class EnemyFightPresenter : FightPresenter
    {
        private readonly Enemy _enemy;
        private readonly UnitFightView _unitFightView;

        public EnemyFightPresenter(Enemy enemy, UnitFightView unitFightView)
        {
            _enemy = enemy;
            _unitFightView = unitFightView;
        }

        public Enemy Enemy => _enemy;
        public UnitFightView UnitFightView => _unitFightView;
    }
}