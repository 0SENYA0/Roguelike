using System;
using Assets.TeachingLevels.StateMachines.TeachingStates;
using UnityEngine;

namespace Assets.TeachingLevels.StateMachines
{
    public class TeachingStateMachine : MonoBehaviour
    {
        [SerializeField] private State _firstState;

        public State CurrentState { get; private set; }

        private void Start()
        {
            Reset(_firstState);
        }

        private void Update()
        {
            if (CurrentState == null)
            {
                return;
            }

            State nextState = CurrentState.GetNext();

            if (nextState != null)
            {
                Transit(nextState);
            }
        }

        private void Transit(State nextState)
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
            }

            CurrentState = nextState;

            if (CurrentState != null)
            {
                CurrentState.Enter();
            }
        }
        
        private void Reset(State firstState)
        {
            CurrentState = firstState;
        
            if (CurrentState != null)
            {
                CurrentState.Enter();
            }
        }
    }
}