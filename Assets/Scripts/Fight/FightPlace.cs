using System;
using System.Collections.Generic;
using Assets.Config;
using Assets.Enemy;
using Assets.Fight.Dice;
using Assets.Interface;
using Assets.ScriptableObjects;
using Assets.Scripts.UI.Widgets;
using Assets.User;
using Assets.Utils;
using UnityEngine;

namespace Assets.Fight
{
    public class FightPlace : MonoBehaviour, ICoroutineRunner
    {
        private const int Enemy = 1;
        private const int TwoEnemy = 2;
        private const int ThreeEnemy = 3;
        private const int Boss = 4;

        [SerializeField] private PlayerPoint _playerPosition;
        [SerializeField] private List<EnemyPoint> _spawnPoints;
        [SerializeField] private StepFightView _stepFightView;

        [Space(25)] [SerializeField] private DiceCanvasGroup _diceGroup;
        [SerializeField] private DiceView _leftDice;
        [SerializeField] private DiceView _centerDice;
        [SerializeField] private DiceView _rightDice;

        [Space(25)] [SerializeField] private PlayerWeaponPanel _elementsPanel;
        [SerializeField] private PlayerAttackView _playerAttackView;
        [SerializeField] private List<EnemyAttackView> _enemyAttackViews;
        [SerializeField] private GameObject _popupReady;
        [SerializeField] private CustomButton _customButtonReady;

        private Fight _fight;
        private EnemyPoint _spawnPoint;
        private PlayerWeaponPanel _elementsDamagePanel;
        private IPlayerPresenter _playerPresenter;
        private RectTransform _playerRectTransform;

        public event Action<FightResult> FightEnded;

        private void OnEnable()
        {
            _elementsDamagePanel = _elementsPanel;
            _playerRectTransform = _playerPosition.GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (_spawnPoint != null)
                ShowEnemyUI(_spawnPoint);

            _playerAttackView.transform.position = GetScreenCoordinates(_playerRectTransform).center;
        }

        public Rect GetScreenCoordinates(RectTransform uiElement)
        {
            Vector3[] worldCorners = new Vector3[4];
            uiElement.GetWorldCorners(worldCorners);
            Rect result = new Rect(
                worldCorners[0].x,
                worldCorners[0].y,
                worldCorners[2].x - worldCorners[0].x,
                worldCorners[2].y - worldCorners[0].y);
            return result;
        }

        public void OnLeaveFight()
        {
            FightEnded?.Invoke(FightResult.Leave);
            _fight.FightEnded -= EndFight;
            _fight.ShowDice -= ShowDice;
            _fight.HideDice -= HideDice;
            _fight.Dispose();
            _fight = null;
            HideDice();
        }

        public void Set(IPlayerPresenter playerPresenter, IEnemyPresenter enemyPresenter)
        {
            _playerPresenter = playerPresenter;

            foreach (EnemyAttackView unitAttackView in _enemyAttackViews)
            {
                unitAttackView.Reset();
                unitAttackView.gameObject.SetActive(false);
            }

            SelectEnemyUIPosition(enemyPresenter);

            PlayerAttackPresenter playerAttackPresenter =
                new PlayerAttackPresenter(playerPresenter.Player, _playerAttackView);

            List<EnemyAttackPresenter> enemyAttackPresenters = new List<EnemyAttackPresenter>();

            for (int i = 0; i < enemyPresenter.Enemy.Count; i++)
                enemyAttackPresenters.Add(new EnemyAttackPresenter(enemyPresenter.Enemy[i], _enemyAttackViews[i]));

            _fight = new Fight(this, enemyAttackPresenters, playerAttackPresenter, _stepFightView,
                GetDicePresenterAdapter(), _elementsDamagePanel, _popupReady, _customButtonReady);
            _fight.FightEnded += EndFight;
            _fight.ShowDice += ShowDice;
            _fight.HideDice += HideDice;
            _fight.Start();
        }

        private void HideDice() =>
            _diceGroup.HideCanvas();

        private void ShowDice() =>
            _diceGroup.ShowCanvas();

        private void EndFight()
        {
            FightEnded?.Invoke(_playerPresenter.Player.Health > PlayerHealth.MinPlayerHealth
                ? FightResult.Win
                : FightResult.Lose);

            _fight.FightEnded -= EndFight;
            _fight.ShowDice -= ShowDice;
            _fight.HideDice -= HideDice;
            _fight.Dispose();
            _fight = null;
        }

        private DicePresenterAdapter GetDicePresenterAdapter()
        {
            DiceSpriteScriptableObject scriptableObject =
                Resources.Load<DiceSpriteScriptableObject>("BlackDiceSpriteScriptableObject");

            DicePresenter leftDicePresenter =
                new DicePresenter(_leftDice, new DiceModel(scriptableObject.Sprites), this);
            DicePresenter centerDicePresenter =
                new DicePresenter(_centerDice, new DiceModel(scriptableObject.Sprites), this);
            DicePresenter rightDicePresenter =
                new DicePresenter(_rightDice, new DiceModel(scriptableObject.Sprites), this);

            return new DicePresenterAdapter(leftDicePresenter, centerDicePresenter, rightDicePresenter);
        }

        private void SelectEnemyUIPosition(IEnemyPresenter enemyPresenter)
        {
            switch (enemyPresenter.Enemy.Count)
            {
                case Enemy:
                    DisableAllEnemyUI();

                    if (CheckOnTheBoss(enemyPresenter.Enemy[Enemy - 1]))
                        break;

                    _spawnPoint = _spawnPoints[Enemy - 1];
                    ShowEnemyUI(_spawnPoint);
                    break;
                case TwoEnemy:
                    DisableAllEnemyUI();
                    _spawnPoint = _spawnPoints[TwoEnemy - 1];
                    ShowEnemyUI(_spawnPoint);
                    break;
                case ThreeEnemy:
                    DisableAllEnemyUI();
                    _spawnPoint = _spawnPoints[ThreeEnemy - 1];
                    ShowEnemyUI(_spawnPoint);
                    break;
            }
        }

        private bool CheckOnTheBoss(Enemy.Enemy enemy)
        {
            if (enemy.IsBoss)
            {
                _spawnPoint = _spawnPoints[Boss - 1];
                ShowEnemyUI(_spawnPoint);
                return true;
            }

            return false;
        }

        private void ShowEnemyUI(EnemyPoint spawnPoint)
        {
            for (int i = 0; i < spawnPoint.Points.Count; i++)
            {
                RectTransform rectTransform = spawnPoint.Points[i].GetComponent<RectTransform>();

                _enemyAttackViews[i].transform.position = GetScreenCoordinates(rectTransform).center;
                _enemyAttackViews[i].gameObject.SetActive(true);
            }

            spawnPoint.gameObject.SetActive(true);
        }

        private void DisableAllEnemyUI()
        {
            foreach (SpawnPoint spawnPoint in _spawnPoints)
                spawnPoint.gameObject.SetActive(false);
        }
    }
}