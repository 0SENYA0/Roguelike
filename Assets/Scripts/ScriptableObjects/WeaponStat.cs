using System;
using UnityEngine;

namespace Assets.ScriptableObjects
{
	[Serializable]
	public class WeaponStat
	{
		[SerializeField] private Range _damage;

		public Range Damage => _damage;
	}
}