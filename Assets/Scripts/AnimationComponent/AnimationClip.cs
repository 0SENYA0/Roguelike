using System;
using UnityEngine;

namespace Assets.Scripts.AnimationComponent
{
	[Serializable]
	public class AnimationClip
	{
		[SerializeField] private AnimationState _state;
		[SerializeField] private Sprite[] _sprites;
		[SerializeField] private bool _isLoop;
		[SerializeField] private bool _isAllowNextClip;
		[SerializeField] private AnimationState _nextState;

		public AnimationState State => _state;

		public Sprite[] Sprites => _sprites;

		public bool IsLoop => _isLoop;

		public bool IsAllowNextClip => _isAllowNextClip;

		public AnimationState NextState => _nextState;
	}
}