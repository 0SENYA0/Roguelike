namespace Assets.Interface
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}