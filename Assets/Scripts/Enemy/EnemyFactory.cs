using Assets.DefendItems;
using Assets.Interface;
using Assets.Inventory.ItemGeneratorSystem;

namespace Assets.Enemy
{
    public class EnemyFactory
    {
        public Enemy CreateEnemy(EnemyView enemyView)
        {
            IWeapon weapon = ItemGenerator.Instance.GetRandomWeapon();
            Armor armor = ItemGenerator.Instance.GetRandomArmor();

            return new Enemy(enemyView.Health, weapon, armor, enemyView.SpriteAnimation);
        }
    }
}