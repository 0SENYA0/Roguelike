using UnityEngine;

namespace Assets.Enemy
{
    public class EnemyFactory
    {
        public Enemy Create(Sprite sprite) =>
            new Enemy(sprite: sprite);
    }
}