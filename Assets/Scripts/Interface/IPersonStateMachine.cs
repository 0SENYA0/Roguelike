using Assets.Person.PersonStates;

namespace Assets.Interface
{
    public interface IPersonStateMachine
    {
        public void SetState(PersonState newState);
    }
}