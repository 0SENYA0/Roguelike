using Assets.Person;

namespace Assets.Player
{
    public class PlayerAttackPresenter : UnitAttackPresenter
    {
        private readonly PlayerAttackView _playerAttackView;

        public PlayerAttackPresenter(Player player, PlayerAttackView playerAttackView) : base(player, playerAttackView)
        {
            _playerAttackView = playerAttackView;
        }

        public PlayerAttackView PlayerAttackView => _playerAttackView;
    }
}