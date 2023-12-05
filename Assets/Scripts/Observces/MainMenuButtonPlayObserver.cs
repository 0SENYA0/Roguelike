using System.Collections.Generic;
using Assets.Interface;

namespace Assets.Observces
{
	public class MainMenuButtonPlayObserver
	{
		private static MainMenuButtonPlayObserver _instance;

		private List<IButtonObserver> _observers = new List<IButtonObserver>();

		public static MainMenuButtonPlayObserver Instance
		{
			get
			{
				if (_instance == null)
					_instance = new MainMenuButtonPlayObserver();

				return _instance;
			}
		}

		public void Registry(IButtonObserver buttonObserver) =>
			_observers.Add(buttonObserver);

		public void UnRegistry(IButtonObserver buttonObserver) =>
			_observers.Remove(buttonObserver);
	}
}