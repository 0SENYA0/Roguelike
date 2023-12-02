using System;
using UnityEngine;

namespace Assets.ScriptableObjects
{
	[Serializable]
	public class Level
	{
		[SerializeField] private int _levelNumber;
		[SerializeField] private ListOfItemStatsRangesScriptableObject _list;

		public int LevelNumber => _levelNumber;

		public ListOfItemStatsRangesScriptableObject List => _list;
	}
}