using Assets.DefendItems;
using Assets.Weapons;

namespace Assets.Inventory.ItemGeneratorSystem
{
	public class InventoryItem
	{
		public InventoryItem(Armor armor, Weapon weapon)
		{
			Armor = armor;
			Weapon = weapon;
		}

		public Armor Armor { get; }

		public Weapon Weapon { get; }
	}
}