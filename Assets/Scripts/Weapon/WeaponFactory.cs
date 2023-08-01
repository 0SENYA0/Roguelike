using UnityEngine;

namespace Assets.Weapon
{
    public class WeaponFactory
    {
        public Weapon Create(DamageData damageData, int chanceToSplash, int minValueToCriticalDamage, int valueModifier, ParticleSystem particleSystem) =>
            new Weapon(damageData, chanceToSplash, minValueToCriticalDamage, valueModifier, particleSystem);
        
        public Weapon Create(DamageData damageData, ParticleSystem particleSystem)
        {
            int chanceToSplash = 1;
            int minValueToCriticalDamage = 1;
            int valueModifier = 1;
            return new Weapon(damageData, chanceToSplash, minValueToCriticalDamage, valueModifier, particleSystem);
        }
    }
}