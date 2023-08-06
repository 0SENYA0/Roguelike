using System;
using System.Linq;
using Assets.ScriptableObjects;
using Assets.Scripts.InteractiveObjectSystem;
using UnityEngine;

namespace Assets.Fight
{
    public class UIFight : MonoBehaviour
    {
        [SerializeField] private FightPlace _fightPlace;

        // private void OnEnable()
        // {
        //     SetActiveFightPlace(new Player.Player(100, null, null, null, null),
        //         new[] { new Enemy.Enemy(100, null, null, null, null) });
        // }

        public void SetActiveFightPlace(Player.Player player,params Enemy.Enemy[] _enemies)
        {
            gameObject.SetActive(true);
            _fightPlace.Set(player, _enemies.ToList());
        }
        
        public void SetActiveFightPlace(InteractiveEnemyObject enemyObject)
        {
            gameObject.SetActive(true);
            //TODO сделать фабрику для игрока и врага
            _fightPlace.Set(null, null);
        }
    }

}