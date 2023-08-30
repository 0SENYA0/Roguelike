using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Enemy;
using Assets.Fight.Dice;
using Assets.Interface;
using Assets.Person;
using Assets.Player;
using UnityEngine;
using AnimationState = Assets.Scripts.AnimationComponent.AnimationState;
using Random = UnityEngine.Random;

namespace Assets.Fight
{
    public class Fight : IDisposable
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IReadOnlyList<EnemyFightPresenter> _enemyPresenters;
        private readonly PlayerFightPresenter _playerPresenter;
        private readonly IStepFightView _stepFightView;
        private readonly DicePresenterAdapter _dicePresenterAdapter;
        private readonly Queue<Unit> _unitStackOfAttack;
        private int _countSteps = 10;
        private Coroutine _coroutine;
        private Coroutine _animationCoroutine;
        private bool _isCompleteAnimation;

        public Fight(ICoroutineRunner coroutineRunner, IReadOnlyList<EnemyFightPresenter> enemyPresenters, PlayerFightPresenter playerPresenter, IStepFightView stepFightView,
            DicePresenterAdapter dicePresenterAdapter)
        {
            _coroutineRunner = coroutineRunner;
            _enemyPresenters = enemyPresenters;
            _playerPresenter = playerPresenter;
            _stepFightView = stepFightView;
            _dicePresenterAdapter = dicePresenterAdapter;
            _unitStackOfAttack = new Queue<Unit>();

            SubscribeOnDieEnemies();

            GenerateAttackingSteps(enemyPresenters, playerPresenter.Player);
        }

        public void Dispose() =>
            _dicePresenterAdapter.Dispose();

        public void Start()
        {
            if (_coroutine != null)
                _coroutineRunner.StopCoroutine(_coroutine);

            UnitFightViewAdapter PlayerFightViewAdapter = new UnitFightViewAdapter(_playerPresenter.PlayerView, 
                _playerPresenter.Player.SpriteAnimation);
            
            foreach (EnemyFightPresenter enemyPresenter in _enemyPresenters)
            {
                UnitFightViewAdapter enemyFightViewAdapter = new UnitFightViewAdapter(enemyPresenter.UnitFightView, 
                    enemyPresenter.Enemy.SpriteAnimation);
            }            
            _coroutine = _coroutineRunner.StartCoroutine(AnimateAttackCoroutine());
        }

        private IEnumerator AnimateAttackCoroutine()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(3);
            WaitUntil waitUntil = new WaitUntil(_dicePresenterAdapter.CheckOnDicesShuffeled);
            
            foreach (EnemyFightPresenter enemyPresenter in _enemyPresenters)
                enemyPresenter.UnitFightView.SetClip(AnimationState.Idle);

            _playerPresenter.PlayerView.SetClip(AnimationState.Idle);
            
            while (_enemyPresenters.All(x => x.Enemy.IsDie == false) && _playerPresenter.Player.IsDie == false)
            {
                if (_unitStackOfAttack.Count <= 0)
                    GenerateAttackingSteps(_enemyPresenters, _playerPresenter.Player);

                Unit unit = _unitStackOfAttack.Dequeue();

                if (unit is Player.Player player)
                {
                    yield return waitUntil;
                    // Получить данные с первого кубика
                    Debug.Log("данные с первого кубика " + _dicePresenterAdapter.LeftDiceValue);
                    // Получить данные со второго кубика
                    Debug.Log("данные с второго кубика " + _dicePresenterAdapter.CenterDiceValue);
                    // Получить данные с третьего кубика
                    Debug.Log("данные с третьего кубика " + _dicePresenterAdapter.RightDiceValue);

                    // Проиграть анимацию атаки игрока

                    // yield return _coroutineRunner.StartCoroutine(StartAnimationCoroutine(AnimationState.Attack, _playerPresenter));

                    // yield return _coroutineRunner.StartCoroutine(StartAnimationCoroutine(AnimationState.Hit, _enemyPresenters.ToArray()));

                    // Нанести урон одному врагу || Нанести урон нескольким врагам
                    Debug.Log("Нанесли урон врагам");

                    Debug.Log("Ходит игрок");

                    // enemy.TakeDamage(player.Weapon.DamageData);
                }
                else if (unit is Enemy.Enemy enemy)
                {
                    _dicePresenterAdapter.SetDisactive();

                    Debug.Log("Ходит враг");
                    //_player.TakeDamage(enemy.Weapon.DamageData);
                }

                yield return waitForSeconds;
            }
        }

        private IEnumerator StartAnimationCoroutine(AnimationState animationState,  PlayerFightPresenter presenters)
        {
            presenters.PlayerView.SetClip(animationState);
            presenters.PlayerView.OnAnimationComplete += SwitchNextAnimation;

            while (SpriteAnimationOAnimationComplete())
            {
                yield return null;
            }   
        }

        private bool SpriteAnimationOAnimationComplete() =>
            _isCompleteAnimation;

        private void SwitchNextAnimation() =>
            _isCompleteAnimation = true;

        private void GenerateAttackingSteps(IReadOnlyList<EnemyFightPresenter> enemiesPresenters, Player.Player player)
        {
            List<Unit> persons = new List<Unit>();
            persons.AddRange(enemiesPresenters.Select(x => x.Enemy));
            persons.Add(player);

            for (int i = 0; i < _countSteps; i++)
            {
                Unit unit = persons[Random.Range(0, persons.Count)];
                _stepFightView.SetSprite(unit.Sprite, i);
                _unitStackOfAttack.Enqueue(unit);
            }
        }

        private void SubscribeOnDieEnemies()
        {
            foreach (EnemyFightPresenter enemyFightPresenter in _enemyPresenters)
                enemyFightPresenter.Enemy.Died += ActionAfterDie;
        }

        private void ActionAfterDie(Unit unit) =>
            Debug.Log("Я вмЭр");
    }
}