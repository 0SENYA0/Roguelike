using System.Collections.Generic;
using UnityEngine;

namespace Assets.Person.PersonStates
{
    public class PersonStateMachine : MonoBehaviour, IPersonStateMachine
    {
        [SerializeField] private List<PersonState> _personStates;

        private PersonState _currentState;

        public void SetState(PersonState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }
}