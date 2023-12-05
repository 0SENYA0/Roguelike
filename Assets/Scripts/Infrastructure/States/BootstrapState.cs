using Agava.YandexGames;
using Assets.Interface;
using Assets.UI.SettingsWindow.Localization;
using UnityEngine;

namespace Assets.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string MenuSceneName = "Menu";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SdkLoader _sdkLoader;
        private readonly SceneLoader _sceneLoader;        

        public BootstrapState(GameStateMachine gameStateMachine, SdkLoader sdkLoader, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sdkLoader = sdkLoader;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            _sdkLoader.LoadSdk(onSuccessCallback: SetStartLanguage);
#endif
            _sceneLoader.LoadScene(MenuSceneName);
            _gameStateMachine.Enter<MainMenuState>();
        }

        public void Exit()
        {
        }
    }
}