using System;
using UnityEngine;

namespace Assets.TeachingLevels
{
    public class TeachingLevel : MonoBehaviour
    {
        [SerializeField] private TrainingInfo _trainingInfo;
        private void Awake()
        {
            _trainingInfo.StartTeach();
        }
    }
}