using System.Collections;
using Assets.Person;
using Assets.Scripts.InteractiveObjectSystem;
using Assets.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Enemy
{
    public class EnemyAttackView : UnitAttackView
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private Image _weaponElement;
        [SerializeField] private Image _armorElement;
        [SerializeField] private ElementsSpriteView _elementsSpriteView;

        [SerializeField] private HealthBarView _health;

        public ElementsSpriteView ElementsSpriteView => _elementsSpriteView;

        public Image WeaponElement => _weaponElement;

        public Image ArmorElement => _armorElement;

        private BoxCollider2D _boxCollider;
        private Coroutine _coroutine;

        public override void ChangeUIHealthValue(float value)
        {
            _health.HealthBar.fillAmount = value;
        }
        
        public void HideViewObject()
        {
            _weaponElement.gameObject.SetActive(false); 
            _armorElement.gameObject.SetActive(false);    
            _health.gameObject.SetActive(false);
            _boxCollider.enabled = false;
        }
        
        public void PlayParticleEffect()
        {
            _particle.gameObject.SetActive(true);
            _particle.Play();
        }

        public void StopParticleEffect() => 
            _particle.gameObject.SetActive(false);
        
        protected override void OnPastAwake()
        {
            base.OnPastAwake();
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        protected override void OnPastEnable()
        {
            base.OnPastEnable();
            _boxCollider.enabled = true;
            _weaponElement.gameObject.SetActive(true); 
            _armorElement.gameObject.SetActive(true);
            _health.gameObject.SetActive(true);
        }
    }
}