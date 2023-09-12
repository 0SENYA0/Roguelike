using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Fight.Dice;
using Assets.Interface;
using Assets.Person;
using Assets.Scripts.InteractiveObjectSystem;
using Assets.Scripts.UI.Widgets;
using Assets.Utils;
using UnityEngine;
using AnimationState = Assets.Scripts.AnimationComponent.AnimationState;
using Random = UnityEngine.Random;

namespace Assets.Fight
{
    public class Fight : IDisposable
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly List<UnitAttackPresenter> _enemyAttackPresenters;
        private readonly UnitAttackPresenter _playerAttackPresenter;
        private readonly IStepFightView _stepFightView;
        private readonly DicePresenterAdapter _dicePresenterAdapter;
        private readonly IElementsDamagePanel _elementsDamagePanel;
        private readonly GameObject _popupReady;
        private readonly CustomButton _customButtonReady;
        private readonly ElementsSpriteView _elementsSpriteView;
        private readonly Queue<UnitAttackPresenter> _unitsOfQueue;
        
        private int _countSteps = 10;
        private Coroutine _coroutine;
        private bool _userIsReady;

        public Fight(ICoroutineRunner coroutineRunner, List<UnitAttackPresenter> enemyAttackPresenters,
            UnitAttackPresenter playerAttackPresenter, IStepFightView stepFightView,
            DicePresenterAdapter dicePresenterAdapter, IElementsDamagePanel elementsDamagePanel, GameObject popupReady,
            CustomButton customButtonReady, ElementsSpriteView elementsSpriteView)
        {
            _coroutineRunner = coroutineRunner;
            _enemyAttackPresenters = enemyAttackPresenters;
            _playerAttackPresenter = playerAttackPresenter;
            _stepFightView = stepFightView;
            _dicePresenterAdapter = dicePresenterAdapter;
            _elementsDamagePanel = elementsDamagePanel;
            _popupReady = popupReady;
            _customButtonReady = customButtonReady;
            _elementsSpriteView = elementsSpriteView;

            _customButtonReady.onClick.AddListener(GetUserAnswer);
            _unitsOfQueue = new Queue<UnitAttackPresenter>();

            SubscribeOnDieEnemies();
            _playerAttackPresenter.Unit.Died += ActionAfterDie;
            
            GenerateAttackingSteps(enemyAttackPresenters, playerAttackPresenter);
        }

        public void Dispose()
        {
            _customButtonReady.onClick.RemoveListener(GetUserAnswer);
            _dicePresenterAdapter.Dispose();
        }

        public void Start()
        {
            if (_coroutine != null)
                _coroutineRunner.StopCoroutine(_coroutine);

            _coroutine = _coroutineRunner.StartCoroutine(AnimateAttackCoroutine());
        }

        private IEnumerator AnimateAttackCoroutine()
        {
            EnemyViewChooser enemyChooser = new EnemyViewChooser(_elementsDamagePanel, _playerAttackPresenter.Unit as Player.Player, _elementsSpriteView);
            WaitUntil waitUntil = new WaitUntil(_dicePresenterAdapter.CheckOnDicesShuffeled);
            WaitUntil untilChooseEnemy = new WaitUntil(enemyChooser.TryChooseEnemy);
            WaitUntil getUserAnswer = new WaitUntil(() => _userIsReady);
            _playerAttackPresenter.ShowAnimation(AnimationState.Idle);

            foreach (UnitAttackPresenter enemyAttackPresenter in _enemyAttackPresenters)
                enemyAttackPresenter.ShowAnimation(AnimationState.Idle);
            
            _dicePresenterAdapter.SetDisactive();
            yield return getUserAnswer;
            
            while (_enemyAttackPresenters.Count > 0 && _playerAttackPresenter.Unit.IsDie == false)
            {
                if (_unitsOfQueue.Count <= 0)
                    GenerateAttackingSteps(_enemyAttackPresenters, _playerAttackPresenter);

                UnitAttackPresenter unitAttackPresenter = _unitsOfQueue.Dequeue();

                _dicePresenterAdapter.SetDisactive();

                if (unitAttackPresenter.Unit is Player.Player player)
                {
                    yield return untilChooseEnemy;
                    
                    _dicePresenterAdapter.SetActive();
                    yield return waitUntil;

                    _dicePresenterAdapter.RestartShuffelValue();

                    #region Show enemy info in console

                    Debug.Log("Ходит игрок жизни врагов = ");
                    int i = 1;
                    foreach (UnitAttackPresenter unit in _enemyAttackPresenters)
                        Debug.Log($"{i} = {unit.Unit.Healh}");

                    #endregion

                    bool isSplashAttack = _dicePresenterAdapter.LeftDiceValue == enemyChooser.Weapon.ChanceToSplash;

                    yield return _coroutineRunner.StartCoroutine(
                        StartSingleAnimationCoroutine(AnimationState.Attack, _playerAttackPresenter));
                    _playerAttackPresenter.ShowAnimation(AnimationState.Idle);

                    List<UnitAttackPresenter> allLiveEnemy =
                        _enemyAttackPresenters.Where(x => x.Unit.IsDie == false).ToList();

                    if (isSplashAttack)
                    {
                        yield return _coroutineRunner.StartCoroutine(
                            StartMultipleAnimationCoroutine(AnimationState.Hit, allLiveEnemy));

                        foreach (UnitAttackPresenter enemy in allLiveEnemy)
                        {
                            enemy.Unit.TakeDamage(enemyChooser.Weapon);

                            if (enemy.Unit.IsDie)
                                enemy.ShowAnimation(AnimationState.Dei);
                            else
                                enemy.ShowAnimation(AnimationState.Idle);
                        }
                    }
                    else
                    {
//                        UnitAttackPresenter randomEnemy =
//                            _enemyAttackPresenters[Random.Range(0, allLiveEnemy.Count - 1)];

                        UnitAttackPresenter enemy = _enemyAttackPresenters.FirstOrDefault(x => x.UnitAttackView == enemyChooser.AttackView);
                        
                        if (enemy is null)
                            throw new NullReferenceException("выбранный враг is null");
                        
                        yield return _coroutineRunner.StartCoroutine(
                            StartSingleAnimationCoroutine(AnimationState.Hit, enemy));

                        enemy.Unit.TakeDamage(enemyChooser.Weapon);

                        if (enemy.Unit.IsDie)
                            enemy.ShowAnimation(AnimationState.Dei);
                        else
                            enemy.ShowAnimation(AnimationState.Idle);
                    }
                    
                    Debug.Log("Игрок походил жизни врагов = ");
                    int j = 1;
                    foreach (UnitAttackPresenter unit in _enemyAttackPresenters)
                        Debug.Log($"{j} = {unit.Unit.Healh}");
                }
                else if (unitAttackPresenter.Unit is Enemy.Enemy enemy)
                {
                    Debug.Log($"Ходит враг жизни игрока = {_playerAttackPresenter.Unit.Healh}");

                    yield return _coroutineRunner.StartCoroutine(
                        StartSingleAnimationCoroutine(AnimationState.Attack, unitAttackPresenter));
                    unitAttackPresenter.ShowAnimation(AnimationState.Idle);

                    _playerAttackPresenter.Unit.TakeDamage(enemy.Weapon);

                    yield return _coroutineRunner.StartCoroutine(
                        StartSingleAnimationCoroutine(AnimationState.Hit, _playerAttackPresenter));

                    if (_playerAttackPresenter.Unit.IsDie)
                        _playerAttackPresenter.ShowAnimation(AnimationState.Dei);
                    else
                        _playerAttackPresenter.ShowAnimation(AnimationState.Idle);

                    Debug.Log($"Враг походил жизни игрока = {_playerAttackPresenter.Unit.Healh}");
                }
            }
            Debug.Log("Конец боя");
        }

        private void GetUserAnswer()
        {
            _popupReady.gameObject.SetActive(false);
            _userIsReady = true;
        }

        private IEnumerator StartSingleAnimationCoroutine(AnimationState animationState,
            UnitAttackPresenter attackPresenter)
        {
            Debug.Log("Старт StartAnimationCoroutine");

            attackPresenter.ShowAnimation(animationState);

            yield return new WaitUntil(() => attackPresenter.UnitAttackView.IsComplete);
            Debug.Log("Конец StartAnimationCoroutine");
        }

        private IEnumerator StartMultipleAnimationCoroutine(AnimationState animationState,
            List<UnitAttackPresenter> attackPresenters)
        {
            Debug.Log("Старт StartMultipleAnimationCoroutine");

            attackPresenters.ForEach(unitAttackPresenter => unitAttackPresenter.ShowAnimation(animationState));

            yield return new WaitUntil(
                () => attackPresenters.All(unitAttackPresenter => unitAttackPresenter.UnitAttackView.IsComplete)
            );

            Debug.Log("Конец StartMultipleAnimationCoroutine");
        }

        private void GenerateAttackingSteps(List<UnitAttackPresenter> enemyAttackPresenters,
            UnitAttackPresenter playerAttackPresenter)
        {
            _unitsOfQueue.Clear();
            List<UnitAttackPresenter> persons = new List<UnitAttackPresenter>();

            foreach (UnitAttackPresenter enemyAttackPresenter in enemyAttackPresenters)
                persons.Add(enemyAttackPresenter);

            persons.Add(playerAttackPresenter);

            for (int i = 0; i < _countSteps; i++)
            {
                UnitAttackPresenter unitAttackPresenter = persons[Random.Range(0, persons.Count)];
                _stepFightView.SetSprite(unitAttackPresenter.Unit.Sprite, i);
                _unitsOfQueue.Enqueue(unitAttackPresenter);
            }
        }

        private void SubscribeOnDieEnemies()
        {
            foreach (UnitAttackPresenter unitAttackPresenter in _enemyAttackPresenters)
                unitAttackPresenter.Unit.Died += ActionAfterDie;
        }

        private void ActionAfterDie(Unit unit)
        {
            if (_playerAttackPresenter.Unit == unit)
            {
                Debug.Log("Я вмЭр");
                StartSingleAnimationCoroutine(AnimationState.Dei, _playerAttackPresenter);
            }
            else
            {
                Debug.Log("враг вмЭр");
                UnitAttackPresenter enemyPresenter = _enemyAttackPresenters.FirstOrDefault(x => x.Unit == unit);
                _enemyAttackPresenters.Remove(enemyPresenter);

                GenerateAttackingSteps(_enemyAttackPresenters, _playerAttackPresenter);
                StartSingleAnimationCoroutine(AnimationState.Dei, enemyPresenter);
            }
        }
    }
}