using Assets.Interface;

namespace Assets.Infrastructure
{
    public class Game
    {
        private GameStateMachine _gameStateMachine;
        private static GameSettings _gameSettings;

        public Game(ICoroutineRunner coroutineRunner)
        {
            _gameSettings = new GameSettings();
            _gameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), new SdkLoader(coroutineRunner));
        }

        public static GameSettings GameSettings => _gameSettings;

        public GameStateMachine GameStateMachine => _gameStateMachine;
    }
}