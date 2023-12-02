using UnityEngine;

namespace Assets.Scripts.GenerationSystem
{
	[System.Serializable]
	public class SpawnerItem
	{
		[SerializeField] private GameObject[] _template;
		[SerializeField] private int _maxCount;
		[SerializeField] [Range(0, 1f)] private float _chanceSpawn = 0.05f;

		public GameObject[] Template => _template;

		public float ChanceSpawn => _chanceSpawn;

		public int MaxCount => _maxCount;
	}
}