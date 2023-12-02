using System;
using System.Collections;
using UnityEngine;

namespace Assets.UI
{
	[RequireComponent(typeof(Activator))]
	public class Curtain : MonoBehaviour
	{
		private readonly int _isHideKey = Animator.StringToHash("IsShow");
		private readonly int _showKey = Animator.StringToHash("Show");
		private readonly float _artificialDelay = 0.5f;

		private Animator _animator;

		private Action NextAction;

		public static Curtain Instance { get; private set; }

		private void OnEnable()
		{
			if (Instance == null)
				Instance = this;

			_animator = GetComponent<Animator>();
		}

		public void HideCurtain() =>
			_animator.SetTrigger(_showKey);

		public void ShowAnimation(Action nextAction = null)
		{
			NextAction = nextAction;
			_animator.SetBool(_isHideKey, true);
		}

		private void ShowNextAction()
		{
			NextAction?.Invoke();
			StartCoroutine(ExpectDelay());
		}

		private IEnumerator ExpectDelay()
		{
			yield return new WaitForSeconds(_artificialDelay);
			_animator.SetBool(_isHideKey, false);
		}
	}
}