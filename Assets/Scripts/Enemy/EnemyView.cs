using Assets.DefendItems;
using Assets.Infrastructure;
using Assets.Interface;
using Assets.Inventory.ItemGeneratorSystem;
using Assets.Scripts.AnimationComponent;
using Assets.Scripts.InteractiveObjectSystem;
using Assets.Weapons;
using UnityEngine;

namespace Assets.Enemy
{
    public class EnemyView : InteractiveObject, IEnemyViewReadOnly
    {
        [SerializeField] private int _health;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private SpriteAnimation _spriteAnimation;

        private EnemyPresenter _enemyPresenter;
        private Armor _armor;
        private Weapon _weapon;

        public IEnemyPresenter EnemyPresenter => _enemyPresenter;

        public int Health => _health;

        public Weapon Weapon => _weapon;

        public Armor Armor => _armor;

        public Sprite Sprite => _sprite;

        public SpriteAnimation SpriteAnimation => _spriteAnimation;

        public string Name => _translationName.GetLocalization(Game.GameSettings.CurrentLocalization);

        protected override void OnStart()
        {
            _enemyPresenter = new EnemyPresenter(this);
            _weapon = ItemGenerator.Instance.GetRandomWeapon();
            _armor = ItemGenerator.Instance.GetRandomArmor();
            base.OnStart();
        }
    }
}