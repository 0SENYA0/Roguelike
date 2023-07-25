using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.GenerationSystem
{
    [RequireComponent(typeof(LevelGenerator))]
    [RequireComponent(typeof(LevelView))]
    [RequireComponent(typeof(Spawner))]
    public class ProceduralGeneration : MonoBehaviour
    {
        private LevelGenerator _levelGenerator;
        private LevelView _levelView;
        private Spawner _spawner;
        
        private GridSpace[,] _grid;

        private void Start()
        {
            _levelGenerator = GetComponent<LevelGenerator>();
            _levelView = GetComponent<LevelView>();
            _spawner = GetComponent<Spawner>();
            
            _grid = _levelGenerator.GenerateLevel();
            _levelView.Init(_grid);
            _spawner.Init(_grid);

            //ShowDebugGrid();
        }

        private void ShowDebugGrid()
        {
            for (int i = 0; i < _grid.GetLength(0); i++)
            {
                string line = "";
                
                for (int j = 0; j < _grid.GetLength(1); j++)
                {
                    line += $"{(int)_grid[i, j]}; ";
                }
                
                Debug.Log(line);
            }
        }
    }
}