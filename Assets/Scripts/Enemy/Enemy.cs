using Assets.Person;
using UnityEngine;

namespace Assets.Enemy
{
    public class Enemy : Unit
    {
        public bool Boss { get; private set; }

        public Enemy(Sprite sprite) : base(sprite) =>
            Boss = false;

        public void MakeBoss() =>
            Boss = true;
    }
}