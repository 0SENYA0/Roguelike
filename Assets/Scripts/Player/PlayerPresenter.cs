using Assets.DefendItems;
using Assets.Interface;
using Assets.Weapon;

namespace Assets.Player
{
    public class PlayerPresenter
    {
        private Player _player;
        private readonly PlayerView _playerView;

        public PlayerPresenter(PlayerView playerView)
        {
            _playerView = playerView;
            CreatePlayer();
        }

        public Player Player => _player;
        public PlayerView PlayerView => _playerView;
        
        private void CreatePlayer()
        {
            if (_player != null)
                return;

            // TODO Сделать: Если это первый раз или игрок потерял все жизни создаём стартовый билд
            ArmorFactory armorFactory = new ArmorFactory();
            Armor armor = armorFactory.Create(new Body(_playerView.ArmorScriptableObject.BodyPart.Value, _playerView.ArmorScriptableObject.BodyPart.Element),
                new Head(_playerView.ArmorScriptableObject.HeadPart.Value), _playerView.ArmorScriptableObject.ParticleSystem);

            WeaponFactory weaponFactory = new WeaponFactory();
            IWeapon weapon = weaponFactory.Create(_playerView.WeaponScriptableObject.Damage, _playerView.WeaponScriptableObject.Element,
                _playerView.WeaponScriptableObject.ChanceToSplash, _playerView.WeaponScriptableObject.MinValueToCriticalDamage,
                _playerView.WeaponScriptableObject.ValueModifier, _playerView.WeaponScriptableObject.ParticleSystem);
            
            _player = new Player(_playerView.Health, weapon, armor, new MagicItem(), _playerView.SpriteAnimation);
            _player.Sprite = _playerView.Sprite;
        }
    }
}