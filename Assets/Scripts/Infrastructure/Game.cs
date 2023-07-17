namespace Infrastructure
{
    public class Game
    {
        private GameStateMachine _gameStateMachine;
        
        public Game()
        {
            _gameStateMachine = new GameStateMachine();
        }
        
        public GameStateMachine GameStateMachine => _gameStateMachine;
    }
}