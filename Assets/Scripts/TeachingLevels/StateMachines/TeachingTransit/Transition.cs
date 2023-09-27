using Assets.TeachingLevels.StateMachines.TeachingStates;
using UnityEngine;

namespace Assets.TeachingLevels.StateMachines.TeachingTransit
{
    public abstract class Transition : MonoBehaviour
    {
        [SerializeField] private State _targetState;
    
        public bool NeedTransit { get; protected set; }

        public State TargetState => _targetState;

        private void OnEnable()
        {
            NeedTransit = false;
        }
    }
}