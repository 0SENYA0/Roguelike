using Assets.DefendItems;
using Assets.Interface;
using Assets.Inventory.ItemGeneratorSystem;

namespace Assets.Inventory
{
    public class InventoryPresenter
    {
        private readonly InventoryModel _inventoryModel;
        private Armor _activeArmor;
        private Weapon.Weapon _activeWeapon;
        
        public InventoryPresenter(int inventorySize)
        {
            _inventoryModel = new InventoryModel(inventorySize);
            GenerateDefaultInventory();
            
            _inventoryModel.AddItem(ItemGenerator.Instance.GetRandomArmor());
            _inventoryModel.AddItem(ItemGenerator.Instance.GetRandomArmor());
            _inventoryModel.AddItem(ItemGenerator.Instance.GetRandomArmor());
        }

        public void SelectActiveArmor(Armor armor)
        {
            _activeArmor.UnSelect();
            _activeArmor = armor;
            _activeArmor.Select();
        }

        public void SelectActiveWeapon(Weapon.Weapon weapon)
        {
            _activeWeapon.UnSelect();
            _activeWeapon = weapon;
            _activeWeapon.Select();
        }

        private void GenerateDefaultInventory()
        {
            var defaultInventory = ItemGenerator.Instance.GetDefaultInventory();
            _inventoryModel.AddItem(defaultInventory.Armor);
            _inventoryModel.AddItem(defaultInventory.Weapon);

            _activeArmor = defaultInventory.Armor;
            _activeArmor.Select();
            
            _activeWeapon = defaultInventory.Weapon;
            _activeWeapon.Select();
        }

        public InventoryModel InventoryModel => _inventoryModel;
        public Armor ActiveArmor => _activeArmor;
        public IWeapon ActiveWeapon => _activeWeapon;
    }
}