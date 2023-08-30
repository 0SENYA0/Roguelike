using Assets.Fight;
using Assets.Person;

namespace Assets.Player
{
    public class PlayerFightPresenter : FightPresenter
    {
        private readonly Player _player;
        private readonly UnitFightView _playerFightView;

        public PlayerFightPresenter(Player player, UnitFightView playerFightView)
        {
            _player = player;
            _playerFightView = playerFightView;
        }

        public Player Player => _player;
        public UnitFightView PlayerView => _playerFightView;
    }
}