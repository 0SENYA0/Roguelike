using Assets.DefendItems;
using Assets.Interface;
using Assets.Scripts.AnimationComponent;

namespace Assets.Enemy
{
    public class EnemyFactory
    {
        public Enemy Create(IWeapon weapon, Armor armor, int health, SpriteAnimation spriteAnimation) =>
            new Enemy(health, weapon, armor, spriteAnimation);
    }
}