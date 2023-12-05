using UnityEngine;
using AnimationState = Assets.Scripts.AnimationComponent.AnimationState;

namespace Assets.Person
{
	public class Clip
	{
		public Clip(
			Sprite[] sprites,
			AnimationState state,
			bool isLoop,
			AnimationState nextState,
			bool isAllowNextClip)
		{
			Sprites = sprites;
			State = state;
			IsLoop = isLoop;
			NextState = nextState;
			IsAllowNextClip = isAllowNextClip;
		}

		public Sprite[] Sprites { get; }

		public AnimationState State { get; }

		public bool IsLoop { get; }

		public AnimationState NextState { get; }

		public bool IsAllowNextClip { get; }
	}
}