using Assets.DefendItems;

namespace Assets.Inventory.ItemGeneratorSystem
{
    public class InventoryItem
    {
        public InventoryItem(Armor armor, Weapon.Weapon weapon)
        {
            Armor = armor;
            Weapon = weapon;
        }

        public Armor Armor { get; }
        public Weapon.Weapon Weapon { get; }
    }
}