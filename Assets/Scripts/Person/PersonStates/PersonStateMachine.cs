using Assets.Interface;

namespace Assets.Person.PersonStates
{
    public class PersonStateMachine : IPersonStateMachine
    {
        private IUnitState _currentState;

        public void SetState<T>(T newState) where T : IUnitState
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }

}