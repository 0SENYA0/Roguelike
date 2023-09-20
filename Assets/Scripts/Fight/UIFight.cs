using Assets.Enemy;
using Assets.Player;
using Assets.Scripts.GenerationSystem.LevelMovement;
using Assets.Scripts.InteractiveObjectSystem;
using DefaultNamespace.Tools;
using UnityEngine;

namespace Assets.Fight
{
    public class UIFight : MonoBehaviour
    {
        [SerializeField] private Transform _globalMap;
        [SerializeField] private Transform _battlefieldMap;
        [SerializeField] private FightPlace _fightPlace;
        [SerializeField] private InteractiveObjectHandler _interactiveObjectHandler;

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

        public void SetActiveFightPlace(IPlayerPresenter playerPresenter, IEnemyPresenter enemyPresenter)
        {
            _battlefieldMap.gameObject.SetActive(true);
            _globalMap.gameObject.SetActive(false);

            _fightPlace.Set(playerPresenter, enemyPresenter);
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
            ConsoleTools.LogSuccess("ДОЛЖНА ВЫПАСТЬ НАГРАДА ИГРОКУ!!!");
        }
    }
}