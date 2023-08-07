using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Fight.Dice;
using Assets.Interface;
using Assets.Person;
using Assets.Person.PersonStates;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Fight
{
    public class Fight : IDisposable
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly List<Enemy.Enemy> _enemies;
        private readonly Player.Player _player;
        private readonly IStepFightView _stepFightView;
        private readonly DicePresenterAdapter _dicePresenterAdapter;
        private readonly Queue<Unit> _unitStackOfAttack;
        private int _countSteps = 10;
        private Coroutine _coroutine;

        public Fight(ICoroutineRunner coroutineRunner, List<Enemy.Enemy> enemies, Player.Player player, IStepFightView stepFightView,
            DicePresenterAdapter dicePresenterAdapter)
        {
            _coroutineRunner = coroutineRunner;
            _enemies = enemies;
            _player = player;
            _stepFightView = stepFightView;
            _dicePresenterAdapter = dicePresenterAdapter;
            _unitStackOfAttack = new Queue<Unit>();

            SubscribeOnDieEnemies();

            GenerateAttackingSteps(enemies, player);
        }

        public void Dispose() =>
            _dicePresenterAdapter.Dispose();

        public void Start()
        {
            if (_coroutine != null)
                _coroutineRunner.StopCoroutine(_coroutine);

            _coroutine = _coroutineRunner.StartCoroutine(AnimateAttackCoroutine());
        }

        private IEnumerator AnimateAttackCoroutine()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(3);
            WaitUntil waitUntil = new WaitUntil(_dicePresenterAdapter.CheckOnDicesShuffeled);

            while (_enemies.Any(x => x.IsDie == false) && _player.IsDie == false)
            {
                if (_unitStackOfAttack.Count <= 0)
                    GenerateAttackingSteps(_enemies, _player);

                Unit unit = _unitStackOfAttack.Dequeue();

                if (unit is Player.Player player)
                {
                    yield return waitUntil;
                    foreach (Enemy.Enemy enemy in _enemies)
                    {
                        Debug.Log("Ходит игрок");
                        enemy.PersonStateMachine.SetState(new PersonStateTakeDamage());
                        // enemy.TakeDamage(player.Weapon.DamageData);
                    }
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

        private bool AllEnemyIsDie()
        {
            foreach (var enemy in _enemies)
            {
                if (enemy.IsDie == false)
                    return false;
            }

            return true;
        }

        private void GenerateAttackingSteps(List<Enemy.Enemy> enemies, Player.Player player)
        {
            List<Unit> persons = new List<Unit>();
            persons.AddRange(enemies);
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
            foreach (Enemy.Enemy enemy in _enemies)
            {
                enemy.Died += RemoveFromList;
            }
        }

        private void RemoveFromList(Unit unit)
        {
            if (unit is Enemy.Enemy enemy)
                _enemies.Remove(enemy);
        }

    }
}