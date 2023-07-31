using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Fight
{
    [RequireComponent(typeof(Image))]
    public class DiceAnimation : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _sprites;

        private Image _image;
        private Button _button;
        private bool _doShuffle = true;
        private Coroutine _coroutine;
        private void Awake()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
        }

        private void OnEnable() =>
            _button.onClick.AddListener(StartAnimation);

        private void OnDisable() =>
            _button.onClick.RemoveListener(StartAnimation);

        private void StartAnimation()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
                return;
            }
            
            _coroutine = StartCoroutine(StartAnimationCoroutine());
        }

        private IEnumerator StartAnimationCoroutine()
        {
            int step = 1000;
            while (step > 0)
            {
                // TODO Add Shuffle method for List
                foreach (Sprite sprite in _sprites)
                {
                    _image.sprite = sprite;
                    step--;
                    yield return null;
                }
            }
        }
    }
}