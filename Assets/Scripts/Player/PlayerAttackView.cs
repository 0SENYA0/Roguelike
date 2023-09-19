using System.Collections.Generic;
using System.Linq;
using Assets.Person;
using Assets.UI;
using UnityEngine;

namespace Assets.Player
{
    public class PlayerAttackView : UnitAttackView
    {
        [SerializeField] private List<HealthBarView> _health;

        private List<HealthBarView> _activeHealth;
        protected override void OnPastEnable()
        {
            base.OnPastEnable();
            GetActiveHealth();
        }

        public override void ChangeUIHealthValue(float value)
        {
            float valuePerHeart = 1f / _health.Count;

            for (int i = 0; i < _health.Count; i++)
            {
                float temp = -value + valuePerHeart * (i + 1);
                
                if (0 < temp)
                    _health[i].HealthBar.fillAmount = 1;
                else
                    _health[i].HealthBar.fillAmount = temp;
            }
        }
        
        private void GetActiveHealth()
        {
            _activeHealth = _health.Where(healthBarView => healthBarView.gameObject.activeSelf).ToList();
            //_currentHealthImage = _activeHealth[_activeHealth.Count - 1];
        }
    }
}