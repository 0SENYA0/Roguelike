using System.Collections.Generic;
using Assets.Enemy;
using Assets.Fight.Dice;
using Assets.Interface;
using Assets.Person;
using Assets.Player;
using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.Fight
{
    public class FightPlace : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private PlayerPoint _playerPosition;
        [SerializeField] private List<EnemyPoint> _spawnPoints;

        [SerializeField] private StepFightView _stepFightView;

        [Space(25)] [SerializeField] private DiceView _leftDice;
        [SerializeField] private DiceView _centerDice;
        [SerializeField] private DiceView _rightDice;

        [Space(25)] [SerializeField] private UnitFightView _playerFightView;
        [SerializeField] private List<UnitFightView> _enemiesFightView;

        private Fight _fight;

        private const int Enemy = 1;
        private const int TwoEnemy = 2;
        private const int ThreeEnemy = 3;
        private const int Boss = 4;

        private void Start()
        {
            RectTransform playerUIPosition = _playerPosition.GetComponent<RectTransform>();
            Vector3 vector3 = _playerFightView.transform.InverseTransformPoint(playerUIPosition.rect.center);
            Debug.Log("local vector " + vector3);
            Debug.Log("global vector "+ _playerFightView.transform.TransformPoint(playerUIPosition.rect.center));

            _playerFightView.transform.position = vector3;
        }

        public void Set(PlayerPresenter playerPresenter, EnemyPresenter enemyPresenters)
        {
            foreach (UnitFightView enemyFightView in _enemiesFightView)
                enemyFightView.gameObject.SetActive(false);

            switch (enemyPresenters.Enemies.Count)
            {
                case Enemy:
                    DisableAllEnemyUI();

                    if (CheckOnTheBoss(enemyPresenters.Enemies[Enemy - 1]))
                        break;

                    ShowEnemyUI(_spawnPoints[Enemy - 1]);
                    break;
                case TwoEnemy:
                    DisableAllEnemyUI();
                    ShowEnemyUI(_spawnPoints[TwoEnemy - 1]);
                    break;
                case ThreeEnemy:
                    DisableAllEnemyUI();
                    ShowEnemyUI(_spawnPoints[ThreeEnemy - 1]);
                    break;
            }

            EnemyFightAdapter enemyFightAdapter = new EnemyFightAdapter(enemyPresenters, _enemiesFightView);
            PlayerFightAdapter playerFightAdapter = new PlayerFightAdapter(playerPresenter, _playerFightView);

            DiceSpriteScriptableObject scriptableObject = Resources.Load<DiceSpriteScriptableObject>("BlackDiceSpriteScriptableObject");

            DicePresenter leftDicePresenter = new DicePresenter(_leftDice, new DiceModel(scriptableObject.Sprites), this);
            DicePresenter centerDicePresenter = new DicePresenter(_centerDice, new DiceModel(scriptableObject.Sprites), this);
            DicePresenter rightDicePresenter = new DicePresenter(_rightDice, new DiceModel(scriptableObject.Sprites), this);

            _fight = new Fight(this, enemyFightAdapter.EnemyFightPresenters, playerFightAdapter.PlayerFightPresenter, _stepFightView,
                new DicePresenterAdapter(leftDicePresenter, centerDicePresenter, rightDicePresenter));

            _fight.Start();
        }

        private void OnDisable() =>
            _fight.Dispose();

        private bool CheckOnTheBoss(Enemy.Enemy enemy)
        {
            if (enemy.IsBoss)
            {
                ShowEnemyUI(_spawnPoints[Boss - 1]);
                return true;
            }

            return false;
        }

        private void ShowEnemyUI(EnemyPoint spawnPoint)
        {
            spawnPoint.gameObject.SetActive(true);

            for (int i = 0; i < spawnPoint.Points.Count; i++)
            {
                RectTransform rectTransform = spawnPoint.Points[i].GetComponent<RectTransform>();
                _enemiesFightView[i].transform.position = rectTransform.TransformPoint(rectTransform.rect.center);
                _enemiesFightView[i].gameObject.SetActive(true);
                _enemiesFightView[i].enabled = true;
            }
        }

        private void DisableAllEnemyUI()
        {
            foreach (SpawnPoint spawnPoint in _spawnPoints)
                spawnPoint.gameObject.SetActive(false);
        }
    }
}