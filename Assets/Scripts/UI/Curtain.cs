using System;
using System.Collections;
using UnityEngine;

namespace Assets.UI
{
    [RequireComponent(typeof(Activator))]
    public class Curtain : MonoBehaviour
    {
        private readonly int _isHideKey = Animator.StringToHash("IsShow");
        private readonly float _artificialDelay = 0.5f;

        public static Curtain Instance { get; private set; }

        private Animator _animator;
        private Action _nextAction;

        private void OnEnable()
        {
            if (Instance == null)
                Instance = this;

            _animator = GetComponent<Animator>();
        }

        public void HideCurtain()
        {
            _animator.SetTrigger("Show");
            //_animator.SetBool(_isHideKey, false);
        }
        
        public void ShowAnimation(Action nextAction = null)
        {
            _nextAction = nextAction;
            _animator.SetBool(_isHideKey, true);
        }

        // Called on animation
        private void ShowNextAction()
        {
            _nextAction?.Invoke();
            StartCoroutine(ExpectDelay());
        }

        private IEnumerator ExpectDelay()
        {
            yield return new WaitForSeconds(_artificialDelay);
            _animator.SetBool(_isHideKey, false);
        }
    }
}