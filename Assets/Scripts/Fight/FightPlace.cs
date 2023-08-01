using System.Collections.Generic;
using Assets.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Fight
{
    public class FightPlace : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private List<SpawnPoint> _spawnPoints;
        [SerializeField] private StepFightView _stepFightView;

        [SerializeField] private Button _button1;
        [SerializeField] private Button _button2;
        [SerializeField] private Button _button3;
        
        private const int Enemy = 1;
        private const int TwoEnemy = 2;
        private const int ThreeEnemy = 3;
        private const int Boss = 4;

        public void Set(Player.Player player, List<Enemy.Enemy> _enemies)
        {
            switch (_enemies.Count)
            {
                case Enemy:
                    DisableAllEnemyUI();

                    if (CheckOnTheBoss(_enemies[Enemy - 1]))
                        break;
                    
                    ShowEnemyUI(_spawnPoints[Enemy - 1]);
                    break;
                case TwoEnemy:
                    DisableAllEnemyUI();
                    ShowEnemyUI(_spawnPoints[TwoEnemy - 1]);
                    break;
                case ThreeEnemy:
                    DisableAllEnemyUI();
                    ShowEnemyUI(_spawnPoints[ThreeEnemy - 1]);
                    break;
            }
            
            Fight fight = new Fight(this, _enemies, player, _stepFightView);
            
            fight.Start();
        }

        private bool CheckOnTheBoss(Enemy.Enemy enemy)
        {
            if (enemy.Boss)
            {
                ShowEnemyUI(_spawnPoints[Boss - 1]);
                return true;
            }
            
            return false;
        }

        private void ShowEnemyUI(SpawnPoint spawnPoint) =>
            spawnPoint.gameObject.SetActive(true);

        private void DisableAllEnemyUI()
        {
            foreach (SpawnPoint spawnPoint in _spawnPoints)
                spawnPoint.gameObject.SetActive(false);
        }
    }
}