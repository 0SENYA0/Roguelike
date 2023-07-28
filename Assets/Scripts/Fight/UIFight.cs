using System.Linq;
using UnityEngine;

namespace Assets.Fight
{
    public class UIFight : MonoBehaviour
    {
        [SerializeField] private FightPlace _fightPlace;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                gameObject.SetActive(false);
        }

        public void SetActiveFightPlace(Player.Player player, Enemy.Enemy[] _enemies) =>
            _fightPlace.Set(player, _enemies.ToList());
    }

}