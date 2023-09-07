using Assets.Enemy;
using Assets.Person;
using UnityEngine;

namespace Assets.Player
{
    public class PlayerPresenter : IPlayerPresenter
    {
        private readonly PlayerView _playerView;

        private readonly Player _player;

        public PlayerPresenter(PlayerView playerView)
        {
            _playerView = playerView;
            _player = GetNewPlayer();
        }

        public PlayerView PlayerView => _playerView;
        public Player Player => _player;

        private Player GetNewPlayer()
        {
            Player player = new Player(
                _playerView.Health,
                new PlayerInventary(_playerView.PlayerInventory.GetWeapons(), _playerView.PlayerInventory.GetArmors(), _playerView.PlayerInventory.MagicItems),
                _playerView.SpriteAnimation);
            
            Debug.Log($"{_playerView.PlayerInventory.GetArmors().Length}");
            Debug.Log($"{player.PlayerInventary.Armor.Length}");
            
            player.Sprite = _playerView.Sprite;

            return player;
        }
    }

    public interface IPlayerPresenter : IUnitPresenter
    {
        public PlayerView PlayerView { get; }
        public Player Player { get; }
    }
}