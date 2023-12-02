using System.Collections.Generic;
using UnityEngine;

namespace Assets.ScriptableObjects
{
	[CreateAssetMenu(fileName = "LevelStats", menuName = "ScriptableObject/LevelStats", order = 0)]
	public class LevelsStatsScriptableObject : ScriptableObject
	{
		[SerializeField] private List<Level> _listStats;

		public List<Level> ListStats => _listStats;
	}
}