using System.Collections.Generic;
using System.Linq;
using Assets.Person;
using Assets.UI;
using DefaultNamespace.Tools;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Assets.Player
{
    public class PlayerAttackView : UnitAttackView
    {
        [SerializeField] private List<Image> _heart;
        [SerializeField] private PlayerView _player;

        protected override void OnPastEnable()
        {
            base.OnPastEnable();
            ChangeUIHealthValue(0);
        }

        public override void ChangeUIHealthValue(float value)
        {
            int currentHeartsCount = ((int)_player.PlayerPresenter.Player.Healh) / 100 + 1;

            foreach (var heart in _heart)
            {
                heart.gameObject.SetActive(currentHeartsCount > 0);
                currentHeartsCount--;
            }
        }
    }
}