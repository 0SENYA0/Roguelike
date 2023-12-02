using System.Collections.Generic;
using UnityEngine;
using AnimationClip = Assets.Scripts.AnimationComponent.AnimationClip;

namespace Assets.Person
{
	[RequireComponent(typeof(SpriteRenderer))]
	public abstract class UnitAttackView : MonoBehaviour
	{
		private int _frameRate = 10;
		private List<Clip> _clips = new List<Clip>();

		private SpriteRenderer _renderer;
		private float _secPerFrame;
		private float _nextFrameTime;
		private int _currentFrame;
		private bool _isPlaying = true;

		private Sprite[] _sprites;
		private bool _isLoop;
		private Clip _currentClip;

		public bool IsComplete { get; private set; }

		private void Awake()
		{
			_renderer = GetComponent<SpriteRenderer>();
			OnPastAwake();
		}

		private void OnEnable()
		{
			_secPerFrame = 1f / _frameRate;
			StartAnimation();
			_nextFrameTime = Time.time + _secPerFrame;

			OnPastEnable();
		}

		private void Update()
		{
			if (_currentClip == null)
				return;

			if (_nextFrameTime > Time.time)
				return;

			if (_currentFrame >= _currentClip.Sprites.Length)
			{
				if (_currentClip.IsLoop)
					_currentFrame = 0;
				else if (_currentClip.IsAllowNextClip)
					SetClip(_currentClip.NextState);
				else
					IsComplete = true;
			}
			else
			{
				_renderer.sprite = _currentClip.Sprites[_currentFrame];

				_nextFrameTime += _secPerFrame;
				_currentFrame++;
			}
		}

		public void SetClip(Assets.Scripts.AnimationComponent.AnimationState state)
		{
			for (var i = 0; i < _clips.Count; i++)
			{
				if (_clips[i].State == state)
				{
					_currentClip = _clips[i];
					StartAnimation();
					return;
				}
			}
		}

		public void FillDataForClips(IReadOnlyList<AnimationClip> spriteAnimationAnimationClips)
		{
			_clips.Clear();
			_clips = new List<Clip>();

			foreach (AnimationClip clip in spriteAnimationAnimationClips)
			{
				_clips.Add(new Clip(clip.Sprites,
					clip.State,
					clip.IsLoop,
					clip.NextState,
					clip.IsAllowNextClip));
			}
		}

		public virtual void ChangeUIHealthValue(float value)
		{
		}

		protected virtual void OnPastAwake()
		{
		}

		protected virtual void OnPastEnable()
		{
		}

		private void StartAnimation()
		{
			IsComplete = false;
			enabled = true;
			_isPlaying = true;
			_nextFrameTime = Time.time + _secPerFrame;
			_currentFrame = 0;
		}
	}
}