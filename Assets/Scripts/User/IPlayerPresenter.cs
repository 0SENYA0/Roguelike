using Assets.Enemy;
using Assets.Person;

namespace Assets.User
{
	public interface IPlayerPresenter : IUnitPresenter
	{
		public PlayerView PlayerView { get; }

		public Player Player { get; }

		public void UsePotion();
	}
}