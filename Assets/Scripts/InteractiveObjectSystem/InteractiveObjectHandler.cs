using System;
using System.Collections;
using Assets.Fight;
using Assets.Loot;
using Assets.Scripts.GenerationSystem.LevelMovement;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.InteractiveObjectSystem
{
    public class InteractiveObjectHandler : MonoBehaviour
    {
        // Должен отвечать: какое окно включить, если игрок решил пойти к объекту
        
        [SerializeField] private MouseClickTracker _clickTracker;
        [SerializeField] private AgentMovement _agent;
        [SerializeField] private float _minDistanceToStartBattle = 10.1f;
        [SerializeField] private Button _closeButton;
        [Space]
        [SerializeField] private UIFight _battlefild;
        [SerializeField] private RandomLootView _lootPanel;
        [SerializeField] private GameObject _randomEventPanel;

        private InteractiveObject _targetObject;
        private float _distance;

        private void Awake()
        {
            _closeButton.onClick.AddListener(ReturnToGlobalMap);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(ReturnToGlobalMap);
        }

        public void ProduceInteraction(InteractiveObject targetObject, Vector3 targetPosition)
        {
            _targetObject = targetObject;
            _clickTracker.enabled = false;

            Action openPanel = () => {};

            switch (targetObject.Type)
            {
                case ObjectType.Enemy:
                    openPanel = () => { _battlefild.gameObject.SetActive(true);};
                    break;
                case ObjectType.RandomEvent:
                    openPanel = () => { _lootPanel.ShowPanel(this); };
                    break;
                case ObjectType.Loot:
                    openPanel = () => { _lootPanel.ShowPanel(this); };
                    break;
                case ObjectType.Boos:
                    openPanel = () => { Debug.Log("Панелька босса"); };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            StartCoroutine(GoToTarget(targetPosition, openPanel));
        }

        public void ReturnToGlobalMap()
        {
            _clickTracker.enabled = true;
            _targetObject.DestroyObject();
        }

        private IEnumerator GoToTarget(Vector3 targetPosition, Action action)
        {
            _agent.SetFixedMovement(targetPosition);
            
            _distance = Vector3.Distance(_agent.transform.position, targetPosition);
            
            while (_distance > _minDistanceToStartBattle)
            {
                yield return null;
                _distance = Vector3.Distance(_agent.transform.position, targetPosition);
            }
            
            // TODO: Изменить влючение объекта на вызов метода из класса
            // _battlefild.gameObject.SetActive(true);
            action();
        }
    }
}