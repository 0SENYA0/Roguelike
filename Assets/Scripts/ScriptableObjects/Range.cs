using System;
using UnityEngine;

namespace Assets.ScriptableObjects
{
	[Serializable]
	public class Range
	{
		[SerializeField] private float _minValue;
		[SerializeField] private float _maxValue;

		public float MinValue => _minValue;

		public float MaxValue => _maxValue;
	}
}