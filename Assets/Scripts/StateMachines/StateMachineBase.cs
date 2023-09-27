using Assets.Infrastructure;
using Assets.Interface;

namespace Assets.StateMachines
{
    public class StateMachineBase : IStateMachine
    {
        public void Enter<TState>() where TState : class, IState
        {
            throw new System.NotImplementedException();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            throw new System.NotImplementedException();
        }
    }
}