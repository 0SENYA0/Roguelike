using System;
using Assets.Scripts.EnemyScripts;
using UnityEngine;
using AnimationState = Assets.Scripts.EnemyScripts.AnimationState;

namespace Assets.Scripts.ProceduralGeneration.LevelMovement
{
    public class PlayerMovementAnimation : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private SpriteAnimation _animation;

        private Vector3 _point;
        private Vector3 _lastTranformPosition;
        private bool _isAnimationPlaying = false;

        private void Start()
        {
            _lastTranformPosition = transform.position;
        }

        private void LateUpdate()
        {
            UpdateSpriteRender();
            CheckMovement();
            _lastTranformPosition = transform.position;
        }

        public void SetPoint(Vector3 point)
        {
            _point = point;
        }
        
        private void UpdateSpriteRender()
        {
            if (_lastTranformPosition.x - transform.position.x < 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (_lastTranformPosition.x - transform.position.x > 0.001f)
            {
                _spriteRenderer.flipX = true;
            }
        }

        private void CheckMovement()
        {
            var distance = Vector3.Distance(transform.position, _lastTranformPosition);

            if(distance > 0.001f)
            {
                if (_isAnimationPlaying == false)
                {
                    _isAnimationPlaying = true;
                    _animation.SetClip(AnimationState.Walk);
                }   
            }
            else
            {
                if (_isAnimationPlaying)
                {
                    _isAnimationPlaying = false;
                    _animation.SetClip(AnimationState.Idle);
                }
            }
        }
    }
}