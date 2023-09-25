using System.Collections;
using Assets.Infrastructure;
using UnityEngine;

namespace Assets.Scripts.SoundSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundComponent : MonoBehaviour
    {
        [SerializeField] private SoundType _soundType;
        [SerializeField] private AudioClip _clip;
        [SerializeField] private bool _playOnAwake;
        [SerializeField] [Range(0, 1)] private float _volume;

        [Tooltip("If zero is specified, then the sound will be played completely")] 
        [SerializeField] [Min(0f)] private float _duration;

        [Space(10f)] 
        [SerializeField] private bool _isLoop;
        [SerializeField] private bool _isRestartIfCalledAgain;

        [Space(10f)] 
        [SerializeField] private bool _smoothFade;
        [SerializeField] private float _timeToFade;

        private AudioSource _source;
        private Coroutine _coroutine;
        private SoundState _state;

        private bool _isStopped;
        private bool _isMusicOn;
        private bool _isSfxOn;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _source.clip = _clip;
            _source.loop = false;
            _source.playOnAwake = false;

            _state = SoundState.Stop;

            if (Game.GameSettings != null)
            {
                _isMusicOn = Game.GameSettings.Sound.IsMusicOn;
                _isSfxOn = Game.GameSettings.Sound.IsSfxOn;
            }
            else
            {
                _isMusicOn = true;
                _isSfxOn = true;
            }
        }

        private void OnEnable()
        {
            if (Game.GameSettings == null)
                return;
            
            Game.GameSettings.Sound.OnMusicStateChanged += ChangeMusicState;
            Game.GameSettings.Sound.OnSfxStateChanged += ChangeSfxState;
            Game.GameSettings.Sound.OnPauseStateChanged += ChangePauseState;
        }

        private void OnDisable()
        {
            if (Game.GameSettings == null)
                return;
            
            Game.GameSettings.Sound.OnMusicStateChanged -= ChangeMusicState;
            Game.GameSettings.Sound.OnSfxStateChanged -= ChangeSfxState;
            Game.GameSettings.Sound.OnPauseStateChanged -= ChangePauseState;
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
            bool isMusicOn = _soundType == SoundType.Music && _isMusicOn;
            bool isSfxOn = _soundType == SoundType.SFX && _isSfxOn;

            if (isMusicOn || isSfxOn)
                StartState(FadeSound());
            else
                StopAndTryRestartSound();
        }

        private void ChangeMusicState(bool value)
        {
            if (_soundType == SoundType.Music)
            {
                _isMusicOn = value;
                _source.volume = _isMusicOn ? _volume : 0;
            }
        }

        private void ChangeSfxState(bool value)
        {
            if (_soundType == SoundType.SFX)
            {
                _isSfxOn = value;
                _source.volume = _isSfxOn ? _volume : 0;
            }
        }

        private void ChangePauseState(bool value)
        {
            if (value)
                ApplyPause();
            else
                ApplyUpPause();
        }

        private void ApplyPause()
        {
            if (_state == SoundState.Stop)
                return;

            _source.Pause();

            if (_state == SoundState.Fade && _coroutine != null)
                StopCoroutine(_coroutine);
        }

        private void ApplyUpPause()
        {
            if (_state == SoundState.Play)
            {
                _source.Play();
            }
            else if (_state == SoundState.Fade)
            {
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }

                StopAndTryRestartSound();
            }
        }

        private void StartState(IEnumerator coroutine)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(coroutine);
        }

        private void PlaySound()
        {
            if (_source.clip == null)
            {
                Debug.LogWarning($"There is no music video on the object {gameObject.name}");
                return;
            }
            
            SetCorrectVolume();
            _state = SoundState.Play;
            _source.Play();

            if (_duration == 0)
                StartState(WaitPlaySound(_source.clip.length));
            else
                StartState(WaitPlaySound(_duration));
        }

        private void SetCorrectVolume()
        {
            if (_soundType == SoundType.Music)
                _source.volume = _isMusicOn ? _volume : 0;
            else
                _source.volume = _isSfxOn ? _volume : 0;
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
            _state = SoundState.Fade;

            if (_smoothFade == false)
            {
                StopAndTryRestartSound();
                yield break;
            }

            float timeElapsed = 0;

            while (timeElapsed < _timeToFade)
            {
                _source.volume = Mathf.Lerp(_volume, 0, timeElapsed / _timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            StopAndTryRestartSound();
        }

        private void StopAndTryRestartSound()
        {
            _state = SoundState.Stop;
            _source.Stop();

            if (_isLoop && _isStopped == false)
                PlaySound();
        }
    }
}