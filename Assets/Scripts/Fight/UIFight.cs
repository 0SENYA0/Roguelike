using System;
using System.Linq;
using Assets.Enemy;
using Assets.Player;
using Assets.ScriptableObjects;
using Assets.Scripts.InteractiveObjectSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Fight
{
    public class UIFight : MonoBehaviour
    {
        [SerializeField] private Transform _globalMap;
        [SerializeField] private Transform _fightMap;


        [SerializeField] private FightPlace _fightPlace;
        
        private PlayerPresenter _player;
        private EnemyPresenter _enemies;

        public void SetActiveFightPlace(PlayerPresenter player, EnemyPresenter enemies)
        {
            _enemies = enemies;
            _player = player;
            
            _globalMap.gameObject.SetActive(false);
            _fightMap.gameObject.SetActive(true);
            _fightPlace.Set(_player, _enemies);
        }
    }
}