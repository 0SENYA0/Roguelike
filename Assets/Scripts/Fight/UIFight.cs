using System;
using UnityEngine;

namespace Fight
{
    public class UIFight : MonoBehaviour
    {
        [SerializeField] private FightPlace _fightPlace;
        [SerializeField] private int _countEnenmy;
        
        private void OnEnable()
        {
            switch (_countEnenmy)
            {
                case 1:
                    _fightPlace.SetOneEnemyPlace();
                    break;
                case 2:
                    _fightPlace.SetTwoEnemyPlace();
                    break;
                case 3:
                    _fightPlace.SetThreeEnemyPlace();
                    break;
                case 4:
                    _fightPlace.SetBossEnemyPlace();
                    break;
                default:
                    _fightPlace.SetBossEnemyPlace();
                    break;
            }
        }
    }
}