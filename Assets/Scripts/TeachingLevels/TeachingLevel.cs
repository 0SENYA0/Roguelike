using System;
using Assets.TeachingLevels.StateMachines.TeachingStates;
using DefaultNamespace;
using UnityEngine;

namespace Assets.TeachingLevels
{
    [RequireComponent(typeof(TeachingWelcomeText))]
    public class TeachingLevel : MonoBehaviour
    {
        [SerializeField] private TrainingInfo _trainingInfo;
        private TeachingWelcomeText _teachingWelcome;
        private void Start()
        {
            _trainingInfo.StartTeach();
            
            _teachingWelcome = GetComponent<TeachingWelcomeText>();
            _teachingWelcome.enabled = true;
        }
    }
}