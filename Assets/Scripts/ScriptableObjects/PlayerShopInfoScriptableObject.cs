using UnityEngine;

namespace Assets.ScriptableObjects
{
	[CreateAssetMenu(fileName = "PlayerShopInfo", menuName = "ScriptableObject/PlayerShopInfo", order = 0)]
	public class PlayerShopInfoScriptableObject : ScriptableObject
	{
		[SerializeField] private LocalizedText _numberOfAttempts;
		[SerializeField] private LocalizedText _numberOfEnemy;
		[SerializeField] private LocalizedText _numberOfBoss;
		[SerializeField] private LocalizedText _money;

		public LocalizedText NumberOfAttempts => _numberOfAttempts;

		public LocalizedText NumberOfEnemy => _numberOfEnemy;

		public LocalizedText NumberOfBoss => _numberOfBoss;

		public LocalizedText Money => _money;
	}
}