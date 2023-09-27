using System.Collections.Generic;
using Assets.TeachingLevels.StateMachines.TeachingTransit;
using UnityEngine;

namespace Assets.TeachingLevels.StateMachines.TeachingStates
{
    public abstract class State : MonoBehaviour
    {
        [SerializeField] private List<Transition> _transitions;

        public void Enter()
        {
            if (enabled == false)
            {
                enabled = true;

                foreach (Transition transition in _transitions)
                {
                    transition.enabled = true;
                }
            }
        }

        public State GetNext()
        {
            foreach (Transition transition in _transitions)
            {
                if (transition.NeedTransit)
                    return transition.TargetState;
            }
        
            return null;
        }

        public void Exit()
        {
            if (enabled)
            {
                foreach (Transition transition in _transitions)
                {
                    transition.enabled = false;
                }

                enabled = false;
            }
        }
    }
}