using Assets.Fight.Element;
using Assets.Interface;
using UnityEngine;

namespace Assets.Weapon
{
    public class WeaponBuilder
    {
        private readonly WeaponFactory _weaponFactory;

        public WeaponBuilder() =>
            _weaponFactory = new WeaponFactory();

        public IWeapon BuildStaffOfDeath()
        {
            // TODO или сделать загрузку спрайта через RESOURCES
            
            DamageData damageData = new DamageData(100, Element.Fire);
            int chanceToSplash = 3;
            int minValueToCriticalDamage = 2;
            int valueModifier = 1;
            
            //Weapon weapon = _weaponFactory.Create(damageData, chanceToSplash, minValueToCriticalDamage, valueModifier, sprite);
            return null;
        }
    }

}