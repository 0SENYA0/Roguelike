using System;
using System.Collections.Generic;
using Assets.Scripts.AnimationComponent;
using UnityEngine;
using UnityEngine.UI;
using AnimationState = Assets.Scripts.AnimationComponent.AnimationState;

namespace Assets.Person
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class UnitFightView : MonoBehaviour
    {
        [SerializeField] private Image _health;

        [SerializeField] [Range(1, 30)] private int _frameRate = 10;
        private List<Clip> _clips = new List<Clip>();

        public event Action OnAnimationComplete;
        private SpriteRenderer _renderer;
        private float _secPerFrame;
        private float _nextFrameTime;
        private int _currentFrame;
        private int _currentClip;
        private bool _isPlaying = true;

        private void OnEnable()
        {
            _renderer = GetComponent<SpriteRenderer>();

            _secPerFrame = 1f / _frameRate;

            StartAnimation();

            _nextFrameTime = Time.time + _secPerFrame;
        }

        private void OnBecameVisible()
        {
            enabled = _isPlaying;
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }

        private void Update()
        {
            if (_nextFrameTime > Time.time)
                return;

            Clip clip = _clips[_currentClip];

            if (_currentFrame >= clip.Sprites.Length)
            {
                if (clip.IsLoop)
                {
                    _currentFrame = 0;
                }
                else if (clip.IsAllowNextClip)
                {
                    SetClip(clip.NextState);
                }
                else
                {
                    OnAnimationComplete?.Invoke();
                    enabled = false;
                    _isPlaying = false;
                }
            }
            else
            {
                Sprite rendererSprite = clip.Sprites[_currentFrame];
                _renderer.sprite = rendererSprite;
                _nextFrameTime += _secPerFrame;
                _currentFrame++;
            }
        }

        public void SetClip(AnimationState state)
        {
            for (var i = 0; i < _clips.Count; i++)
            {
                if (_clips[i].State == state)
                {
                    _currentClip = i;
                    StartAnimation();
                    return;
                }
            }

            enabled = false;
            _isPlaying = false;
        }

        public void FillClips(IReadOnlyList<IReadOnlyAnimationClip> clips)
        {
            for (int i = 0; i < clips.Count; i++)
            {
                _clips.Add(new Clip
                {
                    Sprites = clips[i].Sprites,
                    State = clips[i].State,
                    IsLoop = clips[i].IsLoop,
                    NextState = clips[i].NextState,
                    IsAllowNextClip = clips[i].IsAllowNextClip
                });
            }
        }

        private void StartAnimation()
        {
            enabled = true;
            _isPlaying = true;
            _nextFrameTime = Time.time + _secPerFrame;
            _currentFrame = 0;
        }
    }

    public class Clip
    {
        public Sprite[] Sprites { get; set; }
        public AnimationState State { get; set; }
        public bool IsLoop { get; set; }
        public AnimationState NextState { get; set; }
        public bool IsAllowNextClip { get; set; }
    }
}