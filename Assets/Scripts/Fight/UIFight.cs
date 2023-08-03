using System;
using System.Linq;
using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.Fight
{
    public class UIFight : MonoBehaviour
    {
        [SerializeField] private FightPlace _fightPlace;

        private void OnEnable()
        {
            SetActiveFightPlace(new Player.Player(100, null, null, null, null),
                new[] { new Enemy.Enemy(100, null, null, null, null) });
        }

        public void SetActiveFightPlace(Player.Player player, Enemy.Enemy[] _enemies) =>
            _fightPlace.Set(player, _enemies.ToList());
    }

}