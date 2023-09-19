using Assets.Person;

namespace Assets.Enemy
{
    public class EnemyAttackPresenter : UnitAttackPresenter
    {
        private readonly Enemy _enemy;
        private readonly EnemyAttackView _enemyAttackView;
        
        public EnemyAttackPresenter(Enemy enemy, EnemyAttackView enemyAttackView) : base(enemy, enemyAttackView)
        {
            _enemy = enemy;
            _enemyAttackView = enemyAttackView;
            SetElementsSpriteForUI();
        }
        
        public EnemyAttackView EnemyAttackView => _enemyAttackView;
        
        private void SetElementsSpriteForUI()
        {
            EnemyAttackView.ArmorElement.sprite =
                EnemyAttackView.ElementsSpriteView.GetElementSprite(_enemy.Armor.Body.Element);
            EnemyAttackView.WeaponElement.sprite =
                EnemyAttackView.ElementsSpriteView.GetElementSprite(_enemy.Weapon.Element);
        }
    }
}