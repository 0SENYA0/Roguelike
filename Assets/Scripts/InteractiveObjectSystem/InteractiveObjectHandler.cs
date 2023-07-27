using System.Collections;
using Assets.Scripts.GenerationSystem.LevelMovement;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.InteractiveObjectSystem
{
    public class InteractiveObjectHandler : MonoBehaviour
    {
        [SerializeField] private MouseClickTracker _clickTracker;
        [SerializeField] private AgentMovement _agent;
        [SerializeField] private GameObject _battlefild;
        [SerializeField] private float _minDistanceToStartBattle = 10.1f;
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

        public void ProduceInteraction(InteractiveObject enemy, Vector3 targetPosition)
        {
            _targetObject = enemy;
            _clickTracker.enabled = false;
            StartCoroutine(GoToTarget(targetPosition));
        }

        private void CloseBattle()
        {
            _clickTracker.enabled = true;
            _battlefild.gameObject.SetActive(false);
            _targetObject.DestroyObject();
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
            
            // TODO: Изменить влючение объекта на вызов метода из класса
            _battlefild.gameObject.SetActive(true);
        }
    }
}