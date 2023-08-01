using System.Collections;
using System.Collections.Generic;
using Assets.Interface;
using Assets.Person;
using UnityEngine;

namespace Assets.Fight
{
    public class Fight
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly List<Enemy.Enemy> _enemies;
        private readonly Player.Player _player;
        private readonly IStepFightView _stepFightView;

        private readonly Queue<Unit> _persons;
        private int _countSteps = 10;
        private Coroutine _coroutine;

        public Fight(ICoroutineRunner coroutineRunner, List<Enemy.Enemy> enemies, Player.Player player, IStepFightView stepFightView)
        {
            _coroutineRunner = coroutineRunner;
            _enemies = enemies;
            _player = player;
            _stepFightView = stepFightView;
            _persons = new Queue<Unit>();

            SubscribeOnDieEnemies();

            GenerateAttackingSteps(enemies, player);
        }

        public void Start()
        {
            if (_coroutine != null)
                _coroutineRunner.StopCoroutine(_coroutine);

            _coroutine = _coroutineRunner.StartCoroutine(AnimateAttackCoroutine());
        }

        private IEnumerator AnimateAttackCoroutine()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(3);
            WaitUntil waitUntil = new WaitUntil(Test);

            while (_enemies.Count > 0 || _player.Healh > 0)
            {
                if (_persons.Count <= 0)
                    GenerateAttackingSteps(_enemies, _player);

                Unit unit = _persons.Dequeue();
                if (unit is Player.Player player)
                {
                    yield return waitUntil;
                    foreach (Enemy.Enemy enemy in _enemies)
                    {
                        enemy.TakeDamage(player.Weapon.DamageData);
                    }
                }
                else if (unit is Enemy.Enemy enemy)
                {
                    _player.TakeDamage(enemy.Weapon.DamageData);
                }

                yield return waitForSeconds;
            }
        }

        private bool Test()
        {
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
                //_stepFightView.SetSprite(unit.Sprite, i);
                _persons.Enqueue(unit);
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