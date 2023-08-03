using System;
using System.Collections.Generic;
using Assets.Fight.Dice;
using Assets.Interface;
using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.Fight
{
    public class FightPlace : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private List<SpawnPoint> _spawnPoints;
        [SerializeField] private StepFightView _stepFightView;
        
        [Space(25)]
        [SerializeField] private DiceView _leftDice;
        [SerializeField] private DiceView _centerDice;
        [SerializeField] private DiceView _rightDice;

        private Fight _fight;

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
            
            DiceSpriteScriptableObject scriptableObject = Resources.Load<DiceSpriteScriptableObject>("BlackDiceSpriteScriptableObject");
            
            DicePresenter leftDicePresenter = new DicePresenter(_leftDice, new DiceModel(scriptableObject.Sprites), this);
            DicePresenter centerDicePresenter = new DicePresenter(_centerDice, new DiceModel(scriptableObject.Sprites), this);
            DicePresenter rightDicePresenter = new DicePresenter(_rightDice, new DiceModel(scriptableObject.Sprites), this);

            _fight = new Fight(this, _enemies, player, _stepFightView, new DicePresenterAdapter(leftDicePresenter, centerDicePresenter, rightDicePresenter));
            
            _fight.Start();
        }

        private void OnDisable() =>
            _fight.Dispose();

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