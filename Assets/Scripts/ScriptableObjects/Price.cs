using System;
using UnityEngine;

namespace Assets.ScriptableObjects
{
	[Serializable]
	public class Price
	{
		[SerializeField] private int _level;
		[SerializeField] private int _nextPrice;

		public int Level => _level;

		public int NextPrice => _nextPrice;
	}
}