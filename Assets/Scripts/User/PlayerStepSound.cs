using Assets.Scripts.GenerationSystem.LevelMovement;
using Assets.Scripts.SoundSystem;
using UnityEngine;

namespace Assets.User
{
    public class PlayerStepSound : MonoBehaviour
    {
        [SerializeField] private SoundService _sound;
        [SerializeField] private float _stepTime = 2f;
        [SerializeField] private AgentMovement _agentMovement;

        private float _timeLastStep;

        private void Update()
        {
            if (_agentMovement.IsMovement)
            {
                if (_timeLastStep >= _stepTime)
                {
                    _timeLastStep = 0;
                    _sound.Play();
                }
            }

            _timeLastStep += Time.deltaTime;
        }
    }
}