using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GenerationSystem
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Transform _conteiner;
        [SerializeField] private int _maxCountOfSpawnItems = 20;
        [SerializeField] private GameObject _bossTemplate;
        [SerializeField] private SpawnerItem _randomEvent;
        [SerializeField] private SpawnerItem _randomLoot;
        [SerializeField] private SpawnerItem _enemy;

        private GridSpace[,] _grid;
        private List<Vector2> _spawnPosition = new List<Vector2>();
        private Vector2 _bossPosition;
        private int _numberOfObjectsCreated;
        
        public void Init(GridSpace[,] grid)
        {
            _grid = grid;

            CreateSpawnPosition(_grid);
            ShuffleList(_spawnPosition);
            
            SpawnObjects(_randomEvent);
            SpawnObjects(_randomLoot);
            SpawnObjects(_enemy);
            SpawnBoss(_bossPosition);
        }

        private void CreateSpawnPosition(GridSpace[,] grid)
        {
            Vector2 offset = new Vector2(_grid.GetLength(0), _grid.GetLength(1)) / 2.0f;
            float gridOffset = 0.5f;
            
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == GridSpace.Floor)
                    {
                        _spawnPosition.Add(new Vector2(i+gridOffset, j+gridOffset) - offset);
                    }
                    else if (grid[i, j] == GridSpace.Last)
                    {
                        _bossPosition = new Vector2(i + gridOffset, j + gridOffset) - offset;
                    }
                }
            }
        }
        
        private void ShuffleList(List<Vector2> list)
        {
            int upperIndex = list.Count - 1;
            int lowerIndex = 0;
            int coefficientOfMixing = 5;
            int numberOfIterations = coefficientOfMixing * list.Count;

            int oldIndex;
            int newIndex;

            for (int i = 0; i < numberOfIterations; i++)
            {
                oldIndex = Random.Range(lowerIndex, upperIndex + 1);
                newIndex = Random.Range(lowerIndex, upperIndex + 1);

                (list[oldIndex], list[newIndex]) = (list[newIndex], list[oldIndex]);
            }
        }

        private void SpawnObjects(SpawnerItem items)
        {
            int countToInstantiate = 0;

            for (int i = 0; i < items.MaxCount; i++)
            {
                float chance = Random.value;

                if (chance <= items.ChanceSpawn)
                {
                    countToInstantiate++;
                }
            }

            for (int i = 0; i < countToInstantiate; i++)
            {
                if (IsPossibleToInstantiate())
                {
                    var newPosition = _spawnPosition[0];
                    _spawnPosition.RemoveAt(0);
                    Instantiate(GetRandomObject(items.Template), newPosition, Quaternion.identity, _conteiner);
                    _numberOfObjectsCreated++;
                }
            }
        }

        private bool IsPossibleToInstantiate()
        {
            return _numberOfObjectsCreated < _maxCountOfSpawnItems && _spawnPosition.Count > 0;
        }

        private GameObject GetRandomObject(GameObject[] gameObjects)
        {
            return gameObjects[Random.Range(0, gameObjects.Length)];
        }

        private void SpawnBoss(Vector2 position)
        {
            Instantiate(_bossTemplate, position, Quaternion.identity, _conteiner);
        }
    }

    [System.Serializable]
    public class SpawnerItem
    {
        [SerializeField] private GameObject[] _template;
        [SerializeField] private int _maxCount;
        [SerializeField] [Range(0, 1f)] private float _chanceSpawn = 0.05f;

        public GameObject[] Template => _template;
        public float ChanceSpawn => _chanceSpawn;
        public int MaxCount => _maxCount;
    }
}