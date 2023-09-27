using System;
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
            _enemyAttackView.Guid = Guid.NewGuid();
            SetElementsSpriteForUI();
        }
        
        public EnemyAttackView EnemyAttackView => _enemyAttackView;
        
        private void SetElementsSpriteForUI()
        {
            _enemyAttackView.ArmorElement.sprite = EnemyAttackView.ElementsSpriteView.GetElementSprite(_enemy.Armor.Body.Element);
            _enemyAttackView.WeaponElement.sprite = EnemyAttackView.ElementsSpriteView.GetElementSprite(_enemy.Weapon.Element);
        }
    }
}