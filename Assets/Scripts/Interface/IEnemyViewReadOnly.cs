using Assets.DefendItems;
using Assets.Scripts.AnimationComponent;
using UnityEngine;

namespace Assets.Interface
{
    public interface IEnemyViewReadOnly
    {
        public int Health { get; }

        public Weapon.Weapon Weapon { get; }

        public Armor Armor { get; }

        public Sprite Sprite { get; }

        public SpriteAnimation SpriteAnimation { get; }

        public string Name { get; }
    }
}