using Agava.YandexGames;
using Assets.Interface;
using Assets.UI.SettingsWindow.Localization;
using UnityEngine;

namespace Assets.Infrastructure.States
{
    public class BootstrapState : IState
    {
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
            _sceneLoader.LoadScene("Menu");
            _gameStateMachine.Enter<MainMenuState>();
        }

        private void SetStartLanguage()
        {
            if (Game.GameSettings != null && Game.GameSettings.CurrentLocalization == "")
            {
                var lang = Language.DefineLanguage(YandexGamesSdk.Environment.i18n.lang);
                Game.GameSettings.ChangeLocalization(lang);
            }

            if (PlayerAccount.IsAuthorized)
            {
                PlayerAccount.GetProfileData(SetPlayerName);
            }
            
            _sceneLoader.LoadScene("Menu");
            _gameStateMachine.Enter<MainMenuState>();
        }

        private void SetPlayerName(PlayerAccountProfileDataResponse playerData)
        {
            Game.GameSettings.SetPlayerProfileName(playerData.publicName);
            
            _sceneLoader.LoadScene("Menu");
            _gameStateMachine.Enter<MainMenuState>();
        }

        public void Exit()
        {
            Debug.Log("BootstrapState exited");
        }
    }
}