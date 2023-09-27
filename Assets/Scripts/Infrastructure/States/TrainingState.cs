using Assets.Interface;

namespace Assets.Infrastructure.States
{
    public class TrainingState : IExitableState, IState
    {
        private readonly SceneLoader _sceneLoader;

        public TrainingState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            _sceneLoader.LoadScene("TrainingLevel");
        }
    }
}