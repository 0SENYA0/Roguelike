using System;
using System.Collections;
using Assets.Enemy;
using Assets.Fight;
using Assets.Inventory;
using Assets.Loot;
using Assets.Person;
using Assets.Player;
using Assets.Scripts.GenerationSystem.LevelMovement;
using Assets.Scripts.InteractiveObjectSystem.RandomEventSystem;
using Assets.UI;
using DefaultNamespace.Tools;
using UnityEngine;

namespace Assets.Scripts.InteractiveObjectSystem
{
    public class InteractiveObjectHandler : MonoBehaviour
    {
        [SerializeField] private MouseClickTracker _clickTracker;
        [SerializeField] private AgentMovement _agent;
        [SerializeField] private float _minDistanceToStartBattle = 10.1f;
        [Space] 
        [SerializeField] private UIFight _battlefild;

        private InteractiveObject _targetObject;
        private IPlayerPresenter _playerPresenter;
        private float _distance;

        public void ProduceInteraction(InteractiveObject targetObject, Vector3 targetPosition)
        {
            _targetObject = targetObject;
            _clickTracker.enabled = false;

            Action openPanel = () => { };

            _playerPresenter ??= FindObjectOfType<PlayerView>().PlayerPresenter;

            if (targetObject.TryGetComponent(out EnemyView enemyView))
            {
                openPanel = () =>
                {
                    Curtain.Instance.ShowAnimation(() =>
                    {
                        _battlefild.SetActiveFightPlace(_playerPresenter, enemyView.EnemyPresenter);
                    });
                };
            }
            else if (targetObject.TryGetComponent(out InteractiveLootObject lootObject))
            {
                openPanel = () =>
                {
                    IInventoryItem newItem = lootObject.Weapon != null ? lootObject.Weapon : lootObject.Armor;
                    _playerPresenter.PlayerView.InventoryPresenter.InventoryModel.AddItem(newItem);
                    
                    _clickTracker.enabled = true;
                    _targetObject.DestroyObject();
                };
            }
            else if (targetObject.TryGetComponent(out InteractiveRandomEventObject randomEventObject))
            {
                openPanel = CreateRandomEvent(_playerPresenter, randomEventObject);
            }

            StartCoroutine(GoToTarget(targetPosition, openPanel));
        }

        public void ReturnToGlobalMap()
        {
            _clickTracker.enabled = true;
            _targetObject.DestroyObject();
        }

        private Action CreateRandomEvent(IPlayerPresenter player, InteractiveRandomEventObject randomEventObject)
        {
            LevelRandomEvent levelRandomEvent = new LevelRandomEvent();
            RandomEventType randomEvent = levelRandomEvent.GetRandomEvent();
            Debug.Log($"{randomEvent}");
            
            switch (randomEvent)
            {
                case RandomEventType.Enemy:
                    return () =>
                    {
                        Curtain.Instance.ShowAnimation(
                            () =>
                            {
                                _battlefild.SetActiveFightPlace(player, randomEventObject.GetRandomEnemy());
                            });
                    };
                case RandomEventType.Loot:
                    return () =>
                    {
                        IInventoryItem newItem = randomEventObject.GetRandomLoot();
                        _playerPresenter.PlayerView.InventoryPresenter.InventoryModel.AddItem(newItem);
                    
                        _clickTracker.enabled = true;
                        _targetObject.DestroyObject();
                    };
                case RandomEventType.AD:
                    return () =>
                    {
                        ConsoleTools.LogSuccess("Идет показ рекламы");
                        _clickTracker.enabled = true;
                        _targetObject.DestroyObject();
                    };
                default:
                    throw new Exception("Рандом сломался :(");
            }
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

            action();
        }
    }
}