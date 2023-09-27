using System.Collections;
using Assets.Infrastructure;
using Assets.UI.SettingsWindow.Localization;
using UnityEngine;

namespace Assets.TeachingLevels.StateMachines.TeachingStates
{
    public class TeachingWelcomeText : State
    {
        [SerializeField] private TeachingAreaText _teachingAreaText;
        [SerializeField] private TeachingTextScriptableObject _welcomeText;
        
        private const int CountStringLine = 5;
        
        public bool skipText = false;
        private bool isPrint = false;

        private string _currentText;
        private Coroutine _coroutine;
        
        private void OnEnable()
        {
            _currentText = _welcomeText.TextRU;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(TextPrint(0.1f, skipText));
        }

        private void SetCurrentText()
        {
            switch (Game.GameSettings.CurrentLocalization)
            {
                case Language.ENG:
                    _currentText = _welcomeText.TextEN;
                    break;
                case Language.RUS:
                    _currentText = _welcomeText.TextRU;
                    break;
                case Language.TUR:
                    _currentText = _welcomeText.TextTR;
                    break;
            }
        }

        private IEnumerator TextPrint(float delay, bool skip)
        {
            if (isPrint)
                yield break;

            isPrint = true;
            
            //_teachingAreaText.SoundComponent.Play();
            for (int i = 1; i <= _currentText.Length; i++)
            {
                if (skip)
                {
                    _teachingAreaText.Text.text = _currentText;
                    yield break;
                }

                _teachingAreaText.Text.text = _currentText.Substring(0, i);
                yield return new WaitForSeconds(delay);
            }
            
            //_teachingAreaText.SoundComponent.Stop();
            isPrint = false;
        }
    }
}