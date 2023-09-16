using Assets.DefendItems;
using Assets.Interface;
using Assets.Inventory.ItemGeneratorSystem;

namespace Assets.Inventory
{
    public class InventoryPresenter
    {
        private readonly InventoryModel _inventoryModel;
        private Armor _activeArmor;
        private IWeapon _activeWeapon;
        
        public InventoryPresenter(int inventorySize)
        {
            _inventoryModel = new InventoryModel(inventorySize);
            GenerateDefaultInventory();
            _inventoryModel.AddItem(ItemGenerator.Instance.GetRandomWeapon());
            _inventoryModel.AddItem(ItemGenerator.Instance.GetRandomWeapon());
            _inventoryModel.AddItem(ItemGenerator.Instance.GetRandomWeapon());
        }

        private void GenerateDefaultInventory()
        {
            var defaultInventory = ItemGenerator.Instance.GetDefaultInventory();
            _inventoryModel.AddItem(defaultInventory.Armor);
            _inventoryModel.AddItem(defaultInventory.Weapon);

            _activeArmor = defaultInventory.Armor;
            _activeWeapon = defaultInventory.Weapon;
        }

        public InventoryModel InventoryModel => _inventoryModel;
        public Armor ActiveArmor => _activeArmor;
        public IWeapon ActiveWeapon => _activeWeapon;
    }
}