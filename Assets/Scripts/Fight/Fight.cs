using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Enemy;
using Assets.Fight.Dice;
using Assets.Interface;
using Assets.Person;
using Assets.Player;
using Assets.Scripts.UI.Widgets;
using Assets.Utils;
using DefaultNamespace.Tools;
using UnityEngine;
using AnimationState = Assets.Scripts.AnimationComponent.AnimationState;

namespace Assets.Fight
{
    public class Fight : IDisposable
    {
        private readonly ICoroutineRunner _coroutineRunner;                 // основная карутина
        private readonly List<EnemyAttackPresenter> _enemyAttackPresenters; //  
        private readonly PlayerAttackPresenter _playerAttackPresenter;      // сам игрок
        private readonly IStepFightView _stepFightView;                     // верхняя плашка ходов
        private readonly DicePresenterAdapter _dicePresenterAdapter;        // кубики
        private readonly PlayerWeaponPanel _elementsDamagePanel;            // инвентарь
        private readonly GameObject _popupReady;                            // окошка "готов?"
        private readonly CustomButton _customButtonReady;                   // кнопка в окошке "готов?"
        private readonly Queue<UnitAttackPresenter> _unitsOfQueue;          // очередь атак

        private readonly int _countSteps = 10;
        private Coroutine _coroutine;
        private bool _userIsReady;

        public Action FightEnded;
        private readonly GeneratorAttackSteps _generatorAttackSteps;

        public Fight(ICoroutineRunner coroutineRunner, 
            List<EnemyAttackPresenter> enemyAttackPresenters,
            PlayerAttackPresenter playerAttackPresenter, 
            IStepFightView stepFightView,
            DicePresenterAdapter dicePresenterAdapter, 
            PlayerWeaponPanel elementsDamagePanel, 
            GameObject popupReady,
            CustomButton customButtonReady)
        {
            _coroutineRunner = coroutineRunner;
            _enemyAttackPresenters = enemyAttackPresenters;
            _playerAttackPresenter = playerAttackPresenter;
            _stepFightView = stepFightView;
            _dicePresenterAdapter = dicePresenterAdapter;
            _elementsDamagePanel = elementsDamagePanel;
            _popupReady = popupReady;
            _customButtonReady = customButtonReady;

            _customButtonReady.onClick.AddListener(GetUserAnswer);
            _unitsOfQueue = new Queue<UnitAttackPresenter>();

            SubscribeOnDieEnemies();
            _playerAttackPresenter.Unit.Died += ActionAfterDie;

            _generatorAttackSteps = new GeneratorAttackSteps();
            
            _generatorAttackSteps.GenerateAttackingSteps(_enemyAttackPresenters, _playerAttackPresenter, _unitsOfQueue, _countSteps, _stepFightView);
        }

        public void Dispose()
        {
            _customButtonReady.onClick.RemoveListener(GetUserAnswer);
            _dicePresenterAdapter.Dispose();

            UnSubscribeOnDieEnemies();
            _playerAttackPresenter.Unit.Died -= ActionAfterDie;
        }

        public void Start()
        {
            if (_coroutine != null)
                _coroutineRunner.StopCoroutine(_coroutine);

            _coroutine = _coroutineRunner.StartCoroutine(AnimateAttackCoroutine());
        }

        private IEnumerator AnimateAttackCoroutine()
        {
            WaitUntil waitDiceChooseUntil = new WaitUntil(_dicePresenterAdapter.CheckOnDicesShuffeled);
            WaitUntil getUserAnswer = new WaitUntil(() => _userIsReady);
            
            # region Влючаем у всех Idle
            _playerAttackPresenter.ShowAnimation(AnimationState.Idle);
            foreach (UnitAttackPresenter enemyAttackPresenter in _enemyAttackPresenters)
                enemyAttackPresenter.ShowAnimation(AnimationState.Idle);
            # endregion

            _dicePresenterAdapter.SetDisactive();
            yield return getUserAnswer;

            _stepFightView.Show();
            _elementsDamagePanel.Init(_playerAttackPresenter.Inventory.InventoryModel.GetWeapon());
            
            while (_enemyAttackPresenters.Count > 0 && _playerAttackPresenter.Unit.IsDie == false)
            {
                // Если кончилась очередь, генерируем новую
                if (_unitsOfQueue.Count <= 0)
                    _generatorAttackSteps.GenerateAttackingSteps(_enemyAttackPresenters, _playerAttackPresenter, _unitsOfQueue, _countSteps, _stepFightView);

                UnitAttackPresenter unitAttackPresenter = _unitsOfQueue.Dequeue();

                _dicePresenterAdapter.SetDisactive();

                if (unitAttackPresenter.Unit is Player.Player player)
                {
                    # region выбираем врага
                    EnemyViewChooser enemyChooser = new EnemyViewChooser(_elementsDamagePanel);

                    foreach (EnemyAttackPresenter enemyAttackPresenter in _enemyAttackPresenters)
                        enemyAttackPresenter.EnemyAttackView.PlayParticleEffect();

                    yield return new WaitUntil(enemyChooser.TryChooseEnemy);
                    # endregion
                    
                    # region жмякаем кубики
                    foreach (EnemyAttackPresenter enemyAttackPresenter in _enemyAttackPresenters)
                        enemyAttackPresenter.EnemyAttackView.StopParticleEffect();

                    _dicePresenterAdapter.SetActive();
                    yield return waitDiceChooseUntil;
                    # endregion

                    # region все выбрали, переходим в Idle

                    _dicePresenterAdapter.RestartShuffelValue();

                    yield return _coroutineRunner.StartCoroutine(StartSingleAnimationCoroutine(AnimationState.Attack, _playerAttackPresenter));
                    _playerAttackPresenter.ShowAnimation(AnimationState.Idle);
                    # endregion
                    
                    # region применяем атаку/атаки
                    
                    bool isSplashAttack = _dicePresenterAdapter.LeftDiceValue == enemyChooser.Weapon.ChanceToSplash;
                    
                    if (isSplashAttack)
                    {
                        List<EnemyAttackPresenter> allLiveEnemy = _enemyAttackPresenters.Where(x => x.Unit.IsDie == false).ToList();
                        
                        yield return _coroutineRunner.StartCoroutine(StartMultipleAnimationCoroutine(AnimationState.Hit, allLiveEnemy));

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
                        UnitAttackPresenter enemy = _enemyAttackPresenters.FirstOrDefault(x => x.EnemyAttackView == enemyChooser.AttackView);

                        yield return _coroutineRunner.StartCoroutine(StartSingleAnimationCoroutine(AnimationState.Hit, enemy));

                        enemy.Unit.TakeDamage(enemyChooser.Weapon);

                        if (enemy.Unit.IsDie)
                            enemy.ShowAnimation(AnimationState.Dei);
                        else
                            enemy.ShowAnimation(AnimationState.Idle);
                    }
                    
                    enemyChooser.Dispose();
                    # endregion 
                }
                else if (unitAttackPresenter.Unit is Enemy.Enemy enemy)
                {
                    yield return _coroutineRunner.StartCoroutine(StartSingleAnimationCoroutine(AnimationState.Attack, unitAttackPresenter));
                    unitAttackPresenter.ShowAnimation(AnimationState.Idle);

                    _playerAttackPresenter.Unit.TakeDamage(enemy.Weapon);

                    yield return _coroutineRunner.StartCoroutine(StartSingleAnimationCoroutine(AnimationState.Hit, _playerAttackPresenter));

                    if (_playerAttackPresenter.Unit.IsDie)
                        _playerAttackPresenter.ShowAnimation(AnimationState.Dei);
                    else
                        _playerAttackPresenter.ShowAnimation(AnimationState.Idle);
                }
            }
            
            _elementsDamagePanel.Dispose();
            _stepFightView.Hide();
            FightEnded?.Invoke();
        }

        private void GetUserAnswer()
        {
            _popupReady.gameObject.SetActive(false);
            _userIsReady = true;
        }

        private IEnumerator StartSingleAnimationCoroutine(AnimationState animationState,
            UnitAttackPresenter attackPresenter)
        {
            attackPresenter.ShowAnimation(animationState);

            if (attackPresenter is EnemyAttackPresenter enemyAttackPresenter)
                yield return new WaitUntil(() => enemyAttackPresenter.EnemyAttackView.IsComplete);
            else if (attackPresenter is PlayerAttackPresenter playerAttackPresenter)
                yield return new WaitUntil(() => playerAttackPresenter.PlayerAttackView.IsComplete);
        }

        private IEnumerator StartMultipleAnimationCoroutine(AnimationState animationState,
            List<EnemyAttackPresenter> attackPresenters)
        {
            attackPresenters.ForEach(unitAttackPresenter => unitAttackPresenter.ShowAnimation(animationState));

            yield return new WaitUntil(() => attackPresenters.All(unitAttackPresenter => unitAttackPresenter.EnemyAttackView.IsComplete));
        }
        
        private void SubscribeOnDieEnemies()
        {
            foreach (UnitAttackPresenter unitAttackPresenter in _enemyAttackPresenters)
                unitAttackPresenter.Unit.Died += ActionAfterDie;
        }

        private void UnSubscribeOnDieEnemies()
        {
            foreach (UnitAttackPresenter unitAttackPresenter in _enemyAttackPresenters)
                unitAttackPresenter.Unit.Died -= ActionAfterDie;
        }

        private void ActionAfterDie(Unit unit)
        {
            if (_playerAttackPresenter.Unit == unit)
            {
                ConsoleTools.LogError("Я вмЭр");
                StartSingleAnimationCoroutine(AnimationState.Dei, _playerAttackPresenter);
            }
            else
            {
                ConsoleTools.LogError($"враг вмЭр: {unit}");
                EnemyAttackPresenter enemyPresenter = _enemyAttackPresenters.FirstOrDefault(x => x.Unit == unit);
                _enemyAttackPresenters.Remove(enemyPresenter);

                _generatorAttackSteps.GenerateAttackingSteps(_enemyAttackPresenters, _playerAttackPresenter, _unitsOfQueue, _countSteps, _stepFightView);
                _coroutineRunner.StartCoroutine(StartSingleAnimationCoroutine(AnimationState.Dei, enemyPresenter));
                enemyPresenter.EnemyAttackView.HideViewObject();
            }
        }
    }
}