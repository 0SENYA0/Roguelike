using System;
using UnityEngine;

namespace Assets.ScriptableObjects
{
	[Serializable]
	public class ArmorStat
	{
		[SerializeField] private Range _headStatValue;
		[SerializeField] private Range _bodyStatValue;

		public Range HeadStatValue => _headStatValue;

		public Range BodyStatValue => _bodyStatValue;
	}
}