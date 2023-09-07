using Assets.DefendItems;
using Assets.Interface;
using Assets.ScriptableObjects;
using Assets.Weapon;
using UnityEngine;

namespace Assets.Scripts.InteractiveObjectSystem
{
    public class InteractiveLootObject : InteractiveObject
    {
        [SerializeField] private WeaponScriptableObject _weaponScriptableObject;
        [SerializeField] private ArmorScriptableObject _armorScriptableObject;

        private IWeapon _weapon;
        private Armor _armor;

        protected override void OnStart()
        {
            base.OnStart();
            ArmorFactory armorFactory = new ArmorFactory();
            _armor = armorFactory.Create(
                new Body(_armorScriptableObject.BodyPart.Value,
                    _armorScriptableObject.BodyPart.Element),
                new Head(_armorScriptableObject.HeadPart.Value),
                _armorScriptableObject.ParticleSystem);
            // if (Random.Range(0, 2) == 0)
            // {
            //     ArmorFactory armorFactory = new ArmorFactory();
            //     _armor = armorFactory.Create(
            //         new Body(_armorScriptableObject.BodyPart.Value,
            //             _armorScriptableObject.BodyPart.Element),
            //         new Head(_armorScriptableObject.HeadPart.Value),
            //         _armorScriptableObject.ParticleSystem);
            // }
            // else
            // {
            //     WeaponFactory weaponFactory = new WeaponFactory();
            //     _weapon = weaponFactory.Create(
            //         _weaponScriptableObject.Damage, _weaponScriptableObject.Element,
            //         _weaponScriptableObject.ChanceToSplash,
            //         _weaponScriptableObject.MinValueToCriticalDamage,
            //         _weaponScriptableObject.ValueModifier, _weaponScriptableObject.ParticleSystem);
            // }
        }

        public IWeapon Weapon => _weapon;
        public Armor Armor => _armor;
    }
}