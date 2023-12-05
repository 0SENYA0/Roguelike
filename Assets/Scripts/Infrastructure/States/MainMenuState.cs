using Assets.Interface;
using Assets.Observces;

namespace Assets.Infrastructure.States
{
    public class MainMenuState : IState, IButtonObserver
    {
        private const string GenerationStateName = "LevelGeneration";
        
        private readonly SceneLoader _sceneLoader;

        public MainMenuState(SceneLoader sceneLoader) =>
            _sceneLoader = sceneLoader;

        public void Enter() => 
            MainMenuButtonPlayObserver.Instance.Registry(this);

        public void Exit() => 
            MainMenuButtonPlayObserver.Instance.UnRegistry(this);

        public void Update() =>
            _sceneLoader.LoadScene(GenerationStateName);
    }
}