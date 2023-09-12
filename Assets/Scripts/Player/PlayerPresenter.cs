using Assets.Enemy;
using Assets.Inventory;
using Assets.Person;
using UnityEngine;

namespace Assets.Player
{
    public class PlayerPresenter : IPlayerPresenter
    {
        private readonly PlayerView _playerView;
        private readonly InventoryPresenter _inventoryPresenter;

        private readonly Player _player;

        public PlayerPresenter(PlayerView playerView, InventoryPresenter inventoryPresenter)
        {
            _playerView = playerView;
            _inventoryPresenter = inventoryPresenter;
            _player = GetNewPlayer();
        }

        public PlayerView PlayerView => _playerView;
        public Player Player => _player;

        private Player GetNewPlayer()
        {
            Player player = new Player(
                _playerView.Health,
                _inventoryPresenter,
                _playerView.SpriteAnimation);
            
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