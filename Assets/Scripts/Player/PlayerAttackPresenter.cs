using Assets.Inventory;
using Assets.Person;

namespace Assets.Player
{
    public class PlayerAttackPresenter : UnitAttackPresenter
    {
        private readonly PlayerAttackView _playerAttackView;
        private readonly InventoryPresenter _inventory;

        public PlayerAttackPresenter(Player player, PlayerAttackView playerAttackView) : base(player, playerAttackView)
        {
            _playerAttackView = playerAttackView;
            _inventory = player.InventoryPresenter;
        }

        public PlayerAttackView PlayerAttackView => _playerAttackView;
        public InventoryPresenter Inventory => _inventory;
    }
}