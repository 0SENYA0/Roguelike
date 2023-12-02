using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Fight
{
	[Serializable]
	public class LineInfo : ILineInfo
	{
		[SerializeField] private Button _buttonAttack;

		[field: SerializeField] public TMP_Text Damage { get; set; }
		[field: SerializeField] public TMP_Text ChanceToSplash { get; set; }
		[field: SerializeField] public TMP_Text ChanceCriticalDamage { get; set; }
		[field: SerializeField] public TMP_Text ValueModifier { get; set; }

		public Button ButtonAttack => _buttonAttack;
	}
}