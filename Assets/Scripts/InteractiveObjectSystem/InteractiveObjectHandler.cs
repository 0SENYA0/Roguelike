using System;
using System.Collections;
using Assets.Enemy;
using Assets.Fight;
using Assets.Loot;
using Assets.Person;
using Assets.Player;
using Assets.Scripts.GenerationSystem.LevelMovement;
using Assets.Scripts.InteractiveObjectSystem.RandomEventSystem;
using Assets.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.InteractiveObjectSystem
{
    public class InteractiveObjectHandler : MonoBehaviour
    {
        [SerializeField] private MouseClickTracker _clickTracker;
        [SerializeField] private AgentMovement _agent;
        [SerializeField] private float _minDistanceToStartBattle = 10.1f;
        [SerializeField] private Button _closeButton;
        [Space] [SerializeField] private UIFight _battlefild;
        [SerializeField] private RandomEventView _eventPanel;
        [SerializeField] private LootInfoView _lootInfoView;
        
        private InteractiveObject _targetObject;
        private float _distance;

        private void Awake()
        {
            //_closeButton.onClick.AddListener(CloseBattlefild);
        }

        private void OnDestroy()
        {
            //_closeButton.onClick.RemoveListener(CloseBattlefild);
        }

        public void ProduceInteraction(InteractiveObject targetObject, Vector3 targetPosition)
        {
            _targetObject = targetObject;
            _clickTracker.enabled = false;

            Action openPanel = () => { };

            IPlayerPresenter playerPresenter = FindObjectOfType<PlayerView>().PlayerPresenter;
            Debug.Log($"count weapons in InteractiveObjectHandler {playerPresenter.Player.PlayerInventary is null}");
            Debug.Log($"count weapons in InteractiveObjectHandler {playerPresenter.Player.PlayerInventary.Weapon.Length}");

            if (targetObject.TryGetComponent(out EnemyView enemyView))
                openPanel = () => { Curtain.Instance.ShowAnimation(() => 
                    { _battlefild.SetActiveFightPlace(playerPresenter, enemyView.EnemyPresenter); }); };
             else if (targetObject.TryGetComponent(out InteractiveLootObject lootObject))
                openPanel = () => { _lootInfoView.Show(lootObject); };
            else if (targetObject.TryGetComponent(out InteractiveRandomEventObject randomEventObject))
                openPanel = CreateRandomEvent(randomEventObject);

            StartCoroutine(GoToTarget(targetPosition, openPanel));
        }

        public void ReturnToGlobalMap()
        {
            _clickTracker.enabled = true;
            _targetObject.DestroyObject();
        }

        // TODO временное решешие. Сам скрипт UIFight должен принимать InteractiveObjectHandler и вызывать метод ReturnToGlobalMap
        private void CloseBattlefild()
        {
            Curtain.Instance.ShowAnimation(() => { _battlefild.gameObject.SetActive(false); });
            ReturnToGlobalMap();
        }

        private Action CreateRandomEvent(InteractiveRandomEventObject randomEventObject)
        {            
            IPlayerPresenter playerPresenter = FindObjectOfType<PlayerView>().PlayerPresenter;

            LevelRandomEvent levelRandomEvent = new LevelRandomEvent();
            RandomEventType randomEvent = levelRandomEvent.GetRandomEvent();

            switch (randomEvent)
            {
                case RandomEventType.Enemy:
                    return () => { Curtain.Instance.ShowAnimation(
                        () => { _battlefild.SetActiveFightPlace(playerPresenter, randomEventObject.EnemyPresenter); }); };                // case RandomEventType.Loot:
                case RandomEventType.Loot:
                    return () => { _lootInfoView.Show(randomEventObject.InteractiveLootObject); };
                case RandomEventType.AD:
                    return () => { _eventPanel.ShowPanel(this); };
                   default:
                       return () => { Curtain.Instance.ShowAnimation(
                        () => { _battlefild.SetActiveFightPlace(playerPresenter, randomEventObject.EnemyPresenter); }); };
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