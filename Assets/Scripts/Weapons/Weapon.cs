using Assets.Fight.Element;
using Assets.Interface;
using Assets.Inventory;

namespace Assets.Weapons
{
	public class Weapon : IWeapon, IInventoryItem
	{
		public Weapon(float damage, Element element, int chanceToSplash, int minValueToCriticalDamage, int valueModifier)
		{
			ChanceToSplash = chanceToSplash;
			ChanceToCritical = minValueToCriticalDamage;
			ChanceToModifier = valueModifier;
			Damage = damage;
			Element = element;
		}

		public float Damage { get; }

		public Element Element { get; }

		public int ChanceToSplash { get; }

		public int ChanceToCritical { get; }

		public int ChanceToModifier { get; }
	}
}