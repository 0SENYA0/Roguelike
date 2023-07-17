using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.EnemyScripts
{
    public class TestingScript : MonoBehaviour
    {
        [SerializeField] private SpriteAnimation _spriteAnimation;

        private void OnEnable()
        {
            _spriteAnimation.OnAnimationComplete += OnAnimationComplete;
        }

        private void OnDisable()
        {
            _spriteAnimation.OnAnimationComplete -= OnAnimationComplete;
        }

        private void OnAnimationComplete()
        {
            Debug.Log("Увидел, что аниматор отработал");
        }

        public void OnAwake()
        {
            _spriteAnimation.SetClip(AnimationState.Awake);
        }

        public void Attack()
        {
            _spriteAnimation.SetClip(AnimationState.Attack);
        }

        public void Hit()
        {
            _spriteAnimation.SetClip(AnimationState.Hit);
        }

        public void Die()
        {
            _spriteAnimation.SetClip(AnimationState.Dei);
        }
    }
}