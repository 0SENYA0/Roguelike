using TMPro;
using UnityEngine.UI;

namespace Assets.Fight
{
	public interface ILineInfo
	{
		public TMP_Text Damage { get; set; }

		public TMP_Text ChanceToSplash { get; set; }

		public TMP_Text ChanceCriticalDamage { get; set; }

		public TMP_Text ValueModifier { get; set; }

		public Button ButtonAttack { get; }
	}
}