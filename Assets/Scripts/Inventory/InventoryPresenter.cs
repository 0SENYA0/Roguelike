using System;
using System.Linq;
using Assets.DefendItems;
using Assets.Inventory.ItemGeneratorSystem;

namespace Assets.Inventory
{
	public class InventoryPresenter
	{
		private readonly InventoryModel _inventoryModel;

		public InventoryPresenter(int inventorySize)
		{
			_inventoryModel = new InventoryModel(inventorySize);
			GenerateDefaultInventory();
		}

		public InventoryModel InventoryModel => _inventoryModel;

		public Armor ActiveArmor => _inventoryModel.GetArmor().FirstOrDefault(x => x.IsSelect);

		public void SelectActiveArmor(Armor armor)
		{
			Armor selectedArmor = _inventoryModel.GetArmor().FirstOrDefault(x => x.IsSelect);
			selectedArmor?.UnSelect();

			armor.Select();
		}

		private void GenerateDefaultInventory()
		{
			if (ItemGenerator.Instance == null)
				throw new Exception("Inventory don't loaded");

			InventoryItem defaultInventory = ItemGenerator.Instance.GetDefaultInventory();

			_inventoryModel.AddItem(defaultInventory.Armor);
			_inventoryModel.AddItem(defaultInventory.Weapon);

			defaultInventory.Armor.Select();
		}
	}
}