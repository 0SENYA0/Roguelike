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

        private readonly Queue<Person.Person> _persons;
        private int _countSteps = 10;

        public Fight(ICoroutineRunner coroutineRunner, List<Enemy.Enemy> enemies, Player.Player player)
        {
            _coroutineRunner = coroutineRunner;
            _enemies = enemies;
            _player = player;
            _persons = new Queue<Person.Person>();
            GenerateAttackingSteps(enemies, player);
        }

        public void Start()
        {
            while (_enemies.Count > 0)
            {
                if (_enemies.Count <= 0)
                    GenerateAttackingSteps(_enemies, _player);

                Person.Person person = _persons.Dequeue();

                if (person is Player.Player player)
                {
                    foreach (Enemy.Enemy enemy in _enemies)
                    {
                        enemy.TakeDamage(player.Weapon.AttackData.DamageData);
                    }
                }
                else if (person is Enemy.Enemy enemy)
                {
                    _player.TakeDamage(enemy.Weapon.AttackData.DamageData);
                }
            }
        }

        private void GenerateAttackingSteps(List<Enemy.Enemy> enemies, Player.Player player)
        {
            List<Person.Person> persons = new List<Person.Person>();
            persons.AddRange(enemies);
            persons.Add(player);

            for (int i = 0; i < _countSteps; i++)
            {
                Person.Person person = persons[Random.Range(0, persons.Count)];
                _persons.Enqueue(person);
            }
        }
    }
}