using System.Collections;
using Assets.Infrastructure;
using UnityEngine;

namespace Assets.Scripts.SoundSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundComponent : MonoBehaviour
    {
        [SerializeField] private SoundType _soundType; // Нужен для отслеживания настроек, что у нас включено
        [SerializeField] private AudioClip _clip; // сам клип
        [SerializeField] private bool _playOnAwake;
        [SerializeField] [Range(0, 1)] private float _volume; // громкость
        [Tooltip("If zero is specified, then the sound will be played completely")]
        [SerializeField] [Min(0f)] private float _duration; // продолжительность
        [Space(10f)] 
        [SerializeField] private bool _isLoop; // нужно ли зацикливать
        [SerializeField] private bool _isRestartIfCalledAgain; // при повторном вызове продолжать играть или рестартанусть
        [Space(10f)] 
        [SerializeField] private bool _smoothFade; // нужно ли планое затухание
        [SerializeField] private float _timeToFade; // время плавного затузхани

        private AudioSource _source;
        private Coroutine _coroutine;
        private bool _isStopped;

        private bool _isMusicOn;
        private bool _isSfxOn;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _source.clip = _clip;
            _source.loop = false;
            _source.playOnAwake = false;
            _isMusicOn = GameRoot.Instance.Sound.IsMusicOn;
            _isSfxOn = GameRoot.Instance.Sound.IsSfxOn;
        }

        private void OnEnable()
        {
            GameRoot.Instance.Sound.OnMusicStateChanged += ChangeMusic;
            GameRoot.Instance.Sound.OnSfxStateChanged += ChangeSfx;
        }

        private void OnDisable()
        {
            GameRoot.Instance.Sound.OnMusicStateChanged -= ChangeMusic;
            GameRoot.Instance.Sound.OnSfxStateChanged -= ChangeSfx;
        }

        private void ChangeMusic(bool value)
        {
            if (_soundType == SoundType.Music)
            {
                _isMusicOn = value;
                _source.volume = _isMusicOn ? _volume : 0;
            }
        }

        private void ChangeSfx(bool value)
        {
            if (_soundType == SoundType.SFX)
            {
                _isSfxOn = value;
                _source.volume = _isSfxOn ? _volume : 0;
            }
        }

        private void Start()
        {
            if (_playOnAwake)
                Play();
        }

        private void OnDestroy()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        public void Play()
        {
            if (_source.isPlaying && _isRestartIfCalledAgain == false)
                return;

            _isStopped = false;
            PlaySound();
        }

        public void Stop()
        {
            _isStopped = true;
            StartState(FadeSound());
        }

        private void StartState(IEnumerator coroutine)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(coroutine);
        }

        private void PlaySound()
        {
            if (_soundType == SoundType.Music)
                _source.volume = _isMusicOn ? _volume : 0;
            else
                _source.volume = _isSfxOn ? _volume : 0;

            _source.Play();

            if (_duration == 0)
                StartState(WaitPlaySound(_source.clip.length));
            else
                StartState(WaitPlaySound(_duration));
        }

        private IEnumerator WaitPlaySound(float timeToStop)
        {
            while (_source.time < timeToStop)
            {
                yield return null;
            }

            StartState(FadeSound());
        }

        private IEnumerator FadeSound()
        {
            if (_smoothFade == false)
            {
                RestartPlaySound();
                yield break;
            }

            float timeElapsed = 0;

            while (timeElapsed < _timeToFade)
            {
                _source.volume = Mathf.Lerp(_volume, 0, timeElapsed / _timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            RestartPlaySound();
        }

        private void RestartPlaySound()
        {
            _source.Stop();

            if (_isLoop && _isStopped == false)
                PlaySound();
        }
    }
}