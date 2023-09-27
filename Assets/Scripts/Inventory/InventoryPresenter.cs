using System;
using System.Linq;
using Assets.DefendItems;
using Assets.Inventory.ItemGeneratorSystem;
using UnityEngine;

namespace Assets.Inventory
{
    public class InventoryPresenter
    {
        private readonly InventoryModel _inventoryModel;

        public InventoryPresenter(int inventorySize)
        {
            _inventoryModel = new InventoryModel(inventorySize);
            GenerateDefaultInventory();
            
            _inventoryModel.AddItem(ItemGenerator.Instance.GetRandomWeapon());
            _inventoryModel.AddItem(ItemGenerator.Instance.GetRandomWeapon());
            _inventoryModel.AddItem(ItemGenerator.Instance.GetRandomWeapon());
            _inventoryModel.AddItem(ItemGenerator.Instance.GetRandomWeapon());
            _inventoryModel.AddItem(ItemGenerator.Instance.GetRandomWeapon());
        }

        public void SelectActiveArmor(Armor armor)
        {
            var selectedArmor = _inventoryModel.GetArmor().FirstOrDefault(x => x.IsSelect);
            selectedArmor?.UnSelect();

            armor.Select();
        }

        private void GenerateDefaultInventory()
        {
            if (ItemGenerator.Instance == null)
                throw new Exception("Inventory don't loaded");
            
            var defaultInventory = ItemGenerator.Instance.GetDefaultInventory();

            _inventoryModel.AddItem(defaultInventory.Armor);
            _inventoryModel.AddItem(defaultInventory.Weapon);

            defaultInventory.Armor.Select();
        }

        public InventoryModel InventoryModel => _inventoryModel;
        public Armor ActiveArmor => _inventoryModel.GetArmor().FirstOrDefault(x => x.IsSelect);
    }
}