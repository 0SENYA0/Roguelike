using System.Collections;
using Agava.YandexGames;
using DefaultNamespace.Extension;
using DefaultNamespace.Interface;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        
        private const string EnglishLanguage = "en";
        private const string RussionLanguage = "ru";
        private const string TurkishLanguage = "tr";

        public BootstrapState(GameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        public void Enter()
        {
            CustomCoroutine.Start(InitSdk());
            Debug.Log("BootstrapState - Enter - Установлен нужный язык");
        }

        private IEnumerator InitSdk()
        {
            yield return new WaitForSeconds(4);
            //yield return YandexGamesSdk.Initialize(SetStartLanguage);
        }   

        private void SetStartLanguage()
        {
            switch (YandexGamesSdk.Environment.i18n.lang)
            {
                case EnglishLanguage:
                    Debug.Log(EnglishLanguage);
                    break;
                case RussionLanguage:
                    Debug.Log(RussionLanguage);
                    break;
                case TurkishLanguage:
                    Debug.Log(TurkishLanguage);
                    break;
                default:
                    Debug.Log("хуета");
                    break;
            }

            SceneManager.LoadScene(1);
        }

        public void Exit()
        {
        }
    }
}