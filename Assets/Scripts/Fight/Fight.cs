using System.Collections.Generic;
using Assets.Interface;
using UnityEngine;

namespace Assets.Fight
{
    public class Fight
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly List<Enemy.Enemy> _enemies;
        private readonly Player.Player _player;
        private readonly IStepFightView _stepFightView;

        private readonly Queue<Person.Unit> _persons;
        private int _countSteps = 10;

        public Fight(ICoroutineRunner coroutineRunner, List<Enemy.Enemy> enemies, Player.Player player, IStepFightView stepFightView)
        {
            _coroutineRunner = coroutineRunner;
            _enemies = enemies;
            _player = player;
            _stepFightView = stepFightView;
            _persons = new Queue<Person.Unit>();
            
            SubscribeOnDieEnemies();
            
            GenerateAttackingSteps(enemies, player);
        }

        public void Start()
        {
            while (_enemies.Count > 0 || _player.Healh > 0)
            {
                if (_persons.Count <= 0)
                    GenerateAttackingSteps(_enemies, _player);

                Person.Unit unit = _persons.Dequeue();

                if (unit is Player.Player player)
                {
                    foreach (Enemy.Enemy enemy in _enemies)
                    {
                        enemy.TakeDamage(player.Weapon.DamageData);
                    }
                }
                else if (unit is Enemy.Enemy enemy)
                {
                    _player.TakeDamage(enemy.Weapon.DamageData);
                }
            }
        }

        private void GenerateAttackingSteps(List<Enemy.Enemy> enemies, Player.Player player)
        {
            List<Person.Unit> persons = new List<Person.Unit>();
            persons.AddRange(enemies);
            persons.Add(player);

            for (int i = 0; i < _countSteps; i++)
            {
                Person.Unit unit = persons[Random.Range(0, persons.Count)];
                _stepFightView.SetSprite(unit.Sprite, i);
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

        private void RemoveFromList(Person.Unit unit)
        {
            if (unit is Enemy.Enemy enemy)
                _enemies.Remove(enemy);
        }
    }
}