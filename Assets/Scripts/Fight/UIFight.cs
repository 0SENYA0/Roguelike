using Assets.Enemy;
using Assets.Player;
using Assets.Scripts.GenerationSystem.LevelMovement;
using Assets.Scripts.InteractiveObjectSystem;
using UnityEngine;

namespace Assets.Fight
{
    public class UIFight : MonoBehaviour
    {
        [SerializeField] private Transform _globalMap;
        [SerializeField] private Transform _battlefieldMap;
        [SerializeField] private FightPlace _fightPlace;
        [SerializeField] private ElementsSpriteView _elementsSpriteView;
        [SerializeField] private InteractiveObjectHandler _interactiveObjectHandler;
        private IPlayerPresenter _playerPresenter;
        private IEnemyPresenter _enemyPresenter;
        private MouseClickTracker _mouseClickTracker;

        private void OnEnable()
        {
            _fightPlace.FightEnded += ShowGlobalMap;
            _fightPlace.FightEnded += HideFightMap;
        }

        private void OnDisable()
        {
            _fightPlace.FightEnded -= ShowGlobalMap;
            _fightPlace.FightEnded -= HideFightMap;
        }

        private void HideFightMap()
        {
            _battlefieldMap.gameObject.SetActive(false);
        }

        private void ShowGlobalMap()
        {
            _globalMap.gameObject.SetActive(true);
            _battlefieldMap.gameObject.SetActive(false);
            _interactiveObjectHandler.ReturnToGlobalMap();
        }
        
        public void SetActiveFightPlace(IPlayerPresenter playerPresenter, IEnemyPresenter enemyPresenter)
        {
            _enemyPresenter = enemyPresenter;
            _playerPresenter = playerPresenter;
            _mouseClickTracker = _playerPresenter.PlayerView.gameObject.GetComponent<MouseClickTracker>();
            _battlefieldMap.gameObject.SetActive(true);
            _globalMap.gameObject.SetActive(false);

            _fightPlace.Set(_playerPresenter, _enemyPresenter, _elementsSpriteView);
        }
    }
}