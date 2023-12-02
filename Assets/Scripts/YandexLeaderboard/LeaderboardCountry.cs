using UnityEngine;

namespace Assets.YandexLeaderboard
{
	[CreateAssetMenu(fileName = "LeaderboardCountry", menuName = "ScriptableObject/LeaderboardCountry", order = 0)]
	public class LeaderboardCountry : ScriptableObject
	{
		[SerializeField] private Sprite _eng;
		[SerializeField] private Sprite _rus;
		[SerializeField] private Sprite _tur;

		public Sprite Eng => _eng;

		public Sprite Rus => _rus;

		public Sprite Tur => _tur;
	}
}