using Assets.Scripts.GenerationSystem.LevelMovement;
using UnityEngine;

namespace Assets.Player
{
    public class PlayerStepSound : MonoBehaviour
    {
        //[SerializeField] private SoundComponent _sound;
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
                    //_sound.Play();
                }
            }

            _timeLastStep += Time.deltaTime;
        }
    }
}