using System;
using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.UI.TrainingPanel
{
	[Serializable]
	public class Info
	{
		[SerializeField] private TrainingStep _step;
		[SerializeField] private LocalizedSprite _sprite;
		[SerializeField] private LocalizedText _label;
		[SerializeField] private LocalizedText _text;

		public TrainingStep Step => _step;

		public LocalizedSprite Sprite => _sprite;

		public LocalizedText Label => _label;

		public LocalizedText Text => _text;
	}
}