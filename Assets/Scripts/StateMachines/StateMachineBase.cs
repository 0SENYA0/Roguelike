using System;
using System.Collections.Generic;
using Assets.Infrastructure;
using Assets.Interface;

namespace Assets.StateMachines
{
    public abstract class StateMachineBase : IStateMachine
    {
        protected Dictionary<Type, IExitableState> _states;
        protected IExitableState _currentState;
        
        public void Enter<TState>() where TState : class, IState
        {
            IState newState = ChangeState<TState>();
            newState.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            TState newState = ChangeState<TState>();
            newState.Enter(payload);
        }
        
        protected TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();

            TState newState = GetState<TState>();
            _currentState = newState;

            return newState;
        }

        protected TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}