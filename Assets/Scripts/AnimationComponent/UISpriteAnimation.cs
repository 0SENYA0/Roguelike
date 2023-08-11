using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.AnimationComponent
{
    [RequireComponent(typeof(Image))]
    public class UISpriteAnimation : MonoBehaviour
    {
        [SerializeField] [Range(1, 30)] private int _frameRate = 10;
        [SerializeField] private Sprite[] _sprites;

        private float _secPerFrame;
        private float _nextFrameTime;
        private int _currentFrame;
        private Image _image;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
            _secPerFrame = 1f / _frameRate;
            _nextFrameTime = Time.time + _secPerFrame;
            _currentFrame = 0;
        }

        private void Update()
        {
            if (_nextFrameTime > Time.time)
                return;

            if (_currentFrame >= _sprites.Length)
                _currentFrame = 0;
            
            _image.sprite = _sprites[_currentFrame];
            _nextFrameTime += _secPerFrame;
            _currentFrame++;
        }
    }
}