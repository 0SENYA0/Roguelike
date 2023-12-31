using Assets.Fight.Element;

namespace Assets.Interface
{
	public interface IWeapon
	{
		public float Damage { get; }

		public Element Element { get; }

		int ChanceToSplash { get; }

		int ChanceToCritical { get; }

		int ChanceToModifier { get; }
	}
}