using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GenerationSystem.LevelMovement
{
    public class BattleHandler : MonoBehaviour
    {
        [SerializeField] private MouseClickTracker _clickTracker;
        [SerializeField] private AgentMovement _agent;
        [SerializeField] private GameObject _battlefild;
        [SerializeField] private float _minDistanceToStartBattle = 0.1f;
        [SerializeField] private Button _closeButton;

        private InteractiveObject _targetObject;
        private float _distance;

        private void Awake()
        {
            _closeButton.onClick.AddListener(CloseBattle);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(CloseBattle);
        }

        public void DisplayBattle(InteractiveObject interactiveObject, Vector3 targetPosition)
        {
            _targetObject = interactiveObject;
            _clickTracker.enabled = false;
            StartCoroutine(GoToTarget(targetPosition));
        }

        private void CloseBattle()
        {
            _clickTracker.enabled = true;
            _battlefild.gameObject.SetActive(false);
            _targetObject.Destroy();
        }

        private IEnumerator GoToTarget(Vector3 targetPosition)
        {
            _agent.SetFixedMovement(targetPosition);
            
            _distance = Vector3.Distance(_agent.transform.position, targetPosition);
            
            while (_distance > _minDistanceToStartBattle)
            {
                yield return null;
                _distance = Vector3.Distance(_agent.transform.position, targetPosition);
            }
            
            _battlefild.gameObject.SetActive(true);
        }
    }
}