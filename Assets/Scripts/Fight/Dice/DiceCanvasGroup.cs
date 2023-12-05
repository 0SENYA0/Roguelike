using System.Collections;
using Assets.Interface;
using UnityEngine;

namespace Assets.Fight.Dice
{
    public class DiceCanvasGroup : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _time;

        private Coroutine _coroutine;

        public void ShowCanvas()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ChangeAlpha(0, 1, _time));
        }

        public void HideCanvas()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ChangeAlpha(1, 0, _time));
        }

        private IEnumerator ChangeAlpha(float startAlpha, float endAlpha, float duration)
        {
            _canvasGroup.alpha = startAlpha;
            float startTime = Time.time;

            while (Time.time < startTime + duration)
            {
                float time = (Time.time - startTime) / duration;
                _canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, time);

                yield return null;
            }

            _canvasGroup.alpha = endAlpha;
            _coroutine = null;
        }
    }
}