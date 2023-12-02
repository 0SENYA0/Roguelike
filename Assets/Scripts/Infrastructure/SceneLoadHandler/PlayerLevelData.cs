using Assets.Inventory;

namespace Assets.Infrastructure.SceneLoadHandler
{
    public class PlayerLevelData
    {
        public PlayerLevelData(float health, InventoryPresenter inventory)
        {
            Health = health;
            Inventory = inventory;
        }

        public float Health { get; }

        public InventoryPresenter Inventory { get; }
    }
}